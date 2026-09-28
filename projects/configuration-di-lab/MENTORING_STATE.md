# ConfigurationDiLab — mentoring state

Utolsó frissítés: 2026-09-28

Állapot: aktív. Aktív mérföldkő: `M1`.

## Current Goal

Az ismert szervizköltség-számítással bizonyítani a constructor injection, az objektumok összeállítása, a lifetime és a külső konfiguráció szerepét. A projekt véges tanulási labor, négy kötelező mérföldkővel.

## Milestone Plan

| ID | Eredmény | Teljesítési bizonyíték | Állapot |
|---|---|---|---|
| `M1` | Kézi constructor injection | A tanuló a kalkulátort két kívülről átadott díjtáblával példányosítja; Ducati 1.5 órára 27000 és 36000 HUF. Az API az alapdíjas kalkulátort használja, a regressziós tesztek zöldek. Saját magyarázat a függőség létrehozójáról és fogyasztójáról, illetve a container nélküli DI-ról. | `Active` |
| `M2` | DI container és indokolt lifetime | A kalkulátor regisztrált service-ként kerül az endpointba. Egy kis azonosítós kísérletben a transient/scoped/singleton példányazonosság megfigyelhető azonos scope-on belül és több HTTP-kérés között. A tanuló indokolja a kalkulátor választott lifetime-ját; a tesztek zöldek. | `Planned` |
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
- A konfigurációs szeletben újraindítás utáni érvényesülést vizsgálunk; reload és `IOptionsMonitor` nem exit-feltétel.

## Evidence

- Belépő diagnózis: az óradíj és a kerekítési szabály helyes elkülönítése; a konstruktorban kapott díjtábla és a container nélküli `new` használatának felismerése. Rövid fogalmi bizonyíték, nem önálló implementáció.
- 2026-09-28, agent által előkészített scaffold: `dotnet build ConfigurationDiLab.slnx --no-restore` sikeres, 0 warning és 0 hiba; `dotnet test ConfigurationDiLab.slnx` eredménye `9/9` zöld. A parancsok a projektkönyvtárra vonatkoznak; a tényleges futás a repository gyökeréből teljes relatív solution-útvonallal történt. A kalkulátorosztály még üres, saját DI/configuration implementáció nincs. Ez infrastruktúra-ellenőrzés, nem tanulói tudásszint-bizonyíték.

## Deferred

- Runtime reload, saját configuration provider, named/keyed services, factory- és disposal-mélyítés: csak későbbi konkrét igénynél.
- Adatbázis, EF Core, külső szolgáltatás és deployment nem szükséges a labor exit gate-jéhez.

## Open Questions

Nincs az első feladatot blokkoló nyitott döntés.

## Learning Materials

- [Dependency injection és konfiguráció — backend alapok](../../learning-materials/web-backend/dependency-injection-and-configuration.md) — a négy mérföldkőhöz igazított hivatalos források, olvasási sorrend, gyakorlati kísérletek és opcionális mélyítés. M1 előtt az első olvasási lépés aktuális. Az anyag elkészítése vagy elolvasása nem tudásszint-bizonyíték.

## Next Step

A tanuló elvégzi a [TASKS.md](TASKS.md) `M1` feladatát: constructor injection, kézi példányosítás, endpoint-bekötés, két díjszabás futási bizonyítéka és zöld regressziós tesztek.
