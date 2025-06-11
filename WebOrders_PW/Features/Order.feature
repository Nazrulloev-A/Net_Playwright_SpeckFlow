Feature: Web Orders Page Functionalities

    Scenario: Validate grid, delete all orders and add a new one
        Given user navigates to Test home page
        When user logs in using username and password for <userRole>
        Given I am on the Web Orders page
        Then I should see the orders grid with at least one record
        When I select all orders
        And I click the Delete Selected button
        Then the grid should be empty or show a deletion success message
        When I navigate to the Order page
        And I fill in and submit a new order
#        Then the new order should appear in the orders grid

    Examples:
      | userRole |
      | Staff    |
      
       