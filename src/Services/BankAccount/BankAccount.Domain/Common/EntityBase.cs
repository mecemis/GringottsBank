using System;

namespace BankAccount.Domain.Common
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}