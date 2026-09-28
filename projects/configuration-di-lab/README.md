# ConfigurationDiLab

Kis ASP.NET Core Minimal API labor `net10.0` targettel. Az ismert szervizköltség-számításból indulunk: különválasztjuk a díjadatokat, a számítási viselkedést és az objektumok összeállítását.

## Indulás

A repository gyökeréből:

```powershell
dotnet build projects/configuration-di-lab/ConfigurationDiLab.slnx
dotnet test projects/configuration-di-lab/ConfigurationDiLab.slnx
dotnet run --project projects/configuration-di-lab/ConfigurationDiLab --launch-profile http
```

Az API címe `http://localhost:5187`. A futó alkalmazást `Ctrl+C` állítja le. A HTTP-példák a [ConfigurationDiLab.http](ConfigurationDiLab/ConfigurationDiLab.http) fájlban vannak.

Példa PowerShellből:

```powershell
Invoke-RestMethod 'http://localhost:5187/services/wage-calculation/ducati?workHours=1.5'
```

## Mi készült el?

- Solution, API- és integrationteszt-projekt, indítási profil.
- Ismert, működő kiinduló endpoint szerveroldali díjtáblával, DTO-val és Problem Details hibaválaszokkal.
- Kilenc kész regressziós teszteset a későbbi átalakítás viselkedésének ellenőrzésére.
- Üres [ServiceCostCalculator](ConfigurationDiLab/Services/ServiceCostCalculator.cs) osztály az első feladathoz.

A kiinduló handler a korábbi példát követi. A kalkulátor, saját DI registration, óradíj-konfiguráció és hibakísérletek tanulói feladatok. A kész tesztek jelenleg a HTTP-viselkedést ellenőrzik; zöld eredményük önmagában nem bizonyít constructor injectiont vagy helyes lifetime-ot.

Ellenőrzött kiinduló állapot (2026-09-28): build 0 hibával és 0 warninggal; tesztek `9/9` zöld.

## Hol folytasd?

- [TASKS.md](TASKS.md): az aktuális, egyetlen implementációs lépés és elfogadási feltételei.
- [MENTORING_STATE.md](MENTORING_STATE.md): a négy mérföldkő, bizonyítékok és exit gate.
- [DI és konfiguráció tananyag](../../learning-materials/web-backend/dependency-injection-and-configuration.md): a mérföldkövekhez igazított olvasási sorrend és hivatalos források.

Az első feladat a kézi constructor injection. A későbbi feladatok részleteit az elkészült kód alapján bontjuk ki.
