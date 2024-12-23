using System.ComponentModel.DataAnnotations;

namespace Instapet.Domain.Contracts.Requests;

public class CadastrarUsuarioRequest
{

    [Required(ErrorMessage = "O campo nome é obrigatorio")]
    [MaxLength(255, ErrorMessage = "O campo nome deve ter no maximo 255 caracteres")]
    public string Nome { get; set; }

    [EmailAddress(ErrorMessage = "Deve ser informado um email valido")]
    [MaxLength(255, ErrorMessage = "Numero maximo de caracteres é 255")]
    public string Email { get; set; }

    public string? Apelido { get; set; }

    [Required(ErrorMessage = "Deve ser fornecido uma data de nascimento")]
    public DateOnly DataNascimento { get; set; }

    [Required(ErrorMessage = "Deve ser fornecido um CEP")]
    [MaxLength(8, ErrorMessage = "Cep deve ter no maximo 8 caracteres")]
    public string Cep { get; set; }

    [MaxLength(512, ErrorMessage = "Foto do perfil deve ter no maximo 512 caracteres")]
    public string UrlFotoPerfil { get; set; }

    [Required(ErrorMessage = "Deve ser definido uma senha")]
    [MaxLength(ErrorMessage = "Senha deve ter no maximo 128 caracteres")]
    public string Senha { get; set; }

}