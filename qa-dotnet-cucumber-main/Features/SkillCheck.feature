@SkillCheck
Feature: Check Skills
  To manage my profile
  As a logged-in user
  I want to verify my skills are listed correctly

  Scenario: Verify all skills are present
    Given I logged into the portal
    When I click on the Skills tab
     Then I should see skill "C#" listed with "Expert"
    Then I should see skill "Selenium" listed with "Intermediate"
    Then I should see skill "SQL" listed with "Expert"
    Then I should see skill "Python" listed with "Expert"
    Then I should see skill "Manual Testing" listed with "Intermediate"  
    Then I should see skill "API Testing" listed with "Intermediate"
    Then I should see skill "HTML" listed with "Beginner"
    Then I should see skill "CSS" listed with "Beginner"
    Then I should see skill "JavaScript" listed with "Intermediate"
    Then I should see skill "Power BI" listed with "Beginner"
