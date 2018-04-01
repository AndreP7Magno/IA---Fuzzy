namespace IA___Fuzzy.Domain
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public int CodMelhorRelacionado { get; set; }
        public bool PossuiTempoLivre { get; set; }
        public int QuantidadeHorasTempoLivre { get; set; }
        public double NotaProgramacao { get; set; }
        public double NotaEstruturaDados { get; set; }
        public double NotaBancoDados { get; set; }
        public double NotaCalculo { get; set; }
        public double NotaGerenciaProjetos { get; set; }
        public int CodMaiorConfianca { get; set; }
        public int CodMaiorAtividadesComum { get; set; }
        public bool PossuiDominioProgramacao { get; set; }
        public bool PossuiDominioEstruturaDados { get; set; }
        public bool PossuiDominioBancoDados { get; set; }
        public bool PossuiDominioCalculo { get; set; }
        public bool PossuiDominioGerenciaProjetos { get; set; }
    }
}
