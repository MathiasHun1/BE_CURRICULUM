# Gyakorlóprojektek indexe

Utolsó frissítés: 2026-09-28

Ez a fájl csak összeköti a globális tanulási állapotot a valós projektekkel. A projektek részletes előzményeit és döntéseit nem másolja ide.

## Aktív projekt

### ConfigurationDiLab

- **Repository:** [`projects/configuration-di-lab/`](configuration-di-lab/).
- **Projektspecifikus állapot:** [MENTORING_STATE.md](configuration-di-lab/MENTORING_STATE.md).
- **Aktuális feladat:** [TASKS.md](configuration-di-lab/TASKS.md).
- **Szerep a tantervben:** kézi constructor injection, DI container és lifetime, külső konfiguráció/options és kontrollált hibadiagnózis az ismert szervizköltség-számítással.
- **Elsődlegesen kapcsolódó témák:** `WEB-DI-CONFIGURATION`, `DOTNET-DI-COMPOSITION`.
- **Közvetlen kapcsolatok:** `WEB-API-TESTING`, `WEB-BINDING-VALIDATION`, `WEB-ERROR-HANDLING`.
- **Aktív mérföldkő:** `M1` — kézi constructor injection. A négy mérföldkő és az explicit exit gate a saját állapotfájlban szerepel.
- **Állapot:** aktív; a scaffold agent által előkészítve, a kalkulátor implementációja tanulói feladat.

## Lezárt projektek

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
