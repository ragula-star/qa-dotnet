@SkillDelete
Feature: Delete Skills
  To keep my profile clean
  As a logged-in user
  I want to delete multiple skills at once

  Scenario: Delete all listed skills
    Given I logged into the portal
    When I click on the Skills tab
    Then I delete the following skills:
      | Skill           |
      | C#              |
      | Selenium        |
      | SQL             |
      | Python          |
      | Manual Testing  |
      | API Testing     |
      | HTML            |
      | CSS             |
      | JavaScript      |
      | Power BI        |
