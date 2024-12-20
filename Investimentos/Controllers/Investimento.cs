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
            if (!CalculosServices.ValidarRequest(request))
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
     
    }
}
