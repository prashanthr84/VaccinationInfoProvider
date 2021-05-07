using System.Collections.Generic;

namespace VaccinationInfoProvider.VaccinationInfoFetcherService {

    internal interface ICowinWrapper {
        List<VaccinationCenter> GetVaccinationCenters();
    }
}