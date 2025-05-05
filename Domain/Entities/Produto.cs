namespace Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; } 
        public string Codigo { get; private set; } 
        public string Nome { get; private set; } 
        public string Descricao { get; private set; } 
        public decimal Preco { get; private set; } 
        public bool Ativo { get; private set; } 

        public Produto(string codigo, string nome, string descricao, decimal preco)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("O código do produto é obrigatório.", nameof(codigo));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));

            if (preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.", nameof(preco));

            Id = Guid.NewGuid(); 
            Codigo = codigo;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Ativo = true; 
        }

        public void Atualizar(string nome, string descricao, decimal preco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));

            if (preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.", nameof(preco));

            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}