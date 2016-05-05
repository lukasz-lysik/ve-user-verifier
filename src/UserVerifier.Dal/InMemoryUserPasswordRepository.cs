using System.Collections.Generic;
using UserVerifier.Entities;
using UserVerifier.Repositories;
using UserVerifier.ValueObjects;

namespace UserVerifier.Dal
{
    public class InMemoryUserPasswordRepository : IUserPasswordRepository
    {
        private static readonly IDictionary<UserId, UserPassword> List = new Dictionary<UserId, UserPassword>();

        public UserPassword Get(UserId id)
        {
            return List.ContainsKey(id) 
                ? List[id] 
                : null;
        }

        public void Save(UserPassword userPassword)
        {
            List[userPassword.UserId] = userPassword;
        }
    }
}
