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
Ejecuta un demo con un argumento del `1` al `10`:
```bash
dotnet run -- 1
```

| Número | Tema |
|--------|------|
| 1 | LINQ: sintaxis de consulta (estilo SQL) |
| 2 | LINQ: sintaxis de métodos |
| 3 | Joins entre entidades |
| 4 | Propiedades de navegación |
| 5 | Carga ansiosa (*eager loading* con `Include`) |
| 6 | Carga explícita (`Load`) |
| 7 | Carga diferida (*lazy loading*) |
| 8 | Problema N+1 |
| 9 | Evaluación en cliente vs servidor (dónde va `ToList()`; revisa el SQL en consola) |
| 10 | Inyección SQL y `FromSqlRaw` |
