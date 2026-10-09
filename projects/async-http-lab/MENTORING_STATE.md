# AsyncHttpLab — projektállapot

Létrehozás: 2026-10-09

## Szerep és hatókör

- Külön, véges backend labor a DI/configuration alapozás után.
- Elfogadott hely: `projects/async-http-lab/`; target framework: `net10.0`.
- Elsődleges téma: `WEB-ASYNC-CANCELLATION`; keresztkapcsolat: `DOTNET-ASYNC-TASKS`.
- A tanuló 2026-10-09-én elfogadta a külön projektet és az alábbi négylépéses tervet.
- Mentor által előkészített környezet: két alkalmazás, lassú helyi tesztfüggőség, HTTP-kliensregisztráció és feladatleírás. A `/fetch` implementációja tanulói munka.
- A gyakorlati indítás és az M1 követelményei a [README.md](README.md) fájlban.

## Mérföldkőterv

| ID | Eredmény | Ellenőrizhető teljesítési bizonyíték | Állapot |
|---|---|---|---|
| M1 — Aszinkron várakozás | A LabApi valódi HTTP-hívással lekéri a SlowService válaszát. | Tényleges kód; kb. 3 másodperces sikeres HTTP-válasz; időpontok és szálazonosítók az await előtt/után; saját értelmezés az azonos vagy eltérő szálazonosító korlátairól. | Active |
| M2 — Több művelet és blokkolás | Egymás utáni hívások és Task.WhenAll összehasonlítása; blokkoló várakozás felismerése. | Mindkét változat futásideje és kódja; saját magyarázat a különbségről; sync-over-async felismerése egy konkrét példán. | Planned |
| M3 — Cancellation | A bejövő kérés megszakítása eljut a külső HTTP-híváshoz. | Token továbbadása a tényleges kódban; megszakított klienskérés és a lassú szolgáltatás cancellation-naplója; saját magyarázat a kooperatív megszakításról. | Planned |
| M4 — Timeout és hibák | Túl lassú vagy hibás függőség kontrollált kezelése. | Futás a határidőnél lassabb és hibás függőséggel; ellenőrzött kimenetek és naplók; timeout, kliensmegszakítás és HTTP-hiba megkülönböztetése. | Planned |

## Exit gate

M1–M4 teljesült tényleges kóddal és a fenti futási bizonyítékokkal. A tanuló saját szavaival összekapcsolja a Task, await, állapotgép, szál és megszakítás szerepét. A labor lezárható; ez nem jelent automatikusan L2/L3 vagy Confident tudásszintet.

A state machine belső implementációja, ThreadPool-hangolás, terhelésmérés, retry/circuit breaker és streaming későbbi mélyítés; nem része az exit gate-nek.

## Belépő diagnózis és korlátai

- Önálló helyes válasz: az `async` nem hoz létre új szálat.
- A szál felszabadulása és az await utáni folytatás kezdetben bizonytalan; mentor magyarázta el, JavaScript-párhuzammal.
- Magyarázat után helyes válasz: a kérés másik szálon is folytatható; a későbbi végrehajtáshoz szükséges adatok megőrzendők, az állapot memóriában tárolható folyamatosan futó szál nélkül.
- Az állapotgép fogalmát a mentor vezette be. Nincs még önálló async HTTP implementáció vagy késleltetett felidézési bizonyíték; tudásszint-emelés nem történt.

## Előkészítés ellenőrzése

- 2026-10-09: `dotnet build projects/async-http-lab/AsyncHttpLab.slnx` sikeres, 0 warning és 0 error.
- Mentor által futtatott helyi HTTP smoke próba: mindkét `/health` → 200; a SlowService alapértelmezett `/slow` válasza 3156 ms alatt 200 és `Slow service response`.
- `delayMs=0` → 200; `-1`, `10001`, `abc` → 400. A LabApi `/fetch` placeholder → 501, a tervezett induló állapot szerint.
- A próba után mindkét szerver leállítva. A scaffold működése önmagában nem teljesíti M1-et; a tanulói outbound HTTP-hívás még hiányzik.

## Egyetlen következő lépés

A tanuló implementálja és lefuttatja a `LabApi/Program.cs` `/fetch` endpointját a README M1 feladata szerint, majd bemutatja a HTTP-választ és a két naplósort.
