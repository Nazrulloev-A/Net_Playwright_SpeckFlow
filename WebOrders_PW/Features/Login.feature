Feature: Login

    @SmokeSuite @LoginTest
    Scenario Outline: User is able to Login with different User role id and verify the page
        Given user navigates to Test home page
        When user logs in using username and password for <userRole>
        Then user successfully logged out

        Examples:
          | userRole |
          | Staff    |
          

    @SmokeSuite @LoginTest
    Scenario Outline: User  should not able to Login with different User role id 
        Given user navigates to Test home page
        When user logs in using invalid username and password for <userRole>
        Then  error message should pop up

        Examples:
          | userRole |
          | Invalid  |
