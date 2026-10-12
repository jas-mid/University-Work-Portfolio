# LDSCI5207 — Experimental Data Science Project

[← Back to all modules](../README.md)

## Module overview

> The LDSCI5207 Experimental Data Science Project is designed for you to apply your data science and programming skills to solve a local problem within London, UK. The problem you choose should have societal impact or be aimed at improving conditions for the residents of the area, helping them become more informed citizens or highlighting opportunities for positive change.
>
> Your project should focus on how data science can address a real-world challenge that affects the local community, such as housing, transportation, healthcare, education, air pollution, or public services. Your analysis should aim to provide insights or solutions that could lead to improved quality of life, better decision-making, or a deeper understanding of an issue.
>
> — *Official module descriptor*

The whole module was a single project. This folder contains the version I built and submitted for assessment.

---

## The project: East London Council Comparison App

A multi-page Streamlit web app that helps residents of East London see how their council is performing, and gives them the means to act on it.

- **Statistics Comparison Tool:** compare seven East London boroughs (Barking & Dagenham, Hackney, Havering, Newham, Redbridge, Tower Hamlets and Waltham Forest) against each other and the Greater London average. Each metric is something the council has direct control over, such as low traffic neighbourhoods, 20 mph roads, protected cycle track, school streets, council debt per resident and council staffing. All metrics are shown as interactive Plotly bar charts.
- **Contacting Your Council:** links each borough to its council's "contact us" page and its voter registration page, so residents can get their voice heard.

**Why:** only 34% of people in the UK trust their local government ([ONS, 2023](https://www.ons.gov.uk/peoplepopulationandcommunity/wellbeing/bulletins/trustingovernmentuk/2023)). Putting a council's performance next to similar councils shows residents where it could do better.

---

## Skills demonstrated

| Area | Tools & techniques |
| --- | --- |
| **Data sourcing** | Finding and combining public datasets from the London Datastore and the Office for National Statistics |
| **Data cleaning** | pandas: standardising column names, converting percentage and comma-formatted strings, filtering to London boroughs, merging three datasets on a shared key |
| **Analysis** | Per-capita normalisation so boroughs of different sizes can be compared fairly |
| **Web app development** | Streamlit multi-page app with sidebar filters and page navigation |
| **Data visualisation** | Interactive Plotly bar charts with hover values and zoom |
| **Testing** | pytest unit tests for the cleaning, merging and link functions (29 tests) |
| **Version control** | Git and GitHub throughout development |

---

## Where it went next

After the module ended, I kept developing the app as a standalone project: **[East London Council Tracker](https://github.com/jas-mid/East-London-Council-Tracker)**. It takes the academic version further with:

- a restructured Python package with a data-driven council registry
- live data from the ONS Explore Local Statistics API
- automatic health checks for every council link
- a larger test suite (41 tests)
- an MIT licence and a data sources page

The code in this folder is kept exactly as submitted, so the two can be compared.

---

## Running the submitted version

```bash
cd "EDSP Project"
python -m venv venv
venv\Scripts\activate        # macOS / Linux: source venv/bin/activate
pip install -r requirements.txt
streamlit run app.py
```

To run the tests:

```bash
pytest test_sample.py
```

---

## Data sources

The data belongs to the organisations that publish it:

- **London Datastore** (Greater London Authority): safe streets measures for each borough
- **Office for National Statistics:** mid-year population estimates. Contains public sector information licensed under the [Open Government Licence v3.0](https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/).
- **Council websites:** contact and voting pages are linked to, not copied

The test suite in `test_sample.py` was generated with GitHub Copilot, as noted in the file.
