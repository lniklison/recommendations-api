# recommendations-api
##### By Lucas Niklison



### Prerequisites

- .NET Core SDK


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/lniklison/recommendations-api
   ```

2. Navigate to the project directory
   ```sh
   cd recommendations-api
   ```

3. Install the required packages
	```sh
   dotnet restore
   ```

4. Update the TMDB Api token in appsettings.json

5. Run the project
	```sh
   dotnet run
   ```
6. The API will be available at https://localhost:44347/
7. The Swagger UI will be available at https://localhost:44347/swagger/index.html

### Assumptions

- **Blockbuster and Minority Genres Selection**: A genre is considered a 'blockbuster' if more than 70% of the seats for movies of that genre are taken in the theaters. Otherwise, it's considered a 'minority'.

- **Billboard Generation**: When generating a billboard, the system first checks for movies in the local database. If the "similar to other successful movies in the city" option is selected, the system queries the TMDB API using the data from the local database as reference for genres.

- **Caching Suggested Movies**: Suggested movies are currently cached. Ideally, they would be stored in a database table, along with the selected week and the theater and room to which they were assigned.

- **Unit Testing**: The unit tests provided are representative of the testing approach. Ideally, tests would be more extensive, covering a wider array of scenarios.

- **Database Seeding for Tests**: Ideally, a dedicated database would be set up for testing purposes, populated using a seeding script. Each row in this test database would correspond to a specific test case, rather than generating the test data within each test suite.