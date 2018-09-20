using Spotzer.DataLayer.UnitOfWork;
using Spotzer.Model;
using Spotzer.Model.Entities;
using Spotzer.Model.Enums;
using Spotzer.Model.Inputs;
using Spotzer.Model.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Service
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ICollection<OrderOutput> GetOrders()
        {
            var k = 
           _unitOfWork.OrderRepository.Queryable().OrderByDescending(x => x.Id).Select(x => new OrderOutput
            {
                Id = x.Id,
                PartnerId = x.PartnerId,
                CompanyName = x.CompanyName,
                SubmittedBy = x.SubmittedBy,
                TypeOfOrder = x.TypeOfOrder,
                CompanyId = x.CompanyId
            }).ToList();

            return k;
        }
        public ICollection<Product> GetProducts()
        {
            return _unitOfWork.ProductRepository.Queryable().OrderByDescending(x => x.Id).ToList();
        }
        public void InsertOrder(InsertOrderInput input)
        {
            try
            {
                if (!input.IsValid())
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, input.GetErrorMessage());

                if (input.PartnerId == PartnerType.PartnerA && input.PaidProducts.Count > 1)
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, OrderConstants.PartnerAIncludePaidProduct);

                if (input.PartnerId == PartnerType.PartnerA && input.WebSites.Count == 0)
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, OrderConstants.PartnerAMissingWebsite);

                if (input.PartnerId == PartnerType.PartnerD && input.WebSites.Count > 1)
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, OrderConstants.PartnerDIncludeWebsite);

                if (input.PartnerId == PartnerType.PartnerD && input.PaidProducts.Count == 0)
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, OrderConstants.PartnerDMissingPaidProduct);

                if ((input.PartnerId == PartnerType.PartnerB || input.PartnerId == PartnerType.PartnerD) && input.AdditionalOrderInfo != null)
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, OrderConstants.PartnerBandDCantHaveAdditionalInfo);

                if ((input.PartnerId == PartnerType.PartnerA || input.PartnerId == PartnerType.PartnerC) && input.AdditionalOrderInfo == null)
                    throw new CustomException(CustomExceptionTypeEnum.BadRequest, OrderConstants.PartnerAandCHaveAdditionalInfo);
                var orderEntity = new Order
                {
                    AdditionalOrderInfo = input.AdditionalOrderInfo,
                    CompanyId = input.CompanyId,
                    CompanyName = input.CompanyName,
                    PartnerId = input.PartnerId,
                    SubmittedBy = input.SubmittedBy,
                    TypeOfOrder = input.TypeOfOrder,
                    Products = new List<Product>()
                };

                foreach (var item in input.PaidProducts)
                    orderEntity.Products.Add(new PaidSearch
                    {
                        LeadPhoneNumber = item.LeadPhoneNumber,
                        CampaignAddressLine1 = item.CampaignAddressLine1,
                        CampaignName = item.CampaignName,
                        CampaignPostCode = item.CampaignPostCode,
                        Category = item.Category,
                        DestinationURL = item.DestinationURL,
                        Notes = item.Notes,
                        Offer = item.Offer,
                        Radius = item.Radius,
                        SMSPhoneNumber = item.SMSPhoneNumber,
                        UniqueSellingPoint1 = item.UniqueSellingPoint1,
                        UniqueSellingPoint2 = item.UniqueSellingPoint2,
                        UniqueSellingPoint3 = item.UniqueSellingPoint3
                    });

                foreach (var item in input.WebSites)
                    orderEntity.Products.Add(new WebSite
                    {
                        Category = item.Category,
                        Notes = item.Notes,
                        TemplateId = item.TemplateId,
                        WebsiteAddressLine1 = item.WebsiteAddressLine1,
                        WebsiteAddressLine2 = item.WebsiteAddressLine2,
                        WebsiteBusinessName = item.WebsiteBusinessName,
                        WebsiteCity = item.WebsiteCity,
                        WebsiteEmail = item.WebsiteEmail,
                        WebsiteMobile = item.WebsiteMobile,
                        WebsitePhone = item.WebsitePhone,
                        WebsitePostCode = item.WebsitePostCode,
                        WebsiteState = item.WebsiteState
                    });

                _unitOfWork.OrderRepository.Insert(orderEntity);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (ex is CustomException)
                    throw ex;
                throw new CustomException(CustomExceptionTypeEnum.InternalServerError, "An Error Occurred", ex);
            }
        }
    }
}
