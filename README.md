# REST API на ASP.NET Core

Изучение построения RESTful API-приложение на ASP.NET Core с поддержкой:

- Аутентификации и авторизации (JWT)
- Разграничения доступа по ролям (пользователь / админ)
- Управления товарами и заказами
- Подключения к PostgreSQL через Entity Framework Core
- Swagger-документации
- Docker-контейнеризации

---

### Требования

- [.NET 7+](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/)
- [pgAdmin (по желанию)](https://www.pgadmin.org/)

---

### Запуск с помощью Docker

```bash
docker-compose up --build
```

---

### Настройки проекта (appsettings.json)

Для правильной настройки необходимо добавить appsettings.json файл с описанием:

```json
{
  "Jwt": {
    "Key": {key},
    "Issuer": {issuer},
    "Audience": {users},
    "ExpiresInMinutes": {time}
  },
  "AdminUser": {
    "Username": {admin},
    "Password": {admin_password}
  },
  "ConnectionStrings": {
    "DefaultConnection": {db_connection_string}
  }
}
```

---