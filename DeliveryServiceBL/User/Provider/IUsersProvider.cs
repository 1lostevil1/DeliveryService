using DeliveryServiceDL.Entity;

namespace DeliveryServiceBL.User.Provider;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(FilterUserModel filter = null);
    UserModel GerUserInfo(int id);
}