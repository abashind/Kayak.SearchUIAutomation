using JuribaKayak.SearchUIAutomation.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JuribaKayak.SearchUIAutomation.Helpers
{
    /// <summary>
    /// Methods and classes for functionality that I don't know where to put.
    /// </summary>
    internal static class MiscUtils
    {

        /// <summary>
        /// Invert bool value.
        /// </summary>
        /// <param name="val">Value.</param>
        /// <returns>Inverted bool value.</returns>
        public static bool Invert(this bool val) { return !val; }

        public static bool IsLocked(this FileInfo file)
        {
            try
            {
                using FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Build an http get request, that will be put into the browser url field.
        /// I deliberately don't make the builder for evry case as unfortunately I have no time for that.
        /// Only cases enumerated in Search.feature are supported.
        /// </summary>
        /// <param name="fps">FlightParams</param>
        /// <param name="travelers">AmountOfTravelers</param>
        /// <returns></returns>
        public static string BuildRequest(FlightParams fps, IEnumerable<AmountOfTravelers> travelers)
        {
            var result = Constants.FlightApiUri;

            result += $"{fps.From}-{fps.To}/{fps.WhenToThere}/";

            if (fps.WhenBack != "--")
                result += $"{fps.WhenBack}/";

            foreach(var i in travelers)
                result += $"{i.Amount}{i.Type}";

            return result += "?sort=bestflight_a";
        }
    }
}
