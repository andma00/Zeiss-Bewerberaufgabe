using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
    class ConsoleExporter : IExporter
    {
        public void Export(IEnumerable<double> sortedValues)
        {
            Console.WriteLine("--- Exportierte Werte ---");
            foreach (var value in sortedValues)
            {
                Console.WriteLine(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
        }
    }
}
