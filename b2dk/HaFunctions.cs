using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace b2dk;

public class HaFunctions(ILogger<HaFunctions> logger, HttpClient client)
{

    [Function("HaList")]
    public async Task<ActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        var content = await (await client.GetAsync("https://drive.google.com/file/d/1aAgRv0inkoZFqZMzLbiKBsaSkRPzSBDo/view?usp=drive_link"))
            .Content.ReadAsStringAsync();
        var lines = content.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        return new OkObjectResult(lines.Select(l => l.Split(";")).Select(a => new
        {
            id = a[0],
            type = a[1],
            description = a[2]
        }));
    }
}