namespace DeliveryServiceDL.Entity;

public class UserModel

{

    public int Id { get; set; }

    public Guid ExternalId { get; set; }




    public DateTime CreationTime { get; set; }

    public DateTime ModificationTime { get; set; }

    

    public string PasswordHash { get; set; }
    

    public string PhoneNumber { get; set; }

    public string Email { get; set; }




    public string Name { get; set; }
    public string SurName { get; set; }



}