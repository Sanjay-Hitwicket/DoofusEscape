using System;
using System.Text;

namespace Systems.TimeSystem {
    public static class TimeHelper {
        
        // Define the Unix epoch start date (January 1, 1970, 00:00:00 UTC)
        private static readonly DateTime Epoch = new (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        /// <summary>
        /// Returns the current system time.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTime() {
            return DateTime.Now;
        }
        
        /// <summary>
        /// For converting seconds since epoch to DateTime
        /// </summary>
        /// <param name="secondsSinceEpoch"></param>
        /// <returns></returns>
        public static DateTime ConvertSecondsToDateTime(long secondsSinceEpoch) {
            // Add the number of seconds to the epoch date
            return Epoch.AddSeconds(secondsSinceEpoch);
        }
        
        /// <summary>
        /// Converts total seconds to a formatted string "hh:mm:ss".
        /// </summary>
        /// <param name="totalSeconds"></param>
        /// <returns></returns>
        public static string ConvertToHHMMSS(float totalSeconds) {
            var timeSpan = TimeSpan.FromSeconds(totalSeconds);
            return timeSpan.ToString(@"hh\:mm\:ss");
        }

        public static TimeSpan ConvertSecondsToTimeSpan(float secondsLeft) {
            return TimeSpan.FromSeconds(secondsLeft);
        }
        
    }
}