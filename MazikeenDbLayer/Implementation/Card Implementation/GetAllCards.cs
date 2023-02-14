using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.Card_Implementation;

public class GetAllCards
{
    public static async Task ReadAllCardsDB()
    {
        using ICardAtmService cardAtmService = new CardAtmService(new AtmDbContext());
        var allCustomersCards = await cardAtmService.GetAllCards();
        foreach (var cardDetails in allCustomersCards)
        {
            Console.WriteLine($"Card with {cardDetails.CardId} retrieved successfully " +
                              $"with details of {cardDetails.CardNumber}");
        }

        
    }
}