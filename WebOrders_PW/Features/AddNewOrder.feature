Feature: AddNewOrder

User should add new order 

 Scenario Outline: User should be able to add new order
        Given user navigates to Test home page
        When user logs in using username and password for <userRole>
        When I select all orders
        And I click the Delete Selected button
        Then the grid should be empty or show a deletion success message
        And the user should click on this link button 

        Examples:
          | userRole |
          | Staff    |
