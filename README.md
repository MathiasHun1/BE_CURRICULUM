# Software Engineering Learning

Ez a repository a modern .NET backend fókuszú, hosszú távú szoftvermérnöki fejlődés központi tanulási munkaterülete.

Nem kurzusgyűjtemény és nem teljesítési százalékot számoló checklist. Feladata, hogy összekösse:

```text
hosszú távú tanterv
→ aktuális tanulási fókusz
→ valós gyakorlóprojekt
→ implementáció és ellenőrzés
→ bizonyítékokon alapuló haladás
```

## A fontos fájlok

- [`software-engineering-curriculum.md`](software-engineering-curriculum.md): a hosszú távú tudástérkép és a nyolc pillér.
- [`LEARNING_STATE.md`](LEARNING_STATE.md): rövid, folyamatosan aktualizált globális állapot és az egyetlen következő lépés.
- [`LEARNER_PROFILE.md`](LEARNER_PROFILE.md): háttér, célok, megfigyelt erősségek, hiányosságok és tanulási preferenciák.
- [`AGENTS.md`](AGENTS.md): a mentoráló agent tartós működési szabályai és a beolvasási sorrend.
- [`projects/PROJECT_INDEX.md`](projects/PROJECT_INDEX.md): a tanuláshoz kapcsolt gyakorlóprojektek és projektspecifikus állapotfájljaik.
- [`curriculum/README.md`](curriculum/README.md): a tanterv későbbi, fokozatos felbontásának szabályai.
- [`archive/README.md`](archive/README.md): a már nem aktív állapotok archiválási szabályai.

## Munkamenet indítása

A repository gyökeréből indított agentnek elegendő ezt mondani:

```text
Folytassuk a tanulást.
```

Az agent az `AGENTS.md` alapján először a rövid globális állapotot olvassa el, majd csak az aktuális témához szükséges tantervi és projektfájlokat nyitja meg.

Hasznos rövid üzenetek:

- `status` — rövid helyzetkép bizonyítékokkal és a következő lépéssel;
- `update` — a lezárt munkamenet állapotfájljainak aktualizálása;
- `skip` — az aktuális kérdés vagy lépés kihagyása;
- `implement` — a már pontosan meghatározott aktuális lépés minimális agent-implementációja.

Ezek egyszerű beszélgetési megállapodások, nem telepített parancsok.

## Alapvető működés

Alapesetben a tanuló írja és futtatja a kódot. Az agent egy kis lépést ad, röviden bemutatja a szükséges fogalmat, majd a tényleges implementációt és eredményt ellenőrzi. Egy téma tudásszintje csak megfigyelhető bizonyíték — például önálló implementáció, teszt, hibakeresés vagy pontos magyarázat — alapján változik.

A repository Git-története később megmutatja a tanterv, a tanulási módszer és a tudásállapot fejlődését. Titkok, jelszavak, tokenek és privát deployment credentialök nem kerülhetnek bele.
