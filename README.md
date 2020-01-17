# Permisos
Rápida web de prueba.

### Instalación
#### MSSMS
1. Abra su administrador de base de datos de SQL Server.
2. Abra el script de base de datos ubicado en **Permisos/Documentos/database.sql**
3. Ejecute el script para crear la base de datos. (Asegúrese de que el string de conexión coincida con el servidor elegido.)

#### Code First
Siga las instrucciones de [Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

### Uso
1. Crear la base de datos.
2. Abrir Permisos.sln con visual studio. (vs 2019 preferiblemente.)
3. Ejecutar aplicación con F5.
4. Ir al siguiente url: [localhost:5000](http://localhost:5000).

### Proyectos y Capas
- Permisos           | Dominio
- Permisos.Común     | Común
- Permisos.SqlServer | Persistencia
- Permisos.Pruebas   | Pruebas

### Arquitecturas
- Clean
- MVC
- N-Capas

### Patrones de diseño
- Inyección de dependencias
- Repositorio
- Unidad de trabajo
- Instancia única
- Carga diferida
- Estado
- Traductor

### Frameworks y NuGets
- Ardalis.GuardClauses
- EntityFrameworkCore
- MSTest
- AutoMapper
- coverlet.collector
- Moq

### No implementado
- Auditoria de base de datos
- Auditoria de errores
- Autentificación
- Cahing
- Fluent Validation
- Fluent Assertion













