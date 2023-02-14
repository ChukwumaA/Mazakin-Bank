
//This runs the class in the customer implementation folder

using MazikeenDbLayer.Implementation.Card_Implementation;
using MazikeenDbLayer.Implementation.CustomerImplementation;


// ******* This runs the class in the customer implementation folder ******
//await CreateCustomer.CustomerCreated();
//await UpdateCustomer.CustomerUpdated();
//await DeleteCustomer.CustomerDeleted();
//await GetCustomer.CustomerRetrieved();
//await GetAllCustomers.AllCustomersRetrieved();


// ****** This runs the class in the card implementation folder ******
//await CreateCard.CardCreated();
await GetCard.ReadCardDB();
await GetAllCards.ReadAllCardsDB();