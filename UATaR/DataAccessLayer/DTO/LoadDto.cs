namespace DataAccessLayer.DTO
{
    public class LoadDto
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int LoadTypeId { get; set; }

        public string Description { get; set; }
    }
}
