# 3. pillér — Adatok és adatbázisok

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a tartós adatok modellezését, a relációs adatbázisok működését és a .NET adat-hozzáférési rétegeit bontja stabil, mérhető kompetenciákra. A PostgreSQL az elsődleges gyakorlati adatbázis, az EF Core pedig a fő ORM, de a cél az adatbázis- és providerfüggetlen mentális modellek kialakítása.

A pillér az ORM használata előtt és alatt is láthatóvá teszi az SQL-t, a végrehajtási tervet, az indexeket, a tranzakciókat és a konkurenciát. A cache-ek, message store-ok és elosztott adatkezelés részletes tárgyalása a 8. pillérhez, a production üzemeltetés pedig a 7. pillérhez tartozik.

## Ajánlott tanulási hullámok

1. **Relációs alapok:** `DATA-RELATIONAL-MODEL` → `DATA-SQL-QUERYING` → `DATA-SCHEMA-DESIGN` → `DATA-INTEGRITY-CONSTRAINTS`.
2. **Adatbázis-működés és teljesítmény:** `DATA-STORAGE-ENGINE` → `DATA-INDEXING` → `DATA-QUERY-PLANNING` → `DATA-TRANSACTIONS` → `DATA-CONCURRENCY-MVCC`.
3. **Alkalmazásintegráció:** `DATA-CONNECTIONS-POOLING` → `DATA-POSTGRESQL-FOUNDATIONS` → `DATA-NET-ACCESS` → `DATA-EF-MAPPING-TRACKING` → `DATA-EF-QUERY-EXECUTION` → `DATA-SCHEMA-EVOLUTION`.

Ez függőségi iránytű, nem merev sorrend. Egy projekt az EF Core felől is beléphet, de minden fontos ORM-viselkedést vissza kell kötni az alatta futó SQL-hez és adatbázis-mechanizmushoz.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `DATA-RELATIONAL-MODEL` | Relációs modell és relációs algebra alapjai | P1 | L3 |
| `DATA-SQL-QUERYING` | SQL-lekérdezések és halmazalapú gondolkodás | P1 | L3 |
| `DATA-SCHEMA-DESIGN` | Sématervezés és normalizálás | P1 | L3 |
| `DATA-INTEGRITY-CONSTRAINTS` | Típusok, kulcsok és adatintegritás | P1 | L3 |
| `DATA-STORAGE-ENGINE` | Tárolási motor és fizikai adatelrendezés | P2 | L2 |
| `DATA-INDEXING` | Indexek és adatszerkezetek | P1 | L3 |
| `DATA-QUERY-PLANNING` | Lekérdezéstervezés és optimalizálás | P1 | L3 |
| `DATA-TRANSACTIONS` | Tranzakciók és ACID | P1 | L3 |
| `DATA-CONCURRENCY-MVCC` | Izoláció, MVCC, zárolás és holtpont | P1 | L3 |
| `DATA-CONNECTIONS-POOLING` | Kapcsolatok, sessionök és connection pooling | P1 | L2 |
| `DATA-POSTGRESQL-FOUNDATIONS` | PostgreSQL használat és diagnosztika | P1 | L2 |
| `DATA-NET-ACCESS` | .NET adat-hozzáférés és providerhatár | P1 | L2 |
| `DATA-EF-MAPPING-TRACKING` | EF Core modellezés és change tracking | P1 | L2 |
| `DATA-EF-QUERY-EXECUTION` | EF Core lekérdezésfordítás és teljesítmény | P1 | L3 |
| `DATA-SCHEMA-EVOLUTION` | Migrációk és biztonságos sémafejlődés | P1 | L2 |

## Altémák részletei

### `DATA-RELATIONAL-MODEL` — Relációs modell és relációs algebra alapjai

- **Tartalom:** reláció, tuple, attribútum, domain, kulcs, kardinalitás, `1:1`, `1:N`, `N:M`, kapcsolótábla, relációs műveletek és a táblák közötti logikai kapcsolatok.
- **Előfeltételek:** alapvető adatmodellezési és halmazfogalmak.
- **Kapcsolatok:** `DATA-SQL-QUERYING`, `DATA-SCHEMA-DESIGN`; halmazok és relációk a számítástudományi sávban.
- **Célbizonyíték:** valós domain relációs modelljének önálló kialakítása, a kardinalitások és kulcsok indoklása, valamint hibás kapcsolatmodell felismerése.

### `DATA-SQL-QUERYING` — SQL-lekérdezések és halmazalapú gondolkodás

- **Tartalom:** `SELECT`, projection, filtering, `JOIN`, grouping, aggregation, ordering, subquery, CTE, set operation, window function és `NULL` háromértékű logikája.
- **Előfeltételek:** `DATA-RELATIONAL-MODEL`.
- **Kapcsolatok:** `DATA-QUERY-PLANNING`, `DOTNET-LINQ-EXECUTION`, `DATA-EF-QUERY-EXECUTION`.
- **Célbizonyíték:** többtáblás, aggregáló és ablakfüggvényt használó lekérdezések önálló megírása, eredményük ellenőrzése és gyakori logikai hibák diagnózisa.

### `DATA-SCHEMA-DESIGN` — Sématervezés és normalizálás

- **Tartalom:** entitások és attribútumok határai, funkcionális függőség, 1NF–3NF/BCNF alapmodell, denormalizálás, természetes és helyettesítő kulcs, időbeli és opcionális adatok modellezése.
- **Előfeltételek:** `DATA-RELATIONAL-MODEL`.
- **Kapcsolatok:** `DATA-INTEGRITY-CONSTRAINTS`, domainmodellezés az 5. pillérben.
- **Célbizonyíték:** egy séma normalizálása, a választott kulcsok és esetleges denormalizálás trade-offjának magyarázata, valamint redundanciából eredő anomália felismerése.

### `DATA-INTEGRITY-CONSTRAINTS` — Típusok, kulcsok és adatintegritás

- **Tartalom:** adatbázistípusok, `PRIMARY KEY`, `FOREIGN KEY`, `UNIQUE`, `NOT NULL`, `CHECK`, default, referenciális műveletek és az alkalmazás- kontra adatbázis-validáció határa.
- **Előfeltételek:** `DATA-RELATIONAL-MODEL`, `DATA-SCHEMA-DESIGN`.
- **Kapcsolatok:** `DATA-SCHEMA-EVOLUTION`, binding és validáció a 2. pillérben.
- **Célbizonyíték:** integritási szabályok adatbázisban történő megvalósítása és tesztelése, constraint violation diagnózisa és a szabály megfelelő rétegének indoklása.

### `DATA-STORAGE-ENGINE` — Tárolási motor és fizikai adatelrendezés

- **Tartalom:** page/block, row layout, heap table, tuple verziók, WAL alapmodell, cache/buffer pool, tartósság és a logikai séma fizikai költségei.
- **Előfeltételek:** `DATA-RELATIONAL-MODEL`; fájl- és I/O-alapok a 4. pillérben.
- **Kapcsolatok:** `DATA-INDEXING`, `DATA-TRANSACTIONS`, `DATA-CONCURRENCY-MVCC`.
- **Célbizonyíték:** egy lekérdezés fizikai olvasási/írási útjának magas szintű magyarázata és legalább egy tárolási vagy bloat jellegű probléma méréssel támogatott diagnózisa.

### `DATA-INDEXING` — Indexek és adatszerkezetek

- **Tartalom:** B-tree, hash és PostgreSQL-specifikus indexcsaládok áttekintése, összetett és részleges index, szelektivitás, covering lehetőség, írási és tárhely-költség.
- **Előfeltételek:** `DATA-SQL-QUERYING`, `DATA-STORAGE-ENGINE`.
- **Kapcsolatok:** `DATA-QUERY-PLANNING`; fák és komplexitás a számítástudományi sávban.
- **Célbizonyíték:** workload alapján indokolt index készítése, használatának végrehajtási tervvel történő igazolása és egy felesleges vagy rossz oszlopsorrendű index felismerése.

### `DATA-QUERY-PLANNING` — Lekérdezéstervezés és optimalizálás

- **Tartalom:** parser/planner/executor alapmodell, statisztikák, cardinality estimate, scan- és join-stratégiák, cost model, `EXPLAIN (ANALYZE, BUFFERS)` és paraméterezés.
- **Előfeltételek:** `DATA-SQL-QUERYING`, `DATA-INDEXING`.
- **Kapcsolatok:** `DATA-EF-QUERY-EXECUTION`, observability és teljesítmény a 7. pillérben.
- **Célbizonyíték:** lassú lekérdezés tervének értelmezése, a szűk keresztmetszet méréssel történő azonosítása és igazolt javítása.

### `DATA-TRANSACTIONS` — Tranzakciók és ACID

- **Tartalom:** tranzakcióhatár, atomicity, consistency, isolation, durability, commit, rollback, savepoint és az üzleti művelethez illeszkedő tranzakcióméret.
- **Előfeltételek:** `DATA-INTEGRITY-CONSTRAINTS`, `DATA-STORAGE-ENGINE`.
- **Kapcsolatok:** `DATA-CONCURRENCY-MVCC`, hibakezelés az 1–2. pillérben.
- **Célbizonyíték:** több lépéses adatváltoztatás helyes tranzakcióba szervezése, rollback igazolása és túl tág vagy túl szűk tranzakcióhatár hibájának felismerése.

### `DATA-CONCURRENCY-MVCC` — Izoláció, MVCC, zárolás és holtpont

- **Tartalom:** konkurens tranzakciók, isolation levelek, dirty/non-repeatable/phantom jelenségek, lost update, PostgreSQL MVCC, row/table lock, optimistic concurrency és deadlock.
- **Előfeltételek:** `DATA-TRANSACTIONS`.
- **Kapcsolatok:** konkurencia a 4. pillérben, elosztott konzisztencia a 8. pillérben.
- **Célbizonyíték:** két konkurens sessionnel izolációs vagy zárolási kísérlet végrehajtása, a megfigyelt jelenség magyarázata és megfelelő védekezés választása.

### `DATA-CONNECTIONS-POOLING` — Kapcsolatok, sessionök és connection pooling

- **Tartalom:** kapcsolatlétesítés költsége, session state, pool, lease/release, maximális poolméret, timeout, connection leak és adatbázis-kapacitás.
- **Előfeltételek:** alapvető kliens–szerver és processmodell.
- **Kapcsolatok:** `DATA-NET-ACCESS`, `WEB-ASYNC-CANCELLATION`, hálózatok a 4. pillérben.
- **Célbizonyíték:** poolbeállítás és életciklus helyes használata, kimerülés vagy leak reprodukálása/diagnózisa és a kapacitási trade-off magyarázata.

### `DATA-POSTGRESQL-FOUNDATIONS` — PostgreSQL használat és diagnosztika

- **Tartalom:** cluster, database, schema, role, `search_path`, catalogok, `psql`, alapvető konfiguráció és introspekció, PostgreSQL-típusok és provider-specifikus viselkedés.
- **Előfeltételek:** `DATA-RELATIONAL-MODEL`, `DATA-SQL-QUERYING`.
- **Kapcsolatok:** `DATA-STORAGE-ENGINE`, `DATA-QUERY-PLANNING`, production adatbázis-üzemeltetés a 7. pillérben.
- **Célbizonyíték:** séma és futó lekérdezések közvetlen vizsgálata, gyakori elérési vagy névfeloldási hiba diagnózisa és provider-specifikus döntés indoklása.

### `DATA-NET-ACCESS` — .NET adat-hozzáférés és providerhatár

- **Tartalom:** ADO.NET absztrakciók, provider, connection, command, parameter, reader, async I/O, tranzakció és biztonságos paraméterezés.
- **Előfeltételek:** `DATA-SQL-QUERYING`, `DATA-CONNECTIONS-POOLING`; `DOTNET-ASYNC-TASKS`.
- **Kapcsolatok:** `DATA-EF-MAPPING-TRACKING`, SQL injection a 6. pillérben.
- **Célbizonyíték:** paraméterezett lekérdezés és tranzakció megvalósítása közvetlen providerrel, életciklusának helyes kezelése és egy connection/reader hiba diagnózisa.

### `DATA-EF-MAPPING-TRACKING` — EF Core modellezés és change tracking

- **Tartalom:** `DbContext`, entity state, identity map, tracking/no-tracking, relationship mapping, Fluent API, value generation, unit of work és `SaveChanges`.
- **Előfeltételek:** `DATA-SCHEMA-DESIGN`, `DATA-INTEGRITY-CONSTRAINTS`, objektummodell az 1. pillérben.
- **Kapcsolatok:** `DATA-EF-QUERY-EXECUTION`, `DATA-SCHEMA-EVOLUTION`, DI és scope a 2. pillérben.
- **Célbizonyíték:** relációs modell tudatos EF Core-leképezése, entity state megfigyelése és trackingből vagy hibás kapcsolatkonfigurációból eredő probléma diagnózisa.

### `DATA-EF-QUERY-EXECUTION` — EF Core lekérdezésfordítás és teljesítmény

- **Tartalom:** `IQueryable<T>`, expression tree, providerfordítás, materializálás, projection, eager/explicit/lazy loading, N+1, split query, compiled query és generált SQL vizsgálata.
- **Előfeltételek:** `DATA-SQL-QUERYING`, `DATA-QUERY-PLANNING`, `DOTNET-LINQ-EXECUTION`.
- **Kapcsolatok:** `DATA-EF-MAPPING-TRACKING`, `WEB-OBSERVABILITY-PERFORMANCE`.
- **Célbizonyíték:** kompozit EF Core-lekérdezés generált SQL-jének és tervének ellenőrzése, N+1 vagy túlzott materializálás felismerése és méréssel igazolt javítása.

### `DATA-SCHEMA-EVOLUTION` — Migrációk és biztonságos sémafejlődés

- **Tartalom:** EF Core migration, model snapshot, idempotens script, expand/contract, adat-visszatöltés, backward compatibility, deploysorrend, rollback és adatvesztési kockázat.
- **Előfeltételek:** `DATA-INTEGRITY-CONSTRAINTS`, `DATA-EF-MAPPING-TRACKING`.
- **Kapcsolatok:** API-evolúció a 2. pillérben, CI/CD és release engineering a 7. pillérben.
- **Célbizonyíték:** nem triviális sémaváltozás biztonságos megtervezése és tesztelése, a generált SQL ellenőrzése és a rollout/rollback kompromisszumainak magyarázata.

## Kapcsolat a jelenlegi projektekkel

- **MovieExplorer:** elsődleges korábbi bizonyítékot ad a relációs modellezés, constraint-ek, PostgreSQL, SQL JOIN-ok, tranzakciós import, EF Core mapping/query és migrációk területén. Az index- és query planner mélyítés a projekt tervezett, de még nem teljesített szakasza.
- **Backend Foundations:** jelenlegi adatbázis nélküli szelete tudatosan nem bővíti ezt a pillért.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
