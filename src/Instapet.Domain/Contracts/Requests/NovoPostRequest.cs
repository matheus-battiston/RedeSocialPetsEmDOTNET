namespace Instapet.Domain.Contracts.Requests;

public class NovoPostRequest
{
    public string UrlFoto { get; set; }
    public string Legenda { get; set; }
    public bool Permissao { get; set; }
        
}