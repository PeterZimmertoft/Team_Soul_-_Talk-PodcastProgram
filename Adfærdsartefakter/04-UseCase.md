**UC1: Opret borgerprofil til podcast episode**

Denne use case beskriver, hvordan en ny borger oprettes i systemet, så de senere kan tilknyttes podcast-episoder.

Aktør: Product Owner (PO)

Mål: At oprette en ny borgerprofil i systemet med nødvendige kontakt- og samtykkeoplysninger.

Level: User goal

Preconditions: PO er logget ind. Borgeren findes ikke allerede i systemet. Samtykkeerklæring og basisoplysninger er tilgængelige.

Postconditions: Borgerprofilen er oprettet og kan senere knyttes til en podcast-episode.

Hovedscenarie:
1. PO vælger "Opret ny borger" i systemet.
2. Systemet viser en skabelon til borgeroplysninger.
3.  PO indtaster borgerens navn, CPR-nummer, telefon og email.
4. PO angiver borgerens jobstatus og relevante hensyn.
5. PO uploader samtykkeerklæring.
6. PO gemmer profilen.
7. Systemet kontrollerer, at de obligatoriske felter er udfyldt, og at CPR-nummeret har korrekt længde.

A. Hvis et obligatorisk felt mangler, viser systemet en fejlmeddelelse og beder PO udfylde de manglende oplysninger.

B. Hvis CPR-nummeret ikke har korrekt længde, viser systemet en fejlmeddelelse og beder PO rette feltet.
8. Systemet gemmer brugerprofilen og bekræfter oprettelsen.


**UC2: Planlæg podcast-episode**

Denne use case viser, hvordan programmet skal håndtere den praktiske arbejdsgang for PO.

Aktør: Product Owner (PO)

Mål: At oprette og koordinere en episode med relevante deltager og praktiske oplysninger. 

Level: User goal 

Preconditions: PO er logget ind i systemet med administrative rettigheder. De relevante deltagere findes i systemet eller kan oprettes.

Postconditions: Podcast-episoden er oprettet i systemet og registreret i kalenderen.

Hovedscenarie:
1. PO vælger “Opret ny podcast-episode”.
2. Systemet viser en tom episodeskabelon.
3. PO vælger en borger som deltager.
4. PO vælger en sagsbehandler eller anden kommunal partner som deltager.
5. PO indtaster dato, tidspunkt og mødested.
6. PO tilføjer redaktionelle noter.
7. PO gemmer planlægningen.


8. Systemet validerer dato og deltagere.

A. Hvis den valgte dato eller tid ikke er ledig, viser systemet en fejlmeddelelse og beder PO vælge et andet tidspunkt.

B. Hvis en gæst ikke har gyldigt samtykke, advarer systemet PO og forhindrer planlægningen, indtil det er rettet.

9. Systemet opretter episoden i kalenderen og bekræfter, at data er gemt.


**UC3: Tilføj eksisterende gæst til podcast-episode**

Denne use case viser processen ved at tilføje en allerede eksisterende gæst til en podcast-episode, samt verificering af tidligere information fra borgere.

Aktør: Product Owner (PO)

Mål: At tilføje en allerede eksisterende 

gæst til en podcast-episode. 

Level: User goal 

Preconditions: PO er logget ind. Gæsten findes i systemet. Der er oprettet eller åbnet en podcast-episode.

Postconditions: Gæsten er tilknyttet podcast-episoden.

Hovedscenarie:
1. PO åbner en eksisterende eller ny podcast-episode.

2. PO vælger “Tilføj eksisterende gæst”.

3. Systemet viser en liste eller søgning over registrerede gæster.

4. PO finder og vælger den ønskede gæst.

5. Systemet viser gæstens eksisterende oplysninger og samtykkestatus.

A. Hvis gæstens oplysninger er forældede eller mangelfulde, vælger PO at opdatere dem, før gæsten tilføjes.

B. Hvis samtykke ikke er gyldigt, viser systemet en advarsel og afbryder tilknytningen, indtil samtykket er opdateret.

6. PO kontrollerer oplysningerne.

7. PO godkender tilknytningen.

8. Systemet tilføjer gæsten til episoden.

9. Systemet bekræfter, at gæsten er tilknyttet.

**UC4: Opdatér samtykke/kontrakt på eksisterende gæst**

Denne use case beskriver, hvordan PO opdaterer en eksisterende gæsts samtykke eller kontrakt i systemet, når et tidligere dokument er udløbet, ændret eller skal erstattes af en ny version. Formålet er at sikre, at gæstens profil altid indeholder den nyeste og gyldige dokumentation, samtidig med at tidligere dokumenter bevares som historik eller markeres som arkiveret.

Aktør: Product Owner (PO)

Mål: At opdatere en eksisterende gæsts kontrakt eller samtykkedokument i systemet.

Preconditions: Gæsten findes i systemet. Der er allerede tilknyttet et tidligere dokument.

Postconditions: Den nye kontrakt er gemt, og den gamle er markeret som erstattet eller arkiveret.

Hovedscenarie:
1. PO åbner gæstens profil.

2. PO vælger dokument- eller kontakt sektionen.

3. Systemet viser de eksisterende dokumenter.

4. PO vælger “Opdatér kontrakt”.

5. PO vælger den nye fil.

A. Hvis filformatet ikke er tilladt, viser systemet en fejlmeddelelse.

6. Systemet viser filnavn og beder om bekræftelse.

7. PO bekræfter uploaden.

A. Hvis PO annullerer, fortsætter systemet uden ændringer. Jeres nuværende version mangler netop den slags afklaring. 

8. Systemet gemmer den nye kontrakt.

A. Hvis uploaden fejler, gemmes ingen ændringer, og systemet informerer PO.

9. Systemet markerer den gamle kontrakt som inaktiv eller arkiveret.

10. Systemet bekræfter opdateringen.
