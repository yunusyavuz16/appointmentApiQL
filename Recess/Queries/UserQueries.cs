using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using HotChocolate.AspNetCore.Authorization;
using Newtonsoft.Json;
using Recess.Providers;
using System;

namespace Recess.Queries
{
    [ExtendObjectType("Query")]

    public class UserQueries
    {
        
        private readonly FirestoreProvider _firestoreProvider;

        public UserQueries(FirestoreProvider firestoreProvider)
        {
            _firestoreProvider = firestoreProvider;
        }

        public async Task<UserRecordArgs> GetUser(string email, [Service] IHttpContextAccessor contextAccessor, CancellationToken cancellationToken) {
            try
            {
                
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
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
                var cancelSource = new CancellationTokenSource();
                Console.WriteLine("fs");
                return user;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
    public interface userRole
    {
        public string Id { get; set; }
    };
}
