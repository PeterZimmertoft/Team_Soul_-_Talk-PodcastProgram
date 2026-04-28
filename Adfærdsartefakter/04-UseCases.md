**UC1: Opret profil til podcast episode**

Denne use case beskriver, hvordan PO (Podcast Owner) opretter en ny profil i systemet, så profilen senere kan tilknyttes en podcast-episode. Profilen kan enten være en almindelig gæst eller en gæst med tilknyttede borgeroplysninger.

**Aktør:** Podcast Owner (PO)

**Mål:** At oprette en ny profil i systemet med de nødvendige oplysninger.

**Level:** User goal

**Preconditions:** PO er logget ind. Profilen findes ikke allerede i systemet.

**Postconditions:** Profilen er oprettet i systemet og kan senere tilknyttes en podcast-episode. Hvis der er indtastet borgeroplysninger, er disse også gemt på profilen.

**Hovedscenarie:**

1. PO anmoder om at oprette en ny profil.

2. Systemet præsenterer en formular til indtastning af profiloplysninger.

3. PO angiver profilens basisoplysninger, herunder navn, telefon og email.

4. PO indtaster eventuelt borgeroplysninger for profilen.

5. Systemet validerer de indtastede oplysninger. 
 5A. De indtastede oplysninger er ugyldige.
 Systemet viser en fejlmeddelelse til PO og afventer rettelse.

6. Systemet kontrollerer, om profilen allerede findes i systemet.
 6A. Profilen findes allerede i systemet.
 Systemet informerer PO om, at profilen allerede eksisterer.


**UC2: Planlæg podcast-episode**

Denne use case viser, hvordan PO opretter en ny podcast-episode i systemet og registrerer de relevante oplysninger. Episoden kan oprettes både med og uden tilknyttet gæster. 

**Aktør:** Podcast Owner (PO)

**Mål:** At oprette en podcast-episode i systemet med relevante oplysninger og eventuelt tilknytte gæster.

**Level:** User goal 

**Preconditions:** PO er logget ind i systemet med administrative rettigheder. 

**Postconditions:** Podcast-episoden er oprettet i systemet. Eventuelle valgte gæster er tilknyttet episoden. Hvis ingen gæster er valgt, er episoden stadig oprettet og kan opdateres senere.

**Hovedscenarie:**
1. PO anmoder om at oprette en ny podcast-episode.

2. Systemet viser en formular til indtastning af episodeoplysninger. 

3. PO indtaster episodeoplysninger, herunder titel, dato, tid, status, mdøested og note.

4. PO vælger eventuelt en eller flere eksisterende gæster, der skal tilknyttes episoden.

5. Systemet opretter podcast-episoden.

6. Systemet tilknytter de valgte gæster til episoden, hvis nogen er valgt.

7. Systemet bekræfter, at podcast-episoden er oprettet.


**UC3: Opdater borgerprofil**

Denne use case beskriver, hvordan PO opdaterer en eksisterende profil i systemet. Profilen kan indeholde både gæsteoplysninger og eventuelle tilknyttede borgeroplysninger. 

**Aktør:** Podcast Owner (PO)

**Mål:** At opdatere oplysninger på en eksisterende profil i systemet.

**Level:** User goal 

**Preconditions:** PO er logget ind. Profilen findes i systemet.

**Postconditions:** Profilens oplysninger er opdateret og gemt i systemet.

**Hovedscenarie:**
1. PO identificerer den profil, der skal redigeres.

2. Systemet viser de nuværende oplysninger for profilen.

3. PO ændrer de ønskede oplysninger.

4. Systemet gemmer de opdaterede oplysninger.
 4A. En eller flere indtastede oplysninger er ugyldige.
 Systemet viser en fejlmeddelele, og PO retter oplysningerne. 

5. Systemet bekræfter, at profilen er opdateret.

**UC4: Se episodeoversigt**

Denne use case beskriver, hvordan PO åbner podcastoversigten i systemet, ser en liste over oprettede episoder og vælger en episode for at se dens oplysninger.

**Aktør**: Podcast Owner 

**Mål** at få overblik over oprettede podcast-episoder og se oplysninger om en valgt episode. 

**Level:** User goal

**Preconditions** Systemet viser en oversigt over episoder. Hvis PO vælger en episode, vises episodeoplysningerne.

**Postconditions:** Systemet viser en oversigt over episoder.

**Hovedscenarie:**

1. PO navigerer til podcastoversigten.
2. Systemet viser en liste over oprettede podcast-episoder.
 2A. Der findes ingen oprettede episoder.
 Systemet viser en tom episodeoversigt.

3. PO vælger en episode fra listen.
 3A. PO vælger ingen episode. 
 Systemet viser fortsat episodelisten uden detaljer.

4. Systemet viser oplysninger om den valgte episode i højre side af visningen. 
5. PO gennemser episodeoplysningerne.

