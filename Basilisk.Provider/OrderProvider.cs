using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Shipment;
using Basilisk.ViewModel.Order;
using Microsoft.EntityFrameworkCore;
using Basilisk.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Basilisk.Provider
{
    public class OrderProvider : BaseProvider
    {
        private static OrderProvider _instance = new OrderProvider();
        public static OrderProvider GetProvider()
        {
            return _instance;
        }


        #region Index Order
        public IEnumerable<GridOrderViewModel> GetDataIndex(string searchInv, string searchCus)
        {
            
            
            var orders = (from order in OrderRepository.GetRepository().GetAll().ToList()
                          select new GridOrderViewModel
                          {
                              InvoiceNumber = order.InvoiceNumber,
                              CustomerName = CustomerRepository.GetRepository().GetSingle(OrderRepository.GetRepository().GetSingle(order.InvoiceNumber).CustomerId).CompanyName ,
                              OrderDate = order.OrderDate.ToString("dd MMMM yyyy"),
                              TotalPrice = GetIndoFormat((order.OrderDetails.Sum(a=> a.UnitPrice * (1 - (a.Discount / 100)) * a.Quantity)) + order.DeliveryCost)
                          }).AsEnumerable();


            if (!string.IsNullOrEmpty(searchInv))
            {
                orders = orders.Where(o => o.InvoiceNumber.Contains(searchInv));
            }
            if (!string.IsNullOrEmpty(searchCus))
            {
                orders = orders.Where(o => o.CustomerName.Contains(searchCus));
            }

            return orders.ToList();
        }

        


        public IndexOrderViewModel GetIndex(int page, string searchInv, string searchCus)
        {
            var data = GetDataIndex(searchInv, searchCus);


            var model = new IndexOrderViewModel
            {
                SearchInvNumber = searchInv,
                SearchCustomer = searchCus,
                Orders = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion


        #region Detail Delivery
        public DetailOrderViewModel GetIndexDetail(string invNum)
        {

            var data = OrderRepository.GetRepository().GetSingle(invNum);
            var detailOrder = new DetailOrderViewModel
                               {
                                   InvoiceNumber = invNum,
                                   CustomerName = CustomerRepository.GetRepository().GetSingle(data.CustomerId).CompanyName ,
                                   SalesName = SalesmanRepository.GetRepository().GetSingle(data.SalesEmployeeNumber).FirstName + " " + SalesmanRepository.GetRepository().GetSingle(data.SalesEmployeeNumber).LastName,
                                   OrderDate = GetIndoFormat(data.OrderDate)

                               };

            detailOrder.DeliveryInfo = GetDeliveryInfo(invNum);
            detailOrder.ProductOrders = GetProductInfo(invNum);

            return detailOrder;
        }

        public DeliveryInfoViewModel GetDeliveryInfo(string invNum)
        {

            var data = OrderRepository.GetRepository().GetSingle(invNum);
            var deliveryInfo =  new DeliveryInfoViewModel
                                {
                                    ShippedDate = (data.ShippedDate == null) ? "N/A" : GetIndoFormat(data.ShippedDate.Value),
                                    DueDate = (data.DueDate == null) ? "N/A" : GetIndoFormat(data.DueDate.Value),
                                    DestinationAddress = data.DestinationAddress,
                                    DestinationCity = data.DestinationCity,
                                    DestinationPostalCode = data.DestinationPostalCode,
                                    DeliveryCost = GetIndoFormat(data.DeliveryCost)
                                };

            return deliveryInfo;
        }

        public IEnumerable<ProductInfoOrderViewModel> GetProductInfo(string invNum)
        {
            
            var productOrders = (from ordet in OrderDetailRepository.GetRepository().GetAll().AsEnumerable()
                                 join prod in ProductRepository.GetRepository().GetAll().AsEnumerable()
                                 on ordet.ProductId equals prod.Id
                                 where ordet.InvoiceNumber == invNum
                                 select new ProductInfoOrderViewModel
                                 {
                                     ProductName = prod.Name,
                                     Quantity = ordet.Quantity,
                                     UnitPrice = GetIndoFormat(ordet.UnitPrice),
                                     Discount = (ordet.Discount / 100).ToString("P0")
                                 }).AsEnumerable();

            return productOrders;

        }
        
        

        #endregion

        #region Accept Delivery

        public void AccDelivery(string invNum)
        {
         
                        var model = OrderRepository.GetRepository().GetSingle(invNum); /*context.Orders.SingleOrDefault(a => a.InvoiceNumber == invNum);*/
                        if (model.DueDate == null && model.ShippedDate != null)
                        {

                            model.DueDate = DateTime.Now;
                            OrderRepository.GetRepository().Update(model);
                            //context.SaveChanges();
                            //tran.Commit();

                        }
                    
                
                    
            
        }

        #endregion
    }
}
