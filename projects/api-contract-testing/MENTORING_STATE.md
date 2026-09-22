# API Contract Testing — mentoring state

Utolsó frissítés: 2026-09-22

Állapot: lezárt. Az `M5` mélyebb regressziós kísérlete a felhasználó döntése alapján `Deferred`.

## Current Goal

Egy kicsi, explicit HTTP API-szerződés megtervezése, minimális ASP.NET Core implementációja és a publikus contract sikeres, hibás és határeseteinek automatizált integration tesztelése.

## Milestone Plan

| ID | Eredmény | Teljesítési bizonyíték | Állapot |
|---|---|---|---|
| `M1` | Kis API-operation contract és tesztmátrix | Egy operation célja, fogyasztója, metódusa, útvonala, input/output DTO-ja, media type-ja, sikeres és legalább két fontos hibás válasza konkrét példákkal; a státuszválasztások saját szavas indoklása. | `Complete` |
| `M2` | Futtatható alkalmazás- és integrationteszt-váz | Sikeres build; tesztprojekt; `WebApplicationFactory`/teszthost és `HttpClient`; legalább egy szándékosan bukó contractteszt igazolja a bekötést. | `Complete` |
| `M3` | Sikeres contractág implementációja | A legkisebb szükséges endpoint és stabil response DTO elkészül; a státusz, content type és JSON body integration tesztje zöld. | `Complete` |
| `M4` | Hibás és határesetek konzisztens contractja | A kiválasztott klienshibaágak `ProblemDetails` válasszal és automatizált tesztekkel ellenőrzöttek; a szerkezeti és üzleti validáció határa magyarázható. | `Complete` |
| `M5` | Determinisztikus regressziós csomag és teszthatár-magyarázat | A fontos contractágak ismételhetően zöldek; egy kontrollált contracttörés piros tesztet eredményez; a tanuló indokolja, miért integration teszt és mit nem bizonyít. | `Deferred` |
| `EXIT` | Projektlezárás és transzfer | M1–M5 bizonyítékaudit; nyitott mélyítések `Deferred` besorolása; egy kis contract- vagy tesztváltoztatás önálló elvégzése; következő tanulási irány kijelölése. | `Complete` |

A mérföldkő `Complete` állapota a projekt adott kimenetéhez szükséges bizonyítékot jelenti; tudásszintet csak a curriculum ledgerben rögzített, megfelelő mélységű bizonyíték emel.

## Curriculum Mapping

- **Elsődleges:** `WEB-API-DESIGN-CONTRACTS`, `WEB-API-TESTING`.
- **Közvetlen kapcsolatok:** `WEB-HTTP-SEMANTICS`, `WEB-ROUTING-ENDPOINTS`, `WEB-BINDING-VALIDATION`, `WEB-RESPONSES-SERIALIZATION`, `WEB-ERROR-HANDLING`.
- **Mérnöki keresztkapcsolat:** `SE-TESTING-FUNDAMENTALS`; későbbi mélyítés: `SE-TEST-STRATEGY`.

| Mérföldkő | Elsődleges altémák |
|---|---|
| `M1` | `WEB-API-DESIGN-CONTRACTS`, `WEB-HTTP-SEMANTICS`, `SE-TESTING-FUNDAMENTALS` |
| `M2` | `WEB-API-TESTING`, `SE-TESTING-FUNDAMENTALS` |
| `M3` | `WEB-RESPONSES-SERIALIZATION`, `WEB-API-TESTING` |
| `M4` | `WEB-BINDING-VALIDATION`, `WEB-ERROR-HANDLING`, `WEB-API-TESTING` |
| `M5` | `WEB-API-TESTING`, `SE-TESTING-FUNDAMENTALS`; keresztkapcsolat: `SE-TEST-STRATEGY` |

## Approved Contract

- **Operation:** `GET /services/wage-calculation/{bikeBrand}?workHours=...`
- **Fogyasztó:** frontend kliens.
- **Márkaszabály:** a bemenet case-insensitive; a response kanonikus kisbetűs nevet ad.
- **Munkaóra:** `decimal`, `0 < workHours <= 200`.
- **Díjtábla:** szerveroldali márka–óradíj leképezés. A konkrét fixture-értékek nem részei a tanulási contractnak; a teszt az implementációban választott aktuális értékekkel legyen determinisztikus.
- **Formula:** `Math.Ceiling(appliedHourlyRate * workHours)`, egész HUF-ra.
- **Sikeres contract:** `200 application/json`; mezők: `bikeBrand`, `workHours`, `appliedHourlyRate`, `totalCost`, `currency`.
- **Ismeretlen márka:** `400 application/problem+json`, `type = urn:problem:unsupported-bike-brand`.
- **Érvénytelen munkaóra:** `400 application/problem+json`, `type = urn:problem:invalid-work-hours`.
- **Hiányzó route szegmens:** `404 application/problem+json`, `type = about:blank`, `title = Not Found`.

## Progress

- A tanulási irányt a felhasználó jóváhagyta a Backend Foundations projekt sikeres lezárása után.
- Projektkód és tesztprojekt még nincs; az első lépés szándékosan a publikus contract tisztázása.
- Elkészült az első contractváz egy szervizköltség-számító operationhöz. A tanuló a `GET` metódust a szerverállapot változatlanságával indokolta, és `200`/`400` ágakat jelölt ki. Nyitott korrekció: a `GET` query contract és a JSON request body ellentmondása, a hiányzó route szegmens `404` viselkedése, valamint a `bikeType` üzleti szerepe, a számítási szabály, egységek, pénznem és valid tartomány pontosítása.
- A tanuló pontosította az üzleti felelősséget: az óradíjat a szerver határozza meg a `bikeType` alapján, ezért a kliens nem küld `hourlyRate` paramétert. Ezzel megszűnt a kliens által manipulálható díjszabás, és a request minimális bemenete `bikeType` + `workHours` lett.
- A tanuló az operation útvonalát `GET /services/wage-calculation/{bikeType}?workHours=...` alakban választotta. Tudatos contractkövetkezmény: a hiányzó route szegmens `404`, az ismeretlen, de jelen lévő `bikeType` pedig a handler által adott `400` lehet.
- Elkészült az első szerveroldali díjtábla: Suzuki/Honda 10 000, Kawasaki 14 000, Aprilia 16 000, Ducati 22 000 HUF/óra; a tervezett munkaóra-tartomány `(0, 200]`. Nyitott szemantikai korrekció, hogy ezek gyártómárkák, ezért a `bikeType` helyett a `bikeBrand` pontosabb contractnév. Az `int workHours` mellett a felfelé kerekítésnek nincs hatása, ezért az egész kontra tört munkaóra döntés még nyitott.
- A tanuló elfogadta a `bikeBrand` contractnevet és a `decimal workHours` típust. A végleges alapformula `appliedHourlyRate(brand) * workHours`, majd felfelé kerekítés egész HUF-ra; a munkaóra érvényes tartománya `(0, 200]`.
- Elkészült a `200`, két `400` és egy `404` első konkrét request/response-váza. Javítandó: a Ducati requesthez tévesen Suzuki/10 000-es response került; a `type: "error"` nem stabil Problem Details type URI; a munkaóra-hiba body-ja a deklarált `application/problem+json` ellenére saját `{ error }` formátum; a „.NET natív válasza” nem határoz meg ellenőrizhető `404` contractot.
- A korrekciós körben a sikeres Ducati-számítás `22 000 * 1.5 = 33 000 HUF` alakban konzisztens lett; a hibák egységes Problem Details szerkezetet és stabil `type` URI-t kaptak. A tanuló helyesen azonosította, hogy a frontend a stabil `type`, nem a lokalizálható `detail` alapján ágazhat el.
- Az `M1` teljesítési bizonyítéka elkészült; az `M2` aktív. A könyvtárban jelenleg csak a mentoring state található, a telepített SDK `10.0.103`, és elérhető a `web`, `xunit` és solution template.
- Létrejött az `ApiContractTesting.slnx`, a `net10.0` `ServiceCostApi` ASP.NET Core Empty projekt és a `net10.0` `ServiceCostApi.Tests` xUnit projekt. Mindkét projekt szerepel a solutionben, a tesztprojekt project reference-szel hivatkozik az API-ra; a teljes build 0 hibával és 0 warninggal sikerült. Az API még a sablon `/` endpointot, a tesztprojekt az üres `UnitTest1` tesztet tartalmazza.
- A tesztprojekt megkapta a `Microsoft.AspNetCore.Mvc.Testing` `10.0.12` PackageReference-et, az API top-level belépési pontja pedig `public partial class Program {}` deklarációval elérhető lett a teszt assembly számára. A solution build ezután is 0 hibával és 0 warninggal sikerült. A tanuló jelezte, hogy ez lesz az első .NET/xUnit tesztje, ezért a következő lépés vezetett kódvázat és soronkénti mentális modellt kap.
- Elkészült az első `WebApplicationFactory<Program>`/`HttpClient` alapú xUnit integration teszt. A `dotnet test` futásban 1 teszt indult és a tervezett módon bukott: `Expected: OK`, `Actual: NotFound`, mert a contract szerinti endpoint még nem létezik. Ez igazolja a teljes tesztbekötést és lezárja az `M2` mérföldkövet; `M3` `Active`.
- Elkészült az endpoint első tanulói változata route/query bindinggal, normalizálással és alapvalidációval, valamint a `WageCalculationResponse` első osztályváza. A build `CS0119` hibán áll: a `BrandCost` egy típus, ezért nem használható `BrandCost[normalizedBikeBrand]` formában indexelhető értékként. A külön márkalista és statikus mezőket tartalmazó díjosztály ugyanazt az adatot duplikálja; egy case-insensitive `Dictionary<string, decimal>` egyben tudja reprezentálni az érvényes márkákat és díjakat.
- A dictionary/`TryGetValue` javítás megszüntette a `CS0119` hibát, a DTO `WorkHours` mezője is helyreállt. A build most `CS0029` hibán áll, mert a `new { ... }` compiler által generált anonim típust hoz létre, amely nem alakítható automatikusan `WageCalculationResponse` példánnyá. További eltérés: a forrás díjtáblája nem egyezik a jóváhagyott contracttal, a validációból hiányzik a `workHours > 200` ág, a számításból pedig a `Math.Ceiling`.
- Az explicit `WageCalculationResponse` példány, a teljes `(0, 200]` validáció és a dictionary-alapú díjfeloldás elkészült. A solution buildje sikeres, az első integration teszt zöld (`1/1`), így a státuszkód részlépése teljesült. Az `M3` még aktív: a `Math.Ceiling`, a response content type és a JSON body assertionje hiányzik. A felhasználó pontosította, hogy a díjtábla konkrét számai ennél a tanulási feladatnál nem contractkövetelmények.

## Decisions

- A projekt repository-helye: [`projects/api-contract-testing/`](./).
- A gyakorlati technológia ASP.NET Core Minimal API `net10.0` targettel; az integration tesztek valódi HTTP-határon, teszthoston és `HttpClient`en keresztül ellenőriznek.
- Az első szeletben nincs adatbázis, autentikáció, külső szolgáltatás, Docker vagy indokolatlan architekturális réteg.
- A production implementáció a contract teljesítéséhez szükséges legkisebb megoldás marad; a tesztek a publikus HTTP-viselkedést, nem belső implementációs részleteket rögzítenek.
- Alapértelmezetten a tanuló írja és futtatja a kódot.
- A `hourlyRate` nem kliensinput: a szerveroldali díjszabás rendeli hozzá a `bikeBrand` értékéhez. A publikus request csak a motorkerékpár márkáját és a munkaórát tartalmazza.
- A választott publikus operation: `GET /services/wage-calculation/{bikeBrand}?workHours=...`. ASP.NET route-template-ben a placeholder `{bikeBrand}`; a `:bike-brand` jelölés nem az ASP.NET route-szintaxisa.
- A `workHours` publikus típusa `decimal`, tartománya `(0, 200]`. A költség `appliedHourlyRate(brand) * workHours`, majd `Math.Ceiling` szemantikával egész HUF-ra kerekítendő.
- Az első tesztframework xUnit; a tesztek az ASP.NET Core teszthostot `WebApplicationFactory` segítségével fogják indítani.

## Open Questions

- Nincs projektlezárást blokkoló kérdés. Az `M5` kontrollált törését és a mélyebb tesztstratégiát a felhasználó kérésére halasztjuk.

## Learning Materials

- [ASP.NET Core integration tesztelési alapok](../../learning-materials/testing/aspnet-core-integration-testing-basics.md) — lezáró minimum: teszthatár, contract, generált tesztek ellenőrzése és a zöld suite korlátai.

## Next Step

A projekt lezárt. A következő tanulási irány a DI és konfiguráció; a konkrét következő lépés a globális `LEARNING_STATE.md` fájlban található.

## Session Notes

### 2026-09-18

- A felhasználó jóváhagyta az API-contract és automatizált integration teszt vertikális szeletet.
- Létrejött az M1–M5 plusz `EXIT` mérföldkőterv; `M1` `Active`, a többi mérföldkő `Planned`.
- Az első szervizköltség-contractvázban a `GET` safe jellegének indoklása megfelelő volt. A következő iterációban fel kell oldani a GET/query kontra JSON body ellentmondást, a route-hiány `404` határát és a még implicit üzleti számítási szabályt.
- A tanuló kivette a `hourlyRate` kliensparamétert: az óradíj a `bikeType` alapján szerveroldali üzleti szabály lesz. A contract következő iterációjához a konkrét típus–díj tábla, munkaóra-tartomány, formula és response-formák hiányoznak.
- Az URL-döntés `GET /services/wage-calculation/{bikeType}?workHours=...`; tisztázva lett az ASP.NET `{bikeType}` placeholder és a route szegmens hiányából következő `404`.
- A díjtábla és `(0, 200]` munkaóra-tartomány elkészült. Felismert korrekciós pont a márka/típus elnevezés és az, hogy egész inputnál a felfelé kerekítés hatástalan.
- Véglegesedett a `bikeBrand` elnevezés és a `decimal workHours`; a formula a szerveroldali márka–óradíj feloldás utáni szorzás, egész HUF-ra felfelé kerekítve.
- A tanuló elkészítette a négy HTTP-ág első konkrét vázát. A felülvizsgálat feltárta a request/response márkaeltérést, a két eltérő hibabody-sémát, a Problem Details `type` helytelen használatát és a nem tesztelhetően megfogalmazott `404` választ.
- Az aktív felidézés igazolta a `ProblemDetails.type` stabil gépi azonosító szerepét, valamint a route mismatch `404` és a handlerben felismert érvénytelen márka `400` határát. Az `M1` `Complete`, az `M2` `Active`.
- A solution/API/xUnit scaffold, solution-tagság, project reference és build ténylegesen ellenőrizve lett. A véletlenül törölt `projects/PROJECT_INDEX.md` a felhasználó visszajelzése alapján helyreállt az aktuális aktív/lezárt projektállapottal.
- Ellenőrizve lett a `Microsoft.AspNetCore.Mvc.Testing` csomag és a publikus részleges `Program` deklaráció; mindkettő helyes, a build tiszta. Az első integration teszt megírásához a tanuló vezetett segítséget kért.
- Az első integration teszt helyes Arrange–Act–Assert szerkezettel elkészült, és futással igazolta a várt red állapotot (`OK` kontra `NotFound`). Létrejött a kapcsolódó rövid, hivatalos forrásokra épülő tananyag. Az `M2` `Complete`, az `M3` `Active`.
- Az első endpointimplementáció buildhibája konkrét C# tanulási pontot adott: típus kontra érték, statikus mező kontra indexelhető dictionary, valamint duplikált adatforrás kontra egyetlen díjtábla. A DTO-ban a contract szerinti `WorkHours` helyett jelenleg `Wage` szerepel; ez a következő javítás része.
- A dictionary és DTO-mező javítása után a következő buildhiba az anonim típus és a névvel rendelkező DTO közötti különbséget tette láthatóvá (`CS0029`). A felülvizsgálat egyben feltárta a contracttól eltérő díjakat, a hiányzó felső órahatárt és kerekítést.
- Az újabb implementáció már lefordul, explicit DTO-t ad vissza, `TryGetValue`-val oldja fel a díjat, és mindkét munkaórahatárt validálja. A `dotnet test` eredménye `1/1` zöld; a jelenlegi teszt azonban csak a `200` státuszt bizonyítja, ezért hibás body vagy content type mellett is zöld maradna. A díjtábla konkrét értékeit a felhasználó kivette a tanulási contract kötött követelményei közül.
- Az aktív felidézésben a tanuló helyesen megmagyarázta a csak státuszt vizsgáló teszt body-vakfoltját, a `TryGetValue` kulcskeresését és `out` értékadását, valamint azt, hogy a teszt a teljes API-hívási lánc miatt integration teszt. Ez `WEB-API-TESTING` területen L1 gyakorlati bizonyíték.
- A tanuló önállóan hozzáadta a `Math.Ceiling` kerekítést és a media-type assertiont. A kibővített teszt szándékosan hasznos piros állapotot adott: `response.Content.GetType()` ténylegesen `StreamContent`, nem `WageCalculationResponse`, mert a HTTP body hordozóját még deszerializálni kell. A futás eredménye `0/1`, emellett a közvetlen `ContentType.MediaType` elérés nullable warningot adott (`CS8602`).
- A tanuló `ReadFromJsonAsync<WageCalculationResponse>()` segítségével helyesen deszerializálta a body-t, null-ellenőrzést és mind az öt mezőre assertiont írt, valamint nullable-biztos media-type ellenőrzést használt. A teljes solution tesztfutása warning nélkül `1/1` zöld. Ezzel az `M3` teljesítési bizonyítéka elkészült; az `M3` `Complete`, az `M4` `Active`.
- Az `M3` utáni felidézésben a tanuló helyesen elmagyarázta a `ReadFromJsonAsync<T>` beolvasó/deszerializáló szerepét és a property assertionök előtti null-ellenőrzés szükségességét. Nyitott részletként a szerveroldali DTO és a HTTP-n átvitt JSON-bájtokat hordozó kliensoldali `StreamContent` közötti határ igényelt magyarázatot.

### 2026-09-19

- A tanuló megírta a második, ismeretlen márkára vonatkozó integration teszt első változatát. A teljes futásban a sikeres ág zöld (`1/2`), a hibaág `JsonException` miatt piros: a jelenlegi `Results.BadRequest("Invalid bike brand")` JSON-sztringje nem deszerializálható `ProblemDetails` objektummá. A teszt a deszerializálást az assertionök előtt végzi, `application/json` media type-ot vár, a `Title` üres, a `Detail` pedig eltér a jóváhagyott contracttól. Következő lépés a teszt elvárásainak és sorrendjének korrekciója; az endpoint továbbra sem változik.
- A tanuló kijavította a `Title`, `Detail` és media-type elvárásokat a jóváhagyott contractra. Az újabb `dotnet test` továbbra is `1/2`: a deszerializálás a státusz/media-type assertionök előtt maradt, ezért a teszt újra `JsonException`-nel áll meg, nem a media-type contracteltérést jelzi. A következő egyetlen javítás a `ReadFromJsonAsync<ProblemDetails>()` sor áthelyezése a két HTTP-assertion után.
- A tanuló a státusz és media-type assertionöket a deszerializálás elé helyezte. A teljes `dotnet test` futásban a sikeres ág zöld, az ismeretlen márka tesztje pontosan a kívánt contracteltérésnél piros: várt `application/problem+json`, tényleges `application/json` (`1/2`). A szerveroldali implementáció eddig változatlan; ez az `M4` első bizonyított red állapota.
- Az aktív felidézésben a tanuló helyesen azonosította, hogy a tesztben a `400` státusz assertionje átment, és egy későbbi assertion bukott. A HTTP-státusz és a Problem Details body `status` mezője közötti két külön contractfelület még magyarázatot igényelt. A szerveroldali `Results.Problem` zöldre váltás továbbra is a következő lépés.
- A tanuló az ismeretlen márka ágát nevesített argumentumokkal `Results.Problem` válaszra cserélte. A teljes `dotnet test` futás warning nélkül `2/2` zöld; az `M4` ismeretlenmárka-ágának publikus `400 application/problem+json` és `ProblemDetails` contractja bizonyított. A tanuló a munkaóra-hibaágat is `Results.Problem` válaszra állította, de erre még nincs teszt; a contract szerinti egyedi `type` nincs megadva, és a "between 1 and 200" részlet pontatlan a jóváhagyott `(0, 200]` tartományhoz képest. Az `M4` továbbra is `Active`.
- A harmadik teszt és a munkaóra-hibaág együtt módosult: a teljes futás warning nélkül `3/3` zöld, de az elvárt és tényleges `type` egyaránt `urn:problem:Invalid-work-hours` a jóváhagyott kisbetűs `urn:problem:invalid-work-hours` helyett; a `Detail` mindkét helyen `between 0 and 200`, ami nem fejezi ki, hogy 0 kizárt. A `workHours=0` esetet teszteli, de a contract helyes teljesítését és a test-first red állapotot ez a futás nem bizonyítja. Az `M4` továbbra is `Active`.
- A felhasználó kifejezetten kérte a contracteltérés közvetlen javítását. Az agent előbb csak a harmadik teszt `Type`/`Detail` elvárásait javította a jóváhagyott contractra; a futás célzottan piros lett (`2/3`, `Invalid-work-hours` kontra `invalid-work-hours`). Ezután a szerveroldali munkaóra-választ javította, és a teljes solution tesztje warning nélkül `3/3` zöld. A harmadik teszt `Act`/`Assert` kommentjei is helyreálltak. Ez agent-asszisztált contracthelyreállítás, nem bizonyít önálló tanulói hibadiagnózist; az `M4` továbbra is `Active` a további határesetek és a hiányzó route-szegmens `404` contractja miatt.
- A tanuló a korábbi teszt mintájára önállóan hozzáadta a `workHours=200.5` felső határ feletti integration tesztet. A teljes solution warning nélkül `4/4` zöld, és mindkét érvénytelen munkaóraérték ugyanazt a `400 application/problem+json` body-contractot kapja. Az `M4` továbbra is `Active`: a hiányzó route-szegmens `404` contractja még nincs tesztelve; a `Scenario_1`/`Scenario_2` tesztnevek később kifejezőbbé tehetők.
- A 2026-09-19-i állapotfrissítéskor a teljes solution tesztjeit újrafuttattuk: warning nélkül `4/4` zöld. A tesztfájlban továbbra is négy `[Fact]` található, `404` teszt még nincs; az egyetlen következő lépés változatlan.

### 2026-09-22

- A tanuló ötödik tesztje `GET /services/wage-calculation/notexistingsegment?workHours=1.5` kérésre `404`-et várt. A teljes solution futása `4/5`: az új teszt `Expected: NotFound, Actual: BadRequest` miatt bukott, a korábbi négy zöld maradt. A megadott szegmens illeszkedik a route-hoz, majd a handler ismeretlen márkaként `400`-at ad. Ez nem a jóváhagyott hiányzó szegmens `404` contracttesztje; az `M4` `Active` marad.
- A tanuló a kérésből elhagyta a márkaszegmenst, így az új teszt `404` státuszt kap. A teljes futás `4/5`: az ötödik teszt a várt `application/problem+json` és a tényleges `null` media type eltérésén bukik. A jelenlegi kérésben még záró `/` és `workHours=1.5` szerepel a jóváhagyott pontos URL helyett; a tesztben a `ProblemDetails` mezőassertionök is hiányoznak. Ez már célzott red állapot, de a teljes tesztcontract még nincs kész; `M4` `Active`.
- A tanuló pontosította az ötödik teszt URL-jét és elkezdett kézi `app.Use` middleware-t írni a `404` válaszhoz. A `context.Response.Body.Title` kifejezés félbemaradt; a solution buildje `CS1002` hibán áll. A `Response.Body` stream, nem `ProblemDetails` objektum. A tesztben `urn:problem:not-found`, `Not found`, `Detail = Not found`, illetve `Status = 400` szerepel, miközben a jóváhagyott contract `about:blank`, `Not Found`, `Status = 404`, kötött `Detail` nélkül. A következő lépés a beépített szolgáltatás/middleware bekötése és a tesztértékek helyreigazítása; `M4` `Active`.
- A tanuló a kézi middleware helyett `AddProblemDetails()` és `UseStatusCodePages()` használatára váltott; az ötödik teszt pontos URL-t, `404` státuszt és deszerializált body-t ellenőriz. A teljes solution futása `4/5`: a `Type` várt `about:blank` értéke helyett az ASP.NET Core alapértelmezett RFC 9110 linkje érkezik. A teszt `Title = "Not Found"` elvárása már jó, de a `Status` assertion még `400`, a media type assertion pedig hiányzik. A következő lépés a teszt helyreigazítása és a `404` `Type` célzott konfigurálása; `M4` `Active`.
- A felhasználó kérésére az agent a `404` válasz `Type` mezőjét a `CustomizeProblemDetails` callbackben `about:blank` értékre szűkítette, az ötödik tesztbe visszaállította az `application/problem+json` media type assertiont, és a body `Status` elvárását `404`-re javította. A teljes solution 2026-09-22-én warning nélkül `5/5` zöld; az üzleti `400` hibaágak regresszió nélkül maradtak. Az `M4` még `Active`, mert a routing, binding és üzleti validáció határának tanulói magyarázata hiányzik; ez az agent-asszisztált javítás önmagában nem emel tudásszintet.
- A tanuló helyesen azonosította, hogy hiányzó márkaszegmensnél nem fut handler és a framework ad `404`-et; `workHours=0` és ismeretlen, de jelen lévő márka esetén a handlerig jut a kérés. Két terminológiai hiba maradt: a `workHours=abc` a query paraméter bindingján bukik, nem route-paraméteren, és az ismeretlen márkára a handler `400` eredményt ad vissza, nem kivételt dob. Az `M4` addig `Active`, amíg ezt a tanuló saját szavaival nem pontosítja.
- A felhasználó jelezte, hogy a mentor által pontosított query binding és `Results.Problem`-eredmény különbségét megértette, és továbblépést kért. Az `M4` lezárását a `5/5` zöld publikus contractteszt és a korábbi, részben önálló hibaút-diagnózis támasztja alá; a pontosítás nem önálló L2-bizonyíték. Az `M4` `Complete`, az `M5` `Active`.
- A felhasználó a tesztelés mélyítése helyett más backendterületeket priorizált, és kérte a projekt lezárását. A teljes solution 2026-09-22-én újrafuttatva `5/5` zöld. Az audit szerint `M1`–`M4` teljesült; az önálló felső munkaórahatár-teszt kis tanulói tesztváltoztatás bizonyítéka. A kontrollált contracttörés és a részletes tesztstratégia nem történt meg, ezért `M5` `Deferred`, tudásszint nem emelkedett. A felhasználó által jóváhagyott rövidebb exit gate teljesült; `EXIT` `Complete`, a projekt lezárt. Következő irány: `WEB-DI-CONFIGURATION` és `DOTNET-DI-COMPOSITION`.
