using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class CalculoJurosCompostosResponse
    {
        public double CapitalInicial { get; set; }
        public double TaxaJuros { get; set; }
        public int Periodo { get; set; }
        public double MontanteFinal { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
