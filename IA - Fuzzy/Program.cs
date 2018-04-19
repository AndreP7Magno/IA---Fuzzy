using DotFuzzy;
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
            double input = 0.0;

            Console.Write("Por favor, informe o seu nome: ");
            pessoa.Nome = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine(pessoa.Nome + ", de 0 a 100, nos informe qual seria a sua melhor proporção em relação à:");
            Console.WriteLine("");
            Console.Write("Relacionamento: ");
            pessoa.ProporcaoRelacionamento = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Tempo Livre: ");
            pessoa.ProporcaoTempoLivre = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Notas: ");
            pessoa.ProporcaoNotas = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Confiança: ");
            pessoa.ProporcaoConfianca = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Atividades em Comum: ");
            pessoa.ProporcaoAtividadesComum = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Domínio do Conteúdo: ");
            pessoa.ProporcaoDominioConteudo = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Dedicação: ");
            pessoa.ProporcaoDedicacao = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Faltas: ");
            pessoa.ProporcaoFaltas = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Inteligência: ");
            pessoa.ProporcaoInteligencia = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;
            Console.Write("Comunicação: ");
            pessoa.ProporcaoComunicacao = double.TryParse(Console.ReadLine(), out input) ? input : (double?)null;

            usuarioPessoal = pessoa;

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
                    ProporcaoRelacionamento = double.Parse(divisor[2].Trim()),
                    ProporcaoTempoLivre = double.Parse(divisor[3].Trim()),
                    ProporcaoNotas = double.Parse(divisor[4].Trim()),
                    ProporcaoConfianca = double.Parse(divisor[5].Trim()),
                    ProporcaoAtividadesComum = double.Parse(divisor[6].Trim()),
                    ProporcaoDominioConteudo = double.Parse(divisor[7].Trim()),
                    ProporcaoDedicacao = double.Parse(divisor[8].Trim()),
                    ProporcaoFaltas = double.Parse(divisor[9].Trim()),
                    ProporcaoInteligencia = double.Parse(divisor[10].Trim()),
                    ProporcaoComunicacao = double.Parse(divisor[11].Trim())
                };
                usuarios.Add(usuario);
            }

            System.Threading.Thread.Sleep(3000);

            MontaFuzzy(usuarios);
        }

        public static void MontaFuzzy(List<Usuario> usuarios)
        {
            System.Threading.Thread.Sleep(1000);

            Dictionary<Usuario, double> pontuacoes = new Dictionary<Usuario, double>();
            double Relacionamento, TempoLivre, Notas, Confianca, AtividadesComum;
            double DominioConteudo, Dedicacao, Faltas, Inteligentes, Comunicativos;

            foreach (var item in usuarios)
            {
                Relacionamento = CalcularDesempenhoRelacionamento(usuarioPessoal, item);
                if (Double.IsNaN(Relacionamento) || Double.IsInfinity(Relacionamento)) Relacionamento = 0;

                TempoLivre = CalcularDesempenhoTempoLivre(usuarioPessoal, item);
                if (Double.IsNaN(TempoLivre) || Double.IsInfinity(TempoLivre)) TempoLivre = 0;

                Notas = CalcularDesempenhoNotas(usuarioPessoal, item);
                if (Double.IsNaN(Notas) || Double.IsInfinity(Notas)) Notas = 0;

                Confianca = CalcularDesempenhoConfianca(usuarioPessoal, item);
                if (Double.IsNaN(Confianca) || Double.IsInfinity(Confianca)) Confianca = 0;

                AtividadesComum = CalcularDesempenhoAtividadesComum(usuarioPessoal, item);
                if (Double.IsNaN(AtividadesComum) || Double.IsInfinity(AtividadesComum)) AtividadesComum = 0;

                DominioConteudo = CalcularDesempenhoDominioConteudo(usuarioPessoal, item);
                if (Double.IsNaN(DominioConteudo) || Double.IsInfinity(DominioConteudo)) DominioConteudo = 0;

                Dedicacao = CalcularDesempenhoDedicacao(usuarioPessoal, item);
                if (Double.IsNaN(Dedicacao) || Double.IsInfinity(Dedicacao)) Dedicacao = 0;

                Faltas = CalcularDesempenhoFaltas(usuarioPessoal, item);
                if (Double.IsNaN(Faltas) || Double.IsInfinity(Faltas)) Faltas = 0;

                Inteligentes = CalcularDesempenhoInteligencia(usuarioPessoal, item);
                if (Double.IsNaN(Inteligentes) || Double.IsInfinity(Inteligentes)) Inteligentes = 0;

                Comunicativos = CalcularDesempenhoComunicacao(usuarioPessoal, item);
                if (Double.IsNaN(Comunicativos) || Double.IsInfinity(Comunicativos)) Comunicativos = 0;

                var media =
                    (Relacionamento + TempoLivre + Notas + Confianca + AtividadesComum +
                     AtividadesComum + DominioConteudo + Dedicacao + Faltas + Inteligentes + Comunicativos
                    ) / 10;

                pontuacoes.Add(item, media);
            }

            Dictionary<Usuario, KeyValuePair<Usuario, double>> resultado = MontaMelhorDupla(pontuacoes);

            var maiorPontuacao = resultado.First().Value.Value;
            var diferencaMenor = maiorPontuacao - 5.0;
            var melhoresUsuarios = resultado.Where(w => w.Value.Value >= diferencaMenor).ToList();

            var nome1 = usuarioPessoal.Nome;
            var nome2 = resultado.First().Key.Nome;

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

        private static double CalcularDesempenhoRelacionamento(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable RelacionamentoPessoalQTD = new LinguisticVariable("relacionamentoPessoalQTD");
            RelacionamentoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            RelacionamentoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            RelacionamentoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable RelacionamentoGeralQTD = new LinguisticVariable("relacionamentoGeralQTD");
            RelacionamentoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            RelacionamentoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            RelacionamentoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));
            
            FuzzyEngine fuzzyEngineRelacionamento = new FuzzyEngine();
            fuzzyEngineRelacionamento.LinguisticVariableCollection.Add(RelacionamentoPessoalQTD);
            fuzzyEngineRelacionamento.LinguisticVariableCollection.Add(RelacionamentoGeralQTD);
            fuzzyEngineRelacionamento.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineRelacionamento.Consequent = "riscoQTD";

            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Baixo) AND (relacionamentoGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Baixo) AND (relacionamentoGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Baixo) AND (relacionamentoGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Medio) AND (relacionamentoGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Medio) AND (relacionamentoGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Medio) AND (relacionamentoGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Alto) AND (relacionamentoGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Alto) AND (relacionamentoGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineRelacionamento.FuzzyRuleCollection.Add(new FuzzyRule("IF (relacionamentoPessoalQTD IS Alto) AND (relacionamentoGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            RelacionamentoPessoalQTD.InputValue = usuarioPessoal.ProporcaoRelacionamento.HasValue ? usuarioPessoal.ProporcaoRelacionamento.Value : 0;
            RelacionamentoGeralQTD.InputValue = usuarioGeral.ProporcaoRelacionamento.HasValue ? usuarioGeral.ProporcaoRelacionamento.Value : 0;

            return fuzzyEngineRelacionamento.Defuzzify();
        }

        private static double CalcularDesempenhoTempoLivre(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable TempoLivrePessoalQTD = new LinguisticVariable("tempoLivrePessoalQTD");
            TempoLivrePessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            TempoLivrePessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            TempoLivrePessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable TempoLivreGeralQTD = new LinguisticVariable("tempoLivreGeralQTD");
            TempoLivreGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            TempoLivreGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            TempoLivreGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineTempoLivre = new FuzzyEngine();
            fuzzyEngineTempoLivre.LinguisticVariableCollection.Add(TempoLivrePessoalQTD);
            fuzzyEngineTempoLivre.LinguisticVariableCollection.Add(TempoLivreGeralQTD);
            fuzzyEngineTempoLivre.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineTempoLivre.Consequent = "riscoQTD";

            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Baixo) AND (tempoLivreGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Baixo) AND (tempoLivreGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Baixo) AND (tempoLivreGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Medio) AND (tempoLivreGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Medio) AND (tempoLivreGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Medio) AND (tempoLivreGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Alto) AND (tempoLivreGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Alto) AND (tempoLivreGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineTempoLivre.FuzzyRuleCollection.Add(new FuzzyRule("IF (tempoLivrePessoalQTD IS Alto) AND (tempoLivreGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            TempoLivrePessoalQTD.InputValue = usuarioPessoal.ProporcaoTempoLivre.HasValue ? usuarioPessoal.ProporcaoTempoLivre.Value : 0;
            TempoLivreGeralQTD.InputValue = usuarioGeral.ProporcaoTempoLivre.HasValue ? usuarioGeral.ProporcaoTempoLivre.Value : 0;

            return fuzzyEngineTempoLivre.Defuzzify();
        }

        private static double CalcularDesempenhoNotas(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable NotasPessoalQTD = new LinguisticVariable("notasPessoalQTD");
            NotasPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            NotasPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            NotasPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable NotasGeralQTD = new LinguisticVariable("notasGeralQTD");
            NotasGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            NotasGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            NotasGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineNotas = new FuzzyEngine();
            fuzzyEngineNotas.LinguisticVariableCollection.Add(NotasPessoalQTD);
            fuzzyEngineNotas.LinguisticVariableCollection.Add(NotasGeralQTD);
            fuzzyEngineNotas.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineNotas.Consequent = "riscoQTD";

            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Baixo) AND (notasGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Baixo) AND (notasGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Baixo) AND (notasGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Medio) AND (notasGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Medio) AND (notasGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Medio) AND (notasGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Alto) AND (notasGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Alto) AND (notasGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineNotas.FuzzyRuleCollection.Add(new FuzzyRule("IF (notasPessoalQTD IS Alto) AND (notasGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            NotasPessoalQTD.InputValue = usuarioPessoal.ProporcaoNotas.HasValue ? usuarioPessoal.ProporcaoNotas.Value : 0;
            NotasGeralQTD.InputValue = usuarioGeral.ProporcaoNotas.HasValue ? usuarioGeral.ProporcaoNotas.Value : 0;

            return fuzzyEngineNotas.Defuzzify();
        }

        private static double CalcularDesempenhoConfianca(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable ConfiancaPessoalQTD = new LinguisticVariable("confiancaPessoalQTD");
            ConfiancaPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            ConfiancaPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            ConfiancaPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable ConfiancaGeralQTD = new LinguisticVariable("confiancaGeralQTD");
            ConfiancaGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            ConfiancaGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            ConfiancaGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineConfianca = new FuzzyEngine();
            fuzzyEngineConfianca.LinguisticVariableCollection.Add(ConfiancaPessoalQTD);
            fuzzyEngineConfianca.LinguisticVariableCollection.Add(ConfiancaGeralQTD);
            fuzzyEngineConfianca.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineConfianca.Consequent = "riscoQTD";

            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Baixo) AND (confiancaGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Baixo) AND (confiancaGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Baixo) AND (confiancaGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Medio) AND (confiancaGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Medio) AND (confiancaGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Medio) AND (confiancaGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Alto) AND (confiancaGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Alto) AND (confiancaGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineConfianca.FuzzyRuleCollection.Add(new FuzzyRule("IF (confiancaPessoalQTD IS Alto) AND (confiancaGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            ConfiancaPessoalQTD.InputValue = usuarioPessoal.ProporcaoConfianca.HasValue ? usuarioPessoal.ProporcaoConfianca.Value : 0;
            ConfiancaGeralQTD.InputValue = usuarioGeral.ProporcaoConfianca.HasValue ? usuarioGeral.ProporcaoConfianca.Value : 0;

            return fuzzyEngineConfianca.Defuzzify();
        }

        private static double CalcularDesempenhoAtividadesComum(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable AtividadesComumPessoalQTD = new LinguisticVariable("atividadesComumPessoalQTD");
            AtividadesComumPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            AtividadesComumPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            AtividadesComumPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable AtividadesComumGeralQTD = new LinguisticVariable("atividadesComumGeralQTD");
            AtividadesComumGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            AtividadesComumGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            AtividadesComumGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineAtividadesComum = new FuzzyEngine();
            fuzzyEngineAtividadesComum.LinguisticVariableCollection.Add(AtividadesComumPessoalQTD);
            fuzzyEngineAtividadesComum.LinguisticVariableCollection.Add(AtividadesComumGeralQTD);
            fuzzyEngineAtividadesComum.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineAtividadesComum.Consequent = "riscoQTD";

            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Baixo) AND (atividadesComumGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Baixo) AND (atividadesComumGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Baixo) AND (atividadesComumGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Medio) AND (atividadesComumGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Medio) AND (atividadesComumGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Medio) AND (atividadesComumGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Alto) AND (atividadesComumGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Alto) AND (atividadesComumGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineAtividadesComum.FuzzyRuleCollection.Add(new FuzzyRule("IF (atividadesComumPessoalQTD IS Alto) AND (atividadesComumGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            AtividadesComumPessoalQTD.InputValue = usuarioPessoal.ProporcaoAtividadesComum.HasValue ? usuarioPessoal.ProporcaoAtividadesComum.Value : 0;
            AtividadesComumGeralQTD.InputValue = usuarioGeral.ProporcaoAtividadesComum.HasValue ? usuarioGeral.ProporcaoAtividadesComum.Value : 0;

            return fuzzyEngineAtividadesComum.Defuzzify();
        }

        private static double CalcularDesempenhoDominioConteudo(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable DominioConteudoPessoalQTD = new LinguisticVariable("dominioConteudoPessoalQTD");
            DominioConteudoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            DominioConteudoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            DominioConteudoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable DominioConteudoGeralQTD = new LinguisticVariable("dominioConteudoGeralQTD");
            DominioConteudoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            DominioConteudoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            DominioConteudoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineDominioConteudo = new FuzzyEngine();
            fuzzyEngineDominioConteudo.LinguisticVariableCollection.Add(DominioConteudoPessoalQTD);
            fuzzyEngineDominioConteudo.LinguisticVariableCollection.Add(DominioConteudoGeralQTD);
            fuzzyEngineDominioConteudo.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineDominioConteudo.Consequent = "riscoQTD";

            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Baixo) AND (dominioConteudoGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Baixo) AND (dominioConteudoGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Baixo) AND (dominioConteudoGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Medio) AND (dominioConteudoGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Medio) AND (dominioConteudoGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Medio) AND (dominioConteudoGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Alto) AND (dominioConteudoGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Alto) AND (dominioConteudoGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineDominioConteudo.FuzzyRuleCollection.Add(new FuzzyRule("IF (dominioConteudoPessoalQTD IS Alto) AND (dominioConteudoGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            DominioConteudoPessoalQTD.InputValue = usuarioPessoal.ProporcaoDominioConteudo.HasValue ? usuarioPessoal.ProporcaoDominioConteudo.Value : 0;
            DominioConteudoGeralQTD.InputValue = usuarioGeral.ProporcaoDominioConteudo.HasValue ? usuarioGeral.ProporcaoDominioConteudo.Value : 0;

            return fuzzyEngineDominioConteudo.Defuzzify();
        }

        private static double CalcularDesempenhoDedicacao(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable DedicacaoPessoalQTD = new LinguisticVariable("dedicacaoPessoalQTD");
            DedicacaoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            DedicacaoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            DedicacaoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable DedicacaoGeralQTD = new LinguisticVariable("dedicacaoGeralQTD");
            DedicacaoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            DedicacaoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            DedicacaoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineDedicacao = new FuzzyEngine();
            fuzzyEngineDedicacao.LinguisticVariableCollection.Add(DedicacaoPessoalQTD);
            fuzzyEngineDedicacao.LinguisticVariableCollection.Add(DedicacaoGeralQTD);
            fuzzyEngineDedicacao.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineDedicacao.Consequent = "riscoQTD";

            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Baixo) AND (dedicacaoGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Baixo) AND (dedicacaoGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Baixo) AND (dedicacaoGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Medio) AND (dedicacaoGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Medio) AND (dedicacaoGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Medio) AND (dedicacaoGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Alto) AND (dedicacaoGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Alto) AND (dedicacaoGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineDedicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (dedicacaoPessoalQTD IS Alto) AND (dedicacaoGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            DedicacaoPessoalQTD.InputValue = usuarioPessoal.ProporcaoDedicacao.HasValue ? usuarioPessoal.ProporcaoDedicacao.Value : 0;
            DedicacaoGeralQTD.InputValue = usuarioGeral.ProporcaoDedicacao.HasValue ? usuarioGeral.ProporcaoDedicacao.Value : 0;

            return fuzzyEngineDedicacao.Defuzzify();
        }

        private static double CalcularDesempenhoFaltas(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable FaltasPessoalQTD = new LinguisticVariable("faltasPessoalQTD");
            FaltasPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            FaltasPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            FaltasPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable FaltasGeralQTD = new LinguisticVariable("faltasGeralQTD");
            FaltasGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            FaltasGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            FaltasGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineFaltas = new FuzzyEngine();
            fuzzyEngineFaltas.LinguisticVariableCollection.Add(FaltasPessoalQTD);
            fuzzyEngineFaltas.LinguisticVariableCollection.Add(FaltasGeralQTD);
            fuzzyEngineFaltas.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineFaltas.Consequent = "riscoQTD";

            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Baixo) AND (faltasGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Baixo) AND (faltasGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Baixo) AND (faltasGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Medio) AND (faltasGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Medio) AND (faltasGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Medio) AND (faltasGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Alto) AND (faltasGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Alto) AND (faltasGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineFaltas.FuzzyRuleCollection.Add(new FuzzyRule("IF (faltasPessoalQTD IS Alto) AND (faltasGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            FaltasPessoalQTD.InputValue = usuarioPessoal.ProporcaoFaltas.HasValue ? usuarioPessoal.ProporcaoFaltas.Value : 0;
            FaltasGeralQTD.InputValue = usuarioGeral.ProporcaoFaltas.HasValue ? usuarioGeral.ProporcaoFaltas.Value : 0;

            return fuzzyEngineFaltas.Defuzzify();
        }

        private static double CalcularDesempenhoInteligencia(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable InteligenciaPessoalQTD = new LinguisticVariable("inteligenciaPessoalQTD");
            InteligenciaPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            InteligenciaPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            InteligenciaPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable InteligenciaGeralQTD = new LinguisticVariable("inteligenciaGeralQTD");
            InteligenciaGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            InteligenciaGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            InteligenciaGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineInteligencia = new FuzzyEngine();
            fuzzyEngineInteligencia.LinguisticVariableCollection.Add(InteligenciaPessoalQTD);
            fuzzyEngineInteligencia.LinguisticVariableCollection.Add(InteligenciaGeralQTD);
            fuzzyEngineInteligencia.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineInteligencia.Consequent = "riscoQTD";

            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Baixo) AND (inteligenciaGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Baixo) AND (inteligenciaGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Baixo) AND (inteligenciaGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Medio) AND (inteligenciaGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Medio) AND (inteligenciaGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Medio) AND (inteligenciaGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Alto) AND (inteligenciaGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Alto) AND (inteligenciaGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineInteligencia.FuzzyRuleCollection.Add(new FuzzyRule("IF (inteligenciaPessoalQTD IS Alto) AND (inteligenciaGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            InteligenciaPessoalQTD.InputValue = usuarioPessoal.ProporcaoInteligencia.HasValue ? usuarioPessoal.ProporcaoInteligencia.Value : 0;
            InteligenciaGeralQTD.InputValue = usuarioGeral.ProporcaoInteligencia.HasValue ? usuarioGeral.ProporcaoInteligencia.Value : 0;

            return fuzzyEngineInteligencia.Defuzzify();
        }

        private static double CalcularDesempenhoComunicacao(Usuario usuarioPessoal, Usuario usuarioGeral)
        {
            LinguisticVariable ComunicacaoPessoalQTD = new LinguisticVariable("comunicacaoPessoalQTD");
            ComunicacaoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            ComunicacaoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            ComunicacaoPessoalQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable ComunicacaoGeralQTD = new LinguisticVariable("comunicacaoGeralQTD");
            ComunicacaoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 20, 30, 40));
            ComunicacaoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 35, 50, 50, 65));
            ComunicacaoGeralQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 60, 80, 90, 101));

            LinguisticVariable riscoQTD = new LinguisticVariable("riscoQTD");
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Baixo", 0, 10, 20, 30));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioBaixo", 20, 30, 40, 50));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Medio", 40, 50, 60, 70));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("MedioAlto", 60, 70, 80, 90));
            riscoQTD.MembershipFunctionCollection.Add(new MembershipFunction("Alto", 80, 90, 100, 101));

            FuzzyEngine fuzzyEngineComunicacao = new FuzzyEngine();
            fuzzyEngineComunicacao.LinguisticVariableCollection.Add(ComunicacaoPessoalQTD);
            fuzzyEngineComunicacao.LinguisticVariableCollection.Add(ComunicacaoGeralQTD);
            fuzzyEngineComunicacao.LinguisticVariableCollection.Add(riscoQTD);
            fuzzyEngineComunicacao.Consequent = "riscoQTD";

            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Baixo) AND (comunicacaoGeralQTD IS Baixo) THEN riscoQTD IS Baixo"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Baixo) AND (comunicacaoGeralQTD IS Medio) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Baixo) AND (comunicacaoGeralQTD IS Alto) THEN riscoQTD IS Medio"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Medio) AND (comunicacaoGeralQTD IS Baixo) THEN riscoQTD IS MedioBaixo"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Medio) AND (comunicacaoGeralQTD IS Medio) THEN riscoQTD IS Medio"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Medio) AND (comunicacaoGeralQTD IS Alto) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Alto) AND (comunicacaoGeralQTD IS Baixo) THEN riscoQTD IS Medio"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Alto) AND (comunicacaoGeralQTD IS Medio) THEN riscoQTD IS MedioAlto"));
            fuzzyEngineComunicacao.FuzzyRuleCollection.Add(new FuzzyRule("IF (comunicacaoPessoalQTD IS Alto) AND (comunicacaoGeralQTD IS Alto) THEN riscoQTD IS Alto"));

            ComunicacaoPessoalQTD.InputValue = usuarioPessoal.ProporcaoComunicacao.HasValue ? usuarioPessoal.ProporcaoComunicacao.Value : 0;
            ComunicacaoGeralQTD.InputValue = usuarioGeral.ProporcaoComunicacao.HasValue ? usuarioGeral.ProporcaoComunicacao.Value : 0;

            return fuzzyEngineComunicacao.Defuzzify();
        }


        private static Dictionary<Usuario, KeyValuePair<Usuario, double>> MontaMelhorDupla(Dictionary<Usuario, double> pontuacoes)
        {
            return pontuacoes.OrderByDescending(o => o.Value).ToDictionary(o => o.Key);
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
                    Main(new string[] { "teste" });
                    break;
                default:
                    MontaMenuErro();
                    break;
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



