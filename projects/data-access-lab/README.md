# DataAccessLab

Kis .NET 10, PostgreSQL és EF Core labor a relációs, SQL- és ORM-alapok újragyakorlására. A domain két entitása: `Project` és `WorkItem`. Egy projekthez több munkatétel tartozhat; egy munkatétel pontosan egy projekthez tartozik. A tartós séma migrációval elkészült; a GET a projektet és munkatételeit adja vissza, a POST és PATCH munkatételt hoz létre, illetve módosít.

A véges mérföldkőterv, a teljesítési bizonyítékok és az exit gate a [MENTORING_STATE.md](MENTORING_STATE.md) fájlban vannak. M1–M5 és az alapozó exit gate teljesült; a projekt lezárt.

Az M1-hez tartozó ajánlott olvasási sorrend: [Relációs modell, kulcsok és első SQL](../../learning-materials/data-databases/relational-model-and-first-sql.md).

## Helyi környezet

- `DataAccessLab.Api`: `net10.0` ASP.NET Core projekt Npgsql EF Core providerrel, `DbContext`-tel, entitykkel, `InitialSchema` migrációval, projektet lekérdező GET-tel és munkatétel POST/PATCH végpontokkal.
- `check-get-project.ps1`: a futó API `GET /projects/1` és `GET /projects/99` válaszának ismételhető ellenőrzése; az M3 tesztadat szükséges hozzá.
- `compose.yaml`: helyi PostgreSQL 16 a `localhost:5433` porton; külön named volume-ot használ.
- A Compose a Git által figyelmen kívül hagyott helyi `.env` fájl `DATA_ACCESS_LAB_DB_PASSWORD` értékét használja; az API ugyanezt a jelszót a `Database:Password` User Secrets kulcsból olvassa. A fejlesztői connection string nem tartalmaz jelszót.

A repository gyökeréből a helyi adatbázis indítása és megnyitása:

```powershell
docker compose --env-file projects/data-access-lab/.env -f projects/data-access-lab/compose.yaml up -d
docker compose --env-file projects/data-access-lab/.env -f projects/data-access-lab/compose.yaml exec postgres psql -U data_access_lab -d data_access_lab
```

Az API-váz önállóan buildelhető:

```powershell
dotnet build projects/data-access-lab/DataAccessLab.slnx
```

## Lezárás

A további index-, query planner-, MVCC- és production-migrációs gyakorlat külön, szükségvezérelt tanulási szelet. A rövid, visszafordítható EF Core modell–snapshot kísérlet 2026-10-31-én teljesült; a következő SQL–EF lekérdezésgyakorlás iránya a repository gyökerében lévő [LEARNING_STATE.md](../../LEARNING_STATE.md) fájlban szerepel.
