namespace CMS_API.Models.RequestModels
{
    public sealed record UserRequestModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
