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
- PUT: api/Users/top/{id}                              (Update UserRating, UserName, Password; not allowed to update UserId and NumOfUserRatings)
- POST: api/Users                                      (Create a User with a UserId, unique Username, Password; not allowed to initialize NumOfUserRatings)


UserReviewsController:
- GET: api/UserReviews                                 (Returns all UserReviews in the database)
- GET: api/UserReviews/{type}/{id}                     (Returns all UserReviews for ShowId or ReviewId or UserId) type=shows/reviews/users
- GET: api/UserReviews/{rid}/{sid}/{uid}               (Returns specific review of a show by a user) rid=ReviewId, sid=ShowId, uid=UserId
- POST: api/UserReviews                                (Create a UserReview with ReviewId (optional), ShowId, UserId, UserRating, UserComment (optional))
                                                       (Also updates the table UserInfo the NumOfUserRatings and in TVShows the AVGUserRating)
- PUT: api/UserReviews/{rid}/{sid}/{uid}               (Update a UserReview except for ReviewId, ShowId, UserId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)
- DELETE: api/UserReviews/{rid}/{sid}/{uid}            (Deletes a particular UserReview with rid=ReviewId, sid=ShowId, uid=UserId. It updates the NumOfUserRatings
                                                        from UserInfo and AVGUserRatings from TVShows)

