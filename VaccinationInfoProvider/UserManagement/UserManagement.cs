using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace VaccinationInfoProvider.UserManagement {

    /// <summary>
    /// Responsibility => Manages the user CRUD.
    /// </summary>
    internal class UserManagement : IUserManagement {
        private readonly IUserRepository repository;

        public UserManagement(IUserRepository repository) {
            this.repository = repository;
        }

        /// <inheritdoc />
        public User Register(User user) {
            user.Id = Guid.NewGuid().ToString();
            repository.AddUser(user);
            //File.AppendAllText("users1.txt", JsonConvert.SerializeObject(user));
            return user;
        }

        /// <inheritdoc />
        public IList<User> GetAllUsers() {
            //todo:// just create users from JArray in repository and return.
            throw new NotImplementedException();
        }
    }
}