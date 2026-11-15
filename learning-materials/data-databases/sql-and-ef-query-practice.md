# SQL-lekérdezések és EF Core megfelelőik

Utolsó linkellenőrzés: 2026-10-31

Kapcsolódó altémák: `DATA-SQL-QUERYING`, `DATA-EF-QUERY-EXECUTION`, `DOTNET-LINQ-EXECUTION`

## Miért aktuális?

A DataAccessLab első perzisztencia-szelete elkészült. A következő rövid gyakorlás ugyanazt az adatkérdést előbb SQL-ben, majd EF Core LINQ-ként fogalmazza meg, és összeveti az eredményt és a generált SQL-t. A cél a szűrés, vetítés, rendezés, `JOIN` és később aggregálás önálló használata; az indexek és a végrehajtási terv most nem része a feladatnak. A megoldás és a saját magyarázat számít bizonyítéknak, nem az oldal elolvasása.

## Ajánlott sorrend

1. **Adathalmaz:** [PostgreSQL Exercises — Getting Started](https://www.pgexercises.com/gettingstarted.html). Nézd meg a `members`, `facilities` és `bookings` táblák oszlopait és kapcsolatait. **Cél:** tudd, melyik táblából kell kiindulni egy kérdéshez.
2. **Első böngészős SQL-feladat:** [Control which rows are retrieved](https://www.pgexercises.com/questions/basic/where.html). Írd meg és futtasd a saját `SELECT`-edet a webes szerkesztőben, majd hasonlítsd az elvárt sorokhoz. **Cél:** `WHERE` szűrés és a visszaadott sorok pontos ellenőrzése. A Basic kategóriában előtte az oszlopválasztás, utána a többfeltételes szűrés gyakorolható.
3. **EF Core megfeleltetés:** [Querying Data — Microsoft Learn](https://learn.microsoft.com/en-us/ef/core/querying/). Csak a filtering és az egyedi/többes eredmény példáit olvasd. **Cél:** a SQL `WHERE` és oszlopválasztás szerepét a LINQ `Where` és `Select` lépésekhez, a végrehajtást a materializáló híváshoz kötni.
4. **Generált SQL böngészőben:** [EF Playground — Queries](https://efplayground.io/query) és a [query playground útmutatója](https://efplayground.io/docs/playground/queries). Egyszerű EF-modell és LINQ-lekérdezés után nézd meg a generált SQL-t. **Cél:** ellenőrizni a fordítás alakját. A szolgáltató leírása szerint a lekérdezés nem a PostgreSQL Exercises tényleges adatbázisán fut, így a két oldal eredménysorai közvetlenül nem hasonlíthatók össze.

## Kapcsolódó gyakorlati kísérletek

1. Oldj meg egy PostgreSQL Exercises Basic szűrési feladatot. A megoldás megnyitása előtt jegyezd fel a saját SQL-t, a kapott oszlopokat, sorokat és rendezést.
2. Fogalmazz meg azonos szerkezetű kérdést a DataAccessLab `Project` és `WorkItem` adataira. Ugyanazon helyi adatbázison írd meg SQL-ben, majd EF Core LINQ-ként; csak a két tényleges eredményt és a generált SQL-t összevetve tekintsük ekvivalensnek.
3. Ha a szűrés és vetítés már megy, válassz a [Joins and Subqueries](https://www.pgexercises.com/questions/joins/) kategóriából egy egyszerű két táblás feladatot. Mondd el, melyik `JOIN`-nak milyen LINQ-minta felel meg az adott adatkérdésnél.

Az online PostgreSQL Exercises saját adathalmazt használ, az EF Playground pedig elsősorban SQL-generálást mutat. A teljesen azonos adatokon végzett SQL–EF eredmény-összehasonlításhoz a DataAccessLab helyi PostgreSQL-adatbázisa a közös futtatási hely.

## Opcionális mélyítés

- [SQLBolt](https://sqlbolt.com/) — rövid, böngészős alapozás, ha a `SELECT`, `WHERE` vagy `JOIN` szintaxisa ismét bizonytalan. Az általános SQL-alapokra jó, de a PostgreSQL-specifikus viselkedést a PostgreSQL Exercisesben ellenőrizd.
- [EF Core — How Queries Work](https://learn.microsoft.com/en-us/ef/core/querying/overview) — a lekérdezés felépítése és tényleges végrehajtása közötti határhoz; a részletes pipeline-mélyítést csak konkrét fordítási hiba esetén vedd elő.
