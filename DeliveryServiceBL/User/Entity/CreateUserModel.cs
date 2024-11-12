﻿namespace DeliveryServiceDL.Entity;

public class CreateUserModel
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }

    public int PassportSeries { get; set; }
    public int PassportNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public DateTime BirthDate { get; set; }

    public int PermissionId { get; set; }
}