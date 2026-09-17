# Teljes curriculum — haladási nyilvántartás

Utolsó frissítés: 2026-09-17

## Szerep és értelmezés

Ez a fájl a teljes curriculum változó haladási állapotának forrása. A `LEARNING_STATE.md` csak az aktuális 3–5 fókusztémát és az egyetlen következő lépést tartalmazza; a projektek `MENTORING_STATE.md` fájljai őrzik a részletes végrehajtási bizonyítékot.

- **Unassessed:** még nem történt célzott felmérés; nem állítja, hogy nincs korábbi tudás.
- **L0:** történt érintés vagy felmérés, de még nincs érdemi, ellenőrzött tudásbizonyíték.
- **L1–L3:** a curriculum tudásmélységi definíciói szerinti bizonyított szint.
- **Not Started / Learning / Practiced / Confident:** a tanulási állapot; nem helyettesíti a tudásmélységet.

Nem számítunk teljes curriculum-százalékot. A haladást pillérenként, altémánként, célmélységhez és bizonyítékhoz viszonyítva értékeljük.

## Pillérszintű lefedettség

| Pillér | Részletes bontás | Jelenlegi helyzet |
|---|---|---|
| 1. Programozás és .NET | `Planned` | Van megőrzött bizonyíték, de még nincs egységes altématérképre vetítve. |
| 2. Web- és backendfejlesztés | [Részletezve](curriculum/02-web-backend.md) | Aktív elsődleges pillér; az első bizonyítéktérkép elkészült. |
| 3. Adatok és adatbázisok | `Planned` | MovieExplorerből van megőrzött SQL/PostgreSQL/EF Core bizonyíték, még nincs normalizálva. |
| 4. Számítógépes rendszerek | `Planned` | Process/port/socket területen van bizonyíték, még nincs teljesen felbontva. |
| 5. Szoftverfejlesztés és architektúra | `Planned` | Nincs egységesen felmérve. |
| 6. Biztonság és identitáskezelés | `Planned` | Nincs egységesen felmérve. |
| 7. Infrastruktúra és production engineering | `Planned` | Nincs egységesen felmérve. |
| 8. Elosztott rendszerek és rendszertervezés | `Planned` | Nincs egységesen felmérve. |
| Keresztmetszeti számítástudományi alapok | `Planned` | Nincs egységesen felmérve. |

## 2. pillér — Web- és backendfejlesztés

| Témaazonosító | Cél | Bizonyított mélység | Állapot | Bizonyíték és nyitott rés |
|---|---:|---:|---|---|
| `WEB-HTTP-SEMANTICS` | L3 | L1 | Practiced | `200`, `400`, `404`, `405`, `418`, `500` futási próbák; a kliens- és szerverhibák alapvető magyarázata. Nyitott: safe/idempotent szemantika, cache, protokollmélység. |
| `WEB-REQUEST-LIFECYCLE` | L2 | L1 | Practiced | Kestrel → middleware → endpoint → response útvonal több ága ellenőrizve; `Response.HasStarted` megfigyelés. Az önálló teljes lifecycle-magyarázat az aktív `M6` gate része. |
| `WEB-ROUTING-ENDPOINTS` | L2 | L1 | Practiced | `/`, `/health`, `/greet/{name}`, `/divide`; `404`/`405` különbség és `MapGet` szerepe ellenőrizve. Nyitott: constraint, metadata, route-konfliktus. |
| `WEB-BINDING-VALIDATION` | L2 | L1 | Practiced | Route/query binding, hiányzó és hibás `int` automatikus `400`, nulla osztó explicit `400`; handler előtti binding felismerve. Nyitott: body/header/form binding és összetettebb validáció. |
| `WEB-MIDDLEWARE-PIPELINE` | L2 | L1 | Practiced | Inline logging middleware, előtte/utána ág, short-circuit `418`, exception middleware és sorrend gyakorlati ellenőrzése. Nyitott: branching és új kontextusú önálló alkalmazás. |
| `WEB-RESPONSES-SERIALIZATION` | L2 | L0 | Learning | JSON és text válaszok, `IResult`, `Results.Ok`/`BadRequest` használata megtörtént. A szerializációs és content-negotiation mentális modell még nincs felmérve. |
| `WEB-ERROR-HANDLING` | L2 | L1 | Practiced | `/fail`, kontrollált JSON `500`, `Response.HasStarted`, explicit és automatikus `400`; a `4xx`/`5xx` felelősségi különbsége saját szavakkal megfogalmazva. |
| `WEB-API-DESIGN-CONTRACTS` | L2 | — | Unassessed | Vannak működő endpointok, de önálló szerződéstervezési bizonyíték még nincs értékelve. |
| `WEB-DI-CONFIGURATION` | L2 | L0 | Learning | `RouteHandlerOptions` konfiguráció használata megtörtént. DI lifetime-ok, providerek és options pattern még nem kerültek felmérésre. |
| `WEB-ASYNC-CANCELLATION` | L2 | L1 | Learning | Aszinkron body-írás, flush és kivételterjedés gyakorlati érintése; az `await` hibás `Task`-ot újradobó működése felidézve. Cancellation és backpressure nyitott. |
| `WEB-API-TESTING` | L2 | L0 | Learning | Kézi `curl` regressziós mátrix készült; automatizált integration teszt még nincs. |
| `WEB-SECURITY-INTEGRATION` | L2 | — | Unassessed | Ebben a tanulási szeletben tudatosan nincs autentikáció vagy authorization. |
| `WEB-BACKGROUND-PROCESSING` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `WEB-API-EVOLUTION` | L2 | — | Unassessed | Még nincs célzott bizonyíték. |
| `WEB-OBSERVABILITY-PERFORMANCE` | L2 | L0 | Learning | Strukturált request/response logging készült; metrics, tracing és mérésalapú teljesítményelemzés még nyitott. |

### Bizonyítékforrások

- [Aktív globális állapot](LEARNING_STATE.md)
- [Backend Foundations projektállapot](../sajat/BackendFoundations/MENTORING_STATE.md)
- [MovieExplorer projektállapot](../sajat/MovieExplorer/MENTORING_STATE.md)

## Következő curriculum-karbantartási lépés

A 2. pillér bontását az aktív Backend Foundations `M6` és exit audit után felül kell vizsgálni. A következő pillér részletes bontását csak az új elsődleges tanulási irány kiválasztása után kell elkészíteni.
