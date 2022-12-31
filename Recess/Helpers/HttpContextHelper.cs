namespace Recess.Helpers
{
    public class HttpContextHelper
    {
        public static void IsAuthenticated(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor.HttpContext.Items == null || contextAccessor.HttpContext.Items["User"] == null)
            {
                throw new GraphQLException("The current user is not authorized to access this resource.");
            }
        }
    }
}
