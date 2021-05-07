using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using VaccinationInfoProvider.UserManagement;


namespace VaccinationInfoProvider.VaccinationInfoFetcherService {

    /// <summary>
    /// Responsibility => Background service for talking to CoWin API's.
    /// </summary>
    public class VaccinationInfoFetcherService:CronJobService {

        private readonly IUserManagement userManagement;
        private readonly ICowinWrapper cowinWrapper;
        private readonly INotificationSystem notificationSystem;

        Dictionary<string,List<VaccinationCenter>> districtToVaccineCentersCache = 
            new Dictionary<string, List<VaccinationCenter>>();

        internal VaccinationInfoFetcherService(
            IUserManagement userManagement,
            ICowinWrapper cowinWrapper,
            INotificationSystem notificationSystem
        ) {
            this.userManagement = userManagement;
            this.cowinWrapper = cowinWrapper;
            this.notificationSystem = notificationSystem;
        }

        /// <inheritdoc />
        public override Task DoWork(CancellationToken cancellationToken) {
            // Call the CoWin wrapper.
            IList<User> users = userManagement.GetAllUsers();

            foreach (var user in users) {
                if (districtToVaccineCentersCache.ContainsKey(user.District)) {
                    notificationSystem.Notify(user, districtToVaccineCentersCache[user.District]);
                } else {
                    List<VaccinationCenter> centers = cowinWrapper.GetVaccinationCenters();
                    districtToVaccineCentersCache.Add(user.District, centers);
                    notificationSystem.Notify(user, centers);
                }
            }

            return Task.CompletedTask;
        }

        //todo:// implement dispose/cancellation.
    }
}
