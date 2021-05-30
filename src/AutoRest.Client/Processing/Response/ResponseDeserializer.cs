using RestSharp;

namespace AutoRest.Client.Processing.Response
{
    public class ResponseDeserializer: IResponseDeserializer
    {
        private readonly IRestClient _client;

        public ResponseDeserializer(IRestClient client)
        {
            _client = client;
        }

        public IRestResponse<TBody> Deserialize<TBody>(IRestResponse response)
            => _client.Deserialize<TBody>(response);
    }
}