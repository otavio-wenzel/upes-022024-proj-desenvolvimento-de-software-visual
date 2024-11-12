
using System.ComponentModel.DataAnnotations;

public class Aluno
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public DateTime? DataDeNascimento { get; set; }

    [Required(ErrorMessage = "RG é obrigatório")]
    [RegularExpression(@"^\d+$", ErrorMessage = "O RG deve conter apenas dígitos numéricos")]
    public string Rg { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Telefone é obrigatório")]
    //[RegularExpression(@"^\d{11}$", ErrorMessage = "O telefone deve conter apenas dígitos e ter 11 números")]
    //public string Telefone { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Email é obrigatório")]
    //[EmailAddress(ErrorMessage = "Formato de email inválido")]
    //public string Email { get; set; } = string.Empty;

    //public string? NomeDoTutor { get; set; }

    //[Required(ErrorMessage = "RG do tutor é obrigatório")]
    //[RegularExpression(@"^\d+$", ErrorMessage = "O RG do tutor deve conter apenas dígitos numéricos")]
    //public string RgDoTutor { get; set; } = string.Empty;
}
