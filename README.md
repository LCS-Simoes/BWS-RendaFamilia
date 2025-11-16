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
    ├── Converters                   # Conversão DTO ↔ ViewModel
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
- GET /Clientes/{id}
- POST /Clientes/Cadastrar
- PUT /Clientes/{id}
- DELETE /clientes/{id}

## 👨‍💻 Autor
**Lucas Simões**  
📍 Desenvolvedor focado em soluções web e arquitetura limpa  
🔗 GitHub: [LCS-Simoes](https://github.com/LCS-Simoes)


