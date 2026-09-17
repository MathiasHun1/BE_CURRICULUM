# 1. pillér — Programozás és .NET

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a modern C# nyelv, a .NET alapkönyvtárak és a CLR futásidejű modell olyan ismereteit bontja stabil kompetenciákra, amelyek backend- és általános szoftverfejlesztésben is átvihetők. A cél nem az összes nyelvi funkció felsorolása, hanem a helyes programozási döntésekhez, hibadiagnózishoz és teljesítményérveléshez szükséges mentális modellek felépítése.

A webes request pipeline, adatbázis-lekérdezések, operációs rendszer és architektúra részletes tárgyalása más pillérekben történik. Itt azok C#/.NET oldali mechanizmusait és absztrakcióit rögzítjük.

## Ajánlott tanulási útvonal

Az útvonal a korai backend-munkaképességet optimalizálja, nem a témák lineáris befejezését. Egy altéma hosszú távú célmélysége nem előfeltétele a következő gyakorlati szeletnek; először a munkaképes részt kell bizonyítani, majd valós trigger alapján visszatérni a mélyebb mechanizmusokhoz.

1. **Belépő diagnózis és futási orientáció:** `CSHARP-LANGUAGE-FUNDAMENTALS`, `DOTNET-TYPE-SYSTEM` és `DOTNET-BUILD-RUNTIME` korai szelete. Cél az önálló kisebb kódmódosítás, a nullability és alapvető típusviselkedés kezelése, valamint a build–runtime lánc használható mentális modellje; nem a teljes témák lezárása.
2. **Munkaképes backendmag:** `CSHARP-OBJECT-MODEL`, a generics használati oldala, `DOTNET-COLLECTIONS`, `DOTNET-EXCEPTIONS-RESOURCES`, a delegate/lambda/closure alapok, `DOTNET-LINQ-EXECUTION`, `DOTNET-ASYNC-TASKS` és `DOTNET-DI-COMPOSITION`. Ezeket lehetőleg egy-egy üzleti vertikális szeletben kell összekapcsolni; `DOTNET-IO-STREAMS` akkor lépjen be, amikor valódi fájl- vagy hálózati I/O indokolja.
3. **Mérésvezérelt mélyítés:** a típusrendszer, generics, delegate/event, I/O és async haladó részei, majd `DOTNET-MEMORY-GC` és `DOTNET-CONCURRENCY-THREADING`. A P2-es `DOTNET-REFLECTION-METADATA` framework-internals, trimming/AOT vagy dinamikus aktiválási igény esetén kerül előre.

A `DOTNET-DI-COMPOSITION` gyakorlati használatához nem kell előbb lezárni a reflectiont vagy a teljes konkurenciatémát. A hiányzó előfeltételeket just-in-time kell pótolni, a korábban megszerzett részeket pedig új kontextusban és késleltetve újra ellenőrizni.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `CSHARP-LANGUAGE-FUNDAMENTALS` | Modern C# nyelvi alapok | P1 | L2 |
| `DOTNET-TYPE-SYSTEM` | C#/.NET típusrendszer és nullability | P1 | L3 |
| `CSHARP-OBJECT-MODEL` | Objektummodell, absztrakció és kompozíció | P1 | L2 |
| `DOTNET-GENERICS` | Generikus típusok és variance | P1 | L2 |
| `DOTNET-COLLECTIONS` | Kollekciók és adatszerkezet-választás | P1 | L2 |
| `DOTNET-DELEGATES-EVENTS` | Delegate-ek, lambdák és események | P1 | L2 |
| `DOTNET-EXCEPTIONS-RESOURCES` | Kivételek és erőforrás-élettartam | P1 | L2 |
| `DOTNET-LINQ-EXECUTION` | LINQ kompozíció és végrehajtási modell | P1 | L2 |
| `DOTNET-BUILD-RUNTIME` | Build, assembly, CLR, CIL és JIT | P1 | L2 |
| `DOTNET-MEMORY-GC` | Memória, GC és allokációk | P1 | L3 |
| `DOTNET-IO-STREAMS` | I/O, streamek és fájlkezelés | P1 | L2 |
| `DOTNET-ASYNC-TASKS` | Task-alapú aszinkron programozás | P1 | L3 |
| `DOTNET-CONCURRENCY-THREADING` | Szálak, ThreadPool és konkurencia | P1 | L3 |
| `DOTNET-REFLECTION-METADATA` | Metadata, reflection és dinamikus aktiválás | P2 | L2 |
| `DOTNET-DI-COMPOSITION` | Dependency injection és composition root | P1 | L2 |

## Első munkaképes szelet és későbbi mélyítés

Az alábbi bontás nem módosítja a célmélységeket. Azt rögzíti, mi ad korán napi munkában használható kompetenciát, és mihez érdemes csak konkrét triggerrel visszatérni.

| Azonosító | Első munkaképes szelet | Későbbi mélyítés vagy trigger |
|---|---|---|
| `CSHARP-LANGUAGE-FUNDAMENTALS` | Önálló kisebb funkció, vezérlési ágak, metódusok/propertyk, compiler errorok és idiomatikus alapkód. | Ritkább nyelvi konstrukciók és trade-offok akkor, amikor valós kódban megjelennek. |
| `DOTNET-TYPE-SYSTEM` | Nullability, alapvető konverziók, value/reference viselkedés, equality és API-típusválasztás. | Boxing, variance és finomabb runtime-reprezentáció generics-, memória- vagy teljesítményproblémánál. |
| `CSHARP-OBJECT-MODEL` | Class/interface/record, encapsulation, composition, immutability és DTO/domain határ. | Öröklési és polymorphism trade-offok összetettebb modell vagy hibás absztrakció esetén. |
| `DOTNET-GENERICS` | Generikus API-k biztonságos használata, saját egyszerű generikus típus vagy metódus és alapvető constraint. | Variance, nyitott/zárt típusok és runtime-reprezentáció framework- vagy absztrakciós igénynél. |
| `DOTNET-COLLECTIONS` | Array, `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>`, enumeráció és alapvető komplexitási döntések. | Immutable/concurrent collection, memória-trade-off és finomabb komplexitáselemzés mért igénynél. |
| `DOTNET-DELEGATES-EVENTS` | Lambda, method group, `Func`/`Action`, callback és closure-viselkedés. | Multicast delegate, event-életciklus és leiratkozási hibák eseményalapú komponensnél. |
| `DOTNET-EXCEPTIONS-RESOURCES` | Kivételterjedés, megfelelő catch-határ, `finally`, `using`, `IDisposable` és biztonságos hibajelzés. | Exception filter, `IAsyncDisposable` és összetettebb lifetime-hibák valódi erőforrás-kezelésnél. |
| `DOTNET-LINQ-EXECUTION` | Filtering/projection, kompozíció, deferred execution, materializálás és `IEnumerable<T>`/`IQueryable<T>` határ. | Grouping, providerfordítási korlát, többszörös enumeráció és teljesítménydiagnózis valós queryn. |
| `DOTNET-BUILD-RUNTIME` | SDK/runtime, build artifact, target framework és source → CIL → native kód alapmodell. | Assembly metadata, dependency resolution, JIT/AOT és összetettebb runtime-diagnózis konkrét hibánál. |
| `DOTNET-MEMORY-GC` | Object lifetime, managed heap, allokáció és GC pressure felismerése; optimalizálás előtt mérés. | Generációk, roots, LOH, finalization, pooling és `Span<T>`/`Memory<T>` mért memória- vagy throughputproblémánál. |
| `DOTNET-IO-STREAMS` | Streamelt olvasás/írás, encoding, buffering, disposal, cancellation és részleges műveletek kezelése. | Compression, memória-összehasonlítás és rendszerközeli I/O-diagnózis nagy adaton vagy hibás átvitelnél. |
| `DOTNET-ASYNC-TASKS` | Valós async I/O, `Task`, `await`, kivételterjedés, cancellation, `WhenAll` és sync-over-async felismerése. | State machine, backpressure és ütemezési/performance részletek diagnosztikai vagy terhelési igénynél. |
| `DOTNET-CONCURRENCY-THREADING` | Async, concurrency és parallelism elkülönítése; ThreadPool és shared state alapmodellje. | Race reprodukció, lock/atomic művelet, deadlock és starvation diagnózisa konkurens vagy terhelési problémánál. |
| `DOTNET-REFLECTION-METADATA` | Attribútumok és annak felismerése, hogyan használ reflectiont a DI, szerializáció és más frameworkök. | Saját scanning/aktiválás, költségmérés, source generation és trimming/AOT kockázat konkrét igénynél. |
| `DOTNET-DI-COMPOSITION` | Constructor injection, registration, transient/scoped/singleton lifetime, scope és composition root. | Captive dependency, factory, disposal és összetett objektumgráf diagnózisa valódi lifetime-hibánál. |

## Altémák részletei

### `CSHARP-LANGUAGE-FUNDAMENTALS` — Modern C# nyelvi alapok

- **Tartalom:** kifejezések és statementek, vezérlési szerkezetek, metódusok, propertyk, pattern matching, tuple, record, top-level statement, namespace és alapvető kódolási konvenciók.
- **Előfeltételek:** általános programozási alapok.
- **Kapcsolatok:** `DOTNET-TYPE-SYSTEM`, `CSHARP-OBJECT-MODEL`, `DOTNET-DELEGATES-EVENTS`.
- **Célbizonyíték:** kisebb program vagy funkció önálló, idiomatikus megvalósítása, fordítási hibák értelmezése és egy választott nyelvi konstrukció trade-offjának magyarázata.

### `DOTNET-TYPE-SYSTEM` — C#/.NET típusrendszer és nullability

- **Tartalom:** value/reference type, boxing, konverzió, type inference, nullable value/reference type, equality, identity, `required`, covariance/contravariance alapmodell és type safety.
- **Előfeltételek:** `CSHARP-LANGUAGE-FUNDAMENTALS`.
- **Kapcsolatok:** `DOTNET-GENERICS`, `DOTNET-MEMORY-GC`, binding és DTO-k a 2. pillérben.
- **Célbizonyíték:** típus- és nullability-hibák önálló diagnózisa, API-típusválasztás indoklása és legalább egy value/reference viselkedési kísérlet.

### `CSHARP-OBJECT-MODEL` — Objektummodell, absztrakció és kompozíció

- **Tartalom:** class, struct, record, interface, encapsulation, inheritance, polymorphism, composition, immutability és domainmodell-határok.
- **Előfeltételek:** `DOTNET-TYPE-SYSTEM`.
- **Kapcsolatok:** `DOTNET-DI-COMPOSITION`, architektúra az 5. pillérben.
- **Célbizonyíték:** kis modell önálló kialakítása, inheritance és composition közötti döntés indoklása, valamint hibás absztrakció felismerése.

### `DOTNET-GENERICS` — Generikus típusok és variance

- **Tartalom:** generikus típus és metódus, constraint, nyitott/zárt típus, variance, generikus újrafelhasználás és runtime reprezentáció alapjai.
- **Előfeltételek:** `DOTNET-TYPE-SYSTEM`, `CSHARP-OBJECT-MODEL`.
- **Kapcsolatok:** `DOTNET-COLLECTIONS`, `DOTNET-DELEGATES-EVENTS`, `DOTNET-DI-COMPOSITION`.
- **Célbizonyíték:** indokolt generikus absztrakció készítése, constraint alkalmazása és egy variance-probléma magyarázata.

### `DOTNET-COLLECTIONS` — Kollekciók és adatszerkezet-választás

- **Tartalom:** array, `List<T>`, dictionary, set, queue, stack, immutable és concurrent collection alapok; enumeráció, komplexitás és memória-trade-off.
- **Előfeltételek:** `DOTNET-GENERICS`.
- **Kapcsolatok:** `DOTNET-LINQ-EXECUTION`, adatszerkezetek a számítástudományi sávban.
- **Célbizonyíték:** megfelelő kollekció önálló kiválasztása, gyakori módosítási/iterációs hiba diagnózisa és alapvető komplexitási indoklás.

### `DOTNET-DELEGATES-EVENTS` — Delegate-ek, lambdák és események

- **Tartalom:** delegate mint típusos hívható referencia, method group, lambda, closure, `Func`/`Action`, multicast delegate és event publisher/subscriber modell.
- **Előfeltételek:** `CSHARP-LANGUAGE-FUNDAMENTALS`, `DOTNET-TYPE-SYSTEM`.
- **Kapcsolatok:** LINQ, middleware és callback-alapú API-k.
- **Célbizonyíték:** delegate-et vagy eventet használó kis komponens, closure-viselkedés magyarázata és esemény-leiratkozási hibamód felismerése.

### `DOTNET-EXCEPTIONS-RESOURCES` — Kivételek és erőforrás-élettartam

- **Tartalom:** throw/catch/finally, stack unwinding, exception filter, újradobás, várható eredmény és kivétel határa, `IDisposable`, `using`, `IAsyncDisposable`.
- **Előfeltételek:** `CSHARP-LANGUAGE-FUNDAMENTALS`.
- **Kapcsolatok:** `DOTNET-ASYNC-TASKS`, webes hibakezelés a 2. pillérben.
- **Célbizonyíték:** kivételterjedés diagnózisa, megfelelő catch-határ választása és determinisztikus erőforrás-felszabadítás megvalósítása.

### `DOTNET-LINQ-EXECUTION` — LINQ kompozíció és végrehajtási modell

- **Tartalom:** `IEnumerable<T>` és `IQueryable<T>`, method/query syntax, deferred execution, materializálás, projection, filtering, grouping, ordering és providerfordítás határa.
- **Előfeltételek:** `DOTNET-COLLECTIONS`, `DOTNET-DELEGATES-EVENTS`.
- **Kapcsolatok:** EF Core és adatbázis-végrehajtás a 3. pillérben.
- **Célbizonyíték:** kompozábilis lekérdezés önálló felépítése, materializálási pont felismerése és többszörös enumeráció vagy nem fordítható query diagnózisa.

### `DOTNET-BUILD-RUNTIME` — Build, assembly, CLR, CIL és JIT

- **Tartalom:** SDK és runtime, projekt/build, assembly és metadata, CIL, CLR, JIT/AOT alapmodell, target framework és dependency resolution.
- **Előfeltételek:** `CSHARP-LANGUAGE-FUNDAMENTALS`.
- **Kapcsolatok:** `DOTNET-REFLECTION-METADATA`, process és CPU a 4. pillérben.
- **Célbizonyíték:** build artifactok és futtatási lánc vizsgálata, SDK/runtime vagy target-framework hiba diagnózisa, valamint a source → CIL → native kód út magyarázata.

### `DOTNET-MEMORY-GC` — Memória, GC és allokációk

- **Tartalom:** stack/managed heap mentális modell, object lifetime, GC generations, roots, finalization, LOH, allocation, pooling, `Span<T>`/`Memory<T>` és mérésalapú optimalizálás.
- **Előfeltételek:** `DOTNET-TYPE-SYSTEM`, `DOTNET-BUILD-RUNTIME`.
- **Kapcsolatok:** virtuális memória a 4. pillérben, performance a 7. pillérben.
- **Célbizonyíték:** allocation/GC mérés és diagnózis, erőforrás-élettartam magyarázata és indokolt optimalizálási döntés.

### `DOTNET-IO-STREAMS` — I/O, streamek és fájlkezelés

- **Tartalom:** `Stream`, reader/writer, buffering, file és network I/O, encoding, compression, részleges olvasás/írás és disposal.
- **Előfeltételek:** `DOTNET-EXCEPTIONS-RESOURCES`.
- **Kapcsolatok:** `DOTNET-ASYNC-TASKS`, fájlok és rendszerhívások a 4. pillérben.
- **Célbizonyíték:** nagy adat streamelt feldolgozása, encoding/buffering/disposal hiba diagnózisa és memóriahatás összehasonlítása teljes betöltéssel.

### `DOTNET-ASYNC-TASKS` — Task-alapú aszinkron programozás

- **Tartalom:** TAP, `Task`/`Task<T>`, `async`/`await`, compiler state machine, I/O-bound és CPU-bound munka, kivételterjedés, cancellation, `WhenAll`/`WhenAny`, sync-over-async.
- **Előfeltételek:** `DOTNET-DELEGATES-EVENTS`, `DOTNET-EXCEPTIONS-RESOURCES`.
- **Kapcsolatok:** `DOTNET-CONCURRENCY-THREADING`, aszinkron request processing a 2. pillérben.
- **Célbizonyíték:** valós aszinkron I/O önálló implementációja cancellationnel, gyakori blokkolási/exception hiba diagnózisa és a state machine alapmodell magyarázata.

### `DOTNET-CONCURRENCY-THREADING` — Szálak, ThreadPool és konkurencia

- **Tartalom:** process/szál, ThreadPool, scheduling, shared state, race condition, synchronization, lock, atomic művelet, deadlock és parallelism kontra asynchrony.
- **Előfeltételek:** `DOTNET-ASYNC-TASKS`, `DOTNET-MEMORY-GC`.
- **Kapcsolatok:** operációs rendszerek és konkurencia a 4. pillérben.
- **Célbizonyíték:** race condition reprodukálása és javítása, thread starvation vagy deadlock diagnózisa, valamint async/concurrency/parallelism elkülönítése.

### `DOTNET-REFLECTION-METADATA` — Metadata, reflection és dinamikus aktiválás

- **Tartalom:** assembly/type metadata, attribute, reflection API, dynamic activation, scanning, source generation és trimming/AOT kompromisszumok.
- **Előfeltételek:** `DOTNET-BUILD-RUNTIME`, `DOTNET-TYPE-SYSTEM`.
- **Kapcsolatok:** `DOTNET-DI-COMPOSITION`, szerializáció és framework-internals.
- **Célbizonyíték:** metadata vagy attribute alapú kis mechanizmus, reflection költségének mérése és AOT/trimming kockázat felismerése.

### `DOTNET-DI-COMPOSITION` — Dependency injection és composition root

- **Tartalom:** dependency inversion, constructor injection, service registration, transient/scoped/singleton lifetime, scope, disposal, factory és composition root.
- **Előfeltételek:** `CSHARP-OBJECT-MODEL`, `DOTNET-GENERICS`, `DOTNET-EXCEPTIONS-RESOURCES`.
- **Kapcsolatok:** webes DI/configuration a 2. pillérben, architektúra az 5. pillérben.
- **Célbizonyíték:** megfelelő lifetime-okkal felépített objektumgráf, captive dependency vagy hiányzó registration diagnózisa és a DI nélküli alternatíva trade-offjának magyarázata.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** közvetlen bizonyítékot ad a build/runtime, delegate/lambda, exception és async/Task témákhoz.
- **MovieExplorer:** jelölt bizonyítékot ad a nyelvi alapok, típusrendszer, objektummodell, generics/collections, LINQ, async, I/O és DI témákhoz; a tudásszint csak a projektnaplóban ténylegesen igazolt magyarázat vagy önálló alkalmazás alapján emelhető.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
