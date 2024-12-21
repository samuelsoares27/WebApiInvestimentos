using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Model.Request
{
    public class CalculoGasolinaEtanolRequest
    {
        [SwaggerSchema("Valor da Gasolina")]
        [DefaultValue(5.75)]
        public double Gasolina { get; set; }

        [SwaggerSchema("Valor do Etanol")]
        [DefaultValue(3.50)]
        public double Etanol { get; set; }
    }
}
