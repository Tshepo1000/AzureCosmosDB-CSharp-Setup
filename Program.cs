using Microsoft.Azure.Cosmos;

public class Program
{
    private static readonly string EndpointUri = "your-endpoint-uri";
    private static readonly string PrimaryKey = "your-primary-key";

    // cosmos client instance
    private CosmosClient cosmosClient;

    // database creation
    private Database database;

    // container creation
    private Container container;

    // database and container names
    private string databaseId = "your-database-Id";
    private string containerId = "your-container-Id";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new Program();
            await p.CosmosAsync();
        }

        catch(CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }

        catch(Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }

        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }

    public async Task CosmosAsync()
    {
        // instance of cosmos client
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // runs the CreateDatabaseAsync method
        await this.CreateDatabaseAsync();

        // runs the CreateContainerAsync method
        await this.CreateContainerAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        // create new database using cosmos client
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", this.database.Id);
    }

    private async Task CreateContainerAsync()
    {
        // create new container
        this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Lastname");
        Console.WriteLine("Created Container: {0}\n", this.container.Id);
    }
}
