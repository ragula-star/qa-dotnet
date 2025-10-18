@SkillUpdate
Feature: Update Skills
  To manage my profile
  As a logged-in user
  I want to update my skills levels

  Scenario Outline: Update skill levels
    Given I logged into the portal
    When I click on the Skills tab
    And I edit the skill "<Skill>" to have level "<NewLevel>"
    Then I should see skill "<Skill>" listed with "<NewLevel>"

    Examples:
      | Skill           | NewLevel    |
      | C#              | Intermediate|
      | Selenium        | Expert      |
      | SQL             | Expert      |
      | Python          | Intermediate|
      | Manual Testing  | Expert      |
      | API Testing     | Expert      |
      | HTML            | Intermediate|
      | CSS             | Intermediate|
      | JavaScript      | Expert      |
      | Power BI        | Intermediate|
