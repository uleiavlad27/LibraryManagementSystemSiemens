



## Prerequisites

To successfully run the application, you must have the following software installed:

1. .NET 9.0 SDK
   - Download from: https://dotnet.microsoft.com/download/dotnet/9.0

2. MySQL Server
   - Download from: https://dev.mysql.com/downloads/mysql/



## Configuration

1. Database Setup
   - Create a new MySQL schema (database) named "library_management"
   - The application will take care of the rest, you won't need to create any tables manually

2. Connection String
   - The application uses a default connection string that assumes:
     - connectionString = "server=localhost;user=root;password=<your_password>;database=library_management"
   - If your MySQL configuration differs, you'll need to update the connection string in the application


## Building and Running the Application

1. Clone the Repository
   
   ```
   git clone [repository-url]
   cd LibraryManagementSystem
   ```

2. Build the Application
   
   ```
   dotnet build
   ```

3. Run the Application
   ```
   dotnet run
   ```



## Application Features


1. Book Management
   - Add new books (Functionality 1)
   - View all books (Functionality 2)
   - Update book information (Functionality 3)
   - Remove books (Functionality 4)
   - Search books (Functionality 5)

2. Borrowing Operations
   - Borrow books (Functionality 6)
   - Return books (Functionality 7)

3. Wish list (New functionality Development from the PDF)
   - Add books to wish list (Functionality 8)
   - View wish list (Functionality 9)
   - Remove books from wish list (Functionality 10)

