3️⃣ BookStore MVC – Razor Pages + MVC
| Step Number | Item | Requirement |
|:----|:-----|:------------|
| 1 | Project | dotnet new mvc -n BookStore |
| 2 | Model |	Book (Id:int, Title:string, Author:string, Price:decimal) with DataAnnotations: [Required], [Range] |
| 3 | Controller |	BooksController with actions: Index, Details, Create, Edit, Delete |
| 4 | Views |	Razor files under Views/Books/ for each action (use layout, partial for form) |
| 5 | Validation |	Client‑side + server‑side validation; show validation summary |
| 6 | Filters |	ActionFilter that logs execution time (inject ILogger) |
| 7 | Areas |	Admin area (for CRUD) & Customer area (read‑only) |
| 8 | Dependency Injection |	Register a IBookRepository interface and InMemoryBookRepository (for dev) |
| 9 | Unit Tests |	Controller unit tests with Moq for IBookRepository |
| 10 | Readme |	How to run, add dummy data, test admin area |
| 11 | Folder |	Areas/Admin/Controllers/, Areas/Admin/Views/ etc. |



What to do next :
1. Test the APIs via postman
2. create view
