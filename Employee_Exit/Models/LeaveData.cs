using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Exit
{
    public class LeaveData
    {
        public string leaveType { get; set; }
        public string[] weekdays { get; set; }
        public string[] weeks { get; set; }
        public string[] selectedDays { get; set; }
    }
}