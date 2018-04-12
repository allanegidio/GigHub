using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Extensions
{
    public static class MockDbSetExtensions
    {
        public static void SetSource<TEntity>(this Mock<DbSet<TEntity>> mockSet, IList<TEntity> source) where TEntity : class
        {
            var data = source.AsQueryable();

            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
