using System;

namespace BusinessLayer.Entities
{
    public class Teacher
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string Education { get; set; }

        public DateTimeOffset Birthday { get; set; }
    }
}