using EnsekGlobal.Models;

namespace ENSEK_Technical_Test.Models
{
    public class MeterReadingsViewModel : BaseViewModel
    {
        public List<MeterReadingsDM> SuccessfulMeterReadings { get; set; }
        public Int64 SuccessfulMeterReadingCount { get { return SuccessfulMeterReadings?.Count ?? 0; } }

        public List<MeterReadingsDM> FailedMeterReadings { get; set; }
        public Int64 FailedMeterReadingCount { get { return FailedMeterReadings?.Count ?? 0; } }


    }
}
