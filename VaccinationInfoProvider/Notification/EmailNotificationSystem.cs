using System.Collections.Generic;
using VaccinationInfoProvider.UserManagement;

namespace VaccinationInfoProvider.VaccinationInfoFetcherService {
    internal class EmailNotificationSystem:INotificationSystem {

        /// <inheritdoc />
        public void Notify(User user, IEnumerable<VaccinationCenter> vaccineCenters) {
            throw new System.NotImplementedException();

            //todo:// send an email.
        }
    }
}