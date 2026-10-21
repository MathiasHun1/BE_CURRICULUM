# Teljes curriculum — haladási nyilvántartás

Utolsó frissítés: 2026-10-21

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
| 2. Web- és backendfejlesztés | [Részletezve](curriculum/02-web-backend.md) | Az AsyncHttpLab alapozó szelete lezárult; a webes alapok az új adatszelethez kapcsolódnak. |
| 3. Adatok és adatbázisok | [Részletezve](curriculum/03-data-databases.md) | Aktív elsődleges pillér: a DataAccessLab M1–M4 elkészült, M5 tranzakciós és hibadiagnosztikai szakasza aktív. Az új labor gyakorlati bizonyítékai mellett a konzervatív L1 / Practiced mélység marad. |
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
| `CSHARP-OBJECT-MODEL` | L2 | L0 | Learning | MovieExplorer entitások, propertyk, controller és DbContext korábbi használata. 2026-10-07: a LifeTimeProbe nyilvános mezőből readonly mező, majd csak olvasható auto-property lett, forrással ellenőrizve. Mező/property, rejtett tárolómező és getterenként új GUID különbsége mentor által magyarázott; önálló magyarázat és absztrakciós döntés még hiányzik. |
| `DOTNET-GENERICS` | L2 | L0 | Learning | `List<T>`, `IEnumerable<T>`, `Task<T>`, `ILogger<T>` és framework API-k használata megtörtént; constraint és variance nincs felmérve. |
| `DOTNET-COLLECTIONS` | L2 | L0 | Learning | Korábbi listák, tömbök, import és vezetett OrdinalIgnoreCase-javítás. 2026-10-07: a tanuló helyesen magyarázta, hogy a létező üres dictionary nem null, a foreach törzse nem fut, ezért a Count==0 ellenőrzés külön kell. Adatszerkezet-választás és komplexitás nincs célzottan értékelve; a teljes altéma L1-e nem igazolt. |
| `DOTNET-DELEGATES-EVENTS` | L2 | L1 | Learning | A tanuló a delegate-et típusos, később meghívható metódushivatkozásként azonosította; Minimal API lambdát használt. Event, closure és multicast viselkedés nyitott. |
| `DOTNET-EXCEPTIONS-RESOURCES` | L2 | L1 | Practiced | Korábbi kivételterjedés, finally és throw bizonyíték megmarad. 2026-10-11: AsyncHttpLab using, OperationCanceledException-filter, InnerException/is és HttpRequestException kezelése forrással ellenőrzött. A GC/Dispose különbségét és a factory által kezelt handlerélettartamot a mentor magyarázta. Hibás filter → 500, javítva → 504 mentorpróbával igazolt. 2026-10-12: a tanuló helyesen felidézte a hamis when utáni következő catch vizsgálatát és a filter első feltételének kliensmegszakításkor hamis, timeoutkor igaz értékét. A bontott kliens esetén feltételezett 500-at mentor pontosította. IAsyncDisposable és önálló L2-diagnózis nyitott. |
| `DOTNET-LINQ-EXECUTION` | L2 | L1 | Practiced | MovieExplorerben többtáblás LINQ-lekérdezés, deferred execution és `ToListAsync()` materializálás áttekintve. 2026-10-19: a DataAccessLab GET-ben `Where`, külső/belső `Select` és `FirstOrDefaultAsync` forrással ellenőrzött; a tanuló a végrehajtási pontot és a vetítés célját helyesen felidézte. A beágyazott vetítéshez mentor-kódpélda, a `ToList()` és `LIMIT 1` értelmezéséhez pontosítás kellett; önálló L2-es alkalmazás nincs igazolva. |
| `DOTNET-BUILD-RUNTIME` | L2 | L1 | Practiced | SDK/runtime szerep, `net10.0` build artifact, CIL/JIT alapmodell és build/futtatás különbsége ellenőrizve. Nyitott: assembly metadata, dependency resolution és mélyebb runtime diagnózis. |
| `DOTNET-MEMORY-GC` | L3 | — | Unassessed | Még nincs célzott bizonyíték. |
| `DOTNET-IO-STREAMS` | L2 | L0 | Learning | MovieExplorer importer tömörített TSV-t streamelve dolgoz fel, a Backend Foundations response body/flush kísérlete elkészült. A buffering, encoding és disposal modell nincs önállóan felmérve. |
| `DOTNET-ASYNC-TASKS` | L3 | L1 | Practiced | Korábbi async endpoint/EF Core bizonyíték mellett 2026-10-11: tanulói HttpClient, két sequential és két WhenAll-lal megvárt hívás forrása és buildje ellenőrzött; 6/3 s tanulói mérés. Token továbbadása és timeoutkezelés elkészült. Saját helyes magyarázat: nincs végig lefoglalt szál, állapotmegőrzés szükséges, adatfüggőség korlátozza az egyidejű indítást, blokkolás mellett lehet alacsony CPU. 2026-10-12: saját záró magyarázat a Task<string>, await-felfüggesztés, szálfelszabadulás és állapotgép kapcsolatáról. A megszakításkori kivételtípus vezetett pontosítást igényelt. Az AsyncHttpLab lezárt; L1 marad, új kontextusú önálló alkalmazás nyitott. Részletek: projektállapot. |
| `DOTNET-CONCURRENCY-THREADING` | L3 | L0 | Learning | 2026-10-11: AsyncHttpLab kapcsán a tanuló helyesen indokolta, hogy a blokkolt szál alacsony CPU mellett is foglalt, és az azonos szálazonosító nem bizonyít végig tartó várakozást. ThreadPool, hardveres/szoftveres szál és ütemezés mentor által bevezetve; a 6/3 s I/O-kísérlet nem CPU-parallelism vagy terhelésmérés. A teljes altéma L1-e, szinkronizáció és önálló starvation-diagnózis még nincs igazolva. |
| `DOTNET-REFLECTION-METADATA` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `DOTNET-DI-COMPOSITION` | L2 | L1 | Practiced | 2026-10-07: scoped/transient LifeTimeProbe példányazonosság forrással és két-két HTTP-kéréssel igazolt. Singleton helyes következtetése felhasználói kérésre elfogadva a feladat lezárásához; külön singleton probe-futás nincs. Singleton kalkulátor → scoped snapshot hiba vezetetten reprodukálva és javítva. Saját magyarázat: scope-onkénti példány, transient külön feloldások, regisztráció nem fut újra kérésenként; első feloldás előtt nincs kalkulátorpéldány. Scope és lifetime-indoklás pontosítást igényelt; önálló L2-diagnózis és új kontextus nyitott. |

### Bizonyítékforrások

- [Aktív globális állapot](LEARNING_STATE.md)
- [AsyncHttpLab projektállapot](projects/async-http-lab/MENTORING_STATE.md)
- [ConfigurationDiLab projektállapot](projects/configuration-di-lab/MENTORING_STATE.md)
- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## 2. pillér — Web- és backendfejlesztés

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `WEB-HTTP-SEMANTICS` | L3 | L1 | Practiced | `200`, `400`, `404`, `405`, `418`, `500` futási próbák; a kliens- és szerverhibák alapvető magyarázata. Nyitott: safe/idempotent szemantika, cache, protokollmélység. |
| `WEB-REQUEST-LIFECYCLE` | L2 | L1 | Practiced | Kestrel → `HttpContext` → middleware → routing/binding → endpoint → szerializáció → response útvonal sikeres és hibás ágai ellenőrizve; `Response.HasStarted` megfigyelés. Az `M6` felidézésben a tanuló a lánc fő elemeit és az `await next` visszatérő ágát saját szavaival összekapcsolta; L2-höz késleltetett vagy új kontextusú önálló bizonyíték kell. |
| `WEB-ROUTING-ENDPOINTS` | L2 | L1 | Practiced | `/`, `/health`, `/greet/{name}`, `/divide`, `/rectangle/{width}`; `404`/`405` különbség és `MapGet` szerepe ellenőrizve. A tanuló felismerte, hogy a `{width:int}` constraint a binding előtt kizárta az endpointot és ezért `404`-et eredményezett. Nyitott: metadata és route-konfliktus. |
| `WEB-BINDING-VALIDATION` | L2 | L1 | Practiced | Route/query binding, hiányzó és hibás `int` automatikus `400`, valamint handleren belüli explicit `400` ágak ellenőrizve. A `/rectangle` önálló implementációja és a constraint/binding hibahatár diagnózisa elkészült. Nyitott: body/header/form binding és összetettebb validáció. |
| `WEB-MIDDLEWARE-PIPELINE` | L2 | L1 | Practiced | Inline logging middleware, előtte/utána ág, short-circuit `418`, exception middleware és sorrend gyakorlati ellenőrzése. Nyitott: branching és új kontextusú önálló alkalmazás. |
| `WEB-RESPONSES-SERIALIZATION` | L2 | L1 | Practiced | Korábbi JSON/text, DTO, IResult, media type és deszerializációs bizonyíték megmarad. 2026-10-11: AsyncHttpLab Results.Text és közvetlen JSON-tömb visszaadása forrással ellenőrzött. A string mint C# típus és JSON mint tartalomformátum, valamint anonymous object és tömb különbsége vezetett pontosítást igényelt; a tanuló helyesen mondta, hogy szövegként a teljes JSON-body-t kapjuk. Önálló új kontextus, typed results és content negotiation nyitott. |
| `WEB-ERROR-HANDLING` | L2 | L1 | Practiced | Korábbi /fail, kontrollált 500, Response.HasStarted és 400 ágak mellett 2026-10-11: AsyncHttpLab timeout → 504 és külső 404 → 502 ProblemDetails közvetlen mentor HTTP-próbával igazolt. A tanuló helyesen azonosította, hogy az 504-et a LabApi állítja elő. 2026-10-12: saját magyarázat a külső 404 → HttpRequestException → saját 502 útról és a hamis when utáni következő catch vizsgálatáról. A bontott kliens felé küldhető 500-at mentor javította; önálló L2-diagnózis nyitott. |
| `WEB-API-DESIGN-CONTRACTS` | L2 | L1 | Practiced | Elkészült a szervizköltség-operation explicit `GET` contractja: fogyasztó, route/query input, szerveroldali díjszabás, stabil sikeres DTO, Problem Details hibatípusok és `400`/`404` határ. A tanuló a safe metódust, a route mismatch és az érvénytelen üzleti input különbségét, valamint a stabil problémaazonosító szerepét saját szavaival részben megindokolta. L2-höz új contract önálló tervezése és változtatási trade-off kell. |
| `WEB-DI-CONFIGURATION` | L2 | L1 | Practiced | ConfigurationDiLab alapozó lezárás, 2026-10-07: binding/Validate/ValidateOnStart, snapshot-fogyasztók és 9/9 regresszió. Üres JSON-díjtábla forrással és bemásolt OptionsValidationExceptionnel igazolt; nem pozitív díj korábbi bizonyítéka megmarad. Env 19000/visszaállás és snapshot reload tanulói sikerjelzés, teljes válaszok nélkül. Saját magyarázat: env prioritás, üres dictionary, runtime betöltés. Összefüggő időzítés és 19000 × 1,5 = 28500 végösszeg mentor által javított; önálló L2-alkalmazás/diagnózis továbbra is nyitott. |
| `WEB-ASYNC-CANCELLATION` | L2 | L1 | Practiced | 2026-10-12: AsyncHttpLab M1–M4 és exit gate Complete. Factory/GetStringAsync, sequential/WhenAll, három endpoint tokenlánca, 1 s timeout és szűrt 504/502 kezelés forrással ellenőrzött. Mentorpróba: 504 kb. 1,03 s; külső 404 → 502. Postman-siker, 6/3 s, cancellation-napló és végső 100 ms-os sikeres ág explicit tanulói visszajelzéssel elfogadva. A tanuló a sikeres awaitet, szálfelszabadulást, állapotgépet, filterértékeket és 404 → 502 utat saját szavaival magyarázta; a timeout kivételtípusa és a bontott kliens 500-as feltételezése mentor által javított. L1 / Practiced marad; önálló diagnózis, késleltetett alkalmazás és streaming/backpressure nyitott. |
| `WEB-API-TESTING` | L2 | L1 | Practiced | A sikeres, ismeretlenmárka-, nulla, 200 fölötti munkaórás és hiányzó szegmenses `404` contractteszt `5/5` zöld. A felső határon túli teszt tanulói önálló munka; a `404` utolsó konfigurációja és tesztkorrekciója agent-asszisztált. A felhasználó a mélyítést most elhalasztotta; L2-höz további önálló diagnózis és új kontextusú tesztdizájn hiányzik. |
| `WEB-SECURITY-INTEGRATION` | L2 | — | Unassessed | Ebben a tanulási szeletben tudatosan nincs autentikáció vagy authorization. |
| `WEB-BACKGROUND-PROCESSING` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `WEB-API-EVOLUTION` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `WEB-OBSERVABILITY-PERFORMANCE` | L2 | L0 | Learning | Strukturált request/response logging készült. 2026-10-11: AsyncHttpLab before/after időpont és szálazonosító, timeout és exception/státusz naplózás; üzenetsablon és argumentumok vezetett javítása. Sequential/concurrent 6/3 s tanulói mérés; ez nem terhelésmérés. Metrics, tracing, több komponens korrelációja és önálló teljesítményelemzés nyitott. |

### Bizonyítékforrások

- [Aktív globális állapot](LEARNING_STATE.md)
- [AsyncHttpLab projektállapot](projects/async-http-lab/MENTORING_STATE.md)
- [ConfigurationDiLab projektállapot](projects/configuration-di-lab/MENTORING_STATE.md)
- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## 3. pillér — Adatok és adatbázisok

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `DATA-RELATIONAL-MODEL` | L3 | L1 | Practiced | A MovieExplorer ötentitásos modellje elkészült. 2026-10-12: a DataAccessLab `Project`–`WorkItem` `1:N` modelljében a tanuló helyesen azonosította az FK helyét és indokolta, miért ismétlődik a projektazonosító több munkatételnél; a kötelező mezők és kulcsok magyarázatában mentor-pontosítás is kellett. 2026-10-13: helyesen azonosította, hogy a `Project.WorkItems` gyűjteményes navigáció, a `WorkItem.ProjectId` a tárolt FK; a navigáció adatbetöltését még vezetett magyarázat után sem foglalta össze önállóan. 2026-10-14: a tartós táblákban 1 projekt és 2 hozzá tartozó munkatétel; read-only `JOIN` két kapcsolódó sort igazolt. Relációs algebra és önálló, új kontextusú modellezés nyitott. |
| `DATA-SQL-QUERYING` | L3 | L1 | Practiced | MovieExplorerben háromtáblás JOIN-ok, szűrés, rendezés, `LIMIT`, `COUNT(*)`, window count és `ILIKE` ellenőrizve. 2026-10-12: DataAccessLab ideiglenes táblákba 2 projekt és 3 munkatétel beszúrva; a `JOIN` három eredménysorát a tanuló helyesen értelmezte. Önálló összetettebb query, aggregáció, CTE és `NULL`-logika nyitott. |
| `DATA-SCHEMA-DESIGN` | L3 | L1 | Practiced | A minimális MovieExplorer-séma, a belső/külső azonosítók és a kapcsolótáblák döntései elkészültek. Normalizálási formák és denormalizálási trade-offok még nincsenek célzottan felmérve. |
| `DATA-INTEGRITY-CONSTRAINTS` | L3 | L1 | Practiced | MovieExplorerben primary/foreign/composite key, egyedi index, nullability és cascade szabályok ellenőrizve. 2026-10-12: DataAccessLab `project_id=999` beszúrás FK-hibája ténylegesen futott; a tanuló külön azonosította a `NOT NULL` és ismétlődő PK várható hibáját. 2026-10-14: a tartós séma PK/FK constraintjei és `ON DELETE CASCADE` szabálya közvetlen adatbázis-lekérdezéssel ellenőrizve, a cascade viselkedést a felhasználó elfogadta. 2026-10-21: hiányzó projektre a tanuló `404` API-választ jelzett, míg közvetlen SQL `ProjectId=5` beszúrás `23503` FK-hibát adott; az alkalmazás- és adatbázishatárt helyesen különítette el. Az FK `NULL`-kezelésénél korábban pontosítás kellett; az új, ismétlődő kulcsos hiba önálló diagnózisa és a `CHECK` nyitott. |
| `DATA-STORAGE-ENGINE` | L2 | — | Unassessed | A PostgreSQL fizikai tárolási, page-, WAL- és bufferkezelési modelljére még nincs célzott bizonyíték. |
| `DATA-INDEXING` | L3 | L1 | Practiced | Egyedi és összetett indexek létrejöttek; a uniqueness és az oszlopsorrend szerepe áttekintésre került. Az indexválasztás végrehajtási tervvel történő igazolása még hiányzik. |
| `DATA-QUERY-PLANNING` | L3 | — | Unassessed | A projekt tervezett teljesítménylépése, de `EXPLAIN ANALYZE` vagy önálló tervdiagnózis még nem készült. |
| `DATA-TRANSACTIONS` | L3 | L1 | Practiced | Az importer tranzakcióban upsertel, a `--dry-run` ág rollbackje end-to-end próbával igazolt. 2026-10-21: DataAccessLabban `BEGIN` után `UPDATE` és hibás `INSERT` futott; a `23503` után `25P02` jelezte az abortált tranzakciót, külön `ROLLBACK` lezárta, és a mentor GET-je változatlan címet talált. A javított két `UPDATE` utáni `COMMIT` sikerét a tanuló jelezte; a mentor GET-je mindkét új címet látta. A tanuló helyesen magyarázta, hogy tranzakció nélkül az első módosítás megmaradhatna, más kapcsolat pedig csak `COMMIT` után láthatja a végső értékeket. Az izolációs és tranzakcióhatár-trade-offok mélyebb felmérése nyitott. |
| `DATA-CONCURRENCY-MVCC` | L3 | — | Unassessed | Nincs célzott konkurens session-, izolációs, MVCC-, lock- vagy deadlock-kísérlet. |
| `DATA-CONNECTIONS-POOLING` | L2 | L0 | Learning | Connection string, Npgsql provider és scoped `DbContext` használata megtörtént; a fizikai/logikai kapcsolat, pooléletciklus és kimerülés nincs felmérve. |
| `DATA-POSTGRESQL-FOUNDATIONS` | L2 | L1 | Practiced | Helyi PostgreSQL 16, schema/`search_path`, migrációtörténet és catalog-alapú sémaellenőrzés korábban megtörtént. 2026-10-12: új Docker Compose-os PostgreSQL-környezetben ideiglenes táblák, seed, `JOIN` és FK-hiba futott. 2026-10-14: a DataAccessLab tartós `public` sémájában `Projects`, `WorkItems` és `__EFMigrationsHistory` közvetlenül ellenőrizve; a Riderben a `postgres/public` és `data_access_lab/public` különbsége diagnosztizálva. Önálló rendszeres diagnosztika nyitott. |
| `DATA-NET-ACCESS` | L2 | L0 | Learning | Az alkalmazások az Npgsql provideren keresztül, EF Core-ral érik el a PostgreSQLt, és tranzakciót is használnak. Közvetlen ADO.NET connection/command/reader használat és életciklus-diagnózis még nem történt. |
| `DATA-EF-MAPPING-TRACKING` | L2 | L1 | Practiced | MovieExplorerben `DbContext`, `DbSet`, Fluent API-kapcsolatok, kulcsok és indexek elkészültek. 2026-10-12: a DataAccessLab két entityje, `DbContext`-je, Npgsql providere és DI-regisztrációja elkészült; a tanuló megkülönböztette a kontextus feloldását az első DB-művelettől. 2026-10-13: `Project.WorkItems` és az explicit `HasOne<Project>().WithMany(...).HasForeignKey(...)` forrással ellenőrizve, sikeres buildet jelzett; az FK/navigáció különbségét helyesen felidézte. 2026-10-14: a migráció alkalmazása után a tartós FK és index közvetlenül ellenőrizve; a tanuló megkülönböztette az `OnModelCreating` leképezését a `DbContext`-es adatlekérdezéstől mentor-pontosítás után. 2026-10-19: a `Project.WorkItems` navigáció lekérdezésbeli használata a GET-válaszban két kapcsolódó munkatételt adott; az FK és navigáció együttműködéséhez vezetett magyarázat kellett. 2026-10-21: a POST mentése GET-tel ellenőrzött; a PATCH kódja mindkét azonosítóra szűr, sikeres futását a tanuló jelezte. Ideiglenes `AsNoTracking()` mellett a válasz módosult, de a mentor GET-je változatlan DB-értéket mutatott; a tanuló a követés és `SaveChangesAsync` kapcsolatát helyesen magyarázta, majd eltávolította az `AsNoTracking()` hívást. Konkrét entity state debuggeres megfigyelése és önálló L2-es alkalmazás nyitott. |
| `DATA-EF-QUERY-EXECUTION` | L3 | L1 | Practiced | `FindAsync`, `EF.Functions.ILike`, rendezés, korlátozás, többtáblás LINQ és `ToListAsync()` materializálás korábban készült. 2026-10-19: a DataAccessLab GET `ProjectId` szerinti szűrése, kapcsolódó munkatételeket vetítő `Select`-je és `FirstOrDefaultAsync` végrehajtása forrással, HTTP-válasszal és tanulói magyarázattal ellenőrzött. A tanuló által bemásolt SQL `LIMIT 1` al-lekérdezést és `LEFT JOIN`-t mutatott; egy kérésre egy `Executed DbCommand` bejegyzést jelzett. A mentor által írt HTTP-ellenőrző szkript sikeres futását jelezte, kimenetet nem küldött. A vetítéshez és `LIMIT 1` magyarázatához segítség kellett; önálló L2, SQL-terv és N+1 diagnózis nyitott. |
| `DATA-SCHEMA-EVOLUTION` | L2 | L1 | Practiced | MovieExplorerben több EF Core migráció létrehozása, eltávolítása, újragenerálása, alkalmazása, snapshotja és history táblája ellenőrizve lett. 2026-10-13: a DataAccessLab `InitialSchema` migrációja sikeres `dotnet ef migrations add` futás után létrejött; `Up`/`Down` és snapshot forrással ellenőrizve: két tábla, PK-k, kötelező FK, index és generált cascade törlés. 2026-10-14: `dotnet ef database update` sikeresen alkalmazta; a táblák, PK/FK, index, `ON DELETE CASCADE` és history sor közvetlen PostgreSQL-lekérdezéssel ellenőrizve. A tanuló helyesen különítette el a migráció létrehozását és alkalmazását, valamint a history tábla szerepét; a törlési szabályt elfogadta. A későbbi biztonságos sémaváltozás és önálló diagnózis nyitott. |

### Bizonyítékforrások

- [DataAccessLab projektállapot](projects/data-access-lab/MENTORING_STATE.md) és [M1 SQL-kísérlet](projects/data-access-lab/sql/m1-relational-basics.sql).
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A MovieExplorer tényleges entitás-, `DbContext`-, migration-, importer- és controllerforrásai.

## 4. pillér — Számítógépes rendszerek

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `SYSTEMS-DATA-REPRESENTATION` | L2 | L0 | Learning | TSV-, JSON-, text- és bináris build artifactok használata megtörtént, de byteszintű ábrázolás vagy encodinghiba célzott diagnózisa még nincs. |
| `SYSTEMS-CPU-EXECUTION` | L2 | L0 | Learning | A CIL/JIT és gépi kód alaplánca áttekintésre került; CPU-utasítás, regiszter és CPU-bound viselkedés nem kapott önálló gyakorlati bizonyítékot. |
| `SYSTEMS-MEMORY-HIERARCHY` | L2 | — | Unassessed | Cache-, RAM-, lokalitás- vagy memória-sávszélesség kísérlet még nem történt. |
| `SYSTEMS-VIRTUAL-MEMORY` | L2 | — | Unassessed | Address space, page, stack/heap és operációs rendszerbeli memóriahasználat még nincs célzottan felmérve. |
| `SYSTEMS-PROCESS-PORT` | L2 | L1 | Practiced | A BackendFoundations process/PID/Kestrel/socket és fájl/process különbség korábbi bizonyítékai megmaradnak. 2026-10-06: process-szintű PowerShell env, gyermekfolyamat általi öröklés és újraindítás mentor által magyarázva; változatlan kód miatti buildelhagyás saját magyarázata helyes. A Windows case-insensitive, írásmódot megőrző env-nevét agent-próba reprodukálta, a korábbi nagy D-s létrehozást a tanuló megerősítette. |
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
- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## 5. pillér — Szoftverfejlesztés és architektúra

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `SE-REQUIREMENTS-USE-CASES` | L2 | L1 | Practiced | A MovieExplorer use case-ei és API-műveletei, valamint a Backend Foundations mérföldkövei és elfogadási feltételei rögzítve lettek. Új, homályos igény önálló tisztázása még nincs célzottan felmérve. |
| `SE-VERSION-CONTROL-COLLABORATION` | L2 | L0 | Learning | A projektek Git repositoryban vannak, de áttekinthető commit-workflowra, konfliktuskezelésre vagy code review-ra még nincs ellenőrzött bizonyíték. |
| `SE-DEBUGGING-DIAGNOSTICS` | L3 | L1 | Practiced | Korábbi build-, middleware-, binding- és process/port-diagnózis mellett 2026-10-06: Rider Attach to Process és Evaluate Expression vezetett használata; tanuló bemutatta a dictionary tényleges Ducati=19000 tartalmát. A mentor a ducati kereséssel összevetve diagnosztizálta a casing hibát, a tanuló comparerrel javította és sikeresnek jelezte. Önálló többkomponensű diagnózis továbbra is nyitott. |
| `SE-CODE-QUALITY-READABILITY` | L2 | L0 | Learning | Kis, fókuszált lépések, formázás és guard jellegű validáció megjelent; önálló olvashatósági refaktor és trade-off-magyarázat még nincs értékelve. |
| `SE-REFACTORING-TECH-DEBT` | L2 | — | Unassessed | Nem történt célzott, teszttel biztosított refaktorálási gyakorlat vagy technikaiadósság-priorizálás. |
| `SE-MODULARITY-COUPLING` | L3 | L0 | Learning | A MovieExplorer API és importer külön projekt, a Backend Foundations pedig tudatosan minimális maradt. Kohézió, csatolás és függőségi irány önálló elemzése még nincs felmérve. |
| `SE-SOLID-DESIGN` | L2 | — | Unassessed | Interface- és DI-használat önmagában nem bizonyítja a SOLID elvek megértését vagy megfelelő alkalmazását. |
| `SE-DESIGN-PATTERNS` | L2 | — | Unassessed | Frameworkminták használata megtörtént, de tervezési minta tudatos kiválasztására és trade-offjának magyarázatára nincs bizonyíték. |
| `SE-TESTING-FUNDAMENTALS` | L3 | L1 | Practiced | Elkészült Arrange–Act–Assert integration teszt szándékos red–green átmenettel és teljes response-assertionnel; a tanuló önállóan adott hozzá egy felső határon túli hibás tesztesetet. Saját szavaival megmagyarázta a státusz-only teszt vakfoltját és a teljes API-lánc miatt integration teszt jelleget. Az önálló tesztdizájn új kontextusban, teszthatár-trade-off és diagnózis későbbi, igényvezérelt mélyítés. |
| `SE-TEST-STRATEGY` | L2 | L0 | Learning | A Backend Foundations exit gate-je és kézi regressziós ágai visszajelzési pontokat adnak, de kockázatalapú automatizált tesztportfólió még nem készült. A felhasználó 2026-09-22-én a mélyebb tesztstratégiát más backendtémák javára elhalasztotta. |
| `SE-ARCHITECTURE-BOUNDARIES` | L3 | L0 | Learning | Az API és importer process-/projekthatára, illetve a felesleges rétegek elhalasztása tudatos döntés volt. Komponens- és függőségi határok önálló tervezése még nincs bizonyítva. |
| `SE-LAYERED-CLEAN-ARCHITECTURE` | L2 | — | Unassessed | A projektekben tudatosan nincs teljes réteges vagy Clean Architecture szerkezet; használatára és kritikájára nincs célzott bizonyíték. |
| `SE-DOMAIN-MODELING-DDD` | L2 | L0 | Learning | A MovieExplorer domainentitásai és invariánsai részben kialakultak, de aggregate, bounded context és domainmodell/adatmodell határ nincs felmérve. |
| `SE-MODULAR-MONOLITHS` | L2 | — | Unassessed | Nincs több üzleti modulból álló monolit vagy szolgáltatáshatár-kísérlet. |
| `SE-ARCHITECTURE-DECISIONS` | L3 | L0 | Learning | Több tartós döntés és scope-korlát dokumentálva van, de alternatívákat, minőségi attribútumokat és következményeket összevető önálló ADR még nem készült. |

### Bizonyítékforrások

- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
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

- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
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
| `OPS-ARTIFACT-CONFIG-SECRETS` | L2 | L0 | Learning | Build artifact, appsettings, connection string és User Secrets korábbi használata mellett 2026-10-06: env override 19000 és visszaállás tanulói sikerjelzéssel, teljes HTTP-válaszok nélkül; a kísérlet közben a comparer kódjavítása miatt rebuild is történt. Konfigurációcsere és kódváltozás/build különbségét saját szavaival helyesen indokolta. Több környezet azonos artifacttal és production secret injection még nyitott. |
| `OPS-CI-BUILD-QUALITY` | L2 | — | Unassessed | Nincs repositoryhoz kötött, tiszta környezetben futó CI pipeline és publikált artifact. |
| `OPS-RELEASE-DEPLOYMENT` | L3 | — | Unassessed | Nincs környezetek közötti promotion, deployment-, rollback- vagy kompatibilitási gyakorlat. |
| `OPS-REVERSE-PROXY-TLS` | L2 | — | Unassessed | Nincs reverse proxy, HTTPS termination vagy forwarding header konfiguráció. |
| `OPS-STRUCTURED-LOGGING` | L2 | L1 | Practiced | Backend Foundations strukturált request/response és exception logok. 2026-10-11: AsyncHttpLab Time/ThreadId és exception/StatusCode üzenetsablon forrással ellenőrizve; a tanuló a sablon és külön argumentum szintaxisában segítséget kapott. A cancellation-napló észlelése tanulói visszajelzés. Önálló sablonhasználat, központi gyűjtés, retention és több process közötti korreláció nyitott. |
| `OPS-METRICS-TRACING` | L2 | — | Unassessed | Nincs alkalmazásmetrika, trace, span vagy OpenTelemetry-instrumentáció. |
| `OPS-HEALTH-MONITORING-ALERTING` | L3 | L0 | Learning | Kézi `/health` endpoint és regressziós ellenőrzés készült; liveness/readiness, monitorozás, SLI/SLO és riasztás még nincs. |
| `OPS-CLOUD-ORCHESTRATION` | L2 | — | Unassessed | Nincs cloud deployment, IAM-, orchestrator-, service discovery- vagy autoscaling gyakorlat. |
| `OPS-RELIABILITY-INCIDENTS-CAPACITY` | L3 | — | Unassessed | Nincs load test, kapacitásmérés, runbook, SLO/error budget vagy incidensgyakorlat. |

### Bizonyítékforrások

- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A MovieExplorer `compose.yaml`, valamint a projektek tényleges konfigurációja, kódja és futási eredményei.

## 8. pillér — Elosztott rendszerek és rendszertervezés

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `DIST-FAILURE-MODELS` | L3 | L0 | Learning | Korábbi kliens–szerver és részleges response hibák mellett 2026-10-11: AsyncHttpLab két processze között valódi HTTP, request cancellation, 1 s timeout és külső 404 kezelése. Kontrollált 504/502 mentorpróbával, cancellation és sikeres ág tanulói visszajelzéssel. A hibaforrások megkülönböztetése vezetett; részleges hibák önálló elemzése, retry és idempotencia nyitott. |
| `DIST-COMMUNICATION-CONTRACTS` | L2 | L0 | Learning | HTTP API-k és kliens–szerver szerződések készültek. 2026-10-11: AsyncHttpLab aszinkron I/O-val végzett szolgáltatásközi HTTP request/response; a külső státusz és a saját 502/504 válasz vezetett elkülönítése. Messaging, kompatibilis contract-evolúció és önálló kommunikációs trade-off elemzés nincs felmérve. |
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

- [Backend Foundations projektállapot](projects/backend-foundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)
- A jelenlegi projektek egyprocesses, illetve egyszerű kliens–adatbázis topológiája; a hiányzó elosztott mechanizmusokat nem tekintjük bizonyítéknak.

## Következő curriculum-karbantartási lépés

Az aktív API Contract Testing projekt `M4` mérföldkövében a hiányzó márkaszegmens `404 application/problem+json` contractját kell előbb piros integration teszttel rögzíteni. A négy jelenlegi teszt zöld; a szerveroldali javítás csak a célzott red eredmény után következik.
