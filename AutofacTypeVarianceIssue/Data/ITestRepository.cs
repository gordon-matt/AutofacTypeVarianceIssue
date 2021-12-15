using Microsoft.EntityFrameworkCore;

namespace AutofacTypeVarianceIssue.Data
{
    public interface ITestRepository<T, TKey> : Extenso.Data.Entity.IRepository<T>
        where T : class
    {
        DbContext GetContext();

        // Etc.. (Additional members removed for brevity)
    }
}