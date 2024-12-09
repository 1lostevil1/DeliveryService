namespace DeliveryServiceBL.Exceptions.UsersExceptions;

public class UserAlreadyExistsException(string message) : ApplicationException(message);