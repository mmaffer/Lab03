# 🏫 Sistema de Gestión de Reservas de Aulas

Este es un sistema de escritorio desarrollado en **WPF (C#)** bajo el patrón de arquitectura **MVVM** y conectado a una base de datos **SQL Server** mediante **ADO.NET**. Permite autenticar usuarios, consultar la lista de aulas disponibles y registrar nuevas reservas de espacios controlando automáticamente los conflictos de horario.

---

## 📋 Requisitos previos

Para ejecutar la aplicación en otra computadora se necesita tener instalado:

1. **Visual Studio 2022**, con la carga de trabajo *Desarrollo de escritorio de .NET*.
2. **SQL Server Express**, con la instancia predeterminada `.\SQLEXPRESS`.
3. **SQL Server Management Studio (SSMS)**, recomendado para ejecutar el script de base de datos.

---

## 🚀 Pasos para inicializar el proyecto

### 1. Configurar la base de datos

1. Abre **SQL Server Management Studio (SSMS)** y conéctate a tu servidor local (`.\SQLEXPRESS`).
2. Abre una **Nueva consulta** (*New Query*) y ejecuta el siguiente script para crear la base de datos `ReservasDB` con sus tablas y datos de prueba:

```sql
USE [master]
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'ReservasDB')
BEGIN
    ALTER DATABASE [ReservasDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [ReservasDB];
END
GO

CREATE DATABASE [ReservasDB]
GO

USE [ReservasDB]
GO

-- 1. Tabla Aulas
CREATE TABLE [dbo].[Aulas](
    [AulaId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Nombre] [nvarchar](50) NOT NULL,
    [Capacidad] [int] NOT NULL
)
GO

-- 2. Tabla Usuarios
CREATE TABLE [dbo].[Usuarios](
    [UsuarioId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Username] [nvarchar](50) NOT NULL UNIQUE,
    [Password] [nvarchar](50) NOT NULL,
    [NombreCompleto] [nvarchar](100) NOT NULL
)
GO

-- 3. Tabla Reservas
CREATE TABLE [dbo].[Reservas](
    [ReservaId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [AulaId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Aulas]([AulaId]),
    [UsuarioId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Usuarios]([UsuarioId]),
    [Fecha] [date] NOT NULL,
    [Hora] [time](0) NOT NULL,
    [Motivo] [nvarchar](200) NOT NULL
)
GO

-- Datos iniciales de prueba
INSERT INTO [dbo].[Usuarios] ([Username], [Password], [NombreCompleto]) VALUES
('admin', 'admin123', 'Administrador del Sistema'),
('jperez', '123456', 'Juan Pérez');

INSERT INTO [dbo].[Aulas] ([Nombre], [Capacidad]) VALUES
('Laboratorio de Cómputo 101', 30),
('Aula Magna', 150),
('Sala de Conferencias A', 50),
('Taller de Electrónica 202', 25);
GO
```

### 2. Configurar la cadena de conexión

La aplicación está configurada para conectarse a la instancia `.\SQLEXPRESS`. Si la instancia de SQL Server es diferente, ajusta la cadena de conexión en `DbConnection.cs`, dentro de la carpeta `Data/`:

```csharp
Server=.\SQLEXPRESS;Database=ReservasDB;Trusted_Connection=True;TrustServerCertificate=True;
```

### 3. Iniciar sesión

Puedes iniciar sesión en la aplicación utilizando cualquiera de estas credenciales de prueba:

| Usuario | Contraseña | Nombre completo |
| --- | --- | --- |
| `admin` | `admin123` | Administrador del Sistema |
| `jperez` | `123456` | Juan Pérez |

### 4. Compilar y ejecutar

1. Abre el archivo `Lab03.sln` en Visual Studio.
2. Selecciona **Compilar > Recompilar solución** (`Ctrl + Shift + B`).
3. Presiona `F5` para iniciar la aplicación.

---

## 📁 Estructura del proyecto

```text
Lab03/
├── Data/       # Repositorios y conexión a SQL Server (ADO.NET)
├── Models/     # Clases de entidad (Usuario, Aula, Reserva)
├── ViewModels/ # Lógica de presentación y Data Binding (MVVM)
└── Views/      # Interfaz gráfica de usuario en XAML
```
