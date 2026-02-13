# Camel Registry Backend

Camel Registry egy ASP.NET Core 9 Minimal API alapú szolgáltatás, amely tevék adatait kezeli SQLite adatbázisban.  
CRUD műveleteket biztosít, DTO validációval, és Swagger UI-n keresztül dokumentált.

---

## Fő jellemzők

- **Backend:** ASP.NET Core 9 Minimal API  
- **Adatbázis:** SQLite + Entity Framework Core  
- **Validáció:** FluentValidation (DTO-k)  
- **API dokumentáció:** Swagger UI (`/swagger`)  
- **Tesztelés:** xUnit + FluentAssertions + EF Core InMemory  

---

## Funkcionalitás

### CRUD végpontok

| HTTP | Endpoint             | Leírás                          |
|------|--------------------|--------------------------------|
| POST | `/api/camels`       | Új teve létrehozása            |
| GET  | `/api/camels`       | Tevék listázása                |
| GET  | `/api/camels/{id}`  | Teve lekérdezése ID alapján    |
| PUT  | `/api/camels/{id}`  | Teve adatainak frissítése      |
| DELETE | `/api/camels/{id}`| Teve törlése                   |

### DTO Validáció

- **HumpCount:** csak 1 vagy 2 lehet  
- **Name:** kötelező, minimum 3 karakter, csak betűk  
- **Color:** minimum 3 karakter  
- **LastFed:** nem lehet a jövőben  

---

