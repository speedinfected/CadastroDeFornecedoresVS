# Sistema de Cadastro de Fornecedores  

Aplicação Windows Forms em C# (.NET Framework) para gerenciamento de fornecedores com consulta automática de CNPJ via API e armazenamento em MySQL.  

---

## 📋 Requisitos Técnicos  
- **Linguagem**: C# (.NET Framework 4.7+)  
- **Banco de Dados**: MySQL/MariaDB  
- **API Externa**: [ReceitaWS](https://receitaws.com.br/) (consulta CNPJ)  
- **Dependências**:  
  - `MySql.Data` (NuGet)  
  - `Newtonsoft.Json` (NuGet)  

---
## 🎥 Vídeo Demonstrativo

Clique na imagem abaixo para assistir ao vídeo no YouTube ou entre na Pasta /docs para mais informações:

[![Demonstração do Sistema - Cadastro de Fornecedores](https://img.youtube.com/vi/of3azRc7EUM/maxresdefault.jpg)](https://www.youtube.com/watch?v=of3azRc7EUM "Assista ao vídeo")
---
## 🛠️ Configuração  

### 1. Banco de Dados  
- Execute o script `ScriptSQL.sql` para criar o banco e a tabela:  

CREATE DATABASE cadastro_fornecedores;
USE cadastro_fornecedores;
CREATE TABLE fornecedores (...);  
 -- Ver arquivo completo no repositório (ScriptSQL.sql)

### 2. Connection String

 - Ajuste em DatabaseSingleton.cs se necessário: <br>

 - private readonly string _connectionString = "Server=localhost;Database=cadastro_fornecedores;Uid=root;Pwd=;";

### 3. API de CNPJ

   - A aplicação usa a ReceitaWS (sem chave, mas com limite de 3 consultas/minuto).

   - Tratamento de erros implementado em CNPJService.cs (timeout, falha de conexão).

---

🎯 Funcionalidades
## 1. Cadastro de Fornecedores

  - Automático: Busca dados por CNPJ (via API) e preenche o formulário (BuscarCNPJForm.cs).

  - Manual: Campos validados (CNPJ, CEP, e-mail, etc.) antes do salvamento (CadastroForm.cs).

2. Validações

   - CNPJ: Dígitos verificadores e formato (CNPJValidator.cs).

   - CEP: Formato 8 dígitos (CEPValidator.cs).

   - E-mail: Regex básico (EmailValidator.cs).

   - Estado: Sigla válida (EstadoValidator.cs).

3. CRUD Completo

   - Listagem: DataGridView com ordenação (MainForm.cs).

   - Edição/Exclusão: Botões habilitados apenas com item selecionado (MainForm.cs).

4. Logging

   - Registro de operações em logs.txt (caminho: ./logs.txt) (Logger.cs). <=(Normalmente fica em \bin\Debug)

---

## 🧩 Design Patterns
### 1. Singleton (DatabaseSingleton.cs)

  Objetivo: Garantir uma única conexão ativa com o banco de dados.

  Implementação:
   
       public sealed class DatabaseSingleton {
       private static DatabaseSingleton _instance;
       private DatabaseSingleton() {}  // Construtor privado
       public static DatabaseSingleton Instance {
        get {
            if (_instance == null) {
                _instance = new DatabaseSingleton();
            }
            return _instance;
            }
          }
        }

### 3. Abstract Factory (AbstractFactory.cs)

   Objetivo: Criar famílias de objetos (serviços de API e banco) sem acoplamento.

   Implementação: 

        public interface IServiceFactory {
        IDataService CreateDataService();  // MySQL
        IApiService CreateApiService();    // ReceitaWS
        }

   ---
   ## ⚠️ Tratamento de Erros

  - Banco de Dados: Mensagens claras para falhas de conexão (DatabaseSingleton.cs).

  - API: Feedback se a ReceitaWS estiver offline (CNPJService.cs).

  - Formulário: Campos inválidos destacados em vermelho (CadastroForm.cs).

    ---
 ## 📦 Estrutura do Código

    └── CadastroDeFornecedoresVS/
     ├──CadastroDeFornecedores/
      ├── Services/
       │   ├── AbstractFactory.cs      # Forcene os Serviços da Aplicação
       │   ├── CNPJService.cs          # Consulta CNPJ via API
       │   ├── FornecedorService.cs    # Lógica de negócios
       │   ├── Logger.cs               # Registro de logs
      ├── Validators/
       │   ├── CNPJValidator.cs        # Validação de CNPJ
       │   ├── CEPValidator.cs         # Validação de CEP
       │   └── EmailValidator          # Validação de Email
       │   └── EstadoValidator         # Validação de Estado
      ├── Database/    
       ├── DatabaseSingleton.cs    # Conexão única com MySQL
       └── ScriptSQL.sql           # Script do banco
     - MainForm.cs             # Listagem de fornecedores
     - CadastroForm.cs         # Formulário de cadastro
     - BuscarCNPJForm.cs       # Consulta de CNPJ

   ---
   ## 🚀 Execução

   -Compile o projeto no Visual Studio.

   -Execute WindowsFormsApp1.exe.

  -Use o menu principal para:

   - Cadastrar (manual ou via CNPJ).

   - Editar/Excluir fornecedores existentes.


---
## 📌 Observações 

   - Limitações da API: A ReceitaWS pode recusar consultas frequentes.
    
### DEPENDÊNCIAS! IMPORTANTE NOVAMENTE!
## - Pacotes NuGet: `MySql.Data`, `Newtonsoft.Json`
## - API Externa: ReceitaWS (sem chave, mas com limite de 3 consultas/minuto)
