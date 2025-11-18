@Certifications
Feature: Manage Certifications
  As a user
  I want to add, view, and delete my certifications
  So that my profile is up to date

  Background:
    Given I am logged into the portal
    And I navigate to the Certifications tab

  # Positive Scenario: Add a valid certification
  Scenario: Add a new certification successfully
    When I add the following certification
      | Certificate Name         | Certified From | Year |
      | AWS Certified Developer  | Amazon         | 2025 |
    Then the certification should be added successfully

  # Positive Scenario: Add multiple certifications
  Scenario: Add multiple certifications successfully
    When I add the following certifications
      | Certificate Name        | Certified From | Year |
      | Azure Fundamentals      | Microsoft      | 2024 |
      | Google Cloud Engineer   | Google         | 2023 |
    Then all certifications should be added successfully

    Scenario: Edit an existing certification
  When I add the following certification
    | Certificate Name        | Certified From | Year |
    | AWS Certified Developer | Amazon         | 2025 |
  And I edit the certification to the following values
    | Certificate Name      | Certified From | Year |
    | AWS Architect Expert  | Amazon         | 2026 |
  Then the certification should be updated successfully


  # Negative Scenario: Add duplicate certification
  Scenario: Add duplicate certification
    When I add the following certification
      | Certificate Name         | Certified From | Year |
      | AWS Certified Developer  | Amazon         | 2025 |
    And I attempt to add the same certification again
    Then the portal should show an error for duplicate certification entry

  # Negative Scenario: Add certification with empty fields
  Scenario: Add certification with missing required fields
    When I attempt to add a certification without filling any fields
      | Certificate Name | Certified From | Year |
      |                 |               |      |
    Then the portal should show an error for missing certification fields

  # Scenario: Add certification with invalid characters
  Scenario: Add certification with special characters
    When I attempt to add a certification with invalid characters
      | Certificate Name | Certified From | Year |
      | 22@!!!###        | $$$%%%         | 2025 |
    Then the portal should show an error for invalid characters in certification
    Then the certification should be added successfully in portal
