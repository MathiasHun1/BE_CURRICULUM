# Globális tanulási állapot

Utolsó frissítés: 2026-09-17

## Aktuális fókusz

- **Tanulási szelet:** Backend Foundations — a .NET build- és futási modell, process, port és HTTP request lifecycle összekapcsolása egy minimális ASP.NET Core alkalmazásban.
- **Aktív projekt:** [Backend Foundations](projects/backend-foundations/) ebben a repositoryban.
- **Elsődleges pillér:** P1 — Web- és backendfejlesztés.
- **Kapcsolódó pillérek:** P1 — Programozás és .NET; P1 — Számítógépes rendszerek.
- **Curriculum-helyzet:** mind a nyolc pillér egyenként 15 stabil altémára bontva; a teljes változó állapot a [CURRICULUM_PROGRESS.md](CURRICULUM_PROGRESS.md), a definíciók a [`curriculum/`](curriculum/README.md) pillértérképeiben találhatók.
- **Kapcsolódó tanulási anyag:** [HTTP request lifecycle és ASP.NET Core pipeline](learning-materials/web-backend/http-request-lifecycle.md).
- **Aktuális projektmérföldkő:** `M6 — Önálló integráció és transzfer` (`Active`); `M1`–`M5` bizonyíték alapján `Complete`, az exit gate még `Planned`. A részletes terv a projekt `MENTORING_STATE.md` fájljában található.
- **Aktuális fázis:** az eddig tanult route/query binding, validáció és HTTP-válaszadás önálló összekapcsolása egy új Minimal API végpontban.

## Aktív témák

Az L0 itt azt jelenti, hogy az új tanulási útvonalon még nincs ellenőrzött bizonyíték; nem állítja, hogy a tanuló korábban nem találkozott a témával.

| Témaazonosító | Pillér | Célmélység | Jelenlegi mélység | Állapot | Eddigi bizonyíték |
|---|---|---:|---:|---|---|
| `WEB-REQUEST-LIFECYCLE` | Web- és backendfejlesztés | L2 | L1 | Practiced | Saját endpointok és middleware-ek készültek; a `200`/`404`, `418` short-circuit és kivételes ágak futási próbával ellenőrizve lettek. A saját exception handler a `/fail` esetén kontrollált JSON `500` választ ad. A `/partial-fail` kísérlet igazolta a `Response.HasStarted` következményét. A `/divide` endpoint sikeres, nulla osztós, hiányzó és hibás query paraméteres ágai ellenőrizve lettek; a tanuló saját szavaival megkülönböztette a kliensoldali `400` hibát a kezeletlen szerveroldali `500` hibától. A `curl: (18)` protokollszintű okának önálló magyarázata még nem bizonyított. |
| `DOTNET-BUILD-RUNTIME` | Programozás és .NET | L2 | L1 | Practiced | A tanuló azonosította az SDK/runtime szerepét, buildelt `net10.0` assemblyt, és saját szavaival felismerte a köztes kódot, illetve a build és futtatás különbségét. |
| `SYSTEMS-PROCESS-PORT` | Számítógépes rendszerek | L2 | L1 | Practiced | A futó `BackendFoundations` process, PID, Kestrel és az IPv4/IPv6 `5003` figyelő socket ténylegesen ellenőrizve; a file és process különbségét a tanuló elmagyarázta. |

## Az első tanulóprojekt tervezett kilépési feltételei

- A tanuló önállóan létrehoz és elindít egy minimális ASP.NET Core alkalmazást.
- Elkészít és HTTP-klienssel ellenőriz néhány egyszerű endpointot.
- Gyakorlatban megfigyeli a routing és a middleware pipeline működését.
- Meg tudja mutatni, melyik process melyik porton figyel.
- Saját szavaival elmagyarázza a `HTTP-kérés → Kestrel → middleware → endpoint → válasz` útvonalat.
- Megkülönböztet és diagnosztizál néhány alapvető 4xx és 5xx hibát.
- Egy kisebb új endpointot lépésről lépésre adott recept nélkül is elkészít.

## A következő lépés elfogadási feltételei

- A tanuló részletes recept nélkül elkészít egy `GET /rectangle/{width}?height=...` endpointot egész route- és query paraméterrel.
- Pozitív szélességnél és magasságnál az endpoint `200` JSON választ ad a két bemenettel és a területtel.
- Nem pozitív méretnél az endpoint tudatosan `400` választ ad; hiányzó vagy nem egész paraméternél a framework bindingja ad `400` választ.
- Az endpoint és a `/health`, `/fail` regressziós ágak HTTP-klienssel ellenőrzésre kerülnek.
- A tanuló saját szavaival végigköveti egy sikeres kérés útját Kestreltől a middleware-eken és bindingon át a válaszig.

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

## Egyetlen következő konkrét lépés

A tanuló részletes recept nélkül elkészít és HTTP-klienssel ellenőriz egy `GET /rectangle/{width}?height=...` endpointot, amely pozitív méreteknél területet ad, nem pozitív méretnél explicit `400` választ küld, hibás bindingnál pedig a framework automatikus `400` válaszát használja.
