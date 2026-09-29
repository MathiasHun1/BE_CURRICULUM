# ConfigurationDiLab — aktuális feladat

Aktuális gyakorlati szelet: környezeti konfigurációs felülírás (M3 részleges előkészítése).
Formálisan aktív mérföldkő: M2; az azonosítós kísérlet nincs teljesítve. A teljes terv a [MENTORING_STATE.md](MENTORING_STATE.md) fájlban található.

## Az egyetlen aktuális cél

Ugyanazzal a builddel, változatlan JSON mellett állítsd át a Ducati óradíját környezeti változóval, majd állítsd vissza a felülírás eltávolításával.

A tényleges kódban a Pricing JSON-szekció Get<PricingOptions>() bindinggal adja a díjtáblát. A DI-regisztráció és az endpoint ugyanazt a pricingOptions.HourlyRates instance-t használja. A hiányzó szekció és az üres tábla startup guardja szerepel a forrásban; az utolsó regressziós tesztfutás 9/9 zöld. A külön üres konfigurációs indítás futási bizonyítéka még nincs.

## Közvetlenül szükséges fogalom

A configuration providerek ugyanahhoz a kulcshoz több értéket is adhatnak. Az alapértelmezett környezeti változós provider a JSON után töltődik, így ugyanazt a kulcsot felülírhatja. A dupla aláhúzás a hierarchia szintjeit jelöli: Pricing__HourlyRates__ducati a Pricing:HourlyRates:ducati kulcsnak felel meg.

[Hivatalos dokumentáció](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#environment-variables-configuration-provider)

## Elvégzendő lépések

1. Az appsettings.json Ducati-díja maradjon 18000. Állítsd le a futó API-t.
2. A repository gyökerében, ugyanabban a PowerShell-terminálban állítsd be a környezeti változót, majd indíts új build nélkül:

   ```powershell
   $env:Pricing__HourlyRates__ducati = "19000"
   dotnet run --project projects/configuration-di-lab/ConfigurationDiLab --no-build
   ```

3. Hívd meg az API-t:

   ```http
   GET http://localhost:5187/services/wage-calculation/ducati?workHours=1.5
   ```

4. Mentsd el a HTTP-választ: AppliedHourlyRate=19000 és TotalCost=28500 legyen.
5. Állítsd le az API-t, majd ugyanabban a terminálban töröld a környezeti felülírást:

   ```powershell
   Remove-Item Env:Pricing__HourlyRates__ducati
   ```

6. Ugyanazzal a dotnet run paranccsal indítsd újra, majd ismételd meg a HTTP-kérést. AppliedHourlyRate=18000 és TotalCost=27000 legyen.
7. Küldd el a két HTTP-választ. A kísérlet jelenleg kiadott feladat, még nincs futási bizonyítéka.

## Elfogadási feltételek

| Ellenőrzés | Elvárt eredmény |
|---|---|
| JSON-alapdíj | Végig 18000 |
| Indítás | Mindkét indítás --no-build, ugyanabban a terminálban |
| Környezeti felülírással | Óradíj 19000, 1.5 órára végösszeg 28500 |
| Felülírás eltávolítása és újraindítás után | Óradíj 18000, végösszeg 27000 |
| Bizonyíték | Mindkét tényleges HTTP-válasz bemutatva |

## Felidézés az ellenőrzött futás után

1. Miért az 19000 érvényesült, miközben a JSON-ban 18000 maradt?
2. Mit jelent a környezeti változó nevében a dupla aláhúzás?
3. Miért kellett az új környezeti változóval indítani a folyamatot, miközben új build nem kellett?

## Kapcsolódó tananyag és további útvonal

[Dependency injection és konfiguráció — backend alapok](../../learning-materials/web-backend/dependency-injection-and-configuration.md): a 3. olvasási lépés aktuális.

A DI-options (IOptions), ValidateOnStart, nem pozitív óradíjak validációja és captive dependency diagnózisa még nyitott. A milestone-terv és exit gate nem változott; a félretett M2-kísérletet nem jelöljük késznek. Állapotfájlokat a felhasználó kérésére csak update vagy külön fájlfrissítési kérés után módosítunk.
