# ASP.NET Core integration tesztelési alapok

Utolsó linkellenőrzés: 2026-09-18  
Technológiai kontextus: .NET 10, ASP.NET Core Minimal API, xUnit v2, `WebApplicationFactory`

## Miért aktuális?

Az API Contract Testing projektben elkészült az első automatizált HTTP contractteszt. A teszt `200 OK` választ vár a szervizköltség endpointtól, de az endpoint hiánya miatt jelenleg helyesen `404 Not Found` eredménnyel bukik.

A cél most nem a teljes tesztelési elmélet megtanulása. Elég megérteni:

- mit csinál az xUnit test runner;
- mi a `[Fact]`, az assertion és az Arrange–Act–Assert felosztás;
- hogyan indít teszthostot a `WebApplicationFactory<Program>`;
- mit bizonyít és mit nem bizonyít az in-memory HTTP integration teszt;
- miért hasznos előbb látni a tesztet piros, majd zöld állapotban.

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

### 1. Először a saját piros teszt

Nyisd meg a projekt `WageCalculationContractTests` osztályát, és azonosítsd:

- **Arrange:** a kliens létrehozása;
- **Act:** a `GetAsync(...)` hívás;
- **Assert:** a várt `OK` és a tényleges státusz összehasonlítása.

A `404` itt nem hibás tesztet jelent. Azt bizonyítja, hogy a teszt eléri az alkalmazást, és érzékeli a hiányzó viselkedést.

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

A jelenlegi projekt xUnit `2.9.3` verziót használ, ezért a v2 dokumentáció illeszkedik hozzá.

### 4. Olvasható teszt szerkezete

[Best practices for writing unit tests — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

Bár a mostani teszt integration teszt, ebből az oldalról három általános elvet használunk:

- Arrange–Act–Assert;
- beszédes tesztnév;
- tesztenként egy lényegi Act.

## Mit bizonyít a jelenlegi teszt?

- A solution és a project reference működik.
- Az xUnit megtalálja és lefuttatja a tesztet.
- A `WebApplicationFactory` fel tudja építeni az API teszthostját.
- A `HttpClient` kérése eljut az ASP.NET Core routingig.
- A teszt felismeri, hogy a contract szerinti endpoint még hiányzik.

## Mit nem bizonyít?

- Nem használ valódi Kestrel listening socketet vagy TCP-portot.
- Nem ellenőrzi a TLS-t, proxyt vagy deploymentet.
- A jelenlegi egyetlen assertion még nem ellenőrzi a content type-ot vagy a JSON body-t.
- Nem bizonyítja a hibás contractágak működését.

## Kapcsolódó gyakorlati kísérletek

1. **Red:** a hiányzó endpoint miatt `Expected: OK`, `Actual: NotFound`.
2. **Green:** a legkisebb sikeres endpoint implementációja után ugyanaz a teszt zöld lesz.
3. **Erősebb contract:** külön assertion a content type-ra és a JSON mezőkre.
4. **Kontrollált törés:** egy response mező átnevezése piros tesztet okoz, majd a contract helyreállítása ismét zöldet.

## Opcionális mélyítés

Csak később, valós igény esetén:

- custom `WebApplicationFactory` és tesztkonfiguráció;
- service-ek lecserélése teszt double-ra;
- tesztadatbázis és izoláció;
- authentication tesztelése;
- `[Theory]` több bemeneti esettel;
- párhuzamos tesztfuttatás és megosztott állapot.

## Ellenőrző kérdések

- Mi indítja el a tesztet, és mi dönti el, hogy sikeres-e?
- Mi a `WebApplicationFactory<Program>` feladata?
- Miért nem kellett portot megadni a `HttpClient` számára?
- Miért hasznos, hogy a tesztet az endpoint implementálása előtt pirosnak láttuk?
- Miben több ez a teszt annál, mintha közvetlenül meghívnánk egy számoló metódust?
