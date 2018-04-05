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
                usuario.QuantidadeHorasTempoLivre = int.Parse(divisor[5].Trim());
                usuario.NotaProgramacao = double.Parse(divisor[6].Trim());
                usuario.NotaEstruturaDados = double.Parse(divisor[7].Trim());
                usuario.NotaBancoDados = double.Parse(divisor[8].Trim());
                usuario.NotaCalculo = double.Parse(divisor[9].Trim());
                usuario.NotaGerenciaProjetos = double.Parse(divisor[10].Trim());
                usuario.CodMaiorConfianca = int.Parse(divisor[11].Trim());
                usuario.CodMaiorAtividadesComum = int.Parse(divisor[12].Trim());
                usuario.PossuiDominioProgramacao = bool.Parse(divisor[13].Trim());
                usuario.PossuiDominioEstruturaDados = bool.Parse(divisor[14].Trim());
                usuario.PossuiDominioBancoDados = bool.Parse(divisor[15].Trim());
                usuario.PossuiDominioCalculo = bool.Parse(divisor[16].Trim());
                usuario.PossuiDominioGerenciaProjetos = bool.Parse(divisor[17].Trim());
                usuario.Dedicado = bool.Parse(divisor[18].Trim());
                usuario.QuantidadeFaltas = int.Parse(divisor[19].Trim());
                usuario.EhInteligente = bool.Parse(divisor[20].Trim());
                usuario.EhComunicativo = bool.Parse(divisor[21].Trim());

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
            List<Usuario> usuariosComMelhoresRelacionamento = CalcularDesempenhoRelacionamento(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresTempoLivres = CalcularDesempenhoTempoLivre(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresNotas = CalcularDesempenhoNotas(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresConfianca = CalcularDesempenhoConfianca(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresAtividadesComum = CalcularDesempenhoAtividadesComum(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresDominioConteudo = CalcularDesempenhoDominioConteudo(usuarios, int.Parse(opcao));
            List<Usuario> usariosComMelhoresDedicacoes = CalcularDesempenhoDedicacao(usuarios, int.Parse(opcao));
            List<Usuario> usuariosComMelhoresFaltas = CalcularDesempenhoFaltas(usuarios, int.Parse(opcao));
            List<Usuario> usuariosMaisInteligentes = CalcularDesempenhoInteligencia(usuarios, int.Parse(opcao));
            List<Usuario> usuariosMaisComunicativos = CalcularDesempenhoComunicacao(usuarios, int.Parse(opcao));

            List<UsuarioPorcentagem> duplaFinal = MontaMelhorDupla(usuariosComMelhoresRelacionamento, usuariosComMelhoresTempoLivres, usuariosComMelhoresNotas, usuariosComMelhoresConfianca,
                                                            usuariosComMelhoresAtividadesComum, usuariosComMelhoresDominioConteudo, usariosComMelhoresDedicacoes, usuariosComMelhoresFaltas,
                                                            usuariosMaisInteligentes, usuariosMaisComunicativos);

            if (duplaFinal.Count == 2) {
                var nome1 = duplaFinal.First().Usuario.Nome;
                var nome2 = duplaFinal.Last().Usuario.Nome;

                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("\tMelhor dupla formada junto com suas porcentagens de qualificações:");
                Console.WriteLine("");
                Console.WriteLine("1. " + nome1 + " com " + duplaFinal.First().Porcentagem + " !");
                Console.WriteLine("2. " + nome2 + " com " + duplaFinal.Last().Porcentagem + " !");
                Console.WriteLine("");
                Console.WriteLine("Clique para continuar.");
                Console.ReadKey();
                MontaMenuFinal();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("\tMelhor dupla formada junto com suas porcentagens de qualificações:");
                Console.WriteLine("");
                Console.WriteLine("1. " + duplaFinal[0].Usuario.Nome + " com " + duplaFinal[0].Porcentagem + " !");
                Console.WriteLine("2. " + duplaFinal[1].Usuario.Nome + " com " + duplaFinal[1].Porcentagem + " !");
                Console.WriteLine("");
                Console.WriteLine("\tPode integrar também à eles o terceiro melhor perante suas qualificações:");
                Console.WriteLine("3. " + duplaFinal[2].Usuario.Nome + " com " + duplaFinal[2].Porcentagem + " !");
                Console.WriteLine("");
                Console.WriteLine("Clique para continuar.");
                Console.ReadKey();
                MontaMenuFinal();
            }
        }

        #region Calculo Desempenho        

        private static List<Usuario> CalcularDesempenhoRelacionamento(List<Usuario> usuarios, int opcao)
        {
            Dictionary<int, int> dicionario = new Dictionary<int, int>();

            foreach (var item in usuarios)
            {
                if (!dicionario.ContainsKey(item.CodMelhorRelacionado))
                    dicionario.Add(item.CodMelhorRelacionado, 1);
                else
                {
                    var sucesso = dicionario.TryGetValue(item.CodMelhorRelacionado, out int valorAtualDicionario);
                    if (sucesso)
                        dicionario[item.CodMelhorRelacionado] = valorAtualDicionario + 1;
                }
            }

            var dicionarioOrdenado = dicionario.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);

            var usuariosOrdenados = new List<Usuario>();
            foreach (var item in dicionarioOrdenado)
            {
                var populaUsuario = new Usuario()
                {
                    CodMelhorRelacionado = item.Key
                };
                usuariosOrdenados.Add(populaUsuario);
            }

            var usuarioFinal = new List<Usuario>();
            foreach (var item in usuariosOrdenados)
            {
                var user = usuarios.Where(w => w.Codigo == item.CodMelhorRelacionado).FirstOrDefault();
                usuarioFinal.Add(user);
            }

            return usuarioFinal.Take(6).ToList();
        }

        private static List<Usuario> CalcularDesempenhoTempoLivre(List<Usuario> usuarios, int opcao)
        {
            Dictionary<int, int> dicionario = new Dictionary<int, int>();

            foreach (var item in usuarios)
            {
                if (item.QuantidadeHorasTempoLivre != 0)
                {
                    if (!dicionario.ContainsKey(item.QuantidadeHorasTempoLivre))
                        dicionario.Add(item.QuantidadeHorasTempoLivre, 1);
                    else
                    {
                        var sucesso = dicionario.TryGetValue(item.QuantidadeHorasTempoLivre, out int valorAtualDicionario);
                        if (sucesso)
                            dicionario[item.QuantidadeHorasTempoLivre] = valorAtualDicionario + 1;
                    }
                }
            }

            var dicionarioOrdenado = dicionario.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);

            var usuariosOrdenados = new List<Usuario>();
            foreach (var item in dicionarioOrdenado)
            {
                var populaUsuario = new Usuario()
                {
                    QuantidadeHorasTempoLivre = item.Key
                };
                usuariosOrdenados.Add(populaUsuario);
            }

            var usuarioFinal = new List<Usuario>();
            foreach (var item in usuariosOrdenados)
            {
                var user = usuarios.Where(w => w.QuantidadeHorasTempoLivre == item.QuantidadeHorasTempoLivre).ToList();
                usuarioFinal.AddRange(user);
            }

            return usuarioFinal.Take(6).ToList();

        }

        private static List<Usuario> CalcularDesempenhoNotas(List<Usuario> usuarios, int opcao)
        {

            List<Usuario> retorno = new List<Usuario>();

            if ((Disciplina)opcao == Disciplina.Programacao)
                retorno = usuarios.OrderByDescending(o => o.NotaProgramacao).Take(6).ToList();
            else if ((Disciplina)opcao == Disciplina.EstruturaDados)
                retorno = usuarios.OrderByDescending(o => o.NotaEstruturaDados).Take(6).ToList();
            else if ((Disciplina)opcao == Disciplina.BancoDados)
                retorno = usuarios.OrderByDescending(o => o.NotaBancoDados).Take(6).ToList();
            else if ((Disciplina)opcao == Disciplina.Calculo)
                retorno = usuarios.OrderByDescending(o => o.NotaCalculo).Take(6).ToList();
            else if ((Disciplina)opcao == Disciplina.GerenciaProjetos)
                retorno = usuarios.OrderByDescending(o => o.NotaGerenciaProjetos).Take(6).ToList();

            return retorno;
        }

        private static List<Usuario> CalcularDesempenhoConfianca(List<Usuario> usuarios, int opcao)
        {
            Dictionary<int, int> dicionario = new Dictionary<int, int>();

            foreach (var item in usuarios)
            {
                if (!dicionario.ContainsKey(item.CodMaiorConfianca))
                    dicionario.Add(item.CodMaiorConfianca, 1);
                else
                {
                    var sucesso = dicionario.TryGetValue(item.CodMaiorConfianca, out int valorAtualDicionario);
                    if (sucesso)
                        dicionario[item.CodMaiorConfianca] = valorAtualDicionario + 1;
                }
            }

            var dicionarioOrdenado = dicionario.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);

            var usuariosOrdenados = new List<Usuario>();
            foreach (var item in dicionarioOrdenado)
            {
                var populaUsuario = new Usuario()
                {
                    CodMaiorConfianca = item.Key
                };
                usuariosOrdenados.Add(populaUsuario);
            }

            var usuarioFinal = new List<Usuario>();
            foreach (var item in usuariosOrdenados)
            {
                var user = usuarios.Where(w => w.Codigo == item.CodMaiorConfianca).FirstOrDefault();
                usuarioFinal.Add(user);
            }

            return usuarioFinal.Take(6).ToList();
        }

        private static List<Usuario> CalcularDesempenhoAtividadesComum(List<Usuario> usuarios, int opcao)
        {
            Dictionary<int, int> dicionario = new Dictionary<int, int>();

            foreach (var item in usuarios)
            {
                if (!dicionario.ContainsKey(item.CodMaiorAtividadesComum))
                    dicionario.Add(item.CodMaiorAtividadesComum, 1);
                else
                {
                    var sucesso = dicionario.TryGetValue(item.CodMaiorAtividadesComum, out int valorAtualDicionario);
                    if (sucesso)
                        dicionario[item.CodMaiorAtividadesComum] = valorAtualDicionario + 1;
                }
            }

            var dicionarioOrdenado = dicionario.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);

            var usuariosOrdenados = new List<Usuario>();
            foreach (var item in dicionarioOrdenado)
            {
                var populaUsuario = new Usuario()
                {
                    CodMaiorAtividadesComum = item.Key
                };
                usuariosOrdenados.Add(populaUsuario);
            }

            var usuarioFinal = new List<Usuario>();
            foreach (var item in usuariosOrdenados)
            {
                var user = usuarios.Where(w => w.Codigo == item.CodMaiorAtividadesComum).FirstOrDefault();
                usuarioFinal.Add(user);
            }

            return usuarioFinal.Take(6).ToList();
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
            return usuarios.Where(w => w.Dedicado).ToList();
        }

        private static List<Usuario> CalcularDesempenhoFaltas(List<Usuario> usuarios, int opcao)
        {
            Dictionary<int, int> dicionario = new Dictionary<int, int>();

            foreach (var item in usuarios)
            {
                if (!dicionario.ContainsKey(item.QuantidadeFaltas))
                    dicionario.Add(item.QuantidadeFaltas, 1);
                else
                {
                    var sucesso = dicionario.TryGetValue(item.QuantidadeFaltas, out int valorAtualDicionario);
                    if (sucesso)
                        dicionario[item.QuantidadeFaltas] = valorAtualDicionario + 1;
                }
            }

            var dicionarioOrdenado = dicionario.OrderBy(o => o.Value).ToDictionary(o => o.Key);

            var usuariosOrdenados = new List<Usuario>();
            foreach (var item in dicionarioOrdenado)
            {
                var populaUsuario = new Usuario()
                {
                    QuantidadeFaltas = item.Key
                };
                usuariosOrdenados.Add(populaUsuario);
            }

            var usuarioFinal = new List<Usuario>();
            foreach (var item in usuariosOrdenados)
            {
                var user = usuarios.Where(w => w.QuantidadeFaltas == item.QuantidadeFaltas).ToList();
                usuarioFinal.AddRange(user);
            }

            return usuarioFinal.Take(6).ToList();
        }

        private static List<Usuario> CalcularDesempenhoInteligencia(List<Usuario> usuarios, int opcao)
        {
            return usuarios.Where(w => w.EhInteligente).ToList();
        }

        private static List<Usuario> CalcularDesempenhoComunicacao(List<Usuario> usuarios, int opcao)
        {
            return usuarios.Where(w => w.EhComunicativo).ToList();
        }


        private static List<UsuarioPorcentagem> MontaMelhorDupla(List<Usuario> usuariosComMelhoresRelacionamento, List<Usuario> usuariosComMelhoresTempoLivres, List<Usuario> usuariosComMelhoresNotas,
                                                        List<Usuario> usuariosComMelhoresConfianca, List<Usuario> usuariosComMelhoresAtividadesComum, List<Usuario> usuariosComMelhoresDominioConteudo,
                                                        List<Usuario> usariosComMelhoresDedicacoes, List<Usuario> usuariosComMelhoresFaltas, List<Usuario> usuariosMaisInteligentes,
                                                        List<Usuario> usuariosMaisComunicativos)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();

            List<Usuario> usuariosUnificados = new List<Usuario>();
            usuariosUnificados.AddRange(usuariosComMelhoresRelacionamento);
            usuariosUnificados.AddRange(usuariosComMelhoresTempoLivres);
            usuariosUnificados.AddRange(usuariosComMelhoresNotas);
            usuariosUnificados.AddRange(usuariosComMelhoresConfianca);
            usuariosUnificados.AddRange(usuariosComMelhoresAtividadesComum);
            usuariosUnificados.AddRange(usuariosComMelhoresDominioConteudo);
            usuariosUnificados.AddRange(usariosComMelhoresDedicacoes);
            usuariosUnificados.AddRange(usuariosComMelhoresFaltas);
            usuariosUnificados.AddRange(usuariosMaisInteligentes);
            usuariosUnificados.AddRange(usuariosMaisComunicativos);

            int valorAtualDicionario;
            foreach (var item in usuariosUnificados)
            {
                if (!dicionario.ContainsKey(item))
                    dicionario.Add(item, 1);
                else
                {
                    var sucesso = dicionario.TryGetValue(item, out valorAtualDicionario);
                    if (sucesso)
                        dicionario[item] = valorAtualDicionario + 1;
                }
            }

            var dicionarioOrdenado = dicionario.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);

            var usuariosPorcentagem = new List<UsuarioPorcentagem>();
            foreach (var item in dicionarioOrdenado)
            {
                var populaUsuarioPorcentagem = new UsuarioPorcentagem()
                {
                    Usuario = item.Key,
                    Porcentagem = item.Value.Value.ToString() + "0%"
                };
                usuariosPorcentagem.Add(populaUsuarioPorcentagem);
            }

            List<UsuarioPorcentagem> retorno = new List<UsuarioPorcentagem>();
            foreach (var item in usuariosPorcentagem)
            {
                if (retorno.Count < 2)
                    retorno.Add(item);
                else if (retorno.Count >= 2)
                {
                    if (item.Porcentagem.Equals(retorno[1].Porcentagem))
                        retorno.Add(item);
                }
            }

            return retorno;
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

    public enum Qualificações
    {
        Relacionamento = 1,
        TempoLivre = 2,
        Notas = 3,
        Confiança = 4,
        AtividadesComum = 5,
        DominioConteudo = 6,
        Dedicacao = 7,
        Faltas = 8,
        Inteligencia = 9,
        Comunicacao = 10
    }

    #endregion

}



