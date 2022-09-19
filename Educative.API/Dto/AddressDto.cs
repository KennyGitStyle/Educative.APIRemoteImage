namespace Educative.API.Dto;

public class AddressDto
{


    public string AddressId { get; set; } = string.Empty;

    public string Addr1 { get; set; } = string.Empty;

    public string Add2 { get; set; } = string.Empty;

    public string City { get; set; }


    public string County { get; set; } = string.Empty!;


    public string Postcode { get; set; } = string.Empty!;

    public string StudentAddressId { get; set; }
    public virtual StudentDto StudentDto { get; set; }

}