using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Recess.Helpers;
using Recess.MutationModels;

namespace Recess.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutations
    {
        [Authorize]
        public async Task<UserRecordArgs> UpdateUser(UpdateUserMutationModel model, [Service] IHttpContextAccessor contextAccessor)
        {
            try
            {
                HttpContextHelper.IsAuthenticated(contextAccessor);
                var user = new UserRecordArgs();
                //if(model.CurrentEmail!= null)
                //{
                //    user
                //}
                //UserRecord userRecord =  FirebaseAuth.DefaultInstance.UpdateUserAsync();
                return user;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}
