using System.Data;
using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Model;
using Microsoft.Data.SqlClient;

namespace MazikeenDbLayer.Services;

public class CardAtmService : ICardAtmService
{
    private readonly AtmDbContext _atmDbContext;

    public CardAtmService(AtmDbContext atmDbContext)
    {
        _atmDbContext = atmDbContext;
    }
    public async Task<int> CreateCardIdAndNumber(CardModel cardModel)
    {
        var sqlConn = await AtmDbContext.OpenConnection();

        const string insertQuery = $"INSERT INTO CARD (card_id, card_number) " +
                                   $"VALUES (@card_id, @card_number)" +
                                   $"SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        await using var command = new SqlCommand(insertQuery, sqlConn);

        command.Parameters.AddRange(new SqlParameter[]
        {
            new()
            {
                ParameterName = "@card_id",
                Value = cardModel.CardId,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            },
            new()
            {
                ParameterName = "@card_number",
                Value = cardModel.CardNumber,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            }
        });
        var cardId = (int) (await command.ExecuteScalarAsync() ?? throw new InvalidOperationException());
        return cardId;
    }


    public async Task<CardModel> GetCard(int id)
    {
        
        var sqlConn = await AtmDbContext.OpenConnection();
        const string getCardQuery = $"SELECT * FROM Card WHERE card_id = @card_id";
        await using var command = new SqlCommand(getCardQuery, sqlConn);

        command.Parameters.AddRange(new SqlParameter[]
        {
            new ()
            {
                ParameterName = "@card_id",
                Value = id,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 50
            }
        });
        var cardModel = new CardModel();
        await using var sqlDataReader = await command.ExecuteReaderAsync();
        while (sqlDataReader.Read())
        {
            cardModel.CardId = Convert.ToInt32(sqlDataReader["card_id"].ToString());
            cardModel.CardNumber = Convert.ToInt32(sqlDataReader["card_number"].ToString());
        }
        return cardModel;
    }

    public async Task<IEnumerable<CardModel>> GetAllCards()
    {
        var sqlConn = await AtmDbContext.OpenConnection();
        const string getAllCardsQuery = $"SELECT * FROM Card";
        await using var command = new SqlCommand(getAllCardsQuery, sqlConn);
        var cards = new List<CardModel>();
        await using var sqlDataReader = await command.ExecuteReaderAsync();
        {
            while (sqlDataReader.Read())
            {
                cards.Add(
                    new CardModel()
                    {
                        CardId = Convert.ToInt32(sqlDataReader["card_id"].ToString()),
                        CardNumber = Convert.ToInt32(sqlDataReader["card_number"].ToString())
                    });
            }
        }
        return cards;
    }

    private static void ReleaseUnmanagedResources()
    {
        
    }

    protected virtual void Dispose(bool disposing)
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

    ~CardAtmService()
    {
        Dispose(false);
    }
}