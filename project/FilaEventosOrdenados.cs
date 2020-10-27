using System;

namespace project
{
    public class FilaEventosOrdenados
    {
        public string nome;
        public int duracao;
        public TimeSpan horarioComecou, horarioTerminou;
        public FilaEventosOrdenados Prox;

        public FilaEventosOrdenados()
        {
            nome = "";
            duracao = 0;
            horarioComecou = TimeSpan.Zero;
            horarioTerminou = TimeSpan.Zero;
            Prox = null;
        }
    }


    public class OperFilaFilaEventosOrdenados {
        private FilaEventosOrdenados Início;
        private FilaEventosOrdenados Fim;
        private FilaEventosOrdenados Aux;

        public int Tamanho;

        public OperFilaFilaEventosOrdenados()
        {
            Início = null;
            Fim = null;
            Tamanho = 0;
        }


        public void Inserir(string nome, int duracao, TimeSpan horarioComecou, TimeSpan horarioTerminou)
        {
            FilaEventosOrdenados Novo = new FilaEventosOrdenados();

            Novo.nome = nome;
            Novo.duracao = duracao;
            Novo.horarioComecou = horarioComecou;
            Novo.horarioTerminou = horarioTerminou;
            Novo.Prox = null;


            if (Início == null)
            {
                Início = Novo;
                Fim = Novo;
            }
            else
            {
                Fim.Prox = Novo;
                Fim = Novo;
            }

            Tamanho++;
        }
        public FilaEventosOrdenados RecuperarDadosUltimoElemento()
        {
            return Fim;
        }

        public void MostraFila()
        {


            if (Início == null)
            {
                Console.WriteLine("\nA Fila não possui nenhum elemento!!! \n\n");
            }
            else
            {
                Aux = Início;

                while (Aux != null)
                {
                    Console.WriteLine("Evento: " + Aux.nome + " Horario inicio " + Aux.horarioComecou + " Horario Terminou "+ Aux.horarioTerminou);
                    Aux = Aux.Prox;
                }


            }
        }


        public void RetiraElemento()
        {
            if (Início == null)
            {
                Console.WriteLine("Não a elementos");
            }
            else
            {
                
                Início = Início.Prox;
                Tamanho--;
            }
        }

        public void EsvaziarFila()
        {
            if (Início == null)
            {
                Console.WriteLine("Não a elementos");
            }
            else
            {
               while(Tamanho != 0) {
                    Início = Início.Prox;
                    Tamanho--;
                }
                
            }
        }

        public void Reorganizar(TimeSpan horarioComecou, TimeSpan horarioTerminou)
        {
            if (Início == null)
            {
                Console.WriteLine("\nA Fila não possui nenhum elemento!!! \n\n");
            }
            else
            {
                Aux = Início;

                while (Aux.horarioComecou != horarioComecou && Aux.horarioTerminou != horarioTerminou )
                {
                    Inserir(Aux.nome, Aux.duracao, Aux.horarioComecou, Aux.horarioTerminou);
                    RetiraElemento();
                    Aux = Início;
                    
                }              
            }
        }

    }

}
