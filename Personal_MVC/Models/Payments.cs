namespace Personal_MVC.Models
{
	public class Payments
	{
		public int PaymentID { get; set; }
		public int ContractID { get; set; }
		public DateTime PaymentDate { get; set; }
		public decimal AmountPaid { get; set; }
		public string PaymentStatus { get; set; }
	}
}
