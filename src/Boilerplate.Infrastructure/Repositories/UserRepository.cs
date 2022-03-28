using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Boilerplate.Infrastructure.Context;
using UserEntity = Boilerplate.Domain.Entities.User;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Boilerplate.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        public UserRepository()
        {
        }

        public List<UserEntity> GetAll()
        {
            var jsonDb = System.IO.File.ReadAllText("../Boilerplate.Infrastructure/Repositories/DB.json");

            var jObject = JArray.Parse(jsonDb);
            var userList = new List<UserEntity>();

            foreach (JObject item in jObject)
            {
                var data = new UserEntity
                {
                    Id = new System.Guid(item.GetValue("Id").ToString()),
                    Email = item.GetValue("Email").ToString(),
                    Password = item.GetValue("Password").ToString(),
                    Role = item.GetValue("Role").ToString()
                };
                userList.Add(data);
            }

            return userList;
        }

        public virtual UserEntity Update(UserEntity entity)
        {
            var allUsers = GetAll();

            int index = allUsers.FindIndex(i => i.Id == entity.Id);

            if (index >= 0)
            {
                allUsers[index] = entity;
            } 

            string jsonDb = JsonSerializer.Serialize(allUsers, new JsonSerializerOptions() { WriteIndented = true });
            System.IO.File.WriteAllText("../Boilerplate.Infrastructure/Repositories/DB.json", jsonDb);

            return entity;
        }
    }
}
