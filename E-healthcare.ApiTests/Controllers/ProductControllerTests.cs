using EHealthcare.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProjectManagement.Api.Controllers;
using ProjectManagement.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace ProjectManagement.Api.Controllers.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private IBaseRepository<Category> CatRepository { get; set; }

        

        public async Task AddMedicine(Product product)
        {
            //return await base.Post(product);

        }

        [Test]
        public void UpdateMedicineTest()
        {
            Assert.Pass();
        }        

    }
}