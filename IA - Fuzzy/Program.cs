using IA___Fuzzy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IA___Fuzzy
{
    public class Program
    {
        static List<Usuario> usuarios = new List<Usuario>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Aguarde enquanto estamos recolhendo as informações do usuário...");

            string file = Properties.Resources.Usuario;
            var lines = file.Split(new[] { Environment.NewLine },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var item in lines)
            {
                var divisor = item.Split('|');

                var usuario = new Usuario();
                usuario.Codigo = int.Parse(divisor[0].Trim());
                usuario.Nome = divisor[1].Trim();
                usuario.Idade = int.Parse(divisor[2].Trim());
                usuario.Sexo = divisor[3].Trim();
                usuario.CodMelhorRelacionado = int.Parse(divisor[4].Trim());
                usuario.PossuiTempoLivre = bool.Parse(divisor[5].Trim());
                usuario.QuantidadeHorasTempoLivre = int.Parse(divisor[6].Trim());
                usuario.NotaProgramacao = double.Parse(divisor[7].Trim());
                usuario.NotaEstruturaDados = double.Parse(divisor[8].Trim());
                usuario.NotaBancoDados = double.Parse(divisor[9].Trim());
                usuario.NotaCalculo = double.Parse(divisor[10].Trim());
                usuario.NotaGerenciaProjetos = double.Parse(divisor[11].Trim());
                usuario.CodMaiorConfianca = int.Parse(divisor[12].Trim());
                usuario.CodMaiorAtividadesComum = int.Parse(divisor[13].Trim());
                usuario.PossuiDominioProgramacao = bool.Parse(divisor[14].Trim());
                usuario.PossuiDominioEstruturaDados = bool.Parse(divisor[15].Trim());
                usuario.PossuiDominioBancoDados = bool.Parse(divisor[16].Trim());
                usuario.PossuiDominioCalculo = bool.Parse(divisor[17].Trim());
                usuario.PossuiDominioGerenciaProjetos = bool.Parse(divisor[18].Trim());

                usuarios.Add(usuario);
            }

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
                    MontaFuzzy(usuarios, opcao);
                    break;
                case "2":
                    InformaUsuario();
                    MontaFuzzy(usuarios, opcao);
                    break;
                case "3":
                    InformaUsuario();
                    MontaFuzzy(usuarios, opcao);
                    break;
                case "4":
                    InformaUsuario();
                    MontaFuzzy(usuarios, opcao);
                    break;
                case "5":
                    InformaUsuario();
                    MontaFuzzy(usuarios, opcao);
                    break;
                default:
                    MontaMenuErro();
                    break;
            }
        }
        
        public static void MontaFuzzy(List<Usuario> usuarios, string opcao)
        {
            //Retorno 2 melhores de cada variável
            List<Usuario> usuariosComMelhoresRelacionamento = CalcularDesempenhoRelacionamento(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresTempoLivres = CalcularDesempenhoTempoLivre(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresNotas = CalcularDesempenhoNotas(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresConfianca = CalcularDesempenhoConfianca(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresAtividadesComum = CalcularDesempenhoAtividadesComum(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresDominioConteudo = CalcularDesempenhoDominioConteudo(usuarios, int.Parse(opcao));
            List<Usuario> usariosComMelhoresDedicacoes = CalcularDesempenhoDedicacao(usuarios, int.Parse(opcao));
            /*List<Usuario> usuariosComMelhoresFaltas = CalcularDesempenhoFaltas(usuarios, novaOpcao);*/

            List<Usuario> duplaFinal = MontaMelhorDupla(usuariosComMelhoresRelacionamento, usuariosComMelhoresTempoLivres, usuariosComMelhoresNotas, usuariosComMelhoresConfianca,
                                                            usuariosComMelhoresAtividadesComum, usuariosComMelhoresDominioConteudo, usariosComMelhoresDedicacoes);

            var nome1 = duplaFinal.First().Nome;
            var nome2 = duplaFinal.Last().Nome;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\tMelhor dupla formada:");
            Console.WriteLine("1. " + nome1);
            Console.WriteLine("2. " + nome2 );
            Console.WriteLine("");
            Console.WriteLine("Clique para continuar.");
            Console.ReadKey();
            MontaMenuFinal();
        }



        #region Calculo Desempenho        

        private static List<Usuario> CalcularDesempenhoRelacionamento(List<Usuario> usuarios, int opcao)
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

        private static List<Usuario> CalcularDesempenhoTempoLivre(List<Usuario> usuarios, int opcao)
        {
            var usuariosComTempoLivre = usuarios.Where(w => w.PossuiTempoLivre).ToList();
            List<Usuario> user = new List<Usuario>();

            foreach (var item in usuariosComTempoLivre)
            {
                if (user.Count < 2)
                    user.Add(item);
                else if (user.Count == 2)
                {
                    var usuarioComMenorTempoLivre = user.Where(w => w.QuantidadeHorasTempoLivre < item.QuantidadeHorasTempoLivre).FirstOrDefault();

                    if (usuarioComMenorTempoLivre != null)
                    {
                        user.Remove(usuarioComMenorTempoLivre);
                        user.Add(item);
                    }
                }
            }

            return user;
        }

        private static List<Usuario> CalcularDesempenhoNotas(List<Usuario> usuarios, int opcao)
        {
            List<Usuario> user = new List<Usuario>();

            foreach (var item in usuarios)
            {
                if (user.Count < 2)
                    user.Add(item);
                else if (user.Count == 2)
                {
                    var usuarioComNotaMenor = new Usuario();

                    if ((Disciplina)opcao == Disciplina.Programacao)
                        usuarioComNotaMenor = user.Where(w => w.NotaProgramacao < item.NotaProgramacao).FirstOrDefault();
                    else if ((Disciplina)opcao == Disciplina.EstruturaDados)
                        usuarioComNotaMenor = user.Where(w => w.NotaEstruturaDados < item.NotaEstruturaDados).FirstOrDefault();
                    else if ((Disciplina)opcao == Disciplina.BancoDados)
                        usuarioComNotaMenor = user.Where(w => w.NotaBancoDados < item.NotaBancoDados).FirstOrDefault();
                    else if ((Disciplina)opcao == Disciplina.Calculo)
                        usuarioComNotaMenor = user.Where(w => w.NotaCalculo < item.NotaCalculo).FirstOrDefault();
                    else if ((Disciplina)opcao == Disciplina.GerenciaProjetos)
                        usuarioComNotaMenor = user.Where(w => w.NotaGerenciaProjetos < item.NotaGerenciaProjetos).FirstOrDefault();

                    if (usuarioComNotaMenor != null)
                    {
                        user.Remove(usuarioComNotaMenor);
                        user.Add(item);
                    }
                }
            }

            return user;
        }

        private static List<Usuario> CalcularDesempenhoConfianca(List<Usuario> usuarios, int opcao)
        {
            List<int> cod = new List<int>();
            List<int> codRepetido = new List<int>();

            foreach (var item in usuarios)
            {
                if (!cod.Exists(x => x == item.CodMaiorConfianca))
                    cod.Add(item.CodMaiorConfianca);
                else
                {
                    if (!codRepetido.Exists(x => x == item.CodMaiorConfianca))
                        codRepetido.Add(item.CodMaiorConfianca);
                }
            }

            var primeiroUsuario = usuarios.Where(w => w.Codigo == codRepetido.FirstOrDefault()).FirstOrDefault();
            var segundoUsuario = usuarios.Where(w => w.Codigo == primeiroUsuario.CodMaiorConfianca).FirstOrDefault();

            var retorno = new List<Usuario>();
            retorno.Add(primeiroUsuario);
            retorno.Add(segundoUsuario);

            return retorno;
        }

        private static List<Usuario> CalcularDesempenhoAtividadesComum(List<Usuario> usuarios, int opcao)
        {
            List<int> cod = new List<int>();
            List<int> codRepetido = new List<int>();

            foreach (var item in usuarios)
            {
                if (!cod.Exists(x => x == item.CodMaiorAtividadesComum))
                    cod.Add(item.CodMaiorAtividadesComum);
                else
                {
                    if (!codRepetido.Exists(x => x == item.CodMaiorAtividadesComum))
                        codRepetido.Add(item.CodMaiorAtividadesComum);
                }
            }

            var primeiroUsuario = usuarios.Where(w => w.Codigo == codRepetido.FirstOrDefault()).FirstOrDefault();
            var segundoUsuario = usuarios.Where(w => w.Codigo == primeiroUsuario.CodMaiorAtividadesComum).FirstOrDefault();

            var retorno = new List<Usuario>();
            retorno.Add(primeiroUsuario);
            retorno.Add(segundoUsuario);

            return retorno;
        }

        private static List<Usuario> CalcularDesempenhoDominioConteudo(List<Usuario> usuarios, int opcao)
        {
            List<Usuario> user = new List<Usuario>();

            if ((Disciplina)opcao == Disciplina.Programacao)
                user = usuarios.Where(w => w.PossuiDominioProgramacao).ToList();
            else if ((Disciplina)opcao == Disciplina.EstruturaDados)
                user = usuarios.Where(w => w.PossuiDominioEstruturaDados).ToList();
            else if ((Disciplina)opcao == Disciplina.BancoDados)
                user = usuarios.Where(w => w.PossuiDominioBancoDados).ToList();
            else if ((Disciplina)opcao == Disciplina.Calculo)
                user = usuarios.Where(w => w.PossuiDominioCalculo).ToList();
            else if ((Disciplina)opcao == Disciplina.GerenciaProjetos)
                user = usuarios.Where(w => w.PossuiDominioGerenciaProjetos).ToList();

            var retorno = CalcularDesempenhoNotas(user, opcao);

            return retorno;
        }

        private static List<Usuario> CalcularDesempenhoDedicacao(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static List<Usuario> CalcularDesempenhoFaltas(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }



        private static List<Usuario> MontaMelhorDupla(List<Usuario> usuariosComMelhoresRelacionamento, List<Usuario> usuariosComMelhoresTempoLivres, List<Usuario> usuariosComMelhoresNotas,
                                                        List<Usuario> usuariosComMelhoresConfianca, List<Usuario> usuariosComMelhoresAtividadesComum, List<Usuario> usuariosComMelhoresDominioConteudo,
                                                        List<Usuario> usariosComMelhoresDedicacoes)
        {
            // Implementar (15 regras)
            //TODO
            return usariosComMelhoresDedicacoes;
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
        TempoLivre = 2,
        Notas = 3,
        Confiança = 4,
        AtividadesComum = 5,
        DominioConteudo = 6,
        Dedicacao = 7,
        Faltas = 8,
        //mais 2 variaveis
    }

    #endregion

}



