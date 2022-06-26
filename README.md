# IdentityServer
Instanciando um Identity com o .NET 6

Esse projeto é a modelagem básica para implementação de um IdentityServer, nele há os recursos básicos para controle de usuários, como Login, registro, Recuperação de senha e Logout. A também um TokenService, que gerar um token indivídual para cada instância da API em execução.

Com relação a Token gerada, é gerado de forma aleatória uma chave em um arquivo local, que poderá ser informada do bearer token para realizar a validação.

# Pacotes necessários

* PackageReference Include="AutoMapper" Version="11.0.1"
* AutoMapper.Extensions.Microsoft.DependencyInjection Version="11.0.0"
* FluentResults Version="3.7.0"
* Microsoft.AspNetCore.Identity Version="2.2.0"
* Microsoft.AspNetCore.Identity.EntityFrameworkCore Version="6.0.6"
* Microsoft.EntityFrameworkCore Version="6.0.6"
* Microsoft.EntityFrameworkCore.Relational Version="6.0.6"
* Microsoft.EntityFrameworkCore.Tools Version="6.0.6"
* Microsoft.Extensions.Identity.Stores Version="6.0.6"
* Pomelo.EntityFrameworkCore.MySql Version="6.0.1"
* Swashbuckle.AspNetCore Version="6.3.1"
* System.IdentityModel.Tokens.Jwt Version="6.20.0"
