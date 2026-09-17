# Szoftvermérnöki tanterv — modern .NET backend fókusz

## A dokumentum célja

Ez a hosszú távú tanterv azt támogatja, hogy frontendben erősebb, vállalati tapasztalattal rendelkező fejlesztőből magabiztos full-stack, majd erős backend- és senior szintű szoftvermérnökké váljak modern C# és .NET használatával.

A cél nem egy teljes informatikai diploma tananyagának reprodukálása, és nem is kizárólag keretrendszerek használatának megtanulása. A tanterv a professzionális web- és backendfejlesztéshez legnagyobb értéket adó gyakorlati mérnöki tudást, számítástudományi alapokat és mögöttes működési modelleket kapcsolja össze.

Az Angular és a .NET jelenlegi megvalósítási technológiák. A hangsúly azonban azokon az elveken és mentális modelleken van, amelyek más nyelvekben, keretrendszerekben és rendszerekben is használhatók.

### Irányadó elv: az absztrakciók függőleges összekapcsolása

A témákat nem elszigetelt technológiákként, hanem egymásra épülő rétegekként kell megérteni. Példák a később részletesen kibontandó kapcsolatokra:

```text
CPU → folyamat → szál → operációs rendszer ütemezője → ThreadPool
    → aszinkron I/O → Task → async/await → ASP.NET-kérés

relációs modell → SQL → index → B-fa → lekérdezéstervező → tranzakció
    → izoláció → MVCC / zárolás → kapcsolatpool → EF Core

URL → DNS → TCP → TLS → HTTP → reverse proxy → Kestrel
    → ASP.NET middleware → végpont → adatbázis → válasz → böngésző → Angular

forráskód → fordító → IL → CLR → JIT → gépi kód → CPU
```

Ez a szemlélet olyan tudást épít, amely a technológiák változásakor is használható marad.

## Prioritási rendszer

- **P1 — Alapvető:** közvetlenül szükséges az erős backend- és full-stack mérnöki fejlődéshez.
- **P2 — Fontos:** lényeges mérnöki tudás, amely az alapokkal együtt vagy azok után építendő fel.
- **P3 — Későbbi / specializált:** az alapok megszilárdulása után, illetve konkrét szakmai igény vagy érdeklődés esetén kerül előtérbe.

A prioritások nem végleges címkék. A tudás, a munkakör és a szakmai célok változásával egy téma feljebb vagy lejjebb kerülhet.

## Tudásmélységi rendszer

### L1 — Ismereti szint

El tudom magyarázni, mi a fogalom, miért létezik, és nagy vonalakban hogyan működik.

### L2 — Gyakorlati szint

Önállóan tudom alkalmazni, és meg tudom oldani a hozzá kapcsolódó gyakori valós problémákat.

### L3 — Mély megértés

Értem a fontos megvalósítási részleteket, kompromisszumokat, hibamódokat és teljesítményhatásokat, valamint azt is, mikor nem célszerű használni.

Nem minden témának kell elérnie az L3 szintet. Az olyan alapvető területeknél, mint a HTTP, az adatbázisok, az aszinkron programozás, a C#/.NET futásidejű működése és a backend alapmechanizmusai, gyakran indokolt a mélyebb megértés. Periférikus technológiáknál az L1 vagy L2 is elegendő lehet.

## A tanterv nyolc pillére

### 1. Programozás és .NET — P1

**Cél:** erős C#-tudás kialakítása, miközben a nyelv mögötti .NET futtatókörnyezet és absztrakciók működése is érthetővé válik.

**Részletes altématérkép:** [1. pillér — Programozás és .NET](curriculum/01-programming-dotnet.md).

Későbbi témakörök iránya:

```text
C# → típusrendszer → OOP → generikus típusok → kollekciók → LINQ
   → kivételek → delegate-ek és események → memóriakezelés → GC
   → CLR → IL / JIT → async/await → Task → szálkezelés
   → reflexió → dependency injection
```

A cél nem csupán helyes C#-kód írása, hanem annak megértése is, hogyan fordul le és fut a program, hogyan kezeli a .NET a memóriát és a konkurenciát, illetve milyen költségekkel járnak a magas szintű absztrakciók.

### 2. Web- és backendfejlesztés — P1

**Cél:** a modern webes backend működésének megértése a HTTP alapjaitól a production környezetben futó ASP.NET Core alkalmazásokig.

**Részletes altématérkép:** [2. pillér — Web- és backendfejlesztés](curriculum/02-web-backend.md).

Későbbi témakörök iránya:

```text
HTTP → API-tervezés → REST → ASP.NET Core → kérésfeldolgozási pipeline
     → middleware → controllerek / Minimal API-k → validáció → szerializáció
     → konfiguráció → DI → hibakezelés → háttérfeldolgozás → API-verziózás
```

A böngésző, az Angular kliens, a hálózat és a backend kapcsolata végig látható marad. Egy kérés teljes útját kell érteni, nem csak egy controller vagy végpont implementációját.

### 3. Adatok és adatbázisok — P1

**Cél:** az adatbázisok megértése az ORM-ektől függetlenül, majd annak feltárása, hogyan képezi le az EF Core ezeket az alapokat és milyen kompromisszumokat vezet be.

**Részletes altématérkép:** [3. pillér — Adatok és adatbázisok](curriculum/03-data-databases.md).

Későbbi témakörök iránya:

```text
relációs modell → SQL → sématervezés → normalizálás → megszorítások
    → indexek → B-fa → lekérdezéstervező → tranzakciók → ACID
    → izoláció → zárolás → MVCC → konkurencia → kapcsolatpool
    → PostgreSQL → EF Core
```

Az elsajátítás alapelve:

```text
adatbázis-alapok → PostgreSQL → EF Core
```

és nem:

```text
EF Core → adatbázis-alapok
```

Így az ORM kényelmi funkciói mellett annak generált lekérdezései, teljesítményhatásai, tranzakciós viselkedése és korlátai is értelmezhetők lesznek.

### 4. Számítógépes rendszerek — P1

**Cél:** az alkalmazás-keretrendszerek alatti számítástechnikai infrastruktúra megértése, hogy világos legyen, hogyan hajtja végre a gép a .NET-, webszerver- és elosztott alkalmazásokat.

**Részletes altématérkép:** [4. pillér — Számítógépes rendszerek](curriculum/04-computer-systems.md).

#### Számítógép-architektúra

CPU, utasítások, regiszterek, gyorsítótárak, RAM és virtuális memória; kapcsolatuk a programok futásával és teljesítményével.

#### Operációs rendszerek

Folyamatok, szálak, ütemezés, verem és heap, rendszerhívások, fájlok és I/O; kapcsolatuk a .NET futtatókörnyezettel.

#### Konkurencia

Szálak, szinkronizáció, zárolások, versenyhelyzetek, holtpontok, ThreadPool és aszinkron I/O; közvetlen kapcsolatuk a `Task`, az `async/await` és a backend terhelhetőségének megértésével.

#### Hálózatok

TCP/IP, portok, socketek, DNS, TLS, HTTP és proxyk; kapcsolatuk a böngészőkkel, API-kkal, webszerverekkel és üzemeltetési infrastruktúrával.

### 5. Szoftverfejlesztés és architektúra — P1

**Cél:** továbblépés a funkciók implementálásától a karbantartható rendszerek tudatos tervezése felé.

**Részletes altématérkép:** [5. pillér — Szoftverfejlesztés és architektúra](curriculum/05-software-engineering-architecture.md).

Későbbi témakörök iránya:

```text
tiszta kód → kohézió és csatolás → SOLID → kompozíció
    → tervezési minták → refaktorálás → tesztelés → modularitás
    → réteges architektúra → Clean Architecture → DDD-alapok
    → moduláris monolit → mikroszolgáltatások kompromisszumai
```

Kapcsolódó mérnöki gyakorlatok: Git, kódellenőrzés, követelmények értelmezése, hibakeresés, karbantarthatóság és technikai adósság kezelése.

Az architektúrát elsősorban döntések és kompromisszumok elemzésével kell tanulni, nem minták és elnevezések memorizálásával. A megfelelő megoldás a rendszer méretétől, változási irányaitól, csapatszerkezettől és üzemeltetési igényektől függ.

### 6. Biztonság és identitáskezelés — P2

**Cél:** elegendő biztonsági tudás kialakítása modern webalkalmazások biztonságos tervezéséhez és megvalósításához.

**Részletes altématérkép:** [6. pillér — Biztonság és identitáskezelés](curriculum/06-security-identity.md).

Későbbi témakörök iránya:

```text
biztonsági alapelvek → fenyegetésmodellezés → hash-elés → titkosítás → TLS
    → cookie-k → munkamenetek → JWT → hitelesítés → jogosultságkezelés
    → OAuth 2.0 → OpenID Connect → Keycloak → CORS → CSRF → XSS
    → SQL-injection → titokkezelés
```

A fogalmakat később gyakorlati Angular + ASP.NET Core rendszerekhez kell kapcsolni. A cél annak megértése, hogy hol húzódnak a bizalmi határok, milyen támadási felületek keletkeznek, és melyik védelmi mechanizmus milyen fenyegetést kezel.

### 7. Infrastruktúra és production engineering — P2

**Cél:** annak megértése, mi történik az alkalmazáskóddal azután, hogy elhagyja a fejlesztői gépet, és hogyan lehet egy futó rendszert megbízhatóan megfigyelni és hibakeresni.

**Részletes altématérkép:** [7. pillér — Infrastruktúra és production engineering](curriculum/07-infrastructure-production.md).

Későbbi témakörök iránya:

```text
Linux → shell → folyamatok → fájlrendszer → jogosultságok → Docker
    → image-ek → konténerek → volume-ok → hálózatok → Docker Compose
    → reverse proxy → HTTPS → konfiguráció → CI/CD → telepítés
    → felhőalapok

naplózás → metrikák → tracing → monitorozás → health checkek
    → riasztások → production hibakeresés
```

Az átfogó mentális modell:

```text
kód → build → artifact / konténer → telepítés → hálózat
    → folyamat → megfigyelhetőség → felhasználók
```

### 8. Elosztott rendszerek és rendszertervezés — kezdetben P2, később P1

**Cél:** architekturális és rendszertervezési képességek kialakítása, miután az alacsonyabb szintű alapok már elég stabilak.

**Részletes altématérkép:** [8. pillér — Elosztott rendszerek és rendszertervezés](curriculum/08-distributed-systems-design.md).

Későbbi témakörök iránya:

```text
skálázás → terheléselosztás → gyorsítótárazás → sorok → message brokerek
    → újrapróbálkozás → idempotencia → eventual consistency → replikáció
    → particionálás → CAP → elosztott tranzakciók → szolgáltatások kommunikációja
    → hibamódok
```

A későbbi rendszertervezési feladatok egymással összefüggésben vizsgálják majd az összetevőket:

```text
CDN → load balancer → API → cache → adatbázis → queue
    → workerek → tárolás → megfigyelhetőség
```

Ez a pillér csak fokozatosan válik P1 prioritásúvá. Nem előzheti meg a programozási, adatbázis-, hálózati, operációs rendszerbeli és backend alapok megszilárdítását.

## Keresztmetszeti sáv: számítástudományi alapok

A számítástudományi alapok nem különálló, több hónapos elméleti fázist alkotnak. Akkor kerülnek elő, amikor gyakorlati összefüggést és magyarázó erőt adnak valamelyik mérnöki témához.

| Alapterület | Mérnöki kapcsolódás |
|---|---|
| **Adatszerkezetek** | C# kollekciók, algoritmusválasztás, cache-ek, adatbázisok és belső implementációk megértése. |
| **Algoritmusok** | Valós problémák felbontása, adatfeldolgozás, keresés és rendezés, valamint teljesítménydöntések. |
| **Számítási komplexitás / Big-O** | Megoldások skálázódásának becslése, rejtett költségek felismerése és tudatos teljesítmény-kompromisszumok. |
| **Diszkrét számítástudományi alapok** | Logika, halmazok, relációk, gráfok és kombinatorikai gondolkodás a programozás, adatmodellezés és rendszertervezés szolgálatában. |
| **Számítógép-architektúra** | A CLR, JIT, memóriakezelés, cache-viselkedés és futásidejű teljesítmény háttere. |
| **Operációs rendszerek** | .NET-folyamatok, szálak, memória, fájlok, rendszerhívások és I/O működése. |
| **Hálózatok** | HTTP, API-k, reverse proxyk, konténerek, felhő és backend infrastruktúra alapja. |
| **Konkurencia** | Szálkezelés, `Task`, `async/await`, szinkronizáció, adatkonkurencia és backend terhelhetőség. |

Egy témát először olyan mélységig érdemes megtanulni, amennyire az aktuális gyakorlati probléma indokolja, majd későbbi találkozások során mélyíteni és más pillérekhez visszakötni.

## Tudatosan alacsony prioritású területek

Az alábbiak legitim és értékes számítástudományi területek, de a jelenlegi cél — erős professzionális web- és backendmérnökké válni — szempontjából kezdetben alacsonyabb a várható megtérülésük:

- emelt szintű analízis;
- mély lineáris algebra;
- numerikus módszerek;
- számítógépes grafika;
- gépi tanulás elmélete;
- mély fordítóprogram-építés;
- mély automataelmélet;
- kiszámíthatóságelmélet;
- formális nyelvek;
- számítási geometria.

Ezek nem „haszontalan” témák, és nincsenek végleg kizárva. P3 prioritással később bekerülhetnek, ha szakmai feladat, specializáció, továbbtanulás vagy személyes érdeklődés indokolja őket. Az esetleg szükséges részfogalmak korábban is megjelenhetnek, ha egy magasabb prioritású téma előfeltételei.

## Tanulási stratégia

### Strukturált, de rugalmas

A tanterv később ajánlott tanulási sorrendet kap, de nem akadályozhatja a munkában aktuálissá váló témák ideiglenes előrevételét. Az így megszerzett tudást később vissza kell kapcsolni a megfelelő fogalmi helyére, előfeltételeire és kapcsolódó témáira.

### Elmélet és megvalósítás együtt

Lehetőség szerint minden fontos témát ezen a láncon keresztül kell feldolgozni:

```text
fogalom → megvalósítási működés → gyakorlati használat → hibamódok → kompromisszumok
```

Például az EF Core tranzakciókezelése nem választható el az adatbázis-tranzakciók megértésétől. Az `async/await` gyakorlati használatához pedig idővel hozzá kell kapcsolni a szálakat, az ütemezést, a ThreadPool működését és az aszinkron I/O-t.

### Munkából származó megerősítés

A szakmai munka során felmerülő valós hibákat, követelményeket és tervezési döntéseket gyakorlati példaként és feladatként kell felhasználni. Ezeket érdemes visszavezetni az érintett alapfogalmakhoz, majd a megoldás után rögzíteni a tanulságokat és a kompromisszumokat.

### Átvihető tudás először

Elsőbbséget élveznek azok a fogalmak, amelyek nyelveken és keretrendszereken át is értékesek. A framework-specifikus tudás fontos, de általános programozási, webes, adatbázis-, rendszer- és architekturális alapokra épül.

## Haladás követése

A pillérek altémái fokozatosan, az aktuális tanulási irány és a bizonyítékok szerint készülnek el. Mind a nyolc pillér első részletes, egyenként 15 altémás bontása elkészült. Az altémák az alábbi egyszerű metaadatokat kapják:

- **Prioritás:** P1 / P2 / P3
- **Célmélység:** L1 / L2 / L3
- **Jelenlegi mélység:** L0 / L1 / L2 / L3
- **Állapot:** Nincs elkezdve / Tanulás alatt / Gyakorolt / Magabiztos
- **Előfeltételek**
- **Kapcsolódó témák**
- **Gyakorlati feladatok**

Az **L0** itt kizárólag a haladás jelölésére szolgál: a témában még nincs érdemi, ellenőrzött tudás. A nyilvántartás célja a következő értelmes tanulási lépés kiválasztása, nem pedig egy minél nagyobb checklist kipipálása.

A stabil altémadefiníciók a `curriculum/` könyvtárba, a teljes változó haladási állapot a `CURRICULUM_PROGRESS.md` fájlba, az aktuális 3–5 fókusztéma pedig a `LEARNING_STATE.md` fájlba kerül. Az `Unassessed` azt jelenti, hogy még nem történt célzott felmérés; nem azonos az L0-val.

## Következő iterációk

Az első teljes pillértérkép elkészült. A későbbi iterációk:

1. az aktivált altémák határait és célbizonyítékait valós tapasztalat alapján finomítják;
2. azonosítják a témák közötti függőségeket;
3. hozzárendelik a célként kitűzött tudásmélységet;
4. rögzítik az előfeltételeket;
5. ajánlott tanulási sorrendet alakítanak ki;
6. feltérképezik a meglévő tudást és a hiányosságokat;
7. gyakorlati feladatokat adnak hozzá;
8. összekötik a témákat a valós Angular- és .NET-munkával;
9. válogatott, magas minőségű tanulási forrásokat rendelnek a témákhoz;
10. követhető roadmapet hoznak létre anélkül, hogy a dokumentum kezelhetetlen ellenőrzőlistává válna.

Ez a curriculum szándékosan stabil tudástérképet rögzít, nem heti beosztást vagy több száz részletes leckét. A konkrét sorrendet és feladatokat az aktuális projekt, a bizonyítékok és az egyetlen következő tanulási lépés határozza meg.
