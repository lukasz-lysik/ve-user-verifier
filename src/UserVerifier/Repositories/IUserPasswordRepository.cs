using UserVerifier.Entities;
using UserVerifier.ValueObjects;

namespace UserVerifier.Repositories
{
    public interface IUserPasswordRepository
    {
        UserPassword Get(UserId id);
        void Save(UserPassword userPassword);
    }
}
