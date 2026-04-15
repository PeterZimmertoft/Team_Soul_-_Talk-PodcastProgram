**UC1: Opret borgerprofil til podcast episode**

Denne use case beskriver, hvordan en ny borgerprofil oprettes i systemet, så de senere kan tilknyttes podcast-episoder.

**Aktør:** Product Owner (PO)

**Mål:** At oprette en ny borgerprofil i systemet med nødvendige oplysninger.

**Level:** User goal

**Preconditions:** PO er logget ind. Borgeren findes ikke allerede i systemet.

**Postconditions:** Borgerprofilen er oprettet og kan senere knyttes til en podcast-episode.

**Hovedscenarie:**

1. PO vælger "Gæst" i systemet.

2. Systemet viser en liste over oprettede gæster.

3. PO vælger "Opret Gæst" i systemet.

4. PO indtaster de nødvendige oplysninger for borgeren, herunder borgerens navn, CPR-nummer, telefon og email.

5. PO gemmer borgerprofilen.

6. Systemet kontrollerer, at de nødvendige felter er udfyldt, og at CPR-nummeret har korrekt længde.

6A. Hvis et obligatorisk felt mangler, viser systemet en fejlmeddelelse og beder PO udfylde de manglende oplysninger.

6B. Hvis CPR-nummeret ikke har korrekt længde, viser systemet en fejlmeddelelse og beder PO rette feltet.

7. PO vælger "Gem" i systemet.

8. Systemet gemmer borgerprofilen og bekræfter oprettelsen.


**UC2: Planlæg podcast-episode**

Denne use case viser, hvordan programmet skal håndtere den praktiske arbejdsgang for PO.

**Aktør:** Product Owner (PO)

**Mål:** At oprette og koordinere en episode med relevante deltagere og praktiske oplysninger. 

**Level:** User goal 

**Preconditions:** PO er logget ind i systemet med administrative rettigheder. De relevante deltagere findes i systemet eller kan oprettes.

**Postconditions:** Podcast-episoden er oprettet i systemet. 

**Hovedscenarie:**
1. PO vælger “Podcast Episode”.

2. Systemet viser en tom episodeliste.

3. PO vælger "Opret episode" i systemet.

4. PO angiver de relevante informationer til podcast episoden.

7. PO vælger "Tilføj" ved gæst.

8. PO vælger en eksisterende borgerprofil, eller opretter en.

8. PO vælger "Tilføj".

9. PO vælger "Gem" i den igangværende podcast episode.

9. Systemet opretter episoden i Podcast vinduet og bekræfter, at data er gemt.


**UC3: Rediger eksisterende gæst til podcast-episode**

Denne use case viser processen ved at redigere en allerede eksisterende gæst til en podcast-episode.

**Aktør:** Product Owner (PO)

**Mål:** At redigere en allerede eksisterende gæst til en podcast-episode. 

**Level:** User goal 

**Preconditions:** PO er logget ind. Gæsten findes i systemet. Der er oprettet en podcast-episode.

**Postconditions:** Gæsten er tilknyttet podcast-episoden.

**Hovedscenarie:**
1. PO vælger "Gæst".

2. Systemet viser en liste eller søgning over registrerede gæster.

3. PO vælger en eksisterende gæst og vælger "Rediger”.

4. Systemet viser gæstens eksisterende oplysninger.

4A. Hvis gæstens oplysninger er forældede eller mangelfulde, vælger PO at opdatere dem, før gæsten tilføjes.

5. PO kontrollerer oplysningerne og vælger "Gem".

7. PO trykker tilbage og vælger "Podcast episode".

8. Systemet viser en tom episodeliste.

9. PO vælger "Opret episode".

10. PO vælger "Tilføj" ved gæst.

11. Systemet viser en gæsteliste.

12. PO vælger den redigerede gæst fra gæstelisten, og vælger "Tilføj".

11. Systemet bekræfter, at gæsten er tilknyttet.

