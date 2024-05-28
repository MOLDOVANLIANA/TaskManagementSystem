using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Entities
{
    internal interface ISaveable
    {
        string ConvertToStringForSaving();
    }
}

