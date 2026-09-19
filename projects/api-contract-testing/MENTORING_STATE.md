# API Contract Testing — mentoring state

Utolsó frissítés: 2026-09-18

## Current Goal

Egy kicsi, explicit HTTP API-szerződés megtervezése, minimális ASP.NET Core implementációja és a publikus contract sikeres, hibás és határeseteinek automatizált integration tesztelése.

## Milestone Plan

| ID | Eredmény | Teljesítési bizonyíték | Állapot |
|---|---|---|---|
| `M1` | Kis API-operation contract és tesztmátrix | Egy operation célja, fogyasztója, metódusa, útvonala, input/output DTO-ja, media type-ja, sikeres és legalább két fontos hibás válasza konkrét példákkal; a státuszválasztások saját szavas indoklása. | `Complete` |
| `M2` | Futtatható alkalmazás- és integrationteszt-váz | Sikeres build; tesztprojekt; `WebApplicationFactory`/teszthost és `HttpClient`; legalább egy szándékosan bukó contractteszt igazolja a bekötést. | `Complete` |
| `M3` | Sikeres contractág implementációja | A legkisebb szükséges endpoint és stabil response DTO elkészül; a státusz, content type és JSON body integration tesztje zöld. | `Complete` |
| `M4` | Hibás és határesetek konzisztens contractja | A kiválasztott klienshibaágak `ProblemDetails` válasszal és automatizált tesztekkel ellenőrzöttek; a szerkezeti és üzleti validáció határa magyarázható. | `Active` |
| `M5` | Determinisztikus regressziós csomag és teszthatár-magyarázat | A fontos contractágak ismételhetően zöldek; egy kontrollált contracttörés piros tesztet eredményez; a tanuló indokolja, miért integration teszt és mit nem bizonyít. | `Planned` |
| `EXIT` | Projektlezárás és transzfer | M1–M5 bizonyítékaudit; nyitott mélyítések `Deferred` besorolása; egy kis contract- vagy tesztváltoztatás önálló elvégzése; következő tanulási irány kijelölése. | `Planned` |

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
- A `hourlyRate` nem kliensinput: a szerveroldali díjszabás rendeli hozzá a `bikeType` értékéhez. A publikus request csak a kerékpártípust és a munkaórát tartalmazza.
- A választott publikus operation: `GET /services/wage-calculation/{bikeBrand}?workHours=...`. ASP.NET route-template-ben a placeholder `{bikeBrand}`; a `:bike-brand` jelölés nem az ASP.NET route-szintaxisa.
- A `workHours` publikus típusa `decimal`, tartománya `(0, 200]`. A költség `appliedHourlyRate(brand) * workHours`, majd `Math.Ceiling` szemantikával egész HUF-ra kerekítendő.
- Az első tesztframework xUnit; a tesztek az ASP.NET Core teszthostot `WebApplicationFactory` segítségével fogják indítani.

## Open Questions

- Jelenleg nincs az `M4` ismeretlen márka tesztjét blokkoló nyitott kérdés.

## Learning Materials

- [ASP.NET Core integration tesztelési alapok](../../learning-materials/testing/aspnet-core-integration-testing-basics.md) — rövid xUnit, Arrange–Act–Assert, `WebApplicationFactory`, TestServer és red–green mentális modell.

## Next Step

A tanuló először tesztet ír az ismeretlen `bmw` márkaágra. A teszt `400 Bad Request`, `application/problem+json` media type és deszerializált `ProblemDetails` mezők (`type`, `title`, `status`, `detail`) alapján legyen piros a jelenlegi egyszerű szöveges hibaválaszon; az endpoint implementációja csak a bizonyított red állapot után következik.

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
