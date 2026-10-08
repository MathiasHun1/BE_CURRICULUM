# ConfigurationDiLab — mentoring state

Utolsó frissítés: 2026-10-07

Állapot: lezárt alapozó labor. M1–M4 `Complete`; nincs aktív mérföldkő.

## Current Goal

Az ismert szervizköltség-számítással bizonyítani a constructor injection, az objektumok összeállítása, a lifetime és a külső konfiguráció szerepét. A projekt véges tanulási labor, négy kötelező mérföldkővel.

## Current Checkpoint

2026-10-07: a DI/configuration alapozó szelet a chatben lezárva. A kalkulátor scoped, a konstruktor és a handler is `IOptionsSnapshot<PricingOptions>`-t kér. Az `AddOptions → Bind → Validate → ValidateOnStart` és az `OrdinalIgnoreCase` dictionary megmaradt. A régi kommentelt guardok eltűntek. A JSON Ducati-díja ismét 18000.

A `/debug/lifetime` endpoint két `LifeTimeProbe` paramétert hasonlít össze GUID-okkal és `sameInstance` mezővel. A probe végső regisztrációja transient, az `InstanceId` csak olvasható auto-property egyszeri inicializálással. Scoped/transient működés agent HTTP-próbákkal igazolt; singleton helyes következtetése a felhasználó kifejezett kérésére elfogadva a kísérlet teljesítéséhez, külön singleton probe-futás nélkül.

Üres díjtábla OptionsValidationException hibája és singleton kalkulátor → scoped snapshot hiba bemutatva, javítás ellenőrizve. Utolsó agent-futtatás: `dotnet test projects/configuration-di-lab/ConfigurationDiLab.slnx --verbosity minimal`, 9 passed, 0 failed, 0 skipped, 441 ms; buildhiba vagy warning nem jelent meg.

A lezárás az alapozó gyakorlat elfogadását jelenti. A magyarázatok több ponton vezetett pontosítást igényeltek; a két fő curriculum-téma L1 / Practiced marad. A következő irány async I/O/cancellation; belépő kérdés kiadva, még megválaszolatlan.

## Milestone Plan

| ID | Eredmény | Teljesítési bizonyíték | Állapot |
|---|---|---|---|
| `M1` | Kézi constructor injection | A tanuló a kalkulátort két kívülről átadott díjtáblával példányosítja; Ducati 1.5 órára 27000 és 36000 HUF. Az API az alapdíjas kalkulátort használja, a regressziós tesztek zöldek. Saját magyarázat a függőség létrehozójáról és fogyasztójáról, illetve a container nélküli DI-ról. | `Complete` |
| `M2` | DI container és indokolt lifetime | A kalkulátor regisztrált service-ként kerül az endpointba. Scoped/transient példányazonosság megfigyelése azonos scope-on belül és több HTTP-kérés között; singleton esetén a helyes tanulói következtetés a felhasználó kérésére elfogadott a futási próba helyett. A tanuló indokolja a kalkulátor választott lifetime-ját; a tesztek zöldek. | `Complete` |
| `M3` | Külső konfiguráció és options | A díjtábla JSON-konfigurációból, típusos options objektumon keresztül érkezik. A tanuló környezeti változóval felülírja a Ducati óradíját 19000-re; 1.5 órára 28500 HUF, új build nélkül, újraindítás után. Megmagyarázza a provider-sorrendet; felülírás nélkül az alaptesztek zöldek. | `Complete` |
| `M4` | Hibadiagnózis és lezárás | Egy hiányzó vagy üres díjtábla és egy nem pozitív óradíj startup validation hibát ad. Egy kontrollált singleton → scoped captive dependency hiba scope validation mellett reprodukálható, megmagyarázható és javítható. A helyes konfiguráció és registration visszaállítása után a teljes tesztcsomag zöld. | `Complete` |

Aktív projektben egyszerre egy mérföldkő aktív; a lezárt laborban nincs aktív mérföldkő. A scaffold agent által készített infrastruktúra; önmagában nem teljesíti az `M1`-et és nem emel tanulói tudásszintet.

2026-10-07-i lezárási értékelés: a mérföldkövek alapozó eredményeit a fenti, eltérő erősségű bizonyítékokkal elfogadtuk. A singleton probe következtetésének elfogadása explicit felhasználói döntés. A futás, tanulói sikerjelzés és vezetetten javított magyarázat külön dokumentált; ezek nem jelentenek automatikus L2-t.

## Exit Gate

- `M1`–`M4` kötelező teljesítési bizonyítékai elkészültek.
- A végső build tiszta, a regressziós tesztcsomag zöld.
- A tanuló önállóan megváltoztat egy óradíjat környezeti felülírással, és ellenőrzi a kapott HTTP-választ.
- Saját szavaival végigköveti az adat és a függőség útját: configuration provider → options → objektum-összeállítás → kalkulátor → HTTP-válasz.
- Megindokolja a lifetime-ot, és elkülöníti a konfigurációs hibát a kliensinput hibájától.

2026-10-07: az alapozó szelet lezárását a chatben elfogadtuk. Az exit gate eredményei a mérföldkőtáblában és az Evidence alatt. Bizonyítékkorlát: az env override teljes HTTP-válasza nincs megőrizve, a végső összefüggő magyarázat sorrendje és számtani eredménye mentor által korrigált. Ezt vezetett alapozó lezárásként, nem teljesen önálló vizsgaként rögzítjük. Az önálló alkalmazás és késleltetett felidézés a curriculum nyitott része; nem hosszabbítjuk automatikusan a labort.

## Curriculum Mapping

- **Elsődleges:** `WEB-DI-CONFIGURATION`, `DOTNET-DI-COMPOSITION`.
- **Közvetlen kapcsolatok:** `WEB-API-TESTING`, `WEB-BINDING-VALIDATION`, `WEB-ERROR-HANDLING`.
- **Konfigurációhatár:** `OPS-ARTIFACT-CONFIG-SECRETS`; a labor díjtáblája nem titok. Credential nem kerül a repositoryba.

## Decisions

- 2026-10-07: a felhasználó kérésére a singleton probe helyes következtetése elfogadott az M2 teljesítéséhez; nincs újabb kötelező singleton futás. A labor alapozó szeletét lezártuk; a következő irány async/cancellation, a tudásszintek L1-en maradnak.

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

### 2026-10-06 — környezeti felülírás, diagnózis és options

- Környezeti változó: a tanuló PowerShellben 19000-et olvasott vissza, miközben az API `400 Unsupported bike brand` választ adott az agent kérésére. Rider Attach to Process és Evaluate Expression vezetett használatával bemutatta a tényleges táblát: `aprilia=20000`, `Ducati=19000`, `honda=12000`, `kawasaki=15000`, `suzuki=10000`. A mentor a kisbetűs kereséssel összevetve diagnosztizálta a hibát. A tanuló `new(StringComparer.OrdinalIgnoreCase)` inicializálással javított, az agent a forrást ellenőrizte.
- A tanuló 19000-es eredményt, majd az override eltávolítása és újraindítás után sikert jelzett. Teljes HTTP-választ nem küldött. A JSON Ducati-alapdíja az ellenőrzéskor 18000 volt. Kódjavításkor build kellett; a konfigurációcsere önmagában nem igényel új buildet, ezt a tanuló saját szavaival helyesen megfogalmazta.
- Windows env-casing: az agent külön, ideiglenes változóval reprodukálta, hogy a korábban nagy D-vel létrehozott név kisbetűs értékadás után is megőrizheti írásmódját. A tanuló megerősítette, hogy nála ez történt. Process-környezet, memóriabeli tárolás, gyermekfolyamat öröklése és az újraindítás oka mentor által magyarázott. A TryGetValue sikertelensége az out változónak ad alapértéket, nem a tárolt díjat írja át; ez mentor által pontosított.
- A tanuló foreach alapú nem pozitív díj-ellenőrzést írt a Build elé, és bemásolta a futási hibát: `System.InvalidOperationException: Invalid hourly rate for ducati: -3000`. Felismerte, hogy üres kollekciónál a ciklus nem fog hibát, ezért a Count ellenőrzés külön kell; dictionary/array szóhasználat pontosítva. A JSON -3000 és env 19000 példában az env értékét helyesen nevezte meg a validáció bemeneteként.
- Options átállás: a tanuló a kalkulátorban IOptions-t használt, de először az endpointban concrete PricingOptions-t kért, és csak AddOptions-t regisztrált Bind nélkül. Az agent forrásvizsgálata azonosította mindkét hiányt; a build ettől még 0 warning/0 error eredményű volt. Vezetett javítás után mindkét fogyasztó IOptions-t kap, a Bind a Pricing szekcióra mutat. A külön dictionary-regisztráció megszűnt.
- Validáció: az első Validate lambda saját InvalidOperationException-t dobott (`Invalid hourly rate for suzuki: 0`, tanuló által bemásolva). A mentor a bool predicate és a külön failureMessage szerepét pontosította. A javított kódban null/üres tábla és nem pozitív díj esetén false az eredmény, a végén true; ValidateOnStart szerepel. A tanuló jelezte, hogy a várt OptionsValidationException érkezik. Ennek teljes stack trace-ét nem másolta be. Az üres/hiányzó konfiguráció elutasítása forrásból következik, külön futása nincs igazolva. A régi kézi guardok kommentként még a Program.cs-ben vannak.
- Regresszió: az agent `dotnet test projects/configuration-di-lab/ConfigurationDiLab.slnx --no-restore --verbosity minimal` futása sikeres: 9 passed, 0 failed, 0 skipped, 376 ms; warning nem jelent meg. A JSON ekkor helyes alapdíjakat tartalmazott.
- Felidézés: a tanuló az IOptions-t wrapperként, a Value-t az options eléréseként, a Bind-ot betöltésként és a Validate-et ellenőrzésként helyesen írta le; interface/property és regisztráció/végrehajtás megkülönböztetése mentor által pontosított. ValidateOnStart nélkül az első Value-hozzáférés időzítését nem tudta, magyarázat után helyesen következtetett arra, hogy a `/` végpont nem váltaná ki a validációt. A WEB-DI-CONFIGURATION L1 / Practiced szintet kap az ellenőrzött alkalmazás és saját alapmagyarázatok alapján; önálló L2-diagnózist nem állítunk.
- Következő kísérlet kiadva: csak a kalkulátor konstruktorában IOptions → IOptionsSnapshot csere, a registrationben AddScoped → AddSingleton, majd Development indítás új builddel és a scope-validation hiba bemutatása. A tanuló még nem küldött eredményt, az update-kori forrásban a két változtatás sincs meg.

### 2026-10-07 — lifetime, snapshot, validáció és lezárás

- Captive dependency: az első módosítás csak a handlerbe tett snapshotot, a singleton kalkulátor IOptions-t kapott, ezért nem volt lifetime-hiba. Az agent ezt forrásból diagnosztizálta. A konstruktor snapshotra állítása után a tanuló service descriptor validation InvalidOperationException részletet küldött; a teljes belső kivételszöveg nem érkezett meg. A hibás singleton/scoped gráf forrással igazolt. AddScoped-ra visszaállítva a hiba megszűnt; a tanuló a lifetime-párokat helyesen nevezte meg.
- Snapshot-konzisztencia: a handler IOptions és a kalkulátor snapshot vegyes használatából eredő eltérést mentor magyarázta. Mindkét fogyasztó snapshotra állítása ellenőrzött. A JSON 20000-es futás közbeni módosítására a tanuló sikert jelzett, teljes válasz nélkül. A scoped választás közvetlen oka a scoped snapshotfüggőség; a tanuló először önmagában a runtime configváltozással indokolt. Az IOptionsMonitor singleton alternatíva mentor által bevezetett, nem gyakorolt.
- Üres tábla: üres HourlyRates JSON ellenőrizve; bemásolt hiba: `Microsoft.Extensions.Options.OptionsValidationException: Pricing options are invalid`. Saját magyarázat: a dictionary nem null, nincs foreach-iteráció, Count==0 nélkül a végső true jönne. A startup hibát először fordításkorinak mondta; pontosítás után helyesen állította, hogy runtime betöltés miatt újrafordítás nélkül is meghiúsulhat az indítás.
- Regresszió: a bent maradt 20000-es Ducati-díjjal agent 7 passed / 2 failed eredmény (expected 18000, actual 20000). Az alapdíj 18000-re visszaállt; a végső teljes tesztfutás 9 passed / 0 failed / 0 skipped, 441 ms, warning nélkül. Ez a munkamenet korábbi futása; az update miatt nem ismételtük.
- Scoped probe: két agent HTTP-kérésben az azonosítópárok 209a597e…/209a597e… és 39ed0529…/39ed0529…, mindkét sameInstance true. Egy scope-ban azonos, külön kérésekben külön példány igazolt.
- Transient probe: két agent HTTP-kérésben bf7f4a72…/168f3f4b… és 9de48b18…/bd0201b1…, mindkét sameInstance false. A tanuló előre helyesen jósolta a külön példányokat, mert két paraméterhez két feloldás tartozik.
- Singleton probe: a tanuló helyesen jósolta az azonosságot kérésen belül és kérések között. A kód és az ellenőrzött futás még transient volt. Ezután a felhasználó kifejezetten kérte a helyes következtetés elfogadását (`tekintsd annak, egyertelmu`); a kísérletet ennek megfelelően lezártuk. Ez elfogadott feladatbizonyíték, nem megfigyelt singleton futás.
- C# mellékszál: az InstanceId nyilvános mezőből readonly mező, majd `public Guid InstanceId { get; } = Guid.NewGuid();` auto-property lett. Mező/property, getter, rejtett tárolómező és getterenként új GUID különbsége magyarázva; önálló fogalmi visszamérés még nincs.
- Felidézés: a tanuló megkülönböztette a külön HTTP-kérések scope-jait, ugyanazon scope két feloldását és a regisztrációtól különálló példányosítást. Helyesen mondta, hogy a regisztráció nem fut újra kérésenként, és pusztán AddScoped miatt még nincs kalkulátorpéldány. A kezdeti snapshot miatti példányazonosság-indoklás mentor által javított.
- Lezáró lánc: az env 19000-es prioritását helyesen nevezte meg, de a végösszeget kétszer 27000-nek adta; a 28500 mentor által korrigált. A startup regisztráció/validáció és a HTTP-kéréshez tartozó példányosítás sorrendje is pontosítást igényelt. Az utolsó kérdésre helyesen válaszolta, hogy az első feloldás előtt nincs kalkulátorpéldány. Alapozó lezárás elfogadva; L1 / Practiced marad.
- Átvezetés: WEB-ASYNC-CANCELLATION / DOTNET-ASYNC-TASKS belépő kérdése kiadva: 3 másodperces külső HTTP-várakozás alatt mi történik a szállal, és hoz-e létre új szálat az async? Még nincs válasz, új projekt vagy implementáció.

## Deferred

- Saját configuration provider, named/keyed services, factory/disposal és IOptionsMonitor részletes alkalmazása: későbbi konkrét igénynél. A snapshotos JSON-reload alapkísérlet tanulói sikerjelzést kapott.
- Önálló, új kontextusú DI/config diagnózis és a teljes startup → kérés → válasz lánc késleltetett felidézése a curriculum L2-höz szükséges nyitott része.
- Adatbázis, EF Core, külső szolgáltatás és deployment nem kerül utólag a lezárt laborba.

## Open Questions

Nincs a lezárt laborban aktív feladat. A következő async-projekt helye és terve még nincs egyeztetve; előbb a globális belépő kérdés következik.

## Learning Materials

- [Dependency injection és konfiguráció — backend alapok](../../learning-materials/web-backend/dependency-injection-and-configuration.md) — a lezárt labor referenciaanyaga; a bizonyítékok fent. Az olvasás önmagában nem tudásszint-bizonyíték.

## Next Step

A labor lezárt. A következő lépés a [globális állapotban](../../LEARNING_STATE.md) rögzített async belépő kérdés; ebben a projektben nincs új implementációs feladat.
