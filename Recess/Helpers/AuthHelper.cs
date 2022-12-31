namespace Recess.Helpers
{
    public class AuthHelper
    {
        public class PayloadLogin
        {

            public string idToken { get; set; }
            public string email { get; set; }
            public string refreshToken { get; set; }
            public string expiresIn { get; set; }
            public string localId { get; set; }
            public Boolean registered { get; set; }
        }
    }
}
