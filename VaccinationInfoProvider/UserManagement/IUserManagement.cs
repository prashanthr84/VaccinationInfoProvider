using System.Collections.Generic;

namespace VaccinationInfoProvider.UserManagement {
    public interface IUserManagement {
        User Register(User user);
        IList<User> GetAllUsers();
    }
}