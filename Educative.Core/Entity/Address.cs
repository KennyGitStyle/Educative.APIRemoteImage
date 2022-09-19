using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educative.Core
{
    public class Address
    {
        

        public string AddressId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address Line 1")]
        public string Addr1 { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address Line 2")]
        public string Add2 { get; set; } = string.Empty;

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "County")]
        public string County { get; set; } = string.Empty!;

        [Required]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; } = string.Empty!;
        [ForeignKey("Student")]
        public string StudentAddressId { get; set; }
        public virtual Student Student { get; set; }

    }
}
