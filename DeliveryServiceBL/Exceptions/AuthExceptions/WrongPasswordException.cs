namespace DeliveryServiceBL.Exceptions.AuthExceptions;

public class WrongPasswordException(string message) : ApplicationException(message);