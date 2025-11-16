namespace BWS.FrontEnd.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateTime DataCadastro {  get; set; }
        public decimal? RendaFamilia { get; set; }
        public string? Classe { get; set; }

        public int Idade
        {
            get
            {
                var hoje = DateOnly.FromDateTime(DateTime.Today);
                int idade = hoje.Year - DataNascimento.Year;

                if (DataNascimento > hoje.AddYears(-idade))
                    idade--;

                return idade;
            }
        }
    }
}
