namespace UnitTestDemo.Models
{
	public class CalculationRequest
	{
		public bool IsElk { get; set; }
		public bool IsDouble { get; set; }
		public int Category { get; set; }
		public DateTime TargetMonth { get; set; }
		public decimal OnPeak { get; set; }
		public decimal OffPeak { get; set; }
		public decimal ReturnOnPeak { get; set; }
		public decimal ReturnOffPeak { get; set; }
		public decimal? ReturnTariff { get; set; }
		public decimal PriceSingle { get; set; }
		public decimal PriceOnPeak { get; set; }
		public decimal PriceOffPeak { get; set; }
		public List<decimal> TaxPercent { get; set; } = new List<decimal>();
	}
}
