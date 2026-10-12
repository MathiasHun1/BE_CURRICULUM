# DataAccessLab — projektállapot

Létrehozás: 2026-10-12

Utolsó frissítés: 2026-10-12 — az új labor előkészítése.

## Szerep és hatókör

- Aktív, véges gyakorlóprojekt a lezárt AsyncHttpLab után; fő témája a 3. pillér — Adatok és adatbázisok.
- Cél: a MovieExplorerben már érintett, de megkopott relációs, SQL- és EF Core-tudás újbóli felépítése egy kicsi, új kontextusban. A korábbi eredmények bizonyítékok maradnak; nem tekintjük őket jelenlegi önálló tudásnak új ellenőrzés nélkül.
- Domain: `Project` és `WorkItem`, `1:N` kapcsolat. PostgreSQL az elsődleges adatbázis, EF Core a fő ORM.
- Mentor által előkészítve: üres `net10.0` API-váz, külön helyi PostgreSQL Compose-környezet és feladatleírás. Az SQL-t, az EF Core modellt, a migrációkat, az endpointokat és a próbákat alapértelmezetten a tanuló írja és futtatja.
- M1 tanulási anyag: [Relációs modell, kulcsok és első SQL](../../learning-materials/data-databases/relational-model-and-first-sql.md). Az olvasás önmagában nem mérföldkő-bizonyíték.
- A security minimum — nem megbízható input, biztonságos hibaválasz, titkok Gitből kizárása — minden adatfunkciónál érvényes. Authentication/authorization külön későbbi szelet; megfigyelhetőségi minimumként a hibák és a generált SQL vizsgálata szerepel.

## Mérföldkőterv

| ID | Eredmény | Ellenőrizhető teljesítési bizonyíték | Állapot |
|---|---|---|---|
| M1 — Reláció és SQL-frissítés | A kétentitásos modell, a kulcsok és az alap SQL működése érthető. | Saját indoklás az `1:N`, primary/foreign key és kötelező mezők szerepéről; ideiglenes PostgreSQL-táblák, seed, `JOIN` és egy constraint-sértés tényleges futási eredménye. | Active |
| M2 — EF Core modell és séma | A tanuló ugyanennek a modellnek a tartós tábláit EF Core-ral és migrációval hozza létre. | Entityk, `DbContext`, konfiguráció és migráció forrása; sikeres build és migráció; az adatbázis-séma/kulcsok közvetlen ellenőrzése. | Planned |
| M3 — Lekérdezés és végrehajtás | API-lekérdezés szűréssel és kapcsolódó adattal, tudatos materializálási ponttal. | Működő GET, sikeres és üres ág, legalább egy automatizált ellenőrzés; `IQueryable`-kompozíció és a tényleges generált SQL megvizsgálása, saját magyarázat az adatbázis-lekérdezések számáról. | Planned |
| M4 — Írás és change tracking | Egy létrehozási és egy módosítási út helyesen kezeli az entity állapotát és az adatbázis-szabályokat. | Valós írási próbák, érvénytelen input/constraint ág, tracking/no-tracking különbség megfigyelése és a `SaveChangesAsync` szerepének magyarázata. | Planned |
| M5 — Tranzakció és hibadiagnózis | Egy több lépésből álló adatváltozás hibás ága sem hagy félkész eredményt. | Sikeres és visszagördülő futási próba, ellenőrzött adatbázis-állapot, valamint egy új, célzottan bevezetett adat-hozzáférési hiba önálló leszűkítése. | Planned |

## Exit gate

M1–M5 teljesül a fenti tényleges SQL-, kód-, teszt- és diagnosztikai bizonyítékokkal. A tanuló saját szavaival végigvezeti, hogyan lesz a HTTP-kérésből EF Core-lekérdezés, SQL, adatbázis-módosítás és válasz; megkülönbözteti a lekérdezés felépítését a végrehajtásától, valamint az alkalmazásvalidációt az adatbázis-constrainttől. A teljesítés nem jelent automatikus L2/L3 vagy Confident értékelést.

Index-/query planner-, MVCC-, provider-internals- és production-migrációs mélyítés külön curriculum-feladat lesz, ha egy konkrét hiba vagy mérés indokolja; nem hosszabbítja korlátlanul ezt a labort.

## Kezdő bizonyíték és nyitott rés

- A MovieExplorer korábbi relációs, SQL, PostgreSQL, EF Core mapping/query és migrációs munkája a `CURRICULUM_PROGRESS.md` 3. pillérében L1 / Practiced vagy L0 / Learning szinten szerepel. A tanuló 2026-10-12-én jelezte, hogy a korábban tanultak sokat halványodtak; az új laborban rövid, önálló felidézéssel és futtatással ellenőrizzük az aktuális szintet.
- A MovieExplorer szüneteltetett marad. A megkezdett `Search`/`IQueryable` lépése megőrzött előzmény, amelyet új kontextusban az M3 során gyakorolunk; a régi projekt forrását itt nem módosítjuk.
- Eddig csak a mentor által készített környezet létezik; M1-hez még nincs tanulói megoldás vagy futási bizonyíték.
- Előkészítés ellenőrzése: `dotnet build projects/data-access-lab/DataAccessLab.slnx` sikeres, 0 warning/error; a Compose-konfiguráció az `.env.example` helyettesítő értékével sikeresen validálható. PostgreSQL-konténert és adatbázis-műveletet még nem futtattunk; az előkészítés önmagában nem M1-bizonyíték.

## Egyetlen következő lépés

A tanuló kód nélkül felvázolja a `Project` és `WorkItem` relációs modelljét: két tábla, `1:N` kardinalitás, primary/foreign key, kötelező mezők és a nem létező projektre mutató munkatétel adatbázis-szintű megakadályozása. Ennek ellenőrzése után következik az első ideiglenes táblás SQL-kísérlet.
