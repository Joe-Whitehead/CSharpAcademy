# Habit Logger
Console based CRUD application to track time spent coding.
Developed using C# and SQLite.


# Given Requirements:
- [x] This is an application where you’ll register one habit.
- [x] This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
- [x] The application should store and retrieve data from a real database
- [x] When the application starts, it should create a sqlite database, if one isn’t present.
- [x]  It should also create a table in the database, where the habit will be logged.
- [x] The app should show the user a menu of options.
- [x]  The users should be able to insert, delete, update and view their logged habit.
- [x] You should handle all possible errors so that the application never crashes.
- [x] The application should only be terminated when the user inserts 0.
- [x] You can only interact with the database using raw SQL. You can’t use mappers such as Entity Framework.
- [x]  Your project needs to contain a Read Me file where you'll explain how your app works.

# Features

* SQLite database connection

	- The program uses a SQLite db connection to store and read information. 
	- If no database exists, or the correct table does not exist they will be created on program start.

* A console based UI where users can navigate by key presses
 
 	- ![MainMenu](https://imgur.com/ZNhVz29)

* CRUD DB functions

	- From the main menu users can Create, Read, Update or Delete entries for whichever habit or record they wish
  - Date/Time is auto captured from the time of input to capture accurate tracking

* Reports fetch data from the Database in Weekly, Monthly and Yearly time intervals

	- ![ReportingMenu](https://imgur.com/9R7qRs0)
  - ![ReportingTime](https://imgur.com/7ide4LY)
  - ![ReportResult](https://imgur.com/15XyBSE)

* Reporting and other data output uses ConsoleTables library to output in a more pleasant way

	- ![ViewRecordTable](https://imgur.com/FnzzcUX)
	- [GitHub for ConsoleTables Library](https://github.com/khalidabuhakmeh/ConsoleTables)