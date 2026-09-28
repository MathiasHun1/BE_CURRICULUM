# Globális tanulási állapot

Utolsó frissítés: 2026-09-28

## Aktuális fókusz

- **Tanulási szelet:** dependency injection és konfiguráció szerepének megértése egy kis .NET backendben.
- **Aktív projekt:** [ConfigurationDiLab](projects/configuration-di-lab/), projektspecifikus terv: [MENTORING_STATE.md](projects/configuration-di-lab/MENTORING_STATE.md).
- **Elsődleges pillér:** P1 — Web- és backendfejlesztés (`WEB-DI-CONFIGURATION`).
- **Kapcsolódó pillér:** P1 — Programozás és .NET (`DOTNET-DI-COMPOSITION`).
- **Curriculum-helyzet:** mind a nyolc pillér egyenként 15 stabil altémára bontva; a teljes változó állapot a [CURRICULUM_PROGRESS.md](CURRICULUM_PROGRESS.md), a definíciók a [`curriculum/`](curriculum/README.md) pillértérképeiben találhatók.
- **Lezáró tanulási anyag:** [ASP.NET Core integration tesztelési alapok](learning-materials/testing/aspnet-core-integration-testing-basics.md), benne a generált tesztek értékelésének minimumával.
- **Aktuális tanulási anyag:** [Dependency injection és konfiguráció — backend alapok](learning-materials/web-backend/dependency-injection-and-configuration.md); M1 előtt az első olvasási lépés aktuális, majd következik a kalkulátor implementációja.
- **Projektmérföldkő:** ConfigurationDiLab `M1` — kézi constructor injection — `Active`. A négy mérföldkő és az exit gate a projekt saját állapotfájljában található.
- **Aktuális fázis:** a felhasználó új projektet választott, és az előkészítést az agentre bízta az önálló feladatokig. A scaffold és a regressziós tesztcsomag elkészült; a kalkulátor implementációja tanulói feladat. A belépő diagnózis részleges fogalmi bizonyítékait megőrizzük, a scaffold nem emel tudásszintet.

## Aktív témák

Az L0 itt azt jelenti, hogy a teljes altéma L1-es alapmodellje még nincs igazolva; részleges bizonyíték már lehet, és nem állítja, hogy a tanuló korábban nem találkozott a témával.

| Témaazonosító | Pillér | Célmélység | Jelenlegi mélység | Állapot | Eddigi bizonyíték |
|---|---|---:|---:|---|---|
| `WEB-DI-CONFIGURATION` | Web- és backendfejlesztés | L2 | L0 | Learning | 2026-09-28: saját magyarázatban elkülönítette az óradíjadatot és a kerekítési viselkedést, megnevezte a külső állíthatóság előnyét. A kódmódosítás utáni build/telepítés/újraindítás lánca, providerek és options még nincs igazolva. |
| `DOTNET-DI-COMPOSITION` | Programozás és .NET | L2 | L1 | Learning | Korábbi constructor injection és service registration. 2026-09-28: mérlegelte a service kiemelését; helyesen választotta a konstruktorban kapott óradíjtáblát kétféle díjszabáshoz, és jelezte, hogy ez kézi `new` hívással megvalósítható. Az összeállítás felelősségének saját magyarázata, önálló implementáció, scope és lifetime diagnózisa még hiányzik. |

## A következő tanulási szelet iránya

- A kódba égetett adat, az alkalmazáskonfiguráció és az üzleti viselkedés felelőssége elkülönül.
- Egy kis objektumgráfban a service registration, constructor injection és lifetime megválasztása gyakorlati bizonyítékot kap.
- A konfiguráció hiánya vagy hibája és egy indokolt lifetime-hiba diagnosztizálható lesz; a részletes exit gate az új gyakorlóprojekt tervében rögzül.

## A következő lépés elfogadási feltételei

- A kalkulátor kívülről kapott óradíjtáblával számol; két példány Ducati 1.5 órára 27000 és 36000 HUF eredményt ad.
- Az API az alapdíjas kalkulátort használja, a build tiszta és a kilenc regressziós teszteset zöld; részletes feladat: [TASKS.md](projects/configuration-di-lab/TASKS.md).

## Döntések

- A MovieExplorer 2026-09-14-től szüneteltetett tanulóprojekt; nem töröljük, és korábbi eredményei továbbra is tanulási bizonyítékok.
- Az új tanulási útvonal koherens backend-alapozással indul, nem a MovieExplorer következő funkciójával.
- Az első projekt egy minimális Backend Foundations labor lesz, amely a forráskódtól a HTTP-válaszig teszi láthatóvá a végrehajtási láncot.
- A projekt neve `BackendFoundations`, repository-helye [`projects/backend-foundations/`](projects/backend-foundations/), target frameworkje `net10.0`.
- Az első szeletben még nem használunk adatbázist, EF Core-t, autentikációt, Dockert vagy indokolatlan architekturális rétegeket.
- Alapértelmezetten a tanuló írja és futtatja a kódot; az agent kis lépésekben mentorál és tényleges bizonyíték alapján értékel.
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

## Nyitott döntések

- Jelenleg nincs a következő lépést blokkoló nyitott döntés.

## Megőrzött korábbi bizonyíték

- A MovieExplorer ASP.NET Core, PostgreSQL, SQL, EF Core és LINQ területeken szolgáltatott gyakorlati bizonyítékokat.
- A részletes előzmény és a félbehagyott következő lépés a MovieExplorer saját `MENTORING_STATE.md` fájljában marad.
- Az API Contract Testing öt ellenőrzött HTTP-contracttesztje `5/5` zöld; részletes bizonyíték és a halasztott `M5` a projekt saját `MENTORING_STATE.md` fájljában marad.

## Korábbi rendszerszintű mérföldkő

- A Backend Foundations részletes globális összefoglalója az [archívumban](archive/backend-foundations-global-milestone-2026-09-22.md); a projekt végrehajtási története a saját `MENTORING_STATE.md` fájljában marad.

## Egyetlen következő konkrét lépés

A tanuló elvégzi a ConfigurationDiLab [TASKS.md](projects/configuration-di-lab/TASKS.md) `M1` feladatát: constructor injection, kézi példányosítás, endpoint-bekötés, két díjszabás futási bizonyítéka és zöld regressziós tesztek.
