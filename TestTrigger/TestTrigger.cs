using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using BG.Services;

namespace BG.TestTrigger
{
    public class TestTrigger
    {
        private readonly ILogger<TestTrigger> _logger;
        private readonly HtmlToDocxConverter _converter;

        public TestTrigger(ILogger<TestTrigger> logger)
        {
            _logger = logger;
            _converter = new HtmlToDocxConverter(); // Initialize the service
        }

        [Function("TestTrigger")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                // Check if the request contains a file
                if (!req.HasFormContentType || req.Form.Files.Count == 0)
                {
                    return new BadRequestObjectResult("Please upload an HTML file.");
                }

                // Get the uploaded file
                var file = req.Form.Files[0];

                // Ensure the file has an HTML extension
                if (Path.GetExtension(file.FileName).ToLower() != ".html")
                {
                    return new BadRequestObjectResult("Only HTML files are supported.");
                }

                // Read the content of the HTML file
                string htmlContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    htmlContent = await reader.ReadToEndAsync();
                }

                _logger.LogInformation($"Received HTML file: {file.FileName}");

                // Use the HtmlToDocxConverter service to convert the HTML to DOCX
                var docxBytes = _converter.ConvertHtmlToDocx(htmlContent);

                // Return the DOCX file as a response
                return new FileContentResult(docxBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    FileDownloadName = $"{Path.GetFileNameWithoutExtension(file.FileName)}.docx"
                };
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Return an error response
                return new ObjectResult("An error occurred while processing your request. Please try again later.")
                {
                    StatusCode = 500
                };
            }
        }
    }
}