# DataAccessLab

Kis .NET 10, PostgreSQL és EF Core labor a relációs, SQL- és ORM-alapok újragyakorlására. A domain két entitása: `Project` és `WorkItem`. Egy projekthez több munkatétel tartozhat; egy munkatétel pontosan egy projekthez tartozik. Az API egyelőre csak az ASP.NET Core sablonja, az adatfunkciókat a tanuló írja meg.

A véges mérföldkőterv, a teljesítési bizonyítékok és az exit gate a [MENTORING_STATE.md](MENTORING_STATE.md) fájlban vannak. A labor aktuális feladata M1: relációs modell és SQL-frissítés. Az EF Core a következő mérföldkőben jelenik meg, így nem kell előbb a teljes adatbázis-internals témát feldolgozni.

Az M1-hez tartozó ajánlott olvasási sorrend: [Relációs modell, kulcsok és első SQL](../../learning-materials/data-databases/relational-model-and-first-sql.md).

## Előkészített környezet

- `DataAccessLab.Api`: üres, futtatható ASP.NET Core projekt `net10.0` targettel. Egyelőre nincs EF Core package, `DbContext` vagy üzleti endpoint.
- `compose.yaml`: helyi PostgreSQL 16 a `localhost:5433` porton; külön named volume-ot használ.
- `.env.example`: csak helyettesítő értéket tartalmaz. A valódi helyi jelszó a Git által figyelmen kívül hagyott `.env` fájlba kerüljön.

A repository gyökeréből, ha eljött az első SQL-próba ideje:

```powershell
Copy-Item projects/data-access-lab/.env.example projects/data-access-lab/.env
# Szerkeszd át a projects/data-access-lab/.env fájlban a helyi jelszót.
docker compose --env-file projects/data-access-lab/.env -f projects/data-access-lab/compose.yaml up -d
docker compose --env-file projects/data-access-lab/.env -f projects/data-access-lab/compose.yaml exec postgres psql -U data_access_lab -d data_access_lab
```

Az API-váz önállóan buildelhető:

```powershell
dotnet build projects/data-access-lab/DataAccessLab.slnx
```

## Egyetlen aktuális lépés

Írd le kód nélkül a `Project`–`WorkItem` kapcsolatot: melyik két táblát használnád, hol van az elsődleges és az idegen kulcs, mely mezők kötelezők, és milyen adatbázis-szabály akadályozza meg, hogy egy munkatétel nem létező projektre mutasson. Ezt először közösen ellenőrizzük, utána írod és futtatod az első SQL-t.

Az M1 későbbi gyakorlati részében ideiglenes PostgreSQL-táblákon dolgozunk. Így a kézi SQL-kísérlet nem fog ütközni az EF Core későbbi migrációjával, amely majd a tartós táblákat hozza létre.
