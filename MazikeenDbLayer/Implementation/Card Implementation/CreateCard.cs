using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Model;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.Card_Implementation;

public static class CreateCard
{
    public static async Task CardCreated()
    {
        using ICardAtmService cardAtmService = new CardAtmService(new AtmDbContext());
        var cardData = new CardModel()
        {
            CardId = 4,
            CardNumber = 1000003
        };
        var createdCard = await cardAtmService.CreateCardIdAndNumber(cardData);
        Console.WriteLine($"Created customer with id: {createdCard}");
    }
}