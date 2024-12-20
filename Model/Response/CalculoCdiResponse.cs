namespace Model.Response
{
    public class CalculoCdiResponse
    {
        public decimal ValorTotalInvestido { get; set; }
        public decimal RendimentoTotalBruto { get; set; }
        public decimal RendimentoTotalLiquido { get; set; }
        public decimal ValorBrutoRendimento { get; set; }
        public decimal ValorLiquidoRendimento { get; set; }
        public decimal ImpostoIR { get; set; }
        public decimal ImpostoIOF { get; set; }                
        public string Mensagem { get; set; } = string.Empty;
    }
}
