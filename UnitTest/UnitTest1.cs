using AutoMapper;
using Business.Services;
using Core.DataAccess;
using DataAccess.Concrete.SQL_EntityFrameWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTest
{
    public class CategoryTableManagerTests
    {
        private readonly Mock<ICategoryTableDal> _mockDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryTableManager _categoryTableManager;

        // Test öncesi bağımlılıkları oluşturuyoruz
        public CategoryTableManagerTests()
        {
            _mockDal = new Mock<ICategoryTableDal>();
            _mockMapper = new Mock<IMapper>();

            // CategoryTableManager'ı mock'lanmış bağımlılıklarla başlatıyoruz
            _categoryTableManager = new CategoryTableManager(_mockDal.Object, _mockMapper.Object);
        }

        [Fact]
        public async void CategoryTableGetListTest()
        {
            await _categoryTableManager.GetListAsync();
        }

    }

}