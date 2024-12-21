using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Model.Request
{
    public class CalculoJurosCompostosRequest
    {
        [SwaggerSchema("Capital inicial")]
        [DefaultValue(1000)]
        public double CapitalInicial { get; set; }

        [SwaggerSchema("Taxa de juros")]
        [DefaultValue(5)]
        public double TaxaJuros { get; set; }

        [SwaggerSchema("Periodo em meses")]
        [DefaultValue(12)]
        public int Periodo { get; set; }
    }
}
