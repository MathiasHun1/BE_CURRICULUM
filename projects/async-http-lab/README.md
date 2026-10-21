# AsyncHttpLab

Kis .NET 10 labor: aszinkron HTTP I/O, szálhasználat, több művelet, cancellation és timeout.
A jóváhagyott mérföldkövek és az exit gate a [MENTORING_STATE.md](MENTORING_STATE.md) fájlban vannak.

Állapot: 2026-10-12 — M1–M4 Complete, az exit gate teljesült; a labor lezárt. A bizonyítékok és a nyitott mélyítések a projektállapotban szerepelnek.

## Felépítés

```text
curl / HTTP kliens → LabApi (:5191) → SlowService (:5192)
                     tanulói kód     mentor által adott tesztfüggőség
```

- `LabApi`: Minimal API, `slow` nevű HTTP-kliensregisztrációval és három tanulói endpointtal: `/fetch`, `/fetch-sequential`, `/fetch-concurrent`.
- `SlowService`: a `/slow` alapból 3000 ms után `200` és `Slow service response` szöveges választ ad. A `delayMs` query paraméter 0–10000 között állítható, hibás értéke `400`.
- A `SlowService` a késleltetést időzítővel szimulálja (`Task.Delay`); a két alkalmazás között valódi HTTP I/O történik. A `Task.Delay` önmagában nem hálózati I/O.
- Mindkét alkalmazás `/health` endpointja egyszerű helyi indításellenőrzés.

## Indítás

A repository gyökeréből két külön terminálban:

```powershell
dotnet run --project projects/async-http-lab/SlowService --launch-profile http
```

```powershell
dotnet run --project projects/async-http-lab/LabApi --launch-profile http
```

Egy harmadik terminálban:

```powershell
curl.exe -i "http://localhost:5192/slow?delayMs=3000"
curl.exe -i http://localhost:5191/health
```

Mindkét szerver a saját termináljában `Ctrl+C`-vel állítható le.

## Végső működés

| Endpoint | A mostani forrás viselkedése |
|---|---|
| `/fetch` | 3000 ms-os függőséget hív 1 s timeouttal, ezért normál híváskor szándékosan kb. 1 s után 504 ProblemDetails a várt eredmény. HttpRequestException esetén 502-t ad; a bejövő cancellation tokent továbbadja. |
| `/fetch-sequential` | Két 3000 ms-os hívás egymás után, továbbadott tokennel; két stringből álló JSON-tömb, a tanuló által mért kb. 6 s alatt. |
| `/fetch-concurrent` | Két hívás indítása await előtt, majd Task.WhenAll, továbbadott tokennel; JSON-tömb, a tanuló által mért kb. 3 s alatt. |

A gyors, 100 ms-os sikeres ág, a hibás külső útvonal, a timeout és a korai kliensmegszakítás próbái elkészültek. A közvetlen mentor- és tanulói visszajelzésen alapuló bizonyítékokat a projektállapot külön jelöli. Újabb tesztkör nem előfeltétel.

Az M4 záró magyarázata és a projekt exit gate-je 2026-10-12-én teljesült. A következő tanulási szelet kijelölése a [globális tanulási állapotban](../../LEARNING_STATE.md) követhető.

## Korábbi M1 feladat — teljesítve

Az alábbi leírás az eredeti kísérletet őrzi. A végső `/fetch` már az M4 timeoutját demonstrálja, így a mostani alapbeállítástól nem az eredeti 3 s-os 200 választ várjuk. Az M1-ben ellenőrzött after napló a későbbi átalakításkor kikerült; az akkori bizonyíték a projektállapotban megmarad.

**Cél:** a `LabApi/Program.cs` `/fetch` endpointja aszinkron módon kérje le a lassú szolgáltatás szövegét, és tegye láthatóvá az `await` két oldalának végrehajtását.

Új fogalom: a `HttpClient` küld HTTP-kéréseket. A már regisztrált `IHttpClientFactory` DI-n keresztül kérhető a handler paramétereként; a `CreateClient("slow")` az előkészített alapcímmel ad klienst. A factory a mögöttes handlerek élettartamát is kezeli.

Megvalósítás a meglévő `/fetch` placeholder helyén:

1. Legyen aszinkron a handler, és kérjen `IHttpClientFactory`-t DI-ból.
2. Hozzon létre `slow` nevű klienst a factoryval; a klienspéldányt a handler végén dispose-old, például `using` deklarációval.
3. Közvetlenül a HTTP-hívás előtt naplózza az időpontot, a szálazonosítót és egy `before` jelölést. Eszközök: `app.Logger.LogInformation`, `DateTimeOffset.UtcNow`, `Environment.CurrentManagedThreadId`.
4. A `GetStringAsync` segítségével kérje le a `slow?delayMs=3000` relatív útvonalat, és `await`-tel várja meg az eredményt.
5. Utána újra olvassa ki és naplózza az időpontot és az aktuális szálazonosítót, ezúttal `after` jelöléssel. Ne a korábban eltett szálazonosítót írja ki újra.
6. Adja vissza a kapott szöveget `Results.Text` segítségével.

A fő API-ban ehhez nincs szükség `Task.Delay`, `Task.Run`, `.Result`, `.Wait()` vagy `Thread.Sleep` használatára. A késleltetést a tesztfüggőség adja. A cancellation továbbadása az M3 feladata.

Ellenőrzés a szerver újraindítása után:

```powershell
curl.exe -i -w "\nTotal: %{time_total}s\n" http://localhost:5191/fetch
```

Elfogadási feltételek:

- `200 OK`, `Slow service response` válasz, nagyjából 3 másodperces idővel (helyi overhead lehetséges).
- Látható `before` és `after` naplósor, külön kiolvasott időponttal és szálazonosítóval.
- Nincs blokkoló várakozás a handlerben.
- A tanuló bemutatja a kódot, a HTTP-választ és a két naplósort, majd értelmezi a látottakat.
- Azonos szálazonosító is szabályos eredmény. Ebből önmagában nem következik, hogy a szál végig foglalt volt; eltérő azonosító pedig nem jelenti, hogy az `async` új szálat hozott létre. Ez a kísérlet nem ThreadPool-terhelésmérés.

## Build

```powershell
dotnet build projects/async-http-lab/AsyncHttpLab.slnx
```
