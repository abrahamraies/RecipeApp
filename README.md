# 🍽️ Recipe API - .NET 9 ASP.NET Core Web API

[![.NET](https://img.shields.io/badge/.NET-9-blue)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/MySQL-9-green)](https://www.mysql.com/)
[![Swagger](https://img.shields.io/badge/Swagger-Docs-yellow)](https://swagger.io/)
[![Docker](https://img.shields.io/badge/Docker-Supported-blue)](https://www.docker.com/)
[![Render](https://img.shields.io/badge/Deployed%20on-Render-blueviolet)](https://render.com/)

## Descripción General
Recipe API es un backend desarrollado con **.NET 9** y **ASP.NET Core Web API**, siguiendo la arquitectura limpia (`API`, `Application`, `Domain`, `Infrastructure`). Permite a los usuarios buscar recetas por ingredientes, agregar recetas a favoritos, gestionar un carrito de compras y verificar correos electrónicos mediante tokens.

El proyecto utiliza tecnologías modernas como:
- **JWT**: Para autenticación segura.
- **SendGrid**: Para envío de correos electrónicos (verificación de correo, recuperación de contraseña).
- **Entity Framework Core**: Con migraciones para gestionar una base de datos MySQL 9.
- **Swagger**: Para documentación interactiva de la API.
- **Docker**: Soporte para contenerización.
- **Render**: Plataforma de despliegue en producción.

## 🚀 Características Principales
- 🔐 **Autenticación JWT**: Protección de rutas privadas con tokens JWT.
- ✉️ **Verificación de correo electrónico**: Envío de enlaces de verificación mediante SendGrid.
- 🍳 **Búsqueda de recetas**: Filtrado avanzado de recetas por ingredientes.
- ⭐ **Gestión de favoritos**: Agregar y eliminar recetas favoritas.
- 🛒 **Carrito de compras**: Agregar y gestionar ingredientes para planificar compras.
- 📚 **Documentación interactiva**: Swagger disponible en `/swagger`.
- 🐳 **Soporte Docker**: Configuración lista para contenerización.
- 🚀 **Despliegue en Render**: Optimizado para producción.

## 📋 Requisitos Previos
Antes de ejecutar el proyecto, asegúrate de tener instalado:
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [MySQL 9](https://dev.mysql.com/downloads/)
- [Docker](https://www.docker.com/) (opcional, para contenerización)
- Una cuenta en [SendGrid](https://sendgrid.com/) para enviar correos electrónicos
- Herramientas de línea de comandos (Git, CLI de .NET, etc.)
- [Postman](https://www.postman.com/) (opcional, para probar la API)

## 🛠️ Instalación Local

1. Clonar el repositorio:
   ```sh
   git clone https://github.com/usuario/recipe-api.git
   cd recipe-api
   ```

2. Configurar las variables de entorno en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=recipeapp;User=root;Password=root;"
     },
     "JwtSettings": {
       "Key": "your_jwt_secret_key",
       "Issuer": "your_issuer",
       "Audience": "your_audience",
       "ExpirationMinutes": 3600
     },
     "SendGrid": {
       "ApiKey": "your_sendgrid_api_key",
       "FromEmail": "your_email@example.com",
       "FromName": "Recipe App"
     }
   }
   ```

3. Aplicar las migraciones:
   `dotnet ef database update`

4. Ejecutar la API:
   `dotnet run --project RecipeApp.API`

🐳 **Despliegue con Docker**

1. Crear un archivo `.env` con las siguientes variables:
   - `ConnectionStrings__DefaultConnection="Server=db;Database=recipeapp;User=root;Password=root"`
   - `JwtSettings__Key="your_jwt_secret_key"`
   - `JwtSettings__Issuer="your_issuer"`
   - `JwtSettings__Audience="your_audience"`
   - `SendGrid__ApiKey="your_sendgrid_api_key"`
   - `SendGrid__FromEmail="your_email@example.com"`
   - `FrontendUrl="http://localhost:3000"`

2. Construir y levantar los contenedores con Docker Compose:
   `docker-compose up --build -d`

3. Verificar los contenedores en ejecución:
   `docker ps`

4. La API estará disponible en: [http://localhost:7253](http://localhost:7253)

📡 **Despliegue en Render**

- Crear una cuenta en Render.
- Subir el código a GitHub/GitLab y conectarlo con Render.
- Configurar las variables de entorno en Render.
- Hacer deploy desde el panel de Render.

🤝 **Contribuciones**

¡Las contribuciones son bienvenidas! Si deseas mejorar la API, corregir errores o agregar nuevas funcionalidades, sigue estos pasos:

- Haz un fork del repositorio.
- Crea una nueva rama: `git checkout -b feature/nueva-funcionalidad`.
- Haz commit de tus cambios: `git commit -m "feat: descripción de tu cambio"`.
- Envía tus cambios: `git push origin feature/nueva-funcionalidad`.
- Abre un Pull Request explicando tus cambios.

Por favor, asegúrate de seguir las pautas de estilo del código y de que tus cambios pasen todas las pruebas existentes.

📜 **Licencia**

Este proyecto está bajo la licencia MIT.

📧 **Contacto**

Para consultas, puedes escribir a: abrahamraies@gmail.com
