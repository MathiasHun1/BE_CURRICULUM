# Globális tanulási állapot

Utolsó frissítés: 2026-10-07

## Aktuális fókusz

- **Tanulási szelet:** átmenet a DI/configuration alapozásból az aszinkron I/O, request cancellation és külső HTTP-hívások témájába.
- **Projektállapot:** a [ConfigurationDiLab](projects/configuration-di-lab/) alapozó labor lezárt; M1–M4 `Complete`, a bizonyítékok és korlátaik a [MENTORING_STATE.md](projects/configuration-di-lab/MENTORING_STATE.md) fájlban. Nincs új aktív fő projekt vagy aktív projektmérföldkő.
- **Elsődleges pillér:** P1 — Web- és backendfejlesztés (`WEB-ASYNC-CANCELLATION`).
- **Kapcsolódó pillér:** P1 — Programozás és .NET (`DOTNET-ASYNC-TASKS`); a DI/configuration tanulságai átvezető témák.
- **Aktuális fázis:** az async/await és a szálak viszonyának belépő kérdése kiadva, még nincs válasz vagy új async implementáció. A gyakorlati projekt helyét és véges tervét a diagnózis után kell egyeztetni.
- **Utolsó ellenőrzés:** a ConfigurationDiLab tesztcsomagja `9/9` zöld (2026-10-07, 441 ms); a handler és a scoped kalkulátor is `IOptionsSnapshot<PricingOptions>`-t kap, a Ducati-alapdíj 18000.
- **Lezárt tanulási anyag:** [Dependency injection és konfiguráció — backend alapok](learning-materials/web-backend/dependency-injection-and-configuration.md).
- **Curriculum-helyzet:** a tudástérkép a [curriculum/](curriculum/README.md), a teljes haladási ledger a [CURRICULUM_PROGRESS.md](CURRICULUM_PROGRESS.md).

## Aktív témák

A DI/configuration sorok az új szelethez átviendő tudást jelölik; a projektlezárás nem jelent automatikus L2-t.

| Témaazonosító | Pillér | Célmélység | Jelenlegi mélység | Állapot | Eddigi bizonyíték |
|---|---|---:|---:|---|---|
| `WEB-ASYNC-CANCELLATION` | Web- és backendfejlesztés | L2 | L1 | Learning | Korábbi aszinkron response/flush és kivételterjedés. Új belépő kérdés kiadva; cancellation, timeout és outbound HTTP még nyitott. |
| `DOTNET-ASYNC-TASKS` | Programozás és .NET | L3 | L1 | Practiced | Korábbi Task/await és EF Core alkalmazások. A szálak és az aszinkron várakozás viszonyára most még nincs új tanulói válasz. |
| `WEB-DI-CONFIGURATION` | Web- és backendfejlesztés | L2 | L1 | Practiced | Binding/validáció, üres konfiguráció bemásolt OptionsValidationException hibája, snapshot-fogyasztók, 9/9 teszt. Env-prioritás és runtime-betöltés saját magyarázattal; az időzítés és a 19000 × 1,5 = 28500 eredmény mentor által korrigált. |
| `DOTNET-DI-COMPOSITION` | Programozás és .NET | L2 | L1 | Practiced | Scoped/transient probe HTTP-próbával igazolt; singleton helyes következtetése felhasználói kérésre elfogadva a feladat lezárásához. Captive dependency vezetett reprodukció/javítás; regisztráció és feloldáskori példányosítás saját válasszal elkülönítve. |

## A következő tanulási szelet iránya

- Aszinkron I/O és szálhasználat alapmodelljének felmérése, majd a hiányokhoz igazított kísérlet.
- Lassú külső HTTP-függőség, a kliens megszakítása és a cancellation továbbadása lesz a gyakorlati irány.
- Új projekt és mérföldkőterv még nincs; a ConfigurationDiLab nem bővül automatikusan új feladatokkal.

## A következő lépés elfogadási feltételei

- A tanuló saját szavaival elmondja, mit gondol a szál viselkedéséről egy még be nem fejeződött aszinkron HTTP-művelet await-je közben.
- Kitér arra, jelent-e az async kulcsszó új szál létrehozását. Ez belépő diagnózis, nem már igazolt tudás.

## Döntések

- A MovieExplorer 2026-09-14-től szüneteltetett tanulóprojekt; nem töröljük, és korábbi eredményei továbbra is tanulási bizonyítékok.
- Az új tanulási útvonal koherens backend-alapozással indul, nem a MovieExplorer következő funkciójával.
- Az első projekt egy minimális Backend Foundations labor lesz, amely a forráskódtól a HTTP-válaszig teszi láthatóvá a végrehajtási láncot.
- A projekt neve `BackendFoundations`, repository-helye [`projects/backend-foundations/`](projects/backend-foundations/), target frameworkje `net10.0`.
- Az első szeletben még nem használunk adatbázist, EF Core-t, autentikációt, Dockert vagy indokolatlan architekturális rétegeket.
- Alapértelmezetten a tanuló írja és futtatja a kódot; az agent kis lépésekben mentorál és tényleges bizonyíték alapján értékel.
- A felhasználó 2026-09-28-i kérése alapján a tanulási állapotokat csak explicit `update` vagy külön fájlfrissítési kérés után aktualizáljuk, az addigi bizonyított haladást egyben rögzítve. Közben chatben mentorálunk és ellenőrizzük a kódot.
- A `RouteHandlerOptions.ThrowOnBadRequest` értéke explicit `false`, hogy a Minimal API bindinghibái környezettől függetlenül automatikus `400` választ adjanak, és ne váljanak a saját exception middleware-ben `500` hibává.
- Az aktív projektek véges, bizonyíték-alapú mérföldkőtervet és explicit exit gate-et kapnak; egyszerre egy mérföldkő és azon belül egy konkrét lépés aktív.
- A curriculum pillérei inkrementálisan bomlanak altémákra; a teljes haladási ledger és az aktív fókuszállapot külön forrás-of-truth fájlban marad.
- A curriculum végrehajtása a korai munkaképességet és a spirális mélyítést követi: először témánként a legkisebb koherens, gyakorlattal és hibamóddal bizonyított szelet készül el, az L3-részletekhez pedig valós projekt-, diagnosztikai vagy teljesítménytrigger alapján térünk vissza.
- A tesztelés az első működő feature-től a delivery-loop része; architekturális réteg, minta vagy modul csak megnevezhető változási probléma és ellenőrizhető előny alapján kerül be.
- A felhasználó a generált tesztek mindennapi használata mellett a tesztelési mélyítést most elhalasztotta; az API Contract Testing `M5` kontrollált contracttörése nem történt meg. A minimum elméleti lezáró anyag megmarad, a következő fókusz DI és konfiguráció.
- A következő fő gyakorlóprojekt a ConfigurationDiLab (`net10.0`), külön könyvtárban. A felhasználó 2026-09-28-án elfogadta az új projektet és az előkészítést az agentre bízta; a saját DI/configuration implementáció tanulói munka marad.
- A security P1 keresztmetszeti alap: minden új üzleti szelet arányos threat-, input/output-, secret-, hozzáférés- és naplózási minimumot kap; a teljes identity- és kriptográfiai mélység a tényleges felelősséghez igazodik.
- A production minimum az első telepíthető szolgáltatástól együtt kezeli az immutable artifactot, külső konfigurációt, visszaállítható deploymentet, strukturált telemetriát és elkülönített startup/readiness/liveness jelzést; a cloud- és orchestration-mélység szükségvezérelt.
- Elosztott rendszereknél az első külső dependencyvel együtt jön a deadline/cancellation, timeout, biztonságos retry, idempotencia és korreláció; cache, broker, replika, shard, konszenzus vagy saga csak mért korlát vagy konkrét workflow-igény alapján kerül be.

- 2026-10-07: a scoped/transient probe futásai után a felhasználó a singleton helyes következtetését kérte elfogadni a kísérlet teljesítéséhez. Ez elfogadott lezárási bizonyíték; külön singleton probe-futást nem állítunk. A DI/configuration alapozó szelet lezárult, a következő irány a tantervi async/cancellation szelet.

## Nyitott döntések

Az async belépő kérdés után egyeztetendő a gyakorlóprojekt helye és véges mérföldkőterve. Ez a fogalmi belépő kérdést nem blokkolja.

## Megőrzött korábbi bizonyíték

- A MovieExplorer ASP.NET Core, PostgreSQL, SQL, EF Core és LINQ területeken szolgáltatott gyakorlati bizonyítékokat.
- A részletes előzmény és a félbehagyott következő lépés a MovieExplorer saját `MENTORING_STATE.md` fájljában marad.
- Az API Contract Testing öt ellenőrzött HTTP-contracttesztje `5/5` zöld; részletes bizonyíték és a halasztott `M5` a projekt saját `MENTORING_STATE.md` fájljában marad.

## Korábbi rendszerszintű mérföldkő

- A Backend Foundations részletes globális összefoglalója az [archívumban](archive/backend-foundations-global-milestone-2026-09-22.md); a projekt végrehajtási története a saját `MENTORING_STATE.md` fájljában marad.

## Egyetlen következő konkrét lépés

A tanuló megválaszolja az async belépő kérdést: egy endpoint 3 másodpercig await-tel vár egy külső HTTP-válaszra; mi történik ezalatt a kérést addig végrehajtó szállal, és létrehoz-e az async külön szálat? Válasz még nem érkezett.
