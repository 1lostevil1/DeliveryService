using AutoMapper;
using DeliveryServiceBL.Exceptions.UsersExceptions;
using DeliveryServiceDataAccess.Entities;
using DeliveryServiceDL.Entity;

namespace DeliveryServiceBL.User.Provider;

public class UsersProvider : IUsersProvider

{

    private readonly IRepository<DeliveryServiceDataAccess.Entities.User> _userRepository;

    private readonly IMapper _mapper;




    public UsersProvider(IRepository<DeliveryServiceDataAccess.Entities.User> userRepository, IMapper mapper)

    {

        _userRepository = userRepository;

        _mapper = mapper;

    }




    public IEnumerable<UserModel> GetUsers(FilterUserModel filter = null)

    {
        

        string? namePart = filter?.NamePart;

        string? phoneNumberPart = filter?.PhoneNumberPart;

        string? emailPart = filter?.EmailPart;

        DateTime? creationTime = filter?.CreationTime;

        DateTime? modificationTime = filter?.ModificationTime;
        




        var users = _userRepository.GetAll(u =>
            

            (namePart == null || u.Name.Contains(namePart)) &&

            (phoneNumberPart == null || u.Phone.Contains(phoneNumberPart)) &&

            (emailPart == null || u.EMail.Contains(emailPart)) &&

            (creationTime == null || u.CreationTime == creationTime) &&

            (modificationTime == null || u.ModificationTime == modificationTime) 
            
        );

        return _mapper.Map<IEnumerable<UserModel>>(users);

    }




    public UserModel GerUserInfo(int id)

    {

        var entity = _userRepository.GetById(id);

        if (entity == null)

        {

            throw new UserNotFoundException("User not found");

        }




        return _mapper.Map<UserModel>(entity);

    }

}