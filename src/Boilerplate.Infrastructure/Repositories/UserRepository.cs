using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Boilerplate.Infrastructure.Context;
using UserEntity = Boilerplate.Domain.Entities.User;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Boilerplate.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        public UserRepository()
        {
        }

        public List<UserEntity> GetAll()
        {
            var text = System.IO.File.ReadAllText("../Boilerplate.Infrastructure/Repositories/datastore2.json");
            // Console.WriteLine(text);

            var jObject = JObject.Parse(text);
            JArray jArray= (JArray)jObject["users"];
            var userList = new List<UserEntity>();

            foreach (JObject item in jArray) // <-- Note that here we used JObject instead of usual JProperty
            {
                var data = new UserEntity
                {
                    Email = item.GetValue("Email").ToString(),
                    Password = item.GetValue("Password").ToString(),
                    Role = item.GetValue("Role").ToString()
                };
                userList.Add(data);
            }

            return userList;
        }
    }
}
