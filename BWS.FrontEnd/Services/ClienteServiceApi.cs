using BWS.FrontEnd.Models;

public class ClienteServiceApi
{
    private readonly HttpClient _http;

    public ClienteServiceApi(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ClienteViewModel>> ListarAsync(string? nome = null)
    {
        var url = "Clientes";

        if (!string.IsNullOrWhiteSpace(nome))
            url += $"?nome={nome}";

        return await _http.GetFromJsonAsync<List<ClienteViewModel>>(url);
    }

    public async Task<ClienteViewModel?> BuscarPorIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ClienteViewModel>($"Clientes/{id}");
    }

    
    public async Task<bool> CriarAsync(CadastroViewModel model)
    {
        var resposta = await _http.PostAsJsonAsync("Clientes/Cadastrar", model);
        return resposta.IsSuccessStatusCode;
    }

    
    public async Task<bool> AtualizarAsync(int id, ClienteViewModel model)
    {
        var resposta = await _http.PutAsJsonAsync($"Clientes/{id}", model);
        return resposta.IsSuccessStatusCode;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var resposta = await _http.DeleteAsync($"Clientes/{id}");
        return resposta.IsSuccessStatusCode;
    }

}
