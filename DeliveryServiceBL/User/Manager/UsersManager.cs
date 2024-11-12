using AutoMapper;
using DeliveryServiceDataAccess.Entities;
using DeliveryServiceDL.Entity;
using DeliveryServiceDL.User.Exceptions;

namespace DeliveryServiceDL.User.Manager;

public class UsersManager : IUsersManager
{
    private readonly IRepository<DeliveryServiceDataAccess.Entities.User> _usersRepository;
    private readonly IMapper _mapper;

    public UsersManager(IRepository<DeliveryServiceDataAccess.Entities.User> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public UserModel CreateUser(CreateUserModel createModel)
    {
        //validation

        var entity = _mapper.Map<DeliveryServiceDataAccess.Entities.User>(createModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }

    public void DeleteUser(int id)
    {
        try
        {
            var entity = _usersRepository.GetById(id);
            _usersRepository.Delete(entity);
        }
        catch (Exception e)
        {
            throw new UserNotFoundException(e.Message);
        }
    }

    public UserModel UpdateUser(UpdateUserModel updateModel)
    {
        //validation

        var entity = _mapper.Map<DeliveryServiceDataAccess.Entities.User>(updateModel);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}