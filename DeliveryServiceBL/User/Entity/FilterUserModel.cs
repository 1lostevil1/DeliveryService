﻿namespace DeliveryServiceDL.Entity;

public class FilterUserModel

{

    public string? Login { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    public DateTime? CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    
    public List<string>? Permissions { get; set; }

    

}