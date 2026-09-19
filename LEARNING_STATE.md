# Globális tanulási állapot

Utolsó frissítés: 2026-09-18

## Aktuális fókusz

- **Tanulási szelet:** egy kis HTTP API-operation contract tudatos megtervezése és publikus viselkedésének automatizált integration tesztelése.
- **Aktív projekt:** [API Contract Testing](projects/api-contract-testing/) ebben a repositoryban.
- **Elsődleges pillér:** P1 — Web- és backendfejlesztés.
- **Kapcsolódó pillér:** P1 — Szoftvertervezés és architektúra, elsősorban tesztelési alapok.
- **Curriculum-helyzet:** mind a nyolc pillér egyenként 15 stabil altémára bontva; a teljes változó állapot a [CURRICULUM_PROGRESS.md](CURRICULUM_PROGRESS.md), a definíciók a [`curriculum/`](curriculum/README.md) pillértérképeiben találhatók.
- **Kapcsolódó tanulási anyag:** [ASP.NET Core integration tesztelési alapok](learning-materials/testing/aspnet-core-integration-testing-basics.md); korábbi háttér: [HTTP request lifecycle és ASP.NET Core pipeline](learning-materials/web-backend/http-request-lifecycle.md).
- **Aktuális projektmérföldkő:** `M4 — Hibás és határesetek konzisztens contractja` (`Active`); `M1`–`M3` `Complete`, `M5` és az exit gate `Planned`. A részletes terv a projekt `MENTORING_STATE.md` fájljában található.
- **Aktuális fázis:** a sikeres response teljes contracttesztje warning nélkül `1/1` zöld. Következik az ismeretlen márka Problem Details contractjának test-first megvalósítása.

## Aktív témák

Az L0 itt azt jelenti, hogy az új tanulási útvonalon még nincs ellenőrzött bizonyíték; nem állítja, hogy a tanuló korábban nem találkozott a témával.

| Témaazonosító | Pillér | Célmélység | Jelenlegi mélység | Állapot | Eddigi bizonyíték |
|---|---|---:|---:|---|---|
| `WEB-API-DESIGN-CONTRACTS` | Web- és backendfejlesztés | L2 | L1 | Practiced | Elkészült a szervizköltség-operation explicit `GET` contractja, díjtáblája, stabil sikeres DTO-ja, Problem Details hibatípusai és `400`/`404` határa. A safe metódus, route mismatch, üzleti inputhiba és stabil `type` azonosító alapmodellje felidézve; L2-höz új contract önálló tervezése kell. |
| `WEB-API-TESTING` | Web- és backendfejlesztés | L2 | L1 | Practiced | Az első `WebApplicationFactory`/`HttpClient` contractteszt red–green átmenete után a státusz, media type és mind az öt deszerializált DTO-mező warning nélkül zöld. A tanuló saját szavaival felismerte a csak státuszt vizsgáló teszt vakfoltját és a teljes HTTP-lánc miatt integration teszt jellegét. L2-höz a hibaágak önálló tesztelése és diagnózisa hiányzik. |
| `SE-TESTING-FUNDAMENTALS` | Szoftvertervezés és architektúra | L3 | L1 | Practiced | Elkészült egy Arrange–Act–Assert integration teszt szándékos red állapottal, zöldre váltással és teljes response-assertionnel. A tanuló megmagyarázta, hogy a státusz-only teszt mit nem bizonyít, és miért integration teszt a teljes API-lánc vizsgálata. Önálló tesztdizájn, teszthatár-trade-off és új kontextusú diagnózis még nyitott. |
| `WEB-RESPONSES-SERIALIZATION` | Web- és backendfejlesztés | L2 | L1 | Practiced | A szerveroldali explicit DTO, `Results.Ok`, `application/json`, `System.Text.Json` szerializáció és a kliensoldali `ReadFromJsonAsync<T>` deszerializáció teljes HTTP-teszttel ellenőrzött. A tanuló a deszerializálást és null-ellenőrzést magyarázta; a DTO → JSON bájtok → `StreamContent` határ még vezetett magyarázatot igényelt. |

## Az aktív tanulási szelet kilépési feltételei

- Egy kis operation publikus HTTP-contractja implementáció előtt explicit és indokolható.
- A sikeres, fontos hibás és határesetek automatizált integration tesztjei valódi HTTP-határon futnak.
- A response DTO, media type és hibaválasz stabil szerződésként ellenőrzött.
- A tanuló megkülönbözteti az integration és unit teszt határát, és meg tudja mondani, mit bizonyít, illetve mit nem bizonyít az elkészült teszt.
- Egy kontrollált contracttörés piros tesztet eredményez, majd a suite visszaáll zöldre.

## A következő lépés elfogadási feltételei

- Elkészül a `GET /services/wage-calculation/{bikeBrand}?workHours=...` legkisebb sikeres ága.
- Ducati és `1.5` munkaóra esetén az endpoint `200 application/json` választ ad mind az öt jóváhagyott mezővel; a konkrét díj az aktuális szerveroldali fixture-ből jön.
- A `totalCost` a szorzás `Math.Ceiling` szerinti, egész HUF-os eredménye.
- Az integration teszt a státusz mellett a content type-ot és a teljes deszerializált DTO-t is ellenőrzi.
- Ebben a részlépésben még nem szükséges a `400`/`404` hibaágak Problem Details contractjának elkészítése.

## Döntések

- A MovieExplorer 2026-09-14-től szüneteltetett tanulóprojekt; nem töröljük, és korábbi eredményei továbbra is tanulási bizonyítékok.
- Az új tanulási útvonal koherens backend-alapozással indul, nem a MovieExplorer következő funkciójával.
- Az első projekt egy minimális Backend Foundations labor lesz, amely a forráskódtól a HTTP-válaszig teszi láthatóvá a végrehajtási láncot.
- A projekt neve `BackendFoundations`, repository-helye [`projects/backend-foundations/`](projects/backend-foundations/), target frameworkje `net10.0`.
- Az első szeletben még nem használunk adatbázist, EF Core-t, autentikációt, Dockert vagy indokolatlan architekturális rétegeket.
- Alapértelmezetten a tanuló írja és futtatja a kódot; az agent kis lépésekben mentorál és tényleges bizonyíték alapján értékel.
- A `RouteHandlerOptions.ThrowOnBadRequest` értéke explicit `false`, hogy a Minimal API bindinghibái környezettől függetlenül automatikus `400` választ adjanak, és ne váljanak a saját exception middleware-ben `500` hibává.
- Az aktív projektek véges, bizonyíték-alapú mérföldkőtervet és explicit exit gate-et kapnak; egyszerre egy mérföldkő és azon belül egy konkrét lépés aktív.
- A curriculum pillérei inkrementálisan bomlanak altémákra; a teljes haladási ledger és az aktív fókuszállapot külön forrás-of-truth fájlban marad.
- A curriculum végrehajtása a korai munkaképességet és a spirális mélyítést követi: először témánként a legkisebb koherens, gyakorlattal és hibamóddal bizonyított szelet készül el, az L3-részletekhez pedig valós projekt-, diagnosztikai vagy teljesítménytrigger alapján térünk vissza.
- A tesztelés az első működő feature-től a delivery-loop része; architekturális réteg, minta vagy modul csak megnevezhető változási probléma és ellenőrizhető előny alapján kerül be.
- A security P1 keresztmetszeti alap: minden új üzleti szelet arányos threat-, input/output-, secret-, hozzáférés- és naplózási minimumot kap; a teljes identity- és kriptográfiai mélység a tényleges felelősséghez igazodik.
- A production minimum az első telepíthető szolgáltatástól együtt kezeli az immutable artifactot, külső konfigurációt, visszaállítható deploymentet, strukturált telemetriát és elkülönített startup/readiness/liveness jelzést; a cloud- és orchestration-mélység szükségvezérelt.
- Elosztott rendszereknél az első külső dependencyvel együtt jön a deadline/cancellation, timeout, biztonságos retry, idempotencia és korreláció; cache, broker, replika, shard, konszenzus vagy saga csak mért korlát vagy konkrét workflow-igény alapján kerül be.

## Nyitott döntések

- Jelenleg nincs a következő lépést blokkoló nyitott döntés.

## Megőrzött korábbi bizonyíték

- A MovieExplorer ASP.NET Core, PostgreSQL, SQL, EF Core és LINQ területeken szolgáltatott gyakorlati bizonyítékokat.
- A részletes előzmény és a félbehagyott következő lépés a MovieExplorer saját `MENTORING_STATE.md` fájljában marad.

## Legutóbbi rendszerszintű mérföldkő

- A Backend Foundations projekt `net10.0` targettel létrejött, buildelt és Kestrellel elindult.
- A futó process és az `5003` port kapcsolata ténylegesen ellenőrizve lett.
- A `/`, `/health` és egy nem létező útvonal `200`, JSON és `404` válaszai HTTP-klienssel ellenőrizve lettek.
- A `/greet/{name}` route paraméteres endpoint automatikus paraméterkötése és JSON-válasza ellenőrizve lett.
- A tanuló `POST /health` kéréssel `405 Method Not Allowed` választ idézett elő, megfigyelte az `Allow: GET` headert, és saját szavaival megkülönböztette a `404`-től.
- A tanuló inline middleware-t készített; builddel és futási próbával ellenőrizve lett a `next` előtti requestnapló és az utána futó `200`/`404` response-napló.
- Egy második middleware a `/short-circuit` ágat `next` nélkül, explicit `418` státusszal és JSON bodyval lezárta; a `/health` továbbra is `200` választ adott. A tanuló helyesen magyarázta a short-circuit vezérlési folyamatát.
- A `/fail` endpoint szándékos kivétele `500` választ eredményezett, a host életben maradt, és a stack trace igazolta a kivétel visszaterjedését az `await next(context)` hívásokon át a fejlesztői exception middleware-ig.
- A saját exception-handling middleware a `/fail` kivételét naplózta, majd ellenőrzötten `500 application/json` választ adott általános hibaüzenettel; a `/health` `200` és a `/short-circuit` `418` regressziós ág változatlan maradt.
- A tanuló elkészítette az aszinkron `/partial-fail` endpointot helyesen `await`elt body-írással és explicit flusholással. A kliens `200` státuszt és részleges body-t kapott, majd `curl: (18)` hibával észlelte a szabálytalanul lezárult chunked átvitelt; a `/fail` JSON `500` és a `/health` `200` regressziós próbája sikeres maradt.
- A `/divide` endpoint `200` eredménye, explicit nulla osztós `400` válasza, valamint a hiányzó és nem egész query paraméter automatikus `400` válasza ellenőrizve lett. A `/health` `200` és a `/fail` kontrollált JSON `500` regresszió sikeres maradt; az aktív felidézés igazolta a kliens- és szerverhibák alapvető megkülönböztetését.
- A `/rectangle/{width}` endpoint részletes recept nélkül elkészült. A `200` JSON, explicit és automatikus `400`, `/health` `200` és `/fail` JSON `500` ágak ellenőrizve lettek; a `{width:int}` constraint okozta kezdeti `404` diagnózisa és javítása is megtörtént.
- A lifecycle-felidézés igazolta a route/query forrás, binding kontra handler-validáció, `Results.Ok` kontra `System.Text.Json`, valamint az `await next(context)` utáni visszatérés alapmodelljét. Az `M6` `Complete`, az `EXIT` gate `Active`.

## Egyetlen következő konkrét lépés

A tanuló test-first módon hozzáadja az ismeretlen `bmw` márka integration tesztjét: `400`, `application/problem+json`, valamint a jóváhagyott `ProblemDetails` type/title/status/detail mezők ellenőrzésével először piros futást hoz létre.
