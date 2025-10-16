using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Exports sorted double values to a text file in the user's home directory.
    /// </summary>
    public class FileExporter : IExporter
    {
        /// <summary>
        /// Exports a collection of sorted double values to a text file in the user's home directory.
        /// </summary>
        /// <param name="sortedValues">The collection of sorted double values to be exported.</param>
        public void Export(IEnumerable<double> sortedValues)
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(homeDirectory, "werte.txt");

            var lines = sortedValues.Select(v => v.ToString(System.Globalization.CultureInfo.InvariantCulture));
            File.WriteAllLines(filePath, lines);
        }
    }
}
