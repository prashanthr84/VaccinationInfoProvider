using System.Collections.Generic;
using VaccinationInfoProvider.UserManagement;

namespace VaccinationInfoProvider.VaccinationInfoFetcherService {
    internal interface INotificationSystem {
        void Notify(User user, IEnumerable<VaccinationCenter> vaccineCenters);
    }
}