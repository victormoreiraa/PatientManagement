﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public decimal Exam1 { get; set; }
        public string Exam2 { get; set; }
        public decimal Exam3 { get; set;}
        public decimal Exam4 { get; set;}
        public string Calculation { get; set;}
        
    }
}
