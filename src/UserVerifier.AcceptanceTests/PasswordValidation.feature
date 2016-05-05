Feature: Password validation
    In order to verify users identity
    As a product owner
    I want the verifier to validate temporary passwords

Scenario: Correct password is validated before expiration
    Given I generated a password for a user
    When I call the API to validate this password
    Then the password should be validated succesfully

Scenario: Incorrect password is not validated
    Given I generated a password for a user
    When I call the API to validate different password
    Then the password should not be validated

Scenario: Password is not validated when it was not generated
    Given I don't generate password for a user
    When I call the API to validate different password
    Then the password should not be validated

Scenario: Correct password is expired
    Given I generated a password for a user
    When I call the API to validate this password after expiration time
    Then the password should not be validated
