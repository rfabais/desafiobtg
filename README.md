# desafiobtg
Repositório para desafio de Engenheiro de Software BTG - Processar pedidos e gerar relatório


Funcionalidades
	Publicação de mensagens no RabbitMQ.
	Consumo e processamento de mensagens.
	Registro e consulta de pedidos no PostgreSQL


Tecnologias Utilizadas
	ASP.NET Core: Framework para construção da API.
	PostgreSQL: Banco de dados relacional.
	RabbitMQ: Mensageria para processamento assíncrono.
	Docker: Gerenciamento de contêineres.


Acessar Endpoints
	Documentação via Swagger: http://localhost:5000/swagger.


Endpoints Disponíveis
	Método	Endpoint				Descrição
	POST	/api/rabbitmq/publicar	Publicar mensagem no RabbitMQ.
	GET		/api/pedidos			Listar todos os pedidos.
	GET		/api/pedidos/{id}		Consultar pedido por ID.

