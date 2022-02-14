using System.Text.RegularExpressions;

namespace IOETChallenge.Business
{
    public class EmployeePaymentDataWrapper : IEmployeePaymentDataWrapper
    {
        public (string Employee, bool AllPaymentDataProcessed, Dictionary<string, List<TimeRange>>? WorkedTime) GetEmployeePaymentData(string employeePaymentData)
        {
            //TODO: get valid the format from config file for whole regex, day match, hour format, time separator
            var match = Regex.Match(employeePaymentData,
                "([a-zA-Z])+=((MO|TU|WE|TH|FR|SA|SU)([0-9]{2}:[0-9]{2})-[0-9]{2}:[0-9]{2},?)+");

            // It stops processing if the employeePaymentData does not match the format 
            if (!match.Success) return ("", false, null);

            var workedTime = new Dictionary<string, List<TimeRange>>();

            try
            {
                //Separate employee name and day information
                var employee = match.Value.Split("=")[0];
                var dayAndHours = match.Value.Split("=")[1].Split(",");

                //Process all day and hours for the provided employee
                foreach (var dayAndHour in dayAndHours)
                {
                    //Separate the day and the time
                    var dayMatch = Regex.Split(dayAndHour, "(MO|TU|WE|TH|FR|SA|SU)");
                    var day = dayMatch[1];
                    var hourRange = dayMatch[2];
                    //Separate hour range
                    var hourFrom = hourRange.Split('-')[0];
                    var hourTo = hourRange.Split('-')[1];

                    //Add the hours to the specific days
                    if (!workedTime.ContainsKey(day))
                    {
                        workedTime[day] = new List<TimeRange>();
                    }
                    workedTime[day].Add(new TimeRange()
                    {
                        HourFrom = new TimeSpan(int.Parse(hourFrom.Split(':')[0]), int.Parse(hourFrom.Split(':')[1]), 0),
                        HourTo = new TimeSpan(int.Parse(hourTo.Split(':')[0]), int.Parse(hourTo.Split(':')[1]), 0),
                    });

                }
             
                return (employee, employeePaymentData == match.Value, workedTime);
            }
            catch (Exception ex)
            {
                //TODO: Add some log information
                return ("", false, null);
            }
        }
    }
}
