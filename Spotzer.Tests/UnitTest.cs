using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using Spotzer.DataLayer;
using Spotzer.DataLayer.UnitOfWork;
using Spotzer.Model;
using Spotzer.Model.Entities;
using Spotzer.Service;
using NUnit;
using NUnit.Framework;
using Spotzer.DataLayer.DatabaseContext;
using Spotzer.Model.Inputs;
using System.Collections.Generic;
using System.Linq;
using Spotzer.Model.Enums;

namespace Spotzer.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private WindsorContainer Container;

        [Test]
        public void InsertOrder_InputNotValid_CustomException_CustomExceptionTypeBadRequest()
        {

            var orderService = GetOrderService();
            var order = new InsertOrderInput() { };

            Assert.Throws<CustomException>(() => orderService.InsertOrder(order));
        }

        [Test]
        public void InsertOrder_InputValid_NumberOfOrdersGreaterThanZero()
        {

            var orderService = GetOrderService();
            var order = GetValidOrderInput(PartnerType.PartnerB);
            order.PaidProducts.Add(GetValidCampaignInput());
            orderService.InsertOrder(order);
            var orders = orderService.GetOrders();
            Assert.IsTrue(orders.Count > 0);
        } 

        [Test]
        public void InsertOrder_PartnerAWithtWCampaignProuct_CustomExceptionMessage_PartnerAIncludePaidProduct()
        {
            //Arrange 
            var orderService = GetOrderService();
            var orderInput = GetValidOrderInput(PartnerType.PartnerA);
            orderInput.PaidProducts.Add(GetValidCampaignInput());

            //Act and Assert
            Assert.Throws<CustomException>(() => orderService.InsertOrder(orderInput), OrderConstants.PartnerAIncludePaidProduct);
        }

        [Test]
        public void InsertOrder_PartnerAWithoutWebsiteProuct_CustomExceptionMessage_PartnerAMissingWebsite()
        {
            //Arrange 
            var orderService = GetOrderService();
            var orderInput = GetValidOrderInput(PartnerType.PartnerA);

            //Act and Assert
            Assert.Throws<CustomException>(() => orderService.InsertOrder(orderInput), OrderConstants.PartnerAMissingWebsite);
        }


        [Test]
        public void InsertOrder_PartnerDWithWebSiteProuct_CustomExceptionMessage_PartnerDIncludeWebsite()
        {
            //Arrange 
            var orderService = GetOrderService();
            var orderInput = GetValidOrderInput(PartnerType.PartnerD);
            orderInput.WebSites.Add(GetValidWebsiteInput());

            //Act and Assert
            Assert.Throws<CustomException>(() => orderService.InsertOrder(orderInput), OrderConstants.PartnerDIncludeWebsite);
        }

        [Test]
        public void InsertOrder_PartnerDWithoutPaidProuct_CustomExceptionMessage_PartnerDMissingPaidProduct()
        {
            //Arrange 
            var orderService = GetOrderService();
            var orderInput = GetValidOrderInput(PartnerType.PartnerD);

            //Act and Assert
            Assert.Throws<CustomException>(() => orderService.InsertOrder(orderInput), OrderConstants.PartnerDMissingPaidProduct);
        }

        [Test]
        [TestCase(PartnerType.PartnerB)]
        [TestCase(PartnerType.PartnerD)]
        public void InsertOrder_PartnerBandDWithAdditionalInfo_CustomExceptionMessage_PartnerBandDCantHaveAdditionalInfo(PartnerType partnerType)
        {
            //Arrange 
            var orderService = GetOrderService();
            var orderInput = GetValidOrderInput(partnerType);
            orderInput.AdditionalOrderInfo = "deneme";
            //Act and Assert
            Assert.Throws<CustomException>(() => orderService.InsertOrder(orderInput), OrderConstants.PartnerBandDCantHaveAdditionalInfo);
        }


        [Test]
        [TestCase(PartnerType.PartnerA)]
        [TestCase(PartnerType.PartnerC)]
        public void InsertOrder_PartnerAandCWithoutAdditionalInfo_CustomExceptionMessage_PartnerAandCHaveAdditionalInfo(PartnerType partnerType)
        {
            //Arrange 
            var orderService = GetOrderService();
            var orderInput = GetValidOrderInput(partnerType);
            //Act and Assert
            Assert.Throws<CustomException>(() => orderService.InsertOrder(orderInput), OrderConstants.PartnerAandCHaveAdditionalInfo);
        }


        [SetUp]
        public void SetUp()
        {
            Container = new WindsorContainer();
            // register your dependencies

            var orderList = new List<Order>();
            var orderSet = new Mock<DbSet<Order>>();

            orderSet.Setup(m => m.Add(It.IsAny<Order>())).Callback((Order item) => orderList.Add(item)); ;


            var productList = new List<Product>();
            var productSet = new Mock<DbSet<Product>>();

            productSet.Setup(m => m.Add(It.IsAny<Product>())).Callback((Product item) => productList.Add(item)); ;

            orderSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orderList.AsQueryable().Provider);
            orderSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orderList.AsQueryable().Expression);
            orderSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orderList.AsQueryable().ElementType);
            orderSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orderList.AsQueryable().GetEnumerator());

            productSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(productList.AsQueryable().Provider);
            productSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(productList.AsQueryable().Expression);
            productSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(productList.AsQueryable().ElementType);
            productSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(productList.AsQueryable().GetEnumerator());

            var contextMock = new Mock<IDataBaseContext>();
            contextMock.Setup(a => (a).Set<Order>()).Returns(orderSet.Object);
            contextMock.Setup(a => (a).Set<Product>()).Returns(productSet.Object);
            //contextMock.Setup(a => (a).Set<Order>()).Returns(Mock.Of<DbSet<Order>>);

            var orderService = new Mock<IOrderService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>().LifeStyle.Transient);
            Container.Register(Component.For<IDataBaseContext>().ImplementedBy<DataBaseContext>().LifeStyle.Transient);
            Container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifeStyle.Transient);

            Container.Register(Component.For<Mock<IDataBaseContext>>().Instance(contextMock));
            Container.Register(Component.For<Mock<IOrderService>>().Instance(orderService));
            Container.Register(Component.For<Mock<IUnitOfWork>>().Instance(unitOfWorkMock));
            //Container.Register(Component.For<List<Order>>().Instance(orderList));
        }

        [TearDown]
        public void Cleanup()
        {
            Container.Dispose();
        }

        private IOrderService GetOrderService()
        {
            var service = Container.Resolve<Mock<IOrderService>>();
            var uow = new UnitOfWork(Container.Resolve<Mock<IDataBaseContext>>().Object);
            var orderService = new OrderService(uow);
            return orderService;
        }
        private IUnitOfWork GetUnitOfWork()
        {
            var uow = new UnitOfWork(Container.Resolve<Mock<IDataBaseContext>>().Object);
            return uow;
        }

        private InsertOrderInput GetValidOrderInput(PartnerType partnerType)
        {
            return new InsertOrderInput()
            {
                CompanyId = 1,
                CompanyName = "CompanyName",
                PartnerId = partnerType,
                SubmittedBy = "Cuma Kılınç",
                TypeOfOrder = "TypeOfOrder",
                PaidProducts = new List<InsertCampaignProduct>(),
                WebSites = new List<InsertWebSiteProduct>()
            };
        }

        private InsertCampaignProduct GetValidCampaignInput()
        {
            return new InsertCampaignProduct()
            {
                CampaignAddressLine1 = "CampaignAddressLine1",
                CampaignName = "CampaignName",
                CampaignPostCode = "CampaignPostCode",
                DestinationURL = "DestinationURL",
                Category = "Category",
                LeadPhoneNumber = "905059117499",
                Offer = 1.0M,
                Radius = 1.0M,
                Notes = "Notes",
                ProductType = ProductType.PaidProduct,
                SMSPhoneNumber = "+905059117499",
                UniqueSellingPoint1 = "UniqueSellingPoint1"
            };
        }

        private InsertWebSiteProduct GetValidWebsiteInput()
        {
            return new InsertWebSiteProduct()
            {
                Notes = "Notes",
                ProductType = ProductType.WebSite,
                Category = "Category",
                TemplateId = 1,
                WebsiteAddressLine1 = "WebsiteAddressLine1",
                WebsiteAddressLine2 = "WebsiteAddressLine2",
                WebsiteBusinessName = "BussinessName",
                WebsiteCity = "Istanbul",
                WebsiteEmail = "cuma@cuma.com",
                WebsiteMobile = "905059117499",
                WebsitePhone = "905059117499",
                WebsitePostCode = "sllla",
                WebsiteState = "Marmara"
            };
        }

        private InsertOrderInput GetInvalidOrderInput()
        {
            return new InsertOrderInput();
        }
    }
}
