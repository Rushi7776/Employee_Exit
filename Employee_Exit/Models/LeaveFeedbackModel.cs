using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Exit
{
    public class LeaveFeedbackModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReasonForLeaving { get; set; }
        public string ReasonForLeavingAnyOther { get; set; }
        public string CompanyLike { get; set; }
        public string CompanyDislike { get; set; }
        public string SupervisorPoints { get; set; }
        public string YourRating { get; set; }
        public int RejoinAgain { get; set; }
        public string SuggestChanges { get; set; }
        public string NJ_CompanyName { get; set; }
        public string NJ_Designation { get; set; }
        public string NJ_Function { get; set; }
        public decimal NJ_CostToCompany { get; set; }
    }

}