
# EF Core Code First
https://www.youtube.com/watch?v=pYpqymkByoc&ab_channel=TheAmazingCodeverse

# Repository Pattern

## Brief Introduction (without DB Context)
https://www.youtube.com/watch?v=Wiy54682d1w&ab_channel=PatrickGod
## More detailed Introduction:
https://youtu.be/rtXpYpZdOzM
## Purpose:
Meditates between the domain and data mapping layers, acting like an **in-memory collection** of domain objects
## Benefits
- Minimizes duplicate query logic 
- Decouples your application from persistence frameworks
- Promotes testability
> Repository should not have methods like Update and Save
## A little note on Unit of Work
Keeps track of changes and coordinates the writings and savings
## Implementation:
https://youtu.be/rtXpYpZdOzM?t=703

