using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Services
{
    using System.Collections.Generic;

    public interface IExporter
    {
        void Export(IEnumerable<double> sortedValues);
    }
}
