using Extenso.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutofacTypeVarianceIssue.Data
{
    public class TestRepository<T, TKey> : Extenso.Data.Entity.EntityFrameworkRepository<T>, ITestRepository<T, TKey>
        // where T : class, IEntity
        where T : BaseEntity<TKey>
    {
        public TestRepository(IDbContextFactory contextFactory, ILoggerFactory loggerFactory)
            : base(contextFactory, loggerFactory)
        {
        }

        DbContext ITestRepository<T, TKey>.GetContext()
        {
            return base.GetContext();
        }
    }
}