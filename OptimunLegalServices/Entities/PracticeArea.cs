namespace OptimunLegalServices.Entities
{
    public class PracticeArea:BaseEntity
    {
        public string Name { get; set; }

        public List<ContactUs> ContactUs { get; set; }

        public PracticeArea()
        {
            ContactUs = new();
        }
    }
}
