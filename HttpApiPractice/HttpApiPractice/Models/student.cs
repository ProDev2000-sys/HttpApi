using System;
using System.Collections.Generic;

#nullable disable

namespace HttpApiPractice.Models
{
    public partial class student
    {
        public int stu_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email_id { get; set; }
        public decimal? m_no { get; set; }
        public decimal? age { get; set; }
    }
}
