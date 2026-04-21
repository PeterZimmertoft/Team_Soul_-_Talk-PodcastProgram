**UC1: Opret profil til podcast episode**

Denne use case beskriver, hvordan en ny profil oprettes i systemet, så de senere kan tilknyttes podcast-episoder.

**Aktør:** Podcast Owner (PO)

**Mål:** At oprette en ny profil i systemet med nødvendige oplysninger.

**Level:** User goal

**Preconditions:** PO er logget ind. Profilen findes ikke allerede i systemet.

**Postconditions:** Profilen er oprettet og kan senere knyttes til en podcast-episode.

**Hovedscenarie:**

1. PO anmoder om at oprette en ny profil.

2. Systemet anmoder om profiloplysninger.

3. PO indsender de nødvendige oplysninger.

4. Systemet validerer data og kontrollerer, at profilen ikke allerede sksisterer
 4A. Systemet informerer PO om fejlen og afventer rettelse.
 4B. Systemet påviser, at profilen allerede eksisterer.

5. Systemet bekræfter, at profilen er oprettet.


**UC2: Planlæg podcast-episode**

Denne use case viser, hvordan programmet skal håndtere den praktiske arbejdsgang for PO.

**Aktør:** Podcast Owner (PO)

**Mål:** At oprette og koordinere en episode med relevante deltagere og praktiske oplysninger. 

**Level:** User goal 

**Preconditions:** PO er logget ind i systemet med administrative rettigheder. De relevante deltagere findes i systemet eller kan oprettes.

**Postconditions:** Podcast-episoden er oprettet i systemet. 

**Hovedscenarie:**
1. PO anmoder om at oprette en ny podcast-episode.

2. Systemet anmoder om episodedetaljer.

3. PO indsender oplysninger om episoden.

4. PO identificerer de gæster, der skal deltage i episoden.
 4A. Den ønskede gæst findes ikke i systemet, så PO udfører UC1.

5. Systemet tilknytter de valgte borgerprofiler til episoden.

6. PO færdiggør planlægningen.

7. Systemet lagrer episoden og bekræfter handlingen.


**UC3: Opdater borgerprofil**

Denne use case viser processen ved at opdatere en allerede eksisterende borger/gæst.

**Aktør:** Podcast Owner (PO)

**Mål:** At ændre oplysninger på en eksisterende gæst/borger. 

**Level:** User goal 

**Preconditions:** PO er logget ind. Borgeren findes i systemet.

**Postconditions:** Gæsten er tilknyttet podcast-episoden.

**Hovedscenarie:**
1. PO identificerer den borger, der skal redigeres.

2. Systemet viser de nuværende oplysninger for borgeren.

3. PO indsender de opdaterede oplysninger.

4. Systemet validerer og gemmer ændringerne.

5. Systemet bekræfter opdateringen.



