using LesKita.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LesKita.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private const string ServerKey = "";

    [HttpPost("create")]
    public async Task<IActionResult> CreateSnapToken([FromBody] MidtransRequestDto request)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes(ServerKey + ":")));

        var payload = new
        {
            transaction_details = new
            {
                order_id = request.IdOrder,
                gross_amount = request.Total
            },
            customer_details = new
            {
                first_name = request.Siswa_Nama,
                email = request.Siswa_Email
            }
        };

        var json = JsonConvert.SerializeObject(payload);
        var response = await client.PostAsync("https://app.sandbox.midtrans.com/snap/v1/transactions",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("MIDTRANS RESPONSE: " + content); // <-- Tambahkan ini
        return Content(content, "application/json");
    }

    public class MidtransRequestDto
    {
        public string IdOrder { get; set; }
        public string Siswa_Nama { get; set; }
        public string Siswa_Email { get; set; }
        public double Total { get; set; }
    }
}

