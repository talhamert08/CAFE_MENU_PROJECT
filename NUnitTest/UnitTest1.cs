using AutoMapper;
using Business.Services;
using Core.DataAccess;
using DataAccess.Concrete.SQL_EntityFrameWork;
using DryIoc;
using DryIoc.ImTools;
using Entities.Concrete;
using Moq;

namespace NUnitTest
{

    public class CategoryTableManagerTests
    {
        private Mock<ICategoryTableDal> _mockDal;
        private Mock<IMapper> _mockMapper;
        private CategoryTableManager _categoryTableManager;

        // Test öncesi bağımlılıkları oluşturuyoruz
        [SetUp]
        public void Setup()
        {
            _mockDal = new Mock<ICategoryTableDal>();
            _mockMapper = new Mock<IMapper>();

            // CategoryTableManager'ı mock'lanmış bağımlılıklarla başlatıyoruz
            _categoryTableManager = new CategoryTableManager(_mockDal.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CategoryTableGetListTestNUnit()
        {
            await _categoryTableManager.GetListAsync();
        }
    }

}