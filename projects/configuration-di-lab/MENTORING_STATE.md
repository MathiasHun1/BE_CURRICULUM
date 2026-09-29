# ConfigurationDiLab — mentoring state

Utolsó frissítés: 2026-09-28

Állapot: aktív. Aktív mérföldkő: `M2`.

## Current Goal

Az ismert szervizköltség-számítással bizonyítani a constructor injection, az objektumok összeállítása, a lifetime és a külső konfiguráció szerepét. A projekt véges tanulási labor, négy kötelező mérföldkővel.

## Current Checkpoint

2026-09-28, a DI-bekötés után az M3 konfigurációs szelete megkezdődött. A díjtábla az appsettings.json Pricing szekciójából Get<PricingOptions>() bindinggal érkezik; az endpoint és a kalkulátor ugyanazt a dictionary-t használja. A null és Count==0 startup guard a tényleges forrásban ellenőrzött. Az utolsó solution tesztfutás warning nélkül `9/9` zöld. A szerverkonfigurációs hiba és kliensinput saját elkülönítése helyes. A következő feladat az env override két HTTP-válaszának bemutatása; eredmény még nincs. A mérföldkőterv nem változott: M2 formálisan `Active`, az azonosítós kísérlet félretett és nem teljesült; M3 `Planned`, részleges bizonyítékkal. IOptions és ValidateOnStart még nincs az implementációban.

## Milestone Plan

| ID | Eredmény | Teljesítési bizonyíték | Állapot |
|---|---|---|---|
| `M1` | Kézi constructor injection | A tanuló a kalkulátort két kívülről átadott díjtáblával példányosítja; Ducati 1.5 órára 27000 és 36000 HUF. Az API az alapdíjas kalkulátort használja, a regressziós tesztek zöldek. Saját magyarázat a függőség létrehozójáról és fogyasztójáról, illetve a container nélküli DI-ról. | `Complete` |
| `M2` | DI container és indokolt lifetime | A kalkulátor regisztrált service-ként kerül az endpointba. Egy kis azonosítós kísérletben a transient/scoped/singleton példányazonosság megfigyelhető azonos scope-on belül és több HTTP-kérés között. A tanuló indokolja a kalkulátor választott lifetime-ját; a tesztek zöldek. | `Active` |
| `M3` | Külső konfiguráció és options | A díjtábla JSON-konfigurációból, típusos options objektumon keresztül érkezik. A tanuló környezeti változóval felülírja a Ducati óradíját 19000-re; 1.5 órára 28500 HUF, új build nélkül, újraindítás után. Megmagyarázza a provider-sorrendet; felülírás nélkül az alaptesztek zöldek. | `Planned` |
| `M4` | Hibadiagnózis és lezárás | Egy hiányzó vagy üres díjtábla és egy nem pozitív óradíj startup validation hibát ad. Egy kontrollált singleton → scoped captive dependency hiba scope validation mellett reprodukálható, megmagyarázható és javítható. A helyes konfiguráció és registration visszaállítása után a teljes tesztcsomag zöld. | `Planned` |

Egyszerre egy mérföldkő aktív. A scaffold agent által készített infrastruktúra; önmagában nem teljesíti az `M1`-et és nem emel tanulói tudásszintet.

## Exit Gate

- `M1`–`M4` kötelező teljesítési bizonyítékai elkészültek.
- A végső build tiszta, a regressziós tesztcsomag zöld.
- A tanuló önállóan megváltoztat egy óradíjat környezeti felülírással, és ellenőrzi a kapott HTTP-választ.
- Saját szavaival végigköveti az adat és a függőség útját: configuration provider → options → objektum-összeállítás → kalkulátor → HTTP-válasz.
- Megindokolja a lifetime-ot, és elkülöníti a konfigurációs hibát a kliensinput hibájától.

A feltételek teljesülése után a projekt lezárható. A tudásszintek a curriculum ledgerben, a tényleges tanulói bizonyíték alapján értékelendők.

## Curriculum Mapping

- **Elsődleges:** `WEB-DI-CONFIGURATION`, `DOTNET-DI-COMPOSITION`.
- **Közvetlen kapcsolatok:** `WEB-API-TESTING`, `WEB-BINDING-VALIDATION`, `WEB-ERROR-HANDLING`.
- **Konfigurációhatár:** `OPS-ARTIFACT-CONFIG-SECRETS`; a labor díjtáblája nem titok. Credential nem kerül a repositoryba.

## Decisions

- A felhasználó 2026-09-28-án új projektet választott, és az agentre bízta az előkészítést az önálló feladatokig.
- Hely: `projects/configuration-di-lab/`. API: `ConfigurationDiLab`; target: `net10.0`.
- A korábban egyeztetett négy mérföldkő marad a projekt útvonala.
- A kiinduló API az ismert contractot használja: `GET /services/wage-calculation/{bikeBrand}?workHours=...`, szerveroldali díjszabás, `(0, 200]` tartomány, egész HUF-ra felfelé kerekítés.
- A service első metódusa csak támogatott márkát és érvényes munkaórát kap; a HTTP-validáció az endpointban marad.
- A scaffoldhoz kilenc kész HTTP-regressziós teszteset tartozik. A későbbi DI/configuration kísérleteket a tanuló végzi.
- A saját implementáció alapértelmezetten tanulói munka. A feladat első részletes leírása a [TASKS.md](TASKS.md).
- Állapotfájlok frissítése csak explicit `update` vagy külön fájlfrissítési kérés után; az addigi bizonyítékokat ilyenkor egyben rögzítjük (2026-09-28-i felhasználói kérés).
- A konfigurációs szeletben újraindítás utáni érvényesülést vizsgálunk; reload és `IOptionsMonitor` nem exit-feltétel.
- M2 első részlépésében az alapdíjtábla meglévő instance-ként, `IReadOnlyDictionary<string, decimal>` singleton service-ként regisztrált, a kalkulátor kezdő lifetime-ja scoped. Az endpoint `ICostCalculatorService` paraméterként kapja a kalkulátort. A bekötés forrással és futással ellenőrzött; a lifetime-ok megfigyelése és a végső választás indoklása külön következő részlépés.

## Evidence

- Belépő diagnózis: az óradíj és a kerekítési szabály helyes elkülönítése; a konstruktorban kapott díjtábla és a container nélküli `new` használatának felismerése. Rövid fogalmi bizonyíték, nem önálló implementáció.
- 2026-09-28, agent által előkészített scaffold: `dotnet build ConfigurationDiLab.slnx --no-restore` sikeres, 0 warning és 0 hiba; `dotnet test ConfigurationDiLab.slnx` eredménye `9/9` zöld. A parancsok a projektkönyvtárra vonatkoznak; a tényleges futás a repository gyökeréből teljes relatív solution-útvonallal történt. A kalkulátorosztály még üres, saját DI/configuration implementáció nincs. Ez infrastruktúra-ellenőrzés, nem tanulói tudásszint-bizonyíték.

- 2026-09-28, M1 tanulói próbálkozás: a `ServiceCostCalculator.cs` fájlban a tényleges osztálynév `CostCalculatorService`, és létrejött az `ICostCalculatorService`. A konstruktor eltárolja a díjtáblát, a `Calculate` dictionary-lookupot, szorzást és felfelé kerekítést végez; a forrásvizsgálat alapján ez a számítás megfelelő érvényes inputra. A konstruktor jelenleg privát, a mező és paraméter konkrét `Dictionary`, a mező nem `readonly`. A `Program.cs` hibás `app.Use(ICostCalculatorService CostCalculatorService)` sora miatt a solution build `CS1003` hibával megáll; az endpoint még az eredeti képletet használja. Nincs új zöld tesztfutás vagy teljesített M1; a diagnózis mentor által végzett.

- 2026-09-28, vezetett javítás után: a tanuló sikeres solution buildet küldött. A tényleges forrásban a konstruktor már `public`, a mező és paraméter `IReadOnlyDictionary<string, decimal>`, és a `Program.cs` kézzel létrehozza a kalkulátort. A hibás `app.Use` sor eltűnt. A mező `readonly` módosítója még hiányzik; a handler továbbra is saját maga számítja a `TotalCost` értékét. Új tesztfutás és a második díjszabás eredménye még nincs. M1 `Active`, tudásszint-emelés nincs.

- 2026-09-28, endpoint-bekötés: a tanuló a fájlt `CostCalculatorService.cs` névre igazította, pótolta a mező `readonly` módosítóját, és az endpoint `TotalCost` mezőjét a `calculator.Calculate(normalizedBikeBrand, workHours)` hívásból adja. A tényleges forrás ellenőrzött. Az agent által futtatott `dotnet test projects/configuration-di-lab/ConfigurationDiLab.slnx --no-restore --verbosity minimal` eredménye warning nélkül `9/9` zöld. A második díjtábla és kalkulátorpéldány még nincs a kódban. M1 `Active`, a vezetett implementáció nem emeli automatikusan a tudásszintet.
- Felidézés: a tanuló a jelenlegi konstruktorhívást helyesen az induláshoz kötötte. A sikeres buildet a szintaktikai hibák hiányával indokolta; mentor által pontosítandó, hogy a fordító típusokat és hozzáférést is ellenőriz, az üzleti bekötés teljességét viszont nem bizonyítja. A konstruktor általában minden példányosításkor fut, ebben a kódban a `new` egyszer, induláskor hajtódik végre.

- 2026-09-28, két díjszabás kísérlete: a tanuló külön `hourlyRates_2` dictionary-t készített 24000 HUF-os Ducati-óradíjjal, és átadta a második kalkulátornak. Mindkét példány `Calculate("ducati", 1.5m)` hívása szerepel a forrásban; a tanulói futási eredmény 27000 és 36000. Az endpoint továbbra is az alapdíjas kalkulátort használja. A friss solution tesztfutása warning nélkül `9/9` zöld. M1 implementációs és futási bizonyítékai megvannak; a container nélküli constructor injection saját magyarázata még hiányzik, ezért M1 egyelőre `Active`.

- 2026-09-28, M1 lezáró magyarázat: a tanuló saját szavaival a dictionary-t nevezte meg függőségként, a top-level Program.cs-t annak létrehozójaként, és a konstruktorparaméteres átadással indokolta az injectiont. Az ellenőrzött kód, a 27000/36000 HUF futási eredmény és a `9/9` zöld teszt mellett az M1 összes feltétele teljesült. M1 `Complete`, M2 `Active`. A curriculum státusza a constructor injection gyakorlati bizonyítéka alapján `Practiced`, a mélység L1 marad; L2-höz további önálló alkalmazás és diagnózis hiányzik.

- 2026-09-28, M2 első implementáció: a tanuló eltávolította a kézi kalkulátorpéldányosítást és az M1 kísérleti kiírásokat, az endpointba `[FromServices] ICostCalculatorService calculator` paramétert tett, és singletonként regisztrálta a kalkulátort. A tanuló által küldött `InvalidOperationException` szerint az `IReadOnlyDictionary<string, decimal>` nem feloldható a `CostCalculatorService` aktiválásakor. A tényleges forrásban nincs díjtábla-registration; a lokális `hourlyRates` változó létrehozása önmagában nem DI registration. A hiányzó függőség diagnózisa mentor által végzett. A scoped helyett választott singleton külön eltérés az aktuális gyakorlati feladattól; a mostani kivétel oka a hiányzó registration. Új zöld tesztfutás vagy M2 teljesítés nincs.

- 2026-09-28, M2 registration-javítás után: a tanuló a `builder.Build()` előtt regisztrálta a meglévő `hourlyRates` példányt `IReadOnlyDictionary<string, decimal>` típusként, és a kalkulátor registrationjét scoped-ra állította. A forrás ellenőrzött; az endpoint `[FromServices]` paraméterként kapja a service-t. Az agent friss solution tesztfutása warning nélkül `9/9` zöld, a függőségfeloldási hiba megszűnt. Ez vezetett javítás és ellenőrzött containerhasználat; önálló DI-hibadiagnózist vagy lifetime-ismeretet még nem bizonyít. M2 `Active`.

- 2026-09-28, M2 felidézés: a tanuló helyesen azonosította a `[FromServices]` szerepét és a containerből érkező endpoint service-paramétert. A díjtábla létrehozóját nem nevezte meg helyesen, és jelezte, hogy a konstruktorba kerülése nem világos. A pontosítás a már meglévő instance regisztrációját, a container általi kalkulátorpéldányosítást és a konstruktorparaméter típusa szerinti resolutiont követi végig. Következő bizonyíték: két rövid transzferkérdés a paraméternévről és a konkrét/interfész service-típusról. Tudásszint és M2-állapot nem változik.

- 2026-09-28, M2 további felidézés: a tanuló helyesen válaszolta, hogy a konstruktorparaméter átnevezése nem befolyásolja a DI-t, a csak concrete típusra regisztrált tábla pedig nem oldja fel az interfészparamétert. Az első válasz pontos regisztrált típusa mentor által korrigált. Saját magyarázata szerint scoped esetén egy HTTP-requesthez egy példány tartozik; az alapmodell helyes. Az azonosítós kísérletet nem tartja szükségesnek, ezért most kódmódosítás helyett a kalkulátor állapotára épülő lifetime-indoklás következik. Ez nem önálló futási bizonyíték és nem teljes M2-lezárás.

- 2026-09-28, lifetime-indoklás: a tanuló felismerte, hogy a csak számoló kalkulátor singleton is lehet, és a közös mezőben tárolt bikeBrand a kérések állapotát összekeverheti. A mentor pontosította a csak olvasott díjtábla feltételét, a lokális változók és közös mezők különbségét, valamint a párhuzamos felülírás mindkét irányát. Fogalmi, részben vezetett bizonyíték; az azonosítós lifetime-kísérlet továbbra sem készült el.
- 2026-09-28, configuration binding: a tanuló elkészítette a Configuration/PricingOptions.cs osztályt, a JSON Pricing:HourlyRates táblát és a Get<PricingOptions>() hívást. Az első bekötésben a kalkulátor a JSON-t, az endpoint a korábbi beégetett táblát használta; a `9/9` teszt a megegyező alapdíjak miatt ezt nem mutatta ki. A kettős díjforrás mentor által diagnosztizált. A javított forrásban a beégetett tábla eltűnt, az endpoint és a DI-regisztráció ugyanazt a pricingOptions.HourlyRates instance-t használja. A friss regressziós tesztfutás `9/9` zöld. A JSON 19000-as próba HTTP-válasza nem érkezett meg; nem tekintjük igazoltnak.
- 2026-09-28, startup guard: a tanuló binding-magyarázata még pontatlan volt, és azt jósolta, hogy a null guard felismeri a hiányzó HourlyRates-t. A mentor pontosította a szekcióobjektum, a bound PricingOptions és a property üres kezdőértékének különbségét. A tanuló megvalósította a Count==0 ellenőrzést a Build előtt, InvalidOperationException-nel. A forrás ellenőrzött, a solution tesztfutás warning nélkül `9/9` zöld (377 ms tesztidő). A külön, ideiglenes üres konfigurációs indítás parancsát az automatikus ellenőrzés blokkolta, részletes indok nélkül; ennek runtime-bizonyítéka nincs. A tanuló az induláskor felismerés előnyét és az üres díjtábla szerverkonfigurációs / workHours=0 kliensinput-hiba különbségét helyesen megfogalmazta.
- 2026-09-28, következő kísérlet kiadva: PowerShell Pricing__HourlyRates__ducati=19000 és dotnet run --no-build, 1.5 órára 19000/28500 HTTP-válasz, majd override eltávolítása és újraindítás után 18000/27000. A JSON marad 18000. A két HTTP-válasz, a provider-sorrend és a változatlan build saját magyarázata még hiányzik.

## Deferred

- Runtime reload, saját configuration provider, named/keyed services, factory- és disposal-mélyítés: csak későbbi konkrét igénynél.
- Adatbázis, EF Core, külső szolgáltatás és deployment nem szükséges a labor exit gate-jéhez.

## Open Questions

Nincs az aktuális feladatot blokkoló nyitott döntés.

## Learning Materials

- [Dependency injection és konfiguráció — backend alapok](../../learning-materials/web-backend/dependency-injection-and-configuration.md) — jelenleg a 3. olvasási lépés configuration provider és környezeti felülírás része aktuális. A Get<T> binding és a startup guard nem igazolja önmagában az IOptions/ValidateOnStart használatát. Az olvasás nem tudásszint-bizonyíték.

## Next Step

A tanuló elvégzi a [TASKS.md](TASKS.md) környezeti változós felülírási kísérletét: változatlan JSON és --no-build mellett előbb 19000/28500, majd az override törlése és újraindítás után 18000/27000 HTTP-választ küld. A mérföldkövek lezárását és az M2 félretett kísérletének sorsát külön értékeljük; nem jelöljük automatikusan késznek.
