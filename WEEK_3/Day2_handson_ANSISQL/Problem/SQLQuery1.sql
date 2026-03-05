CREATE TABLE UserInfo (
    EmailId VARCHAR(100) PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,

    CONSTRAINT CK_UserName_Length 
        CHECK (LEN(UserName) BETWEEN 1 AND 50),

    CONSTRAINT CK_User_Role 
        CHECK (Role IN ('Admin', 'Participant')),

    CONSTRAINT CK_Password_Length 
        CHECK (LEN(Password) BETWEEN 6 AND 20)
);
CREATE TABLE EventDetails (
    EventId INT PRIMARY KEY,
    EventName VARCHAR(50) NOT NULL,
    EventCategory VARCHAR(50) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description VARCHAR(255),
    Status VARCHAR(20) NOT NULL,

    CONSTRAINT CK_EventName_Length 
        CHECK (LEN(EventName) BETWEEN 1 AND 50),

    CONSTRAINT CK_EventCategory_Length 
        CHECK (LEN(EventCategory) BETWEEN 1 AND 50),

    CONSTRAINT CK_Event_Status 
        CHECK (Status IN ('Active', 'In-Active'))
);
CREATE TABLE SpeakersDetails (
    SpeakerId INT PRIMARY KEY,
    SpeakerName VARCHAR(50) NOT NULL,

    CONSTRAINT CK_SpeakerName_Length 
        CHECK (LEN(SpeakerName) BETWEEN 1 AND 50)
);
CREATE TABLE SessionInfo (
    SessionId INT PRIMARY KEY,
    EventId INT NOT NULL,
    SessionTitle VARCHAR(50) NOT NULL,
    SpeakerId INT NOT NULL,
    Description VARCHAR(255),
    SessionStart DATETIME NOT NULL,
    SessionEnd DATETIME NOT NULL,
    SessionUrl VARCHAR(255),

    CONSTRAINT FK_Session_Event 
        FOREIGN KEY (EventId) REFERENCES EventDetails(EventId),

    CONSTRAINT FK_Session_Speaker 
        FOREIGN KEY (SpeakerId) REFERENCES SpeakersDetails(SpeakerId),

    CONSTRAINT CK_SessionTitle_Length 
        CHECK (LEN(SessionTitle) BETWEEN 1 AND 50),

    CONSTRAINT CK_Session_Time 
        CHECK (SessionEnd > SessionStart)
);

CREATE TABLE ParticipantEventDetails (
    Id INT PRIMARY KEY,
    ParticipantEmailId VARCHAR(100) NOT NULL,
    EventId INT NOT NULL,
    SessionId INT NOT NULL,
    IsAttended BIT NOT NULL,

    CONSTRAINT FK_Participant_User 
        FOREIGN KEY (ParticipantEmailId) REFERENCES UserInfo(EmailId),

    CONSTRAINT FK_Participant_Event 
        FOREIGN KEY (EventId) REFERENCES EventDetails(EventId),

    CONSTRAINT FK_Participant_Session 
        FOREIGN KEY (SessionId) REFERENCES SessionInfo(SessionId)
);

INSERT INTO UserInfo VALUES 
('admin@gmail.com','AdminUser','Admin','admin123'),
('guru@gmail.com','Gurunath','Participant','guru123');

INSERT INTO EventDetails VALUES
(1,'Tech Conference','Technology','2026-05-10',
 'AI and Cloud Event','Active');

INSERT INTO SpeakersDetails VALUES
(101,'Dr. Ravi Kumar');

INSERT INTO SessionInfo VALUES
(1001,1,'AI Trends',101,
 'AI Session','2026-05-10 10:00:00',
 '2026-05-10 11:00:00',
 'https://sessionlink.com');

INSERT INTO ParticipantEventDetails VALUES
(1,'guru@gmail.com',1,1001,1);




SELECT * FROM UserInfo;
SELECT * FROM EventDetails;
SELECT * FROM SpeakersDetails;
SELECT * FROM SessionInfo;
SELECT * From ParticipantEventDetails