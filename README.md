# Prueba NeComPlus (BackEnd)

Esta es la parte de backend de la prueba técnica de la entrevista.


# Setup
Deberemos tener instalado el **SDK de .NET Framework Core 2.2** para poder ejecutarlo.

# Running
Para ejecutarlo solamente necesitaremos ir a la carpeta del proyecto y ejecutar el comando `dotnet run` y escuchará desde la dirección `http://localhost:4000/`.

# Logros
En este punto voy a describir lo que he podido hacer en esta prueba.
 - He creado la base de datos relacional de las entidades descritas.
 - La he ampliado con una base de datos de usuarios y roles (relación N:N) para el acceso al login (user: `admin@admin.com` y password: `P4$$w0rD`).
 - Todo ello ha sido modelado mediante Entity Framework Core 2.2, con Code First para ser más exactos (tengo más fresco este método).
 - He creado la API básica que se me pedía (listar entidades y crear entidades).
 - Además, he ampliado la API para que fuese un CRUD total (Crear, Editar, Ver y Eliminar) en ambas tablas (Entidades y Grupos).
 - Se ha implementado seguridad mediante JWT, tal y como se pedía.
 - Se ha usado para todo acceso a la base de datos consultas realizadas con LINQ, además de usar LINQ en algún que otro punto.