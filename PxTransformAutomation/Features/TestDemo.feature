Feature: GetMethodTest
	Test GET operation with Restsharp.net

Background: 
Given User authentication with following details
		| UserName    | Password |
		| Phreesia    | wRUf^6eP8ixEc^F7agU+|

Scenario: Verify Get method with Auth
	Given User perform GET  operation for "r1/rcm/activity/PatientDemographics/{facilityCode}/v1.0/Visit/{EncounterId}"
	And User perform operation to get "BOMC" and "0000026250128"
	Then User should see the "FacilityCode" name as "BOMC"


Scenario: Verify POST method with Auth
	Given User perform POST  operation for "r1/rcm/activity/Text/{facilityCode}/v1.0/Visit/{EncounterId}/events"
	And User perform operation to get "BOMC" and "0000026250128"
	And User send "TextData" as a body for POST request
	Then User should receive "201" as a Status code