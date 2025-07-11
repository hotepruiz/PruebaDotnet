# Prueba de lafise - API basica de banco (.NET 8)

Este proyecto es una API RESTful desarrollada usando **ASP.NET Core (.NET 8)**, basada
en todos los requerimientos dados en el PDF de instrucciones.

# Funciones implementadas en esta api:
- Creacion de clientes
- Obtener datos de un cliente (y un resumen de sus cuentas) por su id

-Creacion de cuentas
-Obtener info de una cuenta (y un resumen de sus transacciones) por su id
-Obtener el saldo actual de una cuenta (calculado a partir del saldo inicial y las transacciones)

-Hacer un deposito a una cuenta
-Hacer un retiro de una cuenta (validando conforme al saldo disponible)
-Obtener un historial mas detallado de las transacciones de una cuenta mediante su id

-Se implementaron tests para probar funcionalidades de: creacion de una cuenta, operaciones retiro y deposito, consulta de saldo y revision de historial de trasnacciones

# Consideraciones importantes!!!!!!!
-Se pretende que esta API sea probada utilizando Swagger

- Los id de las cuentas son numeros enteros positivos en orden secuencial (1, 2, 3 ,4, etc)

- Los numeros de cuenta siguen la misma convencion, pero rellenando con ceros al a izquerda para llegar a 10 caracteres (0000000001, 0000000002, etc)

-El documento mencionaba la elaboracion de un test para una funcionalidad de intereses, esta funcionalidad no se mencionaba en la parte de requerimientos, asi que decidí omitirla (Pero no tendria problemas con implementarla si se proporciona mas informacion sobre los requerimientos)


# Herramientas utilizadas:
- ASP.NET Core 8 Web API
- Entity Framework Core con SQLite
- Pruebas Unitarias con xUnit
- Swagger (documentación interactiva)
- Visual Studio 2022

- Arquitectura limpia por capas (Models, Services, Controllers, DTOs)

# Cómo correr este proyecto: Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (Necesario)


# Cómo correr este proyecto: Instrucciones
1. Clona el repositorio de github usando los siguientes comandos:
    git clone https://github.com/hotepruiz/PruebaDotnet.git
     
2. Entra a la carpeta pruebadotnet y abre el .sln con VisualStudio 2022:
- cd pruebadotnet
- dotnet restore
- dotnet build

3. Aplica las migraciones con los siguientes comandos:
- dotnet ef database update --project PruebaHotep.WebApi

4. corre la aplicacion con el comando:
- dotnet run --project PruebaHotep.WebApi

5. Abre Swagger para probar la API en:
- http://localhost:5049/swagger/index.html

# Cómo ejecutar las pruebas
1. Ejecuta este comando desde la raiz del proyecto (carpeta pruebadotnet)
    dotnet test
