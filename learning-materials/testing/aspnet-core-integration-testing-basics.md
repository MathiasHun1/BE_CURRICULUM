# ASP.NET Core integration tesztelési alapok

Utolsó linkellenőrzés: 2026-09-22
Technológiai kontextus: .NET 10, ASP.NET Core Minimal API, xUnit v2, `WebApplicationFactory`

## Miért aktuális?

Az API Contract Testing projektben öt publikus HTTP-contractteszt készült, és a teljes suite `5/5` zöld. A tesztelés mélyítése most későbbre kerül. A napi munkához az a minimum fontos, amellyel a generált teszteket is értelmesen lehet átnézni.

A rövid áttekintés célja:

- mit csinál az xUnit test runner;
- mi a `[Fact]`, az assertion és az Arrange–Act–Assert felosztás;
- hogyan indít teszthostot a `WebApplicationFactory<Program>`;
- mit bizonyít és mit nem bizonyít az in-memory HTTP integration teszt;
- miért nem bizonyít mindent a zöld suite vagy a magas coverage;
- hogyan ellenőrizhető, hogy egy generált teszt a valódi contractot védi-e.

## Rövid mentális modell

```text
dotnet test
  → test host
  → xUnit megtalálja a [Fact] tesztet
  → WebApplicationFactory<Program>
  → in-memory TestServer
  → HttpClient request
  → ASP.NET Core pipeline és endpoint
  → HttpResponseMessage
  → Assert: várt érték kontra tényleges érték
```

A tesztprojekt project reference-szel hivatkozik az API-projektre. A `Microsoft.AspNetCore.Mvc.Testing` csomag adja a `WebApplicationFactory` infrastruktúrát. A teszt nem indít külső processt vagy valódi TCP-portot, de lefuttatja a routingot, bindingot, endpointot és response-képzést.

## Ajánlott olvasási sorrend

### 1. Először a saját tesztmátrix

Nyisd meg a projekt `WageCalculationContractTests` osztályát. Öt ellenőrzött példa van: sikeres válasz, ismeretlen márka, két érvénytelen munkaóra és hiányzó route-szegmens. Azonosítsd bennük:

- **Arrange:** a kliens létrehozása;
- **Act:** a `GetAsync(...)` hívás;
- **Assert:** a státusz, media type és a publikus JSON-mezők összehasonlítása.

A hiányzó route-szegmens tesztje a korábbi üres `404` válasznál előbb a media type-on bukott; a `StatusCodePages` és Problem Details beállítása után zöld lett. Ez konkrét példa arra, hogy egy célzott piros teszt a contracteltérés helyét is megmutatja.

### 2. ASP.NET Core integration tesztek

[Integration tests in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0)

Most csak ezeket a részeket olvasd:

1. *Introduction to integration tests*
2. *ASP.NET Core integration tests*
3. *Basic tests with the default WebApplicationFactory*

Cél: értsd a SUT, test host, `TestServer`, `WebApplicationFactory` és `HttpClient` kapcsolatát. A custom factory, adatbázis, authentication és további konfiguráció most még nem szükséges.

### 3. xUnit alapok

[Getting Started with xUnit.net v2 — xUnit.net](https://xunit.net/docs/getting-started/v2/getting-started)

Most csak ezekre figyelj:

- `[Fact]`: egy konkrét, paraméter nélküli teszteset;
- `[Theory]`: ugyanaz a viselkedés több bemeneti adattal;
- `Assert`: a teszt automatikus pass/fail döntése;
- `dotnet test`: build, tesztfelfedezés, futtatás és eredményjelentés.

A projekt xUnit v2-t használ, ezért a v2 dokumentáció illeszkedik hozzá.

### 4. Olvasható teszt szerkezete

[Best practices for writing unit tests — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

Bár a projektben integration tesztek vannak, ebből az oldalból négy általános elv hasznos:

- Arrange–Act–Assert;
- beszédes tesztnév;
- tesztenként egy lényegi Act.
- ismételhető eredmény és a publikus viselkedésre összpontosító assertion.

## Mit bizonyít a jelenlegi suite?

- A solution és a project reference működik.
- Az xUnit megtalálja és lefuttatja a tesztet.
- A `WebApplicationFactory` fel tudja építeni az API teszthostját.
- A `HttpClient` kérése eljut az ASP.NET Core routingig.
- A kiválasztott öt HTTP-kérés válasza a tesztelt státusz, media type és JSON-mezők szerint megfelel az elfogadott contractnak.

## Mit nem bizonyít?

- Nem használ valódi Kestrel listening socketet vagy TCP-portot.
- Nem ellenőrzi a TLS-t, proxyt vagy deploymentet.
- Nem bizonyít nem tesztelt bemeneteket, minden kerekítési határt vagy későbbi contractváltoztatás kompatibilitását.
- Nem bizonyítja, hogy az elvárt értékek helyesek: egy generált teszt a hibás implementáció értékeit is átveheti.
- A coverage csak azt mutatja, hogy mely kódrészek futottak teszt közben; önmagában nem mutatja meg, hogy az assertion felismerne-e hibás viselkedést.

## Minimum ellenőrzés generált teszthez

1. **Melyik kockázatot védi?** A sikeres ág mellett van-e fontos hibás vagy határeset?
2. **Honnan jön az elvárt érték?** A jóváhagyott contractból vagy csupán a jelenlegi implementációból?
3. **Mi bukna el egy hibánál?** Az assertion a publikus viselkedést figyeli-e, és tud-e hasznos hibaüzenetet adni?
4. **Megismételhető-e?** Nem függ-e véletlen sorrendtől, órától, külső állapottól vagy megosztott tesztadattól?

Példa a jelenlegi suite vakfoltjára: `18 000 * 1.5 = 27 000`, ezért ez az egy sikeres példa nem különbözteti meg a `Math.Ceiling` és `Math.Floor` kerekítést. Ez nem mostani új feladat, hanem emlékeztető arra, hogy a tesztadatnak különbséget kell tennie a fontos viselkedések között.

## Kapcsolódó gyakorlati kísérletek

1. Kódmódosítás nélkül nevezd meg, melyik meglévő teszt bukna, ha a sikeres válasz `currency` mezője `EUR` lenne.
2. Kódmódosítás nélkül keress olyan kerekítési bemenetet, amelynél a `Math.Ceiling` és `Math.Floor` más eredményt adna.
3. Valós új feature-nél válaszd ki az egy legfontosabb hibás ágat, és ellenőrizd, hogy a generált teszt a publikus contractot védi-e.

## Opcionális mélyítés

Csak később, valós igény esetén:

- custom `WebApplicationFactory` és tesztkonfiguráció;
- service-ek lecserélése teszt double-ra;
- tesztadatbázis és izoláció;
- authentication tesztelése;
- `[Theory]` több bemeneti esettel;
- párhuzamos tesztfuttatás és megosztott állapot;
- mutation testing és kockázatalapú tesztstratégia.

## Ellenőrző kérdések

- Mi indítja el a tesztet, és mi dönti el, hogy sikeres-e?
- Mi a `WebApplicationFactory<Program>` feladata?
- Miért nem kellett portot megadni a `HttpClient` számára?
- Miért hasznos, hogy a tesztet az endpoint implementálása előtt pirosnak láttuk?
- Miben több ez a teszt annál, mintha közvetlenül meghívnánk egy számoló metódust?
