using ApiGateway.Dto;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGateway.Aggregator
{
    public class ArticuloDetailAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if(responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var articuloInventarioStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var productoStr = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();


            var articuloInventarioObj = JsonConvert.DeserializeObject<ArticuloInventarioDto>(articuloInventarioStr);
            var productoObj = JsonConvert.DeserializeObject<ProductoDto>(productoStr);

            articuloInventarioObj.PrecioVenta = productoObj.PrecioVenta;

            var contentBody = JsonConvert.SerializeObject(articuloInventarioObj);

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");

        }
    }
}
