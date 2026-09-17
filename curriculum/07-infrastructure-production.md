# 7. pillér — Infrastruktúra és production engineering

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a forráskódtól a futó, megfigyelhető és biztonságosan változtatható production szolgáltatásig vezető életciklust bontja stabil, mérhető kompetenciákra. A cél, hogy a tanuló ne csak alkalmazáskódot írjon, hanem értse az artifact, konfiguráció, deployment, hálózati belépési pont és üzemeltetési visszajelzés kapcsolatát is.

A CPU, process, fájlrendszer és hálózat belső mechanizmusai a 4. pillérhez, a mély security- és identitásmodell a 6. pillérhez, az elosztott koordináció és skálázás pedig a 8. pillérhez tartozik. Itt ezek production környezetben történő összekapcsolása és működtetése áll a középpontban.

## Ajánlott tanulási hullámok

1. **Operációs környezet:** `OPS-LINUX-SHELL` → `OPS-FILES-PERMISSIONS` → `OPS-PROCESS-SERVICE-MANAGEMENT`.
2. **Csomagolás és szállítás:** `OPS-CONTAINER-IMAGES` → `OPS-CONTAINER-RUNTIME-COMPOSE` → `OPS-CONTAINER-NETWORK-STORAGE` → `OPS-ARTIFACT-CONFIG-SECRETS` → `OPS-CI-BUILD-QUALITY` → `OPS-RELEASE-DEPLOYMENT`.
3. **Production működés:** `OPS-REVERSE-PROXY-TLS` → `OPS-STRUCTURED-LOGGING` → `OPS-METRICS-TRACING` → `OPS-HEALTH-MONITORING-ALERTING` → `OPS-CLOUD-ORCHESTRATION` → `OPS-RELIABILITY-INCIDENTS-CAPACITY`.

Ez függőségi iránytű, nem merev sorrend. Egy deployment- vagy productionhiba előrevehet későbbi témát, de a javítást vissza kell kötni a build artifacthoz, a futó processhez, a konfigurációhoz és a megfigyelhető bizonyítékhoz.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `OPS-LINUX-SHELL` | Linux- és shell-alapok | P1 | L2 |
| `OPS-FILES-PERMISSIONS` | Fájlrendszer, felhasználók és jogosultságok | P1 | L2 |
| `OPS-PROCESS-SERVICE-MANAGEMENT` | Processek, szolgáltatások és host lifecycle | P1 | L2 |
| `OPS-CONTAINER-IMAGES` | Konténerimage-ek és reprodukálható build | P1 | L2 |
| `OPS-CONTAINER-RUNTIME-COMPOSE` | Konténerfuttatás és többkomponensű helyi környezet | P1 | L2 |
| `OPS-CONTAINER-NETWORK-STORAGE` | Konténerhálózatok és perzisztens tárolás | P1 | L2 |
| `OPS-ARTIFACT-CONFIG-SECRETS` | Artifactok, konfiguráció és secret injection | P1 | L2 |
| `OPS-CI-BUILD-QUALITY` | CI, buildellenőrzés és artifact-előállítás | P1 | L2 |
| `OPS-RELEASE-DEPLOYMENT` | Release- és deployment-stratégiák | P1 | L3 |
| `OPS-REVERSE-PROXY-TLS` | Reverse proxy, ingress és TLS termination | P1 | L2 |
| `OPS-STRUCTURED-LOGGING` | Strukturált naplózás és korreláció | P1 | L2 |
| `OPS-METRICS-TRACING` | Metrikák és distributed tracing | P1 | L2 |
| `OPS-HEALTH-MONITORING-ALERTING` | Health checkek, monitorozás és riasztások | P1 | L3 |
| `OPS-CLOUD-ORCHESTRATION` | Felhő- és orchestration-alapok | P2 | L2 |
| `OPS-RELIABILITY-INCIDENTS-CAPACITY` | Megbízhatóság, incidensek és kapacitás | P1 | L3 |

## Altémák részletei

### `OPS-LINUX-SHELL` — Linux- és shell-alapok

- **Tartalom:** shell, parancs, argumentum, pipe, redirect, environment variable, exit code, processzlista, hálózati és fájlrendszer-diagnosztikai alapparancsok, scriptelési alapok.
- **Előfeltételek:** operációs rendszerbeli process- és fájlfogalmak a 4. pillérben.
- **Kapcsolatok:** `OPS-FILES-PERMISSIONS`, `OPS-PROCESS-SERVICE-MANAGEMENT`, CI és konténerek.
- **Célbizonyíték:** nem interaktív diagnosztikai parancssor összeállítása, output és exit code helyes kezelése, valamint egy process-, fájl- vagy hálózati hiba parancssori leszűkítése.

### `OPS-FILES-PERMISSIONS` — Fájlrendszer, felhasználók és jogosultságok

- **Tartalom:** path, mount, ownership, user/group, read/write/execute bit, umask, ACL alapok, service account, least privilege és fájl-/directory-permission különbség.
- **Előfeltételek:** `OPS-LINUX-SHELL`; fájlrendszeri mechanizmusok a 4. pillérben.
- **Kapcsolatok:** `OPS-PROCESS-SERVICE-MANAGEMENT`, konténervolume-ok, secretfájlok.
- **Célbizonyíték:** alkalmazásfájlok és futtató felhasználó minimális jogosultságainak kialakítása, permission denied hiba diagnózisa és túl széles jogosultság felismerése.

### `OPS-PROCESS-SERVICE-MANAGEMENT` — Processek, szolgáltatások és host lifecycle

- **Tartalom:** foreground/background process, signal, exit code, environment, service manager, startup, restart policy, graceful shutdown, resource limit és process supervision.
- **Előfeltételek:** process és szál alapmodell a 4. pillérben, `OPS-LINUX-SHELL`.
- **Kapcsolatok:** ASP.NET Core host lifecycle a 2. pillérben, konténer runtime.
- **Célbizonyíték:** .NET szolgáltatás indítása, leállítása és felügyelete, konfigurációjának és logjának vizsgálata, valamint indulási vagy leállási hiba diagnózisa.

### `OPS-CONTAINER-IMAGES` — Konténerimage-ek és reprodukálható build

- **Tartalom:** image, layer, registry, tag és digest, Dockerfile, build context, multi-stage build, base image, cache, image size, non-root futtatás és reproducibility.
- **Előfeltételek:** build artifact az 1. pillérben, process és fájlrendszer a 4. pillérben.
- **Kapcsolatok:** `OPS-CONTAINER-RUNTIME-COMPOSE`, supply chain a 6. pillérben, CI.
- **Célbizonyíték:** kis, reprodukálható és nem rootként futó .NET image készítése, tartalmának/layerjeinek vizsgálata és verziózási vagy cache-probléma diagnózisa.

### `OPS-CONTAINER-RUNTIME-COMPOSE` — Konténerfuttatás és többkomponensű helyi környezet

- **Tartalom:** container lifecycle, isolation alapmodell, runtime, environment, port publish, resource limit, log, restart, Docker Compose service, dependency és lifecycle.
- **Előfeltételek:** `OPS-CONTAINER-IMAGES`, `OPS-PROCESS-SERVICE-MANAGEMENT`.
- **Kapcsolatok:** `OPS-CONTAINER-NETWORK-STORAGE`, többkomponensű alkalmazások a 8. pillérben.
- **Célbizonyíték:** több service-ből álló helyi környezet felépítése és ellenőrzése, container exit/restart vagy hibás konfiguráció diagnózisa és host–container határ magyarázata.

### `OPS-CONTAINER-NETWORK-STORAGE` — Konténerhálózatok és perzisztens tárolás

- **Tartalom:** bridge network, service name és DNS, port publish/expose különbsége, volume, bind mount, ephemeral filesystem, adat-élettartam és host/container path.
- **Előfeltételek:** `OPS-CONTAINER-RUNTIME-COMPOSE`; hálózat és fájlrendszer a 4. pillérben.
- **Kapcsolatok:** adatbázis-tartósság a 3. pillérben, cloud storage és orchestration.
- **Célbizonyíték:** alkalmazás–adatbázis konténerkapcsolat és tartós volume kialakítása, névfeloldási/port- vagy mount-hiba diagnózisa és adatvesztési kockázat magyarázata.

### `OPS-ARTIFACT-CONFIG-SECRETS` — Artifactok, konfiguráció és secret injection

- **Tartalom:** immutable artifact, environment-specific configuration, configuration precedence, environment variable, config file, feature flag, secret reference, rotációs határ és build/deploy szétválasztása.
- **Előfeltételek:** .NET build és konfiguráció az 1–2. pillérben, secretkezelés a 6. pillérben.
- **Kapcsolatok:** CI, release deployment, container image.
- **Célbizonyíték:** ugyanazon artifact több környezetben külső konfigurációval történő futtatása, titkot nem tartalmazó build és konfigurációs precedence/hiány diagnózisa.

### `OPS-CI-BUILD-QUALITY` — CI, buildellenőrzés és artifact-előállítás

- **Tartalom:** pipeline, trigger, clean checkout, dependency restore, build, test, lint/format, security check, cache, artifact publishing, failure feedback és pipeline credential.
- **Előfeltételek:** buildrendszer az 1. pillérben, tesztstratégia az 5. pillérben.
- **Kapcsolatok:** supply chain a 6. pillérben, `OPS-RELEASE-DEPLOYMENT`.
- **Célbizonyíték:** determinisztikus CI pipeline készítése, amely tiszta környezetben buildel és tesztel, verziózott artifactot állít elő, valamint egy pipeline-hibát logok alapján diagnosztizál.

### `OPS-RELEASE-DEPLOYMENT` — Release- és deployment-stratégiák

- **Tartalom:** release kontra deploy, verziózás, environment promotion, rolling/blue-green/canary stratégia, database migration sorrend, backward compatibility, rollback/roll-forward és feature flag.
- **Előfeltételek:** `OPS-CI-BUILD-QUALITY`, `OPS-ARTIFACT-CONFIG-SECRETS`.
- **Kapcsolatok:** API- és sémaevolúció a 2–3. pillérben, architekturális döntések az 5. pillérben.
- **Célbizonyíték:** alkalmazás- és sémaváltozás biztonságos release-terve, kontrollált deployment és visszaállítási próba, valamint downtime- és kompatibilitási kockázatok elemzése.

### `OPS-REVERSE-PROXY-TLS` — Reverse proxy, ingress és TLS termination

- **Tartalom:** reverse proxy/ingress szerep, host/path routing, TLS termination, certificate deployment, forwarding header, client IP, request limit, timeout és buffering.
- **Előfeltételek:** proxy- és TLS-alapok a 4. pillérben, PKI a 6. pillérben.
- **Kapcsolatok:** ASP.NET Core hosting a 2. pillérben, load balancing a 8. pillérben.
- **Célbizonyíték:** HTTPS-en elérhető reverse proxy mögötti alkalmazás konfigurálása, forwarding/TLS/timeout hiba diagnózisa és a bizalmi határ magyarázata.

### `OPS-STRUCTURED-LOGGING` — Strukturált naplózás és korreláció

- **Tartalom:** log level, strukturált esemény, template, context, correlation/request ID, trace ID, centralizált gyűjtés, retention, sampling és érzékeny adatok kizárása.
- **Előfeltételek:** alkalmazáslogolás és request lifecycle a 2. pillérben.
- **Kapcsolatok:** security audit a 6. pillérben, tracing és incident response.
- **Célbizonyíték:** több requestágat kereshető mezőkkel és korrelációval naplózó rendszer, egy hiba logokból történő rekonstruálása és zajos vagy adatot szivárogtató logolás javítása.

### `OPS-METRICS-TRACING` — Metrikák és distributed tracing

- **Tartalom:** counter, gauge, histogram, latency percentilis, throughput, error rate, saturation, span, trace, context propagation, sampling és OpenTelemetry-alapmodell.
- **Előfeltételek:** `OPS-STRUCTURED-LOGGING`; request lifecycle a 2. pillérben.
- **Kapcsolatok:** distributed observability a 8. pillérben, kapacitástervezés.
- **Célbizonyíték:** releváns service-metrikák és trace-ek instrumentálása, lassú vagy hibás request path azonosítása és hibás cardinality/sampling döntés felismerése.

### `OPS-HEALTH-MONITORING-ALERTING` — Health checkek, monitorozás és riasztások

- **Tartalom:** liveness, readiness, startup check, dependency health, synthetic check, dashboard, SLI/SLO alapok, alert threshold, symptom-based alert és alert fatigue.
- **Előfeltételek:** `OPS-STRUCTURED-LOGGING`, `OPS-METRICS-TRACING`.
- **Kapcsolatok:** service orchestration, reliability és incident response.
- **Célbizonyíték:** helyes readiness/liveness viselkedés és felhasználói tünetre épülő monitorozás kialakítása, dependency-hiba és hamis pozitív riasztás diagnózisa.

### `OPS-CLOUD-ORCHESTRATION` — Felhő- és orchestration-alapok

- **Tartalom:** compute, managed service, object/block storage, virtual network, IAM, region/zone, scheduler, desired state, replica, service discovery, autoscaling és managed kontra self-hosted trade-off.
- **Előfeltételek:** konténerek, hálózat, konfiguráció és health checkek.
- **Kapcsolatok:** authorization a 6. pillérben, skálázás és service discovery a 8. pillérben.
- **Célbizonyíték:** kis szolgáltatás és függőségei platformfüggetlen deployment-terve, erőforrás- és identitáshatárok indoklása, valamint orchestrator scheduling/health hiba diagnózisa.

### `OPS-RELIABILITY-INCIDENTS-CAPACITY` — Megbízhatóság, incidensek és kapacitás

- **Tartalom:** availability, reliability, error budget, capacity, load test, bottleneck, saturation, runbook, on-call alapok, triage, mitigation, recovery, postmortem és tartós javítás.
- **Előfeltételek:** monitorozás, deployment és rendszerszintű diagnosztika.
- **Kapcsolatok:** resilience és system design a 8. pillérben, security incident response a 6. pillérben.
- **Célbizonyíték:** terhelés- vagy hibaszcenárió mérése, kapacitási határ és felhasználói hatás azonosítása, runbook szerinti helyreállítás és hibáztatásmentes, akciókat eredményező postmortem.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** process-, port- és alkalmazáslog-vizsgálatot, kézi health endpointot és regressziós hibadiagnózist ad. Ezek kezdeti production-közeli elemek, de még nincs deployment, központi observability vagy monitorozás.
- **MovieExplorer:** helyi Docker Compose-zal futó PostgreSQLt, névvel ellátott volume-ot, publisholt portot, környezeti változókat és .NET User Secrets konfigurációt használ. CI/CD, alkalmazásimage és production deployment még nem készült.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
