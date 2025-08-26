using System.ComponentModel.DataAnnotations;

namespace WebApiApp.Model
{
    public class Calculation
    {
        [Key]
        public int Id { get; set; }
        public double Num1 { get; set; }
        public double Num2 { get; set; }

        public double Result { get; set; }
        public string Operator { get; set; } = "+";

    }
}