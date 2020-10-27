using System;

namespace project
{
    public class FilaEventosNaoOrdenados
    {
        public int duracao, QntVezesReposicionado;
        public string nome;
        public FilaEventosNaoOrdenados Prox;

        public FilaEventosNaoOrdenados()
        {
            duracao = 0;
            nome = "";
            QntVezesReposicionado = 0;
            Prox = null;
        }


    }

    public class OperFilaFilaEventosNaoOrdenados
    {
        private FilaEventosNaoOrdenados Início;
        private FilaEventosNaoOrdenados Fim;
        private FilaEventosNaoOrdenados Aux;

        public int Tamanho;

        public OperFilaFilaEventosNaoOrdenados()
        {
            Início = null;
            Fim = null;
            Tamanho = 0;
        }


        public void Inserir(int duracao, string nome, int QntVezesReposicionado)
        {
            FilaEventosNaoOrdenados Novo = new FilaEventosNaoOrdenados();

            Novo.nome = nome;
            Novo.duracao = duracao;
            Novo.QntVezesReposicionado = QntVezesReposicionado;

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

        public FilaEventosNaoOrdenados RecurarDadosInicio()
        {
            return Início;
        }

        public int RecurarDadosTamanho()
        {
            return Tamanho;
        }

        public void ReposicionarElemento(int duracao, string nome, int QntVezesReposicionado)
        {
            Inserir(duracao, nome, QntVezesReposicionado);
            RetiraElemento();
        }

        public void AtualizarQntVezes()
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
                    Aux.QntVezesReposicionado = 0;
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
    }

}
