using System;

namespace UATaRApi.ViewModels
{
    public class ExecuteLoadViewModel
    {
        public int Id { get; set; }

        public int LoadId { get; set; }

        public DateTimeOffset Date { get; set; }

        public bool IsFullExecuted { get; set; }
    }
}