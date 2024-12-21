using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.utils;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Investimento : ControllerBase
    {
        [HttpPost("calcular-investimento-CDB")]
        [SwaggerResponse(200, "Resultado do cálculo de investimento CDB", typeof(CalculoCdiResponse))]
        public ActionResult<CalculoCdiResponse> CalcularCdi([FromBody] CalculoCdiRequest request)
        {
            if (!(request.ValorInicial > 0 && request.CdiAnual > 0 && request.PercentualCdi > 0 && request.MesesInvestidos > 0))
            {
                return BadRequest(new CalculoCdiResponse
                {
                    Mensagem = "Todos os valores devem ser positivos e maiores que zero."
                });
            }

            decimal cdiDiario = CalculosServices.CalcularCdiDiario(request.CdiAnual);
            int diasTotais = request.MesesInvestidos * 30; // Aproximadamente 30 dias por mês
            decimal valorTotalInvestido = request.ValorInicial;

            decimal rendimentoFinal = CalculosServices.CalcularRendimentoInicial(request, cdiDiario, diasTotais);
            rendimentoFinal = CalculosServices.CalcularRendimentoComAportes(request, cdiDiario, rendimentoFinal, diasTotais, ref valorTotalInvestido);
            decimal impostoIOF = CalculosServices.CalcularImpostoIOF(rendimentoFinal, diasTotais);
            decimal impostoIR = CalculosServices.CalcularImpostoIR(rendimentoFinal, valorTotalInvestido, diasTotais);
            decimal valorLiquido = CalculosServices.CalcularValorLiquido(rendimentoFinal, impostoIOF, impostoIR);
            decimal rendimentoLiquido = CalculosServices.CalcularRendimentoLiquido(rendimentoFinal, valorTotalInvestido, impostoIOF, impostoIR);


            return Ok(new CalculoCdiResponse
            {
                ValorTotalInvestido = valorTotalInvestido,
                RendimentoTotalBruto = Math.Round(rendimentoFinal, 2),
                RendimentoTotalLiquido = Math.Round(valorLiquido, 2),
                ValorBrutoRendimento = Math.Round(rendimentoLiquido, 2) + Math.Round(impostoIOF, 2) + Math.Round(impostoIR, 2),
                ValorLiquidoRendimento = Math.Round(rendimentoLiquido, 2),
                ImpostoIOF = Math.Round(impostoIOF, 2),
                ImpostoIR = Math.Round(impostoIR, 2),
                
                
                Mensagem = "Cálculo realizado com sucesso."
            });
        }

        [HttpPost("calcular-juros-compostos")]
        [SwaggerResponse(200, "Resultado do cálculo de Juros Compostos", typeof(CalculoJurosCompostosResponse))]
        public ActionResult<CalculoJurosCompostosResponse> CalcularJurosCompostos([FromBody] CalculoJurosCompostosRequest request)
        {
            if (!(request.CapitalInicial > 0 && request.TaxaJuros > 0 && request.Periodo > 0))
            {
                return BadRequest(new CalculoJurosCompostosResponse
                {
                    Mensagem = "Todos os valores devem ser positivos e maiores que zero."
                });
            }

            double montanteFinal = CalculosServices.CalcularJurosCompostos(request.CapitalInicial, request.TaxaJuros, request.Periodo);

            return Ok(new CalculoJurosCompostosResponse
            {
               CapitalInicial = request.CapitalInicial,
               TaxaJuros = request.TaxaJuros,
               Periodo = request.Periodo,
               MontanteFinal = Math.Round(montanteFinal, 2),
               Mensagem = "Cálculo realizado com sucesso."
            });
        }

        [HttpPost("calcular-gasolina-etanol")]
        [SwaggerResponse(200, "Resultado do cálculo de qual vale mais a pena abastecer, Gasolina ou Etanol", typeof(CalculoJurosCompostosResponse))]
        public ActionResult<CalculoGasolinaEtanolResponse> CalcularGasolinaEtanol([FromBody] CalculoGasolinaEtanolRequest request)
        {
            if (!(request.Gasolina > 0 && request.Etanol > 0))
            {
                return BadRequest(new CalculoGasolinaEtanolResponse
                {
                    Mensagem = "Todos os valores devem ser positivos e maiores que zero."
                });
            }

            string mensagem = CalculosServices.CalcularGasolinaEtanol(request.Gasolina, request.Etanol);

            return Ok(new CalculoGasolinaEtanolResponse
            {
                Gasolina = request.Gasolina,
                Etanol = request.Etanol,
                Mensagem = $"Cálculo realizado com sucesso, {mensagem}"
            });
        }
    }
}
