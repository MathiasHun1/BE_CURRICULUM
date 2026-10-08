# ConfigurationDiLab — lezárási összefoglaló

Utolsó frissítés: 2026-10-07

Állapot: lezárt alapozó labor. M1–M4 `Complete`; nincs aktív projektfeladat. A részletes bizonyítékok és korlátaik a [MENTORING_STATE.md](MENTORING_STATE.md) fájlban.

## Elkészült gyakorlatok

- Kézi constructor injection két díjtáblával: Ducati 1,5 órára 27000 és 36000 HUF.
- DI-ból kapott scoped kalkulátor, IOptionsSnapshot a konstruktorban és a handlerben.
- Konfiguráció bindingja, környezeti felülírás, nem pozitív és üres díjtábla startup validációja.
- Singleton kalkulátor → scoped snapshot hibakísérlet, értelmezés és javítás.
- `/debug/lifetime`: két probe-paraméter, InstanceId és ReferenceEquals. Scoped/transient két-két HTTP-kéréssel igazolt; singleton helyes következtetése felhasználói kérésre elfogadva a teljesítéshez.
- Végső buildet is végző tesztfutás: 9 passed, 0 failed, 0 skipped (2026-10-07, 441 ms).

## Megőrzött kódállapot

- Scoped kalkulátor, mindkét options-fogyasztó snapshotot kap.
- Transient probe; InstanceId csak olvasható auto-property, példányonként egyszer létrehozott GUID-dal.
- Ducati JSON-alapdíj 18000. A diagnosztikai endpoint megmaradt a laborban.

## Továbblépés

Az új fókusz async I/O és request cancellation. A még megválaszolatlan belépő kérdés a [LEARNING_STATE.md](../../LEARNING_STATE.md) fájlban. Új projekt vagy async implementáció még nincs.

A lezárás nem jelent automatikus L2-t: az env-válaszok nem lettek teljes terjedelmükben bemásolva, a végső magyarázat és a 19000 × 1,5 = 28500 eredmény mentor által pontosított. A késleltetett, önálló alkalmazás a curriculum nyitott része.
