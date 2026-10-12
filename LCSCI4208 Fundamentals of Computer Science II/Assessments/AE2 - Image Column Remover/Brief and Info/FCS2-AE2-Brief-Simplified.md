# Fundamentals of Computer Science II AE2: Assessment Brief (Simplified)

Individual Coursework

> [!NOTE]
> **About the code in this assessment:** this assessment was built on starting code provided by the module. In the files in [`AE2 code`](../AE2%20code), every part that comes from that starting code is marked: blocks are wrapped in `// ===== STARTING CODE (provided by the module) =====` and `// ===== END OF STARTING CODE =====` comments, and single lines end with `// <- starting code`. Everything without a marker is my own work.

## Assessment Task

Have you ever been unable to send an image because it was too large to export? In this assignment, you will be reducing the size of colour images stored in PNG format by removing columns of pixels.

You will write a program in Java that reduces the size of an image horizontally by implementing an image manipulation system. The program will allow users to highlight, remove, and insert columns in an image, while providing undo functionality.

## Overview

You are provided with a partially implemented Java application structured into multiple classes:

- **Image.java** - Represents an image as a collection of colour columns. Several key methods are incomplete and require implementation.
- **ImageEditor.java** - Provides functionality for modifying image columns and includes placeholder methods to be completed.
- **Main.java** - Implements a command-line interface for user interactions. Some functions require completion.
- **AppTest.java** - Contains unit tests to verify the correctness of your implementation. The tests guide you in implementing missing functionality

## Program Functionality

The program should allow users to:

- Remove the greenest column (g) – Identifies and highlights the column with the highest sum of green values before removal.
- Remove a specific column (r[number]) – Highlights a user-specified column before removal.
- Confirm deletion (d) – Removes a highlighted column from the image.
- Undo previous deletion (u) – Restores the most recently removed column.
- Exit the program (q).

## Implementation Tasks

Your task is to complete the missing parts of the provided code while ensuring correctness and functionality:

### Complete the Methods in Image.java

- Implement `highlightColumn(int i)` to highlight a specified column.
- Implement `removeColumn(int i)` to remove a column and return the removed colors.
- Implement `addColumn(int index, List<Color> column)` to insert a column at a given index.
- Implement `getGreenest()` to determine the column with the highest sum of green values.

### Implement Missing Logic in Main.java

- Complete `removeGreenest(Scanner scan)` to remove the greenest column.
- Complete `removeSpecific(Scanner scan, String choice)` to remove a user-specified column.
- Complete `undo(Scanner scan)` to restore the last removed column.

### Ensure Passing Tests in AppTest.java

The provided unit tests serve as a guide for implementing the missing functionality. Ensure all tests pass successfully before submission.

### User Experience

- Improve error handling to prevent crashes due to invalid inputs.
- Display clearer messages for user actions.
