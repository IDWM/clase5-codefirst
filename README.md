# Clase 5 - Code First

API demostrativa de Entity Framework Core (Code First).

## Configuración Inicial
Prepara el archivo de configuración a partir del ejemplo proporcionado:
```bash
cp appsettings.Example.json appsettings.json
```

## Base de Datos
La base de datos se genera automáticamente al iniciar el servidor. 
Comandos manuales útiles para Entity Framework Core:
- **Levantar/Actualizar:** `dotnet ef database update`
- **Eliminar:** `dotnet ef database drop`

## Ejecución
```bash
dotnet run
```
