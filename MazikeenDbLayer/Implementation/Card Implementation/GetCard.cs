using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.Card_Implementation;

public class GetCard
{
    public static async Task ReadCardDB()
    {
        using ICardAtmService cardAtmService = new CardAtmService(new AtmDbContext());
        var cardDetails = await cardAtmService.GetCard(4);
        Console.WriteLine($"Card with {cardDetails.CardId} retrieved successfully " +
                          $"with details of {cardDetails.CardNumber}");
    }
}