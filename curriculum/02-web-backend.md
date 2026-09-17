# 2. pillér — Web- és backendfejlesztés

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a HTTP-kérés teljes útját és a modern webes backend fejlesztését bontja stabil, mérhető kompetenciákra. Az ASP.NET Core az elsődleges gyakorlati környezet, de a témák célja frameworkök között átvihető mentális modellek kialakítása.

A pillér nem másolja át az adatbázisok, az identitáskezelés, az operációs rendszerek vagy a production infrastruktúra teljes tananyagát. Ezeknél csak a webes alkalmazáshoz szükséges integrációs határt rögzíti, és a megfelelő pillérre hivatkozik.

## Ajánlott tanulási hullámok

1. **Request mechanics:** `WEB-HTTP-SEMANTICS` → `WEB-REQUEST-LIFECYCLE` → `WEB-ROUTING-ENDPOINTS` → `WEB-BINDING-VALIDATION` → `WEB-MIDDLEWARE-PIPELINE` → `WEB-RESPONSES-SERIALIZATION` → `WEB-ERROR-HANDLING`.
2. **Alkalmazásépítés:** `WEB-API-DESIGN-CONTRACTS` → `WEB-DI-CONFIGURATION` → `WEB-ASYNC-CANCELLATION` → `WEB-API-TESTING`.
3. **Production-közeli képességek:** `WEB-SECURITY-INTEGRATION` → `WEB-BACKGROUND-PROCESSING` → `WEB-API-EVOLUTION` → `WEB-OBSERVABILITY-PERFORMANCE`.

Ez ajánlott függőségi sorrend, nem merev kurzusterv. Valós munkából vagy projektből érkező témát előre lehet venni, de később vissza kell kötni az előfeltételeihez.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `WEB-HTTP-SEMANTICS` | HTTP-alapok és szemantika | P1 | L3 |
| `WEB-REQUEST-LIFECYCLE` | Hosting és teljes request lifecycle | P1 | L2 |
| `WEB-ROUTING-ENDPOINTS` | Routing és endpointmodellek | P1 | L2 |
| `WEB-BINDING-VALIDATION` | Parameter binding és validáció | P1 | L2 |
| `WEB-MIDDLEWARE-PIPELINE` | Middleware pipeline | P1 | L2 |
| `WEB-RESPONSES-SERIALIZATION` | Válaszok, szerializáció és content negotiation | P1 | L2 |
| `WEB-ERROR-HANDLING` | Hibakezelés és konzisztens hibaválaszok | P1 | L2 |
| `WEB-API-DESIGN-CONTRACTS` | API-tervezés és szerződések | P1 | L2 |
| `WEB-DI-CONFIGURATION` | Dependency injection, konfiguráció és options | P1 | L2 |
| `WEB-ASYNC-CANCELLATION` | Aszinkron request processing, cancellation és streaming | P1 | L2 |
| `WEB-API-TESTING` | HTTP API-k automatizált tesztelése | P1 | L2 |
| `WEB-SECURITY-INTEGRATION` | Webes security-határok és identitásintegráció | P2 | L2 |
| `WEB-BACKGROUND-PROCESSING` | Háttérfeldolgozás és host lifecycle | P2 | L2 |
| `WEB-API-EVOLUTION` | API-verziózás és kompatibilis fejlődés | P2 | L2 |
| `WEB-OBSERVABILITY-PERFORMANCE` | Observability és request-path teljesítmény | P2 | L2 |

## Altémák részletei

### `WEB-HTTP-SEMANTICS` — HTTP-alapok és szemantika

- **Tartalom:** URI, metódusok, headerek, body, státuszkódok, media type, cache-elhetőség, safe/idempotent szemantika, HTTP/1.1–HTTP/3 alapmodell.
- **Előfeltételek:** alapvető kliens–szerver modell.
- **Kapcsolatok:** `WEB-API-DESIGN-CONTRACTS`, `SYSTEMS-PROCESS-PORT`; hálózatok és TLS a 4. pillérben.
- **Célbizonyíték:** kérések és válaszok nyers vizsgálata, megfelelő metódus- és státuszkód-választás, gyakori protokollhibák diagnózisa és a fontos trade-offok magyarázata.

### `WEB-REQUEST-LIFECYCLE` — Hosting és teljes request lifecycle

- **Tartalom:** host indulása, Kestrel, kapcsolat és kérés fogadása, `HttpContext`, pipeline, endpoint, válasz és kliens közötti végrehajtási lánc.
- **Előfeltételek:** `WEB-HTTP-SEMANTICS`; process, port és socket alapmodell.
- **Kapcsolatok:** `DOTNET-BUILD-RUNTIME`, `SYSTEMS-PROCESS-PORT`, `WEB-MIDDLEWARE-PIPELINE`.
- **Célbizonyíték:** egy kérés útjának saját szavas végigkövetése és legalább egy lifecycle-hiba futási diagnózisa.

### `WEB-ROUTING-ENDPOINTS` — Routing és endpointmodellek

- **Tartalom:** route template, constraint, endpoint selection, metódusillesztés, endpoint metadata, Minimal API és controlleres modell összehasonlítása.
- **Előfeltételek:** `WEB-HTTP-SEMANTICS`, `WEB-REQUEST-LIFECYCLE`.
- **Kapcsolatok:** `WEB-BINDING-VALIDATION`, `WEB-API-DESIGN-CONTRACTS`.
- **Célbizonyíték:** több útvonalas API önálló kialakítása, `404`/`405` és route-konfliktusok diagnózisa.

### `WEB-BINDING-VALIDATION` — Parameter binding és validáció

- **Tartalom:** route, query, header, body, form és DI binding; konverzió, required/optional értékek, üzleti és szerkezeti validáció elkülönítése.
- **Előfeltételek:** `WEB-ROUTING-ENDPOINTS`.
- **Kapcsolatok:** `WEB-RESPONSES-SERIALIZATION`, `WEB-ERROR-HANDLING`, típusrendszer az 1. pillérben.
- **Célbizonyíték:** több bindingforrást használó endpoint, automatikus és explicit `400` ágak tesztelése, valamint a handler előtti és handleren belüli hibák magyarázata.

### `WEB-MIDDLEWARE-PIPELINE` — Middleware pipeline

- **Tartalom:** request delegate, sorrend, `next` előtti/utáni működés, short-circuit, branching, terminal middleware és response-indítás.
- **Előfeltételek:** `WEB-REQUEST-LIFECYCLE`.
- **Kapcsolatok:** `WEB-ERROR-HANDLING`, `WEB-SECURITY-INTEGRATION`, delegate-ek és `async`/`await` az 1. pillérben.
- **Célbizonyíték:** saját middleware, sorrend- és short-circuit-kísérlet, hibás sorrend vagy már elkezdett response diagnózisa.

### `WEB-RESPONSES-SERIALIZATION` — Válaszok, szerializáció és content negotiation

- **Tartalom:** `IResult`, typed results, JSON-szerializáció, DTO-k, media type-ok, headerek, content negotiation és streaming válaszok alapjai.
- **Előfeltételek:** `WEB-HTTP-SEMANTICS`, `WEB-BINDING-VALIDATION`.
- **Kapcsolatok:** `WEB-API-DESIGN-CONTRACTS`, `WEB-ERROR-HANDLING`.
- **Célbizonyíték:** stabil JSON-szerződés készítése és tesztelése, szerializációs vagy media type probléma diagnózisa.

### `WEB-ERROR-HANDLING` — Hibakezelés és konzisztens hibaválaszok

- **Tartalom:** várható klienshibák és váratlan szerverhibák, exception propagation, exception handler, `ProblemDetails`, belső részletek védelme és `Response.HasStarted`.
- **Előfeltételek:** `WEB-MIDDLEWARE-PIPELINE`, `WEB-RESPONSES-SERIALIZATION`.
- **Kapcsolatok:** kivételek az 1. pillérben, security logging a 6. pillérben.
- **Célbizonyíték:** konzisztens `4xx`/`5xx` politika, regressziós tesztek és legalább egy kivételes request-path diagnózisa.

### `WEB-API-DESIGN-CONTRACTS` — API-tervezés és szerződések

- **Tartalom:** resource- és operation-modellezés, REST-korlátok, idempotencia, pagination/filtering/sorting, DTO-határok, OpenAPI és kompatibilitási döntések.
- **Előfeltételek:** `WEB-HTTP-SEMANTICS`, `WEB-ROUTING-ENDPOINTS`, `WEB-RESPONSES-SERIALIZATION`.
- **Kapcsolatok:** architektúra az 5. pillérben, `WEB-API-EVOLUTION`.
- **Célbizonyíték:** egy kisebb API-szerződés indokolt megtervezése, dokumentálása és kliensoldali használhatóságának ellenőrzése.

### `WEB-DI-CONFIGURATION` — Dependency injection, konfiguráció és options

- **Tartalom:** service registration/resolution, lifetime-ok, composition root, configuration providerek, környezeti felülírás, options pattern és titkok határa.
- **Előfeltételek:** objektum- és interface-alapok az 1. pillérben.
- **Kapcsolatok:** `WEB-API-TESTING`, architektúra az 5. pillérben, secrets a 6–7. pillérben.
- **Célbizonyíték:** több környezetben működő konfiguráció és helyesen választott service lifetime, hibás lifetime vagy konfiguráció diagnózisa.

### `WEB-ASYNC-CANCELLATION` — Aszinkron request processing, cancellation és streaming

- **Tartalom:** aszinkron I/O a request pathon, `Task`, request cancellation, timeout, backpressure, streaming és sync-over-async hibák.
- **Előfeltételek:** `WEB-REQUEST-LIFECYCLE`; `Task` és `async`/`await` az 1. pillérben.
- **Kapcsolatok:** konkurencia és I/O a 4. pillérben, `WEB-OBSERVABILITY-PERFORMANCE`.
- **Célbizonyíték:** cancellationt továbbadó aszinkron endpoint, megszakított vagy lassú kérés diagnózisa és a skálázhatósági hatás magyarázata.

### `WEB-API-TESTING` — HTTP API-k automatizált tesztelése

- **Tartalom:** endpoint unit- és integration tesztek, teszthost, `HttpClient`, test doubles, determinisztikus konfiguráció és szerződés/regresszió ellenőrzése.
- **Előfeltételek:** legalább egy működő endpoint és alapvető tesztelési ismeretek.
- **Kapcsolatok:** tesztstratégia az 5. pillérben, `WEB-DI-CONFIGURATION`.
- **Célbizonyíték:** sikeres és hibás ágakat lefedő automatizált integration tesztek, amelyek valódi regressziót jeleznek.

### `WEB-SECURITY-INTEGRATION` — Webes security-határok és identitásintegráció

- **Tartalom:** HTTPS-követelmény, CORS, authentication/authorization middleware helye, input/output támadási felület, CSRF és biztonságos hibaválaszok áttekintése.
- **Előfeltételek:** `WEB-MIDDLEWARE-PIPELINE`, `WEB-API-DESIGN-CONTRACTS`.
- **Kapcsolatok:** a mély identitás-, token- és jogosultságmodell a 6. pillérben található.
- **Célbizonyíték:** védett endpoint és policy alkalmazása, `401`/`403` megkülönböztetése, egy gyakori webes konfigurációs hiba diagnózisa.

### `WEB-BACKGROUND-PROCESSING` — Háttérfeldolgozás és host lifecycle

- **Tartalom:** `IHostedService`, `BackgroundService`, queue, scope-kezelés, graceful shutdown, retry/idempotencia határa és annak felismerése, mikor kell külső broker.
- **Előfeltételek:** `WEB-DI-CONFIGURATION`, `WEB-ASYNC-CANCELLATION`.
- **Kapcsolatok:** konkurencia a 4. pillérben, elosztott feldolgozás a 8. pillérben.
- **Célbizonyíték:** leállítható háttérmunka vagy queue megvalósítása, scope- és leállási hibák diagnózisa.

### `WEB-API-EVOLUTION` — API-verziózás és kompatibilis fejlődés

- **Tartalom:** breaking/non-breaking változás, additive evolution, deprecation, verziózási stratégiák, consumer contract és rollout.
- **Előfeltételek:** `WEB-API-DESIGN-CONTRACTS`, `WEB-API-TESTING`.
- **Kapcsolatok:** release engineering a 7. pillérben, elosztott kompatibilitás a 8. pillérben.
- **Célbizonyíték:** egy szerződés változtatásának kompatibilitási elemzése és biztonságos migrációs terv készítése.

### `WEB-OBSERVABILITY-PERFORMANCE` — Observability és request-path teljesítmény

- **Tartalom:** strukturált logging, correlation, metrics, tracing, latency/error rate, hot path, buffering, allocations és mérésalapú optimalizálás.
- **Előfeltételek:** `WEB-REQUEST-LIFECYCLE`, `WEB-ASYNC-CANCELLATION`.
- **Kapcsolatok:** production observability és kapacitástervezés a 7. pillérben.
- **Célbizonyíték:** egy lassú vagy hibás kérés trace/log/metric alapján történő diagnózisa és mérésen alapuló javítása.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** elsődlegesen az első hullám témáihoz ad bizonyítékot, majd az `M6` mérföldkő az önálló transzfert ellenőrzi.
- **MovieExplorer:** korábbi bizonyítékot ad routing, controlleres endpointok, binding, API-válaszok és részben API-tervezés területén; a bizonyítékok részletes újraértékelése csak akkor történjen, amikor az adott altéma aktívvá válik.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
