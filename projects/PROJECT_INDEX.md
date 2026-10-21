# Gyakorlóprojektek indexe

Utolsó frissítés: 2026-10-21

Ez a fájl csak összeköti a globális tanulási állapotot a valós projektekkel. A projektek részletes előzményeit és döntéseit nem másolja ide.

## Aktív projekt

### DataAccessLab

- **Repository:** [`projects/data-access-lab/`](data-access-lab/).
- **Projektspecifikus állapot és terv:** [MENTORING_STATE.md](data-access-lab/MENTORING_STATE.md).
- **Indítás és aktuális feladat:** [README.md](data-access-lab/README.md).
- **Szerep:** a relációs modell, SQL, PostgreSQL és EF Core alapjainak újbóli gyakorlása kis, új kontextusban.
- **Elsődleges témák:** `DATA-EF-MAPPING-TRACKING`, `DATA-INTEGRITY-CONSTRAINTS`, `DATA-TRANSACTIONS`; a relációs, SQL- és lekérdezési előzmény a projektállapotban szerepel.
- **Állapot:** aktív; M1–M4 `Complete`, M5 — Tranzakció és hibadiagnózis `Active`. A POST/PATCH és a tracking-kísérlet után a sikeres és hibás tranzakciós ág elkészült; az új constraint-hiba önálló diagnózisa következik. A MovieExplorer szüneteltetett marad.

## Lezárt projektek

### AsyncHttpLab

- **Repository:** [`projects/async-http-lab/`](async-http-lab/).
- **Projektspecifikus állapot és elfogadott terv:** [MENTORING_STATE.md](async-http-lab/MENTORING_STATE.md).
- **Indítás és feladatleírás:** [README.md](async-http-lab/README.md).
- **Szerep:** aszinkron HTTP I/O, szálhasználat, több művelet, cancellation és timeout egy helyi lassú függőséggel.
- **Elsődleges témák:** `WEB-ASYNC-CANCELLATION`, `DOTNET-ASYNC-TASKS`.
- **Lezárás:** 2026-10-12, M1–M4 `Complete`, exit gate teljesült. A tanulói kód, a futási próbák és a saját záró magyarázat elfogadva; a vezetett javítások miatt a mélység L1 / Practiced marad. A bizonyítékkorlátok a projektállapotban.
- **Állapot:** lezárt alapozó labor; a következő aktív projekt a DataAccessLab.

### ConfigurationDiLab

- **Repository:** [`projects/configuration-di-lab/`](configuration-di-lab/).
- **Projektspecifikus állapot:** [MENTORING_STATE.md](configuration-di-lab/MENTORING_STATE.md); [lezárási összefoglaló](configuration-di-lab/TASKS.md).
- **Szerep:** constructor injection, DI container/lifetime, configuration/options és hibadiagnózis az ismert szervizköltség-számítással.
- **Elsődleges témák:** `WEB-DI-CONFIGURATION`, `DOTNET-DI-COMPOSITION`.
- **Lezárás:** 2026-10-07, M1–M4 `Complete`, alapozó szelet elfogadva. Scoped/transient probe HTTP-próbával igazolt; singleton helyes következtetése felhasználói kérésre elfogadva a feladat teljesítéséhez. Üres konfigurációs hiba és captive dependency reprodukció/javítás, végső regresszió `9/9`.
- **Bizonyítékkorlát:** env override és snapshot reload tanulói sikerjelzés; a végső magyarázat vezetett pontosítást igényelt. L1 / Practiced marad, részletek a projektállapotban.
- **Állapot:** lezárt alapozó labor; az utána következő AsyncHttpLab is lezárult 2026-10-12-én.

### API Contract Testing

- **Repository:** [`projects/api-contract-testing/`](api-contract-testing/).
- **Projektspecifikus állapot:** [`projects/api-contract-testing/MENTORING_STATE.md`](api-contract-testing/MENTORING_STATE.md).
- **Szerep a tantervben:** egy kis HTTP API-szerződés tudatos megtervezése, minimális implementációja, valamint sikeres, hibás és határeseteinek `WebApplicationFactory`- és `HttpClient`-alapú integration tesztelése.
- **Elsődlegesen kapcsolódó témák:** `WEB-API-DESIGN-CONTRACTS`, `WEB-API-TESTING`, `WEB-RESPONSES-SERIALIZATION`, `WEB-ERROR-HANDLING`.
- **Mérnöki keresztkapcsolat:** `SE-TESTING-FUNDAMENTALS`; későbbi mélyítés: `SE-TEST-STRATEGY`.
- **Lezárás oka:** `M1`–`M4` teljesült, a teljes HTTP contractsuite `5/5` zöld. A felhasználó kérésére az `M5` mélyebb tesztelési kísérlete `Deferred`; az exit gate a rövidebb tanulási cél szerint teljesült.
- **Állapot:** lezárt.

### Backend Foundations

- **Repository:** [`projects/backend-foundations/`](backend-foundations/).
- **Projektspecifikus állapot:** [`projects/backend-foundations/MENTORING_STATE.md`](backend-foundations/MENTORING_STATE.md).
- **Szerep a tantervben:** a .NET build- és futási modell, process, Kestrel, port, HTTP request lifecycle, middleware és endpointok összekapcsolása egy minimális ASP.NET Core alkalmazásban.
- **Elsődlegesen kapcsolódó témák:** `WEB-HTTP-SEMANTICS`, `WEB-REQUEST-LIFECYCLE`, `WEB-ROUTING-ENDPOINTS`, `WEB-BINDING-VALIDATION`, `WEB-MIDDLEWARE-PIPELINE`, `WEB-RESPONSES-SERIALIZATION`, `WEB-ERROR-HANDLING`; keresztkapcsolatok: `DOTNET-BUILD-RUNTIME`, `DOTNET-DELEGATES-EVENTS`, `DOTNET-EXCEPTIONS-RESOURCES`, `DOTNET-ASYNC-TASKS`, `SYSTEMS-PROCESS-PORT`.
- **Mérnöki keresztkapcsolatok:** `SE-REQUIREMENTS-USE-CASES`, `SE-DEBUGGING-DIAGNOSTICS`, `SE-TESTING-FUNDAMENTALS`; security-határok: `SEC-INPUT-DATA-SECURITY`, `SEC-AUDIT-INCIDENT-RESPONSE`.
- **Production keresztkapcsolatok:** `OPS-PROCESS-SERVICE-MANAGEMENT`, `OPS-STRUCTURED-LOGGING`, `OPS-HEALTH-MONITORING-ALERTING`; az elosztott témák közül csak a `DIST-FAILURE-MODELS` és `DIST-COMMUNICATION-CONTRACTS` kliens–szerver határa érintett.
- **Lezárás oka:** az M1–M6 és az exit gate ellenőrzött bizonyíték alapján teljesült; a további protokoll- és runtime-mélyítések a curriculum megfelelő pontjain maradnak nyitva.
- **Állapot:** lezárt.

## Szüneteltetett projektek

### MovieExplorer

- **Repository:** `C:\Users\Lenovo T14\repos\sajat\MovieExplorer`
- **Projektspecifikus állapot:** `C:\Users\Lenovo T14\repos\sajat\MovieExplorer\MENTORING_STATE.md`
- **Szerep a tantervben:** controlleres ASP.NET Core Web API, PostgreSQL, relációs adatmodellezés, nyers SQL, LINQ és EF Core gyakorlása valós adathalmazon.
- **Elsődlegesen kapcsolódó témák:** `DATA-RELATIONAL-MODEL`, `DATA-SQL-QUERYING`, `DATA-SCHEMA-DESIGN`, `DATA-INTEGRITY-CONSTRAINTS`, `DATA-INDEXING`, `DATA-TRANSACTIONS`, `DATA-POSTGRESQL-FOUNDATIONS`, `DATA-NET-ACCESS`, `DATA-EF-MAPPING-TRACKING`, `DATA-EF-QUERY-EXECUTION`, `DATA-SCHEMA-EVOLUTION`.
- **Kapcsolódó 1. pillérbeli témák:** `CSHARP-LANGUAGE-FUNDAMENTALS`, `DOTNET-TYPE-SYSTEM`, `CSHARP-OBJECT-MODEL`, `DOTNET-GENERICS`, `DOTNET-COLLECTIONS`, `DOTNET-LINQ-EXECUTION`, `DOTNET-IO-STREAMS`, `DOTNET-ASYNC-TASKS`, `DOTNET-DI-COMPOSITION`.
- **Mérnöki keresztkapcsolatok:** `SE-REQUIREMENTS-USE-CASES`, `SE-DEBUGGING-DIAGNOSTICS`, `SE-MODULARITY-COUPLING`, `SE-ARCHITECTURE-BOUNDARIES`, `SE-DOMAIN-MODELING-DDD`.
- **Security-helyzet:** `SEC-SECRETS-KEY-MANAGEMENT` és `SEC-INPUT-DATA-SECURITY` részlegesen érintett; `SEC-AUTHENTICATION`, `SEC-AUTHORIZATION`, `SEC-TOKENS-JWT`, `SEC-OAUTH2` és `SEC-OPENID-CONNECT` csak későbbre tervezett, még nem bizonyított témák.
- **Infrastructure-helyzet:** `OPS-CONTAINER-IMAGES`, `OPS-CONTAINER-RUNTIME-COMPOSE`, `OPS-CONTAINER-NETWORK-STORAGE` és `OPS-ARTIFACT-CONFIG-SECRETS` részlegesen érintett a PostgreSQL Compose-környezetben; CI/CD és production deployment még nincs.
- **Megőrzött bizonyítékok:** működő API-végpontok, relációs modell, PostgreSQL-adatbázis, IMDb-import, nyers SQL JOIN-ok és EF Core-lekérdezések.
- **Szüneteltetés oka:** a tanulási út 2026-09-14-től koherens Backend Foundations alapozással indul újra.
- **Állapot:** szüneteltetett.

## Projektállapotok

Új projekt csak akkor kerüljön ide, amikor konkrét tanulási szerepet kap. Egy projekt lehet:

- **tervezett:** a célja már ismert, de a repository még nem jött létre;
- **aktív:** az aktuális tanulási szelet végrehajtási helye;
- **támogató:** kisebb kísérletet vagy összehasonlítást szolgál;
- **szüneteltetett:** később folytatható;
- **lezárt:** már nem aktív, de bizonyítékot szolgáltat korábbi tanulásra.

Egyszerre lehetőleg csak egy aktív fő projekt legyen.
