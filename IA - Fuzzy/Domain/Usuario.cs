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
    }
}
