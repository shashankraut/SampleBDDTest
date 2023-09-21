using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Reports
{
    public enum ExecutionStatus
    {
        OK = 0,
        StepDefinitionPending = 1,
        UndefinedStep = 2,
        BindingError = 3,
        TestError = 4,
        Skipped = 5
    }
}
