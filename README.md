# SchoolBackoffice
School Backoffice - application written in ASP.NET Core - MVC model

I've decided to write application using MVC model because it's the most similar from the 3 options to the projects I'm writing daily in Java.

### Project contains:
- CRUD for Teachers and Lessons
- relation one to many between Teacher and Lessons:
  - there is no possibility to add lesson if list of teachers is empty
- Authentication - only GET methods are allowed for anonymous
