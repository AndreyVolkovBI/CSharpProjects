# Homework assignment 1 - City transport scheduling
Technological development has signicantly changed the way modern cities
operate. Taxi ordering through a mobile app, car sharing, hotspots for internet
access, live webcams, smart lighting - these being just a few examples. One of
the big changes came in the area of public transport.
The homework assignments for the Programming course (spring 2018) will
be targeted at creating an information system for a city transport management system. Well, as you will see, it will only be a very simplied model of the real one.
### Tested skills
1. Basic OOP and class composition/aggregation
2. Collections
3. File input-output
### Description of tasks
You need to store the following information about the public transport schedule:
1. Names of stops/stations
2. Routes. A single route is formed of a sequence of stops/stations. For each
route you need to store the time of rst and last departures from each
end stop/station (terminus) and the interval between buses/trams/trains
in minutes. You also need to store the time it takes to reach each of the
stations forming the route starting from the terminus.

For example, consider an abstract bus route 104, which follows 4 stops:
A,B,C,D (see picture below). Assume that the trac starts from 5:00 in
the morning from both A and D and follows a 10 minute interval until
01:00. Having interstation travel times as shown on the gure, means that
station B will have buses arriving from A at 5:03, 5:13, 5:23, etc., the last 2
bus coming at 01:03 at night. At the same buses will arrive at B from C
at 5:09, 5:19, 5:29, etc.
Note that the same stop/station may be a part of several routes (will be
shown in another example further down).
Your main task in this assignment is to come up with an algorithm, which,
given the current time and a station, prints a schedule of the nearest
departures from it.
The eciency of the algorithm will greatly depend on the data structure
you decide to utilize. There may even be several independent data structures.
