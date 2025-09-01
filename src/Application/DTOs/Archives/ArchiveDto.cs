namespace Application.DTOs
{
    public partial class ArchiveDto
    {
        public int ArchiveID { get; set; }
        public int ApplicantID { get; set; }
        public int DecisionID { get; set; }
        public string FileNumber { get; set; } = null!;
        public string ApplicantFileNumber { get; set; } = null!;
        public DateTime? ArchiveDate { get; set; }
        public string? DigitalCopy { get; set; }
        public virtual ApplicantDto Applicant { get; set; } = null!;
    }
}
