namespace Recess.MutationModels
{
    public class UpdateUserMutationModel
    {
      public string CurrentEmail { get; set; }
      public string NewEmail { get; set; }
      
      public string NewPassword { get; set; }
      public string NewPhoneNumber { get; set; }
    }
}
