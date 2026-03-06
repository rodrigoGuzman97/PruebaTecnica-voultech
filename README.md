# 🧾 OrdenesAPI

API REST desarrollada en **.NET 9** para la gestión de:

* Productos
* Órdenes de compra
* Asociación Orden–Producto (relación many-to-many)

Incluye:

* ✅ Entity Framework Core
* ✅ AutoMapper
* ✅ Swagger
* ✅ Paginación

---

# 🚀 Requisitos

Antes de ejecutar el proyecto debes tener instalado:

* [.NET SDK 9](https://dotnet.microsoft.com/)
* SQL Server (Express, Developer o LocalDB)
* SQL Server Management Studio (opcional)
* Visual Studio 2022 o VS Code

---

# 🗄️ Configuración de Base de Datos

El proyecto utiliza **SQL Server**.

## 1️⃣ Configurar cadena de conexión

Ir al archivo:

```
appsettings.json
```

Modificar la cadena de conexión:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SERVIDOR;Database=DATABASENAME;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
---

# 🛠️ Crear Base de Datos con Migraciones

Desde la raíz del proyecto ejecutar:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

# ▶️ Ejecutar el Proyecto

Desde la carpeta del proyecto:

```bash
dotnet run
```

O desde Visual Studio:

```
F5
```

---

# 📘 Swagger

Una vez iniciado el proyecto, acceder a:

```
https://localhost:xxxx/swagger
```

Ahí podrás probar todos los endpoints:

* POST /api/productos
* GET /api/productos
* GET /api/productos/{id}
* POST /api/ordenes
* GET /api/ordenes
* GET /api/ordenes/{id}
* PUT /api/ordenes/{id}
* DELETE /api/ordenes/{id}

---

# 🧩 Arquitectura

El proyecto está organizado en:

```
Controllers/
Services/
IServices/
DTO/
Models/
DataContext/
Mappings/
```

### 🔹 Controllers

Manejan la entrada HTTP y devuelven respuestas REST.

### 🔹 Services

Contienen la lógica de negocio.

### 🔹 DTOs

Separación entre Request y Response.

### 🔹 AutoMapper

Mapea entidades ↔ DTOs.

---

# 📦 Endpoints Principales

## 🛍 Productos

Crear producto:

```json
POST /api/productos
{
  "nombre": "Teclado",
  "precio": 15000
}
```

---

## 🧾 Órdenes

Crear orden:

```json
POST /api/ordenes
{
  "cliente": "Rodrigo",
  "productosIds": [1, 2, 3]
}
```

---

# 📄 Respuestas

Las respuestas de orden incluyen:

* Id
* Cliente
* FechaCreacion
* Total
* Productos asociados

---

# 🧪 Buenas Prácticas Implementadas

* Separación Controller / Service
* DTOs diferenciados
* Manejo transaccional en creación de orden
* NoTracking en consultas
* Paginación en listados
* Uso correcto de CreatedAtAction

---

# ⚠️ Notas Importantes

* La orden debe tener al menos un producto.
* El total se calcula automáticamente.
* Si un producto no existe, la orden no se crea.
* Se utiliza transacción para garantizar consistencia.

---

# 👨‍💻 Autor

Desarrollado como prueba técnica backend en .NET 9.

---

# 🏁 Estado del Proyecto

✔ CRUD completo de Productos
✔ CRUD completo de Órdenes
✔ Relación many-to-many
✔ Paginación
---
