This project is used for understanding and do some practice on different topic

## Steps to create Custom action filters in ASP.NET Core
#  Follow https://mittalv3.atlassian.net/wiki/spaces/Dev/pages/791085059/Custom+action+filters+in+ASP.NET+Core

1. Authorization filters
	--> Check code under Todo.API > Filter > AdminOnlyAuthorizationFilter
	-->	Applied above AuthorizationFilter to one of the action method in ToDo Controllers
	--> Action method -->  GetSingleTodo
	--> Use postman to add header as X-User-Role and value as Admin, to make it work.

2. Resource filters
	--> Check code under Todo.API > Filter > IPAddressResourceFilter
	-->	Applied above Resource filter on a global level on program.cs
	--> The allowed IP are "127.0.0.1", "::1"  -- ::1 This is the IPv6 loopback address, equivalent to 127.0.0.1 in IPv4.

