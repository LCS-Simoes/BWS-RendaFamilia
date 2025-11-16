# BWS – Sistema de Cadastro e Relatórios de Clientes

O BWS é um sistema desenvolvido em ASP.NET Core MVC com uma API REST em .NET 8 para gerenciamento completo de clientes.
A aplicação permite realizar cadastro, edição, exclusão, listagem e geração de relatórios dinâmicos com filtros por data (Hoje, Semana e Mês).

O objetivo é fornecer uma solução prática e didática para estudos de ASP.NET, MVC, consumo de API e Entity Framework.

## 🚀 Funcionalidades

-  Cadastro completo de clientes
-  Edição e atualização de dados
-  Tela de confirmação antes de excluir
-  Listagem com filtro por nome

📊 Relatorios de clientes, incluindo:
- Clientes maiores de 18 anos acima da média de renda
- Quantidade de clientes por classe: A, B e C
- Filtro de período: Hoje, Esta Semana, Este Mês

🔌 Consumo da API via HttpClient no Front-End
🎨 Design usando Bootstrap + CSS customizado para cards

## ⚙️ Dependências Utilizadas
- API (.NET 8)
- Biblioteca	Versão
- Entity Framework Core	9.0.9
- EF Tools	9.0.9
- EF Design	9.0.9
- EF SQLite	9.0.9

Front-End
- ASP.NET Core MVC
- Bootstrap 

## Estrutura do Projeto

```
  BWS/
├── BWS.API                          # API REST (.NET 8)
│   ├── Controllers                  # Endpoints da API
│   ├── Dependências                 # Injeção de dependência
│   ├── appsettings.json             # Configurações gerais da API
│   └── Program.cs                   # Configuração da aplicação
│
├── BWS.Application                  # Camada de Aplicação 
│   ├── Dependências                 # Registro da camada na DI
│   ├── DTOs                         # Objetos de transferência de dados
│   ├── Helper                       # Dependencia
│   ├── Services                     # Regras de negocio da aplicação
│   └── UseCases                     # Casos de uso específicos
│
├── BWS.Domain                        # Domínio (entidades e regras centrais / Interfaces)
│   └── *Entidades / Objetos de domínio*  
│
├── BWS.Infrastructure               # Infraestrutura (SQLite, EF etc)
│   ├── Dependências                 # Configuração e DI da camada
│   ├── Data                         # DbContext, repositórios e arquivos .db
│   │   ├── Map                      # Mapeamentos do EF Core
│   │   └── Repositorios             # Implementações concretas
│   └── bwsDbContext.cs              # Contexto do banco de dados
│
└── BWS.FrontEnd                     # Aplicação MVC consumindo a API
    ├── Controllers                  # Lógica das views
    ├── Converters                   # Provisório devigo a bug
    ├── Models                       # ViewModels usados na interface
    ├── Services                     # Cliente HTTP que chama a API
    ├── Views                        # Telas Razor
    └── wwwroot
        ├── css                      # CSS customizado
        ├── js                       # Scripts
        └── lib                      # Bootstrap e libs externas
```

## ⚠️ Configuração para o Banco

É necessário passar o caminho do banco no appsettings.json da API, exemplo:
```
  "ConnectionStrings": {
  "DefaultConnection": "Data Source=C:\\SEU_CAMINHO\\Seubanco.db"
```

## EndPoints

- GET /Clientes
  🔹- Retorna todos os clientes 
- GET /Clientes/{id}
  🔹 - Retorna somente um cliente especifico
- POST /Clientes/Cadastrar
  🔹 - Cria um cliente
- PUT /Clientes/{id}
  🔹 - Atualiza um cliente especifico
- DELETE /clientes/{id}
  🔹 - Deleta um cliente especifico

Exemplo do JSON gerado para um cliente cadastrado:
```
  {
  "nome": "Lucas",
  "cpf" : "xxxxxxxxxxxxx",
  "dataNascimento": "2025-11-16",
  "dataCadastro": "2025-01-01T00:00:00",
  "rendaFamilia": 2500,
  "classe": "A" 
}
```

## Cálculo da Idade 
Ele pega o ano atual e subtrai o ano de nascimento para obter a idade bruta. Depois verifica se o aniversário da pessoa ainda não aconteceu neste ano comparando o “dia do ano” da data de nascimento com o “dia do ano” de hoje.
Se o aniversário ainda não chegou, ele subtrai 1. Se já passou, mantém a idade.
```
  public int Idade =>
    DateOnly.FromDateTime(DateTime.Today).Year - DataNascimento.Year -
    (DataNascimento.DayOfYear > DateOnly.FromDateTime(DateTime.Today).DayOfYear ? 1 : 0);
```

## 🐱‍🏍 Como Rodar o Projeto
1️⃣ Rodar a API
```
cd BWS.API
dotnet ef database update
dotnet run
```
```
2️⃣ Rodar o Front-End
cd BWS.FrontEnd
dotnet run
```
```
Acesse:
https://localhost:{porta}/Clientes
```
⚠️ Observação: É necessário sincronizar as portas de acesso nas Program.cs da API e do FrontEnd para funcionarem corretamente ⚠️

## 👨‍💻 Autor
**Lucas Simões**  
📍 Desenvolvedor focado em soluções web e arquitetura limpa  
🔗 GitHub: [LCS-Simoes](https://github.com/LCS-Simoes)


