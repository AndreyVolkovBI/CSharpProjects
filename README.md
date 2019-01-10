# Transport schedule with GUI
The goal of this assignment is to practice skills related to the following topics:
* GUI
* Libraries and packages
* Abstract programming

## Application idea and requirements
This assignment is a logical continuation of the Transport Schedule project. You
will need to implement a GUI user-oriented application. Note that you are not
limited to using WPF in multi-window mode, you can use paging as well as new
libraries for creating UWP applications (online learning part of the course).

The main functionality of the app is similar to what you had to implement
in Assignment 1. The program should allow to view departures from a given
stop/station. The main differences are the following:
* Your application should provide a graphical user interface (GUI). An example of a possible mobile-oriented design is shown on figure 1. However,
it is highly advisable to come up with your own design ideas, just follow
the general guidelines listed below.
* Serialization (JSON, XML) needs to be used for saving data instead of a
custom text format.
* The code should be structured appropriately: model, data access and
business-logic classes need to be stored in a separate library project(s).
* Abstract principles need to be applied.

In addition to the above-mentioned changes you need to implement a peruser storage of personal preferences. The user is described by the following
attributes: full name, email, password

The personal preferences include a list of selected stops/stations with a description of them. The main purpose of preferences is simplifying search - instead of entering or selecting a station name from a large list (imagine how
many stations are there in a big city like Moscow) the user simply chooses one
from his/her several favourites.

The logic of the application should be divided into several loosely coupled
classes. Ideally each class should have one single responsibility.

Follow the guidelines related to abstract programming and to the app in
general:
* Logic/data classes should never directly reference UI classes. Abstract the
calls using either delegates or interfaces
* If a certain component providing several functions for outer classes, can
potentially be changed in the future, abstract it using an interface. Describe the required functionality in the interface, then implement it in a
class. Dependant components should rely on the interface rather than on
the concrete class implementing it.
* The user interface should be adaptive. Test your interface by changing
the window size and check how different components behave.
* Loading data from a file should only be done once (at program startup).
Saving should be performed whenever the user makes a change (new user
registration, finished editing favourites)

## Description of tasks
1. Take your solution for HW1 or the sample solution that is given to you
as the basis for your work. You can take the model classes and the main
algorithms from it.
2. Create a new graphical application with at least two projects (a WPF or
any other kind of GUI + class library). All the classes related to entities,
data storage and logic should go to the class library. The WPF project
should only contain windows + code behind files.
3. Design the workflow in a WPF app that replicates the main task from
HW1 - a user selects a station and the program shows the nearest departures from it.
4. Add new features to the app - user authentication with login/password,
registration and personal preferences. The application should start from
the login/registration window/page. In case of successful authentication
the user is navigated to the main window.
5. Implement the new logic related to user preferences. The app needs to
provide functionality to add/remove a station to/from favourites and to
select a station out of the saved favourites. Personal preferences should
also be saved in a file.
6. Apply abstract programming principles where necessary.


