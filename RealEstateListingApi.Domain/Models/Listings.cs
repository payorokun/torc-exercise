namespace RealEstateListingApi.Domain.Models
{
    public class Listing:BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0,5);  // Default to empty string if nulls aren't allowed
        //[Required(AllowEmptyStrings = false)]
        //[StringLength(50)]
        public string Title { get; set; } = string.Empty;
        //[Required]
        public decimal Price { get; set; }  // Decimal is a value type and non-nullable by default
        //[StringLength(500)]
        public string? Description { get; set; }  // Mark as nullable if appropriate
    }
    public class BaseEntity{}
}