using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.Region;
using Basilisk.ViewModel.Shipment;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Provider
{
    public class ShipmentProvider : BaseProvider
    {
        private static ShipmentProvider _instance = new ShipmentProvider();
        public static ShipmentProvider GetProvider()
        {
            return _instance;
        }

        #region Index Delivery
        public IEnumerable<GridShipmentViewModel> GetDataIndex(string searchShip)
        {
            
            var shipments = (from ship in ShipmentRepository.GetRepository().GetAll()
                             select new GridShipmentViewModel
                             {
                                 ShipId = ship.Id,
                                 ShipName = ship.CompanyName,
                                 Phone = ship.Phone,
                                 Cost = GetIndoFormat(ship.Cost)
                             });

            if (!string.IsNullOrEmpty(searchShip))
            {
                shipments = shipments.Where(a => a.ShipName.Contains(searchShip));
            }

            return shipments.ToList();
        }

        public IndexShipmentViewModel GetIndex(int page, string searchShip)
        {
            var data = GetDataIndex(searchShip);


            var model = new IndexShipmentViewModel
            {
                SearchShipper = searchShip,
                Shipments = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion


        #region Detail Delivery
        public DetailsShipmentViewModel GetIndexDetail(long idShip)
        {

            var data = ShipmentRepository.GetRepository().GetSingle(idShip);
            var shipment = new DetailsShipmentViewModel();
            MappingModel(shipment, data);
            shipment.Products = GetDataDetail(idShip).ToList();
            return shipment;
        }

        public IEnumerable<OrderShipmentViewModel> GetDataDetail(long idShip)
        {
            
            var ships = (from ship in ShipmentRepository.GetRepository().GetAll().ToList()
                         join ord in OrderRepository.GetRepository().GetAll().ToList() 
                         on ship.Id equals ord.DeliveryId
                         join cus in CustomerRepository.GetRepository().GetAll().ToList() 
                         on ord.CustomerId equals cus.Id
                         into newShip
                         where ship.Id == idShip
                         from subShip in newShip.DefaultIfEmpty()
                         group subShip by new
                         {
                             ord.InvoiceNumber,
                             subShip.CompanyName,
                             ord.DeliveryCost,
                             ord.OrderDate,
                             ord.ShippedDate,
                             ord.DueDate,
                             ord.DestinationAddress,
                             ord.DestinationCity,
                             ord.DestinationPostalCode
                         } into s
                         select new OrderShipmentViewModel
                         {
                             InvoiceNumber = s.Key.InvoiceNumber,
                             CustomerName = s.Key.CompanyName,
                             OrderDate = GetIndoFormat(s.Key.OrderDate),
                             ShippedDate = s.Key.ShippedDate == null ? "N/A" : GetIndoFormat(s.Key.ShippedDate.Value),
                             DueDate = s.Key.DueDate == null ? "N/A" : GetIndoFormat(s.Key.DueDate.Value),
                             DestinationAddress = s.Key.DestinationAddress,
                             DestinationCity = s.Key.DestinationCity,
                             DestinationPostalCode = s.Key.DestinationPostalCode,
                             Cost = GetIndoFormat(s.Key.DeliveryCost)
                         }).OrderBy(a => a.InvoiceNumber);

            return ships.ToList();

        }

        #endregion


        #region Send Delivery
        public void ShippedData(string invNum)
        {
            
            using(var context = new BasiliskTFContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var model = context.Orders.SingleOrDefault(a => a.InvoiceNumber == invNum);
                        if (model.ShippedDate == null)
                        {
                            model.ShippedDate = DateTime.Now;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        
                     
                                    
                    }
                    catch
                    {
                        context.Database.RollbackTransaction();
                    }
                }
                    
            }
            
        }

        #endregion

        #region Add - Edit Delivery

        public UpsertShipmentViewModel GetAddForm()
        {
            var model = new UpsertShipmentViewModel();
            return model;
        }
        public UpsertShipmentViewModel GetUpdateData(long idShip)
        {


            var data = ShipmentRepository.GetRepository().GetSingle(idShip);
            var model = new UpsertShipmentViewModel();
            MappingModel(model, data);

            return model;
            
        }

        public void SaveData(UpsertShipmentViewModel model)
        {

            if(model.Id == 0)
            {
                var data = new Delivery();

                MappingModel<Delivery, UpsertShipmentViewModel>(data, model);


                ShipmentRepository.GetRepository().Insert(data);
            }
            else
            {
                var oldShip = ShipmentRepository.GetRepository().GetSingle(model.Id);

                MappingModel<Delivery, UpsertShipmentViewModel>(oldShip, model);

                ShipmentRepository.GetRepository().Update(oldShip);
            }
        }

        #endregion

        #region Delete Delivery


        public bool DeleteData(long idShip)
        {

            return ShipmentRepository.GetRepository().Delete(idShip);

            
        }

        #endregion
    }
}
