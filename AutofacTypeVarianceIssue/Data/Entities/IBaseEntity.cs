using System.ComponentModel.DataAnnotations;
using Extenso.Data.Entity;

namespace AutofacTypeVarianceIssue.Data.Entities
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>, IEntity
    {
        [Key]
        public virtual TKey Id { get; set; }

        public object[] KeyValues => new object[] { Id };
    }
}