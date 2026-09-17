# Gyakorlóprojektek indexe

Utolsó frissítés: 2026-09-17

Ez a fájl csak összeköti a globális tanulási állapotot a valós projektekkel. A projektek részletes előzményeit és döntéseit nem másolja ide.

## Aktív projekt

### Backend Foundations

- **Repository:** `C:\Users\Lenovo T14\repos\sajat\BackendFoundations`.
- **Projektspecifikus állapot:** `C:\Users\Lenovo T14\repos\sajat\BackendFoundations\MENTORING_STATE.md`.
- **Szerep a tantervben:** a .NET build- és futási modell, process, Kestrel, port, HTTP request lifecycle, middleware és endpointok összekapcsolása egy minimális ASP.NET Core alkalmazásban.
- **Elsődlegesen kapcsolódó témák:** `WEB-HTTP-SEMANTICS`, `WEB-REQUEST-LIFECYCLE`, `WEB-ROUTING-ENDPOINTS`, `WEB-BINDING-VALIDATION`, `WEB-MIDDLEWARE-PIPELINE`, `WEB-RESPONSES-SERIALIZATION`, `WEB-ERROR-HANDLING`; keresztkapcsolatok: `DOTNET-BUILD-RUNTIME`, `DOTNET-DELEGATES-EVENTS`, `DOTNET-EXCEPTIONS-RESOURCES`, `DOTNET-ASYNC-TASKS`, `SYSTEMS-PROCESS-PORT`.
- **Mérnöki keresztkapcsolatok:** `SE-REQUIREMENTS-USE-CASES`, `SE-DEBUGGING-DIAGNOSTICS`, `SE-TESTING-FUNDAMENTALS`; security-határok: `SEC-INPUT-DATA-SECURITY`, `SEC-AUDIT-INCIDENT-RESPONSE`.
- **Production keresztkapcsolatok:** `OPS-PROCESS-SERVICE-MANAGEMENT`, `OPS-STRUCTURED-LOGGING`, `OPS-HEALTH-MONITORING-ALERTING`; az elosztott témák közül csak a `DIST-FAILURE-MODELS` és `DIST-COMMUNICATION-CONTRACTS` kliens–szerver határa érintett.
- **Állapot:** aktív.

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
