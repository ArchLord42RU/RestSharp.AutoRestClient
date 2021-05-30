using RestSharp;

namespace AutoRest.Client.Processing.Response
{
    public interface IResponseDeserializer
    {
        IRestResponse<TBody> Deserialize<TBody>(IRestResponse response);
    }
}