# IntroToAPIFinal
Endpoints:
GET: api/TVShows                                     (Returns all TVShows in the database)
GET: api/TVShows/top/{database}/{num}                (Returns top num of TV Shows ordered by ratings in the chosen database; Highest to lowest)
GET: api/TVShows/{id}                                (Returns TVShow of ShowId id)
GET: api/TVShows/genre/{genre}                       (Returns all TV Shows of a specific genre)
GET: api/Users                                       (Returns all UserInfo in the database)
GET: api/Users/{id}                                  (Returns Userinfo of UserId id)
GET: api/UserReviews                                 (Returns all UserReviews in the database)
GET: api/UserReviews/{type}/{id}                     (Returns all UserReviews for ShowId or ReviewId or UserId) type=shows or users
GET: api/UserReviews/{id}                           (Returns specific review of a show by a user)

PUT: api/Users/{id}                                  (Update UserName and/or Password; not allowed to update UserId and NumOfUserRatings)
PUT: api/UserReviews/{rid}                           (Update a UserReview except for ReviewId, ShowId, UserId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
POST: api/Users                                      (Create a User with a UserId, unique Username, Password; not allowed to initialize NumOfUserRatings)
POST: api/UserReviews                                (Create a UserReview with ReviewId (optional), ShowId, UserId, UserRating, UserComment (optional))
                                                       (Also updates the table UserInfo the NumOfUserRatings and in TVShows the AVGUserRating)

DELETE: api/Users/{id}                               (Delete User with UserId id. Remove all of the user's posts and update the AVGUserRatings in table TVShows)
DELETE: api/UserReviews/{rid}                        (Deletes a particular UserReview with rid=ReviewId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
____________________________________________________________________________


EXPLANATIONS:

GET: api/TVShows                                     (Returns all TVShows in the database)
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
        }
    ]
}
____________________________________________________________________________

GET: api/TVShows/{id}                                (Returns TVShow of ShowId id)
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

GET: api/TVShows/top/{database}/{num}                (Returns top num of TV Shows ordered by ratings in the chosen database; Highest to lowest)
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

GET: api/TVShows/genre/{genre}                       (Returns all TV Shows of a specific genre)
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

GET: api/Users                                       (Returns all UserInfo in the database)
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

GET: api/Users/{id}                                  (Returns Userinfo of UserId id)
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

GET: api/UserReviews                                 (Returns all UserReviews in the database)
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

GET: api/UserReviews/{type}/{id}                     (Returns all UserReviews for ShowId or ReviewId or UserId) type=shows or users
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

GET: api/UserReviews/{rid}                           (Returns specific review of a show by a user) rid=ReviewId
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

PUT: api/Users/{id}                                  (Update UserName and/or Password; not allowed to update UserId and NumOfUserRatings)
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

PUT: api/UserReviews/{rid}                           (Update a UserReview except for ReviewId, ShowId, UserId. It updates the NumOfUserRatings
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

POST: api/Users                                      (Create a User with a UserId, unique Username, Password; not allowed to initialize NumOfUserRatings)
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


POST: api/UserReviews                                (Create a UserReview with ReviewId (optional), ShowId, UserId, UserRating, UserComment (optional))
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

DELETE: api/Users/{id}                               (Delete User with UserId id. Remove all of the user's posts and update the AVGUserRatings in table TVShows)
GOOD SAMPLE RESPONSE ( api/Users/3 ):
(Nothing but Code 204 from Postman. Also removed all of the user's posts and updated the AVGUserRatings in table TVShows)
____________________________________________________________________________

DELETE: api/UserReviews/{rid}                        (Deletes a particular UserReview with rid=ReviewId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
GOOD SAMPLE RESPONSE ( api/userreviews/8 ):
(Nothing but Code 204 from Postman. Also other necessary values are removed/updated)
BAD SAMPLE RESPONSE ( api/userreviews/5 ):
{
    "statusCode": 404,
    "statusDescription": "User Review of User Id 5 could not be found.",
    "result": null
}
