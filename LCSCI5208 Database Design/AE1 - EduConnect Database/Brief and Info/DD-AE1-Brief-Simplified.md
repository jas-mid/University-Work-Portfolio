# Database Design AE1: Assessment Brief (Simplified)

Individual Coursework

## Assessment Task

### The setting

EduConnect is a growing online learning platform that wants to redesign their database system. Currently, they store all data in spreadsheets, but rapid growth has made this approach inefficient and error-prone. You have been hired as a database consultant to design and implement a robust database solution.

The platform needs to manage:

- Instructors who create and teach courses
- Students who enroll in multiple courses
- Courses with different difficulty levels and categories
- Lessons within each course (video, text, or interactive content)
- Student progress tracking through lessons
- Assessments (quizzes, assignments) with submissions and grades
- Discussion forums for each course with posts and replies

### Business Rules

1. Each instructor can create multiple courses, but each course has only one primary instructor
2. Students can enroll in multiple courses, and courses can have multiple students
3. Each course contains multiple lessons arranged in a specific sequence
4. Students must complete lessons in order within a course
5. Each lesson can have multiple assessments, but each assessment belongs to only one lesson
6. Students can submit multiple attempts for assessments, but only the latest grade counts
7. Each course has one discussion forum, with multiple posts and nested replies
8. Students receive certificates only after completing all lessons and achieving 70% average on assessments
