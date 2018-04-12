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
            Console.WriteLine(usuarioPessoal.Nome + ", aguarde enquanto estamos recolhendo as informações dos usuários...");

            string file = Properties.Resources.Usuario;
            var lines = file.Split(new[] { Environment.NewLine },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();

            usuarios = new List<Usuario>();
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

            MontaFuzzy(usuarios);
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

        /*public static void MontaLogica(string opcao)
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
        }*/

        public static void MontaFuzzy(List<Usuario> usuarios)
        {
            System.Threading.Thread.Sleep(1000);

            Dictionary<Usuario, int> usuariosComMelhoresRelacionamento = CalcularDesempenhoRelacionamento(usuarios);
            Dictionary<Usuario, int> usuariosComMelhoresTempoLivres = CalcularDesempenhoTempoLivre(usuarios);
            Dictionary<Usuario, int> usuariosComMelhoresNotas = CalcularDesempenhoNotas(usuarios);
            Dictionary<Usuario, int> usuariosComMelhoresConfianca = CalcularDesempenhoConfianca(usuarios);
            Dictionary<Usuario, int> usuariosComMelhoresAtividadesComum = CalcularDesempenhoAtividadesComum(usuarios);
            Dictionary<Usuario, int> usuariosComMelhoresDominioConteudo = CalcularDesempenhoDominioConteudo(usuarios);
            Dictionary<Usuario, int> usariosComMelhoresDedicacoes = CalcularDesempenhoDedicacao(usuarios);
            Dictionary<Usuario, int> usuariosComMelhoresFaltas = CalcularDesempenhoFaltas(usuarios);
            Dictionary<Usuario, int> usuariosMaisInteligentes = CalcularDesempenhoInteligencia(usuarios);
            Dictionary<Usuario, int> usuariosMaisComunicativos = CalcularDesempenhoComunicacao(usuarios);

            Dictionary<Usuario, KeyValuePair<Usuario, int>> duplaFinal = MontaMelhorDupla(usuariosComMelhoresRelacionamento, usuariosComMelhoresTempoLivres, usuariosComMelhoresNotas, usuariosComMelhoresConfianca,
                                                            usuariosComMelhoresAtividadesComum, usuariosComMelhoresDominioConteudo, usariosComMelhoresDedicacoes, usuariosComMelhoresFaltas,
                                                            usuariosMaisInteligentes, usuariosMaisComunicativos);


            var maiorPontuacao = duplaFinal.First().Value.Value;
            var melhoresUsuarios = duplaFinal.Where(w => w.Value.Value == maiorPontuacao).ToList();

            var nome1 = usuarioPessoal.Nome;
            var nome2 = duplaFinal.First().Key.Nome;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\tMelhor dupla formada:");
            Console.WriteLine("");
            Console.WriteLine("1. " + nome1 + "!");
            Console.WriteLine("2. " + nome2 + "!");
            Console.WriteLine("");
            if (melhoresUsuarios.Count > 1)
            {
                Console.WriteLine("Também podem integrar: ");
                Console.WriteLine("");
                var bla = melhoresUsuarios.Skip(1).ToList();
                foreach (var item in bla)
                {
                    Console.WriteLine("* " + item.Key.Nome + ".");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Clique para continuar.");
            Console.ReadKey();
            MontaMenuFinal();

        }

        #region Calculo Desempenho        

        private static Dictionary<Usuario, int> CalcularDesempenhoRelacionamento(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.RelacionamentoFraco && item.RelacionamentoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.RelacionamentoFraco && item.RelacionamentoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.RelacionamentoFraco && item.RelacionamentoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.RelacionamentoMedio && item.RelacionamentoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.RelacionamentoMedio && item.RelacionamentoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.RelacionamentoMedio && item.RelacionamentoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.RelacionamentoForte && item.RelacionamentoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.RelacionamentoForte && item.RelacionamentoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.RelacionamentoForte && item.RelacionamentoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoTempoLivre(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.TempoLivreFraco && item.TempoLivreFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.TempoLivreFraco && item.TempoLivreMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.TempoLivreFraco && item.TempoLivreForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.TempoLivreMedio && item.TempoLivreFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.TempoLivreMedio && item.TempoLivreMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.TempoLivreMedio && item.TempoLivreForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.TempoLivreForte && item.TempoLivreFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.TempoLivreForte && item.TempoLivreMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.TempoLivreForte && item.TempoLivreForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoNotas(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.NotasFraco && item.NotasFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.NotasFraco && item.NotasMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.NotasFraco && item.NotasForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.NotasMedio && item.NotasFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.NotasMedio && item.NotasMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.NotasMedio && item.NotasForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.NotasForte && item.NotasFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.NotasForte && item.NotasMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.NotasForte && item.NotasForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoConfianca(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.ConfiancaFraco && item.ConfiancaFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.ConfiancaFraco && item.ConfiancaMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.ConfiancaFraco && item.ConfiancaForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.ConfiancaMedio && item.ConfiancaFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.ConfiancaMedio && item.ConfiancaMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.ConfiancaMedio && item.ConfiancaForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.ConfiancaForte && item.ConfiancaFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.ConfiancaForte && item.ConfiancaMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.ConfiancaForte && item.ConfiancaForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoAtividadesComum(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.AtividadesComumFraco && item.AtividadesComumFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.AtividadesComumFraco && item.AtividadesComumMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.AtividadesComumFraco && item.AtividadesComumForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.AtividadesComumMedio && item.AtividadesComumFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.AtividadesComumMedio && item.AtividadesComumMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.AtividadesComumMedio && item.AtividadesComumForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.AtividadesComumForte && item.AtividadesComumFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.AtividadesComumForte && item.AtividadesComumMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.AtividadesComumForte && item.AtividadesComumForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoDominioConteudo(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.DominioConteudoFraco && item.DominioConteudoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.DominioConteudoFraco && item.DominioConteudoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.DominioConteudoFraco && item.DominioConteudoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.DominioConteudoMedio && item.DominioConteudoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.DominioConteudoMedio && item.DominioConteudoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.DominioConteudoMedio && item.DominioConteudoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.DominioConteudoForte && item.DominioConteudoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.DominioConteudoForte && item.DominioConteudoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.DominioConteudoForte && item.DominioConteudoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoDedicacao(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.DedicacaoFraco && item.DedicacaoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.DedicacaoFraco && item.DedicacaoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.DedicacaoFraco && item.DedicacaoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.DedicacaoMedio && item.DedicacaoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.DedicacaoMedio && item.DedicacaoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.DedicacaoMedio && item.DedicacaoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.DedicacaoForte && item.DedicacaoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.DedicacaoForte && item.DedicacaoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.DedicacaoForte && item.DedicacaoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoFaltas(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.FaltasFraco && item.FaltasFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.FaltasFraco && item.FaltasMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.FaltasFraco && item.FaltasForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.FaltasMedio && item.FaltasFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.FaltasMedio && item.FaltasMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.FaltasMedio && item.FaltasForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.FaltasForte && item.FaltasFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.FaltasForte && item.FaltasMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.FaltasForte && item.FaltasForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoInteligencia(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.InteligenciaFraco && item.InteligenciaFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.InteligenciaFraco && item.InteligenciaMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.InteligenciaFraco && item.InteligenciaForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.InteligenciaMedio && item.InteligenciaFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.InteligenciaMedio && item.InteligenciaMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.InteligenciaMedio && item.InteligenciaForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.InteligenciaForte && item.InteligenciaFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.InteligenciaForte && item.InteligenciaMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.InteligenciaForte && item.InteligenciaForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }

        private static Dictionary<Usuario, int> CalcularDesempenhoComunicacao(List<Usuario> usuarios)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Monta Desempenho            

            foreach (var item in usuarios)
            {
                if (usuarioPessoal.ComunicacaoFraco && item.ComunicacaoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 0);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 0;
                    }
                }

                if (usuarioPessoal.ComunicacaoFraco && item.ComunicacaoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.ComunicacaoFraco && item.ComunicacaoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.ComunicacaoMedio && item.ComunicacaoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 15);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 15;
                    }
                }

                if (usuarioPessoal.ComunicacaoMedio && item.ComunicacaoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 50);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 50;
                    }
                }

                if (usuarioPessoal.ComunicacaoMedio && item.ComunicacaoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.ComunicacaoForte && item.ComunicacaoFraco)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 30);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 30;
                    }
                }

                if (usuarioPessoal.ComunicacaoForte && item.ComunicacaoMedio)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 75);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 75;
                    }
                }

                if (usuarioPessoal.ComunicacaoForte && item.ComunicacaoForte)
                {
                    if (!dicionario.ContainsKey(item))
                        dicionario.Add(item, 100);
                    else
                    {
                        if (dicionario.TryGetValue(item, out valorAtual))
                            dicionario[item] = valorAtual + 100;
                    }
                }
            }

            #endregion

            return dicionario;
        }


        private static Dictionary<Usuario, KeyValuePair<Usuario, int>> MontaMelhorDupla(
                                                        Dictionary<Usuario, int> usuariosComMelhoresRelacionamento, Dictionary<Usuario, int> usuariosComMelhoresTempoLivres,
                                                        Dictionary<Usuario, int> usuariosComMelhoresNotas, Dictionary<Usuario, int> usuariosComMelhoresConfianca,
                                                        Dictionary<Usuario, int> usuariosComMelhoresAtividadesComum, Dictionary<Usuario, int> usuariosComMelhoresDominioConteudo,
                                                        Dictionary<Usuario, int> usariosComMelhoresDedicacoes, Dictionary<Usuario, int> usuariosComMelhoresFaltas,
                                                        Dictionary<Usuario, int> usuariosMaisInteligentes, Dictionary<Usuario, int> usuariosMaisComunicativos)
        {
            Dictionary<Usuario, int> dicionario = new Dictionary<Usuario, int>();
            int valorAtual;

            #region Junção dos Resultados

            foreach (var item in usuariosComMelhoresRelacionamento)
                dicionario.Add(item.Key, item.Value);

            foreach (var item in usuariosComMelhoresTempoLivres)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosComMelhoresNotas)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosComMelhoresConfianca)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosComMelhoresAtividadesComum)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosComMelhoresDominioConteudo)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usariosComMelhoresDedicacoes)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosComMelhoresFaltas)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosMaisInteligentes)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            foreach (var item in usuariosMaisComunicativos)
            {
                if (!dicionario.ContainsKey(item.Key))
                    dicionario.Add(item.Key, item.Value);
                else
                {
                    if (dicionario.TryGetValue(item.Key, out valorAtual))
                        dicionario[item.Key] = valorAtual + item.Value;
                }
            }

            #endregion

            return dicionario.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);
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

            //MontaLogica(opcao);
        }

        public static void MontaMenuErro()
        {
            Console.Clear();
            Console.WriteLine("Opção Inválida.");
            Console.WriteLine("");
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║ \tDeseja refazer o cálculo?        ║");
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
                    Console.Clear();
                    Main(new string[] { "teste" });
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
                Console.WriteLine("║ \tDeseja refazer o cálculo?        ║");
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
                        Console.Clear();
                        Main(new string[] { "teste"});
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

}



