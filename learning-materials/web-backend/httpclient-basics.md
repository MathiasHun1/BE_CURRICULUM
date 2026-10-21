# HttpClient — az első kimenő HTTP-hívás

Utolsó linkellenőrzés: 2026-10-09
Technológiai kontextus: .NET 10, ASP.NET Core
Kapcsolódó altémák: `WEB-ASYNC-CANCELLATION`, `DOTNET-ASYNC-TASKS`; előzmény: `WEB-DI-CONFIGURATION`

## Miért aktuális?

Az [AsyncHttpLab M1](../../projects/async-http-lab/README.md) egy másik alkalmazást hív HTTP-n keresztül. A kód megírásához előbb különítsük el a kliens, a kérés, a válasz, a Task és a factory szerepét. Az anyagot kis lépésekben dolgozzuk fel; az olvasás önmagában nem tudásbizonyíték.

## Alapmodell

Az alkalmazás szerverként fogadja a bejövő HTTP-kérést, és kliensként új kérést küldhet egy másik szolgáltatásnak. Ez két külön HTTP-kérés; a külső válasz nem kerül automatikusan a saját hívónkhoz.

| Fogalom | Szerep |
|---|---|
| `HttpClient` | HTTP-kérések küldésére és válaszok fogadására szolgáló objektum. Több kéréshez használható. |
| `HttpRequestMessage` | Egy kérés adatai: például metódus, URI, fejlécek, body. Az egyszerű metódusok ezt elrejtik. |
| `HttpResponseMessage` | Egy válasz státuszkódja, fejlécei és a body elérését biztosító `Content`. |
| `HttpContent` | A HTTP body és a hozzá tartozó tartalomfejlécek reprezentációja. |

Forrás és példák: [Make HTTP requests with HttpClient](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient).

## Ajánlott olvasási sorrend

### 1. Először: egy GET és a válasz feldolgozása

[Make HTTP requests with HttpClient — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient)

Olvasd a **Make an HTTP request**, **Understand HTTP content** és **Use an HTTP GET request** részeket, a JSON-os alfejezetig. A példában a `WriteRequestToConsole` saját segédmetódus, nem beépített API.

**Cél:** tudd, hogy a `GetAsync` eredménye `Task<HttpResponseMessage>`, az await után pedig a válaszobjektumhoz férsz hozzá. A body szövegét külön olvashatod ki. A `GetAsync` önmagában nem dob kivételt pusztán egy 404/500 státusz miatt; a példabeli `EnsureSuccessStatusCode` kéri a sikeresség ellenőrzését.

**Mostani megálló:** magyarázd el a státuszkód és a body különbségét. Ezután térünk át a labor rövidebb API-jára.

### 2. Az M1 egyszerűsítése: GetStringAsync és BaseAddress

[HttpClient.GetStringAsync — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0)

Olvasd a string URI-t fogadó overload **Returns**, **Exceptions** és **Remarks** részeit.

**Cél:** a metódus `Task<string>`-et ad; sikeres await után a body szövegét kapod. A hívás a teljes body beolvasását is megvárja. Nem sikeres, azaz nem 200–299 közötti HTTP-státusznál `HttpRequestException` keletkezik. A szöveg nem feltétlenül JSON, és nem automatikusan egy C# üzleti objektum.

[HttpClient.BaseAddress — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.baseaddress?view=net-10.0)

Olvasd a **Remarks** részt. A `BaseAddress` és a relatív URI URI-feloldással áll össze; ez nem egyszerű szöveg-összefűzés.

**Papírkísérlet:** jósolj végső URI-t, majd később ellenőrizd a tényleges kérés naplójában:

| BaseAddress | Kérés URI-ja |
|---|---|
| `http://localhost:5192/` | `slow?delayMs=3000` |
| `https://example.com/api/` | `items` |
| `https://example.com/api/` | `/items` |

A kezdő `/` a szerver gyökerétől induló útvonalat jelöl. A base cím utolsó `/` utáni része relatív feloldásnál lecserélődhet; ezért az alapcím záró perjele is számít.

### 3. A már előkészített DI-regisztráció: IHttpClientFactory

[Use IHttpClientFactory — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory)

Olvasd a **Basic usage**, **Named clients** és **HttpClient lifetime management** részeket.

**Cél:** az `AddHttpClient` regisztrálja a factoryt és a klienskonfigurációt. A `CreateClient("slow")` létrehoz egy új klienspéldányt az adott nevű konfigurációval. A `slow` egy helyi regisztrációs név; nem HTTP-metódus vagy automatikusan hívott endpoint.

**Megfigyelés a laborban:** keresd meg, melyik konfigurációs kulcsból érkezik a `BaseAddress`. A regisztráció és a `CreateClient` nem küldi el a GET-kérést; azt a kliens megfelelő metódusa indítja.

### 4. A használat után: élettartam és kapcsolatok

[HttpClient guidelines — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines)

Olvasd a **Pooled connections** és **Recommended use** részeket.

**Cél:** különítsd el a `HttpClient` objektum és a mögöttes hálózati kapcsolatok élettartamát. Két bevett megoldás a hosszú életű kliens megfelelő `PooledConnectionLifetime` beállítással, illetve a factorytól kért rövid életű kliensek. A factory a mögöttes handlereket újrahasználhatja; az így kapott kliens dispose-olása nem zárja le azonnal a megosztott kapcsolatokat. Ezért szerepelhet `using` a labor feladatában.

Gyakori hiba minden kéréshez saját `new HttpClient()` és új handler létrehozása, majd azonnali eldobása: felesleges kapcsolatépítést és nagy terhelésnél portkimerülést okozhat. A factory használata ezt az élettartam-kezelést segíti.

## Gyakorlati kísérletek, egymás után

1. A fenti olvasás után valósítsd meg az AsyncHttpLab README M1 feladatát. A 3 másodperces HTTP-válasz és a naplósorok adják az első futási bizonyítékot.
2. Később egy átmeneti, visszaállítható kísérletben kérj nem létező útvonalat. Figyeld meg a `GetStringAsync` kivételét, majd hasonlítsd össze a `GetAsync` válaszobjektumának státuszával. A fő API és a külső szolgáltatás státusza két külön megfigyelés.

## Opcionális mélyítés

- Az első forrás JSON-os és POST példái: amikor objektumot kell küldeni vagy fogadni.
- A factory dokumentáció typed client része: amikor egy külső szolgáltatás hívásait önálló típusba érdemes rendezni.
- A guidelines DNS és `PooledConnectionLifetime` része: amikor a hosszú életű kapcsolatok működését vizsgáljuk.
- Cancellation és timeout a labor M3–M4 lépésében következik. Streaming, retry és saját delegating handlerek későbbi mélyítés.

## Aktív felidézés

1. Mit jelent a `Task<string>`, és mit kapsz a sikeres await után?
2. Miben más a `GetAsync` és a `GetStringAsync` visszatérési értéke és 404-es viselkedése?
3. Mi a különbség az `AddHttpClient`, a `CreateClient` és a `GetStringAsync` szerepe között?
4. Miért fér össze a factorytól kapott kliens dispose-olása a hálózati kapcsolatok újrahasználatával?
