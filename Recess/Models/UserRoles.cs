using Google.Cloud.Firestore;
using System.ComponentModel;
using static Recess.Providers.FirestoreProvider;

namespace Recess.Models
{
    [FirestoreData]
    public class UserRoles : IFirebaseEntity
    {
        [FirestoreProperty]
        public string DisplayName { get; set; }
        [FirestoreProperty]
        public string Id { get; set; }
        public UserRoles()
        {
        }

        public UserRoles(string displayName)
        {
            Id = Guid.NewGuid().ToString("N");
            DisplayName = displayName;
        }
    }
}
