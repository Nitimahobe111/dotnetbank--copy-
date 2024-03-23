
namespace DotNetBank
{

    public class EMICalculatorResponse
    {

        public string? Message { get; set; }
        public double EMI { get; set; }
        public double TotalAmountToBePaid { get; set; }
        public double MonthlyInterestRate { get; set; }
        public double Amount { get; set; }
        public double DurationInMonth { get; set; }

    }

}