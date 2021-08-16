Feature: HomePage
	
	@Response
Scenario Outline: Response
	Given Post on website  '<website_URL>'
	When Payload with email '<user_Email>' and password '<user_Password>'  is entered
	Then  response '<result_response> 'of  token entered email '<user_Email>' and  password  '<user_Password>' is shown

	Examples: 
	| website_URL                            | user_Email              | user_Password | result_response |
	| https://reqres.in/api/register |eve.holt@reqres.in   | pistol                  |OK                       |
	| https://reqres.in/api/register |sydney@fife              |                            |BadRequest         |
	
	
@UserList
Scenario: Userlist
	Given list of users 
	When no payload 
	Then the  response of list users 