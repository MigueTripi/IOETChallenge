namespace IOETChallenge.Business
{
    public class PaymentCalculator : IPaymentCalculator
    {
        private DayRate[] _dayRates = new DayRate[]
        {
            new DayRate()
            {
                Day = "MO",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "MO",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "MO",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            },
            new DayRate()
            {
                Day = "TU",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "TU",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "TU",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            },
            new DayRate()
            {
                Day = "WE",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "WE",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "WE",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            },
            new DayRate()
            {
                Day = "TH",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "TH",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "TH",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            },
            new DayRate()
            {
                Day = "FR",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "FR",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "FR",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            },
            new DayRate()
            {
                Day = "SA",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "SA",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "SA",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            },
            new DayRate()
            {
                Day = "SU",
                HourFrom = new TimeSpan(0,1,0),
                HourTo = new TimeSpan(9,0,0),
                Rate = 25
            },
            new DayRate()
            {
                Day = "SU",
                HourFrom = new TimeSpan(9,1,0),
                HourTo = new TimeSpan(18,0,0),
                Rate = 15
            },
            new DayRate()
            {
                Day = "SU",
                HourFrom = new TimeSpan(18,1,0),
                HourTo = new TimeSpan(23,59,59),
                Rate = 20
            }
        };

        /// <summary>
        /// Calculate the total to pay for all provided days 
        /// </summary>
        /// <param name="workedTime"></param>
        /// <returns></returns>
        public float CalculateTotalToPay(Dictionary<string, List<TimeRange>> workedTime)
        {
            float result = 0;
            foreach( var (day, hours) in workedTime)
            {
                var rates = this._dayRates.Where(x => x.Day == day);
                foreach( var timeRange in hours)
                {
                    var hoursFirstInterval = rates.First(x => timeRange.HourFrom.Hours >= x.HourFrom.Hours && timeRange.HourFrom.Hours < x.HourTo.Hours);
                    var hoursSecondInterval = rates.FirstOrDefault(x => 
                        timeRange.HourTo.Hours >= x.HourFrom.Hours && 
                        timeRange.HourTo.Hours < x.HourTo.Hours && 
                        hoursFirstInterval.HourFrom.Hours != x.HourFrom.Hours //This condition is to avoid return the same time range
                    );

                    result += GetAmountToPay(timeRange, hoursFirstInterval, hoursSecondInterval);
                }
            }

            return result;
        }

        /// <summary>
        /// return the amount to pay for the provided intervals. 
        /// Minutes lower than 60 are not considered to be paid.
        /// </summary>
        /// <param name="timeRange"></param>
        /// <param name="hoursFirstInterval"></param>
        /// <param name="hoursSecondInterval"></param>
        /// <returns></returns>
        private float GetAmountToPay(TimeRange timeRange, DayRate hoursFirstInterval, DayRate? hoursSecondInterval)
        {
            var higherHourFirstInterval = timeRange.HourTo < hoursFirstInterval.HourTo ? timeRange.HourTo : hoursFirstInterval.HourTo;
            var result = (higherHourFirstInterval.Hours - timeRange.HourFrom.Hours) * hoursFirstInterval.Rate;

            if (hoursSecondInterval != null)
            {
                var lowerHourSecondInterval = timeRange.HourFrom < hoursSecondInterval.HourFrom ? hoursSecondInterval.HourFrom : timeRange.HourFrom;
                result += (timeRange.HourTo.Hours - lowerHourSecondInterval.Hours) * hoursSecondInterval.Rate;
            }

            return result;
        }

    }
}
