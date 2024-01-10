namespace Raythos.DTOs.AddressDtos
{
    public class UpdateAddressDto
    {
        public long AddressID { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public long CountryId { get; set; } = -1;
        public long UserId { get; set; } = -1;
    }
}
