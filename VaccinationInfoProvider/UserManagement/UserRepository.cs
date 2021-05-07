using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VaccinationInfoProvider.UserManagement {
    internal class UserRepository : IUserRepository {
        private JArray users = new JArray();

        public UserRepository() {
            LoadData();
        }

        private void LoadData() {
            // read JSON directly from a file
            if (!File.Exists("users.json")) {
                return;
            }
            using (StreamReader file = File.OpenText(@"users.json"))
            using (JsonTextReader reader = new JsonTextReader(file)) {
                users = (JArray)JToken.ReadFrom(reader);
            }
        }

        /// <inheritdoc />
        public void AddUser(User user) {
            users.Add(JToken.FromObject(user));
            File.WriteAllText("users.json", users.ToString());
        }
    }
}