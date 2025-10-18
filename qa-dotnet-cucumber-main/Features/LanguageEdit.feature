@LanguageEdit
Feature: Edit an existing language
  To manage my profile
  As a logged-in user
  I want to edit existing languages successfully

  Scenario: Edit an existing language
    Given I logged into the portal
    When I click on the Languages tab
    And I edit the language "English" to have level "Native/Bilingual"
    Then I should see "English" listed with level "Native/Bilingual"

    Scenario Outline: Check, edit and delete a language 
    Given I logged into the portal 
    When I click on the Languages tab
    And I should see "<Language>" listed with "<Level>"
    When I edit the language "<Language>" to have level "<NewLevel>"
    Then I should see "<Language>" listed with "<NewLevel>"
    Then I delete the "<Language>"
    Then I should not see "<Language>" in my language list

    Examples:
    |Language  |Level  |NewLevel  |
    |French    |Basic  |Conversational | 