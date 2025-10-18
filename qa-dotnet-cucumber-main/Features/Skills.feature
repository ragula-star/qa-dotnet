@Skill
Feature: Manage skills
  To manage my profile
  As a logged-in user
  I want to add, update, and delete skills successfully

  Scenario: Add multiple skills
    Given I logged into the portal
    When I click on the Skills tab
    Then I add the following skills:
| Skill          | Skill Level  |
| C#             | Expert       |
| Selenium       | Intermediate |
| SQL            | Expert       |
| Python         | Expert       |
| Manual Testing | Intermediate |
| API Testing    | Intermediate |
| HTML           | Beginner     |
| CSS            | Beginner     |
| JavaScript     | Intermediate |
| Power BI       | Beginner     |


