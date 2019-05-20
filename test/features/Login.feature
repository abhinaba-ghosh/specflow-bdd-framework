Feature: Page Login
		As a user, I want to login to the page

@SmokeTest
@Browser_Chrome
Scenario Outline: user login with valid credentials
	Given user navigate to the target login page
	And user enter "<username>" and "<password>"
	When user click the login button
	Then user should see the login success message

	Examples:
		| username | password             |
		| tomsmith | SuperSecretPassword! |
		| user     | 123!                 |