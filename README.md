# a_solid_adventure_into_arch_principles_design_patterns_.NET6_C-10

This repository is based on the book An Atypical ASP.NET Code 6 Design Patterns Guide, by Carl-Hugo Marcotte.

Our objective here, is follow the chapters and the main high-level plan of the book, which is:

1. Explore basic patterns, unit testing, architectural principles, and ASP.NET Core Mechanisms.
2. Explore component scale, through patterns oriented toward small chunks of software and individual units.
3. Explore application-scale patterns and techniques, through higher-level patterns and how to structure an application as a whole.

## Section 1: Principles and Methodologies

### What is a design pattern?

From an abstract definition, design pattern is a proven technique that can be used to solve a specific problem. Take care, well-thought-out applications of design pattern should improve your application design, but throwing patterns into the mix just to use them can lead to an opposite result.

**Avoid over-engineering with as many patterns as you can. Aim to write readable code that solves the issue at hand.**

### What about a anti-patterns and code smells?

**Anti-patterns** is a proven flawed techniques that will cause some trouble and cost time and money.

Some anti-patterns can start as a viable design pattern, but through the development of the project it become a anti-pattern. For instance, **God Class**.

The **God Class** is a well known anti-patterns and it is a class that handles many responsibilities, other classes are in some kind of relation to it. It is the class that knows and manages everything. On the other hand, it is the class that broke the application every time someone touch it.

**The best way to solve that anti-pattern is segregate the responsibilities through multiple small classes.**

**Code smell** is a indicator of a possible problem, it does not mean that there is a problem. An example of this is a method with many comments to explain what it does. It means that the method must be split in small ones more, with proper names, more readable code.

Other examples:

1. **Control Freak**: it is related to the use of the keyword new, which means a hardcoded dependency because the creator controls the new object and its lifetime. To solve this dependency, think about dependency injection.

2. **Long methods**: it is when a method start to extend more than 10 to 15 lines of code. Some causes of a long method, can be:

1.it contains complex logic intertwined in multiple conditional statements.
2.it contains a big switch block
3.it does too many things
4.it contains duplicate code

An alternative to solver those causes can be:

1. extract one or more private methods
2. extract some code to new classes
3. reuse the code from external classes
4. a lot of conditional statements or a huge switch block, use a Chain of responsibility.

### Understand the web - request/response

### Running and building your program

|Command| Description|
|-------|------------|
|dotnet restore| Restore the dependencies (a.k.a. NuGet packages) based on the .csproj or .sln file present in the current dictionary|
|dotnet build| Build the application based on the .csproj or .sln file present in the current dictionary. It implicitly runs the restore command first|
|dotnet run| Run the current application based on the .csproj file present in the current dictionary. It implicitly runs the build and restore commands first |dotnet watch run|Watch for file changes. When a file has changed, the CLI updates the code from that file using the hot-reload feature. When that is impossible, it rebuilds the application and then reruns it (equivalent to executing the run command again). If it is a web application, the page should refresh automatically|
|dotnet test|Run the tests based on the .csproj or .sln file present in the current directory. It implicitly runs the build and restore commands first. We cover testing in the next chapter|
|dotnet watch test| Watch for file changes. When a file has changed, the CLI reruns the tests (equivalent to executing the test command again)| 
|dotnet publish|Publish the current application, based on the .csproj or .sln file present in the current directory, to a directory or remote location, such as a hosting provider. It implicitly runs the build and restore commands first|
|dotnet pack|Create a NuGet package based on the .csproj or .sln file present in the current directory. It implicitly runs the build and restore commands first. You don’t need a .nuspec file|
|dotnet clean| Clean the build(s) output of a project or solution based on the .csproj or .sln file present in the current directory|

### Automated testing

Testing is a integrated part of the development process, and in a CI/CD scenario it becomes crucial. As the application becomes bigger and more complex, tests is a good way to guarantee maintenance.

Tests can be divided in:

1. **unit tests**: are faster to run, faster to write, but are more isolated. They tend to be cheaper. Unit tests focus on individual units, like testing the outcome of a method. Unit tests **should be fast and should not rely on any infrastructure such as a database**. Unit tests should focus on testing algorithms (the ins and outs) and domain logic, not the code itself; how you wrote the code should have no impact on the intent of the test.

2. **integration tests**: Integration tests focus on the interaction between components, such as what happens when a component queries the database or what happens when two components interact with each other. Integration tests often require some infrastructure to interact with, which makes them slower to run.

3. end-to-end tests: slower to execute, lengthier to write, more integration, more expensive. End-to-end tests focus on application-wide behaviors, such as what happens when a user clicks on a specific button, navigates to a particular page, posts a form, or sends a PUT request to some web API endpoint. E2E tests focus on testing the whole application from the user’s perspective, not just part of it, as unit and integration tests do. E2E tests are usually run on actual infrastructure to test your application and your deployment

There are some testing approaches, such as: behavior-driven development (BDD), acceptance test-driven development (ATDD), test-driven development (TDD).The DevOps culture brings a
mindset to the table that focuses on embracing automated testing in line with **its continuous integration (CI) and continuous deployment (CD) ideals**. **CD is really where a robust and healthy suite of tests shine, giving you a high degree of confidence in your code, high enough to deploy the program when all tests pass.**

**TDD is a method** of developing software that states that you should write one or more tests before writing the actual code.

You invert your development flow by following the Red-GreenRefactor technique, which goes like this:
1. You write a failing test (red).
2. You write just enough code to make your test pass (green).
3. You refactor that code to improve the design by ensuring that all of the tests are still passing.

**ATDD is similar to TDD but** focuses on acceptance (or functional) tests instead of software units and involves multiple parties like customers, developers, and testers.

**BDD focuses on formulating test cases around application behaviors** using spoken language and also involves multiple parties like customers, developers, and testers. The given–when–then template defines the way to describe the behavior of a user story or acceptance test, like this:
• Given one or more preconditions (context)
• When something happens (behavior)
• Then one or more observable changes are expected (measurable side effects)

### Refactoring

Refactoring is about (continually) improving the code without changing its behavior. Having an automated test suite should help you achieve that goal and should help you discover when you break something.

### Technical debt

Technical debt represents the corners you cut short while developing a feature or a system. That
happens no matter how hard you try because life is life, and there are delays, deadlines, budgets, and people, including developers.

One way to limit the piling up of technical debt is to refactor the code often. So, factor the refactoring time into your time estimates. Another way is to improve collaboration between all the parties involved. Everyone must work toward the same goal if you want your projects to succeed.

### Testing .NET applications

**dotnet new xunit**. 

For unit testing projects, name the project the same as the project you want to test and append .Tests to it. For example, MyProject would have an associated MyProject.Tests project associated with it.

In xUnit, the [Fact] attribute is the way to create unique test cases, while the [Theory] attribute is the way to make data-driven test cases.

[Fact] attribute example:

public class FactTest
{
    [Fact]
    public void Should_be_equal()
    {
        var expectedValue = 2;
        var actualValue = 2;

        Assert.Equal(expectedValue, actualValue)
    }
}

Or:

public class AsyncFactTest
{
    [Fact]
    public async Task Should_be_equal()
    {
        var expectedValue = 2;
        var actualValue = 2;
        await Task.Yield();

        Assert.Equal(expectedValue, actualValue);
    }
}

Assertions, in xUnit, throws an exception when it fails.

Assert.Equal(expected, actual);
Assert.NotEqual(expected, actual);
Assert.Same(object_1, object_1);
Assert.NotSame(object_1, object_2);
Assert.Equal(object_1, object_2);
Assert.Null(object_1);
Assert.NotNull(object_1);

[Theory] attribute is used for more complex test cases. A theory is defined in two parts:

1. a [Theory] attribute
2. at least of the three following data attributes: [InlineData], [MemberData], [ClassData].

When writing a theory, your primary constraint is to ensure that the number of values matches the number of parameters defined in the test method. For example, a theory with one parameter must be fed with one value.

The [InlineData] attribute is the most suitable for constant values or smaller sets of values. Inline data is the most straightforward way of the three because of the proximity of the test values and the test method

public class InlineDataTest
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(5, 5)]
    public void Should_be_equal(int value1, int value2)
    {
        Assert.Equal(value1, value2);
    }
}

Then, the [MemberData] and [ClassData] attributes can be used to simplify the test method’s
declaration. When it is impossible to instantiate the data in the attribute, reuse the data in multiple test methods, or encapsulate the data away from the test class.
Here is an example of [MemberData] usage:

public class MemberDataTest
{
    public static IEnumerable<object[]> Data => new[]
    {
        new object[] { 1, 2, false },
        new object[] { 2, 2, true },
        new object[] { 3, 3, true },
    };
    
    public static TheoryData<int, int, bool> TypedData =>new TheoryData<int, int, bool>
    {
        { 3, 2, false },
        { 2, 3, false },
        { 5, 5, true },
    };
    
    [Theory]
    [MemberData(nameof(Data))]
    [MemberData(nameof(TypedData))]
    [MemberData(nameof(ExternalData.GetData), 10, MemberType = typeof(ExternalData))]
    [MemberData(nameof(ExternalData.TypedData), MemberType = typeof(ExternalData))]
    public void Should_be_equal(int value1, int value2, bool shouldBeEqual)
    {
        if (shouldBeEqual)
        {
        Assert.Equal(value1, value2);
        }
        else
        {
        Assert.NotEqual(value1, value2);
        }
    }

    public class ExternalData
    {
        public static IEnumerable<object[]> GetData(int start) => new[]
        {
            new object[] { start, start, true },
            new object[] { start, start + 1, false },
            new object[] { start + 1, start + 1, true },
        };
        
        public static TheoryData<int, int, bool> TypedData => new TheoryData<int, int, bool>
        {
            { 20, 30, false },
            { 40, 50, false },
            { 50, 50, true },
        };
    }
}

Last but not least, the [ClassData] attribute gets its data from a class implementing
IEnumerable<object[]> or inheriting from TheoryData<…>. The concept is the same as the other
two. Here is an example:

public class ClassDataTest
{
    [Theory]
    [ClassData(typeof(TheoryDataClass))]
    [ClassData(typeof(TheoryTypedDataClass))]
    public void Should_be_equal(int value1, int value2, bool shouldBeEqual)
    {
        if (shouldBeEqual)
        {
            Assert.Equal(value1, value2);
        }
        else
        {
            Assert.NotEqual(value1, value2);
        }
    }

    public class TheoryDataClass : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 2, false };
            yield return new object[] { 2, 2, true };
            yield return new object[] { 3, 3, true };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public class TheoryTypedDataClass : TheoryData<int, int, bool>
    {
        public TheoryTypedDataClass()
        {
            Add(102, 104, false);
        }
    }
}

**It is important to note that xUnit creates an instance of the test class for every test run, so your dependencies are recreated every time if you are not using the fixtures.**

Instead, xUnit uses existing OOP concepts:
• To set up your tests, use the class constructor.
• To tear down (clean up) your tests, implement IDisposable or IAsyncDisposable and dispose
of your resources there.

#### Arrange, Act, Assert

One well-known method for writing readable tests is Arrange, Act, Assert (AAA or 3A). This technique allows you to clearly define your setup (arrange), the operation under test (act), and your assertions (assert).

[Fact]
public void Should_be_equals()
{
    // Arrange
    var a = 1;
    var b = 2;
    var expectedResult = 3;

    // Act
    var result = a + b;

    // Assert
    Assert.Equal(expectedResult, result);
}

#### Integration tests

Integration tests are harder to organize **because they depend on multiple units and can cross project boundaries and interact with various dependencies**.

#### ASP.NET Core integration testing

#### Important testing principles

**Tests test use cases (features), not the code itself.** if the expected outcome of a feature is correct, that also means the codebase is correct.

**To help with that, the test requirements usually revolve around the inputs and outputs.** The interaction between two components or two systems **should always be tied to a data contract**, whether using a classic request/response model over a REST API where the data contract is the API signature, or using an event-driven architecture approach and the data contract is the event signature or, even simpler, ComponentA returns an object that is injected into  ComponentB; the correctness of those interactions gravitates around the ins and outs.

### Architectural Principles

Fundamental architectural principles, those are the foundation of modern software engineering.

1. SOLID principles
2. The separation of concerns principle
3. DRY principle
4. KISS principle

### The SOLID principles

SOLID extends the basic OOP concepts of Abstraction, Encapsulation, Inheritance, and Polymorphism. It is also important to note that they are principles, not rules to follow at all costs. **Weigh the cost in the context of what you are building**.

The SOLID acronym represents the following:
• Single responsibility principle
• Open/Closed principle
• Liskov substitution principle
• Interface segregation principle
• Dependency inversion principle

**By following these principles, your systems should become easier to test and maintain**.

### Single responsibility principle (SRP)

the SRP means that a single class should hold one, and only one, responsibility, leading
me to the following quote: “There should never be more than one reason for a class to change.”

Software maintainability problems reduce with SRP.

Let’s review why that principle exists:
* **Applications are born to change**.
* To make our classes **more reusable and create more flexible systems**.
* To help **maintain applications**. Since you know the only thing a class does before updating it, you can quickly foresee the impact on the system, unlike with classes that hold many responsibilities, where updating one can break one or more other parts.
* To make **our classes more readable**. Fewer responsibilities lead to less code, and less code
is simpler to visualize in a few seconds, leading to a quicker understanding of that piece of
software.

SRP must not be thinking as an over-separate method because more classes in a system, the more complex to assemble the system can become, and harder it can be to debug, follow execution paths. On the other, well-separated responsibilities should lead to a better and testable system.

To follow a single responsibility, aim at packing a cohesive set of functionalities in a single class that revolves around its responsability.

To indicators of SRP violation:

1. it become harder to name a class; and remember name classes, methods and other elementos in a clear and significant way is very important.
2. a method become too big.

### What is an interface?

It is a powerful tool for creating flexible and maintainable software alike.

* The role of an interface is to define a cohesive contract (public methods, properties, and events). In its theoretical form, there is no code in an interface; it is only a contract. In practice, since C# 8, we can create default implementation in interfaces, which could be helpful to limit breaking changes in a library (such as adding a method to an interface without breaking any class implementing that interface).
* An interface should be small (ISP), and its members should align toward a common goal (cohesion) and share a single responsibility (SRP).
* In C#, a class can implement multiple interfaces, exposing multiples of those public contracts, or, more accurately, be any and all of them. By leveraging polymorphism, a class can be used as any of the interfaces it implements as well as its supertype (if any).

### Open/Closed principle (OCP)

"Software entities (classes, modules, functions, and so on) should be open for extension but closed for modification.” Bertrand Meyer.

Or in other words, you should be able to change the class behaviors from the outside without altering the code itself.

### Composition over inheritance

Composition improves code reuse since multiple classes can use those other smaller
classes. The idea is to have an object use other objects to achieve the correct behaviors instead of inheriting a base class.

Combining composition and dependency injection allows to follow the Open/Close principle.

The first appearance of the OCP, in 1988, was referring to inheritance, and OOP has evolved
a lot since then. You should, most of the time, opt for composition over inheritance.
Inheritance is still a useful concept, but you should be careful when using it; it is a concept
that is easy to misuse, creating direct coupling between classes and deep hierarchy. We
explore that more throughout the book.

### Liskov substitution principle (LSP)

The LSP focuses on preserving subtype behaviors, which leads to system stability. This principle mom linguagem de programação Java.lass, the correctness of the program does not break. 

* Any precondition implemented in a supertype should yield the same outcome in its subtypes, but subtypes can be less strict about it, never more. For example, if a supertype validates that an argument cannot be null, the subtype could remove that validation but not add stricter validation rules.

* Any postcondition implemented in a supertype should yield the same outcome in its subtypes, but aubtypes can be more strict about it, never less. For example, if the supertype never returns null, the subtype should not return null either or risk breaking the consumers of the object that are not testing for null. For example, if the supertype never returns null ,
the subtype should not return null either or risk breaking the consumers of the object that are not testing for null . On the other hand, if the supertype does not guarantee the returned value cannot be null , then a subtype could decide never to return null, making both instances interchangeable.

* Subtypes must preserve the invariance of the supertype. The behaviors of the supertype must not change. For example, a supertype must pass all the tests written for the supertype, so there is no variance between them.

* In your subtypes, add new behaviors, do not change existing ones.

### Interface Segregation Principle (ISP)

“Many client-specific interfaces are better than one general-purpose interface.”

It means:

* create interfaces
* value small interfaces more
* not try to create a multipurpose interface as 'an interface to rule them all'

**ISP is about a smaller cohesive set of functionalities that can be reused and extended**.

The last but not the least, be careful not to overuse this principle blindly. Think about cohesion and what you are trying to build, and not about how granular an interface can blindly become.

### Dependency Inversion Principle (DIP)

"One should “depend upon abstractions, [not] concretions."

Interfaces are one of the pivotal elements of our SOLID arsenal. Moreover, using interfaces is the best way to approach the DIP. Of course, abstract classes are also abstrations, but you should depend on interfaces whenever possible instead.

An abstraction class is an abstraction but is not 100% abstract, and if it is, you should replace it with an interface.

Abstract classes are used to encapsulate default behaviors that you can then inherit in sub-classes. They are helpful, but interfaces are more flexible, more powerful, and better suited to design contracts.1.
Ninja -----------> IWeapon <--------------- Sword.

### Other important principles

* Separation of concerns

    Separate your software into logical blocks, where each block is a concern. Concern can be a significant matter or a tiny detail; nonetheless, it is imperative to consider concerns when dividing your software into pieces to create cohesive units. 

* Don't repeat yourself (DRY)

    When you have duplicated logic in your system, encapsulate it and reuse that new encapsulation in multiple places instead.

* Keep it simple, stupid (KISS)

## Section 2: designing for ASP.NET core

