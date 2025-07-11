# Prueba de lafise - API basica de banco (.NET 8)

Este proyecto es una API RESTful desarrollada usando **ASP.NET Core (.NET 8)**, basada
en todos los requerimientos dados en el PDF de instrucciones.

#Herramientas utilizadas:
- ASP.NET Core 8 Web API
- Entity Framework Core con SQLite
- Pruebas Unitarias con xUnit
- Swagger (documentación interactiva)

- Arquitectura limpia por capas (Models, Services, Controllers, DTOs)

# Cómo correr este proyecto: Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE recomendado: Visual Studio 2022 
- (Opcional) EF CLI: `dotnet tool install --global dotnet-ef`

# Cómo correr este proyecto: Instrucciones
1. Clona el repositorio de github usando los siguientes comandos:
    git clone https://github.com/usuario/repositorio.git
     
2. Entra a la carpeta pruebadotnet, restaura los paquetes y compilalos con los siguientes comandos:
    cd pruebadotnet
    dotnet restore
    dotnet build

3. Aplica las migraciones con los siguientes comandos:
    dotnet ef database update --project PruebaHotep.WebApi

4. corre la aplicacion con el comando:
    dotnet run --project PruebaHotep.WebApi

5. Abre Swagger para probar la API en:
    http://localhost:5000/swagger

# Cómo ejecutar las pruebas
1. Ejecuta este comando desde la raiz del proyecto (carpeta pruebadotnet)
    dotnet test
