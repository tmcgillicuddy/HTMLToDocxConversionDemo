using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BG.TestSVGGenerator
{
    public class MakeSVGFile
    {
        private readonly ILogger<MakeSVGFile> _logger;

        public MakeSVGFile(ILogger<MakeSVGFile> logger)
        {
            _logger = logger;
        }

        [Function("MakeSVGFile")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Generate SVG content for a 3D-like solar system organizational chart
            string svgContent = @"
<svg xmlns='http://www.w3.org/2000/svg' width='800' height='600' style='font-family: Arial, sans-serif; background: radial-gradient(circle, #000428, #004e92);'>
    <!-- Background -->
    <defs>
        <radialGradient id='sunGradient' cx='50%' cy='50%' r='50%'>
            <stop offset='0%' style='stop-color: #FFD700; stop-opacity: 1;' />
            <stop offset='100%' style='stop-color: #FF8C00; stop-opacity: 1;' />
        </radialGradient>
        <radialGradient id='planetGradient' cx='50%' cy='50%' r='50%'>
            <stop offset='0%' style='stop-color: #4CAF50; stop-opacity: 1;' />
            <stop offset='100%' style='stop-color: #2E7D32; stop-opacity: 1;' />
        </radialGradient>
        <radialGradient id='orbitGradient' cx='50%' cy='50%' r='50%'>
            <stop offset='0%' style='stop-color: #2196F3; stop-opacity: 1;' />
            <stop offset='100%' style='stop-color: #1565C0; stop-opacity: 1;' />
        </radialGradient>
    </defs>

    <!-- Sun (CEO) -->
    <circle cx='400' cy='300' r='50' fill='url(#sunGradient)' />
    <text x='400' y='305' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='14'>CEO</text>

    <!-- Orbits -->
    <circle cx='400' cy='300' r='120' fill='none' stroke='#ffffff55' stroke-width='1' />
    <circle cx='400' cy='300' r='200' fill='none' stroke='#ffffff55' stroke-width='1' />
    <circle cx='400' cy='300' r='280' fill='none' stroke='#ffffff55' stroke-width='1' />

    <!-- Level 1 (Managers) -->
    <circle cx='280' cy='300' r='30' fill='url(#orbitGradient)' />
    <text x='280' y='305' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='12'>Manager 1</text>

    <circle cx='520' cy='300' r='30' fill='url(#orbitGradient)' />
    <text x='520' y='305' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='12'>Manager 2</text>

    <!-- Level 2 (Employees) -->
    <circle cx='200' cy='300' r='20' fill='url(#planetGradient)' />
    <text x='200' y='305' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='10'>Emp 1</text>

    <circle cx='600' cy='300' r='20' fill='url(#planetGradient)' />
    <text x='600' y='305' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='10'>Emp 2</text>

    <circle cx='400' cy='180' r='20' fill='url(#planetGradient)' />
    <text x='400' y='185' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='10'>Emp 3</text>

    <circle cx='400' cy='420' r='20' fill='url(#planetGradient)' />
    <text x='400' y='425' fill='white' text-anchor='middle' alignment-baseline='middle' font-size='10'>Emp 4</text>
</svg>";

            // Return the SVG content as a downloadable file
            var result = new FileContentResult(System.Text.Encoding.UTF8.GetBytes(svgContent), "image/svg+xml")
            {
                FileDownloadName = "solar_system_chart.svg"
            };

            return result;
        }
    }
}
