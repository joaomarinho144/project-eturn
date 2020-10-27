using System;

namespace project
{
    class Program
    {
        static OperFilaFilaEventosNaoOrdenados FilaNaoOrdenada = new OperFilaFilaEventosNaoOrdenados();
        static OperFilaFilaEventosOrdenados FilaOrdenada = new OperFilaFilaEventosOrdenados();
        static TimeSpan horaInicioAlmoco = TimeSpan.Parse("12:00");
        static TimeSpan horaTerminoAlmoco = TimeSpan.Parse("13:00");
        static TimeSpan horaInicioNetworking = TimeSpan.Parse("17:00");
        static int DiaEvento = 1;

        static void Main(string[] args)
        {
            try
            {
                SolicitarEntrada();
                VerificarTempo();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ooops! Aconteceu um erro, tente novamente.\n Detalhes do erro: \n " + e);
            }

        }
        public static void SolicitarEntrada()
        {
            int totalAcumuladoMinutosEvento = 0;
            do
            {
                Console.Write("Digite o nome do evento: ");
                string nome = Console.ReadLine();
                Console.Write("Digite a duração: ");
                int duracao = Convert.ToInt32(Console.ReadLine());
                if ((totalAcumuladoMinutosEvento + duracao) <= 840)
                {
                    FilaNaoOrdenada.Inserir(duracao, nome, 0);
                    totalAcumuladoMinutosEvento += Convert.ToInt32(duracao);
                }
                else
                {
                    Console.WriteLine("Limite de tempo excedido!");
                }

            } while (totalAcumuladoMinutosEvento < 840);
        }

        public static void VerificarTempo()
        {
            int Tamanho = FilaNaoOrdenada.RecurarDadosTamanho();
            while (Tamanho > 0)
            {
                var InicioNaoOrdenado = FilaNaoOrdenada.RecurarDadosInicio();
                var FimOrdenada = FilaOrdenada.RecuperarDadosUltimoElemento();

                if (FimOrdenada == null || FimOrdenada.horarioTerminou == TimeSpan.Parse("18:00"))
                {
                    AdicionarPrimeiroElementoOrdenada();
                }
                else if (InicioNaoOrdenado.QntVezesReposicionado > 1)
                {
                    ReordenarElementosFila();
                }
                else if (VerificarAlmoco())
                {
                    AdicionarElementoOrdenada();
                }
                else if (FimOrdenada.horarioTerminou == TimeSpan.Parse("12:00"))
                {
                    FilaOrdenada.Inserir("Almoço", 60, TimeSpan.Parse("12:00"), TimeSpan.Parse("13:00"));
                }
                else if (FimOrdenada.horarioTerminou == TimeSpan.Parse("17:00"))
                {
                    FinalizarDia();
                }
                else
                {
                    ReposicionarElemento(InicioNaoOrdenado.duracao, InicioNaoOrdenado.nome, (InicioNaoOrdenado.QntVezesReposicionado + 1));
                }
                Tamanho = FilaNaoOrdenada.RecurarDadosTamanho();
            }
            FinalizarDia();
        }

        public static bool VerificarAlmoco()
        {
            var retorno = false;

            var InicioNaoOrdenado = FilaNaoOrdenada.RecurarDadosInicio();
            var FimOrdenada = FilaOrdenada.RecuperarDadosUltimoElemento();


            TimeSpan PossivelHorarioInicioEvento = FimOrdenada.horarioTerminou;
            TimeSpan PossivelHorarioFimEvento = FimOrdenada.horarioTerminou.Add(TimeSpan.FromMinutes(InicioNaoOrdenado.duracao));

            if ((PossivelHorarioFimEvento <= horaInicioAlmoco || PossivelHorarioFimEvento > horaTerminoAlmoco) && PossivelHorarioFimEvento <= horaInicioNetworking)
            {
                retorno = true;
            }

            TimeSpan verificacao1 = horaInicioAlmoco.Subtract(PossivelHorarioInicioEvento);
            TimeSpan verificacao2 = horaTerminoAlmoco.Subtract(PossivelHorarioFimEvento);

            if (verificacao1.Hours >= 0 && verificacao2.Hours < 0)
            {
                retorno = false;
            }

            return retorno;

        }


        public static void FinalizarDia()
        {
            FilaOrdenada.Inserir("Evento Networking", 60, TimeSpan.Parse("17:00"), TimeSpan.Parse("18:00"));
            Imprimir(DiaEvento);
        }

        public static void ReposicionarElemento(int duracao, string nome, int QntVezesReposicionado)
        {
            FilaNaoOrdenada.ReposicionarElemento(duracao, nome, QntVezesReposicionado);
        }

        public static void ReordenarElementosFila()
        {
            var FimOrdenada = FilaOrdenada.RecuperarDadosUltimoElemento();
            FilaNaoOrdenada.Inserir(FimOrdenada.duracao, FimOrdenada.nome, 0);
            FilaOrdenada.Reorganizar(FimOrdenada.horarioComecou, FimOrdenada.horarioTerminou);
            FilaOrdenada.RetiraElemento();
            FilaNaoOrdenada.AtualizarQntVezes();
        }

        public static void Imprimir(int dia)
        {
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("Dia " + dia);
            FilaOrdenada.MostraFila();
            FilaOrdenada.EsvaziarFila();
            DiaEvento++;
        }

        public static void AdicionarElementoOrdenada()
        {
            var InicioNaoOrdenado = FilaNaoOrdenada.RecurarDadosInicio();
            var FimOrdenada = FilaOrdenada.RecuperarDadosUltimoElemento();
            TimeSpan horaterminou = FimOrdenada.horarioTerminou.Add(TimeSpan.FromMinutes(InicioNaoOrdenado.duracao));
            FilaOrdenada.Inserir(InicioNaoOrdenado.nome, InicioNaoOrdenado.duracao, FimOrdenada.horarioTerminou, horaterminou);
            FilaNaoOrdenada.RetiraElemento();
        }

        public static void AdicionarPrimeiroElementoOrdenada()
        {
            var InicioNaoOrdenado = FilaNaoOrdenada.RecurarDadosInicio();
            TimeSpan horainicio = TimeSpan.Parse("09:00");
            TimeSpan horaterminou = horainicio.Add(TimeSpan.FromMinutes(InicioNaoOrdenado.duracao));
            FilaOrdenada.Inserir(InicioNaoOrdenado.nome, InicioNaoOrdenado.duracao, horainicio, horaterminou);
            FilaNaoOrdenada.RetiraElemento();
        }
    }
}
