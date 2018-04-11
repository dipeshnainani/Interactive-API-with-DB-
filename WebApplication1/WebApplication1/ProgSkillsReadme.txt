As stated in class, this is the framework to get you started.  The name is messenger, 
as that what this project started as, but it is now a library book sharing API.

Things you will want to look at:
	- Models/Book.cs - This is how we represent all of our data.  There are several 
	                   pieces of logic in here depending on which framework we are 
					   using due to the case sensitivity issue.  Please review the
					   file if you want to use Entity Framework and comment/uncomment
					   the appropriate lines.  This will cause several reverses to be 
					   wrong (bookid changes to BookId for example).  You will need
					   to update these throughout your code

	- Services/BookProvider - Where we actually get there data.  This is current 
						setup for Dapper, there are comment on how to set it up for 
						Entity Framework

	- Controllers/* - This is how all the requests get to the Provider.  You should 
						not need to do much with this at this point.

    - DDL.txt - This is the script to create the sctucture in Postgres - this is 
	                    currently setup for Entity Framework.  You may need to change
						names of fields to support Dapper


Tools I use:
	- Visual Studio 2017 - The IDE
	- Reshaper - Allows for code cleanup
	- Datagrip - DB interface tool - https://www.jetbrains.com/datagrip/
	- Postman - Allows you to make web requests without writing an APP - https://www.getpostman.com/

Libraries (already installed in this package)
	- Dapper - Minimalistic ORM
	- Dapper.Contrib - Addon to Dapper for CRUD operation
	- Entity Framework - Microsofts ORM



