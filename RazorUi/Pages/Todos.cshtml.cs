using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorUi.Models;
using System.Text.Json;

namespace RazorUi.Pages;

public class TodosModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TodosModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<TodoItem> Todos { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ApiLegacyHost");
            var response = await client.GetAsync("/api/todos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Todos = JsonSerializer.Deserialize<List<TodoItem>>(content, options) ?? new();
            }
            else
            {
                ErrorMessage = $"Failed to fetch todos. Status: {response.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error calling API: {ex.Message}";
        }
    }
}
