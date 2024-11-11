using AutoMapper;
using Business.Services;
using DataAccess.Concrete.SQL_EntityFrameWork;
using Moq;

namespace UnitTest
{
    public class CategoryTableManagerTests
    {
        private readonly Mock<ICategoryTableDal> _mockDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryTableManager _categoryTableManager;

        // Test öncesi baðýmlýlýklarý oluþturuyoruz
        public CategoryTableManagerTests()
        {
            _mockDal = new Mock<ICategoryTableDal>();
            _mockMapper = new Mock<IMapper>();

            // CategoryTableManager'ý mock'lanmýþ baðýmlýlýklarla baþlatýyoruz
            _categoryTableManager = new CategoryTableManager(_mockDal.Object, _mockMapper.Object);
        }

        [Fact]
        public async void CategoryTableGetListTest()
        {
            await _categoryTableManager.GetListAsync();
        }

    }
}