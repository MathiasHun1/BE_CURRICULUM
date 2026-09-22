# Backend Foundations — korábbi globális mérföldkő

Archiválva: 2026-09-22. A projektspecifikus részletek a `projects/backend-foundations/MENTORING_STATE.md` fájlban maradnak.

## Legutóbbi rendszerszintű mérföldkő

- A Backend Foundations projekt `net10.0` targettel létrejött, buildelt és Kestrellel elindult.
- A futó process és az `5003` port kapcsolata ténylegesen ellenőrizve lett.
- A `/`, `/health` és egy nem létező útvonal `200`, JSON és `404` válaszai HTTP-klienssel ellenőrizve lettek.
- A `/greet/{name}` route paraméteres endpoint automatikus paraméterkötése és JSON-válasza ellenőrizve lett.
- A tanuló `POST /health` kéréssel `405 Method Not Allowed` választ idézett elő, megfigyelte az `Allow: GET` headert, és saját szavaival megkülönböztette a `404`-től.
- A tanuló inline middleware-t készített; builddel és futási próbával ellenőrizve lett a `next` előtti requestnapló és az utána futó `200`/`404` response-napló.
- Egy második middleware a `/short-circuit` ágat `next` nélkül, explicit `418` státusszal és JSON bodyval lezárta; a `/health` továbbra is `200` választ adott. A tanuló helyesen magyarázta a short-circuit vezérlési folyamatát.
- A `/fail` endpoint szándékos kivétele `500` választ eredményezett, a host életben maradt, és a stack trace igazolta a kivétel visszaterjedését az `await next(context)` hívásokon át a fejlesztői exception middleware-ig.
- A saját exception-handling middleware a `/fail` kivételét naplózta, majd ellenőrzötten `500 application/json` választ adott általános hibaüzenettel; a `/health` `200` és a `/short-circuit` `418` regressziós ág változatlan maradt.
- A tanuló elkészítette az aszinkron `/partial-fail` endpointot helyesen `await`elt body-írással és explicit flusholással. A kliens `200` státuszt és részleges body-t kapott, majd `curl: (18)` hibával észlelte a szabálytalanul lezárult chunked átvitelt; a `/fail` JSON `500` és a `/health` `200` regressziós próbája sikeres maradt.
- A `/divide` endpoint `200` eredménye, explicit nulla osztós `400` válasza, valamint a hiányzó és nem egész query paraméter automatikus `400` válasza ellenőrizve lett. A `/health` `200` és a `/fail` kontrollált JSON `500` regresszió sikeres maradt; az aktív felidézés igazolta a kliens- és szerverhibák alapvető megkülönböztetését.
- A `/rectangle/{width}` endpoint részletes recept nélkül elkészült. A `200` JSON, explicit és automatikus `400`, `/health` `200` és `/fail` JSON `500` ágak ellenőrizve lettek; a `{width:int}` constraint okozta kezdeti `404` diagnózisa és javítása is megtörtént.
- A lifecycle-felidézés igazolta a route/query forrás, binding kontra handler-validáció, `Results.Ok` kontra `System.Text.Json`, valamint az `await next(context)` utáni visszatérés alapmodelljét. A Backend Foundations `M6` és `EXIT` gate-je teljesült; a projekt lezárt.
