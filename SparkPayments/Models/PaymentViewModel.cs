using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SparkPayments.Models
{
    public class CreateEditPaymentViewModel
    {
        public SelectList Categories { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public string Description { get; set; }
        public double Amount { get; set; }
        public int PaymentId { get; set; }
    }
}