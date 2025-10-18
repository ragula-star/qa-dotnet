Feature: Login Functionality
  This feature tests various login scenarios for the Mars web application.

  @smoke
  Scenario: Login with valid credentials
    Given I am on the login page
    When I enter valid credentials
    Then I should see the secure area

  @negative
  Scenario: Login with invalid username and valid password
    Given I am on the login page
    When I enter an invalid username and valid password
    Then I should see an email error message

  @negative
  Scenario: Login with valid username and invalid password
    Given I am on the login page
    When I enter a valid username and invalid password
    Then I should see an email password error message

  @negative
  Scenario: Login with empty credentials
    Given I am on the login page
    When I enter empty credentials
    Then I should see an email valid alert

  @negative
  Scenario: Login with username having wrong case
    Given I am on the login page
    When I enter username with wrong case
    Then I should see an email wrong alert

  @negative
  Scenario: Login with password having wrong case
    Given I am on the login page
    When I enter password with wrong case
    Then I should see an invalid email message

  @negative
  Scenario: Login with username containing special characters
    Given I am on the login page
    When I enter username with special characters
    Then I should see an enter valid email message

  @negative
  Scenario: Login with password containing special characters
    Given I am on the login page
    When I enter password with special characters
    Then I should see an email password error message

  @negative
  Scenario: Login with a very long username
    Given I am on the login page
    When I enter a very long username
    Then I should see an invalid email message

  @negative
  Scenario: Login with a very long password
    Given I am on the login page
    When I enter a very long password
    Then I should see an email password error message

  @negative
  Scenario: Login with username containing leading and trailing spaces
    Given I am on the login page
    When I enter username with leading and trailing spaces
    Then I should see an invalid email message

  @negative
  Scenario: Login with password containing leading and trailing spaces
    Given I am on the login page
    When I enter password with leading and trailing spaces
    Then I should see an enter valid email message
