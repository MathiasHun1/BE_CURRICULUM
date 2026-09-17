# 1. pillér — Programozás és .NET

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a modern C# nyelv, a .NET alapkönyvtárak és a CLR futásidejű modell olyan ismereteit bontja stabil kompetenciákra, amelyek backend- és általános szoftverfejlesztésben is átvihetők. A cél nem az összes nyelvi funkció felsorolása, hanem a helyes programozási döntésekhez, hibadiagnózishoz és teljesítményérveléshez szükséges mentális modellek felépítése.

A webes request pipeline, adatbázis-lekérdezések, operációs rendszer és architektúra részletes tárgyalása más pillérekben történik. Itt azok C#/.NET oldali mechanizmusait és absztrakcióit rögzítjük.

## Ajánlott tanulási hullámok

1. **Nyelv és modellezés:** `CSHARP-LANGUAGE-FUNDAMENTALS` → `DOTNET-TYPE-SYSTEM` → `CSHARP-OBJECT-MODEL` → `DOTNET-GENERICS` → `DOTNET-COLLECTIONS` → `DOTNET-DELEGATES-EVENTS` → `DOTNET-EXCEPTIONS-RESOURCES`.
2. **Adatfeldolgozás és futásidő:** `DOTNET-LINQ-EXECUTION` → `DOTNET-BUILD-RUNTIME` → `DOTNET-MEMORY-GC` → `DOTNET-IO-STREAMS`.
3. **Aszinkronitás és extensibility:** `DOTNET-ASYNC-TASKS` → `DOTNET-CONCURRENCY-THREADING` → `DOTNET-REFLECTION-METADATA` → `DOTNET-DI-COMPOSITION`.

Ez függőségi iránytű, nem merev sorrend. Egy projekt több hullámból is elővehet témát, de a hiányzó előfeltételeket később célzottan pótolni kell.

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
