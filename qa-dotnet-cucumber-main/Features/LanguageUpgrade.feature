@LanguageUpgrade
Feature: Language management

  Scenario Outline: Add multiple languages
    Given I am logged into the portal and on the Language tab
    When I add a new language "<language>" with level "<level>"
    Then I should see "<language>" in the language table

    Examples:
      | language | level          |
      | Spanish  | Native/Bilingual|
      | English  | Fluent         |
      | French   | Basic          |
      | Hindi    | Fluent         |
     


