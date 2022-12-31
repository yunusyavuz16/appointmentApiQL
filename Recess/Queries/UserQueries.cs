using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using HotChocolate.AspNetCore.Authorization;
using Newtonsoft.Json;
using Recess.Models;
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
        public async Task<UserRoles> GetUserRoles(string id, [Service] IHttpContextAccessor contextAccessor, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _firestoreProvider.Get<UserRoles>(id, cancellationToken);
                var userRole = new UserRoles
                {
                    DisplayName = store.DisplayName,
                    Id = id
                };
                return userRole;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }

}
