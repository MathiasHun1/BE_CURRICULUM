# HTTP request lifecycle és ASP.NET Core pipeline

Utolsó linkellenőrzés: 2026-09-16  
Technológiai kontextus: ASP.NET Core / .NET 10, Minimal API

## Miért aktuális?

A Backend Foundations projektben a teljes `HTTP-kérés → Kestrel → middleware pipeline → endpoint → HTTP-válasz` útvonalat vizsgáljuk. Az anyag közvetlenül kapcsolódik a naplózó és short-circuit middleware-ekhez, a saját exception handlerhez, valamint a `/fail`, `/partial-fail` és `/divide` kísérletekhez.

Nem cél minden hivatkozást egyszerre végigolvasni. Az **Elsőként olvasandó** rész adja a jelenlegi, rövid tanulási útvonalat; a többi forrás szükség szerint használható.

## Elsőként olvasandó

### 1. ASP.NET Core alapmodell és middleware

1. [Understand ASP.NET Core fundamentals — Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/aspnet-core-fundamentals/)
   - Elsősorban a *How ASP.NET Core works* és a *Customize ASP.NET Core behavior with middleware* egységek érdekesek.
   - Cél: lásd egyben a host, a szerver és a request pipeline szerepét.
2. [ASP.NET Core middleware — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-10.0)
   - Figyeld meg a middleware-sorrendet, az `await next(...)` előtti és utáni vezérlést, valamint a short-circuit működését.
   - Kapcsolódó kísérlet: request/response naplózás és `GET /short-circuit`.

### 2. `HttpContext`, request és response

1. [Request and Response operations in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/request-response?view=aspnetcore-10.0)
   - Cél: értsd a request és response body streamelését, a `WriteAsync` és a flush szerepét.
2. [Use HttpContext in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/use-http-context?view=aspnetcore-10.0)
   - Első körben a request/response elérésére és a response indítására koncentrálj.
3. [`HttpResponse.HasStarted` — .NET API reference](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpresponse.hasstarted?view=aspnetcore-10.0)
   - Cél: pontosan különítsd el a még módosítható szerveroldali response-állapotot a már elküldött headerektől.
   - Kapcsolódó kísérlet: `GET /partial-fail`.

### 3. Kivételkezelés és hibaválaszok

1. [Handle errors in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-10.0)
   - Elsőként az *Exception handler*, *Response headers* és *Server exception handling* részeket olvasd.
   - Cél: értsd, mikor készíthető kontrollált `500` válasz, és mikor marad csak a kapcsolat megszakítása.
2. [Handle errors in ASP.NET Core APIs — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling-api?view=aspnetcore-10.0)
   - Cél: lásd a production API-k beépített exception handling és Problem Details lehetőségeit.
   - Kapcsolódó kísérlet: `GET /fail` és a saját exception-handling middleware.

### 4. Minimal API paraméterkötés

1. [Parameter binding in Minimal API applications — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/parameter-binding?view=aspnetcore-10.0)
   - A query string binding, a binding source kiválasztása és a *Binding failures* táblázat a jelenlegi fontos rész.
   - Cél: különítsd el a hibás kliensbemenetből származó `400` választ a kezeletlen szerverhibából származó `500` választól.
   - Kapcsolódó kísérlet: `GET /divide?dividend=...&divisor=...`.

### 5. `Task`, `async` és `await`

1. [Asynchronous programming with async and await — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/)
   - Cél: a `Task` folyamatban lévő műveletként való értelmezése és a kivétel továbbterjedésének megértése.
2. [CS4014 compiler warning — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs4014)
   - Cél: értsd, miért kellett `await`elni a `/partial-fail` endpoint `WriteAsync` hívását.

## Gyakorlati leképezés

| Projektkísérlet | Elsődleges fogalom | Ajánlott forrásrész |
|---|---|---|
| Request/response naplózó middleware | Pipeline oda-vissza haladása | ASP.NET Core middleware |
| `/short-circuit` | A `next` kihagyása és terminal behavior | ASP.NET Core middleware |
| `/fail` | Kivételterjedés és kontrollált `500` | Error handling |
| `/partial-fail` | `HasStarted`, flush és megszakadt response | Request/Response operations; Error handling |
| `/divide` | Query binding, várható `400` és kezeletlen `500` | Minimal API parameter binding |

## Opcionális protokollszintű mélyítés

1. [RFC 9112, 7.1 — Chunked Transfer Coding](https://www.rfc-editor.org/rfc/rfc9112.html#section-7.1)
   - Ne olvasd végig az egész RFC-t. Most azt figyeld meg, hogyan jelzi a nulla méretű utolsó chunk a HTTP/1.1 üzenet szabályos végét.
2. [curl manual — exit code 18](https://curl.se/docs/manpage.html#18)
   - A 18-as curl hibakód részleges vagy hiányos átvitelre utal; ez nem új HTTP-státuszkód.
3. [Web server implementations in ASP.NET Core — Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/?view=aspnetcore-10.0)
   - Cél: helyezd el Kestrelt a hálózati kapcsolat és az ASP.NET Core pipeline között.

## Olvasás közbeni ellenőrző kérdések

- Hol fordul meg a vezérlés az egymásba ágyazott middleware-ek között?
- Mit jelent pontosan a `Response.HasStarted`, és mit nem jelent?
- Mi a különbség egy várható `400` válasz és egy kezeletlen kivételből származó `500` között?
- Miért nem lehet lecserélni a státuszt, miután a response headerei eljutottak a klienshez?
- Mit vár meg az `await`, és hogyan válik újra megfigyelhetővé rajta keresztül egy aszinkron művelet kivétele?
