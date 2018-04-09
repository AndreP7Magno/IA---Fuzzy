namespace IA___Fuzzy.Domain
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int? ProporcaoRelacionamento { get; set; }
        public int? ProporcaoTempoLivre { get; set; }
        public int? ProporcaoNotas { get; set; }
        public int? ProporcaoConfianca { get; set; }
        public int? ProporcaoAtividadesComum { get; set; }
        public int? ProporcaoDominioConteudo { get; set; }
        public int? ProporcaoDedicacao { get; set; }
        public int? ProporcaoFaltas { get; set; }
        public int? ProporcaoInteligencia { get; set; }
        public int? ProporcaoComunicacao { get; set; }

        public bool RelacionamentoFraco { get; set; }
        public bool RelacionamentoMedio { get; set; }
        public bool RelacionamentoForte { get; set; }

        public bool TempoLivreFraco { get; set; }
        public bool TempoLivreMedio { get; set; }
        public bool TempoLivreForte { get; set; }

        public bool NotasFraco { get; set; }
        public bool NotasMedio { get; set; }
        public bool NotasForte { get; set; }

        public bool ConfiancaFraco { get; set; }
        public bool ConfiancaMedio { get; set; }
        public bool ConfiancaForte { get; set; }

        public bool AtividadesComumFraco { get; set; }
        public bool AtividadesComumMedio { get; set; }
        public bool AtividadesComumForte { get; set; }

        public bool DominioConteudoFraco { get; set; }
        public bool DominioConteudoMedio { get; set; }
        public bool DominioConteudoForte { get; set; }

        public bool DedicacaoFraco { get; set; }
        public bool DedicacaoMedio { get; set; }
        public bool DedicacaoForte { get; set; }

        public bool FaltasFraco { get; set; }
        public bool FaltasMedio { get; set; }
        public bool FaltasForte { get; set; }

        public bool InteligenciaFraco { get; set; }
        public bool InteligenciaMedio { get; set; }
        public bool InteligenciaForte { get; set; }

        public bool ComunicacaoFraco { get; set; }
        public bool ComunicacaoMedio { get; set; }
        public bool ComunicacaoForte { get; set; }
    }
}
