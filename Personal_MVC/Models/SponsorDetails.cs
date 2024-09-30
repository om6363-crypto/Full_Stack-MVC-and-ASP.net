namespace Personal_MVC.Models
{
	public class SponsorDetails
	{
        public int SponsorId { get; set; }
        public string SponsorName { get; set; }
        public decimal TotalPayments { get; set; }
        public int NumberOfPayments { get; set; }
        public DateTime LatestPaymentDate { get; set; }
    }
}
