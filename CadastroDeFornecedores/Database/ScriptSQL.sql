CREATE DATABASE IF NOT EXISTS cadastro_fornecedores;

USE cadastro_fornecedores;

CREATE TABLE fornecedores (
	Id INT AUTO_INCREMENT PRIMARY KEY,
	RazaoSocial VARCHAR(100) NOT NULL,
	CNPJ VARCHAR(18) NOT NULL UNIQUE,
	Logradouro VARCHAR(100) NOT NULL,
	Numero VARCHAR(10),
	Bairro VARCHAR(50),
	Cidade VARCHAR(50) NOT NULL,
	Estado VARCHAR(2) NOT NULL,
	CEP VARCHAR(9),
	Telefone VARCHAR(15),
	Email VARCHAR(100),
	NomeResponsavel VARCHAR(100),
	DataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_fornecedor_cnpj ON fornecedores(CNPJ);
CREATE INDEX idx_fornecedor_cidade ON fornecedores(Cidade);
CREATE INDEX idx_fornecedor_estado ON fornecedores(Estado);
CREATE INDEX idx_fornecedor_razao_social ON fornecedores(RazaoSocial);