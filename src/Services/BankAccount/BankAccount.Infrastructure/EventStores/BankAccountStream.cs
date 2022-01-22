using System;
using BankAccount.Application.Abstract;
using EventStore.ClientAPI;

namespace BankAccount.Infrastructure.EventStores
{
    public class BankAccountStream : AbstractStream
    {
        public const string StreamName = "BankAccountStream"; //means {get => "ProductStream";}
        public const string GroupName = "group1";//replay

        public BankAccountStream(IEventStoreConnection eventStoreConnection) : base(eventStoreConnection,
            StreamName)
        {
        }

        public void Created(CreateProductDto createProductDto)
        {
            Events.AddLast(new ProductCreatedEvent
            {
                Id = Guid.NewGuid(), Name = createProductDto.Name, Price = createProductDto.Price,
                Stock = createProductDto.Stock, UserId = createProductDto.UserId
            });
        }

        public void NameChanged(ChangeProductNameDto changeProductNameDto)
        {
            Events.AddLast(new ProductNameChangedEvent
                { ChangedName = changeProductNameDto.Name, Id = changeProductNameDto.Id });
        }
        
        public void PriceChanged(ChangeProductPriceDto changeProductPriceDto)
        {
            Events.AddLast(new ProductPriceChangedEvent
                { Id = changeProductPriceDto.Id, ChangedPrice = changeProductPriceDto.Price});
        }

        public void Deleted(Guid id)
        {
            Events.AddLast(new ProductDeletedEvent(){Id = id});
        }
    }
}