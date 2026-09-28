# Dependency injection és konfiguráció — backend alapok

Utolsó linkellenőrzés: 2026-09-28
Technológiai kontextus: .NET 10, ASP.NET Core Minimal API
Kapcsolódó altémák: `DOTNET-DI-COMPOSITION`, `WEB-DI-CONFIGURATION`

## Miért aktuális?

A [ConfigurationDiLab](../../projects/configuration-di-lab/README.md) az ismert szervizköltség-számítással teszi láthatóvá a díjadatok, a számítás és az objektumok összeállításának felelősségét. Az első feladat a kézi constructor injection; a container, lifetime és konfiguráció a következő mérföldköveken kerül elő.

**Most az 1. olvasási lépést végezd el, majd térj vissza az M1 implementációhoz.** A teljes anyagot a labor négy mérföldkövével együtt dolgozd fel. Az olvasás nem helyettesíti a kódot, a futási bizonyítékot és a saját magyarázatot.

## Rövid mentális modell

- **Függőség:** adat vagy objektum, amelyre egy másik objektumnak szüksége van a feladatához.
- **Constructor injection:** a fogyasztó a konstruktorában kapja meg a függőségét; kézi `new` hívással is működik.
- **DI container:** a regisztrációk alapján összeállítja az objektumgráfot és kezeli a rá bízott példányok élettartamát.
- **Composition root:** az alkalmazás központi összeállítási helye; a laborban ez a `Program.cs`.

Az M1 útja: díjtábla létrehozása → átadás a kalkulátor konstruktorának → számítás → HTTP-válasz. A függőséget előállító kód és az azt használó kalkulátor külön felelősséget kap. A külön osztályba kiemelés mellett külön döntés az is, hogyan kapja meg az osztály a szükséges adatokat. [Alapfogalmak: .NET DI overview](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/overview).

## Ajánlott olvasási sorrend

### 1. M1 előtt: a függőség átadása

[.NET dependency injection — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/overview)

Olvasd a bevezetést és a **The concept** részt. Figyeld meg a saját maga által létrehozott függőség és a konstruktorban kapott függőség különbségét. A Worker és `BackgroundService` példákban az objektumok kapcsolatára koncentrálj; háttérfeldolgozót most nem kell készítened.

**Cél:** tudd megnevezni, ki állítja össze és ki használja a függőséget. Az interfész és a container hasznos eszköz, de a constructor injectionhez egyik sem kötelező.

**Kísérlet:** a [labor M1 feladatában](../../projects/configuration-di-lab/TASKS.md) ugyanazt a kalkulátorosztályt példányosítsd két díjtáblával. Ducati 1.5 órára az alapdíjas eredmény `27000`, a második eredmény `36000` HUF. A különbséget kizárólag az átadott adat okozza.

### 2. M2 előtt: registration, resolution és lifetime

[Dependency injection in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0)

Olvasd a **Service registration methods**, majd a **Lifetime and registration options** részt. A webes példákban az alkalmazásindításkor megadott regisztráció és a fogyasztó számára feloldott példány kapcsolatát keresd.

[Service lifetimes — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/service-lifetimes)

A három élettartamot innen olvasd el:

| Lifetime | Új példány mikor keletkezik? | Mit figyelj a kísérletben? |
|---|---|---|
| `Transient` | Minden feloldáskor | Két feloldás ugyanabban a scope-ban is eltérő példányt ad. |
| `Scoped` | Scope-onként az első feloldáskor | Azonos scope-ban ugyanaz, két külön request scope-ban eltérő. |
| `Singleton` | A service providerben az első feloldáskor | Ugyanabból a providerből több request is ugyanazt kapja. |

Az ASP.NET Core HTTP-kérések saját scope-ot kapnak. A singletonban tárolt módosítható állapotot több kérés egyszerre érheti el; a választásnál a függőségek és a szálbiztonság is számít. Egy `readonly` dictionary-mező a referencia újraértékadását korlátozza, a dictionary tartalmát nem fagyasztja be.

**Cél:** különítsd el a service regisztrációját, az objektum feloldását és a példány élettartamát.

**Kísérlet:** egy példány létrejöttekor képzett azonosítót figyelj meg két feloldásnál ugyanabban a kérésben, majd egy új kérésben. Az előre megjósolt eredményt vesd össze a futással. A számítás eredménye önmagában nem árulja el a lifetime-ot.

### 3. M3 előtt: honnan érkezik a konfiguráció?

[Configuration in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0)

Olvasd a **Default app configuration sources** és az **Environment Variables Configuration Provider** részt.

**Cél:** értsd, hogy több provider ugyanazt a kulcsot adhatja, és a prioritás dönti el a használt értéket. Az alapértelmezett alkalmazáskonfiguráció fő forrásai, növekvő prioritással:

```text
appsettings.json
  → appsettings.{Environment}.json
  → User Secrets (Developmentben, ha beállítottuk)
  → környezeti változók
  → parancssori argumentumok
```

Az azonos kulcsú érték felülíródik; más kulcsok megmaradnak. A környezeti változókban a `__` a hierarchikus kulcs `:` elválasztóját helyettesíti. Például `Pricing__HourlyRates__ducati` a `Pricing:HourlyRates:ducati` kulcsnak felel meg, ha ezt a szekciónevet választjuk.

**Kísérlet:** a Ducati óradíját környezeti változóval állítsd `19000`-re. Az új értéket öröklő alkalmazás újraindítása után 1.5 órára `28500` HUF legyen az eredmény, új build nélkül. Az override eltávolítása és újraindítás után az alapdíj térjen vissza. Ezt az M3-ban készítjük elő; a jelenlegi M1 kód még beégetett díjtáblát használ.

Az óradíj konfigurációs adat, a kerekítés számítási szabály. Jelszó és token külön kezelendő titok; ne kerüljenek verziózott konfigurációs fájlba.

### 4. M3–M4 előtt: options és konfigurációvalidáció

[Options pattern in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-10.0)

Olvasási sorrend: **Bind hierarchical configuration**, **Bind options to the dependency injection service container**, **Options interfaces**, majd **Options validation** és a `ValidateOnStart` példa.

**Cél:** különítsd el a kulcs–érték konfigurációt, annak típusos objektumba kötését és az objektum szabályainak ellenőrzését. A sikeres binding önmagában nem igazolja, hogy a díjtábla használható.

A labor kezdetben újraindítás után érvényesülő konfigurációval dolgozik. Az `IOptions<T>` nem frissíti automatikusan a már létrehozott options értéket. Az `IOptionsSnapshot<T>` scoped, ezért singleton konstruktorába nem adható. Az `IOptionsMonitor<T>` változáskövetése későbbi mélyítés.

**Kísérlet:** előbb egy üres díjtáblával, majd egy nulla vagy negatív óradíjjal indítsd a programot. A beállított szabályokat a `ValidateOnStart` már induláskor ellenőrizze. A szerver hibás konfigurációját különböztesd meg a kliens érvénytelen `workHours` bemenetétől.

### 5. M4 előtt: lifetime-hiba diagnózisa

[.NET DI overview — Scope validation](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/overview#scope-validation)

Ezt a részt az M2 megfigyelései után olvasd. Ha egy singleton eltárol egy scoped függőséget, a hosszabb életű objektum magához köti azt. Ez captive dependency; a scope validation felismerheti az élettartam-ellentmondást.

**Kísérlet:** kontrollált singleton → scoped kapcsolatot hozz létre, ellenőrizd a validation bekapcsolását, és reprodukáld a hibát. A hibaüzenet alapján nevezd meg a két típust, majd javítsd az objektumgráfot. A részletes lépést M4-ben adjuk ki.

## Kompromisszumok és gyakori félreértések

- A kézi összeállítás kis objektumgráfnál áttekinthető; a container több összekapcsolt service és élettartam kezelését segíti.
- Egy külön service indokoltságát a változó felelősség és ellenőrizhetőség alapján mérlegeld, ne pusztán a függvény hosszából.
- A singleton nem automatikus teljesítménymegoldás. Megosztott állapotával és függőségeivel együtt kell vizsgálni.
- A JSON-provider változáskövetése és a fogyasztó által használt options frissülése külön mechanizmus. A labor jelenlegi útvonala újraindítással ellenőrizhető.
- A HTTP-regressziós tesztek a külső válaszokat védik. A helyes objektum-összeállítást és lifetime-ot a kód, a célzott kísérlet és a saját magyarázat együtt igazolja.

## Opcionális mélyítés

[Dependency injection guidelines — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/guidelines)

Később olvasd a disposalra, a thread safetyre és a captive dependencyre vonatkozó részeket. Ezek segítenek akkor, amikor egy service erőforrást birtokol vagy módosítható állapotot oszt meg.

Az options dokumentáció `IOptionsSnapshot` és `IOptionsMonitor` részeihez runtime frissítési igénynél térj vissza. A saját providerek, keyed services, factoryk és összetett disposal nem szükségesek a jelenlegi labor lezárásához.

## Aktív felidézés az adott gyakorlat után

1. Mi a különbség a kalkulátor kiemelése, a constructor injection és a container használata között?
2. Ki hozza létre a függőséget M1-ben, és ki fogja létrehozni M2-ben?
3. Miért kell két feloldást egy kérésen belül és egy második kérésben is megfigyelni a lifetime megkülönböztetéséhez?
4. Mi dönti el, melyik óradíj érvényesül, ha ugyanaz a kulcs JSON-ban és környezeti változóban is szerepel?
5. Miért nem bizonyít használható díjtáblát a sikeres configuration binding?
6. Miért problémás egy singletonban közvetlenül eltárolt scoped függőség?
