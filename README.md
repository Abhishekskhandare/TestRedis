# TestRedis is a simple User Management API designed for managing user data efficiently using a combination of SQL Server and Redis cache for optimal performance.

Features

Get All Users: Fetches the complete list of users.

Get User by Email: Retrieves details of a specific user by their email ID.

Create User: Allows adding a new user to the system.

Delete User: Removes a user from the system.

How It Works

1. SQL Server as the Data Source:

The API connects to a SQL Server database for all user-related operations.



2. Redis Caching Mechanism:

When a user list or specific user is fetched from the SQL Server, the data is cached in Redis.

On subsequent requests, the API checks the Redis cache first.

If data is found in Redis, it retrieves it from the cache (faster response).

If not, it queries the SQL Server again, caches the result in Redis, and returns the data to the user.
