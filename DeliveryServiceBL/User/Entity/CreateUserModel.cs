﻿namespace DeliveryServiceDL.Entity;

public class CreateUserModel
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }

    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
}