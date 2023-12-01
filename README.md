## Authors
Iván Figueredo

Pere Joan Mateu

# Moonefy Api

See the all the [issues](https://github.com/ItsIvanPsk/Monefly-Api/issues) for a full list of proposed features (and known issues) and see the [project](https://github.com/users/ItsIvanPsk/projects/10) kanbam.
### Github

Referring to how GitHub has been used for version control is as follows:

GitFlow has been followed for the development of the project, taking into account the dimensions, the need to have a develop branch has been dismissed, therefore the structure of the repository will be as follows:

- master

- release

- feature/[ISSUE_NUMBER]-[ISSUE_LITTLE_DESCRIPTION].

### Github Project

- User stories have been created in [project kanban](https://github.com/users/ItsIvanPsk/projects/10).
- Each user story has N technical tasks, each with its own issue.

- A [release](https://github.com/ItsIvanPsk/Monefly-Api/releases) has been generated with the name "Vueling First Exam Release v1.0" with the label "v1.0". This will be the final release.

Due to the casuistry of this project I have decided to add ALL the project files, however in a real project, a ".gitignore" should be used to avoid uploading some of the configuration files.

### Custom Exceptions
All custom exceptions created for this program are in the same .Net Core project.

### Resources
The project uses several resource files to obtain data, such as error messages, or date patterns.

### Aspects
I've implemented aspect programation to avoid repeating the same code, since in all the methods the same lines are always repeated, as for example the Logs, and other common functionalities.

**Log Attribute Aspect:** This aspect add the following functionalities:

    Info Log:  with the namespace and the name of the method called
    Debug Log: with all the params, and the param values of the method called.

This project has the name 'VuelingCore.Aspects' because I uncoupled responsabilities and it ables to create a common project to use it in other projects. Such as, the tests. This project ables to send to the testers all necesary project without sending the program code.

### Log4net
I used the Log4Net library for generate logs on all the projects. I make some extra configurations on the basic library configuration to get some extra cases:

The project Log structure are the following:

    📁 Logs
        📁 Info
            📄 InfoLog-dd_MM_yyyy-HH_mm.log
            📄 ...
        📁 Debug
            📄 DebugLog-dd_MM_yyyy-HH_mm.log
            📄 ...

## SOLID Principles

### Single Responsability integration
I have implemented the **SRP** principle, i.e. on the Repository class & the RedisCache class. Both classes has his separate responsabilities.

### Open Close integration
The Open/Close principle has been implemented since, for example, in the Model classes in a predefined way, as they are designed, they implement Open/Close thanks to their interfaces to be able to generate them.

### Liskov substitution integration
The Liskov substitution has been implemented since, for example, in the Factories in a predefined way, as they are designed, they implement Liskov substitution thanks to their interfaces to be able to generate them.

### Dependency Injection
The Dependency injection has been implemented on all the project, I used DI for inject all the necesary dependencies on the entire solution.

## Design Patterns

### YAGNI ('You are not gonna need it')
I have used the YAGNI design pattern, refactored the classes and models to adjust them so that they are invoked only with the necessary data.

### KISS ('Keep It Simple, Idiot')
I have implemented KISS as I have tried to generate methods that are very descriptive and have only the necessary functionalities. Ensuring that the Flow of the program is as simple as possible (without too many nested calls).

### DRY
I have implemented the DRY pattern, all the methods has his own unic functionality.
## Running Tests

To run tests, some explanations:

All the projects has his own Unit Test & Integration Test Projects.

I used **Moq** and **MSTest** to make the tests for all the projects.

### Unit Testing
I used the following name rules on all projects:

    [PROJECT_TO_TEST].Unit.Test

On the CSharp file (Test), are declarated all the necesary content to Test, such as the Loggers and the Mocked Contracts.

### Integration Testing
I used the following name rules on all projects:
    
    [PROJECT_TO_TEST].Integration.Test

On the CSharp file (Test), are declarated all the necesary content to Test, such as the Loggers and the implementation of the tested contracts.

## Tech Stack

    .Net Core
    ASP.NET Core
    AutoMock
    MSTest
    Log4net
    BoundaryAspect.Fody
    Web Api
  
## Authors
Iván Figueredo

Pere Joan Mateu

# Moonefy Api

See the all the [issues](https://github.com/ItsIvanPsk/Monefly-Api/issues) for a full list of proposed features (and known issues) and see the [project](https://github.com/users/ItsIvanPsk/projects/10) kanbam.
### Github

Referring to how GitHub has been used for version control is as follows:

GitFlow has been followed for the development of the project, taking into account the dimensions, the need to have a develop branch has been dismissed, therefore the structure of the repository will be as follows:

- master

- release

- feature/[ISSUE_NUMBER]-[ISSUE_LITTLE_DESCRIPTION].

### Github Project

- User stories have been created in [project kanban](https://github.com/users/ItsIvanPsk/projects/10).
- Each user story has N technical tasks, each with its own issue.

- A [release](https://github.com/ItsIvanPsk/Monefly-Api/releases) has been generated with the name "Vueling First Exam Release v1.0" with the label "v1.0". This will be the final release.

Due to the casuistry of this project I have decided to add ALL the project files, however in a real project, a ".gitignore" should be used to avoid uploading some of the configuration files.

### Custom Exceptions
All custom exceptions created for this program are in the same .Net Core project.

### Resources
The project uses several resource files to obtain data, such as error messages, or date patterns.

### Aspects
I've implemented aspect programation to avoid repeating the same code, since in all the methods the same lines are always repeated, as for example the Logs, and other common functionalities.

**Log Attribute Aspect:** This aspect add the following functionalities:

    Info Log:  with the namespace and the name of the method called
    Debug Log: with all the params, and the param values of the method called.

This project has the name 'VuelingCore.Aspects' because I uncoupled responsabilities and it ables to create a common project to use it in other projects. Such as, the tests. This project ables to send to the testers all necesary project without sending the program code.

### Log4net
I used the Log4Net library for generate logs on all the projects. I make some extra configurations on the basic library configuration to get some extra cases:

The project Log structure are the following:

    📁 Logs
        📁 Info
            📄 InfoLog-dd_MM_yyyy-HH_mm.log
            📄 ...
        📁 Debug
            📄 DebugLog-dd_MM_yyyy-HH_mm.log
            📄 ...

## SOLID Principles

### Single Responsability integration
I have implemented the **SRP** principle, i.e. on the Repository class & the RedisCache class. Both classes has his separate responsabilities.

### Open Close integration
The Open/Close principle has been implemented since, for example, in the Model classes in a predefined way, as they are designed, they implement Open/Close thanks to their interfaces to be able to generate them.

### Liskov substitution integration
The Liskov substitution has been implemented since, for example, in the Factories in a predefined way, as they are designed, they implement Liskov substitution thanks to their interfaces to be able to generate them.

### Dependency Injection
The Dependency injection has been implemented on all the project, I used DI for inject all the necesary dependencies on the entire solution.

## Design Patterns

### YAGNI ('You are not gonna need it')
I have used the YAGNI design pattern, refactored the classes and models to adjust them so that they are invoked only with the necessary data.

### KISS ('Keep It Simple, Idiot')
I have implemented KISS as I have tried to generate methods that are very descriptive and have only the necessary functionalities. Ensuring that the Flow of the program is as simple as possible (without too many nested calls).

### DRY
I have implemented the DRY pattern, all the methods has his own unic functionality.
## Running Tests

To run tests, some explanations:

All the projects has his own Unit Test & Integration Test Projects.

I used **Moq** and **MSTest** to make the tests for all the projects.

### Unit Testing
I used the following name rules on all projects:

    [PROJECT_TO_TEST].Unit.Test

On the CSharp file (Test), are declarated all the necesary content to Test, such as the Loggers and the Mocked Contracts.

### Integration Testing
I used the following name rules on all projects:
    
    [PROJECT_TO_TEST].Integration.Test

On the CSharp file (Test), are declarated all the necesary content to Test, such as the Loggers and the implementation of the tested contracts.

## Tech Stack

    .Net Core
    ASP.NET Core
    AutoMock
    MSTest
    Log4net
    BoundaryAspect.Fody
    Web Api
  
