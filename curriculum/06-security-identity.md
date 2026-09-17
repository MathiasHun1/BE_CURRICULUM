# 6. pillér — Biztonság és identitáskezelés

Utolsó szerkezeti felülvizsgálat: 2026-09-17

## Cél és hatókör

Ez a pillér a modern webalkalmazások biztonságos tervezéséhez szükséges alapelveket, identitásmodelleket, protokollokat és alkalmazásbiztonsági gyakorlatokat bontja stabil, mérhető kompetenciákra. A cél nem biztonsági specializáció, hanem a bizalmi határok, támadási felületek és védelmi döntések önálló felismerése Angular–ASP.NET Core környezetben is.

A TLS hálózati működése a 4. pillérhez, az ASP.NET Core security middleware bekötése a 2. pillérhez, a secret deployment és production hardening a 7. pillérhez tartozik. Itt a fenyegetés, a bizalom, az identitás és a kontrollok helyes biztonsági modellje áll a középpontban.

## Ajánlott tanulási útvonal

A security nem a funkciók elkészülte utáni hardening-fázis. Minden üzleti szelet kap egy arányos biztonsági minimumot; a teljes kriptográfiai, identity-protokoll- és incidenskezelési mélység később, a tényleges felelősség és threat model alapján épül hozzá.

1. **Biztonságos alap minden feature-ben:** a `SEC-PRINCIPLES-THREAT-MODELING` vékony threat-model szelete → `SEC-INPUT-DATA-SECURITY` → `SEC-SECRETS-KEY-MANAGEMENT`, mellettük deny-by-default hozzáférési határ, biztonságos konfiguráció/hibakezelés, dependency-ellenőrzés és titkot nem szivárogtató security logging. Ezek nem várnak külön security projektre.
2. **Első védett vertikális szelet:** `SEC-IDENTITY-SESSIONS` → `SEC-AUTHENTICATION` → `SEC-AUTHORIZATION`, a `SEC-WEB-BROWSER-SECURITY` releváns cookie/token, CORS és CSRF határaival. Egy valós erőforrás engedélyezett, anonim és tiltott ágát automatizáltan ellenőrizni kell.
3. **Szabványos identity-integráció:** `SEC-OAUTH2`, `SEC-OPENID-CONNECT` és `SEC-TOKENS-JWT` egymáshoz kapcsolva. Böngészős kliensnél az Authorization Code + PKCE az alap; az implicit flow kerülendő. A szereplőket, tokeneket, redirect/nonce/state/PKCE kontrollokat és az API tokenvalidációját együtt kell érteni.
4. **Felelősségalapú kriptográfiai mélyítés:** `SEC-CRYPTOGRAPHY`, `SEC-PKI-TLS` és `SEC-PASSWORDS-CREDENTIALS`. Saját password store, kulcskezelés vagy alacsonyabb szintű kriptográfiai döntés csak akkor kap implementációs mélységet, ha a rendszer ténylegesen birtokolja ezt a felelősséget; külső identity provider mellett először a helyes integráció és bizalmi határ a cél.
5. **Folyamatos védelem:** `SEC-SUPPLY-CHAIN` és `SEC-AUDIT-INCIDENT-RESPONSE` teljes mélysége, rotáció, visszaélésvédelem és incidensgyakorlat production-közeli környezetben.

Ez végrehajtási iránytű, nem merev kurzusterv. Egy kész framework- vagy Keycloak-konfiguráció csak akkor bizonyít tudást, ha a tanuló vissza tudja kötni a threat modelhez, a protokollszereplőkhöz, a bizalmi határokhoz és a negatív tesztekhez.

## Altémák áttekintése

| Azonosító | Altéma | Prioritás | Célmélység |
|---|---|---:|---:|
| `SEC-PRINCIPLES-THREAT-MODELING` | Biztonsági alapelvek és threat modeling | P1 | L3 |
| `SEC-CRYPTOGRAPHY` | Alkalmazott kriptográfiai alapok | P1 | L2 |
| `SEC-PASSWORDS-CREDENTIALS` | Jelszavak és hitelesítő adatok védelme | P1 | L2 |
| `SEC-PKI-TLS` | PKI, tanúsítványok és TLS-bizalom | P1 | L2 |
| `SEC-SECRETS-KEY-MANAGEMENT` | Secret- és kulcskezelés | P1 | L2 |
| `SEC-IDENTITY-SESSIONS` | Identitás, account lifecycle és munkamenetek | P1 | L2 |
| `SEC-AUTHENTICATION` | Authentication mechanizmusok | P1 | L2 |
| `SEC-AUTHORIZATION` | Authorization és hozzáférés-szabályozás | P1 | L3 |
| `SEC-TOKENS-JWT` | Tokenek, claim-ek és JWT | P1 | L2 |
| `SEC-OAUTH2` | OAuth 2.0 delegált authorization | P1 | L2 |
| `SEC-OPENID-CONNECT` | OpenID Connect és federált bejelentkezés | P1 | L2 |
| `SEC-WEB-BROWSER-SECURITY` | Böngésző- és webalkalmazás-biztonság | P1 | L3 |
| `SEC-INPUT-DATA-SECURITY` | Input-, output- és adat-hozzáférési biztonság | P1 | L3 |
| `SEC-SUPPLY-CHAIN` | Függőségek és software supply chain | P2 | L2 |
| `SEC-AUDIT-INCIDENT-RESPONSE` | Biztonsági naplózás, visszaélésvédelem és incidenskezelés | P2 | L2 |

A `SEC-SUPPLY-CHAIN` és `SEC-AUDIT-INCIDENT-RESPONSE` P2 besorolása a teljes működési mélységre vonatkozik. Az ismert sérülékenységek alapellenőrzése, a biztonságos alapkonfiguráció, a releváns események naplózása és az érzékeny adatok naplózási tilalma keresztmetszeti P1 minimumok.

## Első munkaképes szelet és későbbi mélyítés

| Azonosító | Első munkaképes szelet | Későbbi mélyítés vagy trigger |
|---|---|---|
| `SEC-PRINCIPLES-THREAT-MODELING` | Asset, actor, trust boundary, támadási felület, legfontosabb fenyegetés és arányos kontroll egy kis feature-höz. | Strukturált threat model, risk scoring és defense-in-depth több komponens, érzékeny adat vagy külső szereplő esetén. |
| `SEC-CRYPTOGRAPHY` | Hash/MAC/titkosítás/aláírás céljának megkülönböztetése és szabványos könyvtár választása saját crypto helyett. | AEAD, nonce-, random- és kulcséletciklus részletek saját adatvédelmi vagy protokollfelelősségnél. |
| `SEC-PASSWORDS-CREDENTIALS` | Annak felismerése, hogy jelszóhoz KDF, salt, rate limit, MFA és biztonságos recovery kell; külső IdP esetén a felelősséghatár. | Work factor, credential stuffing és teljes reset/recovery implementáció csak saját credential store esetén. |
| `SEC-PKI-TLS` | Tanúsítványlánc, hostname validation, trust store és TLS termination bizalmi határa. | Revocation, certificate lifecycle és összetett belső PKI saját tanúsítvány- vagy platformfelelősségnél. |
| `SEC-SECRETS-KEY-MANAGEMENT` | Secret kontra konfiguráció, repositoryból kizárás, fejlesztői/környezeti injection és kiszivárgás esetén azonnali rotáció. | Vault, hozzáférési policy, kulcshierarchia, automatikus rotáció és audit több környezet vagy magas értékű secret esetén. |
| `SEC-IDENTITY-SESSIONS` | Subject/account/principal/credential/session elkülönítése, létrehozás–megszüntetés és alapvető timeout/logout modell. | Account linking, provisioning, session fixation és összetett revocation több identity source vagy hosszú session esetén. |
| `SEC-AUTHENTICATION` | Cookie vagy bearer flow helyes bekötése, challenge, `401`, MFA/IdP felelősséghatár és negatív teszt. | Step-up, phishing-resistant faktor, lockout és saját credential flow magasabb kockázatú műveletnél. |
| `SEC-AUTHORIZATION` | Deny by default, least privilege, ownership/policy ellenőrzés és `401`/`403`/IDOR teszt egy valódi erőforráson. | ABAC, resource-based és tenant-határ több szereplő, delegáció vagy összetett jogosultsági modell esetén. |
| `SEC-TOKENS-JWT` | Access kontra ID token, issuer/audience/expiry/signature validáció, claim és token leakage alapmodellje. | Rotation, revocation, sender constraint és opaque/JWT trade-off több resource server vagy magas replay-kockázat esetén. |
| `SEC-OAUTH2` | Szereplők, scope, Authorization Code + PKCE, client credentials, redirect URI és access/refresh token útja. | State/nonce/PKCE, sender constraint és összetett klienshitelesítés több provider vagy magas fenyegetettség esetén. |
| `SEC-OPENID-CONNECT` | Discovery, login, ID/access token különbsége, nonce, claim mapping és Angular/API integráció külső IdP-vel. | UserInfo, logout, account linking és több provider közötti claim/identity probléma federált rendszerben. |
| `SEC-WEB-BROWSER-SECURITY` | Origin/SOP, CORS nem-auth szerepe, cookie flag/SameSite, CSRF, XSS/output encoding és token-tárolási döntés. | CSP, BFF, clickjacking és összetett böngészős session/token trade-off valós Angular-integrációnál. |
| `SEC-INPUT-DATA-SECURITY` | Untrusted input, allowlist, paraméterezett SQL, biztonságos output/error és mass-assignment/adatminimalizálási alapok. | SSRF, path traversal, file upload és unsafe deserialization kontrollált reprodukciója az adott bemeneti határ megjelenésekor. |
| `SEC-SUPPLY-CHAIN` | Direct/transitive dependency áttekintése, advisory ellenőrzés, rögzített verzió és biztonságos update-döntés. | SBOM, provenance, aláírás és build integrity szabályozott vagy többcsapatos release-folyamatnál. |
| `SEC-AUDIT-INCIDENT-RESPONSE` | Security esemény, correlation, érzékeny adat naplózási tilalma, alapvető alert és rövid containment-lépések. | Abuse detection, triage, recovery és post-incident gyakorlat production-közeli esemény vagy megfelelési igény esetén. |

## Altémák részletei

### `SEC-PRINCIPLES-THREAT-MODELING` — Biztonsági alapelvek és threat modeling

- **Tartalom:** confidentiality, integrity, availability, least privilege, defense in depth, secure by default, fail securely, biztonsági konfiguráció és exceptional condition, trust boundary, asset, actor, attack surface, threat és risk.
- **Előfeltételek:** alapvető webes és rendszerismeretek.
- **Kapcsolatok:** minden további security-altéma, architekturális döntések az 5. pillérben.
- **Célbizonyíték:** konkrét rendszer egyszerű threat modelje, eszközök és bizalmi határok azonosítása, fenyegetések priorizálása és arányos kontrollok indoklása.

### `SEC-CRYPTOGRAPHY` — Alkalmazott kriptográfiai alapok

- **Tartalom:** véletlenszerűség, hash, MAC, szimmetrikus és aszimmetrikus titkosítás, digitális aláírás, kulcs és nonce, authenticated encryption, algoritmus- és könyvtárválasztás határa.
- **Előfeltételek:** bináris adatreprezentáció a 4. pillérben.
- **Kapcsolatok:** `SEC-PASSWORDS-CREDENTIALS`, `SEC-PKI-TLS`, `SEC-TOKENS-JWT`.
- **Célbizonyíték:** megfelelő kriptográfiai primitív kiválasztása adott célra, integritás és titkosság megkülönböztetése, valamint veszélyes saját kriptográfiai megoldás felismerése.

### `SEC-PASSWORDS-CREDENTIALS` — Jelszavak és hitelesítő adatok védelme

- **Tartalom:** password hashing, salt, work factor, memóriaigényes KDF, credential stuffing, brute force, rate limiting, MFA, reset/recovery és biztonságos összehasonlítás.
- **Előfeltételek:** `SEC-CRYPTOGRAPHY`.
- **Kapcsolatok:** `SEC-AUTHENTICATION`, `SEC-IDENTITY-SESSIONS`, visszaélésvédelem.
- **Célbizonyíték:** jelszótárolási és resetfolyamat biztonságos megtervezése, hash és titkosítás különbségének magyarázata, valamint gyenge credential-policy hibáinak felismerése.

### `SEC-PKI-TLS` — PKI, tanúsítványok és TLS-bizalom

- **Tartalom:** certificate authority, tanúsítványlánc, public/private key, hostname és identity validation, trust store, revocation, certificate lifecycle és TLS termination bizalmi következményei.
- **Előfeltételek:** a hash, aláírás és public/private key vékony alapmodellje; TLS-mechanika a 4. pillérben. A teljes `SEC-CRYPTOGRAPHY` mélység nem előfeltétele az első trust-chain diagnózisnak.
- **Kapcsolatok:** `SEC-SECRETS-KEY-MANAGEMENT`, HTTPS-integráció a 2. pillérben.
- **Célbizonyíték:** tanúsítványlánc és bizalmi hiba elemzése, megfelelő validáció fenntartása és a TLS termination utáni belső bizalmi határ indoklása.

### `SEC-SECRETS-KEY-MANAGEMENT` — Secret- és kulcskezelés

- **Tartalom:** secret kontra konfiguráció, környezeti és fejlesztői secret store, vault, hozzáférés, rotáció, lejárat, audit, kulcshierarchia és titkok kiszivárgásának kezelése.
- **Előfeltételek:** konfigurációs és hozzáférési határok; kriptográfiai mélység a kulcshierarchia és saját titkosítás esetén szükséges.
- **Kapcsolatok:** konfiguráció a 2. pillérben, deployment és platform secret management a 7. pillérben.
- **Célbizonyíték:** titkot nem tartalmazó repository és környezetenkénti secret injection megvalósítása, rotációs folyamat és kiszivárgás utáni intézkedések magyarázata.

### `SEC-IDENTITY-SESSIONS` — Identitás, account lifecycle és munkamenetek

- **Tartalom:** subject, account, identity, principal, claim, credential, session, account linking, provisioning/deprovisioning, session fixation, idle/absolute timeout és kijelentkezés.
- **Előfeltételek:** `SEC-PRINCIPLES-THREAT-MODELING`.
- **Kapcsolatok:** `SEC-AUTHENTICATION`, `SEC-AUTHORIZATION`, `SEC-OPENID-CONNECT`.
- **Célbizonyíték:** account- és session-lifecycle megtervezése, principal és credential megkülönböztetése, valamint elavult vagy eltérített munkamenet hibamódjának kezelése.

### `SEC-AUTHENTICATION` — Authentication mechanizmusok

- **Tartalom:** knowledge/possession/inherence faktorok, cookie- és bearer-alapú authentication, MFA, phishing resistance, challenge, lockout, step-up authentication és külső identity provider.
- **Előfeltételek:** `SEC-IDENTITY-SESSIONS`; a `SEC-PASSWORDS-CREDENTIALS` csak saját jelszóalapú identity store esetén közvetlen előfeltétel.
- **Kapcsolatok:** `SEC-TOKENS-JWT`, `SEC-OPENID-CONNECT`, webes security middleware a 2. pillérben.
- **Célbizonyíték:** alkalmazási kockázathoz illeszkedő authentication flow megvalósítása, `401` okának diagnózisa és authentication/authorization világos megkülönböztetése.

### `SEC-AUTHORIZATION` — Authorization és hozzáférés-szabályozás

- **Tartalom:** deny by default, least privilege, RBAC, ABAC, policy- és resource-based authorization, ownership, tenant boundary, permission evaluation és privilege escalation.
- **Előfeltételek:** `SEC-IDENTITY-SESSIONS`, `SEC-AUTHENTICATION`.
- **Kapcsolatok:** domain- és komponenshatárok az 5. pillérben, ASP.NET Core policy-k a 2. pillérben.
- **Célbizonyíték:** több erőforrásra és szereplőre helyes policy-k megvalósítása és tesztelése, `401`/`403` megkülönböztetése, valamint IDOR vagy túl széles jogosultság felismerése.

### `SEC-TOKENS-JWT` — Tokenek, claim-ek és JWT

- **Tartalom:** opaque és self-contained token, JWT header/payload/signature, claim, issuer, audience, lifetime, clock skew, aláírás kontra titkosítás, key rotation, revocation és token leakage.
- **Előfeltételek:** `SEC-CRYPTOGRAPHY`, `SEC-IDENTITY-SESSIONS`.
- **Kapcsolatok:** `SEC-OAUTH2`, `SEC-OPENID-CONNECT`, `SEC-AUTHENTICATION`.
- **Célbizonyíték:** token validációs követelmények helyes konfigurálása és tesztelése, gyakori issuer/audience/expiry/signature hiba diagnózisa és annak magyarázata, miért nem munkamenet-megoldás minden JWT.

### `SEC-OAUTH2` — OAuth 2.0 delegált authorization

- **Tartalom:** resource owner, client, authorization server és resource server szerepek, scope, consent, Authorization Code + PKCE, client credentials, refresh token, pontos redirect URI, state és az implicit flow kerülése az OAuth 2.0 Security BCP szerint.
- **Előfeltételek:** `SEC-AUTHENTICATION`, `SEC-AUTHORIZATION`, `SEC-TOKENS-JWT` alapjai.
- **Kapcsolatok:** `SEC-OPENID-CONNECT`, külső API-k és service-to-service kommunikáció a 8. pillérben.
- **Célbizonyíték:** megfelelő flow kiválasztása és konfigurálása kliens- és API-típus alapján, szereplők és tokenek útjának magyarázata, valamint redirect/scope/client hibák diagnózisa.

### `SEC-OPENID-CONNECT` — OpenID Connect és federált bejelentkezés

- **Tartalom:** OAuth 2.0 fölötti identity layer, ID token, access token, UserInfo, discovery metadata, nonce, authentication request, claim mapping, logout és identity provider integráció.
- **Előfeltételek:** `SEC-OAUTH2`, `SEC-IDENTITY-SESSIONS`.
- **Kapcsolatok:** Angular és ASP.NET Core integráció, Keycloak mint lehetséges gyakorlati identity provider.
- **Célbizonyíték:** böngészős kliens és API OIDC-integrációja, ID/access token szerepének megkülönböztetése és discovery-, nonce- vagy claim-mapping hiba diagnózisa.

### `SEC-WEB-BROWSER-SECURITY` — Böngésző- és webalkalmazás-biztonság

- **Tartalom:** origin, same-origin policy, CORS, cookie flag-ek, SameSite, CSRF, XSS, output encoding, CSP, clickjacking, browser storage és BFF/token-tárolási trade-off.
- **Előfeltételek:** HTTP és webes kliens–szerver modell a 2. pillérben.
- **Kapcsolatok:** `SEC-AUTHENTICATION`, `SEC-IDENTITY-SESSIONS`, Angular kliensbiztonság.
- **Célbizonyíték:** cookie- vagy tokenalapú webalkalmazás támadási felületének elemzése, CORS/CSRF/XSS megkülönböztetése és a megfelelő védelem működő teszttel történő igazolása.

### `SEC-INPUT-DATA-SECURITY` — Input-, output- és adat-hozzáférési biztonság

- **Tartalom:** untrusted input, allowlist validation, SQL/command/template injection, path traversal, SSRF, unsafe deserialization, file upload, mass assignment, output encoding és érzékeny adat minimalizálása.
- **Előfeltételek:** binding és validáció a 2. pillérben, SQL és adat-hozzáférés a 3. pillérben.
- **Kapcsolatok:** `SEC-WEB-BROWSER-SECURITY`, biztonságos hibaválaszok és logging.
- **Célbizonyíték:** több inputhatár fenyegetéseinek elemzése, legalább egy injection vagy path/file jellegű hiba reprodukálása és megfelelő keretrendszeres védelem alkalmazása.

### `SEC-SUPPLY-CHAIN` — Függőségek és software supply chain

- **Tartalom:** dependency provenance, lockfile, transitive dependency, vulnerability/advisory, SBOM, package signature, build integrity, update policy és kompromittált dependency kockázata.
- **Előfeltételek:** build és package-kezelési alapok az 1. pillérben.
- **Kapcsolatok:** CI/CD és artifactkezelés a 7. pillérben.
- **Célbizonyíték:** függőségi kockázatok és ismert sérülékenységek ellenőrzése, javítás priorizálása és reprodukálható, ellenőrizhető dependency-update folyamat kialakítása.

### `SEC-AUDIT-INCIDENT-RESPONSE` — Biztonsági naplózás, visszaélésvédelem és incidenskezelés

- **Tartalom:** security event, audit trail, korreláció, érzékeny adatok naplózási tilalma, brute-force és abuse detection, rate limit, alert, triage, containment, recovery és post-incident learning.
- **Előfeltételek:** `SEC-PRINCIPLES-THREAT-MODELING`; logging és observability a 2. és 7. pillérben.
- **Kapcsolatok:** production incident response a 7. pillérben.
- **Célbizonyíték:** auditálható, titkot nem szivárogtató eseménymodell és visszaélésvédelmi kontroll készítése, riasztás kivizsgálása és rövid incidenskezelési gyakorlat dokumentálása.

## Kapcsolat a jelenlegi projektekkel

- **Backend Foundations:** a belső kivételrészleteket nem kiszivárogtató JSON-hibaválasz és a szerveroldali részletes naplózás kezdeti security-határt érint, de még nem bizonyít átfogó alkalmazásbiztonsági tudást.
- **MovieExplorer:** a connection string jelszava .NET User Secretsben marad és nem kerül Gitbe. A tervezett Keycloak/JWT/policy integráció még nem készült el, ezért authentication-, authorization-, OAuth 2.0- vagy OIDC-bizonyítékot egyelőre nem ad.

Az aktuális tudásszint és bizonyítékok forrása a gyökérben található `CURRICULUM_PROGRESS.md`; ez a fájl csak a stabil tématérképet tartalmazza.
