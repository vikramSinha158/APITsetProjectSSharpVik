Feature: GetMethodTest
	Test GET operation with Restsharp.net

Background: 
Given User authentication with following details
		| UserName    | Password |
		| Phreesia    | wRUf^6eP8ixEc^F7agU+|

Scenario: Verify Get method with Auth
	Given User perform GET  operation for "r1/rcm/activity/PatientDemographics/{{facilityCode}}/v1.0/Visit/{{EncounterId}}"
	And User perform operation to get "BOMC" and "0000026250128"
	Then User should see the "FacilityCode" name as "BOMC"
