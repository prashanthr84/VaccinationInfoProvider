namespace VaccinationInfoProvider.UserManagement {
    public class User {

        public User(string name, string email, string state, string district, string pinCode) {
            Name = name;
            Email = email;
            State = state;
            District = district;
            PinCode = pinCode;
        }

        public string Name { get; }

        public string Email { get; }

        public string State { get; }

        public string District { get; }

        public string PinCode { get; }
    }
}