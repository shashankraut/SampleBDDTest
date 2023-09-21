Feature: SauceDemo

Background:
	Given I launch the browser
		And I navigate to SauceDemo


@SampleTest
Scenario: Sample sauce demo
	Given I enter username as 'standard_user'
		And I enter password as 'secret_sauce'
	When I click on login button
	Then Verify inventory page shall be available
	When I add first item from list to cart and a make note of price
		And I click on cart option
	Then Verify cart page is available
	And Verify shopping cart shall be available with details as expected
	When I click on checkout
		And I fill details as firstname as 'Shashank' lastname as 'Raut' and zipcode as '411015'
		And I click on continue
	Then Verify shopping cart shall be available with details as expected
		And I click Finish
	
