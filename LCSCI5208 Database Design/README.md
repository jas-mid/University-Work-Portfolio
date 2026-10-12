# LCSCI5208 — Database Design

[← Back to all modules](../README.md)

## Module overview

> In this course, students learn about the design of databases in a relational database management system (RDBMS), from requirements gathering, to conceptual and logical modelling, to implementation – how to organise and query data efficiently (database normalisation and query specification, respectively) in a commercial RDBMS. Advanced topics covered include indexing, query optimisation, transactions, and fundamentals of concurrency and recovery.
>
> — *Official module descriptor*

---

## Skills demonstrated

| Area | Tools & techniques |
| --- | --- |
| **Requirements analysis** | Turning a business brief and its rules into a data model |
| **Conceptual modelling** | Entity-relationship diagrams: entities, a weak entity, composite and derived attributes, and one-to-many and many-to-many relationships |
| **SQL** | Inner and left joins, aggregate functions, subqueries and grouping |
| **Databases from Python** | Running SQL against MySQL with `mysql-connector-python` |

---

## Work

| Work | Description | Stack |
| --- | --- | --- |
| [AE1 — EduConnect Database](AE1%20-%20EduConnect%20Database) | Designing a database for EduConnect, an online learning platform moving off spreadsheets. It covers instructors, students, courses, lessons, assessments, certificates and discussion forums. Includes the final ER diagram and a Python script that runs a report query against the module's MySQL database. | MySQL, SQL, Python |

### AE1 ER diagram

The final entity-relationship diagram, built from the [simplified brief](AE1%20-%20EduConnect%20Database/Brief%20and%20Info/DD-AE1-Brief-Simplified.md) and its eight business rules. It is a conceptual design for the full EduConnect platform, so it models entities and relationships that go beyond the database used in the module.

![EduConnect entity-relationship diagram](AE1%20-%20EduConnect%20Database/Submission%20Outputs/DD-AE1-ER-Diagram.jpg)

---

## Running the work

[`dbquery.py`](AE1%20-%20EduConnect%20Database/dbquery.py) connects to the module's MySQL database (`ae1db`), runs a report of each student's completed and remaining resources and their grade average, then prints the results.

> [!NOTE]
> The `ae1db` database used in the module is no longer available, so it isn't included here. The script is shown for its code: the SQL query and how Python connects to MySQL. It will not run without that database.

```bash
cd "AE1 - EduConnect Database"
python -m venv venv
venv\Scripts\activate        # macOS / Linux: source venv/bin/activate
pip install -r requirements.txt
python dbquery.py
```

### Requirements and dependencies

- **Python 3** with `mysql-connector-python` (in `requirements.txt`). Tested with Python 3.13.
- **A MySQL server** running on `localhost:3306`, for example through XAMPP. The script logs in as `root` with no password, which is the default for a local XAMPP install.
- **The `ae1db` database:** not available (see the note above). The query uses its `students`, `peopleinformation`, `studentresources`, `resources` and `grades` tables.
