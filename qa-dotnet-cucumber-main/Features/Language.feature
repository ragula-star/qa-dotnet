@Language
Feature: Manage Languages
  To manage my profile
  As a logged-in user
  I want to add, update, and delete languages successfully

  Scenario: Add multiple languages
    Given I logged into the portal
    When I click on the Languages tab
    And I add the following languages:
      | Language | Level           |
      | English  | Fluent          |
      | French   | Basic           |
      | Hindi    | Conversational  |
      | Spanish  | Native/Bilingual|
    Then I should see all languages listed
