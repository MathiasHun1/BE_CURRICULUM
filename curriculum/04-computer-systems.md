# 4. pillér — Számítógépes rendszerek

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a futó .NET-alkalmazások alatti gépi, operációs rendszerbeli és hálózati mechanizmusokat bontja stabil, mérhető kompetenciákra. A cél nem rendszermérnöki specializáció, hanem olyan mentális modell, amellyel egy backend folyamat, szál, memória-, I/O- vagy hálózati hibája a framework alatt is értelmezhető.

A CLR, GC és `Task` részletes .NET-viselkedése az 1. pillérhez, a HTTP alkalmazásszemantikája a 2. pillérhez, a konténerek és production infrastruktúra pedig a 7. pillérhez tartozik. Itt az ezeket hordozó hardver-, kernel- és hálózati mechanizmusok állnak a középpontban.

## Ajánlott tanulási hullámok

1. **Gép és memória:** `SYSTEMS-DATA-REPRESENTATION` → `SYSTEMS-CPU-EXECUTION` → `SYSTEMS-MEMORY-HIERARCHY` → `SYSTEMS-VIRTUAL-MEMORY`.
2. **Operációs rendszer és I/O:** `SYSTEMS-PROCESS-PORT` → `SYSTEMS-THREADS-SCHEDULING` → `SYSTEMS-KERNEL-SYSCALLS` → `SYSTEMS-FILESYSTEMS-STORAGE` → `SYSTEMS-IO-MODELS` → `SYSTEMS-CONCURRENCY-SYNC`.
3. **Hálózat:** `SYSTEMS-NETWORK-STACK` → `SYSTEMS-TRANSPORT-SOCKETS` → `SYSTEMS-DNS` → `SYSTEMS-TLS` → `SYSTEMS-PROXIES-GATEWAYS`.

Ez függőségi iránytű, nem merev sorrend. Egy webes vagy production hiba előrevehet hálózati témát, de a tapasztalatot később vissza kell kötni a process-, I/O- és protokollalapokhoz.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `SYSTEMS-DATA-REPRESENTATION` | Bináris adatreprezentáció és kódolás | P1 | L2 |
| `SYSTEMS-CPU-EXECUTION` | CPU, utasítás-végrehajtás és gépi kód | P1 | L2 |
| `SYSTEMS-MEMORY-HIERARCHY` | Cache-ek, RAM és lokalitás | P1 | L2 |
| `SYSTEMS-VIRTUAL-MEMORY` | Virtuális memória és process address space | P1 | L2 |
| `SYSTEMS-PROCESS-PORT` | Process, erőforrások, port és futó szolgáltatás | P1 | L2 |
| `SYSTEMS-THREADS-SCHEDULING` | Szálak, scheduling és ThreadPool-kapcsolat | P1 | L3 |
| `SYSTEMS-KERNEL-SYSCALLS` | Kernel, user mode és system callok | P2 | L2 |
| `SYSTEMS-FILESYSTEMS-STORAGE` | Fájlrendszerek és tartós tárolás | P1 | L2 |
| `SYSTEMS-IO-MODELS` | Blokkoló és aszinkron I/O | P1 | L3 |
| `SYSTEMS-CONCURRENCY-SYNC` | Versenyhelyzetek és szinkronizáció | P1 | L3 |
| `SYSTEMS-NETWORK-STACK` | Hálózati rétegek, IP és routing | P1 | L2 |
| `SYSTEMS-TRANSPORT-SOCKETS` | TCP, UDP, QUIC és socketek | P1 | L3 |
| `SYSTEMS-DNS` | DNS és névfeloldás | P1 | L2 |
| `SYSTEMS-TLS` | TLS és biztonságos csatorna | P1 | L2 |
| `SYSTEMS-PROXIES-GATEWAYS` | Proxyk, gatewayek és terheléselosztási határ | P1 | L2 |

## Altémák részletei

### `SYSTEMS-DATA-REPRESENTATION` — Bináris adatreprezentáció és kódolás

- **Tartalom:** bit, byte, számábrázolás, endianness, karakterkódolás, UTF-8, bináris/text formátum, base64 és sorosítási költség.
- **Előfeltételek:** alapvető programozási ismeretek.
- **Kapcsolatok:** típusrendszer és streamek az 1. pillérben, HTTP body és szerializáció a 2. pillérben.
- **Célbizonyíték:** byteszintű adat vagy encoding-probléma vizsgálata, hibás dekódolás diagnózisa és formátumválasztási trade-off magyarázata.

### `SYSTEMS-CPU-EXECUTION` — CPU, utasítás-végrehajtás és gépi kód

- **Tartalom:** utasítás, regiszter, instruction cycle, privilege level, branch, pipeline alapmodell, gépi kód és a JIT által előállított kód kapcsolata.
- **Előfeltételek:** `SYSTEMS-DATA-REPRESENTATION`.
- **Kapcsolatok:** `DOTNET-BUILD-RUNTIME`, `SYSTEMS-MEMORY-HIERARCHY`.
- **Célbizonyíték:** a forráskódtól a CPU-utasításig tartó lánc saját szavas magyarázata és egy egyszerű végrehajtási vagy CPU-bound viselkedés mérése.

### `SYSTEMS-MEMORY-HIERARCHY` — Cache-ek, RAM és lokalitás

- **Tartalom:** regiszter, CPU-cache szintek, cache line, RAM, lokalitás, memória-latency, sávszélesség és false sharing alapmodell.
- **Előfeltételek:** `SYSTEMS-CPU-EXECUTION`.
- **Kapcsolatok:** `DOTNET-MEMORY-GC`, kollekciók az 1. pillérben, adatbázis-bufferelés a 3. pillérben.
- **Célbizonyíték:** memóriahozzáférési minták összehasonlító mérése és a megfigyelt lokalitási vagy cache-hatás helyes magyarázata.

### `SYSTEMS-VIRTUAL-MEMORY` — Virtuális memória és process address space

- **Tartalom:** virtuális és fizikai cím, page, page table, address space, stack, heap, memory mapping, page fault, working set és swapping alapmodell.
- **Előfeltételek:** `SYSTEMS-MEMORY-HIERARCHY`.
- **Kapcsolatok:** `SYSTEMS-PROCESS-PORT`, `DOTNET-MEMORY-GC`.
- **Célbizonyíték:** egy process memóriatérképének vagy memóriahasználatának vizsgálata, valamint managed heap és operációs rendszerbeli virtuális memória megkülönböztetése.

### `SYSTEMS-PROCESS-PORT` — Process, erőforrások, port és futó szolgáltatás

- **Tartalom:** programfájl és process, PID, address space, handle/file descriptor, környezet, parent/child, exit code, signal, listening endpoint, port és a process–socket tulajdonosi kapcsolat.
- **Előfeltételek:** alapvető operációs rendszer- és hálózati fogalmak.
- **Kapcsolatok:** `DOTNET-BUILD-RUNTIME`, `WEB-REQUEST-LIFECYCLE`, `SYSTEMS-TRANSPORT-SOCKETS`.
- **Célbizonyíték:** futó szolgáltatás processének, PID-jének, erőforrásainak és figyelő portjának azonosítása, valamint indulási vagy portütközési hiba diagnózisa.

### `SYSTEMS-THREADS-SCHEDULING` — Szálak, scheduling és ThreadPool-kapcsolat

- **Tartalom:** process és thread, call stack, context switch, preemption, scheduler, runnable/waiting állapot, prioritás, CPU-bound munka és .NET ThreadPool kapcsolata.
- **Előfeltételek:** `SYSTEMS-PROCESS-PORT`, `SYSTEMS-CPU-EXECUTION`.
- **Kapcsolatok:** `DOTNET-CONCURRENCY-THREADING`, `DOTNET-ASYNC-TASKS`, `SYSTEMS-IO-MODELS`.
- **Célbizonyíték:** szál- és CPU-viselkedés megfigyelése, starvation vagy blokkolás diagnózisa és az async művelet száltól való megkülönböztetése.

### `SYSTEMS-KERNEL-SYSCALLS` — Kernel, user mode és system callok

- **Tartalom:** user/kernel mode, system call, interrupt, exception, process- és I/O-erőforrások kernelkezelése, hozzáférési határ és rendszerhívási költség.
- **Előfeltételek:** `SYSTEMS-CPU-EXECUTION`, `SYSTEMS-PROCESS-PORT`.
- **Kapcsolatok:** `SYSTEMS-FILESYSTEMS-STORAGE`, `SYSTEMS-IO-MODELS`, konténerizáció a 7. pillérben.
- **Célbizonyíték:** egy fájl- vagy hálózati művelet user space–kernel útjának magyarázata és rendszerhívás-szintű diagnosztikai eszköz célzott használata.

### `SYSTEMS-FILESYSTEMS-STORAGE` — Fájlrendszerek és tartós tárolás

- **Tartalom:** fájl, directory, path, metadata, permission, file descriptor/handle, page cache, buffering, flush, fsync, inode/NTFS-alapfogalmak és atomic rename.
- **Előfeltételek:** `SYSTEMS-KERNEL-SYSCALLS`.
- **Kapcsolatok:** `DOTNET-IO-STREAMS`, adatbázis-tárolási motor a 3. pillérben, volume-ok a 7. pillérben.
- **Célbizonyíték:** fájl-életciklus és jogosultság ellenőrzése, buffering/durability kísérlet és egy path-, lock- vagy permission-hiba diagnózisa.

### `SYSTEMS-IO-MODELS` — Blokkoló és aszinkron I/O

- **Tartalom:** blocking/non-blocking I/O, readiness/completion modellek, kernel buffer, backpressure, multiplexing, cancellation, timeout és az operációs rendszer async I/O-jának `Task`-kapcsolata.
- **Előfeltételek:** `SYSTEMS-KERNEL-SYSCALLS`, `SYSTEMS-THREADS-SCHEDULING`.
- **Kapcsolatok:** `DOTNET-ASYNC-TASKS`, `WEB-ASYNC-CANCELLATION`, `SYSTEMS-TRANSPORT-SOCKETS`.
- **Célbizonyíték:** blokkoló és aszinkron I/O erőforrás-hatásának összehasonlítása, megszakítás vagy backpressure diagnózisa és a szálfelszabadulás pontos magyarázata.

### `SYSTEMS-CONCURRENCY-SYNC` — Versenyhelyzetek és szinkronizáció

- **Tartalom:** race condition, atomicitás, memory visibility, critical section, mutex/lock, semaphore, interlocked művelet, deadlock, livelock és lock-free alapmodell.
- **Előfeltételek:** `SYSTEMS-THREADS-SCHEDULING`, `SYSTEMS-MEMORY-HIERARCHY`.
- **Kapcsolatok:** `DOTNET-CONCURRENCY-THREADING`, adatbázis-konkurencia a 3. pillérben.
- **Célbizonyíték:** reprodukálható versenyhelyzet készítése és javítása, megfelelő szinkronizációs primitív választása, valamint deadlock feltételeinek magyarázata.

### `SYSTEMS-NETWORK-STACK` — Hálózati rétegek, IP és routing

- **Tartalom:** link–internet–transport–application rétegek, frame, packet, IP-cím, subnet, route, gateway, NAT, loopback, IPv4/IPv6 és MTU alapmodell.
- **Előfeltételek:** `SYSTEMS-DATA-REPRESENTATION`.
- **Kapcsolatok:** `SYSTEMS-TRANSPORT-SOCKETS`, `SYSTEMS-DNS`, konténerhálózatok a 7. pillérben.
- **Célbizonyíték:** egy helyi és távoli packet útjának végigkövetése, címzési vagy routing hiba diagnózisa és IPv4/IPv6 viselkedés összehasonlítása.

### `SYSTEMS-TRANSPORT-SOCKETS` — TCP, UDP, QUIC és socketek

- **Tartalom:** socket, bind/listen/accept/connect, port, TCP handshake és állapotok, byte stream, flow/congestion control alapjai, UDP datagram és QUIC áttekintése.
- **Előfeltételek:** `SYSTEMS-NETWORK-STACK`, `SYSTEMS-PROCESS-PORT`.
- **Kapcsolatok:** HTTP a 2. pillérben, `SYSTEMS-TLS`, elosztott kommunikáció a 8. pillérben.
- **Célbizonyíték:** kapcsolat és listening socket vizsgálata, connection refused/reset/timeout megkülönböztetése és egy egyszerű TCP-adatút magyarázata.

### `SYSTEMS-DNS` — DNS és névfeloldás

- **Tartalom:** resolver, recursive/authoritative szerver, zone, rekordtípusok, cache, TTL, negatív cache, search suffix és alkalmazásoldali névfeloldás.
- **Előfeltételek:** `SYSTEMS-NETWORK-STACK`.
- **Kapcsolatok:** `SYSTEMS-TLS`, service discovery a 7–8. pillérben.
- **Célbizonyíték:** névfeloldási lánc és rekordok vizsgálata, cache/TTL vagy konfigurációs hiba diagnózisa és DNS-hiba elkülönítése a transporthibától.

### `SYSTEMS-TLS` — TLS és biztonságos csatorna

- **Tartalom:** TLS handshake, tanúsítványlánc, hostname validation, trust store, kulcscsere, session resumption, ALPN és titkosítási határ.
- **Előfeltételek:** `SYSTEMS-TRANSPORT-SOCKETS`, `SYSTEMS-DNS`.
- **Kapcsolatok:** HTTPS a 2. pillérben, PKI és kriptográfia a 6. pillérben, certificate deployment a 7. pillérben.
- **Célbizonyíték:** TLS-kapcsolat és tanúsítványlánc vizsgálata, lejárt/nem megbízható/hostname mismatch hiba megkülönböztetése és a titkosítási végpont azonosítása.

### `SYSTEMS-PROXIES-GATEWAYS` — Proxyk, gatewayek és terheléselosztási határ

- **Tartalom:** forward/reverse proxy, gateway, load balancer, connection termination, forwarding header, health check, timeout és buffering alapmodell.
- **Előfeltételek:** `SYSTEMS-TRANSPORT-SOCKETS`, `SYSTEMS-TLS`; HTTP-alapok a 2. pillérben.
- **Kapcsolatok:** reverse proxy és deployment a 7. pillérben, skálázás a 8. pillérben.
- **Célbizonyíték:** kliens–proxy–alkalmazás útvonal megfigyelése, hibás forwarding vagy timeout diagnózisa és a TLS termination helyének indoklása.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** közvetlen bizonyítékot ad a process, PID, Kestrel, IPv4/IPv6 listening socket és port kapcsolatára. A részleges HTTP-válasz kísérlete érinti az I/O- és transportmodellt, de ezekből még nem következik önálló rendszerszintű jártasság.
- **MovieExplorer:** a streamelt fájlimport és a helyi PostgreSQL-konténer későbbi fájlrendszer-, I/O-, process- és hálózati kísérletekhez adhat gyakorlati környezetet; önmagában nem bizonyítja e témák mély megértését.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
