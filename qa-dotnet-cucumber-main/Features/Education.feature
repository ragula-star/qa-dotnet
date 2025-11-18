@Education
Feature: Manage Education
  As a logged-in user
  I want to manage my education details
  So that my profile is accurate

   @PositiveScenario
  Scenario: Reset and add multiple education records
    Given I am logged into the portal
    And I navigate to the Education tab
    And I delete all existing education records
    When I add the following Education
      | university Name | country of College\University | title  | Degree | Year of graduate |
      | Anna            | Afghanistan                   | BArch  | PG     | 2020             |
      | Abc             | Algeria                       | B.Tech | UG     | 2014             |
    Then all education entries should be added successfully

   @NegativeScenarios
  Scenario: Add education with missing required fields
    Given I am logged into the portal
    And I navigate to the Education tab
    When I attempt to add Education with missing required fields
      | university Name | country of College\University | title  | Degree | Year of graduate |
      |                 | Algeria                     | B.Tech | UG     | 2014             |
    Then an error message should be displayed for missing required fields

  Scenario: Add education with all fields empty
    Given I am logged into the portal
    And I navigate to the Education tab
    When I attempt to add Education without filling any fields
      | university Name | country of College\University | title | Degree | Year of graduate |
      |                 |                               |       |        |                  |
    Then an error message should be displayed for missing or empty fields

  Scenario: Add education with invalid characters
    Given I am logged into the portal
    And I navigate to the Education tab
    When I attempt to add Education with special characters
      | university Name        | country of College\University | title  | Degree | Year of graduate |
      | @@@!!!###$$$%%%        | Denmark                      | B.Tech | UG     | 2022             |
    Then all invalid educations entries should be added successfully

    @DestructiveTesting
  Scenario: Add education with extremely long input
    Given I am logged into the portal
    And I navigate to the Education tab
    When I attempt to add Education with extremely long input
      | university Name | country of College\University | title  | Degree | Year of graduate |
      | AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA | Denmark | B.Tech | UG | 2022 |
    Then all destructive education entries should be added successfully

  Scenario: Add duplicate education record
    Given I am logged into the portal
    And I navigate to the Education tab
    When I add the following Education
      | university Name | country of College\University | title  | Degree | Year of graduate |
      | Anna            | Afghanistan                 | BArch   | PG     | 2020             |
    And I attempt to add the same education record again
      | university Name | country of College\University | title  | Degree | Year of graduate |
      | Anna            | Afghanistan                 | BArch   | PG     | 2020             |
    Then an error message for duplicate entry should be displayed
