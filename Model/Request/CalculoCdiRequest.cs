using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Model.Request
{
    public class CalculoCdiRequest
    {
        [SwaggerSchema("Valor inicial do investimento")]
        [DefaultValue(40000)]  
        public decimal ValorInicial { get; set; }

        [SwaggerSchema("Valor do aporte mensal")]
        [DefaultValue(3500)]  
        public decimal ValorAporteMensal { get; set; }

        [SwaggerSchema("Taxa CDI anual em percentual")]
        [DefaultValue(12.15)]  
        public decimal CdiAnual { get; set; }

        [SwaggerSchema("Percentual do CDI que será utilizado")]
        [DefaultValue(107)]  
        public decimal PercentualCdi { get; set; }

        [SwaggerSchema("Número de meses investidos")]
        [DefaultValue(12)]  
        public int MesesInvestidos { get; set; }
    }
}
