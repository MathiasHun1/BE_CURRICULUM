# Globális tanulási állapot

Utolsó frissítés: 2026-10-31 — a DataAccessLab lezárása és az SQL–EF Core páros lekérdezésgyakorlás kijelölése alapján.

## Aktuális fókusz

- **Tanulási szelet:** SQL-lekérdezések megoldása, majd ugyanazon adatkérdések EF Core LINQ-megfelelőinek elkészítése.
- **Projektállapot:** nincs aktív fő gyakorlóprojekt. A [DataAccessLab](projects/data-access-lab/) M1–M5 mérföldköve és exit gate-je `Complete`; a lezárt projekt később közös SQL–EF eredmény-összehasonlításhoz használható. A részletes bizonyíték a saját [MENTORING_STATE.md](projects/data-access-lab/MENTORING_STATE.md) fájljában van.
- **Elsődleges pillér:** P1 — Adatok és adatbázisok (`DATA-SQL-QUERYING`, `DATA-EF-QUERY-EXECUTION`).
- **Kapcsolódó pillér:** Programozás és .NET (`DOTNET-LINQ-EXECUTION`).
- **Aktuális fázis:** a rövid modell–migráció kísérlet után a tanuló az SQL és EF Core lekérdezések páros gyakorlását választotta. Az első online SQL-feladat egy egyszerű szűrés; az EF oldali megfelelőjét utána dolgozzuk ki. Az index-/query planner-mélyítés halasztott.
- **Curriculum-helyzet:** a tudástérkép a [curriculum/](curriculum/README.md), a teljes haladási ledger a [CURRICULUM_PROGRESS.md](CURRICULUM_PROGRESS.md).
- **Ajánlott anyag:** [SQL-lekérdezések és EF Core megfelelőik](learning-materials/data-databases/sql-and-ef-query-practice.md).

## Legutóbbi bizonyíték

- A hibás tranzakció `23503` és `25P02` hibái után `ROLLBACK` történt, a korábbi cím megmaradt. A javított, sikeres két `UPDATE` után mindkét cím perzisztált; a tanuló a tranzakció nélküli részleges eredményt és a `COMMIT` előtti láthatóságot helyesen magyarázta.
- Az új, ismétlődő kulcsos próba közölt PostgreSQL-hibája `23505` / `PK_WorkItems`; a tanuló önállóan a duplikált primary key-t nevezte meg. A korábbi `23503` jelentését nem idézte fel önállóan.
- A POST kérés útját Kestreltől a handlerig saját szavaival kezdte leírni, majd helyesen azonosította a `FindAsync`, `context.Add`, `SaveChangesAsync` és `201` szerepét. A GET `Where`/`Select` építését és `FirstOrDefaultAsync` végrehajtását helyesen elkülönítette. A handler és a DB constraintjeinek különbségéhez néhol mentor-pontosítás kellett.
- A DataAccessLab alapozó bizonyítéka L1 / Practiced mélységet támaszt alá; L2/Confident értékelést nem. A teljes altémánkénti bizonyíték a [ledgerben](CURRICULUM_PROGRESS.md) és a [projektállapotban](projects/data-access-lab/MENTORING_STATE.md) marad.
- A rövid EF Core kísérletben a tanuló átmenetileg `string?`-re módosította a `WorkItem.Title` típust; a `has-pending-model-changes` eltérést jelzett az utolsó migrációs snapshottól. Visszaállítás után a parancs nem talált eltérést, a forrás és a Git-diff ezt igazolta. A tanuló helyesen azonosította a modell–snapshot összehasonlítást és azt, hogy nem történt DB-módosítás.

## Aktuális téma

- `DATA-SQL-QUERYING` (L1 / Practiced): önálló SQL `SELECT`-ek szűréssel, vetítéssel, majd kapcsolással és aggregálással.
- `DATA-EF-QUERY-EXECUTION` (L1 / Practiced): ugyanazon adatkérdések LINQ-megfogalmazása, a végrehajtási pont és a generált SQL ellenőrzése.

## Az aktuális tanulási szelet iránya

- A DataAccessLab alapozó szelete lezárult, a rövid modell–migráció kísérlet teljesült. Most kis SQL–EF feladatpárok következnek; új fő gyakorlóprojekt esetén később véges mérföldkőterv készül.
- Az index-/query planner-, MVCC-, provider-internals- és production-migrációs mélyítés külön, szükségvezérelt tantervi feladat marad.

## Nyitott hiányok

- A DataAccessLab M4 inputellenőrzése kódban látszik, de külön `400` HTTP-próba nincs rögzítve; a PATCH sikeres futása tanulói beszámoló. A konkrét entity state debuggeres megfigyelése, a sikertelen `23505` próba teljes SQL-je és külön utóellenőrzése elmaradt. Az exit gate magyarázata vezetett pontosítást igényelt; önálló L2-es alkalmazás nincs igazolva.
- A rövid index-/`EXPLAIN` próbából csak `Index Scan` és 6 tényleges eredménysor tanulói közlése ismert, teljes SQL és terv nélkül; ebből L1-es query planner tudásszint nem következik.
- A lezárt async labor mélyebb hiányai a [projektállapotban](projects/async-http-lab/MENTORING_STATE.md) és a ledgerben maradnak; nem előfeltételei az adatos labornak.

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
- 2026-10-12: a felhasználó a következő fő iránynak az adatbázist és EF Core-t választotta, új, kis laborral. A MovieExplorer szüneteltetett marad; a korábbi tudás megkopását az új labor lépéseinek méretezésénél figyelembe vesszük.
- 2026-10-31: a DataAccessLab M1–M5 és alapozó exit gate-je teljesült, a projekt lezárult. A tanuló az index-/query planner-mélyítést most túl mélynek találta; ezt nem tesszük a lezárás feltételévé, és későbbi, szükségvezérelt témaként tartjuk nyilván. Következőnek rövid, visszafordítható EF Core modell–migráció gyakorlatot választott, új fő projekt nélkül.
- 2026-10-31: a modell–snapshot kísérlet visszaállítással együtt teljesült. A tanuló a következő iránynak SQL-lekérdezések és EF Core megfelelőik páros gyakorlását választotta; az első feladat online PostgreSQL Exercises környezetben indul. A webes EF Playground a generált SQL szemléltetésére használható, azonos adatokon történő eredmény-összehasonlítás a DataAccessLab helyi adatbázisával lehetséges.

## Nyitott döntések

Az SQL-feladatok online oldalon, a pontos SQL–EF eredmény-összehasonlítás helyi közös adatbázison történhet; az első online feladat után a páros gyakorlás következő futtatási helyét kijelöljük. Új fő projekt nincs kijelölve, az index-/query planner-mélyítés halasztott.

## Megőrzött korábbi bizonyíték

- A MovieExplorer ASP.NET Core, PostgreSQL, SQL, EF Core és LINQ területeken szolgáltatott gyakorlati bizonyítékokat.
- A lezárt AsyncHttpLab M1–M4 és exit gate bizonyítékai a saját [projektállapotában](projects/async-http-lab/MENTORING_STATE.md) és a ledgerben maradnak.
- A DI/configuration lezárt szeletének bizonyítékai és L1 / Practiced értékelése a [ConfigurationDiLab állapotában](projects/configuration-di-lab/MENTORING_STATE.md) és a teljes haladási ledgerben maradnak.
- A részletes előzmény és a félbehagyott következő lépés a MovieExplorer saját `MENTORING_STATE.md` fájljában marad.
- Az API Contract Testing öt ellenőrzött HTTP-contracttesztje `5/5` zöld; részletes bizonyíték és a halasztott `M5` a projekt saját `MENTORING_STATE.md` fájljában marad.

## Korábbi rendszerszintű mérföldkő

- A Backend Foundations részletes globális összefoglalója az [archívumban](archive/backend-foundations-global-milestone-2026-09-22.md); a projekt végrehajtási története a saját `MENTORING_STATE.md` fájljában marad.

## Egyetlen következő konkrét lépés

A tanuló olvassa el a PostgreSQL Exercises [Getting Started](https://www.pgexercises.com/gettingstarted.html) adatsémáját, majd önállóan oldja meg a [Control which rows are retrieved](https://www.pgexercises.com/questions/basic/where.html) feladatot a böngészőben. Küldje el a saját SQL-t és a futási eredményt; ezután ugyanazt az adatkérdést EF Core LINQ-ként fogalmazzuk meg.
