﻿namespace DeliveryServiceWeb.Controller.User;

public class UpdateUsersPermissionsRequest
{
    public int Id { get; set; }
    public List<string> Permissions { get; set; }
}