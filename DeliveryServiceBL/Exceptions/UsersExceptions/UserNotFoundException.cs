namespace DeliveryServiceBL.Exceptions.UsersExceptions;

public class UserNotFoundException(string message) : ApplicationException(message);