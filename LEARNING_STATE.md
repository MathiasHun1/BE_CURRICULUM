# Globális tanulási állapot

Utolsó frissítés: 2026-10-21 — M4 írási és tracking-próbák, valamint M5 tranzakciós kísérletek alapján.

## Aktuális fókusz

- **Tanulási szelet:** relációs modell, PostgreSQL, SQL és EF Core újbóli, fokozatos gyakorlása.
- **Projektállapot:** a [DataAccessLab](projects/data-access-lab/) aktív; M1–M4 `Complete`, M5 — Tranzakció és hibadiagnózis `Active`. A véges terv és exit gate a saját [MENTORING_STATE.md](projects/data-access-lab/MENTORING_STATE.md) fájljában van.
- **Elsődleges pillér:** P1 — Adatok és adatbázisok (most `DATA-TRANSACTIONS` és `DATA-INTEGRITY-CONSTRAINTS`; az M4-ben `DATA-EF-MAPPING-TRACKING`).
- **Kapcsolódó pillérek:** Web- és backendfejlesztés (API-válasz és validáció), szoftvermérnökség (hibadiagnózis).
- **Aktuális fázis:** a POST és PATCH írási út forrása ellenőrzött, a POST mentését közvetlen GET igazolta; a PATCH sikerét a tanuló jelezte. A tracking/no-tracking különbség és a PostgreSQL `23503` FK-hiba megfigyelt. Az M5 sikeres és hibás tranzakciós ága után egy új constraint-hiba önálló diagnózisa következik.
- **Korábbi bizonyíték:** a MovieExplorerben relációs modell, SQL JOIN-ok, PostgreSQL, EF Core mapping/query és migráció készült. Ezek a ledgerben maradnak, de az aktuális önálló felidézést és alkalmazást az új laborban ellenőrizzük.
- **Curriculum-helyzet:** a tudástérkép a [curriculum/](curriculum/README.md), a teljes haladási ledger a [CURRICULUM_PROGRESS.md](CURRICULUM_PROGRESS.md).

## Aktív témák

A korábbi MovieExplorer-bizonyíték nem jelent automatikusan mostani önálló alkalmazást vagy L2-szintet.

| Témaazonosító | Pillér | Célmélység | Jelenlegi mélység | Állapot | Eddigi bizonyíték |
|---|---|---:|---:|---|---|
| `DATA-TRANSACTIONS` | Adatok és adatbázisok | L3 | L1 | Practiced | Sikertelen `BEGIN`–`UPDATE`–FK-sértő `INSERT` után `ROLLBACK` és változatlan adatbázis-állapot; javított két frissítés után mindkét cím perzisztált. A tanuló a `COMMIT` előtti láthatóságot és a tranzakció nélküli részleges eredményt helyesen magyarázta. |
| `DATA-INTEGRITY-CONSTRAINTS` | Adatok és adatbázisok | L3 | L1 | Practiced | A nem létező projektet az API `404`-gyel utasította el a tanuló beszámolója szerint; közvetlen SQL-beszúrás `23503` FK-hibát adott. Új constraint-hiba önálló diagnózisa nyitott. |
| `DATA-EF-MAPPING-TRACKING` | Adatok és adatbázisok | L2 | L1 | Practiced | POST mentését GET igazolta; a PATCH forrása a projekt- és munkatételazonosítóra szűr. Ideiglenes `AsNoTracking()` mellett a válasz módosult, az adatbázis nem; a tanuló helyesen magyarázta a `SaveChangesAsync` és a követés kapcsolatát. |

## Az aktuális tanulási szelet iránya

- A relációs/SQL-alapok, az EF Core modell/séma, a lekérdezés és az írás gyakorlata elkészült; most a tranzakció és a célzott hibadiagnózis lezárása következik.
- A security- és observability-alapok a konkrét adatfunkció részeként, a teljes védett endpoint és production-mélyítés később következik.

## Nyitott hiányok

- Az M4 inputellenőrzése kódban látszik, de külön `400` HTTP-próba nincs rögzítve; a PATCH sikeres futása tanulói beszámoló. A change tracker konkrét entity state-jének debuggeres megfigyelése elmaradt. Az M5 új, célzott hibájának önálló leszűkítése és az exit gate összefoglaló magyarázata nyitott; önálló L2-es alkalmazás nincs igazolva.
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

## Nyitott döntések

Az adatos irány és az új labor elfogadott; a generált `Cascade` törlési szabályt a felhasználó 2026-10-14-én elfogadta. Az M5 lépésmérete a tanuló kódja és futási eredménye alapján adaptálható; a kötelező mérföldkövek vagy az exit gate érdemi változtatását egyeztetjük.

## Megőrzött korábbi bizonyíték

- A MovieExplorer ASP.NET Core, PostgreSQL, SQL, EF Core és LINQ területeken szolgáltatott gyakorlati bizonyítékokat.
- A lezárt AsyncHttpLab M1–M4 és exit gate bizonyítékai a saját [projektállapotában](projects/async-http-lab/MENTORING_STATE.md) és a ledgerben maradnak.
- A DI/configuration lezárt szeletének bizonyítékai és L1 / Practiced értékelése a [ConfigurationDiLab állapotában](projects/configuration-di-lab/MENTORING_STATE.md) és a teljes haladási ledgerben maradnak.
- A részletes előzmény és a félbehagyott következő lépés a MovieExplorer saját `MENTORING_STATE.md` fájljában marad.
- Az API Contract Testing öt ellenőrzött HTTP-contracttesztje `5/5` zöld; részletes bizonyíték és a halasztott `M5` a projekt saját `MENTORING_STATE.md` fájljában marad.

## Korábbi rendszerszintű mérföldkő

- A Backend Foundations részletes globális összefoglalója az [archívumban](archive/backend-foundations-global-milestone-2026-09-22.md); a projekt végrehajtási története a saját `MENTORING_STATE.md` fájljában marad.

## Egyetlen következő konkrét lépés

A tanuló a DataAccessLab Rider SQL-konzoljában érvényes `ProjectId=1` mellett próbáljon új `WorkItem`-et beszúrni a már létező `WorkItemId=1` azonosítóval, külön `BEGIN` nélkül; jegyezze fel az SQLSTATE-et és a constraint nevét, majd saját szavaival mondja el, miben különbözik ez a korábbi `23503` hibától.
