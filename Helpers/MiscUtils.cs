using JuribaKayak.SearchUIAutomation.Models;
using System.Collections.Generic;
using System.IO;

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

        public static string BuildRequest(FlightParams fps, IEnumerable<AmountOfTravelers> travelers)
        {
            return "Bad request.";
        }
    }
}
