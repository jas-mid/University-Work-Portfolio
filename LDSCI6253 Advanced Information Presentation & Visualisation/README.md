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

Each lab is self-contained and has its own `requirements.txt`, so any lab can be downloaded and run on its own. GitHub also shows notebooks in the browser, so you can see the outputs without running anything.

```bash
cd "Labs/Lab 01 - Simple Visualisations"   # or any other lab folder
python -m venv venv
venv\Scripts\activate                      # macOS / Linux: source venv/bin/activate
pip install -r requirements.txt
jupyter notebook
```

Start Jupyter from inside the lab folder so that the notebook can find its data files. Tested with Python 3.13.

| Lab | Libraries | Files it needs | Internet needed? |
| --- | --- | --- | --- |
| Lab 01 | pandas, matplotlib | `Data & Resources/london_new_home_buyers-1.csv`, `Data & Resources/relative_poverty-1.csv` | No |
| Lab 02 | pandas, NumPy, Altair, vega_datasets | None | Yes: the `movies` dataset is downloaded |
| Lab 03 | pandas, Altair, vega_datasets | None | Yes: the `population`, `unemployment_across_industries` and `flights_5k` datasets are downloaded |
| Lab 04 | pandas, NumPy, Altair, vega_datasets | `lab_toolkit.py`, the module's helper file, which must stay next to the notebook | No |

> [!NOTE]
> Lab 04 is in progress. Its setup cells run, but the colour challenges have not been completed yet, so the cells after them will show errors until they are filled in.
