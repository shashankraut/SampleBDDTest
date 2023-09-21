using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Reports
{
    public enum LogStatus
    {
        Pass = 0,
        Fail = 1,
        Fatal = 2,
        Error = 3,
        Warning = 4,
        Info = 5,
        Skip = 6,
        Debug = 7
    }
}
