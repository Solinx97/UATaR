﻿using System;

namespace UATaR.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string Education { get; set; }

        public DateTimeOffset Birthday { get; set; }
    }
}