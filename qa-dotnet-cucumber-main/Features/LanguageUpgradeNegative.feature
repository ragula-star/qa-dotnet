@LanguageUpgradeNegative
Feature: Language management negative tests

  Scenario Outline: Try to add a language with invalid or empty data
    Given I am logged into the portal and on the Language tab for negative tests
    When I try to add a language with "<language>" and "<level>"
     Then I should see error message "Please enter language and level"
    Then no new language should be added

    Examples:
      | language | level   |
      |          |         |  
      | Spanish  |         |  
      |          | Fluent  |  

