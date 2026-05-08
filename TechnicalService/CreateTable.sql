CREATE TABLE LocalAuthority
(
    LocalAuthorityId INT IDENTITY(1,1) NOT NULL,
    LocalAuthorityName NVARCHAR(50) NOT NULL,
    EanNumber NVARCHAR(13) NOT NULL,

    CONSTRAINT PK_LocalAuthority PRIMARY KEY (LocalAuthorityId)
);

CREATE TABLE CaseOfficer
(
    CaseOfficerId INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Department NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    LocalAuthorityId INT NULL,

    CONSTRAINT PK_CaseOfficer PRIMARY KEY (CaseOfficerId),
    CONSTRAINT FK_CaseOfficer_LocalAuthority
        FOREIGN KEY (LocalAuthorityId) REFERENCES LocalAuthority(LocalAuthorityId)
);

CREATE TABLE Citizen
(
    CitizenId INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(50) NOT NULL,
    CprNumber NVARCHAR(10) NOT NULL,
    WorkStatus NVARCHAR(50) NOT NULL,
    WorkType NVARCHAR(50) NOT NULL,
    ConsentStatus BIT NOT NULL,
    CurrentStatus NVARCHAR(50) NOT NULL,
    SpecialConsiderations NVARCHAR(255) NULL,

    CONSTRAINT PK_Citizen PRIMARY KEY (CitizenId),
    CONSTRAINT UQ_Citizen_CprNumber UNIQUE (CprNumber)
);

CREATE TABLE Guest
(
    GuestId INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(50) NOT NULL,
    CitizenId INT NULL,

    CONSTRAINT PK_Guest PRIMARY KEY (GuestId),
    CONSTRAINT FK_Guest_Citizen
        FOREIGN KEY (CitizenId) REFERENCES Citizen(CitizenId)
);

CREATE TABLE PodcastEpisode
(
    PodcastEpisodeID INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    Duration INT NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    MeetingPlace NVARCHAR(100) NOT NULL,
    Note NVARCHAR(255) NULL,
    CaseOfficerId INT NOT NULL,

    CONSTRAINT PK_PodcastEpisode PRIMARY KEY (PodcastEpisodeID),
    CONSTRAINT FK_PodcastEpisode_CaseOfficer
        FOREIGN KEY (CaseOfficerId) REFERENCES CaseOfficer(CaseOfficerId)
);

CREATE TABLE PodcastEpisodeGuests
(
    PodcastEpisodeID INT NOT NULL,
    GuestId INT NOT NULL,

    CONSTRAINT PK_PodcastEpisodeGuests PRIMARY KEY (PodcastEpisodeID, GuestId),
    CONSTRAINT FK_PodcastEpisodeGuests_PodcastEpisode
        FOREIGN KEY (PodcastEpisodeID) REFERENCES PodcastEpisode(PodcastEpisodeID),
    CONSTRAINT FK_PodcastEpisodeGuests_Guest
        FOREIGN KEY (GuestId) REFERENCES Guest(GuestId)
);
