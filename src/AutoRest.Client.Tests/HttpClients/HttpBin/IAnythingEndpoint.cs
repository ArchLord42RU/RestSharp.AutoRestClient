using System.Threading.Tasks;
using AutoRest.Client.Attributes.Requests;
using AutoRest.Client.Attributes.Response;
using AutoRest.Client.Tests.HttpClients.HttpBin.Models;

namespace AutoRest.Client.Tests.HttpClients.HttpBin
{
    [Route("anything")]
    public interface IAnythingEndpoint
    {
        [FromResponse]
        [HttpPost("endpoint")]
        Task<AnythingResponse> GetResponseAsync();
    }
}