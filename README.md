# IntroToAPIFinal
Endpoints:

TVShowsController:
- GET: api/TVShows                                     (Returns all TVShows in the database)
- GET: api/TVShows/{id}                                (Returns TVShow of ShowId id)
- GET: api/TVShows/top/{database}/{num}                (Returns top num of TV Shows ordered by ratings in the chosen database; Highest to lowest)
- GET: api/TVShows/genre/{genre}                       (Returns all TV Shows of a specific genre)

UsersController:
- GET: api/Users                                       (Returns all UserInfo in the database)
- GET: api/Users/{id}                                  (Returns Userinfo of UserId id)
- PUT: api/Users/{id}                                  (Update UserName and/or Password; not allowed to update UserId and NumOfUserRatings)
- POST: api/Users                                      (Create a User with a UserId, unique Username, Password; not allowed to initialize NumOfUserRatings)
- Delete: api/Users/{id}                               (Delete User with UserId id)


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

