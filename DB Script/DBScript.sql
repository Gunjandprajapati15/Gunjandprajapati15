
/* Post table contains post for linkedin page*/
CREATE TABLE Post
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Content VARCHAR(MAX),
	PostedOn DATETIME,
	ImageFileName VARCHAR(200)
)

/* Comment table contains commnet of the particular post*/
CREATE TABLE Comment
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	PostId INT REFERENCES Post(Id),
	Content VARCHAR(MAX),
	PostedOn DATETIME
)

/* PostLike table contains like count of an individual post*/
CREATE TABLE PostLike
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	PostId INT REFERENCES Post(Id),
	LikedOn DATETIME
)