# RedarborEmployeesAPI

RedarborEmployeesAPI es un proyecto de tipo API Web que proporciona un CRUD para gestionar empleados de Redarbor. La aplicación permite realizar operaciones como:

- Consultar una lista de empleados.
- Inscribir un nuevo empleado.
- Editar la información de un empleado existente.
- Eliminar un empleado.

La aplicación utiliza arquitectura MVC y sigue el patrón CQRS. Para la persistencia de datos se usa Entity Framework Core para las operaciones de escritura y Dapper para las lecturas.

---

## Tecnologías y Paquetes Usados

- **.NET Core** - Framework para la construcción de aplicaciones web.
- **Entity Framework Core (EF Core)** - ORM para las operaciones de escritura en SQL Server.
- **Dapper** - Micro ORM para las consultas de lectura.
- **MediatR** - Librería para implementar el patrón CQRS y manejar comandos y consultas.
- **FluentValidation** - Validación de modelos y DTOs.
- **SQL Server** - Base de datos relacional.

---

## Estructura del Proyecto

La estructura del proyecto sigue el siguiente esquema:

- `RedarborEmployees.API` - Contiene los controladores de la API.
  
- `RedarborEmployees.Application` - Contiene la lógica de negocio para los comandos y consultas.
  - **EmployeesAdministration** - Incluye la lógica para gestionar empleados.
  - **DTOs** - Objetos de transferencia de datos.
  - **Mappers** - Configuración de mapeos entre entidades y DTOs.
  - **Validators** - Validadores de FluentValidation para asegurar que los datos cumplen con las reglas de negocio.

- `RedarborEmployees.Domain` - Capa de dominio que contiene las entidades principales y enumeraciones.

- `RedarborEmployees.Infrastructure` - Capa de infraestructura que maneja la conexión con la base de datos, las configuraciones y migraciones de EF Core.
  
- `RedarborEmployees.Web` - La capa de presentación que contiene los controladores y vistas para la interfaz de usuario.

---

## Ejecución del Proyecto

Para ejecutar el proyecto localmente y levantar el entorno, sigue los siguientes pasos:

1. **Clonar el repositorio**: Clona el proyecto en tu entorno local usando:

   ```bash
   git clone <url-del-repositorio>
   cd RedarborEmployeesAPI

2. **Asegúrate de tener Docker instalado** en tu máquina.

3. **Construir y ejecutar los contenedores con Docker Compose**:

   ```bash
   docker-compose up --build

   Este comando construirá las imágenes de Docker y ejecutará los contenedores para la API de RedarborEmployeesAPI y la base de datos SQL Server.
4. Si prefieres ejecutar el proyecto localmente sin Docker, asegúrate de configurar correctamente las variables de entorno en el archivo launchSettings.json. Necesitarás ajustar las cadenas de conexión en una variable de entorno **ConnectionStrings__ApplicationDbConection.**
5. Puedes usar la coleccion que se encuentra en la raiz de este repositorio, si estas corriendo el proyecto en docker el puerto será el 8080.