# Relációs modell, kulcsok és első SQL

Utolsó linkellenőrzés: 2026-10-12

Technológiai kontextus: PostgreSQL 16; kapcsolódó későbbi lépés: EF Core

Kapcsolódó altémák: `DATA-RELATIONAL-MODEL`, `DATA-SCHEMA-DESIGN`, `DATA-INTEGRITY-CONSTRAINTS`, `DATA-SQL-QUERYING`; később `DATA-EF-MAPPING-TRACKING`

## Miért aktuális?

A [DataAccessLab M1](../../projects/data-access-lab/MENTORING_STATE.md) a korábban már gyakorolt, de megkopott adatbázis-alapokat építi vissza. Az első feladat egy `Project`–`WorkItem` kapcsolat saját megtervezése; utána kis PostgreSQL-kísérletben ellenőrizzük a döntéseket. Egy forrás elolvasása önmagában nem tudásszint-bizonyíték: a saját modell, a futó SQL és a hibák magyarázata számít.

## Rövid fogalmi térkép

- A **tábla** azonos szerkezetű sorokat tárol. Az oszlop típusa meghatározza, milyen érték kerülhet bele.
- A **primary key** egy sor azonosítója: egyedi és nem `NULL`. Egy táblának lehet több oszlopból álló összetett elsődleges kulcsa is.
- A **foreign key** egy másik tábla kulcsára hivatkozik. Az adatbázis így megakadályozhatja, hogy egy sor nem létező kapcsolódó sorra mutasson.
- Az `1:N` kapcsolatnál a hivatkozó kulcs rendszerint az **N oldali** táblában van. Például egy részleghez több alkalmazott tartozhat, de egy alkalmazott egy részleghez tartozik; az alkalmazotti sor tartalmazza a részleg azonosítóját.
- A foreign key önmagában megengedhet `NULL` értéket. Ha minden N oldali sornak kötelezően kapcsolódnia kell, ehhez `NOT NULL` is szükséges. Az `1:N` nem jelenti azt, hogy az 1 oldali sorhoz legalább egy N oldali sornak léteznie kell.
- A `CHECK` egy sor értékeire vonatkozó feltételt érvényesít. Az alkalmazásvalidáció segít jó hibaüzenetet adni, de a tartós adatintegritás fontos szabályait az adatbázis is védje.

## Ajánlott olvasási sorrend

1. **Táblák és oszlopok:** [Creating a New Table — PostgreSQL 16](https://www.postgresql.org/docs/16/tutorial-table.html). Csak a `CREATE TABLE`, oszloptípus és a pontosvesszőig tartó SQL-utasítás alapját olvasd. **Cél:** fel tudj sorolni két tábla minimális oszlopait; a labor tábláit még ne másold ki kész példából.
2. **Adatbázis-szabályok:** [Constraints — PostgreSQL 16](https://www.postgresql.org/docs/16/ddl-constraints.html). Most az 5.4.1–5.4.5 részekből a `CHECK`, `NOT NULL`, primary key és foreign key szerepére koncentrálj; a `UNIQUE`-ot csak annyira nézd meg, hogy meg tudd különböztetni a primary keytől. **Cél:** meg tudd jósolni, melyik hibás beszúrást melyik szabály utasítja el.
3. **Próbaadat:** [Populating a Table With Rows — PostgreSQL 16](https://www.postgresql.org/docs/16/tutorial-populate.html). Az `INSERT ... (oszloplista) VALUES (...)` formát figyeld. **Cél:** a helyes próbaadat mellett célzottan hibás sort is tudj előállítani.
4. **Két tábla összekapcsolása:** [Joins Between Tables — PostgreSQL 16](https://www.postgresql.org/docs/16/tutorial-join.html). Az első `JOIN ... ON ...` példáig és az illeszkedő sorok viselkedéséig olvasd. **Cél:** el tudd mondani, mely sorokból lesz eredmény egy belső JOIN esetén.

Az első két pont elég a **mostani, kód nélküli modellválaszhoz**. A 3–4. pontot akkor vedd elő, amikor az első SQL-t írjuk.

## Kapcsolódó gyakorlati kísérletek

1. **Előbb papíron:** írd le a labor két tábláját, az `1:N` irányát, mindkét elsődleges kulcsot, a kapcsolódó idegen kulcsot és a kötelező mezőket. Mondd el, mit engedne meg egy nullable idegen kulcs, és miért nem elég önmagában a C# oldali validáció.
2. **A modell ellenőrzése után PostgreSQLben:** hozz létre ideiglenes táblákat a labor saját SQL-fájljában, majd illessz be néhány összetartozó sort. Az ideiglenes táblák egy adatbázis-sessionhöz tartoznak, így az EF Core későbbi tartós tábláival nem ütköznek. Ehhez a [CREATE TABLE — PostgreSQL 16](https://www.postgresql.org/docs/16/sql-createtable.html) `TEMPORARY`/`TEMP` részét használd.
3. Írj egy `JOIN` lekérdezést, amely a kapcsolódó sorokat mutatja. Próbálj olyan sort beszúrni, amely nem létező szülőre mutat. Jegyezd fel a tényleges PostgreSQL-hibát, és magyarázd el, melyik constraint váltotta ki.

Ha `psql`-ben futtatod a kísérletet, az ideiglenes táblákat ugyanabban a sessionben hozd létre, töltsd fel és kérdezd le. Egy új kapcsolatban nem fognak látszani.

## Opcionális mélyítés

- [One-to-many relationships — EF Core](https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many): az M2 elején hasznos. Először csak a required kapcsolat első példáját olvasd; figyeld meg, hogyan felel meg a C# navigation és foreign key a relációs modellnek. A további konfigurációs változatok most nem szükségesek.
- A PostgreSQL constraint-oldal referenciális törlési szabályai és összetett kulcsai akkor kerülnek elő, amikor a labor modellje vagy egy valós hiba indokolja őket.

## Aktív felidézés

1. Mi a különbség a primary key és a foreign key feladata között?
2. Miért kellhet `NOT NULL` a foreign key mellé egy kötelező kapcsolathoz?
3. Milyen különbség van aközött, hogy a szülőnek lehet nulla gyereke, illetve hogy a gyereknek kötelező szülője legyen?
4. Mit bizonyít egy elutasított hibás `INSERT`, amit a séma puszta olvasása nem?
