@startuml - Use Case Diagram for Soul&Talk
left to right direction
actor "Product Owner" as m
rectangle Soul&Talk {
  usecase "Opret borgerprofil til podcast episode" as UC1
  usecase "Planlæg podcast-episode" as UC2
  usecase "Tilføj eksisterende gæst til podcast-episode" as UC3
  usecase "Opdatér samtykke/kontrakt på eksisterende gæst" as UC4
}
m --> UC1
m --> UC2
m --> UC3
m --> UC4

@enduml