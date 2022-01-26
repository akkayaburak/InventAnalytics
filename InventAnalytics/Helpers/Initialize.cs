using InventAnalytics.Persistence;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventAnalytics.Helpers
{
    public static class Initialize
    {
        public static async Task InitializeDb(DataContext context)
        {
            var con = context.Open();

            var statement = $@"
                IF  NOT EXISTS (SELECT * FROM sys.objects 
                WHERE object_id = OBJECT_ID(N'[dbo].[Stores]') AND type in (N'U'))

                BEGIN
                CREATE TABLE [dbo].[Stores](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [StoreName] [nvarchar](max) NULL,
                 CONSTRAINT [PK_Stores1] PRIMARY KEY CLUSTERED 
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
                    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                END";
            SqlCommand sqlCommand = new(statement, con);
            await sqlCommand.ExecuteNonQueryAsync();

            statement = $@"
                IF  NOT EXISTS (SELECT * FROM sys.objects 
                WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))

                BEGIN
                CREATE TABLE [dbo].[Products](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [ProductName] [nvarchar](max) NULL,
	                [Cost] [int] NOT NULL,
	                [SalesPrice] [int] NOT NULL,
                 CONSTRAINT [PK_Products1] PRIMARY KEY CLUSTERED 
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
                    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                END";
            sqlCommand.CommandText = statement;
            await sqlCommand.ExecuteNonQueryAsync();

            statement = $@"
                IF  NOT EXISTS (SELECT * FROM sys.objects 
                WHERE object_id = OBJECT_ID(N'[dbo].[InventorySales]') AND type in (N'U'))

                BEGIN
                CREATE TABLE [dbo].[InventorySales](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [ProductId] [int] FOREIGN KEY REFERENCES Products(Id),
	                [StoreId] [int] FOREIGN KEY REFERENCES Stores(Id),
	                [Date] [DATE] NOT NULL,
	                [SalesQuantity] [int] NOT NULL,
	                [Stock] [int] NOT NULL,
                 CONSTRAINT [PK_InventorySales1] PRIMARY KEY CLUSTERED 
                (
	                [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
                    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                END";

            sqlCommand.CommandText = statement;
            await sqlCommand.ExecuteNonQueryAsync();

            statement = "SELECT COUNT(*) FROM Stores";
            sqlCommand.CommandText = statement;
            int count = (int)await sqlCommand.ExecuteScalarAsync();

            if(count == 0)
            {
                statement = "INSERT INTO Stores (StoreName) VALUES ('Cevahir'); INSERT INTO Stores (StoreName) VALUES ('Istiklal'); INSERT INTO Stores (StoreName) VALUES ('Kadikoy'); ";
                sqlCommand.CommandText = statement;
                await sqlCommand.ExecuteNonQueryAsync();
            }


            statement = "SELECT COUNT(*) FROM Products";
            sqlCommand.CommandText = statement;
            count = (int)await sqlCommand.ExecuteScalarAsync();

            if (count == 0)
            {
                statement = "INSERT INTO Products (ProductName, Cost, SalesPrice) VALUES ('Shoes',30,120); INSERT INTO Products (ProductName, Cost, SalesPrice) VALUES ('Tshirt',10,25); INSERT INTO Products (ProductName, Cost, SalesPrice) VALUES ('Jacket',5,15); ";
                sqlCommand.CommandText = statement;
                await sqlCommand.ExecuteNonQueryAsync();
            }

            statement = "SELECT COUNT(*) FROM InventorySales";
            sqlCommand.CommandText = statement;
            count = (int)await sqlCommand.ExecuteScalarAsync();

            if (count == 0)
            {
                statement = $@"INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,1,'2017-03-04',2,23); 
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,1,'2017-03-05',4,19);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,1,'2017-03-06',2,15);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,1,'2017-03-07',0,15);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,1,'2017-03-08',4,11);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,2,'2017-03-04',2,26);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,2,'2017-03-05',0,23);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,2,'2017-03-06',0,25);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,2,'2017-03-07',1,25);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,2,'2017-03-08',0,25);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,3,'2017-03-04',0,15);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,3,'2017-03-05',0,16);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,3,'2017-03-06',0,16);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,3,'2017-03-07',2,14);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (1,3,'2017-03-08',0,14);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,1,'2017-03-04',2,12);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,1,'2017-03-05',10,2);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,1,'2017-03-06',0,2);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,1,'2017-03-07',2,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,1,'2017-03-08',0,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,2,'2017-03-04',4,5);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,2,'2017-03-05',3,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,2,'2017-03-06',0,-1);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,2,'2017-03-07',0,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,2,'2017-03-08',0,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,3,'2017-03-04',0,11);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,3,'2017-03-05',9,2);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,3,'2017-03-06',3,-1);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,3,'2017-03-07',0,-1);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (3,3,'2017-03-08',1,-2);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,1,'2017-03-04',7,28);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,1,'2017-03-05',5,23);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,1,'2017-03-06',0,23);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,1,'2017-03-07',3,20);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,1,'2017-03-08',0,20);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,2,'2017-03-04',5,17);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,2,'2017-03-05',8,9);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,2,'2017-03-06',9,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,2,'2017-03-07',0,0);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,2,'2017-03-08',0,1);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,3,'2017-03-04',6,54);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,3,'2017-03-05',3,51);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,3,'2017-03-06',0,51);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,3,'2017-03-07',0,51);
                                INSERT INTO InventorySales (ProductId, StoreId, Date, SalesQuantity,Stock) VALUES (2,3,'2017-03-08',10,41);

                              "
                               ;
                sqlCommand.CommandText = statement;
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
