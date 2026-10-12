import mysql.connector
#importing the modules to connect to the mysql database

#establishing connection with the database
db = mysql.connector.connect(
    #database created in and hosted by localhost
    host="localhost",
    #default username
    user="root",
    #default is no password
    password="",
    #database name
    database="ae1db"
)

#assigning a variable as the database 'pointer' to select data
cursor = db.cursor()

#sql from section 3 being injected to the code
sql = """
SELECT
    p.FirstName,
    p.LastName,
    s.Degree,
    COUNT(sr.ResourceNumber) AS CompletedResources,
    (SELECT COUNT(*) FROM resources) - COUNT(sr.ResourceNumber) AS RemainingResources,
    g.GradeAverage
FROM students s
JOIN peopleinformation p ON p.UniversityID = s.UniversityID
LEFT JOIN studentresources sr ON sr.UniversityID = s.UniversityID AND sr.CompletionPercent = 100
JOIN grades g ON g.UniversityID = s.UniversityID
GROUP BY s.UniversityID;
"""

#the cursor is then used to send the sql written above into the database to be executed
cursor.execute(sql)

#assigning the outputs from the sql to a new variable
reports = cursor.fetchall()

#iterate through every row of data returned and print it
for row in reports:
    print(row)

#close connection of the cursor then close connection with database
cursor.close()
db.close()
