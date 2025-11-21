@EducationUpgrade
Feature: Education Upgrade

  As a user
  I want to add my education in my profile
  So that profile viewers can see my qualifications

  Background:
    Given I am logged into the portal
    Given I navigate to the Education tab 

  Scenario: Add all valid and destructive education entries
    Given I am on the Education tab
    When I delete all education entries
    When I add all education entries from JSON except empty fields
    Then I should see all added universities in my profile

 
  Scenario: Add empty education fields
    Given I am on the Education tab
    When I add empty education entries from JSON
    Then I should see validation error messages for empty fields

  
  Scenario: Edit last education entry
    Given I am on the Education tab
    When I edit the last education entry with JSON data "edit_test"
    Then I should see the updated university in my profile

 
  

