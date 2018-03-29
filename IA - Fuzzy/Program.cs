using IA___Fuzzy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IA___Fuzzy
{
    public class Program
    {
        static List<Usuario> usuariosGlobal = new List<Usuario>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Aguarde enquanto estamos recolhendo as informações do usuário...");

            string file = Properties.Resources.Usuario;
            Random rand = new Random();
            var lines = file.Split(new[] { Environment.NewLine },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();

            List<Usuario> usuarios = new List<Usuario>();
            foreach (var item in lines)
            {
                var divisor = item.Split('|');
                var usuario = new Usuario()
                {
                    Codigo = int.Parse(divisor[0].Trim()),
                    Nome = divisor[1].Trim(),
                    Idade = int.Parse(divisor[2].Trim()),
                    Sexo = divisor[3].Trim(),
                    CodMelhorRelacionado = int.Parse(divisor[4].Trim()),
                };
                usuarios.Add(usuario);
            }
            usuariosGlobal = usuarios;

            System.Threading.Thread.Sleep(3000);

            MontaMenu();
        }

        public static void MontaLogica(string opcao)
        {
            Console.Clear();
            Console.WriteLine("Verificando opção fornecida...");

            switch (opcao)
            {
                case "0":
                    Environment.Exit(1);
                    break;
                case "1":
                    InformaUsuario();
                    MontaFuzzy(usuariosGlobal, opcao);
                    Console.ReadKey();
                    break;
                case "2":
                    InformaUsuario();
                    MontaFuzzy(usuariosGlobal, opcao);
                    break;
                case "3":
                    InformaUsuario();
                    MontaFuzzy(usuariosGlobal, opcao);
                    break;
                case "4":
                    InformaUsuario();
                    MontaFuzzy(usuariosGlobal, opcao);
                    break;
                case "5":
                    InformaUsuario();
                    MontaFuzzy(usuariosGlobal, opcao);
                    break;
                default:
                    MontaMenuErro();
                    break;
            }
        }
        
        public static void MontaFuzzy(List<Usuario> usuarios, string opcao)
        {
            int novaOpcao = int.Parse(opcao);

            //Retorno 2 melhores (15 regras)
            List<Usuario> usuariosComMelhoresRelacionamento = CalcularDesempenhoRelacionamento(usuarios, novaOpcao);
            /*List<Usuario> usuariosComMelhoresTempoLivres = CalcularDesempenhoTempoLivre(usuarios, novaOpcao);
            List<Usuario> usuariosComMelhoresNotas = CalcularDesempenhoNotas(usuarios, novaOpcao);
            List<Usuario> usuariosComMelhoresGostosComum = CalcularDesempenhoGostoComum(usuarios, novaOpcao);
            List<Usuario> usuariosComMelhoresDominioConteudo = CalcularDesempenhoDominioConteudo(usuarios, novaOpcao);
            List<Usuario> usuariosComMelhoresFaltas = CalcularDesempenhoFaltas(usuarios, novaOpcao);
            List<Usuario> usuariosComMelhoresConfianca = CalcularDesempenhoConfianca(usuarios, novaOpcao);*/

            //List<Usuario> duplaFinal = MontaDoisMelhores();

            var nome1 = usuariosComMelhoresRelacionamento.First().Nome;
            var nome2 = usuariosComMelhoresRelacionamento.Last().Nome;

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════╗");
            Console.WriteLine("║ \tMelhor dupla formada:   ║");
            Console.WriteLine("║ 1. " + nome1 + "                    ║");
            Console.WriteLine("║ 2. " + nome2 + "                   ║");
            Console.WriteLine("╚═══════════════════════════════╝");
            Console.WriteLine("Clique para continuar.");
            Console.ReadKey();
            MontaMenuFinal();
        }

        #region Calculo Desempenho        

        private static List<Usuario> CalcularDesempenhoRelacionamento(List<Usuario> usuarios, int novaOpcao)
        {
            List<int> cod = new List<int>();
            List<int> codRepetido = new List<int>();

            foreach (var item in usuarios)
            {
                if (!cod.Exists(x => x == item.CodMelhorRelacionado))
                    cod.Add(item.CodMelhorRelacionado);
                else
                {
                    if (!codRepetido.Exists(x => x == item.CodMelhorRelacionado))
                        codRepetido.Add(item.CodMelhorRelacionado);
                }
            }

            var primeiroUsuario = usuarios.Where(w => w.Codigo == codRepetido.FirstOrDefault()).FirstOrDefault();
            var segundoUsuario = usuarios.Where(w => w.Codigo == primeiroUsuario.CodMelhorRelacionado).FirstOrDefault();

            var retorno = new List<Usuario>();
            retorno.Add(primeiroUsuario);
            retorno.Add(segundoUsuario);

            return retorno;
        }

        private static List<Usuario> CalcularDesempenhoConfianca(List<Usuario> usuarios, int novaOpcao)
        {
            throw new NotImplementedException();
        }

        private static List<Usuario> CalcularDesempenhoFaltas(List<Usuario> usuarios, int novaOpcao)
        {
            throw new NotImplementedException();
        }

        private static List<Usuario> CalcularDesempenhoDominioConteudo(List<Usuario> usuarios, int novaOpcao)
        {
            throw new NotImplementedException();
        }

        private static List<Usuario> CalcularDesempenhoGostoComum(List<Usuario> usuarios, int novaOpcao)
        {
            throw new NotImplementedException();
        }

        private static List<Usuario> CalcularDesempenhoNotas(List<Usuario> usuarios, int novaOpcao)
        {
            throw new NotImplementedException();
        }

        private static List<Usuario> CalcularDesempenhoTempoLivre(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Menus e Informação Usuário

        public static void MontaMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ \tPor favor escolha a disciplina desejada!         ║");
            Console.WriteLine("║                                                        ║");
            Console.WriteLine("║ 1. Programação                                         ║");
            Console.WriteLine("║ 2. Estrutura de dados                                  ║");
            Console.WriteLine("║ 3. Banco de dados                                      ║");
            Console.WriteLine("║ 4. Cálculo                                             ║");
            Console.WriteLine("║ 5. Gerência de Projetos                                ║");
            Console.WriteLine("║ 0. Sair                                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine("");
            Console.Write("SUA OPÇÃO: ");
            var opcao = Console.ReadLine();

            MontaLogica(opcao);
        }

        public static void MontaMenuErro()
        {
            Console.Clear();
            Console.WriteLine("Opção Inválida.");
            Console.WriteLine("");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║ \tDeseja voltar ao menu?           ║");
            Console.WriteLine("║ 1. Sim me leva até lá!                 ║");
            Console.WriteLine("║ 0. Não, sair.                          ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("");
            Console.Write("SUA OPÇÃO: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "0":
                    Environment.Exit(1);
                    break;
                case "1":
                    MontaMenu();
                    break;
                default:
                    MontaMenuErro();
                    break;
            }
        }

        public static void MontaMenuFinal()
        {
            {
                Console.Clear();
                Console.WriteLine("Parabéns, agora você tem a melhor dupla possível para a realização do trabalho. Aproveite!");
                Console.WriteLine("");
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║ \tDeseja voltar ao menu?           ║");
                Console.WriteLine("║ 1. Sim me leva até lá!                 ║");
                Console.WriteLine("║ 0. Não, sair.                          ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("");
                Console.Write("SUA OPÇÃO: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        Environment.Exit(1);
                        break;
                    case "1":
                        MontaMenu();
                        break;
                    default:
                        MontaMenuErro();
                        break;
                }
            }
        }

        public static void InformaUsuario()
        {
            Console.Clear();
            Console.WriteLine("Aguarde enquanto estamos montando a melhor dupla possível para seu trabalho...");
        }

        #endregion

    }

    #region Enuns

    public enum Disciplina
    {
        Programacao = 1,
        EstruturaDados = 2,
        BancoDados = 3,
        Calculo = 4,
        GerenciaProjetos = 5
    }

    public enum VariavelPeso
    {
        Relacionamento = 1,
        Notas = 2,
        TempoLivre = 3,
        Confiança = 4,
        AtividadesComum = 5,
        DominioConteudo = 6,
        Faltas = 7,
        //mais 3 variaveis
    }

    #endregion

}



