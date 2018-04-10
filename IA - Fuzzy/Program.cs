using IA___Fuzzy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IA___Fuzzy
{
    public class Program
    {
        static List<Usuario> usuarios = new List<Usuario>();
        static Usuario usuarioPessoal = new Usuario();

        public static void Main(string[] args)
        {
            var pessoa = new Usuario();
            var input = 0;

            Console.Write("Por favor, informe o seu nome: ");
            pessoa.Nome = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine(pessoa.Nome + ", de 0 a 100, nos informe qual seria a sua melhor proporção em relação à:");
            Console.WriteLine("");
            Console.Write("Relacionamento: ");
            pessoa.ProporcaoRelacionamento = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Tempo Livre: ");
            pessoa.ProporcaoTempoLivre = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Notas: ");
            pessoa.ProporcaoNotas = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Confiança: ");
            pessoa.ProporcaoConfianca = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Atividades em Comum: ");
            pessoa.ProporcaoAtividadesComum = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Domínio do Conteúdo: ");
            pessoa.ProporcaoDominioConteudo = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Dedicação: ");
            pessoa.ProporcaoDedicacao = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Faltas: ");
            pessoa.ProporcaoFaltas = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Inteligência: ");
            pessoa.ProporcaoInteligencia = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;
            Console.Write("Comunicação: ");
            pessoa.ProporcaoComunicacao = int.TryParse(Console.ReadLine(), out input) ? input : (int?)null;

            usuarioPessoal = QualificaProporcoesPessoal(pessoa);

            Console.Clear();
            Console.WriteLine(usuarioPessoal.Nome + ", aguarde enquanto estamos recolhendo as informações dos usuários restantes...");

            string file = Properties.Resources.Usuario;
            var lines = file.Split(new[] { Environment.NewLine },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var item in lines)
            {
                var divisor = item.Split('|');
                var usuario = new Usuario()
                {
                    Codigo = int.Parse(divisor[0].Trim()),
                    Nome = divisor[1].Trim(),
                    ProporcaoRelacionamento = int.Parse(divisor[2].Trim()),
                    ProporcaoTempoLivre = int.Parse(divisor[3].Trim()),
                    ProporcaoNotas = int.Parse(divisor[4].Trim()),
                    ProporcaoConfianca = int.Parse(divisor[5].Trim()),
                    ProporcaoAtividadesComum = int.Parse(divisor[6].Trim()),
                    ProporcaoDominioConteudo = int.Parse(divisor[7].Trim()),
                    ProporcaoDedicacao = int.Parse(divisor[8].Trim()),
                    ProporcaoFaltas = int.Parse(divisor[9].Trim()),
                    ProporcaoInteligencia = int.Parse(divisor[10].Trim()),
                    ProporcaoComunicacao = int.Parse(divisor[11].Trim())
                };
                usuarios.Add(usuario);
            }

            usuarios = QualificaProporcoes(usuarios);

            System.Threading.Thread.Sleep(3000);

            MontaMenu();
        }

        public static Usuario MontaUsuario(Usuario usuario)
        {
            usuario.RelacionamentoFraco = usuario.ProporcaoRelacionamento >= 0 && usuario.ProporcaoRelacionamento <= 40;
            usuario.RelacionamentoMedio = usuario.ProporcaoRelacionamento >= 33 & usuario.ProporcaoRelacionamento <= 70;
            usuario.RelacionamentoForte = usuario.ProporcaoRelacionamento >= 60 & usuario.ProporcaoRelacionamento <= 100;

            usuario.TempoLivreFraco = usuario.ProporcaoTempoLivre >= 0 && usuario.ProporcaoTempoLivre <= 40;
            usuario.TempoLivreMedio = usuario.ProporcaoTempoLivre >= 33 & usuario.ProporcaoTempoLivre <= 70;
            usuario.TempoLivreForte = usuario.ProporcaoTempoLivre >= 60 & usuario.ProporcaoTempoLivre <= 100;

            usuario.NotasFraco = usuario.ProporcaoNotas >= 0 && usuario.ProporcaoNotas <= 40;
            usuario.NotasMedio = usuario.ProporcaoNotas >= 33 & usuario.ProporcaoNotas <= 70;
            usuario.NotasForte = usuario.ProporcaoNotas >= 60 & usuario.ProporcaoNotas <= 100;

            usuario.ConfiancaFraco = usuario.ProporcaoConfianca >= 0 && usuario.ProporcaoConfianca <= 40;
            usuario.ConfiancaMedio = usuario.ProporcaoConfianca >= 33 & usuario.ProporcaoConfianca <= 70;
            usuario.ConfiancaForte = usuario.ProporcaoConfianca >= 60 & usuario.ProporcaoConfianca <= 100;

            usuario.AtividadesComumFraco = usuario.ProporcaoAtividadesComum >= 0 && usuario.ProporcaoAtividadesComum <= 40;
            usuario.AtividadesComumMedio = usuario.ProporcaoAtividadesComum >= 33 & usuario.ProporcaoAtividadesComum <= 70;
            usuario.AtividadesComumForte = usuario.ProporcaoAtividadesComum >= 60 & usuario.ProporcaoAtividadesComum <= 100;

            usuario.DominioConteudoFraco = usuario.ProporcaoDominioConteudo >= 0 && usuario.ProporcaoDominioConteudo <= 40;
            usuario.DominioConteudoMedio = usuario.ProporcaoDominioConteudo >= 33 & usuario.ProporcaoDominioConteudo <= 70;
            usuario.DominioConteudoForte = usuario.ProporcaoDominioConteudo >= 60 & usuario.ProporcaoDominioConteudo <= 100;

            usuario.DedicacaoFraco = usuario.ProporcaoDedicacao >= 0 && usuario.ProporcaoDedicacao <= 40;
            usuario.DedicacaoMedio = usuario.ProporcaoDedicacao >= 33 & usuario.ProporcaoDedicacao <= 70;
            usuario.DedicacaoForte = usuario.ProporcaoDedicacao >= 60 & usuario.ProporcaoDedicacao <= 100;

            usuario.FaltasFraco = usuario.ProporcaoFaltas >= 0 && usuario.ProporcaoFaltas <= 40;
            usuario.FaltasMedio = usuario.ProporcaoFaltas >= 33 & usuario.ProporcaoFaltas <= 70;
            usuario.FaltasForte = usuario.ProporcaoFaltas >= 60 & usuario.ProporcaoFaltas <= 100;

            usuario.InteligenciaFraco = usuario.ProporcaoInteligencia >= 0 && usuario.ProporcaoInteligencia <= 40;
            usuario.InteligenciaMedio = usuario.ProporcaoInteligencia >= 33 & usuario.ProporcaoInteligencia <= 70;
            usuario.InteligenciaForte = usuario.ProporcaoInteligencia >= 60 & usuario.ProporcaoInteligencia <= 100;

            usuario.ComunicacaoFraco = usuario.ProporcaoComunicacao >= 0 && usuario.ProporcaoComunicacao <= 40;
            usuario.ComunicacaoMedio = usuario.ProporcaoComunicacao >= 33 & usuario.ProporcaoComunicacao <= 70;
            usuario.ComunicacaoForte = usuario.ProporcaoComunicacao >= 60 & usuario.ProporcaoComunicacao <= 100;

            return usuario;
        }

        public static Usuario QualificaProporcoesPessoal(Usuario usuario)
        {
            return MontaUsuario(usuario);
        }

        public static List<Usuario> QualificaProporcoes(List<Usuario> usuarios)
        {
            List<Usuario> retorno = new List<Usuario>();
            foreach (var usuario in usuarios)
            {
                retorno.Add(MontaUsuario(usuario));
            }

            return retorno;
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
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresRelacionamento = CalcularDesempenhoRelacionamento(usuarios);
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresTempoLivres = CalcularDesempenhoTempoLivre(usuarios);
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresNotas = CalcularDesempenhoNotas(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresConfianca = CalcularDesempenhoConfianca(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresAtividadesComum = CalcularDesempenhoAtividadesComum(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresDominioConteudo = CalcularDesempenhoDominioConteudo(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usariosComMelhoresDedicacoes = CalcularDesempenhoDedicacao(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresFaltas = CalcularDesempenhoFaltas(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosMaisInteligentes = CalcularDesempenhoInteligencia(usuarios, int.Parse(opcao));
            Dictionary<Tuple<Usuario, Usuario>, int> usuariosMaisComunicativos = CalcularDesempenhoComunicacao(usuarios, int.Parse(opcao));

            Dictionary<Tuple<Usuario, Usuario>, int> duplaFinal = MontaMelhorDupla(usuariosComMelhoresRelacionamento, usuariosComMelhoresTempoLivres, usuariosComMelhoresNotas, usuariosComMelhoresConfianca,
                                                            usuariosComMelhoresAtividadesComum, usuariosComMelhoresDominioConteudo, usariosComMelhoresDedicacoes, usuariosComMelhoresFaltas,
                                                            usuariosMaisInteligentes, usuariosMaisComunicativos);

          
                var nome1 = duplaFinal.First().Key.Item1.Nome;
                var nome2 = duplaFinal.Last().Key.Item2.Nome;

                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("\tMelhor dupla formada:");
                Console.WriteLine("");
                Console.WriteLine("1. " + nome1 + "!");
                Console.WriteLine("2. " + nome2 + "!");
                Console.WriteLine("");
                Console.WriteLine("Clique para continuar.");
                Console.ReadKey();
                MontaMenuFinal();
            
        }

        #region Calculo Desempenho        

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoRelacionamento(List<Usuario> usuarios)
        {
            Dictionary<Tuple<Usuario, Usuario>, int> dicionario = new Dictionary<Tuple<Usuario, Usuario>, int>();
            int valorAtual;

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.RelacionamentoFraco && item.RelacionamentoFraco)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 0);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.RelacionamentoFraco && item.RelacionamentoMedio)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 15);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.RelacionamentoFraco && item.RelacionamentoForte)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 30);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.RelacionamentoMedio && item.RelacionamentoFraco)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 15);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.RelacionamentoMedio && item.RelacionamentoMedio)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 50);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.RelacionamentoMedio && item.RelacionamentoForte)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 75);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.RelacionamentoForte && item.RelacionamentoFraco)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 30);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.RelacionamentoForte && item.RelacionamentoMedio)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 75);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.RelacionamentoForte && item.RelacionamentoForte)
                {
                    if (!dicionario.ContainsKey(new Tuple<Usuario, Usuario>(usuarioPessoal, item)))
                        dicionario.Add(new Tuple<Usuario, Usuario>(usuarioPessoal, item), 100);
                    else
                    {
                        if (dicionario.TryGetValue(new Tuple<Usuario, Usuario>(usuarioPessoal, item), out valorAtual))
                            dicionario[new Tuple<Usuario, Usuario>(usuarioPessoal, item)] = valorAtual + 100;
                    }
                }
            }

            return dicionario;
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoTempoLivre(List<Usuario> usuarios)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoNotas(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoConfianca(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoAtividadesComum(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoDominioConteudo(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoDedicacao(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoFaltas(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoInteligencia(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Tuple<Usuario, Usuario>, int> CalcularDesempenhoComunicacao(List<Usuario> usuarios, int opcao)
        {
            throw new NotImplementedException();
        }


        private static Dictionary<Tuple<Usuario, Usuario>, int> MontaMelhorDupla(Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresRelacionamento, Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresTempoLivres,
                                                        Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresNotas, Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresConfianca, 
                                                        Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresAtividadesComum, Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresDominioConteudo,
                                                        Dictionary<Tuple<Usuario, Usuario>, int> usariosComMelhoresDedicacoes, Dictionary<Tuple<Usuario, Usuario>, int> usuariosComMelhoresFaltas,
                                                        Dictionary<Tuple<Usuario, Usuario>, int> usuariosMaisInteligentes, Dictionary<Tuple<Usuario, Usuario>, int> usuariosMaisComunicativos)
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



