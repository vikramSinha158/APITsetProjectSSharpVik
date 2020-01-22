Feature: PhysicianSearch

A short summary of the feature

@tag1
Scenario:Verify user is getting 200 response in the status code
Given user has physician API for search
And User had provided the required header information <headerName>,<headerValue>
| headerName | headerValue |
| facilitycode | BOMC |
When user hits the API to search for physician whose name contains "A"
Then user should be able to get 200 as status code
