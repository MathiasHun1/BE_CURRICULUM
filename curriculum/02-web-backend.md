# 2. pillér — Web- és backendfejlesztés

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a HTTP-kérés teljes útját és a modern webes backend fejlesztését bontja stabil, mérhető kompetenciákra. Az ASP.NET Core az elsődleges gyakorlati környezet, de a témák célja frameworkök között átvihető mentális modellek kialakítása.

A pillér nem másolja át az adatbázisok, az identitáskezelés, az operációs rendszerek vagy a production infrastruktúra teljes tananyagát. Ezeknél csak a webes alkalmazáshoz szükséges integrációs határt rögzíti, és a megfelelő pillérre hivatkozik.

## Ajánlott tanulási útvonal

Az útvonal nem választja szét hosszú időre a request mechanikáját a valódi alkalmazásépítéstől. Egy rövid orientáció után minden új képességet lehetőleg egy tesztelhető üzleti API-szeletben kell bevezetni, majd valós production- vagy diagnosztikai trigger alapján mélyíteni.

1. **Vékony request-orientáció:** `WEB-HTTP-SEMANTICS`, `WEB-REQUEST-LIFECYCLE` és a `WEB-MIDDLEWARE-PIPELINE` alapmodellje. Cél a kérés útjának, a pipeline sorrendjének és a válasz határának megértése, nem a teljes protokoll- vagy middleware-mélység lezárása.
2. **Első munkaképes API-szelet:** `WEB-API-DESIGN-CONTRACTS` → `WEB-ROUTING-ENDPOINTS` → `WEB-BINDING-VALIDATION` → `WEB-RESPONSES-SERIALIZATION` → `WEB-ERROR-HANDLING` → `WEB-API-TESTING`. A contract, az implementáció és a sikeres/hibás ágakat ellenőrző integration teszt ugyanannak a vertikális szeletnek a része.
3. **Alkalmazás-integráció:** `WEB-DI-CONFIGURATION` → `WEB-ASYNC-CANCELLATION`, beleértve az outbound HTTP-hívásokat is; mellettük a `WEB-SECURITY-INTEGRATION` és `WEB-OBSERVABILITY-PERFORMANCE` kötelező korai alapszelete. Ezeket nem kell külön production-fázisig halasztani.
4. **Production- és változáskezelési mélyítés:** a security és observability teljes L2 mélysége, `WEB-BACKGROUND-PROCESSING`, `WEB-API-EVOLUTION`, továbbá a streaming, backpressure, protokoll-, middleware- és teljesítményrészletek.

Az útvonal függőségi iránytű, nem merev kurzusterv. DI-t, konfigurációt, cancellationt, securityt vagy observabilityt az első olyan üzleti szeletbe be kell húzni, amely ténylegesen igényli; a hiányzó előfeltételeket just-in-time kell pótolni.

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

A `WEB-SECURITY-INTEGRATION` és `WEB-OBSERVABILITY-PERFORMANCE` P2 besorolása a teljes altéma célmélységére vonatkozik. A biztonságos alapbeállítások, a `401`/`403` határ, a strukturált logging, a correlation és az alapvető health-jelzések keresztmetszeti P1 minimumok.

## Első munkaképes szelet és későbbi mélyítés

| Azonosító | Első munkaképes szelet | Későbbi mélyítés vagy trigger |
|---|---|---|
| `WEB-HTTP-SEMANTICS` | URI, metódus, státusz, header, body, media type, valamint safe és idempotent szemantika egy valódi contractban. | Cache-elhetőség, feltételes kérések és HTTP/1.1–HTTP/3 részletek protokoll- vagy teljesítménydiagnózisnál. |
| `WEB-REQUEST-LIFECYCLE` | Kestrel → `HttpContext` → pipeline → endpoint → response útvonal és egy alapvető lifecycle-hiba. | Kapcsolati, response-indítási és host-lifecycle részletek összetettebb futási hibánál. |
| `WEB-ROUTING-ENDPOINTS` | Route template, metódusillesztés, endpoint selection és `404`/`405` diagnózis. | Constraint, metadata, route-konfliktus és Minimal API/controller trade-off valós tervezési igénynél. |
| `WEB-BINDING-VALIDATION` | Route/query/body DTO, required/optional értékek, valamint szerkezeti és üzleti validáció elkülönítése. | Header/form binding és összetett validáció több bemeneti forrás vagy fájlfeltöltés esetén. |
| `WEB-MIDDLEWARE-PIPELINE` | Sorrend, `next` előtti/utáni ág, short-circuit, exception határ és response-indítás. | Branching, terminal middleware és saját újrafelhasználható komponens konkrét cross-cutting igénynél. |
| `WEB-RESPONSES-SERIALIZATION` | DTO, typed result, státusz, header, content type és stabil JSON-szerződés. | Content negotiation, custom serialization és streaming több reprezentáció vagy nagy válasz esetén. |
| `WEB-ERROR-HANDLING` | Várható `4xx`, váratlan `5xx`, egységes `ProblemDetails`, belső részletek védelme és regressziós teszt. | `Response.HasStarted` és összetett kivételes request-path diagnózisa részleges vagy streamelt válasznál. |
| `WEB-API-DESIGN-CONTRACTS` | Kis resource/operation contract, megfelelő metódus és státusz, DTO-határ, alapvető pagination és OpenAPI. | Összetettebb filtering/sorting, REST-trade-off és consumer-kompatibilitás több kliens vagy nagyobb API esetén. |
| `WEB-DI-CONFIGURATION` | Constructor injection, helyes lifetime, options, környezeti felülírás és secrets-határ. | Egyedi providerek, reload, factory és összetett lifetime/configuration diagnózis valós hibánál. |
| `WEB-ASYNC-CANCELLATION` | Valós async I/O, request cancellation továbbadása, timeout és `IHttpClientFactory`-alapú outbound HTTP-hívás. | Streaming, backpressure, sync-over-async és kliens-/kapcsolatélettartam mélyebb diagnózisa lassú vagy hibás függőségnél. |
| `WEB-API-TESTING` | `WebApplicationFactory`/teszthost és `HttpClient` alapú integration teszt a sikeres és fontos hibás contractágakra. | Célzott unit test, test double, determinisztikus külső függőség és szerződésteszt ott, ahol valódi kockázat indokolja. |
| `WEB-SECURITY-INTEGRATION` | HTTPS-határ, middleware-sorrend, authentication/authorization alapmodell, `401`/`403` és biztonságos input/output. | Policyk, CORS/CSRF és összetett identitásintegráció valós védett funkciónál; a mély modell a 6. pillérben. |
| `WEB-BACKGROUND-PROCESSING` | Cancellationt tiszteletben tartó `BackgroundService`, scope-kezelés és graceful shutdown. | Queue, retry/idempotencia és külső brokerhatár tartós vagy elosztott munkánál. |
| `WEB-API-EVOLUTION` | Additive és breaking változás felismerése már az első contract módosításakor. | Deprecation, verziózási stratégia, consumer migration és rollout több élő kliens esetén. |
| `WEB-OBSERVABILITY-PERFORMANCE` | Strukturált logging, correlation, alapvető health-jelzés és egy hibás request visszakeresése. | Metrics, tracing, latency/error-rate elemzés és mérésalapú optimalizálás production-közeli terhelésnél. |

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

- **Tartalom:** aszinkron I/O a request pathon, `Task`, request cancellation, timeout, outbound HTTP `HttpClient`/`IHttpClientFactory` használatával, kliens- és kapcsolatélettartam, válaszfeldolgozás, backpressure, streaming és sync-over-async hibák.
- **Előfeltételek:** `WEB-REQUEST-LIFECYCLE`; `Task` és `async`/`await` az 1. pillérben.
- **Kapcsolatok:** konkurencia és I/O a 4. pillérben, `WEB-OBSERVABILITY-PERFORMANCE`; részleges hibák és resilience a 8. pillérben.
- **Célbizonyíték:** cancellationt és timeoutot továbbadó aszinkron endpoint vagy outbound HTTP-integráció, megszakított vagy lassú függőség diagnózisa és a skálázhatósági hatás magyarázata.

### `WEB-API-TESTING` — HTTP API-k automatizált tesztelése

- **Tartalom:** contracthatárt ellenőrző integration tesztek, indokolt endpoint- vagy service-unit tesztek, teszthost, `HttpClient`, test doubles, determinisztikus konfiguráció és szerződés/regresszió ellenőrzése.
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

- **Tartalom:** strukturált logging, correlation, health-jelzések, metrics, tracing, latency/error rate, hot path, buffering, allocations és mérésalapú optimalizálás.
- **Előfeltételek:** `WEB-REQUEST-LIFECYCLE`, `WEB-ASYNC-CANCELLATION`.
- **Kapcsolatok:** production observability és kapacitástervezés a 7. pillérben.
- **Célbizonyíték:** egy lassú vagy hibás kérés trace/log/metric alapján történő diagnózisa és mérésen alapuló javítása.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** a vékony request-orientáció és az alapvető endpointmechanika témáihoz ad bizonyítékot, majd az `M6` mérföldkő az önálló transzfert ellenőrzi. Nem feladata a teljes munkaképes API-szelet, a security vagy a production-képességek lefedése.
- **MovieExplorer:** korábbi bizonyítékot ad routing, controlleres endpointok, binding, API-válaszok és részben API-tervezés területén; a bizonyítékok részletes újraértékelése csak akkor történjen, amikor az adott altéma aktívvá válik.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
