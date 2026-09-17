# Curriculum-részletek

Ez a könyvtár a nyolc pillér fokozatos kibontására szolgál.

A `software-engineering-curriculum.md` marad a magas szintű térkép és az elsődleges belépési pont. Külön pillérfájl csak akkor jöjjön létre, amikor az adott terület részletes bontása valóban aktuálissá válik. Nem hozunk létre előre több száz üres témakártyát.

Egy témabejegyzés minimális mezői:

- stabil témaazonosító;
- prioritás;
- célmélység;
- előfeltételek;
- kapcsolódó témák;
- gyakorlati bizonyíték vagy feladat;
- rövid elfogadási feltételek.

A változó jelenlegi tudásszintet és státuszt nem kell minden részletes curriculumfájlban megismételni. A teljes ledger elsődleges helye a gyökérben található `CURRICULUM_PROGRESS.md`; a `LEARNING_STATE.md` csak az aktuális 3–5 fókusztémát tartalmazza.

## Kibontott pillérek

- [1. Programozás és .NET](01-programming-dotnet.md) — 15 stabil altéma, a C# nyelvi alapoktól a CLR-en és memórián át az async/concurrency és DI témákig.
- [2. Web- és backendfejlesztés](02-web-backend.md) — 15 stabil altéma, ajánlott tanulási hullámok, előfeltételek és célbizonyítékok.
- [3. Adatok és adatbázisok](03-data-databases.md) — 15 stabil altéma a relációs modelltől az SQL-en, indexeken és tranzakciókon át az EF Core-ig és a sémamigrációkig.
- [4. Számítógépes rendszerek](04-computer-systems.md) — 15 stabil altéma a CPU-, memória- és operációsrendszer-alapoktól az I/O-n és konkurencián át a hálózati útvonalig.
- [5. Szoftverfejlesztés és architektúra](05-software-engineering-architecture.md) — 15 stabil altéma a követelményektől, kódminőségtől és teszteléstől a moduláris és architekturális döntésekig.
- [6. Biztonság és identitáskezelés](06-security-identity.md) — 15 stabil altéma a threat modelingtől és kriptográfiai alapoktól az identitásprotokollokon át az alkalmazásbiztonságig.
- [7. Infrastruktúra és production engineering](07-infrastructure-production.md) — 15 stabil altéma a Linux- és konténeralapoktól a CI/CD-n és observabilityn át a megbízható production működésig.
- [8. Elosztott rendszerek és rendszertervezés](08-distributed-systems-design.md) — 15 stabil altéma a részleges hibáktól és konzisztenciától a messaging-, resilience- és rendszertervezési döntésekig.

## Inkrementális karbantartási szabály

Mind a nyolc pillér első stabil altématérképe elkészült. További részletezést csak az aktuális tanulási út, új bizonyíték vagy valós szakmai igény indokoljon; a témák ne bomoljanak előre több száz spekulatív leckére. A keresztpilléres bizonyítékokat aktiváláskor kell pontosítani és a progress ledgerben normalizálni.
