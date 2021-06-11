﻿namespace BusinessLayer.Entities
{
    public class Load
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int GroupId { get; set; }

        public int LoadTypeId { get; set; }

        public double Hours { get; set; }
    }
}