# IntroToAPIFinal
Endpoints:
TVShowsController:
- GET: api/TVShows                                     (Returns all TVShows in the database)
- GET: api/TVShows/top/{database}/{num}                (Returns top num of TV Shows ordered by ratings in the chosen database; Highest to lowest)
- GET: api/TVShows/{id}                                (Returns TVShow of ShowId id)
- GET: api/TVShows/genre/{genre}                       (Returns all TV Shows of a specific genre)

UsersController:
- GET: api/Users                                       (Returns all UserInfo in the database)
- GET: api/Users/{id}                                  (Returns Userinfo of UserId id)
- PUT: api/Users/{id}                                  (Update UserName and/or Password; not allowed to update UserId and NumOfUserRatings)
- POST: api/Users                                      (Create a User with a UserId, unique Username, Password; not allowed to initialize NumOfUserRatings)
- Delete: api/Users/{id}                               (Delete User with UserId id. Remove all of the user's posts and update the AVGUserRatings in table TVShows)
- 
UserReviewsController:
- GET: api/UserReviews                                 (Returns all UserReviews in the database)
- GET: api/UserReviews/{type}/{id}                     (Returns all UserReviews for ShowId or ReviewId or UserId) type=shows or users
- GET: api/UserReviews/{rid}                           (Returns specific review of a show by a user) rid=ReviewId
- POST: api/UserReviews                                (Create a UserReview with ReviewId (optional), ShowId, UserId, UserRating, UserComment (optional))
                                                       (Also updates the table UserInfo the NumOfUserRatings and in TVShows the AVGUserRating)
- PUT: api/UserReviews/{rid}                           (Update a UserReview except for ReviewId, ShowId, UserId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
- DELETE: api/UserReviews/{rid}                        (Deletes a particular UserReview with rid=ReviewId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
____________________________________________________________________________



EXPLANATIONS:

TVShowsController:
- GET: api/TVShows                                     (Returns all TVShows in the database)
SAMPLE RESPONSE:
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved TVShows",
    "result": [
        {
            "showId": 1,
            "showName": "The Flash",
            "showDesc": "After being struck by lightning, Barry Allen wakes up from his coma to discover he's been given the power of super speed, becoming the Flash, and fighting crime in Central City.",
            "genre": "Superhero",
            "numSeasons": 8,
            "numEpisodes": 171,
            "episodeLength": 43,
            "yearReleased": 2014,
            "ongoing": true,
            "rTrating": 0.89,
            "imdBrating": 7.6,
            "avgUserRating": 0
        },
        {
            "showId": 2,
            "showName": "Criminal Minds",
            "showDesc": "The cases of the F.B.I. Behavioral Analysis Unit (B.A.U.), an elite group of profilers who analyze the nation's most dangerous serial killers and individual heinous crimes in an effort to anticipate their next moves before they strike again.",
            "genre": "Crime Drama",
            "numSeasons": 16,
            "numEpisodes": 326,
            "episodeLength": 42,
            "yearReleased": 2005,
            "ongoing": false,
            "rTrating": 0.85,
            "imdBrating": 8.1,
            "avgUserRating": 0
        },
        {
            "showId": 3,
            "showName": "The Office",
            "showDesc": "A mockumentary on a group of typical office workers, where the workday consists of ego clashes, inappropriate behavior, and tedium.",
            "genre": "Sitcom",
            "numSeasons": 9,
            "numEpisodes": 201,
            "episodeLength": 22,
            "yearReleased": 2005,
            "ongoing": false,
            "rTrating": 0.81,
            "imdBrating": 6,
            "avgUserRating": 0
        },
        {
            "showId": 4,
            "showName": "Breaking Bad",
            "showDesc": "A chemistry teacher diagnosed with inoperable lung cancer turns to manufacturing and selling methamphetamine with a former student in order to secure his family's future.",
            "genre": "Crime Drama",
            "numSeasons": 5,
            "numEpisodes": 62,
            "episodeLength": 49,
            "yearReleased": 2008,
            "ongoing": false,
            "rTrating": 0.96,
            "imdBrating": 7.6,
            "avgUserRating": 0
        },
        {
            "showId": 5,
            "showName": "The Walking Dead",
            "showDesc": "Sheriff Deputy Rick Grimes wakes up from a coma to learn the world is in ruins and must lead a group of survivors to stay alive.",
            "genre": "Zombie Apocalypse",
            "numSeasons": 11,
            "numEpisodes": 177,
            "episodeLength": 44,
            "yearReleased": 2010,
            "ongoing": false,
            "rTrating": 0.8,
            "imdBrating": 8.1,
            "avgUserRating": 0
        },
        {
            "showId": 6,
            "showName": "Arrow",
            "showDesc": "Spoiled billionaire playboy Oliver Queen is missing and presumed dead when his yacht is lost at sea. He returns five years later a changed man, determined to clean up the city as a hooded vigilante armed with a bow.",
            "genre": "Superhero",
            "numSeasons": 8,
            "numEpisodes": 170,
            "episodeLength": 42,
            "yearReleased": 2012,
            "ongoing": false,
            "rTrating": 0.86,
            "imdBrating": 7.5,
            "avgUserRating": 0
        },
        {
            "showId": 7,
            "showName": "The X Files",
            "showDesc": "Two F.B.I. Agents, Fox Mulder the believer and Dana Scully the skeptic, investigate the strange and unexplained, while hidden forces work to impede their efforts.",
            "genre": "Science Fiction",
            "numSeasons": 11,
            "numEpisodes": 218,
            "episodeLength": 45,
            "yearReleased": 1993,
            "ongoing": false,
            "rTrating": 0.74,
            "imdBrating": 8.6,
            "avgUserRating": 0
        },
        {
            "showId": 8,
            "showName": "South Park",
            "showDesc": "Follows the misadventures of four irreverent grade-schoolers in the quiet, dysfunctional town of South Park, Colorado.",
            "genre": "Animated Sitcom",
            "numSeasons": 25,
            "numEpisodes": 319,
            "episodeLength": 22,
            "yearReleased": 1997,
            "ongoing": true,
            "rTrating": 0.8,
            "imdBrating": 8.7,
            "avgUserRating": 0
        },
        {
            "showId": 9,
            "showName": "Family Guy",
            "showDesc": "In a wacky Rhode Island town, a dysfunctional family strives to cope with everyday life as they are thrown from one crazy scenario to another.",
            "genre": "Animated Sitcom",
            "numSeasons": 21,
            "numEpisodes": 397,
            "episodeLength": 22,
            "yearReleased": 1999,
            "ongoing": true,
            "rTrating": 0.63,
            "imdBrating": 8.2,
            "avgUserRating": 0
        },
        {
            "showId": 10,
            "showName": "Parks and Recreation",
            "showDesc": "The absurd antics of an Indiana town's public officials as they pursue sundry projects to make their city a better place.",
            "genre": "Sitcom",
            "numSeasons": 7,
            "numEpisodes": 126,
            "episodeLength": 22,
            "yearReleased": 2009,
            "ongoing": false,
            "rTrating": 0.93,
            "imdBrating": 8.6,
            "avgUserRating": 0
        },
        {
            "showId": 11,
            "showName": "Lost",
            "showDesc": "The survivors of a plane crash are forced to work together in order to survive on a seemingly deserted tropical island.",
            "genre": "Science Fiction",
            "numSeasons": 6,
            "numEpisodes": 121,
            "episodeLength": 44,
            "yearReleased": 2004,
            "ongoing": false,
            "rTrating": 0.85,
            "imdBrating": 8.3,
            "avgUserRating": 0
        },
        {
            "showId": 12,
            "showName": "Squid Game",
            "showDesc": "Hundreds of cash-strapped players accept a strange invitation to compete in children's games. Inside, a tempting prize awaits with deadly high stakes. A survival game that has a whopping 45.6 billion-won prize at stake.",
            "genre": "Survival",
            "numSeasons": 1,
            "numEpisodes": 9,
            "episodeLength": 55,
            "yearReleased": 2021,
            "ongoing": true,
            "rTrating": 0.95,
            "imdBrating": 8,
            "avgUserRating": 0
        },
        {
            "showId": 13,
            "showName": "Rick and Morty",
            "showDesc": "An animated series that follows the exploits of a super scientist and his not-so-bright grandson.",
            "genre": "Animated Sitcom",
            "numSeasons": 6,
            "numEpisodes": 58,
            "episodeLength": 23,
            "yearReleased": 2013,
            "ongoing": true,
            "rTrating": 0.93,
            "imdBrating": 9.1,
            "avgUserRating": 0
        },
        {
            "showId": 14,
            "showName": "Friends",
            "showDesc": "Follows the personal and professional lives of six twenty to thirty year-old friends living in the Manhattan borough of New York City.",
            "genre": "Sitcom",
            "numSeasons": 10,
            "numEpisodes": 236,
            "episodeLength": 22,
            "yearReleased": 1994,
            "ongoing": false,
            "rTrating": 0.93,
            "imdBrating": 8.9,
            "avgUserRating": 0
        },
        {
            "showId": 15,
            "showName": "Daredevil",
            "showDesc": "A blind lawyer by day, vigilante by night. Matt Murdock fights the crime of New York as Daredevil.",
            "genre": "Superhero",
            "numSeasons": 3,
            "numEpisodes": 39,
            "episodeLength": 54,
            "yearReleased": 2015,
            "ongoing": false,
            "rTrating": 0.92,
            "imdBrating": 8.6,
            "avgUserRating": 0
        },
        {
            "showId": 16,
            "showName": "Key & Peele",
            "showDesc": "Project sees Keegan-Michael Key and Jordan Peele in front of a live studio audience bantering about a topic weaved between filmed shorts and sketches.",
            "genre": "Comedy",
            "numSeasons": 5,
            "numEpisodes": 53,
            "episodeLength": 30,
            "yearReleased": 2012,
            "ongoing": false,
            "rTrating": 0.97,
            "imdBrating": 8.3,
            "avgUserRating": 0
        },
        {
            "showId": 17,
            "showName": "How I Met Your Mother",
            "showDesc": "A father recounts to his children - through a series of flashbacks - the journey he and his four best friends took leading up to him meeting their mother.",
            "genre": "Romantic Comedy",
            "numSeasons": 9,
            "numEpisodes": 208,
            "episodeLength": 22,
            "yearReleased": 2005,
            "ongoing": false,
            "rTrating": 0.84,
            "imdBrating": 8.3,
            "avgUserRating": 0
        },
        {
            "showId": 18,
            "showName": "The Simpsons",
            "showDesc": "The satiric adventures of a working-class family in the misfit city of Springfield.",
            "genre": "Animated Sitcom",
            "numSeasons": 34,
            "numEpisodes": 736,
            "episodeLength": 22,
            "yearReleased": 1989,
            "ongoing": true,
            "rTrating": 0.85,
            "imdBrating": 8.7,
            "avgUserRating": 0
        },
        {
            "showId": 19,
            "showName": "Blue Bloods",
            "showDesc": "Revolves around a family of New York cops.",
            "genre": "Crime Drama",
            "numSeasons": 13,
            "numEpisodes": 260,
            "episodeLength": 43,
            "yearReleased": 2010,
            "ongoing": true,
            "rTrating": 0.85,
            "imdBrating": 7.7,
            "avgUserRating": 0
        },
        {
            "showId": 20,
            "showName": "Supernatural",
            "showDesc": "Two brothers follow their father's footsteps as hunters, fighting evil supernatural beings of many kinds, including monsters, demons, and gods that roam the earth.",
            "genre": "Fantasy",
            "numSeasons": 15,
            "numEpisodes": 327,
            "episodeLength": 44,
            "yearReleased": 2005,
            "ongoing": false,
            "rTrating": 0.93,
            "imdBrating": 8.4,
            "avgUserRating": 0
        },
        {
            "showId": 21,
            "showName": "Game of Thrones",
            "showDesc": "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia.",
            "genre": "Fantasy",
            "numSeasons": 8,
            "numEpisodes": 73,
            "episodeLength": 57,
            "yearReleased": 2011,
            "ongoing": false,
            "rTrating": 0.89,
            "imdBrating": 9.2,
            "avgUserRating": 0
        },
        {
            "showId": 22,
            "showName": "Stranger Things",
            "showDesc": "When a young boy disappears, his mother, a police chief and his friends must confront terrifying supernatural forces in order to get him back.",
            "genre": "Fantasy",
            "numSeasons": 4,
            "numEpisodes": 34,
            "episodeLength": 51,
            "yearReleased": 2016,
            "ongoing": true,
            "rTrating": 0.92,
            "imdBrating": 8.7,
            "avgUserRating": 0
        }
    ]
}
____________________________________________________________________________

- GET: api/TVShows/{id}                                (Returns TVShow of ShowId id)
GOOD SAMPLE BODY ( api/TVShows/2 ):
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved TVShow with ShowId of 2",
    "result": {
        "showId": 2,
        "showName": "Criminal Minds",
        "showDesc": "The cases of the F.B.I. Behavioral Analysis Unit (B.A.U.), an elite group of profilers who analyze the nation's most dangerous serial killers and individual heinous crimes in an effort to anticipate their next moves before they strike again.",
        "genre": "Crime Drama",
        "numSeasons": 16,
        "numEpisodes": 326,
        "episodeLength": 42,
        "yearReleased": 2005,
        "ongoing": false,
        "rTrating": 0.85,
        "imdBrating": 8.1,
        "avgUserRating": 0
    }
}
BAD SAMPLE RESPONSE (api/TVShows/40):
{
    "statusCode": 404,
    "statusDescription": "TVShow with ShowId of 40 could not be found.",
    "result": null
}
Explanation: I only inserted 22 TVShows in here so anything above 22 is not a valid entry.
____________________________________________________________________________

- GET: api/TVShows/top/{database}/{num}                (Returns top num of TV Shows ordered by ratings in the chosen database; Highest to lowest)
GOOD RESPONSE ( api/TVShows/top/IMDB/5 ):
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved the top 5 TVShows from IMDB",
    "result": [
        {
            "showId": 21,
            "showName": "Game of Thrones",
            "showDesc": "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia.",
            "genre": "Fantasy",
            "numSeasons": 8,
            "numEpisodes": 73,
            "episodeLength": 57,
            "yearReleased": 2011,
            "ongoing": false,
            "rTrating": 0.89,
            "imdBrating": 9.2,
            "avgUserRating": 0
        },
        {
            "showId": 13,
            "showName": "Rick and Morty",
            "showDesc": "An animated series that follows the exploits of a super scientist and his not-so-bright grandson.",
            "genre": "Animated Sitcom",
            "numSeasons": 6,
            "numEpisodes": 58,
            "episodeLength": 23,
            "yearReleased": 2013,
            "ongoing": true,
            "rTrating": 0.93,
            "imdBrating": 9.1,
            "avgUserRating": 0
        },
        {
            "showId": 14,
            "showName": "Friends",
            "showDesc": "Follows the personal and professional lives of six twenty to thirty year-old friends living in the Manhattan borough of New York City.",
            "genre": "Sitcom",
            "numSeasons": 10,
            "numEpisodes": 236,
            "episodeLength": 22,
            "yearReleased": 1994,
            "ongoing": false,
            "rTrating": 0.93,
            "imdBrating": 8.9,
            "avgUserRating": 0
        },
        {
            "showId": 8,
            "showName": "South Park",
            "showDesc": "Follows the misadventures of four irreverent grade-schoolers in the quiet, dysfunctional town of South Park, Colorado.",
            "genre": "Animated Sitcom",
            "numSeasons": 25,
            "numEpisodes": 319,
            "episodeLength": 22,
            "yearReleased": 1997,
            "ongoing": true,
            "rTrating": 0.8,
            "imdBrating": 8.7,
            "avgUserRating": 0
        },
        {
            "showId": 18,
            "showName": "The Simpsons",
            "showDesc": "The satiric adventures of a working-class family in the misfit city of Springfield.",
            "genre": "Animated Sitcom",
            "numSeasons": 34,
            "numEpisodes": 736,
            "episodeLength": 22,
            "yearReleased": 1989,
            "ongoing": true,
            "rTrating": 0.85,
            "imdBrating": 8.7,
            "avgUserRating": 0
        }
    ]
}

____________________________________________________________________________

- GET: api/TVShows/genre/{genre}                       (Returns all TV Shows of a specific genre)
GOOD SAMPLE RESPONSE ( api/TVShows/genre/Superhero ):
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved TVShows that are Superhero",
    "result": [
        {
            "showId": 1,
            "showName": "The Flash",
            "showDesc": "After being struck by lightning, Barry Allen wakes up from his coma to discover he's been given the power of super speed, becoming the Flash, and fighting crime in Central City.",
            "genre": "Superhero",
            "numSeasons": 8,
            "numEpisodes": 171,
            "episodeLength": 43,
            "yearReleased": 2014,
            "ongoing": true,
            "rTrating": 0.89,
            "imdBrating": 7.6,
            "avgUserRating": 0
        },
        {
            "showId": 6,
            "showName": "Arrow",
            "showDesc": "Spoiled billionaire playboy Oliver Queen is missing and presumed dead when his yacht is lost at sea. He returns five years later a changed man, determined to clean up the city as a hooded vigilante armed with a bow.",
            "genre": "Superhero",
            "numSeasons": 8,
            "numEpisodes": 170,
            "episodeLength": 42,
            "yearReleased": 2012,
            "ongoing": false,
            "rTrating": 0.86,
            "imdBrating": 7.5,
            "avgUserRating": 0
        },
        {
            "showId": 15,
            "showName": "Daredevil",
            "showDesc": "A blind lawyer by day, vigilante by night. Matt Murdock fights the crime of New York as Daredevil.",
            "genre": "Superhero",
            "numSeasons": 3,
            "numEpisodes": 39,
            "episodeLength": 54,
            "yearReleased": 2015,
            "ongoing": false,
            "rTrating": 0.92,
            "imdBrating": 8.6,
            "avgUserRating": 0
        }
    ]
}

____________________________________________________________________________
____________________________________________________________________________

UsersController:
- GET: api/Users                                       (Returns all UserInfo in the database)
SAMPLE RESPONSE:
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved Users",
    "result": [
        {
            "userId": 1,
            "username": "Harry",
            "userPassword": "password",
            "numOfReviews": 0
        },
        {
            "userId": 2,
            "username": "Barry",
            "userPassword": "password",
            "numOfReviews": 0
        },
        {
            "userId": 3,
            "username": "Bob",
            "userPassword": "password",
            "numOfReviews": 0
        }
    ]
}
____________________________________________________________________________

- GET: api/Users/{id}                                  (Returns Userinfo of UserId id)
GOOD SAMPLE RESPONSE ( api/Users/1 ):
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved User of UserId 1",
    "result": {
        "userId": 1,
        "username": "Harry",
        "userPassword": "password",
        "numOfReviews": 0
    }
}
____________________________________________________________________________

- PUT: api/Users/{id}                                  (Update UserName and/or Password; not allowed to update UserId and NumOfUserRatings)
GOOD SAMPLE BODY ( api/Users/1 ):
{
    "userid": 1,
    "username": "Larry",
    "userpassword": "password",
    "numOfReviews": 0
}
GOOD SAMPLE RESPONSE:
(Nothing but Code 204 from Postman)
Explanation: If a username is the same as an existing one, the request will still be valid, but username won't change.
____________________________________________________________________________

- POST: api/Users                                      (Create a User with a UserId, unique Username, Password; not allowed to initialize NumOfUserRatings)
GOOD SAMPLE BODY:
{
    "userid": 7,
    "username": "Matthew",
    "userpassword": "password",
    "numOfReviews": 10
}
GOOD SAMPLE RESPONSE:
{
    "statusCode": 201,
    "statusDescription": "Successfully created User ",
    "result": {
        "userId": 7,
        "username": "Matthew",
        "userPassword": "password",
        "numOfReviews": 0
    }
}
Explanation: This is good because user should never initialize numOfReviews. No matter what value it is, this will be set to 0 upon posting.

BAD SAMPLE BODY:
{
    "userid": 2,
    "username": "Harry",
    "userpassword": "password",
    "numOfReviews": 0
}
BAD SAMPLE RESPONSE:
{
    "statusCode": 409,
    "statusDescription": "Error. The entry already exists",
    "result": null
}
Explanation: This is bad because the name Harry is already being used. The same error occurs when repeating names and UserIds.
____________________________________________________________________________

- Delete: api/Users/{id}                               (Delete User with UserId id. Remove all of the user's posts and update the AVGUserRatings in table TVShows)
GOOD SAMPLE RESPONSE ( api/Users/3 ):
(Nothing but Code 204 from Postman. Also removed all of the user's posts and updated the AVGUserRatings in table TVShows)
____________________________________________________________________________
____________________________________________________________________________

UserReviewsController:
- GET: api/UserReviews                                 (Returns all UserReviews in the database)
SAMPLE RESPONSE:
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved User Reviews",
    "result": [
        {
            "reviewId": 8,
            "showId": 2,
            "userId": 3,
            "userRating": 10,
            "userComment": null
        },
        {
            "reviewId": 9,
            "showId": 6,
            "userId": 2,
            "userRating": 9,
            "userComment": "Loved this show!"
        },
        {
            "reviewId": 10,
            "showId": 2,
            "userId": 2,
            "userRating": 7,
            "userComment": null
        }
    ]
}
____________________________________________________________________________

- GET: api/UserReviews/{type}/{id}                     (Returns all UserReviews for ShowId or ReviewId or UserId) type=shows or users
GOOD SAMPLE RESPONSE ( api/UserReviews/shows/2 ):
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved UserReviews of ShowId 2",
    "result": [
        {
            "reviewId": 8,
            "showId": 2,
            "userId": 3,
            "userRating": 10,
            "userComment": null
        },
        {
            "reviewId": 10,
            "showId": 2,
            "userId": 2,
            "userRating": 7,
            "userComment": null
        }
    ]
}
BAD SAMPLE RESPONSE ( api/UserReviews/shows/3 ):
{
    "statusCode": 404,
    "statusDescription": "UserReviews of ShowId 3 could not be found.",
    "result": null
}
____________________________________________________________________________

- GET: api/UserReviews/{rid}                           (Returns specific review of a show by a user) rid=ReviewId
GOOD SAMPLE RESPONSE ( api/UserReviews/8 ):
{
    "statusCode": 200,
    "statusDescription": "Successfully retrieved UserReview of ReviewId 8",
    "result": {
        "reviewId": 8,
        "showId": 2,
        "userId": 3,
        "userRating": 10,
        "userComment": null
    }
}

BAD SAMPLE RESPONSE ( api/UserReviews/3 ):
{
    "statusCode": 404,
    "statusDescription": "UserReview of ReviewId 3 could not be found.",
    "result": null
}
____________________________________________________________________________

- POST: api/UserReviews                                (Create a UserReview with ReviewId (optional), ShowId, UserId, UserRating, UserComment (optional))
                                                       (Also updates the table UserInfo the NumOfUserRatings and in TVShows the AVGUserRating)
GOOD SAMPLE BODY:
{
    "reviewId": 11,
    "showId": 8,
    "userId": 3,
    "userRating": 7,
    "userComment": "Great show, but person A was a really bad actor."
}
GOOD SAMPLE RESPONSE:
{
    "statusCode": 201,
    "statusDescription": "Successfully created User Review",
    "result": {
        "reviewId": 11,
        "showId": 8,
        "userId": 3,
        "userRating": 7,
        "userComment": "Great show, but person A was a really bad actor."
    }
}
BAD SAMPLE BODY:
{
    "reviewId": 8,
    "showId": 2,
    "userId": 3,
    "userRating": 10
}
BAD SAMPLE RESPONSE:
{
    "statusCode": 409,
    "statusDescription": "Error. The entry already exists",
    "result": null
}
Other Bad samples include using the same pair of (showId, userId) for a post or if a reviewId has already been used. ReviewId is optional so if
not provided the id will increment starting from the previously entered ReviewId (so if the last entered is 10, next is 11).
____________________________________________________________________________

- PUT: api/UserReviews/{rid}                           (Update a UserReview except for ReviewId, ShowId, UserId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
GOOD SAMPLE BODY ( api/UserReviews/8 ):
{
    "reviewId": 8,
    "showId": 2,
    "userId": 3,
    "userRating": 8,
    "userComment": "This is a comment"
}
GOOD SAMPLE RESPONSE:
(Nothing but Code 204 from Postman)
BAD SAMPLE BODY ( api/userreviews/5 ) (Since reviewId 5 does not exist):
{
    "reviewId": 8,
    "showId": 2,
    "userId": 3,
    "userRating": 8,
    "userComment": "This is a comment"
}
BAD SAMPLE RESPONSE:
{
    "statusCode": 400,
    "statusDescription": "Invalid request. Check your request for typos.",
    "result": null
}
If reviewId 5 existed, then the response would match the good sample response, but the reviewId would not be changed.
____________________________________________________________________________

- DELETE: api/UserReviews/{rid}                        (Deletes a particular UserReview with rid=ReviewId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
GOOD SAMPLE RESPONSE ( api/userreviews/8 ):
(Nothing but Code 204 from Postman. Also other necessary values are removed/updated)
BAD SAMPLE RESPONSE ( api/userreviews/5 ):
{
    "statusCode": 404,
    "statusDescription": "User Review of User Id 5 could not be found.",
    "result": null
}
