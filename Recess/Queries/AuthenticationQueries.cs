using Newtonsoft.Json;
using static Google.Rpc.Context.AttributeContext.Types;
using static Recess.Helpers.AuthHelper;

namespace Recess.Queries
{
    [ExtendObjectType("Query")]
    public class AuthenticationQueries
    {
        private readonly IHttpClientFactory _clientFactory;

        public AuthenticationQueries(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<PayloadLogin> GetLogin(string email, string password, [Service] IHttpContextAccessor contextAccessor) 
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var mdata = new { email = email, password = password, returnSecureToken =true };
                var config  = File.ReadAllText("D:\\Recess\\Recess\\Recess\\firebase-config.json");
                var parsed = JsonConvert.DeserializeObject<fireBaseConfig>(config);
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(mdata), System.Text.Encoding.UTF8, "application/json");
                var path = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + parsed.apiKey.ToString().ToString();
                var responseApi = await client.PostAsync(path, content);
                var loginInfo = new PayloadLogin();
                    if (responseApi.IsSuccessStatusCode)
                    {
                       var resp = await responseApi.Content.ReadAsStringAsync();
                       var des = JsonConvert.DeserializeObject<PayloadLogin>(resp);
                       loginInfo = des;
                    }
                    else
                    {
                        var newEx = new GraphQLException("RestAPI Error");
                        throw newEx;
                    }
                    return loginInfo;
            }

            catch (Exception ex) {
                throw ex;
            }
        }

        public class fireBaseConfig
        {
            public string apiKey { get; set; }
        }
    }
}
