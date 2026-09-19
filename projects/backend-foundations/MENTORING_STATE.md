# Backend Foundations — mentoring state

Utolsó frissítés: 2026-09-18

## Current Goal

A .NET buildtől a futó processen és Kestrelen át a Minimal API HTTP-válaszig tartó végrehajtási lánc gyakorlati megértése. A projekt tanulási célja teljesült; a projekt lezárt.

## Milestone Plan

| ID | Eredmény | Teljesítési bizonyíték | Állapot |
|---|---|---|---|
| `M1` | Build, runtime, process és port összekapcsolása | Sikeres `net10.0` build; SDK/runtime és CIL/JIT alapmodell magyarázata; futó process, PID és `5003` figyelő socket ellenőrzése. | `Complete` |
| `M2` | Routing, binding és alapvető HTTP-státuszok | `/`, `/health`, `/greet/{name}` és `/divide`; `200`, `404`, `405`, explicit és automatikus `400` ágak kliensoldali ellenőrzése. | `Complete` |
| `M3` | Middleware-sorrend és short-circuit | Request/response naplózás; a `next` előtti és utáni ág magyarázata; `/short-circuit` explicit `418` válasszal. | `Complete` |
| `M4` | Kivételterjedés és response lifecycle | `/fail` kontrollált JSON `500`; `/partial-fail`, `Response.HasStarted`, részleges body és kliensoldali hibás lezárás megfigyelése. | `Complete` |
| `M5` | Várható klienshiba és váratlan szerverhiba megkülönböztetése | Endpoint-validáció és framework binding különválasztása; `4xx`/`5xx` futási próbák és saját szavas magyarázat. | `Complete` |
| `M6` | Önálló integráció és transzfer | Új route- és query bindingot, validációt és JSON-választ kombináló endpoint részletes recept nélkül; regressziós próbák; a teljes request lifecycle saját szavas végigkövetése. | `Complete` |
| `EXIT` | Projektlezárás és irányváltás | Az M1–M6 bizonyítékainak rövid auditja, a megmaradt hiányok `Deferred` besorolása, a projekt státuszának lezárása és a következő tanulási szelet kijelölése. | `Complete` |

A mérföldkő `Complete` állapota a projekt adott kimenetéhez szükséges gyakorlati bizonyítékot jelenti; önmagában nem emeli automatikusan a kapcsolódó témát `L2` vagy `Confident` szintre.

## Curriculum Mapping

- **Elsődleges:** 2. pillér — Web- és backendfejlesztés (curriculum-prioritás: P1).
- **Részletes tématérkép:** [2. pillér — Web- és backendfejlesztés](../../curriculum/02-web-backend.md).
- **Kapcsolódó tématérkép:** [1. pillér — Programozás és .NET](../../curriculum/01-programming-dotnet.md).
- **Haladási ledger:** [Teljes curriculum — haladási nyilvántartás](../../CURRICULUM_PROGRESS.md).

| Mérföldkő | Elsődleges altémák |
|---|---|
| `M1` | `WEB-REQUEST-LIFECYCLE`; keresztkapcsolat: `DOTNET-BUILD-RUNTIME`, `SYSTEMS-PROCESS-PORT` |
| `M2` | `WEB-HTTP-SEMANTICS`, `WEB-ROUTING-ENDPOINTS`, `WEB-BINDING-VALIDATION`, `WEB-RESPONSES-SERIALIZATION` |
| `M3` | `WEB-MIDDLEWARE-PIPELINE`, `WEB-REQUEST-LIFECYCLE`; keresztkapcsolat: `DOTNET-DELEGATES-EVENTS`, `DOTNET-ASYNC-TASKS` |
| `M4` | `WEB-ERROR-HANDLING`, `WEB-MIDDLEWARE-PIPELINE`, `WEB-REQUEST-LIFECYCLE`; keresztkapcsolat: `DOTNET-EXCEPTIONS-RESOURCES`, `DOTNET-ASYNC-TASKS` |
| `M5` | `WEB-BINDING-VALIDATION`, `WEB-ERROR-HANDLING`, `WEB-HTTP-SEMANTICS` |
| `M6` | `WEB-ROUTING-ENDPOINTS`, `WEB-BINDING-VALIDATION`, `WEB-RESPONSES-SERIALIZATION`, `WEB-REQUEST-LIFECYCLE` |

## Progress

- A `net10.0` minimális ASP.NET Core projekt létrejött és sikeresen buildelt.
- A build artifactok, a CIL/JIT alapmodell, valamint a build és futtatás különbsége áttekintésre került.
- A futó `BackendFoundations` process és az `5003` port figyelő socketje ellenőrizve lett.
- A `GET /` kérés `200 text/plain`, egy nem létező útvonal pedig `404` választ adott.
- A tanuló elkészítette és HTTP-klienssel ellenőrizte a `GET /health` JSON endpointot: `{"status":"ok"}`.
- A tanuló elkészítette a `GET /greet/{name}` endpointot; a route paraméter kötése, a `200` JSON-válasz és a paraméter nélküli `404` ellenőrizve lett.
- A `POST /health` kérés `405 Method Not Allowed` választ és `Allow: GET` headert adott; a tanuló saját szavaival megkülönböztette ezt a `404` útvonalhiánytól.
- A tanuló inline middleware-t készített, amely a `next` előtt naplózza a request metódusát és útvonalát, utána pedig a response státuszát; builddel és futási próbával ellenőrizve lett a `/health` `200` és egy ismeretlen útvonal `404` ága.
- A tanuló helyesen azonosította a `MapGet` route-regisztráló és az `app.Run()` hostindító szerepét.
- Egy második inline middleware a `/short-circuit` kérést explicit `418` státusszal és JSON bodyval lezárta a `next` meghívása nélkül; a `/health` változatlanul `200` választ adott.
- A tanuló saját szavaival helyesen összekapcsolta a külső `await next(context)` visszatérését a módosított `HttpContext` kiolvasásával, és felismerte, hogy a short-circuit után a későbbi middleware-ek és endpointok nem futnak le.
- Elkészült a szándékosan `InvalidOperationException` kivételt dobó `GET /fail` endpoint. A `500` válasz, a kivétel middleware-eken keresztüli visszaterjedése és a process életben maradása futási próbával ellenőrizve lett. A tanuló felismerte, hogy a hibás `Task`-ot váró `await` újradobja a kivételt, ezért a normál folytatás kimarad; a `finally` szerepét is helyesen azonosította.
- Elkészült a pipeline elején álló saját exception-handling middleware. A `/fail` kivételét részletesen naplózza, majd — amíg a response nem indult el — belső részleteket nem szivárogtató JSON `500` választ ad. A build, a `/fail` `500`, a `/health` `200` és a `/short-circuit` `418` ága futási próbával ellenőrizve lett.
- Elkészült az aszinkron `GET /partial-fail` endpoint: `await`elt body-írás és flush után kivételt dob. A kliens megkapta a már elindult `200` választ és a részleges body-t, majd `curl: (18)` hibával jelezte a szabálytalanul lezárult chunked átvitelt. A `/fail` JSON `500` és a `/health` `200` regressziós próbája változatlanul sikeres. A tanuló felismerte a flush, a `Response.HasStarted` és a státusz megváltoztathatatlanságának kapcsolatát; a hiányos átvitel protokollszintű okának önálló magyarázata még nem bizonyított.
- Elkészült a `GET /divide` endpoint két kötelező egész query paraméterrel. A sikeres `200`, az explicit nulla osztós `400`, valamint a hiányzó és nem egész paraméter automatikus `400` válasza HTTP-klienssel ellenőrizve lett; a `/health` `200` és a `/fail` kontrollált JSON `500` regresszió sikeres maradt.
- A tanuló felismerte, hogy a parameter binding a handler meghívása előtt történik, a `Results.BadRequest` szabályos válasz és nem kivétel, továbbá saját szavaival megkülönböztette a kliensoldali `4xx` hibát a kezeletlen szerveroldali `5xx` hibától.
- A tanuló részletes recept nélkül elkészítette a `GET /rectangle/{width}?height=...` endpointot route- és query bindinggal, pozitivitási validációval és JSON területválasszal. A build 0 hibával és 0 warninggal zárult; a `200`, explicit `400`, automatikus `400`, `/health` `200` és `/fail` JSON `500` ágak futási próbája sikeres volt.
- Az első `{width:int}` változat hibás szöveges szélességnél `404`-et adott, mert a route constraint az endpointkiválasztásnál kizárta a jelöltet. A tanuló saját szavaival diagnosztizálta ezt, eltávolította a constraintet, majd a binding automatikus `400` ága ellenőrzötten működött.
- Az aktív felidézésben a tanuló összekapcsolta Kestrelt, a middleware pipeline-t, az endpointot és a választ; megkülönböztette a route/query forrást, a handler előtti bindinghibát a handleren belüli validációtól, a `Results.Ok` szerepét a `System.Text.Json` szerializációjától, valamint felismerte az `await next(context)` utáni visszatérő ágat és az ott látható `200` státuszt.

## Decisions

- A projekt helye: [`projects/backend-foundations/`](./) ebben a repositoryban.
- Target framework: `net10.0`.
- Az alapozó szelet Minimal API-val, adatbázis és további architekturális rétegek nélkül halad.
- Alapértelmezetten a tanuló írja és futtatja a kódot.
- A `RouteHandlerOptions.ThrowOnBadRequest` explicit `false`, így a bindinghibák Development környezetben is automatikus `400` választ adnak ahelyett, hogy a saját általános exception middleware `500`-ra alakítaná őket.
- A projekt véges M1–M6 mérföldkőtervvel és külön exit gate-tel halad; egyszerre egy mérföldkő és azon belül egy `Next Step` aktív.

## Open Questions

- Jelenleg nincs a következő lépést blokkoló nyitott kérdés.
- Nem blokkoló, `Deferred` mélyítés: a `/partial-fail` után jelentkező `curl: (18)` hiba pontos HTTP/1.1 protokollszintű magyarázatát egy későbbi HTTP/hálózati mélyítésben kell önállóan felidézni.

## Learning Materials

- [HTTP request lifecycle és ASP.NET Core pipeline](../../learning-materials/web-backend/http-request-lifecycle.md) — middleware, `HttpContext`, response-indítás, exception handling, Minimal API binding, `async`/`await` és HTTP/1.1 chunked átvitel.

## Next Step

Nincs aktív projektszintű következő lépés. A tanulás az [API Contract Testing](../api-contract-testing/) projektben folytatódik.

## Session Notes

### 2026-09-15

- Ellenőrizve lett a telepített .NET 10 SDK/runtime, a projekt scaffoldja, buildje, futó processe, portja és első HTTP-válaszai.
- Nyitott tanulási rés: a routing endpointkiválasztó szerepének és az `app.Run()` hostindító szerepének stabil megkülönböztetése.
- A `/greet/{name}` lépésben ellenőrzötté vált a route paraméter automatikus handler-paraméterhez kötése és az objektum JSON-szerializációja.
- A `404` és `405` különbsége futási kísérlettel, `Allow` headerrel és saját tanulói magyarázattal rögzítve lett.
- Az inline naplózó middleware request- és response-ága builddel, valamint `200`/`404` futási kérésekkel ellenőrizve lett.
- Az aktív felidézés alapján a pipeline végigfutásának és a `MapGet`/`Run` szerepeknek az alapmodellje megvolt; ekkor a `next` nélküli short-circuit működés maradt nyitott kérdésként.
- A short-circuit kísérlet `418` JSON-válasszal sikerült; az aktív felidézés igazolta a middleware-sorrend, a közös `HttpContext`, az aszinkron bodyírás és a korai visszatérés alapmodelljét.

### 2026-09-16

- A `/fail` endpoint `500` választ adott, miközben a `/health` továbbra is `200` volt, tehát a host életben maradt.
- A stack trace alapján végigkövettük az endpointból a két inline middleware-en át a `DeveloperExceptionPageMiddleware` felé visszaterjedő kivételt.
- Aktív felidézésben a tanuló helyesen leírta a visszaterjedési láncot és azt, hogy egy `finally` blokk a szokásos kivételes visszabontás során is lefut. Terminológiai pontosítás: az `await` önmagában nem elkapja vagy visszaadja a hibát, hanem újradobja a várt `Task` kivételét.
- A saját exception handler kontrollált `{"error":"An unhandled exception has occurred while processing your request."}` választ adott, miközben a teljes kivétel csak a szervernaplóba került. A `Response.HasStarted` ellenőrzés és a már elkezdett válasznál használt `throw;` bekerült.
- Áttekintettük, hogy egy logikai HTTP-válasz body-ja több részletben érkezhet: HTTP/1.1-ben chunkokkal, HTTP/2-ben és HTTP/3-ban DATA frame-ekkel. Nyitott gyakorlati bizonyíték a response megkezdése utáni kivétel tényleges kliensoldali megfigyelése.
- A `/partial-fail` kísérletben a kliens `200` státuszt és `Partial fail text` body-részletet kapott, majd `curl: (18) transfer closed with outstanding read data remaining` hibát jelzett. A forrásellenőrzés során az első, nem `await`elt `WriteAsync` javításra került; az ismételt próba és a `/fail`, `/health` regresszió sikeres volt.
- Az aktív felidézésben a tanuló felismerte, hogy a már elindult válasz státusza nem cserélhető le és a `/fail`/`/partial-fail` között a `Response.HasStarted` ága tesz különbséget. A `curl: (18)` okát nem tudta önállóan megmagyarázni; a pontosítás után a további felidézést kihagyta.

### 2026-09-17

- A tanuló elkészítette a `/divide` endpointot, majd a buildhibákon keresztül tisztázta, hogy a handler visszatérési típusa nem a `MapGet<IResult>` generikus argumentuma; a lambda delegate-té alakul, amelyet a routing később hív meg.
- Development környezetben a hibás binding `BadHttpRequestException` kivételét a saját általános exception middleware tévesen `500`-ra alakította. A `RouteHandlerOptions.ThrowOnBadRequest = false` beállítás után a hiányzó és nem egész query paraméter automatikus `400` választ adott.
- Futási ellenőrzés igazolta a `/divide` `200`, explicit és automatikus `400`, továbbá a `/health` `200` és `/fail` JSON `500` ágait.
- Aktív felidézésben a tanuló helyesen elkülönítette a handler előtti bindinghibát, a kivétel nélküli `Results.BadRequest` választ és a kezeletlen szerveroldali kivételt; a `4xx` és `5xx` felelősségi különbségét saját szavaival megfogalmazta.
- A felhasználó jóváhagyásával bevezetésre került a véges M1–M6 mérföldkőterv és az explicit exit gate. Az eddigi bizonyítékok alapján M1–M5 `Complete`, M6 `Active`; a nem blokkoló `curl: (18)` protokollmélyítés `Deferred` lett, hogy ne nyújtsa korlátlanul a projektet.
- Elkészült a 2. curriculum-pillér 15 altémás bontása és a teljes curriculum inkrementális progress ledgerje; a projekt M1–M6 mérföldkövei hozzá lettek rendelve az érintett web/backend altémákhoz.
- Elkészült az 1. curriculum-pillér 15 altémás bontása; a projekt build/runtime, delegate/lambda, exception és async bizonyítékai hozzá lettek rendelve a kapcsolódó .NET-altémákhoz.

### 2026-09-18

- Elkészült az önálló `/rectangle/{width}` endpoint; a route/query binding, explicit és automatikus `400`, JSON `200`, valamint a `/health` és `/fail` regressziós ágak builddel és futással ellenőrizve lettek.
- A `{width:int}` constraint okozta `404` hibát a tanuló helyesen az endpointkiválasztáshoz kötötte, majd a javítás után a hibás `int` binding `400` válasza működött.
- A lifecycle-felidézés tisztázta a Kestrel → middleware → routing/binding → handler → `IResult`/JSON → visszatérő middleware ágat. Az `M6` bizonyíték alapján `Complete`; az `EXIT` gate `Active`.
- Az M1–M6 audit minden kötelező projekteredményhez ellenőrzött bizonyítékot talált. A pontos HTTP/1.1 `curl: (18)` protokollmélyítés, az assembly metadata/dependency resolution és az L2-höz szükséges késleltetett transzfer nem blokkolja a projekt lezárását; ezek a curriculum megfelelő pontjain maradnak nyitva.
- A felhasználó a következő tanulási szeletként jóváhagyta egy kis API-contract és automatizált integration teszt elkészítését. Az `EXIT` gate `Complete`, a Backend Foundations projekt lezárt.
