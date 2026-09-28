# ConfigurationDiLab — feladatok

Aktuális mérföldkő: `M1` — kézi constructor injection.

## Az egyetlen aktuális cél

Emeld ki a költségszámítást egy olyan kalkulátorba, amely a konstruktorában kapja meg az óradíjtáblát. Ugyanazt a kalkulátorosztályt próbáld ki két különböző díjszabással.

Az objektumok összeállítását most a `Program.cs` végzi kézi példányosítással. A kalkulátor a kapott táblával dolgozik. Az összeállítás központi helyét composition rootnak nevezzük.

## Mit és hol írj meg?

1. A [Services/ServiceCostCalculator.cs](ConfigurationDiLab/Services/ServiceCostCalculator.cs) osztály kapjon egy `IReadOnlyDictionary<string, decimal>` konstruktorparamétert. Tárold el egy privát `readonly` mezőben. Az interfész a kalkulátor számára olvasási hozzáférést ad; a kívülről átadott dictionary ettől még nem válik immutable-lé.
2. Készíts benne egy `decimal` értéket visszaadó `Calculate(string bikeBrand, decimal workHours)` metódust. A kapott táblából keresd ki az óradíjat, szorozd meg a munkaórával, és kerekíts felfelé egész HUF-ra. A kalkulátor ne tartalmazzon konkrét márkához tartozó árat, HTTP-választ vagy konfigurációbeolvasást.
3. A [Program.cs](ConfigurationDiLab/Program.cs) első `M1` kommentjénél hozd létre a kalkulátort `new` hívással, a meglévő `hourlyRates` táblát átadva. Az endpoint `TotalCost` mezőjét ezután a kalkulátor számítsa ki.
4. A kézi ellenőrzéshez ugyanitt hozz létre egy második díjtáblát, amelyben a Ducati óradíja `24000m`, és egy második kalkulátorpéldányt. Induláskor írasd ki mindkét példány `ducati`, `1.5m` bemenethez tartozó eredményét. Ez a második példány csak a constructor injection kísérlete; az API továbbra is az alapdíjas példányt használja.
5. Futtasd a teszteket, majd az API-t. Küldd el a futási eredményt és a két kiírt összeget; a tényleges kódot a repositoryban ellenőrizzük.

A metódus első szerződése egyszerű: támogatott márkával és `0 < workHours <= 200` értékkel hívjuk. Az API-ban a meglévő márka- és munkaóra-validáció marad a hívás előtt. Az `AppliedHourlyRate` továbbra is a handlerben feloldott érték. Így most kizárólag a számítás áthelyezését és a függőség átadását gyakoroljuk.

## Elfogadási feltételek

| Ellenőrzés | Elvárt eredmény |
|---|---|
| Solution build | 0 hiba, 0 warning |
| Meglévő HTTP-tesztek | Mind a 9 teszteset zöld |
| Alapdíjas kalkulátor: Ducati, 1.5 óra | `27000` HUF |
| Második kalkulátor: Ducati, 1.5 óra | `36000` HUF |
| API: Ducati, 1.5 óra | Továbbra is `200`, óradíj `18000`, összeg `27000`, pénznem `HUF` |
| Kalkulátorkód | A díjtáblát kizárólag kívülről kapja; a kerekítés a kalkulátorban történik |

A két különböző eredmény fontos bizonyíték: a HTTP-tesztek egy hibásan beégetett alapdíj mellett is zöldek lehetnének. Az implementáció és a két példány együtt igazolja a függőség átadását.

## Az ellenőrzés után

Az elkészült kód és a futás alapján térünk vissza ezekre a kérdésekre:

- Ki hozza létre az óradíjtáblát, és ki használja fel?
- Mitől constructor injection ez a kézi `new` hívás mellett is?
- Mit bizonyít a két eltérő díjszabású példány a kilenc HTTP-teszten felül?

## Későbbi útvonal

`M2`: DI registration és lifetime-megfigyelés. `M3`: options, JSON-konfiguráció és környezeti felülírás. `M4`: hibás konfiguráció és lifetime-hiba diagnózisa. A részletes, stabil teljesítési bizonyítékok a [MENTORING_STATE.md](MENTORING_STATE.md) fájlban vannak; ezek még nem aktuális implementációs feladatok.

## Kapcsolódó tananyag

[Dependency injection és konfiguráció — backend alapok](../../learning-materials/web-backend/dependency-injection-and-configuration.md): ellenőrzött hivatalos források, olvasási sorrend és mérföldkövenkénti kísérletek. Az M1 előtt az 1. olvasási lépést végezd el; utána térj vissza ehhez az implementációs feladathoz.
