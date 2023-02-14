using MazikeenDbLayer.Model;

namespace MazikeenDbLayer.Interfaces;

public interface ICardAtmService : IDisposable
{
    Task<int> CreateCardIdAndNumber(CardModel cardModel);
    
    Task<CardModel> GetCard(int id);
    
    Task<IEnumerable<CardModel>> GetAllCards();

}