using Aranda.CatalogProductCore.Repository.Context;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Testing.Moq.Helpers;
using Moq;
using System.Linq;
using System;

namespace Aranda.CatalogProductCore.Test.MockDbContext
{
    public class CatalogProductDbContextMock
    {
        public static Mock<CatalogProductDbContext> GetDbContext()
        {
            var dbName = Guid.NewGuid().ToString();
            var dbOptions = new DbContextOptionsBuilder<CatalogProductDbContext>()
                        .UseInMemoryDatabase(dbName)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .EnableSensitiveDataLogging(true)
                        .Options;
            return new Mock<CatalogProductDbContext>(dbOptions);
        }

        public static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            return GetMockDbSet(sourceList).Object;
        }

        public static Mock<DbSet<T>> GetMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet;
        }
    }
}