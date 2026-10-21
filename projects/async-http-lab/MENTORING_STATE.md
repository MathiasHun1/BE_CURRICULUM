# AsyncHttpLab — projektállapot

Létrehozás: 2026-10-09
Utolsó frissítés: 2026-10-12 — explicit `update` alapján.

Állapot: lezárt alapozó labor; M1–M4 `Complete`, az exit gate teljesült.

## Szerep és hatókör

- Külön, véges backend labor a DI/configuration alapozás után.
- Elfogadott hely: `projects/async-http-lab/`; target framework: `net10.0`.
- Elsődleges téma: `WEB-ASYNC-CANCELLATION`; keresztkapcsolat: `DOTNET-ASYNC-TASKS`.
- A tanuló 2026-10-09-én elfogadta a külön projektet és az alábbi négylépéses tervet.
- Mentor által előkészített környezet: két alkalmazás, lassú helyi tesztfüggőség, HTTP-kliensregisztráció és feladatleírás. A tanuló megírta a `/fetch`, `/fetch-sequential` és `/fetch-concurrent` endpointokat; a javítások részben mentorútmutatással készültek.
- A gyakorlati indítás és az M1 követelményei a [README.md](README.md) fájlban.
- M1 előkészítő anyag: [HttpClient — az első kimenő HTTP-hívás](../../learning-materials/web-backend/httpclient-basics.md). A felhasználó a kliens működését szeretné előbb áttekinteni; a mérföldkőterv változatlan.

## Mérföldkőterv

| ID | Eredmény | Ellenőrizhető teljesítési bizonyíték | Állapot |
|---|---|---|---|
| M1 — Aszinkron várakozás | A LabApi valódi HTTP-hívással lekéri a SlowService válaszát. | Az akkori forrásban await előtti/utáni időpont és külön kiolvasott szálazonosító; sikeres build. A tanuló Postman-sikert jelzett, kérésére elfogadva teljes naplók bemásolása nélkül. Helyesen magyarázta, hogy nincs végig lefoglalt szál, és az azonos szálazonosító sem bizonyít blokkolást. | Complete |
| M2 — Több művelet és blokkolás | Egymás utáni hívások és Task.WhenAll összehasonlítása; blokkoló várakozás felismerése. | Mindkét endpoint forrása és buildje ellenőrzött; a tanuló 6 és 3 másodpercet mért. Helyesen azonosította az adatfüggőség korlátját és azt, hogy a blokkolt szál alacsony CPU-használat mellett is foglalt. | Complete |
| M3 — Cancellation | A bejövő kérés megszakítása eljut a külső HTTP-híváshoz. | Token továbbadása mindhárom endpointban forrással ellenőrzött. A tanuló látta a SlowService cancellation-naplóját, és később a timeout mellett is sikeresnek jelezte a korai megszakítást. Saját helyes válasz: token nélkül a Task.Delay végigfutna. | Complete |
| M4 — Timeout és hibák | Túl lassú vagy hibás függőség kontrollált kezelése. | A kód és a futási próbák elkészültek, elfogadva: mentor által mért 504 kb. 1,03 s alatt és külső 404 → saját 502; a 100 ms-os sikeres ág és a többi próba a tanuló explicit visszajelzése alapján. A 2026-10-12-i saját záró magyarázatban helyesen azonosította a sikeres await utáni stringet, a szál felszabadulását, az állapotgép szerepét, a kliensmegszakításnál hamis filtert és a külső 404 → saját 502 utat. A timeout és a kivétel típusa vezetett pontosítást igényelt; ezután helyesen adta meg a filter két esetét. | Complete |

## Exit gate

M1–M4 teljesült tényleges kóddal és a fenti futási bizonyítékokkal. A tanuló saját szavaival összekapcsolja a Task, await, állapotgép, szál és megszakítás szerepét. A labor lezárható; ez nem jelent automatikusan L2/L3 vagy Confident tudásszintet.

A state machine belső implementációja, ThreadPool-hangolás, terhelésmérés, retry/circuit breaker és streaming későbbi mélyítés; nem része az exit gate-nek.

**Exit gate helyzete, 2026-10-12:** teljesült. A tanuló saját szavaival összekapcsolta a Task/string eredményt, az await alatti felfüggesztést, a szál felszabadulását és az állapotgépben megőrzött folytatási adatokat. Helyesen mondta el, hogy siker esetén Results.Text fut; kliensmegszakításnál nem fut, a hamis catch-filter után a következő catch vizsgálata következik; külső 404-ből HttpRequestException és saját 502 lesz. A kliensmegszakítás automatikus 500-nak tekintése, valamint a timeout és HttpRequestException összekeverése mentor által korrigált; a filter első részének kliensmegszakításnál hamis, külső timeoutnál igaz értékét utána helyesen felidézte. A pontos operációsrendszer-szintű I/O-jelzés nem része az exit gate-nek. Új futási próba nem kellett. A labor lezárt, a mélység L1 / Practiced marad.

## Belépő diagnózis és korlátai

- A belépő kérdésre adott helyes válasz: az `async` nem hoz létre új szálat.
- A szál felszabadulása és az await utáni folytatás kezdetben bizonytalan; mentor magyarázta el, JavaScript-párhuzammal.
- Magyarázat után helyes válasz: a kérés másik szálon is folytatható; a későbbi végrehajtáshoz szükséges adatok megőrzendők, az állapot memóriában tárolható folyamatosan futó szál nélkül.
- Az állapotgép fogalmát a mentor vezette be. Nincs még önálló async HTTP implementáció vagy késleltetett felidézési bizonyíték; tudásszint-emelés nem történt.

## A 2026-10-11-i tanulási helyzet és bizonyítékok

- HttpClient-alapozás chatben feldolgozva. A tanuló azonosította a GetAsync válaszobjektumát, a Task<string> szerepét, a CreateClient regisztrációs nevét, és hogy a kliens létrehozása még nem HTTP-kérés. A JSON-szöveg és C# objektum, Content/ReadAsStringAsync, URI-feloldás, GC/Dispose és structured logging vezetett pontosítást igényelt. A forrásanyag teljes elolvasását nem állítjuk.
- A tanuló saját magyarázata szerint a várakozó kérés állapota állapotgépben őrizhető folyamatosan lefoglalt szál nélkül. A fordító által létrehozott állapotgép, ThreadPool és hardveres/szoftveres szál megkülönböztetését a mentor pontosította.
- `/fetch`: factorytól kért kliens `using` deklarációval; 1 s HttpClient.Timeout; `/slow?delayMs=3000` hívás request cancellation tokennel; sikeres ágon Results.Text; szűrt OperationCanceledException → 504 ProblemDetails; HttpRequestException → 502 ProblemDetails, kivételt és státuszt tartalmazó üzenetsablonnal.
- `/fetch-sequential` és `/fetch-concurrent`: két-két 3000 ms-os hívás, közös klienspéldány, token továbbadása, közvetlen JSON-tömb. A concurrent változat a két hívást await előtt indítja, majd Task.WhenAll-lal várja meg.
- Az M1 idején ellenőrzött before/after naplózásból a végső `/fetch` kódban a before és a timeout bejegyzés maradt; a korábbi M1-forrásellenőrzés bizonyítékát ez nem törli. A mostani alapbeállítás szándékosan timeoutot demonstrál.
- Buildellenőrzések a munkamenetben sikeresek, több alkalommal 0 warning/error. Egy korábbi buildnél a futó LabApi fájlzárolása miatt három átmeneti másolási figyelmeztetés volt, végül sikerrel. A legutolsó naplózási módosítás után külön új build nem történt; a végső forrás ellenőrzött.
- Mentor HTTP-próba: hibás filterrel 500, a stack trace-ben TaskCanceledException → TimeoutException. Az InvalidOperationException helyett TimeoutException ellenőrzésre javítva 504, `application/problem+json`, `Slow service timeout`, 1,025757 s.
- Mentor HTTP-próba: a SlowService `/sdfsdf` címe 404; a LabApi ezt 502 ProblemDetails válasszá alakította (`Slow service error`, 0,027595 s).
- Tanulói futási bizonyíték: M1 Postman-siker; M2 6/3 s; M3 látható cancellation-napló; M4 korai cancel működik. A 100 ms-os 200 + szöveg próba és a többi végső ellenőrzés 2026-10-11-én explicit „a többi próba ok, fogadd el” kérés alapján elfogadva. Ezeket nem tüntetjük fel külön mentor által látott logként vagy mérésként.
- Az utolsó mentorpróbán a forrás már a 3000 ms-os címet tartalmazta, de a futó LabApi még gyors 502-t adott, miközben a SlowService health 200 volt. Régi futó verzió lehetősége felmerült, nem lett bizonyítva; a felhasználó ezt követően a többi próba sikerét megerősítette. Nem marad újratesztelési előfeltétel.

## Értékelés és nyitott rés

- WEB-ASYNC-CANCELLATION: L1 / Practiced; DOTNET-ASYNC-TASKS: L1 / Practiced. Vezetett, ellenőrzött gyakorlati alkalmazás történt, önálló L2-diagnózis vagy új kontextusú Confident bizonyíték még nincs.
- Helyes saját magyarázatok: nincs szükség kérésenként lefoglalt szálra; azonos szálazonosító nem bizonyít blokkolást; két hívás lehet 6/3 s; adatfüggőség kizárhatja az egyidejű indítást; blokkolás mellett lehet alacsony CPU; token nélkül a késleltetés továbbfut; a 504-et a LabApi állítja elő.
- Segítséget igényelt: JSON-tömb visszaadása; logger argumentumok és üzenetsablon; catch változó és InnerException/is; a timeout és request cancellation megkülönböztetése. A 2026-10-12-i záró beszélgetésben a hamis when utáni következő catch vizsgálatát, valamint a filter kliensmegszakításkor hamis és timeoutkor igaz első feltételét helyesen felidézte. A kezeletlen cancellation-kivétel és a bontott kliens felé küldhető 500 viszonyát mentor pontosította; önálló L2-diagnózis továbbra sincs.

## Előkészítés ellenőrzése

- 2026-10-09: `dotnet build projects/async-http-lab/AsyncHttpLab.slnx` sikeres, 0 warning és 0 error.
- Mentor által futtatott helyi HTTP smoke próba: mindkét `/health` → 200; a SlowService alapértelmezett `/slow` válasza 3156 ms alatt 200 és `Slow service response`.
- `delayMs=0` → 200; `-1`, `10001`, `abc` → 400. A LabApi `/fetch` placeholder → 501, a tervezett induló állapot szerint.
- A próba után mindkét szerver leállítva. A scaffold működése önmagában nem teljesíti M1-et; a tanulói outbound HTTP-hívás még hiányzik.

## Lezárás és továbblépés

Az AsyncHttpLab további kötelező lépés nélkül lezárt. A következő tanulási irány kijelölése a globális [LEARNING_STATE.md](../../LEARNING_STATE.md) fájlban történik; az önálló hibadiagnózis és a mélyebb I/O-mechanizmusok a curriculum megfelelő pontjain maradnak nyitva.
