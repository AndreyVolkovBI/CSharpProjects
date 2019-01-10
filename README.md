# Database storage
In this assignment you will switch the transport schedule project from file to
database storage and add some minor features to it.

## Description of tasks
The current version of the application that you have after HW2 should have
data serialized and deserialized from JSON (a sample solution for HW2 will be
given after holidays in May). You will now need to store the same dataset in a
database. However don’t rush removing everything related to file storage - you
will need it to seed the database.

The application data model stays the same: routes, stations, users, favourites.
The same station can be part of multiple routes. Each user is authenticated on
startup by the e-mail-password (or login-password) pair. Each user also has
his/her personal favourites. A favourite is a station with a text description.
Descriptions of the same station may easily differ for various users, e.g. one
user may have station “A” with description “Home”, the other user will have
the same station with description “Work”.

## Requirements
1. Make sure that your current data model corresponds to the description
above. Make changes if necessary.
2. Using Entity Framework with code-first migrations, create a new SQL
Server database based on your object-oriented model. Your entity classes
need to contain appropriate restrictions in the form of either attributes
or Fluent API. E.g. things like names of stations or routes are required
fields, they can’t be left blank in the database.
3. Using the Seed method, your current repository class and the json or xml
files, copy data from your file(s) to the new database. Recommendations
on how to load data from a file in the Seed method are specified in a
special section on Canvas.
4. Create a new repository type, which takes data from a database context.
Make sure that both repositories rely on the same basic interface, so that
the transition from FileRepository to DatabaseRepository is as smooth as
possible. Use the appropriate techniques to limit the number of places in
the program where abstractions are instantiated.
