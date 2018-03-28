# Springfield Recreation Center

View a demo [here](http://springfieldrec.azurewebsites.net/)

-----------------

> As John, I would like to provide my name, e-mail, telephone and mailing address to get updates about
activities or classes I am interested in.

This application is an MVP that allows 'John' to achieve their goal. People are able to view the classes available at Springfield Rec and register their interest to receive future communications.

## Technology
This rapid prototype was built using ASP.NET Core MVC, and borrows liberally from Jimmy Bogard's [Contoso University Core](https://github.com/jbogard/ContosoUniversityCore) patterns.

Deployed to a Microsoft Azure free trial via Visual Studio's Publish to Azure feature


## Considerations, Assumptions and Notes
1. Notably out of scope is an activity calendar, and so the "classes" are not attached to any season or timeframe. This was done to avoid engineering a solution to a problem that has not yet been defined by the PO and/or team.
1. Rec Center member models (RecMember) have been built using lists to store contact information, and have a naive implementation to fetch a "primary" email or phone number. By building in the capability at the lowest level means the data model will not have to change significantly to meet the PO's possible requirements, and was cheap to implement.
1. With registration of members being out of scope of this sprint, identifying a member has been done based on first + last name. This will need to be added to the technical debt backlog.
1. Allowing John to enter his address was dropped until further clarification with the PO can be done at the next stand up. The team believes a blocking task to this activity is to allow for member sign up.

### Other
1. The /contacts page is meant for early development purposes and should be removed or secured once an "employee" portal is established.  
1. Images were obtained through Google's "available for reuse" search.
