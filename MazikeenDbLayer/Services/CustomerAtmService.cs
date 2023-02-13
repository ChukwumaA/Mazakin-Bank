using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MazikeenDbLayer.Services;

public class CustomerAtmService : ICustomerAtmService
{
    private readonly AtmDbContext _atmDbContext;
    //private bool _disposed;

    public CustomerAtmService(AtmDbContext atmDbContext)
    {
        _atmDbContext = atmDbContext;
    }

    public async Task<int> AddCustomer(CustomerModel customer)
    {
        var sqlConn = await AtmDbContext.OpenConnection();

        const string insertQuery = $"INSERT INTO CUSTOMER (customer_id, firstname, lastname, gender, age, address) " +
                                   $"VALUES (@customer_id, @firstname, @lastname, @gender, @age, @address); " +
                                   $"SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

        await using var command = new SqlCommand(insertQuery, sqlConn);

        command.Parameters.AddRange(new SqlParameter[]
        {
            new()
            {
                ParameterName = "@customer_id",
                Value = customer.customer_id,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@firstname",
                Value = customer.firstName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@lastname",
                Value = customer.lastName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@gender",
                Value = customer.gender,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@age",
                Value = customer.age,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@address",
                Value = customer.address,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            }

        });
        var customerId = (int)(await command.ExecuteScalarAsync() ?? throw new InvalidOperationException());
        return customerId;
    }

    public async Task<bool> UpdateCustomer(int id, CustomerModel customerAtm)
    {
        var sqlConn = await AtmDbContext.OpenConnection();
        const string insertQuery = $"UPDATE Customer SET firstname = @firstname, lastname = @lastname " +
                                   $"WHERE customer_id = @customer_id";
        await using var command = new SqlCommand(insertQuery, sqlConn);

        command.Parameters.AddRange(new SqlParameter[]
        {
            new()
            {
                ParameterName = "@customer_id",
                Value = customerAtm.customer_id,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@firstname",
                Value = customerAtm.firstName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@lastname",
                Value = customerAtm.lastName,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Size = 50
            }
        });
        var updateCustomer = await command.ExecuteNonQueryAsync();
        return (updateCustomer > 0);
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        var sqlConn = await AtmDbContext.OpenConnection();
        const string insertQuery = $"DELETE FROM Customer WHERE customer_id = @customer_id";
        await using var command = new SqlCommand(insertQuery, sqlConn);

        command.Parameters.AddRange(new SqlParameter[]
        {
            new()
            {
                ParameterName = "@customer_id",
                Value = id,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            }
        });
        var deleteCustomer = await command.ExecuteNonQueryAsync();
        return (deleteCustomer > 0);
    }

    public async Task<CustomerModel> GetCustomer(int id)
    {
        var sqlConn = await AtmDbContext.OpenConnection();
        const string getCustomerQuery = $"SELECT * FROM Customer WHERE customer_id = @customer_id";
        await using var command = new SqlCommand(getCustomerQuery, sqlConn);

        command.Parameters.AddRange(new SqlParameter[]
        {
            new ()
            {
                ParameterName = "@customer_id",
                Value = id,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            }
        });
        var customer = new CustomerModel();
        await using var sqlDataReader = await command.ExecuteReaderAsync();
        while (sqlDataReader.Read())
        {
            customer.customer_id = Convert.ToInt32(sqlDataReader["customer_id"].ToString());
            customer.firstName = sqlDataReader["firstname"].ToString() ?? throw new InvalidOperationException();
            customer.lastName = sqlDataReader["lastname"].ToString() ?? throw new InvalidOperationException();
            customer.age = Convert.ToInt32(sqlDataReader["age"].ToString());
            customer.address = sqlDataReader["address"].ToString() ?? throw new InvalidOperationException();
            customer.gender = sqlDataReader["gender"].ToString() ?? throw new InvalidOperationException();
        }
        return customer;
    }

    public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
    {
        var sqlConn = await AtmDbContext.OpenConnection();
        const string getAllCustomersQuery = $"SELECT * FROM Customer";
        await using var command = new SqlCommand(getAllCustomersQuery, sqlConn);
        var customers = new List<CustomerModel>();
        await using var sqlDataReader = await command.ExecuteReaderAsync();
        {
            while (sqlDataReader.Read())
            {
                customers.Add(
                    new CustomerModel()
                    {
                        customer_id = Convert.ToInt32(sqlDataReader["customer_id"].ToString()),
                        firstName = sqlDataReader["firstname"].ToString() ?? throw new InvalidOperationException(),
                        lastName = sqlDataReader["lastname"].ToString() ?? throw new InvalidOperationException(),
                        age = Convert.ToInt32(sqlDataReader["age"].ToString()),
                        address = sqlDataReader["address"].ToString() ?? throw new InvalidOperationException(),
                        gender = sqlDataReader["gender"].ToString() ?? throw new InvalidOperationException()

                    });
            }
        }
        return customers;
    }

    private static void ReleaseUnmanagedResources()
    {
    }

    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing)
        {
            _atmDbContext.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~CustomerAtmService()
    {
        Dispose(true);
    }
}