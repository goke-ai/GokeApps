namespace Microsoft.AspNetCore.Mvc
{
    public interface IWebHostEnvironment
    {
        string WebRootPath { get; set; }
        string ContentRootPath { get; set; }
    }
}