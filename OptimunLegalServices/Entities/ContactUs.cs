namespace OptimunLegalServices.Entities
{
    public class ContactUs : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public DateTime SendTime { get; set; }
        public int PracticeAreaId { get; set; }
        public PracticeArea PracticeArea { get; set; }
        public bool IsReply { get; set; }
    }
}
