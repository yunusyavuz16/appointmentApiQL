using FirebaseAdmin.Auth;
using HotChocolate.AspNetCore.Authorization;
using System;

namespace Recess.Queries
{
    [ExtendObjectTypeAttribute("Query")]
    
    public class UserQueries
    {
        public async Task<UserRecordArgs> GetUser( [Service] IHttpContextAccessor contextAccessor) {
            try
            {
                
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync("yunusyavuz016@gmail.com");
                Console.WriteLine("userRec");
                Console.WriteLine(userRecord.EmailVerified);
                var user = new UserRecordArgs
                {
                    Email= userRecord.Email,
                    EmailVerified= userRecord.EmailVerified,
                    DisplayName= userRecord.DisplayName,
                    Disabled= userRecord.Disabled,
                    PhoneNumber= userRecord.PhoneNumber,
                    PhotoUrl= userRecord.PhotoUrl,
                    Uid= userRecord.Uid,
                };
                return user;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
