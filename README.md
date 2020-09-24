## Trabalho de Conclusão de Disciplina (Case "Netflix" )

### Arquitetura

Para construção da aplicação empregamos coneceitos de "Clean Architecture" utilizando a stack abaixo:

* ASP.Net Core 2.1 API
* C#
* MySQL
* Dapper
* RabbitMQ

![arquitetura_api](https://user-images.githubusercontent.com/17520851/94091247-29969680-fdee-11ea-9210-7a439a3a5888.PNG)


### Execução (Play With Docker)

Para execução da aplicação sugerimos os seguintes passos:

* 1 - Acesse a URL do Play With Docker (https://labs.play-with-docker.com/)
* 2 - Faça o login na plataforma utilizando a conta DockerHub (https://hub.docker.com/)
* 3 - Após login inicie aplicação cliclando em "Start"
* 4 - Adicione uma nova instância
![add_instance](https://user-images.githubusercontent.com/17520851/93142777-0d437d00-f6bd-11ea-9a2d-2ea4fe123f45.PNG)

* 5 - Será exibido o terminal de comandos, execute a clonagem do repositório
  * 5.1 - `git clone https://github.com/douglaspmachado/fiap73aoj.servicearchitecture.git`
  * 5.2 - `cd fiap73aoj.servicearchitecture`
  * 5.3 - `cd src`
  * 5.4 - `docker-compose up -d` (Inicia aplicação)
* 6 - Após a execução do comando docker-compose aplicação deve ficar em modo "up", sinalizando as "portas" configuradas no arquivo YAML conforme imagem abaixo:

![up](https://user-images.githubusercontent.com/17520851/93144247-fb170e00-f6bf-11ea-8a7b-1ed3fc5b0f92.PNG)

* 7 - Acesse os componentes da aplicação a partir de suas respectivas "portas"
  * 7.1 - Clicando na porta "20001" acesse o Swagger da aplicação.
  * 7.2 - Clicando na porta "15672" acesse o serviço do RabbitMQ (mensageria) da aplicação.
    * 7.2.1 - Realize o login no rabbit para acompanhar o trafego de mensagens utilizando as credencias abaixo: 
    * 7.2.2 - Username: guest
    * 7.2.3 - Password: guest
    
* 8 - Serviços em funcionamento. 
![serviços_up](https://user-images.githubusercontent.com/17520851/93146587-9959a280-f6c5-11ea-9be2-477df6f9106f.PNG)

    
### Dados academicos

Profº TADEU D’ALESSANDRO BARBOSA
  
  
  
  
      





