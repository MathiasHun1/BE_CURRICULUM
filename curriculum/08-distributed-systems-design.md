# 8. pillér — Elosztott rendszerek és rendszertervezés

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a több processen, gépen vagy szolgáltatáson át működő rendszerek sajátos hibamódjait és tervezési döntéseit bontja stabil, mérhető kompetenciákra. A cél annak megértése, hogyan változik a helyesség, késleltetés, rendelkezésre állás és üzemeltetési költség, amikor a komponensek hálózaton kommunikálnak és egymástól függetlenül hibázhatnak.

A mikroszolgáltatás nem alapértelmezett cél. A pillér a szükséges mechanizmusokat és trade-offokat tanítja; a komponenshatárok az 5., az infrastruktúra a 7., az adatbázis belső működése pedig a 3. pillérben található.

## Ajánlott tanulási hullámok

1. **Elosztott alapmodell:** `DIST-FAILURE-MODELS` → `DIST-COMMUNICATION-CONTRACTS` → `DIST-TIME-ORDERING-IDENTITY` → `DIST-CONSISTENCY-CAP` → `DIST-REPLICATION` → `DIST-PARTITIONING` → `DIST-CONSENSUS-COORDINATION`.
2. **Skálázás és adatút:** `DIST-CACHING-CDN` → `DIST-LOAD-BALANCING-SCALING` → `DIST-MESSAGING-STREAMING`.
3. **Megbízható workflow és tervezés:** `DIST-DELIVERY-IDEMPOTENCY` → `DIST-RESILIENCE-CONTROL` → `DIST-TRANSACTIONS-SAGAS` → `DIST-OBSERVABILITY` → `DIST-SYSTEM-DESIGN`.

Ez függőségi iránytű, nem merev sorrend. Konkrét queue-, cache- vagy skálázási igény előrevehet egy témát, de a megoldást mindig vissza kell kötni a részleges hibákhoz, a konzisztenciához és az üzemeltetési költséghez.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `DIST-FAILURE-MODELS` | Elosztott hibamodellek és részleges meghibásodás | P1 | L3 |
| `DIST-COMMUNICATION-CONTRACTS` | Szolgáltatáskommunikáció és szerződések | P1 | L2 |
| `DIST-TIME-ORDERING-IDENTITY` | Idő, sorrend és elosztott azonosítók | P1 | L2 |
| `DIST-CONSISTENCY-CAP` | Konzisztencia, availability és CAP | P1 | L3 |
| `DIST-REPLICATION` | Replikáció és failover | P1 | L2 |
| `DIST-PARTITIONING` | Particionálás és sharding | P1 | L2 |
| `DIST-CONSENSUS-COORDINATION` | Konszenzus és koordináció | P2 | L2 |
| `DIST-CACHING-CDN` | Cache-ek és CDN-ek | P1 | L3 |
| `DIST-LOAD-BALANCING-SCALING` | Terheléselosztás és horizontális skálázás | P1 | L2 |
| `DIST-MESSAGING-STREAMING` | Üzenetsorok, pub/sub és streamek | P1 | L3 |
| `DIST-DELIVERY-IDEMPOTENCY` | Kézbesítési szemantika és idempotencia | P1 | L3 |
| `DIST-RESILIENCE-CONTROL` | Timeout, retry, circuit breaker és backpressure | P1 | L3 |
| `DIST-TRANSACTIONS-SAGAS` | Elosztott tranzakciók, outbox és saga | P1 | L3 |
| `DIST-OBSERVABILITY` | Elosztott observability és hibakeresés | P1 | L2 |
| `DIST-SYSTEM-DESIGN` | Rendszertervezés, kapacitás és trade-offok | P1 | L3 |

## Altémák részletei

### `DIST-FAILURE-MODELS` — Elosztott hibamodellek és részleges meghibásodás

- **Tartalom:** részleges hiba, crash, omission, timeout, hálózati szakadás, késleltetés, duplikáció, újrarendezés, bizonytalan kimenetel, failure detector és fault domain.
- **Előfeltételek:** process-, hálózat- és timeout-alapok a 4. pillérben.
- **Kapcsolatok:** `DIST-RESILIENCE-CONTROL`, `DIST-CONSISTENCY-CAP`, reliability a 7. pillérben.
- **Célbizonyíték:** többkomponensű hibaszcenárió reprodukálása, megfigyelhető tünet és tényleges állapot elkülönítése, valamint a „nem kaptam választ” bizonytalanságának pontos magyarázata.

### `DIST-COMMUNICATION-CONTRACTS` — Szolgáltatáskommunikáció és szerződések

- **Tartalom:** synchronous request/response, asynchronous message, RPC kontra resource API, schema és compatibility, service discovery, API gateway, chatty communication és coupling.
- **Előfeltételek:** HTTP/API-szerződések a 2. pillérben, architekturális határok az 5. pillérben.
- **Kapcsolatok:** `DIST-MESSAGING-STREAMING`, `DIST-RESILIENCE-CONTROL`, API-evolúció.
- **Célbizonyíték:** kommunikációs mód kiválasztása latency-, coupling- és hibatűrési igények alapján, verziózható contract készítése és inkompatibilitási hiba diagnózisa.

### `DIST-TIME-ORDERING-IDENTITY` — Idő, sorrend és elosztott azonosítók

- **Tartalom:** wall clock és monotonic clock, clock skew, timestamp korlátai, happens-before, logical clock alapok, event ordering, sequence és globálisan egyedi azonosítók trade-offjai.
- **Előfeltételek:** konkurencia és hálózati késleltetés alapjai.
- **Kapcsolatok:** `DIST-MESSAGING-STREAMING`, `DIST-CONSISTENCY-CAP`, audit és tracing.
- **Célbizonyíték:** óracsúszásból vagy eltérő sorrendből eredő hiba bemutatása, megfelelő ordering/identity stratégia választása és annak korlátainak magyarázata.

### `DIST-CONSISTENCY-CAP` — Konzisztencia, availability és CAP

- **Tartalom:** strong és eventual consistency, read-your-writes és monotonic read, stale data, linearizability alapmodell, availability, network partition és a CAP állítás pontos határa.
- **Előfeltételek:** `DIST-FAILURE-MODELS`, tranzakciók a 3. pillérben.
- **Kapcsolatok:** `DIST-REPLICATION`, `DIST-PARTITIONING`, distributed transaction.
- **Célbizonyíték:** use case-enként konzisztenciakövetelmény meghatározása, hálózati partíció alatti viselkedés elemzése és megfelelő trade-off indoklása túlzott CAP-szlogenek nélkül.

### `DIST-REPLICATION` — Replikáció és failover

- **Tartalom:** leader/follower, multi-leader és leaderless alapmodell, synchronous/asynchronous replication, replication lag, quorum, read routing, failover, split brain és adatvesztési ablak.
- **Előfeltételek:** `DIST-CONSISTENCY-CAP`, adatbázis-alapok a 3. pillérben.
- **Kapcsolatok:** `DIST-CONSENSUS-COORDINATION`, geo-distribution és disaster recovery.
- **Célbizonyíték:** replika késésének és failoverének megfigyelése, olvasási konzisztencia vagy adatvesztési kockázat diagnózisa és topológiaválasztás indoklása.

### `DIST-PARTITIONING` — Particionálás és sharding

- **Tartalom:** horizontal partitioning, shard key, hash/range/directory alapú kiosztás, consistent hashing alapjai, rebalancing, hot partition, scatter-gather és cross-shard művelet.
- **Előfeltételek:** `DIST-CONSISTENCY-CAP`, adatmodellezés és indexelés a 3. pillérben.
- **Kapcsolatok:** `DIST-REPLICATION`, skálázás és system design.
- **Célbizonyíték:** workloadhoz illeszkedő partition key választása, hot key vagy cross-shard query következményének mérése és rebalancing trade-off magyarázata.

### `DIST-CONSENSUS-COORDINATION` — Konszenzus és koordináció

- **Tartalom:** leader election, quorum, term/epoch, consensus-probléma, Raft-szerű alapmodell, distributed lock/lease, fencing token, membership és split-brain elkerülése.
- **Előfeltételek:** `DIST-FAILURE-MODELS`, `DIST-TIME-ORDERING-IDENTITY`, `DIST-REPLICATION`.
- **Kapcsolatok:** orchestratorok a 7. pillérben, distributed transaction.
- **Célbizonyíték:** leader- vagy lease-alapú koordináció hibamódjának elemzése, stale owner elleni fencing magyarázata és annak felismerése, mikor kell kész koordinációs szolgáltatást használni.

### `DIST-CACHING-CDN` — Cache-ek és CDN-ek

- **Tartalom:** local/distributed cache, CDN, cache key, TTL, invalidation, cache-aside, read/write-through, write-behind, stale-while-revalidate, eviction, stampede és cache poisoning határa.
- **Előfeltételek:** HTTP cache-szemantika a 2. pillérben, `DIST-CONSISTENCY-CAP`.
- **Kapcsolatok:** teljesítmény és skálázás, security a 6. pillérben.
- **Célbizonyíték:** mérés alapján indokolt cache bevezetése, konzisztencia- és invalidációs stratégia tesztelése, valamint stale data vagy stampede diagnózisa.

### `DIST-LOAD-BALANCING-SCALING` — Terheléselosztás és horizontális skálázás

- **Tartalom:** vertical/horizontal scaling, stateless/stateful service, load balancer algoritmus, health-based routing, sticky session, connection draining, autoscaling signal és bottleneck-áthelyeződés.
- **Előfeltételek:** reverse proxy és monitoring a 7. pillérben.
- **Kapcsolatok:** `DIST-PARTITIONING`, `DIST-CACHING-CDN`, capacity planning.
- **Célbizonyíték:** több replika mögötti szolgáltatás terhelési és hibatűrési próbája, state vagy health routing probléma diagnózisa és skálázási korlátok magyarázata.

### `DIST-MESSAGING-STREAMING` — Üzenetsorok, pub/sub és streamek

- **Tartalom:** queue, topic, pub/sub, log/stream, producer, consumer, consumer group, partition, offset, ordering, retention, dead-letter queue és broker szerepe.
- **Előfeltételek:** `DIST-COMMUNICATION-CONTRACTS`, `DIST-PARTITIONING` alapjai.
- **Kapcsolatok:** background processing a 2. pillérben, `DIST-DELIVERY-IDEMPOTENCY`, event-driven architektúra.
- **Célbizonyíték:** queue- vagy streamalapú workflow megvalósítása, ordering/lag/DLQ viselkedés ellenőrzése és broker választási trade-off magyarázata.

### `DIST-DELIVERY-IDEMPOTENCY` — Kézbesítési szemantika és idempotencia

- **Tartalom:** at-most-once, at-least-once és effektív exactly-once viselkedés, duplicate, acknowledgement, redelivery, idempotency key, deduplication, inbox és replay.
- **Előfeltételek:** `DIST-MESSAGING-STREAMING`, tranzakciók a 3. pillérben.
- **Kapcsolatok:** HTTP-idempotencia a 2. pillérben, `DIST-TRANSACTIONS-SAGAS`.
- **Célbizonyíték:** duplikált kézbesítést túlélő consumer vagy API-művelet, crashpontok tesztelése és a választott kézbesítési garancia pontos magyarázata.

### `DIST-RESILIENCE-CONTROL` — Timeout, retry, circuit breaker és backpressure

- **Tartalom:** timeout budget, retry feltétel, exponential backoff és jitter, retry storm, circuit breaker, bulkhead, load shedding, rate limit, backpressure és fallback.
- **Előfeltételek:** `DIST-FAILURE-MODELS`, `DIST-COMMUNICATION-CONTRACTS`.
- **Kapcsolatok:** async/cancellation a 2. pillérben, reliability a 7. pillérben.
- **Célbizonyíték:** lassú vagy hibás függőség kontrollált szimulációja, deadline-t tiszteletben tartó resilience policy, valamint retry amplification vagy resource exhaustion diagnózisa.

### `DIST-TRANSACTIONS-SAGAS` — Elosztott tranzakciók, outbox és saga

- **Tartalom:** local kontra distributed transaction, two-phase commit alapmodell, dual write probléma, transactional outbox/inbox, saga, orchestration/choreography, compensation és semantic rollback.
- **Előfeltételek:** tranzakciók a 3. pillérben, `DIST-DELIVERY-IDEMPOTENCY`.
- **Kapcsolatok:** `DIST-CONSISTENCY-CAP`, messaging és domainhatárok az 5. pillérben.
- **Célbizonyíték:** több komponensű workflow konzisztenciahatárának megtervezése, dual write hiba reprodukálása és outbox vagy saga alapú megoldás hibapontjainak tesztelése.

### `DIST-OBSERVABILITY` — Elosztott observability és hibakeresés

- **Tartalom:** correlation és trace context propagáció, span relationship, service/dependency map, baggage korlátai, metrics/log/trace összekapcsolása, sampling és magas cardinality.
- **Előfeltételek:** logging, metrics és tracing a 7. pillérben.
- **Kapcsolatok:** `DIST-FAILURE-MODELS`, `DIST-RESILIENCE-CONTROL`.
- **Célbizonyíték:** több szolgáltatáson vagy aszinkron hopon átmenő művelet követése, részleges hiba gyökérokának telemetry-alapú azonosítása és hiányzó context propagation javítása.

### `DIST-SYSTEM-DESIGN` — Rendszertervezés, kapacitás és trade-offok

- **Tartalom:** funkcionális és minőségi követelmény, workload és nagyságrendi becslés, adatút, API, storage, cache, queue, bottleneck, fault domain, költség, evolúciós út és döntési bizonytalanság.
- **Előfeltételek:** a pillér alapmechanizmusai, architekturális döntések az 5. pillérben és production engineering a 7. pillérben.
- **Kapcsolatok:** minden elosztott altéma.
- **Célbizonyíték:** konkrét követelményekből kapacitási becsléssel és hibamódokkal alátámasztott rendszerterv, alternatívák összevetése, kritikus trade-offok és fokozatos evolúciós út világos kommunikációja.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** egyetlen processben futó alkalmazás, ezért csak a kliens–szerver kommunikáció és a bizonytalanul lezárult HTTP-válasz érinti az elosztott hibamodell szélét; elosztott rendszerre vonatkozó önálló bizonyítékot még nem ad.
- **MovieExplorer:** az API és a külön PostgreSQL process hálózaton kommunikál, de ez önmagában nem bizonyít replikációs, messaging-, idempotencia- vagy rendszertervezési jártasságot. A projekt később kontrollált cache-, queue- vagy resilience-kísérlethez adhat környezetet.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
