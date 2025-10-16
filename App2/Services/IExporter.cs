using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
    using System.Collections.Generic;
    /// <summary>
    /// Defines a contract for exporting sorted double values.
    /// </summary>
    public interface IExporter
    {
        void Export(IEnumerable<double> sortedValues);
    }
}
