namespace Model.Response
{
    public class CalculoGasolinaEtanolResponse
    {
        public double Gasolina { get; set; }
        public double Etanol { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
