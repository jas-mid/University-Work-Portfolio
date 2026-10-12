# LCSCI4208 — Fundamentals of Computer Science II

[← Back to all modules](../README.md)

## Module overview

> Introduces the fundamental ideas of computing and the principles of programming. Discusses a systematic approach to word problems, including analytic reading, synthesis, goal setting, planning, plan execution, and testing. Presents several models of computing, starting from nothing more than expression evaluation in the spirit of high school algebra. No prior programming experience is assumed; therefore, suitable for freshman students, majors and non majors alike who wish to explore the intellectual ideas in the discipline.
>
> — *Official module descriptor*

---

## Skills demonstrated

| Area | Tools & techniques |
| --- | --- |
| **Java programming** | Java 21 and Maven: completing partially built applications across several classes |
| **Data structures** | An image stored as a list of pixel columns with a stack of image states for undo (AE2), and an image stored as linked pixels, so a seam can be removed or reinserted in O(n) time (AE3) |
| **Algorithms** | Dynamic programming to find the greenest and lowest-energy seams, and an energy calculation based on the brightness of each pixel's neighbours (AE3) |
| **Design patterns** | The Command pattern, with each user action as its own command class so it can be undone (AE3) |
| **Image processing** | Reading and writing PNG files with `ImageIO`, and highlighting, removing and reinserting pixel columns and seams |
| **Command-line interfaces** | `Scanner`-based menus with confirmation steps and input validation |
| **Collaboration** | Working in a group of three through a shared GitHub repository (AE3) |

---

## Work

| Work | Description | Stack |
| --- | --- | --- |
| [AE2 — Image Column Remover](Assessments/AE2%20-%20Image%20Column%20Remover) | Individual project. A command-line editor that shrinks PNG images by removing columns of pixels. It can remove the greenest column or a chosen column, highlighting it in red for confirmation first, and undo previous changes. Includes the [simplified brief](Assessments/AE2%20-%20Image%20Column%20Remover/Brief%20and%20Info/FCS2-AE2-Brief-Simplified.md). | Java 21, Maven |
| [AE3 — Image Seam Remover](Assessments/AE3%20-%20Image%20Seam%20Remover) | Group project with [@tw41158](https://github.com/tw41158) and [@DheerB2303](https://github.com/DheerB2303) ([group repository](https://github.com/tw41158/AE3-Java)). Extends AE2 to remove seams instead of columns: connected paths of pixels from top to bottom, chosen as the greenest seam or the seam with the lowest energy, with highlighting, confirmation and multi-step undo. Includes the [simplified brief](Assessments/AE3%20-%20Image%20Seam%20Remover/Brief%20and%20Info/FCS2-AE3-Brief-Simplified.md). | Java 21, Maven |

> [!IMPORTANT]
> **Both assessments were built on starting code provided by the module.** Code that comes from the starting code looks different from the rest: blocks are wrapped in `// ===== STARTING CODE (provided by the module) =====` and `// ===== END OF STARTING CODE =====` comments, and single lines end with `// <- starting code`. These markers are comments only and do not change how the programs run.
>
> - **AE2 was an individual project**, so everything without a marker is my own work.
> - **AE3 was a group project**, so everything without a marker was written by the group of three: me, [Tom Woodford (@tw41158)](https://github.com/tw41158) and [@DheerB2303](https://github.com/DheerB2303). The group's original shared repository, with each member's commits, is [tw41158/AE3-Java](https://github.com/tw41158/AE3-Java).
>
> The set-up files are entirely from the starting code and have no markers: `pom.xml`, `.gitignore`, the sample images in `src/main/resources` and the approved test snapshots (`AppTest.*.approved.*`). AE2 also has the starting code's `checkstyle.xml`, `checkstyle-suppressions.xml` and `newImg.png`.

---

## Running the work

Each assessment is a separate Maven project. Run either one from its code folder:

```bash
cd "Assessments/AE2 - Image Column Remover/AE2 code"     # or "Assessments/AE3 - Image Seam Remover/AE3 code"
mvn compile
java -cp target/classes uk.ac.nulondon.Main
```

When asked for a file path, enter one of the sample images, for example `src/main/resources/beach.png`. Edited images are saved in the `target` folder.

- **AE2:** use `g` to remove the greenest column, `r` followed by a number (such as `r5`) to remove a specific column, `d` to confirm, `u` to undo and `q` to quit.
- **AE3:** use `g` to remove the greenest seam or `e` to remove the lowest-energy seam, `y` to confirm, `u` to undo and `q` to quit.

To run the tests:

```bash
mvn test
```

> [!NOTE]
> In AE2's submitted version, `removeColumn` removes the column but returns `null` instead of the removed pixels. The starting code's `removeColumn` test expects the removed pixels, so that test fails.

### Requirements and dependencies

- **Java 21** and **Maven**. Maven downloads the test libraries (JUnit 5, AssertJ and ApprovalTests) automatically.
- Run the commands from inside the assessment's code folder, because the programs read the sample images and save their output using paths relative to that folder.
