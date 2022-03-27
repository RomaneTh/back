using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.Auth;
using Boilerplate.Application.DTOs.User;
using UserEntity = Boilerplate.Domain.Entities.User;
using Newtonsoft.Json.Linq;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISession = Boilerplate.Domain.Auth.Interfaces.ISession;

namespace Boilerplate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ISession _session;

        public UserController(IUserService userService, IAuthService authService, ISession session)
        {
            _userService = userService;
            _authService = authService;
            _session = session;
        }

        /// <summary>
        /// Authenticates the user and returns the token information.
        /// </summary>
        /// <param name="loginInfo">Email and password information</param>
        /// <returns>Token information</returns>
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JwtDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginInfo)
        {
            var user = await _userService.Authenticate(loginInfo.Email, loginInfo.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            // var json = File.ReadAllText("datastore.json");
            // File.AppendAllText("log.txt", json);
            // var data = JObject.Parse(File.ReadAllText("datastore.json"));
            // StreamReader r = new StreamReader("Controllers/datastore.json");
            // string jsonString = r.ReadToEnd();
            // User u = JsonConvert.DeserializeObject<User>(jsonString);
            //----------
            // List<UserEntity> source = new List<UserEntity>();

            // using (StreamReader r = new StreamReader("Controllers/datastore.json"))
            // {
            //     string json = r.ReadToEnd();
            //     Console.WriteLine(json);
            //     // JsonSerializer.Deserialize<Person>(jsonPerson);
            //     // List<UserEntity> usersEntities = JsonConvert.DeserializeObject<List<UserEntity>>(response);

            //     List<UserEntity> usersEntities = JsonSerializer.Deserialize<List<UserEntity>>(json);
            //     Console.WriteLine(usersEntities);
            // }
            //----------
            string json = "{\"Email\":\"Share Knowledge\",\"Password\":\"C-sharpcorner\",\"Role\":\"User\"}";

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                // Deserialization from JSON
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(UserEntity));
                UserEntity bsObj2 = (UserEntity)deserializer.ReadObject(ms);
                Console.WriteLine(bsObj2.Email);
                Console.WriteLine(bsObj2.Password);
                // Response.Write("Name: " + bsObj2.Name); // Name: C-sharpcorner
                // Response.Write("Description: " + bsObj2.Description); // Description: Share Knowledge
            }


            // string json = "{\"Email\":\"Share Knowledge\",\"Password\":\"C-sharpcorner\",\"Role\":\"User\"}";

            // using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            // {
            //     // Deserialization from JSON
            //     DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(UserEntity));
            //     UserEntity bsObj2 = (UserEntity)deserializer.ReadObject(ms);
            //     Console.WriteLine(bsObj2.Email);
            //     Console.WriteLine(bsObj2.Password);
            //     // Response.Write("Name: " + bsObj2.Name); // Name: C-sharpcorner
            //     // Response.Write("Description: " + bsObj2.Description); // Description: Share Knowledge
            // }



            // using (StreamReader r = new StreamReader("Controllers/datastore.json"))
            // {
            //     string jsonFile = r.ReadToEnd();

            //     using (var ms = new MemoryStream(jsonFile))
            //     {
            //         // Deserialization from JSON
            //         DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<UserEntity>));
            //         List<UserEntity> truc = List<UserEntity>deserializer.ReadObject(ms);
            //         Console.WriteLine(truc);
            //     }

            //     // Console.WriteLine(json);
            //     // JsonSerializer.Deserialize<Person>(jsonPerson);
            //     // List<UserEntity> usersEntities = JsonConvert.DeserializeObject<List<UserEntity>>(response);
                
            // }

            // Console.WriteLine("123");

            // JObject o1 = JObject.Parse(File.ReadAllText(@"c:\videogames.json"));


            // dump(string json = File.ReadAllText("settings.json"));

            // var moviesJson = new [{
            //     "Email": "romane.thu@gmail.com",
            //     "Password": "Password123!",
            //     "Role": "User"
            // },{
            //     "Email": "romane.thu2@gmail.com",
            //     "Password": "2Password123!",
            //     "Role": "User"
            // }];

            // var list = JsonSerializer.Deserialize<List<UserEntity>>(moviesJson);
            // Console.WriteLine(list);

            var text = System.IO.File.ReadAllText("Controllers/datastore.json");
            Console.WriteLine(text);

            var jObject = JObject.Parse(text);
            JArray jArray= (JArray)jObject["users"];
            var uList = new List<UserEntity>();

            foreach (JObject item in jArray) // <-- Note that here we used JObject instead of usual JProperty
            {
                var data = new UserEntity
                {
                    Email = item.GetValue("Email").ToString(),
                    Password = item.GetValue("Password").ToString(),
                    Role = item.GetValue("Role").ToString()
                };
                uList.Add(data);

                // string name = item.GetValue("Email").ToString();
                // Console.WriteLine(name);
                // ...
            }

            Console.WriteLine(uList);
            Console.WriteLine("-----");

            Console.WriteLine(string.Join("\t", uList));


            Console.WriteLine("-----2");
            var userFound = uList.Find(i => i.Email == "romane.thu@gmail.com" && i.Password == "Password123!");
           
            Console.WriteLine(userFound); // null if not corresponding

            // string[] valuesArray = jArray.ToObject<string[]>();

                // Console.WriteLine(jArray);
                // Console.WriteLine(values);

            // var list = new List<UserEntity>();

            // foreach (var prop in jsonParse.Properties())
            // {
            //     Console.WriteLine(prop);
            //     Console.WriteLine(prop[0]);
            // }

            return Ok(_authService.GenerateToken(user));
        }

        [HttpPatch("updatePassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordDto dto)
        {            
            await _userService.UpdatePassword(_session.UserId, dto);
            return NoContent();
        }
    }
}
