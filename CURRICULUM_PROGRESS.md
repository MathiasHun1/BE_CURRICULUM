# Teljes curriculum — haladási nyilvántartás

Utolsó frissítés: 2026-09-17

## Szerep és értelmezés

Ez a fájl a teljes curriculum változó haladási állapotának forrása. A `LEARNING_STATE.md` csak az aktuális 3–5 fókusztémát és az egyetlen következő lépést tartalmazza; a projektek `MENTORING_STATE.md` fájljai őrzik a részletes végrehajtási bizonyítékot.

- **Unassessed:** még nem történt célzott felmérés; nem állítja, hogy nincs korábbi tudás.
- **L0:** történt érintés vagy felmérés, de még nincs érdemi, ellenőrzött tudásbizonyíték.
- **L1–L3:** a curriculum tudásmélységi definíciói szerinti bizonyított szint.
- **Not Started / Learning / Practiced / Confident:** a tanulási állapot; nem helyettesíti a tudásmélységet.

Nem számítunk teljes curriculum-százalékot. A haladást pillérenként, altémánként, célmélységhez és bizonyítékhoz viszonyítva értékeljük.

## Pillérszintű lefedettség

| Pillér | Részletes bontás | Jelenlegi helyzet |
|---|---|---|
| 1. Programozás és .NET | [Részletezve](curriculum/01-programming-dotnet.md) | A Backend Foundations és MovieExplorer első, konzervatív bizonyítéktérképe elkészült. |
| 2. Web- és backendfejlesztés | [Részletezve](curriculum/02-web-backend.md) | Aktív elsődleges pillér; az első bizonyítéktérkép elkészült. |
| 3. Adatok és adatbázisok | [Részletezve](curriculum/03-data-databases.md) | A MovieExplorer SQL/PostgreSQL/EF Core bizonyítékainak első, konzervatív térképe elkészült. |
| 4. Számítógépes rendszerek | [Részletezve](curriculum/04-computer-systems.md) | A Backend Foundations process/port/socket bizonyítékainak első, konzervatív térképe elkészült. |
| 5. Szoftverfejlesztés és architektúra | [Részletezve](curriculum/05-software-engineering-architecture.md) | A két projekt mérnöki és architekturális bizonyítékainak első, konzervatív térképe elkészült. |
| 6. Biztonság és identitáskezelés | [Részletezve](curriculum/06-security-identity.md) | A kevés meglévő security-határ rögzítve; az identitás- és hozzáféréskezelési témák még felméretlenek. |
| 7. Infrastruktúra és production engineering | [Részletezve](curriculum/07-infrastructure-production.md) | A lokális process-, logging-, konfiguráció- és Docker Compose-bizonyítékok első, konzervatív térképe elkészült. |
| 8. Elosztott rendszerek és rendszertervezés | [Részletezve](curriculum/08-distributed-systems-design.md) | A stabil tématérkép elkészült; célzott elosztottrendszer-bizonyíték még gyakorlatilag nincs. |
| Keresztmetszeti számítástudományi alapok | `Planned` | Nincs egységesen felmérve. |

## 1. pillér — Programozás és .NET

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `CSHARP-LANGUAGE-FUNDAMENTALS` | L2 | L1 | Practiced | Több saját endpoint és vezérlési ág készült, a fordítóhibák javítása és lambda használata ellenőrizve. Nyitott: szélesebb, új kontextusú önálló C# diagnózis. |
| `DOTNET-TYPE-SYSTEM` | L3 | L0 | Learning | `int`, `Guid`, nullable bemenet, `required`, `List<T>`/`IEnumerable<T>` és handler-visszatérési típusok gyakorlati érintése. A value/reference, konverzió és nullability mentális modell nincs célzottan felmérve. |
| `CSHARP-OBJECT-MODEL` | L2 | L0 | Learning | MovieExplorer entitások, propertyk, controller és `DbContext` használata megtörtént; absztrakciós döntések önálló magyarázata még nincs értékelve. |
| `DOTNET-GENERICS` | L2 | L0 | Learning | `List<T>`, `IEnumerable<T>`, `Task<T>`, `ILogger<T>` és framework API-k használata megtörtént; constraint és variance nincs felmérve. |
| `DOTNET-COLLECTIONS` | L2 | L0 | Learning | Listák, tömbök és nagy import-adathalmaz feldolgozása megjelent a projektekben; adatszerkezet-választás és komplexitás nincs célzottan értékelve. |
| `DOTNET-DELEGATES-EVENTS` | L2 | L1 | Learning | A tanuló a delegate-et típusos, később meghívható metódushivatkozásként azonosította; Minimal API lambdát használt. Event, closure és multicast viselkedés nyitott. |
| `DOTNET-EXCEPTIONS-RESOURCES` | L2 | L1 | Practiced | Kivételterjedés, `await` utáni újradobás, `finally`, `throw;` és a válasz kontra exception határ saját magyarázattal és futási próbával ellenőrizve. `IDisposable`/`IAsyncDisposable` nyitott. |
| `DOTNET-LINQ-EXECUTION` | L2 | L1 | Practiced | MovieExplorerben többtáblás LINQ-lekérdezés, deferred execution és `ToListAsync()` materializálás áttekintve. A kompozíciós lépés félbemaradt, ezért az önálló alkalmazás még nem L2. |
| `DOTNET-BUILD-RUNTIME` | L2 | L1 | Practiced | SDK/runtime szerep, `net10.0` build artifact, CIL/JIT alapmodell és build/futtatás különbsége ellenőrizve. Nyitott: assembly metadata, dependency resolution és mélyebb runtime diagnózis. |
| `DOTNET-MEMORY-GC` | L3 | — | Unassessed | Még nincs célzott bizonyíték. |
| `DOTNET-IO-STREAMS` | L2 | L0 | Learning | MovieExplorer importer tömörített TSV-t streamelve dolgoz fel, a Backend Foundations response body/flush kísérlete elkészült. A buffering, encoding és disposal modell nincs önállóan felmérve. |
| `DOTNET-ASYNC-TASKS` | L3 | L1 | Practiced | Aszinkron endpointok és EF Core műveletek, `Task`, `await`, kivételterjedés és részleges response gyakorlati ellenőrzése. Cancellation, `WhenAll` és sync-over-async nyitott. |
| `DOTNET-CONCURRENCY-THREADING` | L3 | — | Unassessed | Az async és process témák nem bizonyítják a szálkezelés, ThreadPool és szinkronizáció ismeretét. |
| `DOTNET-REFLECTION-METADATA` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `DOTNET-DI-COMPOSITION` | L2 | L1 | Learning | MovieExplorer `AppDbContext` constructor injection és service registration; Backend Foundations options-konfiguráció. Lifetime-, scope- és captive dependency diagnózis nyitott. |

### Bizonyítékforrások

- [Aktív globális állapot](LEARNING_STATE.md)
- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## 2. pillér — Web- és backendfejlesztés

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `WEB-HTTP-SEMANTICS` | L3 | L1 | Practiced | `200`, `400`, `404`, `405`, `418`, `500` futási próbák; a kliens- és szerverhibák alapvető magyarázata. Nyitott: safe/idempotent szemantika, cache, protokollmélység. |
| `WEB-REQUEST-LIFECYCLE` | L2 | L1 | Practiced | Kestrel → middleware → endpoint → response útvonal több ága ellenőrizve; `Response.HasStarted` megfigyelés. Az önálló teljes lifecycle-magyarázat az aktív `M6` gate része. |
| `WEB-ROUTING-ENDPOINTS` | L2 | L1 | Practiced | `/`, `/health`, `/greet/{name}`, `/divide`; `404`/`405` különbség és `MapGet` szerepe ellenőrizve. Nyitott: constraint, metadata, route-konfliktus. |
| `WEB-BINDING-VALIDATION` | L2 | L1 | Practiced | Route/query binding, hiányzó és hibás `int` automatikus `400`, nulla osztó explicit `400`; handler előtti binding felismerve. Nyitott: body/header/form binding és összetettebb validáció. |
| `WEB-MIDDLEWARE-PIPELINE` | L2 | L1 | Practiced | Inline logging middleware, előtte/utána ág, short-circuit `418`, exception middleware és sorrend gyakorlati ellenőrzése. Nyitott: branching és új kontextusú önálló alkalmazás. |
| `WEB-RESPONSES-SERIALIZATION` | L2 | L0 | Learning | JSON és text válaszok, `IResult`, `Results.Ok`/`BadRequest` használata megtörtént. A szerializációs és content-negotiation mentális modell még nincs felmérve. |
| `WEB-ERROR-HANDLING` | L2 | L1 | Practiced | `/fail`, kontrollált JSON `500`, `Response.HasStarted`, explicit és automatikus `400`; a `4xx`/`5xx` felelősségi különbsége saját szavakkal megfogalmazva. |
| `WEB-API-DESIGN-CONTRACTS` | L2 | — | Unassessed | Vannak működő endpointok, de önálló szerződéstervezési bizonyíték még nincs értékelve. |
| `WEB-DI-CONFIGURATION` | L2 | L0 | Learning | `RouteHandlerOptions` konfiguráció használata megtörtént. DI lifetime-ok, providerek és options pattern még nem kerültek felmérésre. |
| `WEB-ASYNC-CANCELLATION` | L2 | L1 | Learning | Aszinkron body-írás, flush és kivételterjedés gyakorlati érintése; az `await` hibás `Task`-ot újradobó működése felidézve. Cancellation és backpressure nyitott. |
| `WEB-API-TESTING` | L2 | L0 | Learning | Kézi `curl` regressziós mátrix készült; automatizált integration teszt még nincs. |
| `WEB-SECURITY-INTEGRATION` | L2 | — | Unassessed | Ebben a tanulási szeletben tudatosan nincs autentikáció vagy authorization. |
| `WEB-BACKGROUND-PROCESSING` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `WEB-API-EVOLUTION` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `WEB-OBSERVABILITY-PERFORMANCE` | L2 | L0 | Learning | Strukturált request/response logging készült; metrics, tracing és mérésalapú teljesítményelemzés még nyitott. |

### Bizonyítékforrások

- [Aktív globális állapot](LEARNING_STATE.md)
- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## 3. pillér — Adatok és adatbázisok

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `DATA-RELATIONAL-MODEL` | L3 | L1 | Practiced | A MovieExplorer ötentitásos modellje `1:N` és `N:M` kapcsolatokkal, kapcsolótáblákkal, összetett kulcsokkal és `NULL` döntésekkel elkészült és PostgreSQLben létrejött. A relációs algebra és új kontextusú önálló modellezés még nyitott. |
| `DATA-SQL-QUERYING` | L3 | L1 | Practiced | Háromtáblás JOIN-ok, szűrés, rendezés, `LIMIT`, `COUNT(*)`, window count és `ILIKE` valós adatokon ellenőrizve. Aggregációs, CTE- és összetettebb `NULL`-logikai feladat még nyitott. |
| `DATA-SCHEMA-DESIGN` | L3 | L1 | Practiced | A minimális MovieExplorer-séma, a belső/külső azonosítók és a kapcsolótáblák döntései elkészültek. Normalizálási formák és denormalizálási trade-offok még nincsenek célzottan felmérve. |
| `DATA-INTEGRITY-CONSTRAINTS` | L3 | L1 | Practiced | Primary/foreign/composite key, egyedi index, nullability és cascade szabályok migrációval létrehozva és közvetlenül ellenőrizve. `CHECK` constraint és összetettebb integritási hibák nyitottak. |
| `DATA-STORAGE-ENGINE` | L2 | — | Unassessed | A PostgreSQL fizikai tárolási, page-, WAL- és bufferkezelési modelljére még nincs célzott bizonyíték. |
| `DATA-INDEXING` | L3 | L1 | Practiced | Egyedi és összetett indexek létrejöttek; a uniqueness és az oszlopsorrend szerepe áttekintésre került. Az indexválasztás végrehajtási tervvel történő igazolása még hiányzik. |
| `DATA-QUERY-PLANNING` | L3 | — | Unassessed | A projekt tervezett teljesítménylépése, de `EXPLAIN ANALYZE` vagy önálló tervdiagnózis még nem készült. |
| `DATA-TRANSACTIONS` | L3 | L1 | Practiced | Az importer tranzakcióban upsertel, a `--dry-run` ág rollbackje end-to-end próbával igazolt. Az izolációs és tranzakcióhatár-trade-offok mélyebb felmérése nyitott. |
| `DATA-CONCURRENCY-MVCC` | L3 | — | Unassessed | Nincs célzott konkurens session-, izolációs, MVCC-, lock- vagy deadlock-kísérlet. |
| `DATA-CONNECTIONS-POOLING` | L2 | L0 | Learning | Connection string, Npgsql provider és scoped `DbContext` használata megtörtént; a fizikai/logikai kapcsolat, pooléletciklus és kimerülés nincs felmérve. |
| `DATA-POSTGRESQL-FOUNDATIONS` | L2 | L1 | Practiced | Helyi PostgreSQL 16, schema/`search_path`, migrációtörténet és catalog-alapú közvetlen sémaellenőrzés használata megtörtént. A rendszeres diagnosztika és konfiguráció még nyitott. |
| `DATA-NET-ACCESS` | L2 | L0 | Learning | Az alkalmazások az Npgsql provideren keresztül, EF Core-ral érik el a PostgreSQLt, és tranzakciót is használnak. Közvetlen ADO.NET connection/command/reader használat és életciklus-diagnózis még nem történt. |
| `DATA-EF-MAPPING-TRACKING` | L2 | L1 | Practiced | `DbContext`, `DbSet`, Fluent API-kapcsolatok, kulcsok és indexek elkészültek. A change tracker állapotai, tracking/no-tracking és hibamódjaik nyitottak. |
| `DATA-EF-QUERY-EXECUTION` | L3 | L1 | Practiced | `FindAsync`, `EF.Functions.ILike`, rendezés, korlátozás, többtáblás LINQ és `ToListAsync()` materializálás készült. A kompozíció befejezése, generált SQL/terv és N+1 diagnózis nyitott. |
| `DATA-SCHEMA-EVOLUTION` | L2 | L1 | Practiced | Több EF Core migráció létrehozása, eltávolítása, újragenerálása, alkalmazása, snapshotja és history táblája ellenőrizve lett. A zero-downtime expand/contract rollout még nyitott. |

### Bizonyítékforrások

- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A MovieExplorer tényleges entitás-, `DbContext`-, migration-, importer- és controllerforrásai.

## 4. pillér — Számítógépes rendszerek

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `SYSTEMS-DATA-REPRESENTATION` | L2 | L0 | Learning | TSV-, JSON-, text- és bináris build artifactok használata megtörtént, de byteszintű ábrázolás vagy encodinghiba célzott diagnózisa még nincs. |
| `SYSTEMS-CPU-EXECUTION` | L2 | L0 | Learning | A CIL/JIT és gépi kód alaplánca áttekintésre került; CPU-utasítás, regiszter és CPU-bound viselkedés nem kapott önálló gyakorlati bizonyítékot. |
| `SYSTEMS-MEMORY-HIERARCHY` | L2 | — | Unassessed | Cache-, RAM-, lokalitás- vagy memória-sávszélesség kísérlet még nem történt. |
| `SYSTEMS-VIRTUAL-MEMORY` | L2 | — | Unassessed | Address space, page, stack/heap és operációs rendszerbeli memóriahasználat még nincs célzottan felmérve. |
| `SYSTEMS-PROCESS-PORT` | L2 | L1 | Practiced | A futó `BackendFoundations` process, PID, Kestrel és az IPv4/IPv6 `5003` figyelő socket ténylegesen ellenőrizve; a fájl és process különbségét a tanuló elmagyarázta. |
| `SYSTEMS-THREADS-SCHEDULING` | L3 | — | Unassessed | Az async endpointok nem bizonyítják a thread, scheduler, ThreadPool vagy starvation megértését. |
| `SYSTEMS-KERNEL-SYSCALLS` | L2 | — | Unassessed | User/kernel mode és system call szintű vizsgálat még nem történt. |
| `SYSTEMS-FILESYSTEMS-STORAGE` | L2 | L0 | Learning | A MovieExplorer importer fájlt streamel és a fájl/process különbség felidézésre került; filesystem-, permission-, buffering- és durability-modell nincs felmérve. |
| `SYSTEMS-IO-MODELS` | L3 | L0 | Learning | Aszinkron fájl-, adatbázis- és HTTP body I/O, valamint explicit flush előfordult; az operációs rendszer blocking/non-blocking modellje és backpressure még nincs bizonyítva. |
| `SYSTEMS-CONCURRENCY-SYNC` | L3 | — | Unassessed | Race condition, szinkronizációs primitív és deadlock területén nincs célzott gyakorlati bizonyíték. |
| `SYSTEMS-NETWORK-STACK` | L2 | L0 | Learning | Loopback és IPv4/IPv6 listening endpoint megfigyelése megtörtént; IP-, subnet-, routing-, NAT- és MTU-modell nincs felmérve. |
| `SYSTEMS-TRANSPORT-SOCKETS` | L3 | L0 | Learning | Figyelő socket és megszakadt chunked HTTP-átvitel kliensoldali tünete megfigyelhető volt. TCP-kapcsolatállapotok és a `curl: (18)` transport/protokoll oka önállóan még nem bizonyított. |
| `SYSTEMS-DNS` | L2 | — | Unassessed | Nincs célzott névfeloldási vagy cache/TTL diagnózis. |
| `SYSTEMS-TLS` | L2 | — | Unassessed | Nincs célzott TLS-handshake- vagy tanúsítványlánc-vizsgálat. |
| `SYSTEMS-PROXIES-GATEWAYS` | L2 | — | Unassessed | Nincs reverse proxy-, forwarding header-, timeout- vagy TLS termination kísérlet. |

### Bizonyítékforrások

- [Aktív globális állapot](LEARNING_STATE.md)
- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## 5. pillér — Szoftverfejlesztés és architektúra

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `SE-REQUIREMENTS-USE-CASES` | L2 | L1 | Practiced | A MovieExplorer use case-ei és API-műveletei, valamint a Backend Foundations mérföldkövei és elfogadási feltételei rögzítve lettek. Új, homályos igény önálló tisztázása még nincs célzottan felmérve. |
| `SE-VERSION-CONTROL-COLLABORATION` | L2 | L0 | Learning | A projektek Git repositoryban vannak, de áttekinthető commit-workflowra, konfliktuskezelésre vagy code review-ra még nincs ellenőrzött bizonyíték. |
| `SE-DEBUGGING-DIAGNOSTICS` | L3 | L1 | Practiced | Buildhibák, stack trace, middleware-sorrend, bindinghiba, process/port és HTTP-ágak bizonyíték-alapú diagnózisa megtörtént. Ismeretlen, többkomponensű hiba önálló végigvitele még nyitott. |
| `SE-CODE-QUALITY-READABILITY` | L2 | L0 | Learning | Kis, fókuszált lépések, formázás és guard jellegű validáció megjelent; önálló olvashatósági refaktor és trade-off-magyarázat még nincs értékelve. |
| `SE-REFACTORING-TECH-DEBT` | L2 | — | Unassessed | Nem történt célzott, teszttel biztosított refaktorálási gyakorlat vagy technikaiadósság-priorizálás. |
| `SE-MODULARITY-COUPLING` | L3 | L0 | Learning | A MovieExplorer API és importer külön projekt, a Backend Foundations pedig tudatosan minimális maradt. Kohézió, csatolás és függőségi irány önálló elemzése még nincs felmérve. |
| `SE-SOLID-DESIGN` | L2 | — | Unassessed | Interface- és DI-használat önmagában nem bizonyítja a SOLID elvek megértését vagy megfelelő alkalmazását. |
| `SE-DESIGN-PATTERNS` | L2 | — | Unassessed | Frameworkminták használata megtörtént, de tervezési minta tudatos kiválasztására és trade-offjának magyarázatára nincs bizonyíték. |
| `SE-TESTING-FUNDAMENTALS` | L3 | L0 | Learning | Kézi sikeres, hibás és regressziós HTTP-próbák készültek; automatizált unit/integration teszt és tesztdizájn még nincs. |
| `SE-TEST-STRATEGY` | L2 | L0 | Learning | A Backend Foundations exit gate-je és kézi regressziós ágai visszajelzési pontokat adnak, de kockázatalapú automatizált tesztportfólió még nem készült. |
| `SE-ARCHITECTURE-BOUNDARIES` | L3 | L0 | Learning | Az API és importer process-/projekthatára, illetve a felesleges rétegek elhalasztása tudatos döntés volt. Komponens- és függőségi határok önálló tervezése még nincs bizonyítva. |
| `SE-LAYERED-CLEAN-ARCHITECTURE` | L2 | — | Unassessed | A projektekben tudatosan nincs teljes réteges vagy Clean Architecture szerkezet; használatára és kritikájára nincs célzott bizonyíték. |
| `SE-DOMAIN-MODELING-DDD` | L2 | L0 | Learning | A MovieExplorer domainentitásai és invariánsai részben kialakultak, de aggregate, bounded context és domainmodell/adatmodell határ nincs felmérve. |
| `SE-MODULAR-MONOLITHS` | L2 | — | Unassessed | Nincs több üzleti modulból álló monolit vagy szolgáltatáshatár-kísérlet. |
| `SE-ARCHITECTURE-DECISIONS` | L3 | L0 | Learning | Több tartós döntés és scope-korlát dokumentálva van, de alternatívákat, minőségi attribútumokat és következményeket összevető önálló ADR még nem készült. |

### Bizonyítékforrások

- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A két projekt tényleges forráskódja, build- és futási eredményei.

## 6. pillér — Biztonság és identitáskezelés

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `SEC-PRINCIPLES-THREAT-MODELING` | L3 | — | Unassessed | Nincs célzott threat model, bizalmi határ- vagy kockázatelemzés. |
| `SEC-CRYPTOGRAPHY` | L2 | — | Unassessed | Nincs célzott kriptográfiai primitívválasztás vagy hibadiagnózis. |
| `SEC-PASSWORDS-CREDENTIALS` | L2 | — | Unassessed | A projektek még nem kezelnek felhasználói jelszavakat vagy credential recovery folyamatot. |
| `SEC-PKI-TLS` | L2 | — | Unassessed | Nincs tanúsítványlánc-, trust store- vagy TLS-bizalmi vizsgálat. |
| `SEC-SECRETS-KEY-MANAGEMENT` | L2 | L0 | Learning | A MovieExplorer fejlesztői adatbázis-jelszava .NET User Secretsben marad és nem kerül Gitbe. Rotáció, vault, lejárat és incidenskezelés nincs felmérve. |
| `SEC-IDENTITY-SESSIONS` | L2 | — | Unassessed | Nincs account-, principal- vagy session-lifecycle megvalósítás. |
| `SEC-AUTHENTICATION` | L2 | — | Unassessed | A tervezett Keycloak-integráció még nem készült el; nincs ellenőrzött authentication flow. |
| `SEC-AUTHORIZATION` | L3 | — | Unassessed | Nincs policy-, role- vagy resource-based authorization megvalósítás és teszt. |
| `SEC-TOKENS-JWT` | L2 | — | Unassessed | Nincs JWT-kibocsátási vagy validációs kísérlet; a későbbi terv nem számít bizonyítéknak. |
| `SEC-OAUTH2` | L2 | — | Unassessed | Nincs OAuth 2.0 flow vagy szereplőmodell gyakorlati bizonyítéka. |
| `SEC-OPENID-CONNECT` | L2 | — | Unassessed | Nincs OpenID Connect discovery-, login-, token- vagy claim-mapping integráció. |
| `SEC-WEB-BROWSER-SECURITY` | L3 | — | Unassessed | CORS, cookie, SameSite, CSRF, XSS vagy CSP területen nincs célzott gyakorlati bizonyíték. |
| `SEC-INPUT-DATA-SECURITY` | L3 | L0 | Learning | Framework binding, explicit validáció, EF Core-os paraméterezés és belső részleteket elrejtő hibaválasz használata megtörtént. Injection, SSRF, path/file és deserialization hibamódok még nincsenek felmérve. |
| `SEC-SUPPLY-CHAIN` | L2 | — | Unassessed | Csomagok használata megtörtént, de provenance-, sérülékenység-, SBOM- vagy update-policy ellenőrzés nem. |
| `SEC-AUDIT-INCIDENT-RESPONSE` | L2 | L0 | Learning | A Backend Foundations a teljes kivételt szerveroldalon naplózza, miközben általános választ ad a kliensnek. Audit trail, abuse detection, riasztás és incidensgyakorlat még nincs. |

### Bizonyítékforrások

- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A tényleges konfiguráció és forráskód, titkok értékének rögzítése nélkül.

## 7. pillér — Infrastruktúra és production engineering

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `OPS-LINUX-SHELL` | L2 | — | Unassessed | A jelenlegi Windows/PowerShell munka nem ad célzott Linux- és POSIX shell-bizonyítékot. |
| `OPS-FILES-PERMISSIONS` | L2 | — | Unassessed | Nincs Linux user/group-, permission-, ACL- vagy service account gyakorlat. |
| `OPS-PROCESS-SERVICE-MANAGEMENT` | L2 | L1 | Practiced | A Backend Foundations processe, PID-je, indulása, leállása és figyelő portja ellenőrizve lett. Service manager, signal, restart policy és graceful shutdown még nyitott. |
| `OPS-CONTAINER-IMAGES` | L2 | L0 | Learning | A MovieExplorer hivatalos `postgres:16-alpine` image-et használ, de saját reprodukálható/multi-stage image build még nem készült. |
| `OPS-CONTAINER-RUNTIME-COMPOSE` | L2 | L0 | Learning | A helyi PostgreSQL Docker Compose service elindult és az alkalmazás elérte. Container lifecycle, resource limit, restart és hibadiagnózis nincs önállóan felmérve. |
| `OPS-CONTAINER-NETWORK-STORAGE` | L2 | L0 | Learning | A Compose publisholt `5432` portot és névvel ellátott `postgres-data` volume-ot használ. Service DNS-, mount- és adat-élettartam-hibák diagnózisa még nincs. |
| `OPS-ARTIFACT-CONFIG-SECRETS` | L2 | L0 | Learning | Build artifact, `appsettings`, environmentváltozók, connection string és .NET User Secrets használata megtörtént. Több környezet azonos artifacttal és production secret injection még nincs bizonyítva. |
| `OPS-CI-BUILD-QUALITY` | L2 | — | Unassessed | Nincs repositoryhoz kötött, tiszta környezetben futó CI pipeline és publikált artifact. |
| `OPS-RELEASE-DEPLOYMENT` | L3 | — | Unassessed | Nincs környezetek közötti promotion, deployment-, rollback- vagy kompatibilitási gyakorlat. |
| `OPS-REVERSE-PROXY-TLS` | L2 | — | Unassessed | Nincs reverse proxy, HTTPS termination vagy forwarding header konfiguráció. |
| `OPS-STRUCTURED-LOGGING` | L2 | L1 | Practiced | A Backend Foundations strukturált request/response és exception logokat készít, amelyekkel több végrehajtási ág ellenőrizve lett. Központi gyűjtés, retention és több process közötti korreláció nyitott. |
| `OPS-METRICS-TRACING` | L2 | — | Unassessed | Nincs alkalmazásmetrika, trace, span vagy OpenTelemetry-instrumentáció. |
| `OPS-HEALTH-MONITORING-ALERTING` | L3 | L0 | Learning | Kézi `/health` endpoint és regressziós ellenőrzés készült; liveness/readiness, monitorozás, SLI/SLO és riasztás még nincs. |
| `OPS-CLOUD-ORCHESTRATION` | L2 | — | Unassessed | Nincs cloud deployment, IAM-, orchestrator-, service discovery- vagy autoscaling gyakorlat. |
| `OPS-RELIABILITY-INCIDENTS-CAPACITY` | L3 | — | Unassessed | Nincs load test, kapacitásmérés, runbook, SLO/error budget vagy incidensgyakorlat. |

### Bizonyítékforrások

- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A MovieExplorer `compose.yaml`, valamint a projektek tényleges konfigurációja, kódja és futási eredményei.

## 8. pillér — Elosztott rendszerek és rendszertervezés

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `DIST-FAILURE-MODELS` | L3 | L0 | Learning | Kliens–szerver hiba, timeoutjellegű tünet és részlegesen elküldött HTTP-válasz megfigyelése megtörtént, de több független komponens részleges hibáinak önálló elemzése még nincs. |
| `DIST-COMMUNICATION-CONTRACTS` | L2 | L0 | Learning | HTTP API-k és kliens–szerver szerződések készültek; szolgáltatások közötti sync/async kommunikáció és kompatibilis contract-evolúció nincs felmérve. |
| `DIST-TIME-ORDERING-IDENTITY` | L2 | — | Unassessed | Nincs clock skew-, ordering-, logical clock- vagy elosztottazonosító-kísérlet. |
| `DIST-CONSISTENCY-CAP` | L3 | — | Unassessed | Nincs replikált adat, hálózati partíció vagy konzisztenciamodell gyakorlati elemzése. |
| `DIST-REPLICATION` | L2 | — | Unassessed | A helyi PostgreSQL egyetlen példánya nem ad replikációs vagy failover-bizonyítékot. |
| `DIST-PARTITIONING` | L2 | — | Unassessed | Nincs shard-, partition key-, hot partition- vagy rebalancing gyakorlat. |
| `DIST-CONSENSUS-COORDINATION` | L2 | — | Unassessed | Nincs leader election-, quorum-, lease-, fencing- vagy konszenzuskísérlet. |
| `DIST-CACHING-CDN` | L3 | — | Unassessed | Nincs alkalmazáscache, distributed cache, CDN vagy invalidációs kísérlet. |
| `DIST-LOAD-BALANCING-SCALING` | L2 | — | Unassessed | Nincs több alkalmazásreplika, load balancer vagy horizontális skálázási próba. |
| `DIST-MESSAGING-STREAMING` | L3 | — | Unassessed | Nincs queue, pub/sub, stream, broker vagy consumer group gyakorlati használat. |
| `DIST-DELIVERY-IDEMPOTENCY` | L3 | — | Unassessed | Nincs duplikált kézbesítési, idempotency key-, deduplication- vagy replay-kísérlet. |
| `DIST-RESILIENCE-CONTROL` | L3 | — | Unassessed | Nincs kontrollált timeout-, retry-, backoff-, circuit breaker-, bulkhead- vagy backpressure-gyakorlat. |
| `DIST-TRANSACTIONS-SAGAS` | L3 | — | Unassessed | A MovieExplorer lokális adatbázis-tranzakciója nem elosztott tranzakció; nincs dual write, outbox, inbox vagy saga bizonyíték. |
| `DIST-OBSERVABILITY` | L2 | — | Unassessed | Nincs több service-en vagy aszinkron hopon átmenő trace és context propagation. |
| `DIST-SYSTEM-DESIGN` | L3 | — | Unassessed | Nincs követelmény-, kapacitás-, adatút- és hibamód-alapú teljes rendszertervezési gyakorlat. |

### Bizonyítékforrások

- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A jelenlegi projektek egyprocesses, illetve egyszerű kliens–adatbázis topológiája; a hiányzó elosztott mechanizmusokat nem tekintjük bizonyítéknak.

## Következő curriculum-karbantartási lépés

Az aktív Backend Foundations `M6` és exit audit után az érintett 1., 2., 4., 5. és 7. pillér bizonyítéktérképét felül kell vizsgálni, majd ki kell jelölni a következő elsődleges tanulási szeletet. Mind a nyolc pillér első stabil bontása elkészült; további curriculum-karbantartás az aktivált témák bizonyítékainak, függőségeinek és célfeladatainak finomítása legyen, ne újabb előzetes témaszaporítás.
