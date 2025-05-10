# Library Management System

A C# console application for managing a library system with features for handling books, members, and borrowing operations.

## Prerequisites

Before running the application, ensure you have the following installed:

1. **.NET 9.0 SDK** or later
   - Download from: https://dotnet.microsoft.com/download/dotnet/9.0
   - Verify installation by running: `dotnet --version`

2. **MySQL Server**
   - Download from: https://dev.mysql.com/downloads/mysql/
   - Make sure the MySQL service is running
   - Note down your MySQL server credentials (username and password)

## Configuration

1. **Database Setup**
   - Create a new MySQL database named `library_management`
   - The application will automatically create the necessary tables on first run

2. **Connection String**
   - The application uses a default connection string that assumes:
     - Server: localhost
     - Port: 3306
     - Database: library_management
     - Username: root
     - Password: (your MySQL password)
   - If your MySQL configuration differs, you'll need to update the connection string in the application

## Building and Running the Application

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd LibraryManagementSystem
   ```

2. **Build the Application**
   ```bash
   dotnet build
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

## Application Features

The Library Management System provides the following features:

1. **Book Management**
   - Add new books
   - Update book information
   - Remove books
   - View all books
   - Search books

2. **Member Management**
   - Register new members
   - Update member information
   - Remove members
   - View all members
   - Search members

3. **Borrowing Operations**
   - Borrow books
   - Return books
   - View borrowing history
   - Check overdue books

## Troubleshooting

1. **Database Connection Issues**
   - Ensure MySQL server is running
   - Verify the connection string matches your MySQL configuration
   - Check if the database exists

2. **Build Errors**
   - Ensure you have the correct .NET SDK version installed
   - Run `dotnet restore` to restore any missing packages
   - Check for any missing dependencies

3. **Runtime Errors**
   - Check the console output for error messages
   - Verify database permissions
   - Ensure all required tables are created

## Support

If you encounter any issues or have questions, please:
1. Check the troubleshooting section above
2. Review the error messages in the console
3. Contact the development team for assistance

## License

[Specify your license here] 