## Relationel Databaseskema

GUEST(<ins>GuestId</ins> Name, Phone, Email, ConsentStatus, *PodcastEpisodeId*, *CitizenId*)


CITIZEN(<ins>CitizenId</ins>, Name, CPRNumber, WorkStatus, WorkType, ConsentStatus, CurrentStatus, SpecialConsiderations, *CaseOfficerId*)


CASEOFFICER(<ins>CaseOfficerId</ins>, Name, Department, Phone, Email, *LocalAuthorityId*)

PODCASTEPISODE(<ins>PodcastId</ins>, Title, Date, Duration, Status, MeetingPlace, Note, *GuestId*, *CaseOfficerId*)

