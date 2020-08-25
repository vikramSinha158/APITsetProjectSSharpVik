Feature: Auth Scheduler Validation

To Check eligible Account

@Smoke
Scenario: Validate user is getting positive response by hitting the AuthAPI
Given User has GET request AuthAPI with mindaysout and maxdaysout parameters
And Add Header As facilityCode With its value 
When User executes the GET request 
Then user should get success status response code 


Scenario: Validate patient type E accounts are not eligible to get auth
Given User has GET request AuthAPI with mindaysout and maxdaysout parameters
And Add Header As facilityCode With its value 
When User executes the GET request 
And user get the response
Then user should get eligible accounts whose patienttype is not E in the response

Scenario: Validate self-pay coverage accounts are not eligible to get auth
Given User has GET request AuthAPI with mindaysout and maxdaysout parameters
And Add Header As facilityCode With its value 
When User executes the GET request 
And user get the response
Then user should get eligible accounts whose coverage is not self pay in the response

Scenario: Validate Authschedulerlog  for valid accounts
Given User has GET request AuthAPI with mindaysout and maxdaysout parameters
And Add Header As facilityCode With its value 
When User executes the GET request 
And user get the response
And Authschedulerlog should have entry for all the eligible accounts

Scenario: Validate AuthRequestLog  for valid accounts
Given User has GET request AuthAPI with mindaysout and maxdaysout parameters
And Add Header As facilityCode With its value 
When User executes the GET request 
And user get the response
And authrequestlog should have entry for all the eligible accounts






