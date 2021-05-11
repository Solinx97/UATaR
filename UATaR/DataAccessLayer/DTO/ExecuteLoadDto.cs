using System;

namespace DataAccessLayer.DTO
{
    public class ExecuteLoadDto
    {
        public int Id { get; set; }

        public int LoadId { get; set; }

        public DateTimeOffset Date { get; set; }

        public bool IsFullExecuted { get; set; }
    }
}