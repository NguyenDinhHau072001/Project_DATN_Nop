using Microsoft.EntityFrameworkCore;
using Moq;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repository
{
    [TestFixture]
    public class BrandRepositoryTest 
    {
        private IBrandRepository _repository;
        private Mock<ApplicationDBContext> _context;
        private Mock<DbSet<Brand>> _dbSet;

        [SetUp]
        public void SetUp()
        {
            var data = new List<Brand>()
            {
                new Brand{Id = 1, Name= "Samsung"},
                new Brand{Id = 2, Name = "Apple"}
            }.AsQueryable();

            _dbSet = new Mock<DbSet<Brand>>();
            _dbSet.As<IQueryable<Brand>>().Setup(m => m.Provider).Returns(data.Provider);
            _dbSet.As<IQueryable<Brand>>().Setup(m => m.Expression).Returns(data.Expression);
            _dbSet.As<IQueryable<Brand>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _dbSet.As<IQueryable<Brand>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _context = new Mock<ApplicationDBContext>();
            _context.Setup(x=>x.Brands).Returns(_dbSet.Object);
            _repository = new BrandRepository(_context.Object);
        }

        [Test]
        public void GetAll_WhenCall_ReturnListBrand()
        {
            var result = _repository.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
