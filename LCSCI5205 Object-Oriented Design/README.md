# LCSCI5205 — Object-Oriented Design

[← Back to all modules](../README.md)

## Module overview

> This course fosters a deeper understanding of the concepts and practice of object-oriented programming and design, including polymorphism, software reuse, and software design patterns. The course equips students with practical program design skills via case studies in modern software libraries and hands-on coding sessions in one or more object-oriented languages.
>
> — *Official module descriptor*

---

## Skills demonstrated

| Area | Tools & techniques |
| --- | --- |
| **Object-oriented programming** | C# and .NET: classes, objects, constructors and lists of objects |
| **Abstraction and inheritance** | Abstract base classes extended by specialised types: `Product` into `Pet` and `Item` (AE1), and `ParkingSpots` into regular, disabled and electric vehicle spots (AE2) |
| **Polymorphism** | Overridden methods so each type behaves in its own way, such as `ProductInfo()` (AE1) and each parking spot's availability methods (AE2) |
| **Interfaces** | `ILogin` and `IPurchasing` (AE1), and `ISubject` and `IObserver` (AE2) |
| **Design patterns** | Singleton: one shared `ParkingSystem` instance. Observer: each parking spot notifies the parking system when it is taken or freed (AE2) |
| **Encapsulation** | Private fields for sensitive data, with employee passwords stored only as hashes (AE1) |
| **Design documentation** | UML class diagrams and condensed written reports explaining where each OOP concept and design pattern appears in the code |

---

## Work

| Work | Description | Stack |
| --- | --- | --- |
| [AE1 — Pet Store System](AE1%20-%20Pet%20Store%20System) | A console app for a pet store. Customers can browse pets and items, register and make purchases, and there is an employee section with a login screen. Includes the [simplified brief](AE1%20-%20Pet%20Store%20System/Brief%20and%20Info/OOD-AE1-Brief-Simplified.md), a [condensed code report](AE1%20-%20Pet%20Store%20System/Submission%20Outputs/OOD-AE1-Code-Report-Condensed.md) and a UML diagram. | C#, .NET 8 |
| [AE2 — Smart Parking System](AE2%20-%20Smart%20Parking%20System) | Group project. A console app that simulates a smart car park with regular, disabled and electric vehicle spaces, built around the Singleton and Observer design patterns. Includes the [simplified brief](AE2%20-%20Smart%20Parking%20System/Brief%20and%20Info/OOD-AE2-Brief-Simplified.md), a [condensed code report](AE2%20-%20Smart%20Parking%20System/Submission%20Outputs/OOD-AE2-Code-Report-Condensed.txt) and a UML diagram. | C#, .NET 8 |

### AE1 UML diagram

![Pet store UML class diagram](AE1%20-%20Pet%20Store%20System/Submission%20Outputs/OOD-AE1-UML-Diagram.jpg)

### AE2 UML diagram

![Smart parking system UML class diagram](AE2%20-%20Smart%20Parking%20System/Submission%20Outputs/OOD-AE2-UML-Diagram.png)

---

## Running the work

Each assessment is a separate C# project. Run either one from its code folder:

```bash
cd "AE1 - Pet Store System/AE1 code"        # or "AE2 - Smart Parking System/AE2 code"
dotnet run
```

- **AE1:** choose options from the numbered menu, and enter `6` to close the app.
- **AE2:** choose `1` to park or `2` to leave a space, and `3` to quit.

> [!NOTE]
> In AE1, the employee login screen is not connected up in the submitted version. The `Employee` class has a `Login()` method that checks the ID and hashed password, but the menu in `Program.cs` never calls it, so entering an ID and password asks for them again rather than logging in.

### Requirements and dependencies

- **.NET 8 SDK**, or a newer .NET SDK with the .NET 8 runtime installed. Tested with .NET SDK 9 and the .NET 8 runtime.
- No other packages, files or internet access are needed. All the data each app uses is set up in its `Program.cs`.
