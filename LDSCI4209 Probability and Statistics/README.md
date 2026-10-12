# LDSCI4209 — Probability and Statistics

[← Back to all modules](../README.md)

## Module overview

> This course is an introduction to probability and statistics that covers the material primarily from a practical perspective, although it adopts a more formal approach for certain aspects. Probability is the mathematical study of uncertainty and chance, and in addition to being worthy of study in its own right, it is also deeply linked to statistics, which concerns the analysis and interpretation of data. Students will learn applications of statistical methods, such as hypothesis testing, in the context of data and social sciences.
>
> — *Official module descriptor*

---

## Skills demonstrated

| Area | Tools & techniques |
| --- | --- |
| **Statistical programming** | R, using base R functions only |
| **Data visualisation** | Histograms with custom bin boundaries, labelled bar charts and kernel density plots |
| **Hypothesis testing** | A two-tailed one-sample t-test, worked through step by step from the t statistic and degrees of freedom to the p-value |
| **Estimation** | A 99% confidence interval for a mean, built from the t distribution |
| **Tests of association** | A chi-squared test of association on a contingency table |
| **Simulation** | Demonstrating the Central Limit Theorem by simulating sample means from an Exponential distribution |

---

## Work

| Work | Description | Stack |
| --- | --- | --- |
| [AE1 — Set Exercises](Assessments/AE1%20-%20Set%20Exercises) | Statistics exercises answered in R: a hypothesis test, confidence interval and histogram for chocolate bar masses (A.1), a bar chart of student subjects (A.2), a chi-squared test of favourite colour against favourite drink (A.3), and a Central Limit Theorem simulation (B.2). Includes the [simplified brief](Assessments/AE1%20-%20Set%20Exercises/Brief%20and%20Info/PS-AE1-Brief-Simplified.md). | R |

Each script in [`AE1 code`](Assessments/AE1%20-%20Set%20Exercises/AE1%20code) answers one part of the brief, and its file name starts with the question it answers:

| Script | Question |
| --- | --- |
| `a1_mass_histogram.R` | A.1(a): histogram of the bar masses, with 6 bins |
| `a1_hypothesis_test.R` | A.1(b)(i): t-test of whether the mean mass is 100g |
| `a1_confidence_interval.R` | A.1(b)(ii): 99% confidence interval for the mean mass |
| `a2_course_barchart.R` | A.2: bar chart of students by subject |
| `a3_chisq_colours_drinks.R` | A.3: chi-squared test of association |
| `b2_clt_simulation.R` | B.2: Central Limit Theorem simulation |

---

## Running the work

Open a script in RStudio and run it, or run it from a terminal:

```bash
cd "Assessments/AE1 - Set Exercises/AE1 code"
Rscript a1_hypothesis_test.R
```

When run with `Rscript`, plots are saved to `Rplots.pdf` in the same folder instead of opening in a window.

### Requirements and dependencies

- **R 4.x.** The scripts only use functions built into R, so no packages need installing.
- No data files are needed. The brief refers to a `red77.txt` file, but the 40 bar masses are typed directly into the A.1 scripts.
