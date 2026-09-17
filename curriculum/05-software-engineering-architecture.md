# 5. pillér — Szoftverfejlesztés és architektúra

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a működő funkciók elkészítésétől a hosszú távon módosítható, tesztelhető és indokolható rendszerek építéséig szükséges mérnöki kompetenciákat bontja fel. A hangsúly a problémák tisztázásán, a visszajelzési hurkokon, a kód- és modulhatárokon, valamint az architekturális trade-offok bizonyíték-alapú értékelésén van.

A pillér nem konkrét architektúrát ír elő minden projektre. A Clean Architecture, DDD, moduláris monolit és mikroszolgáltatás eszközök és döntési modellek; csak akkor indokoltak, ha a rendszer változási irányai, minőségi követelményei és csapathatárai valóban igénylik őket.

## Ajánlott tanulási útvonal

Az útvonal a gyors, biztonságos visszajelzési ciklussal kezdődik. A tesztelés nem a design patternök utáni külön blokk, hanem már az első működő funkció és minden későbbi refaktorálás része. Architektúrát csak megfigyelt változási, minőségi vagy csapategyüttműködési probléma indokol.

1. **Mindennapi delivery-loop:** `SE-REQUIREMENTS-USE-CASES` → `SE-VERSION-CONTROL-COLLABORATION` → `SE-TESTING-FUNDAMENTALS` → `SE-DEBUGGING-DIAGNOSTICS` → `SE-CODE-QUALITY-READABILITY`. Egy kis változásnak tisztázott célja, áttekinthető diffje, automatizált vagy megismételhető ellenőrzése és diagnosztizálható eredménye van.
2. **Biztonságos változtatás:** `SE-REFACTORING-TECH-DEBT` → `SE-MODULARITY-COUPLING` → `SE-TEST-STRATEGY`, mellettük a `SE-ARCHITECTURE-DECISIONS` könnyű, alternatívákat és következményeket rögzítő szelete. A határ vagy absztrakció csak konkrét változási okra reagál.
3. **Problémavezérelt tervezés:** `SE-ARCHITECTURE-BOUNDARIES` → `SE-SOLID-DESIGN` → `SE-DESIGN-PATTERNS`. Az elvek és minták diagnosztikai és kommunikációs eszközök, nem kötelező checklist vagy előre felépítendő rétegrendszer.
4. **Architektúra lépték szerint:** `SE-DOMAIN-MODELING-DDD`, `SE-LAYERED-CLEAN-ARCHITECTURE` és `SE-MODULAR-MONOLITHS` csak akkor mélyülnek, amikor a domain, a változási tengelyek, a csapat- vagy deployment-határok ezt indokolják; a döntések eredményét később vissza kell mérni.

Ez végrehajtási iránytű, nem merev kurzusterv. A legegyszerűbb működő szerkezet az alapértelmezés; a következő absztrakcióhoz megnevezhető probléma, várható előny és ellenőrizhető következmény szükséges.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `SE-REQUIREMENTS-USE-CASES` | Követelmények, use case-ek és elfogadási feltételek | P1 | L2 |
| `SE-VERSION-CONTROL-COLLABORATION` | Git, változáskezelés és együttműködés | P1 | L2 |
| `SE-DEBUGGING-DIAGNOSTICS` | Szisztematikus hibakeresés és diagnosztika | P1 | L3 |
| `SE-CODE-QUALITY-READABILITY` | Olvasható kód és lokális tervezés | P1 | L2 |
| `SE-REFACTORING-TECH-DEBT` | Refaktorálás és technikai adósság | P1 | L2 |
| `SE-MODULARITY-COUPLING` | Modularitás, kohézió és csatolás | P1 | L3 |
| `SE-SOLID-DESIGN` | SOLID és objektumorientált tervezési elvek | P1 | L2 |
| `SE-DESIGN-PATTERNS` | Tervezési minták és alkalmazási trade-offjaik | P2 | L2 |
| `SE-TESTING-FUNDAMENTALS` | Tesztelési alapok és tesztdizájn | P1 | L3 |
| `SE-TEST-STRATEGY` | Tesztstratégia és automatizált visszajelzés | P1 | L2 |
| `SE-ARCHITECTURE-BOUNDARIES` | Komponenshatárok és függőségi irányok | P1 | L3 |
| `SE-LAYERED-CLEAN-ARCHITECTURE` | Réteges és Clean Architecture megközelítések | P2 | L2 |
| `SE-DOMAIN-MODELING-DDD` | Domainmodellezés és DDD-alapok | P2 | L2 |
| `SE-MODULAR-MONOLITHS` | Moduláris monolit és szolgáltatáshatárok | P2 | L2 |
| `SE-ARCHITECTURE-DECISIONS` | Minőségi attribútumok és architekturális döntések | P1 | L3 |

## Első munkaképes szelet és későbbi mélyítés

| Azonosító | Első munkaképes szelet | Későbbi mélyítés vagy trigger |
|---|---|---|
| `SE-REQUIREMENTS-USE-CASES` | Homályos igényből kis use case, edge case, scope és ellenőrizhető elfogadási feltétel. | Nem funkcionális követelmények, több stakeholder és konfliktusos prioritások összetett feature vagy rendszer esetén. |
| `SE-VERSION-CONTROL-COLLABORATION` | Kis, koherens commitok, diff/history olvasása, biztonságos revert és érdemi review-visszajelzés. | Branch/rebase stratégia, konfliktuskezelés és többcsapatos workflow tényleges együttműködési problémánál. |
| `SE-DEBUGGING-DIAGNOSTICS` | Reprodukció, tünet/ok különválasztása, hipotézis, kontrollált kísérlet, stack trace/log és regresszió-ellenőrzés. | Dump, profiler és többkomponensű diagnózis olyan hibánál, amelyet lokális eszközökkel nem lehet leszűkíteni. |
| `SE-CODE-QUALITY-READABILITY` | Jó elnevezés, fókuszált egység, explicit invariáns, egyszerű vezérlés és indokolt komment. | Nagyobb konzisztencia- vagy dokumentációs szabályok ismétlődő olvashatósági és onboarding-problémánál. |
| `SE-REFACTORING-TECH-DEBT` | Egy smell viselkedést védő teszt melletti, kis lépésekben végzett javítása és tudatos halasztás rögzítése. | Strangler jellegű átalakítás és adósságportfólió üzleti kockázat, gyakori változás vagy magas hibaköltség esetén. |
| `SE-MODULARITY-COUPLING` | Felelősség, kohézió, public contract és függőségi irány egy változási ok mentén. | Ciklikus függés, stabilitás és komponensgráf elemzése több modul vagy párhuzamos csapatmunka esetén. |
| `SE-SOLID-DESIGN` | Egy konkrét elvsértés következményének felismerése, minimális javítása és a túlzott absztrakció elkerülése. | Az öt elv közötti trade-offok összetett objektumgráf vagy gyakori variációs igény esetén. |
| `SE-DESIGN-PATTERNS` | Már létező strategy/adapter/decorator/factory jellegű megoldás felismerése és probléma alapján történő kiválasztása. | Command, observer vagy repository és alternatíváik összevetése ismétlődő, névvel kommunikálható tervezési problémánál. |
| `SE-TESTING-FUNDAMENTALS` | Viselkedést ellenőrző sikeres, hibás és határeset; megfelelő unit/integration határ és valódi regresszió. | Test double, tesztelhető dizájn és törékeny teszt javítása összetett függőségek vagy lassú visszajelzés esetén. |
| `SE-TEST-STRATEGY` | A feature legfontosabb kockázataihoz illeszkedő kis tesztportfólió és gyors helyi feedback. | Contract/acceptance rétegek, flaky suite, tesztadat és CI-idő optimalizálása növekvő rendszer vagy csapat esetén. |
| `SE-ARCHITECTURE-BOUNDARIES` | Felelősség-, adat- és függőségi határ egy konkrét változási vagy ownership-probléma alapján. | Process/deployment határ és több komponens közötti contract nagyobb rendszer vagy független életciklus esetén. |
| `SE-LAYERED-CLEAN-ARCHITECTURE` | A presentation/application/domain/infrastructure szerepek felismerése és csak indokolt határ bevezetése. | Port/adapter, dependency rule és architecture test nem triviális integrációs vagy változási nyomás esetén. |
| `SE-DOMAIN-MODELING-DDD` | Ubiquitous language, entity/value object és valódi üzleti invariáns egy kis modellben. | Aggregate, domain service, bounded context és context mapping összetett domain vagy csapathatár esetén. |
| `SE-MODULAR-MONOLITHS` | Monolit és moduláris monolit különbsége, modul-ownership és belső contract magas szintű modellje. | Határok kikényszerítése, modulonkénti adat és kiválaszthatóság több üzleti terület vagy szolgáltatási igény esetén. |
| `SE-ARCHITECTURE-DECISIONS` | Rövid döntésfeljegyzés: kontextus, alternatívák, választás, következmények és felülvizsgálati trigger. | Quality-attribute scenario, fitness function és evolutionary architecture jelentős, nehezen visszafordítható döntésnél. |

## Altémák részletei

### `SE-REQUIREMENTS-USE-CASES` — Követelmények, use case-ek és elfogadási feltételek

- **Tartalom:** probléma és megoldás elkülönítése, stakeholder, funkcionális és nem funkcionális követelmény, use case, user story, invariáns, edge case, acceptance criterion és scope.
- **Előfeltételek:** alapvető projektmunka.
- **Kapcsolatok:** API-szerződések a 2. pillérben, domainmodellezés és `SE-ARCHITECTURE-DECISIONS`.
- **Célbizonyíték:** homályos igényből ellenőrizhető use case-ek és elfogadási feltételek készítése, nyitott döntések azonosítása és indokolatlan scope-növekedés felismerése.

### `SE-VERSION-CONTROL-COLLABORATION` — Git, változáskezelés és együttműködés

- **Tartalom:** commit, branch, merge/rebase alapmodell, konfliktus, diff, history, revert, pull request, code review, kis változások és biztonságos együttműködési workflow.
- **Előfeltételek:** fájlok és forráskód alapvető kezelése.
- **Kapcsolatok:** release engineering a 7. pillérben, `SE-REFACTORING-TECH-DEBT`.
- **Célbizonyíték:** áttekinthető commit-sorozat készítése, konfliktus vagy hibás változás biztonságos kezelése és review-visszajelzés érdemi feldolgozása.

### `SE-DEBUGGING-DIAGNOSTICS` — Szisztematikus hibakeresés és diagnosztika

- **Tartalom:** reprodukció, tünet és ok elkülönítése, hipotézis, változók kontrollja, minimal reproduction, debugger, log, stack trace, dump/profiler alapok és regresszió-ellenőrzés.
- **Előfeltételek:** a vizsgált technológia alapvető használata.
- **Kapcsolatok:** exceptionök az 1. pillérben, webes hibakezelés a 2. pillérben, observability a 7. pillérben.
- **Célbizonyíték:** ismeretlen hiba bizonyíték-alapú leszűkítése, gyökérok igazolása, javítás ellenőrzése és a diagnosztikai út világos dokumentálása.

### `SE-CODE-QUALITY-READABILITY` — Olvasható kód és lokális tervezés

- **Tartalom:** elnevezés, kis és fókuszált egységek, guard clause, explicit invariáns, mellékhatás, duplikáció, komment és dokumentáció szerepe, egyszerűség és konzisztencia.
- **Előfeltételek:** biztos nyelvi alapok az 1. pillérben.
- **Kapcsolatok:** `SE-REFACTORING-TECH-DEBT`, `SE-MODULARITY-COUPLING`.
- **Célbizonyíték:** működő kód olvashatóságának javítása viselkedésváltozás nélkül, a választott változtatások indoklása és a túlzott absztrakció felismerése.

### `SE-REFACTORING-TECH-DEBT` — Refaktorálás és technikai adósság

- **Tartalom:** refaktorálás kontra funkcióváltozás, code smell, strangler jellegű kis lépések, characterization test, adósság oka és kamata, kockázat, prioritás és tudatos halasztás.
- **Előfeltételek:** `SE-CODE-QUALITY-READABILITY`, `SE-TESTING-FUNDAMENTALS` alapjai.
- **Kapcsolatok:** `SE-MODULARITY-COUPLING`, architektúra-evolúció.
- **Célbizonyíték:** konkrét smell biztonságos, kis lépésekben végzett javítása, regresszió kizárása és egy technikaiadósság-tétel üzleti/mérnöki trade-offjának megfogalmazása.

### `SE-MODULARITY-COUPLING` — Modularitás, kohézió és csatolás

- **Tartalom:** felelősség, cohesion, coupling, information hiding, public contract, dependency direction, afferent/efferent függés, stabilitás és változási tengely.
- **Előfeltételek:** objektummodell és kompozíció az 1. pillérben.
- **Kapcsolatok:** `SE-SOLID-DESIGN`, `SE-ARCHITECTURE-BOUNDARIES`, DI az 1–2. pillérben.
- **Célbizonyíték:** komponenshatárok kialakítása változási okok alapján, ciklikus vagy indokolatlan függőség felismerése és a modularitási trade-off magyarázata.

### `SE-SOLID-DESIGN` — SOLID és objektumorientált tervezési elvek

- **Tartalom:** SRP, OCP, LSP, ISP és DIP eredeti problémái, interface-ek és kompozíció, tesztelhetőség, valamint az elvek túlhúzásának költsége.
- **Előfeltételek:** objektummodell, interface-ek és `SE-MODULARITY-COUPLING`.
- **Kapcsolatok:** `SE-DESIGN-PATTERNS`, `SE-LAYERED-CLEAN-ARCHITECTURE`.
- **Célbizonyíték:** valós kódban egy elvsértés következményének felismerése és javítása, miközben a tanuló megindokolja, mikor lenne a további absztrakció indokolatlan.

### `SE-DESIGN-PATTERNS` — Tervezési minták és alkalmazási trade-offjaik

- **Tartalom:** stratégia, adapter, decorator, factory, observer, command és repository minták problémaközpontú vizsgálata; pattern, idiom és framework-mechanizmus elkülönítése.
- **Előfeltételek:** `SE-MODULARITY-COUPLING`, `SE-SOLID-DESIGN`.
- **Kapcsolatok:** delegate-ek és DI az 1. pillérben, middleware a 2. pillérben.
- **Célbizonyíték:** egy minta indokolt alkalmazása konkrét változási problémára, alternatíva összehasonlítása és pattern-overuse felismerése.

### `SE-TESTING-FUNDAMENTALS` — Tesztelési alapok és tesztdizájn

- **Tartalom:** teszt célja, arrange–act–assert, állapot- és viselkedésellenőrzés, unit/integration/end-to-end határ, determinisztikusság, izoláció, test double és tesztelhető dizájn.
- **Előfeltételek:** legalább egy működő komponens vagy use case.
- **Kapcsolatok:** API-tesztelés a 2. pillérben, adatbázis-integráció a 3. pillérben.
- **Célbizonyíték:** értelmes sikeres, hibás és határesetek automatizált tesztelése, valódi regresszió kimutatása és törékeny vagy értéktelen teszt felismerése.

### `SE-TEST-STRATEGY` — Tesztstratégia és automatizált visszajelzés

- **Tartalom:** kockázatalapú tesztportfólió, test pyramid/trophy trade-off, contract és acceptance teszt, coverage korlátai, flaky teszt, tesztadat, futási idő és CI feedback loop.
- **Előfeltételek:** `SE-TESTING-FUNDAMENTALS`.
- **Kapcsolatok:** CI/CD a 7. pillérben, szerződés-evolúció a 2. pillérben.
- **Célbizonyíték:** projektkockázatokhoz illeszkedő tesztstratégia készítése, rétegek és felelősségek indoklása, valamint lassú vagy flaky suite diagnózisa.

### `SE-ARCHITECTURE-BOUNDARIES` — Komponenshatárok és függőségi irányok

- **Tartalom:** komponens, modul, interface, contract, dependency graph, policy és mechanism elkülönítése, process- és deployment-határ, adat- és felelősségtulajdon.
- **Előfeltételek:** `SE-MODULARITY-COUPLING`, use case-ek.
- **Kapcsolatok:** `SE-LAYERED-CLEAN-ARCHITECTURE`, `SE-MODULAR-MONOLITHS`, elosztott rendszerek a 8. pillérben.
- **Célbizonyíték:** rendszer komponensekre bontása világos felelősségekkel, függőségi irányok indoklása és rossz helyen meghúzott határ következményeinek elemzése.

### `SE-LAYERED-CLEAN-ARCHITECTURE` — Réteges és Clean Architecture megközelítések

- **Tartalom:** presentation/application/domain/infrastructure szerepek, dependency inversion, port és adapter, use-case orchestration, DTO- és persistence-határ, valamint az architekturális ceremony költsége.
- **Előfeltételek:** `SE-ARCHITECTURE-BOUNDARIES`, `SE-SOLID-DESIGN`.
- **Kapcsolatok:** DI az 1–2. pillérben, EF Core a 3. pillérben.
- **Célbizonyíték:** indokolt réteghatárok kialakítása egy nem triviális alkalmazásban, függőségi szabály ellenőrzése és annak felismerése, amikor az egyszerűbb szerkezet jobb.

### `SE-DOMAIN-MODELING-DDD` — Domainmodellezés és DDD-alapok

- **Tartalom:** ubiquitous language, entity, value object, aggregate, invariant, domain service, bounded context és context mapping alapjai; anemic és rich model trade-off.
- **Előfeltételek:** `SE-REQUIREMENTS-USE-CASES`, objektummodell és `SE-ARCHITECTURE-BOUNDARIES`.
- **Kapcsolatok:** relációs modellezés a 3. pillérben, szolgáltatáshatárok a 8. pillérben.
- **Célbizonyíték:** üzleti szabályokat hordozó kis domainmodell kialakítása, aggregate- és bounded-context határ indoklása, valamint adatmodell és domainmodell megkülönböztetése.

### `SE-MODULAR-MONOLITHS` — Moduláris monolit és szolgáltatáshatárok

- **Tartalom:** monolit, moduláris monolit és mikroszolgáltatás különbsége, modulonkénti ownership, belső contract, tranzakciós határ, adatmegosztás és későbbi kiválaszthatóság.
- **Előfeltételek:** `SE-ARCHITECTURE-BOUNDARIES`, `SE-DOMAIN-MODELING-DDD` alapjai.
- **Kapcsolatok:** deployment a 7. pillérben, elosztott rendszerek a 8. pillérben.
- **Célbizonyíték:** több modulból álló monolit határainak megtervezése és kikényszerítése, valamint a szétosztás előnyeinek és működési költségeinek reális elemzése.

### `SE-ARCHITECTURE-DECISIONS` — Minőségi attribútumok és architekturális döntések

- **Tartalom:** quality attribute, constraint, trade-off, ADR, kockázat, fitness function, evolutionary architecture, build-versus-buy és monolit/mikroszolgáltatás döntési tényezői.
- **Előfeltételek:** `SE-REQUIREMENTS-USE-CASES`; az `SE-ARCHITECTURE-BOUNDARIES` a nagyobb komponens- és deploymentdöntésekhez szükséges, nem egy könnyű döntésfeljegyzéshez.
- **Kapcsolatok:** production engineering a 7. pillérben, rendszertervezés a 8. pillérben.
- **Célbizonyíték:** alternatívák és következmények alapján dokumentált architekturális döntés, utólagos eredményértékelés és új bizonyíték esetén biztonságos irányváltás.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** a véges mérföldkőterv, explicit exit gate, regressziós próbák és lépésenkénti hibadiagnózis ad kezdeti bizonyítékot a követelmény-, tesztelési és diagnosztikai témákhoz. A szándékosan minimális szerkezet egyben példa az indokolatlan rétegek elkerülésére.
- **MovieExplorer:** use case-eket, külön API/importer projektet, dokumentált adat- és API-döntéseket, valamint valós fordítási és futási diagnózist tartalmaz. Ez még nem bizonyítja automatikusan a SOLID, Clean Architecture vagy DDD önálló alkalmazását.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
