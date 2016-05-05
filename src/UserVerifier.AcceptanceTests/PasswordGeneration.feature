Feature: Password generation
    In order to verify users identity
    As a product owner
    I want the verifier to generate temporary passwords
    
Scenario: Password is generated
    Given I want to generate password for a test user
    When I call the API to generate password
    Then the password should be generated

Scenario: Different passwords are generated for different users
    Given I generated a password for a user
    When I generated a password for a different user
    Then the password should be different
