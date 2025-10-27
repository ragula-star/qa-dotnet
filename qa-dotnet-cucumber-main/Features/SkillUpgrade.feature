Feature: Skill Management

  Ensure clean state before running tests
  Background:
    Given I am logged into the portal and on the Skills tab

  @Positive
  Scenario: Add multiple skills
    When I add the following skills:
| Skill      | Level        |
| Java       | Expert       |
| Python     | Intermediate |
| Selenium   | Expert       |
| C#         | Intermediate |
| SQL        | Expert       |   
| JavaScript | Intermediate |
| HTML       | Beginner     |
| CSS        | Beginner     |
| React      | Intermediate |
| Angular    | Intermediate |
Then all skills should be added successfully

  @Negative
  Scenario: Add skill without name
    When I try to add a skill with "" and "Expert"
    Then I should see error message "Please enter skill and experience level"
    And no new skill should be added

  @Negative
  Scenario: Add skill without level
    When I try to add a skill with "Ruby" and ""
    Then I should see error message "Please enter skill and experience level"
    And no new skill should be added
