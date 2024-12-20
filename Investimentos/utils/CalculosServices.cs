using Model.Request;

namespace WebApi.utils
{
    public static class CalculosServices
    {
        public static bool ValidarRequest(CalculoCdiRequest request)
        {
            return request.ValorInicial > 0 && request.CdiAnual > 0 && request.PercentualCdi > 0 && request.MesesInvestidos > 0;
        }

        public static decimal CalcularCdiDiario(decimal cdiAnual)
        {
            return cdiAnual / 252 / 100; // Dividido por 252 dias úteis no ano
        }

        public static decimal CalcularRendimentoInicial(CalculoCdiRequest request, decimal cdiDiario, int diasTotais)
        {
            decimal percentualCdiAjustado = request.PercentualCdi / 100m;
            return request.ValorInicial * (decimal)Math.Pow((double)(1 + cdiDiario * percentualCdiAjustado), diasTotais);
        }

        public static decimal CalcularRendimentoComAportes(CalculoCdiRequest request, decimal cdiDiario, decimal rendimentoFinal, int diasTotais, ref decimal valorTotalInvestido)
        {
            for (int i = 1; i <= request.MesesInvestidos; i++)
            {
                int diasRestantes = (request.MesesInvestidos - i) * 30;
                decimal percentualCdiAjustado = request.PercentualCdi / 100m;
                rendimentoFinal += request.ValorAporteMensal * (decimal)Math.Pow((double)(1 + cdiDiario * percentualCdiAjustado), diasRestantes);
                valorTotalInvestido += request.ValorAporteMensal;
            }
            return rendimentoFinal;
        }

        public static decimal CalcularImpostoIOF(decimal rendimentoFinal, int diasTotais)
        {
            if (diasTotais > 30) return 0; // IOF só é calculado nos primeiros 30 dias

            decimal aliquotaIOF = 1 - diasTotais / 30m; // Alíquota decrescente
            return rendimentoFinal * aliquotaIOF * 0.01m; // 1% simplificado
        }

        public static decimal CalcularImpostoIR(decimal rendimentoFinal, decimal valorTotalInvestido, int diasTotais)
        {
            decimal aliquotaIR = diasTotais switch
            {
                <= 180 => 0.225m, // 22,5%
                <= 360 => 0.20m,  // 20%
                <= 720 => 0.175m, // 17,5%
                _ => 0.15m        // 15%
            };

            return (rendimentoFinal - valorTotalInvestido) * aliquotaIR;
        }

        public static decimal CalcularValorLiquido(decimal rendimentoFinal, decimal impostoIOF, decimal impostoIR)
        {
            return rendimentoFinal - impostoIOF - impostoIR;
        }

        // Novo método para calcular o rendimento líquido
        public static decimal CalcularRendimentoLiquido(decimal rendimentoFinal, decimal valorTotalInvestido, decimal impostoIOF, decimal impostoIR)
        {
            return rendimentoFinal - valorTotalInvestido - impostoIOF - impostoIR;
        }

        // Métodos para cálculo de resultados em dias
        public static decimal CalcularRendimentoFinalEmDias(CalculoCdiRequest request, decimal cdiDiario, int diasTotais)
        {
            decimal percentualCdiAjustado = request.PercentualCdi / 100m;
            return request.ValorInicial * (decimal)Math.Pow((double)(1 + cdiDiario * percentualCdiAjustado), diasTotais);
        }

        public static decimal CalcularValorLiquidoEmDias(decimal rendimentoFinalEmDias, decimal impostoIOF, decimal impostoIR)
        {
            return rendimentoFinalEmDias - impostoIOF - impostoIR;
        }

        public static decimal CalcularRendimentoLiquidoEmDias(decimal rendimentoFinalEmDias, decimal valorTotalInvestido, decimal impostoIOF, decimal impostoIR)
        {
            return rendimentoFinalEmDias - valorTotalInvestido - impostoIOF - impostoIR;
        }
    }
}
