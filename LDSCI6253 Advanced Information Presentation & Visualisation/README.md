# LDSCI6253 — Advanced Information Presentation & Visualisation

[← Back to all modules](../README.md)

## Module overview

> This course introduces foundational principles, methods, and techniques of visualisation to enable the creation of effective information representations suitable for exploration and discovery. The course covers the design and evaluation process of visualisation creation, visual representations of data, relevant principles of human perception and cognition, and basic interactivity principles. It emphasises good programming practices for both static and interactive visualisations. Finally, the course requires extensive writing, including documentation, explanations, and discussions of the findings from the data analyses and the visualisations.
>
> — *Official module descriptor*

---

## Skills demonstrated

| Area | Tools & techniques |
| --- | --- |
| **Data visualisation** | Python, Jupyter, Altair (Vega-Lite grammar), pandas |
| **Visualisation design** | Data and task abstraction, visual encoding channels and marks, the nested model of visualisation design |
| **Colour** | Choosing sequential, diverging and categorical palettes for the data and the task |
| **Critique** | Evaluating published visualisations and suggesting improvements |

---

## Work

| Work | Description | Stack |
| --- | --- | --- |
| [Lab 01 — Simple Visualisations](Labs/Lab%2001%20-%20Simple%20Visualisations) | First charts built from London housing and relative poverty datasets, plus a written critique of a published heatwave visualisation. | Python, Jupyter |
| [Lab 02 — Visual Communication](Labs/Lab%2002%20-%20Visual%20Communication) | Data types, encoding channels and graphical marks. | Altair |
| [Lab 03 — Data Abstraction](Labs/Lab%2003%20-%20Data%20Abstraction) | Choosing the right chart for a given task abstraction. | Altair |
| [Lab 04 — Using Colour](Labs/Lab%2004%20-%20Using%20Colour) | Picking sequential, diverging and categorical palettes to suit the data and the task. | Altair, pandas |

---

## Running the work

```bash
pip install jupyter pandas altair
jupyter notebook
```

Open each `.ipynb` file from inside its own lab folder so the relative paths to `Data & Resources/` work. GitHub also shows notebooks in the browser, so you can see the outputs without running anything.
