using System;

namespace BusinessLayer.Entities
{
    public class ExecuteLoad
    {
        public int Id { get; set; }

        public int LoadId { get; set; }

        public DateTimeOffset Date { get; set; }

        public bool IsFullExecuted { get; set; }
    }
}