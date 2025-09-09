# START

3️⃣ BookStore MVC – Razor Pages + MVC
| Step Number | Item | Requirement |
|:----|:-----|:------------|
| 1 | Project | dotnet new mvc -n BookStore |
| 2 | Model |	Book (Id:int, Title:string, Author:string, Price:decimal) with DataAnnotations: [Required], [Range] |
| 3 | Controller |	BooksController with actions: Index, Details, Create, Edit, Delete |
| 4 | Views |	Razor files under Views/Books/ for each action (use layout, partial for form) |
| 5 | Validation |	Client‑side + server‑side validation; show validation summary |

# NEXT GOAL

Phase 3 – Step 1: Partial Views & Layout Enhancements

Goal: Make your app more modular and maintainable.

What to do:

Move common HTML (like header, footer, navigation bar) into _Layout.cshtml.

Create Partial Views for repeating components (e.g., book cards, alerts, menus).

Use @RenderBody() and @RenderSection() effectively.

Exercise:

Make a partial view for a book card showing Title, Author, Price, and a “View Details” button.

Replace repeated HTML in your Index and Details views with this partial view.