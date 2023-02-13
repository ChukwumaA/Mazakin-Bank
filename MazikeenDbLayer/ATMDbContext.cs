using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MazikeenDbLayer;

public sealed class AtmDbContext : IDisposable
{
    private static string _connString = null!;

    private bool _disposed;

    private static SqlConnection _dbConnection = null!;

    public AtmDbContext():this(@"Data Source=WORKSPACE\SQLEXPRESS;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
    {
            
    }

    private AtmDbContext(string connString)
        => _connString = connString;
    

    public static async Task<SqlConnection> OpenConnection()
    {
        _dbConnection = new SqlConnection(_connString);
        await _dbConnection.OpenAsync();
        return _dbConnection;
    }

    public async Task CloseConnection()
    {
        if (_dbConnection?.State != ConnectionState.Closed)
        {
            await _dbConnection?.CloseAsync()!;
        }
    }


    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return; 
        }

        if (disposing)
        {
            _dbConnection.Dispose();
        }
        _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
