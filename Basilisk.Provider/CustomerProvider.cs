using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Customer;
using Basilisk.ViewModel.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Provider
{
    public class CustomerProvider : BaseProvider
    {
        private static CustomerProvider _instance = new CustomerProvider();
        public static CustomerProvider GetProvider()
        {
            return _instance;
        }

        #region Index Customer
        public IEnumerable<GridCustomerViewModel> GetDataIndex(string searchCust)
        {
            var cart = CartRepository.GetRepository().GetAll();
            var customers = (from cus in CustomerRepository.GetRepository().GetAll().AsEnumerable()
                             select new GridCustomerViewModel
                             {
                                 Id = cus.Id,
                                 CustomerName = cus.CompanyName,
                                 Phone = cus.Phone,
                                 Address = cus.Address,
                                 City = cus.City,
                                 TotalProduct = cart.Where(a=> a.CustomerId == cus.Id).Sum(o => o.Quantity)
                             }).AsEnumerable();

            if (!string.IsNullOrEmpty(searchCust))
            {
                customers = customers.Where(a => a.CustomerName.Contains(searchCust));
            }

            return customers.ToList();

        }

        public IndexCustomerViewModel GetIndex(int page, string searchCust)
        {
            var data = GetDataIndex(searchCust);


            var model = new IndexCustomerViewModel
            {
                SearchCustomer = searchCust,
                Customers = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        #endregion


        #region Detail Order Customer

        public IEnumerable<GridOrderViewModel> GetDataIndex(string searchInv, long id)
        {


            var orders = (from order in OrderRepository.GetRepository().GetAll().ToList()
                          where order.CustomerId == id
                          select new GridOrderViewModel
                          {
                              InvoiceNumber = order.InvoiceNumber,
                              CustomerName = CustomerRepository.GetRepository().GetSingle(OrderRepository.GetRepository().GetSingle(order.InvoiceNumber).CustomerId).CompanyName,
                              OrderDate = order.OrderDate.ToString("dd MMMM yyyy"),
                              TotalPrice = GetIndoFormat((order.OrderDetails.Sum(a => a.UnitPrice * (1 - (a.Discount / 100)) * a.Quantity)) + order.DeliveryCost)
                          }).AsEnumerable();


            if (!string.IsNullOrEmpty(searchInv))
            {
                orders = orders.Where(o => o.InvoiceNumber.Contains(searchInv));
            }


            return orders.ToList();
        }


        public IndexOrderViewModel GetIndexOrder(int page, string searchInv, long id)
        {
            var data = GetDataIndex(searchInv, id);


            var model = new IndexOrderViewModel
            {
                SearchInvNumber = searchInv,
                Orders = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }


        #endregion

        #region Detail Cart Customer

        public CartViewModel GetIndexDetailCart(long id)
        {
            var carts = GetDetailCart(id);
            var newCarts = new CartViewModel
            {
                CartDetails = carts,
                TotalPrice = carts.Sum(a => a.Products.Where(p => p.Checked).Sum(b => b.Quantity * b.Price)),
                FormatTotalPrice = GetIndoFormat(carts.Sum(a => a.Products.Where(p => p.Checked).Sum(b => b.Quantity * b.Price))),
                CustomerId = id
            };

            return newCarts;
        }

        public List<CartDetailViewModel> GetDetailCart(long id)
        {
            
            var customers = CustomerRepository.GetRepository().GetAll().AsEnumerable();
            var filter = CartRepository.GetRepository().GetAll().Where(a=> a.CustomerId == id).AsEnumerable();
            var products = ProductRepository.GetRepository().GetAll().AsEnumerable();
            var suppliers = SupplierRepository.GetRepository().GetAll().AsEnumerable();

            var carts = filter.Join(customers,
                            cart => cart.CustomerId,
                            customer => customer.Id,
                            (cart, customer) => new
                            {
                                cart,
                                customer
                            }).ToList().Join(products,
                            cartCustomer => cartCustomer.cart.ProductId,
                            product => product.Id,
                            (cartCustomer, product) => new
                            {
                                cartCustomer,
                                product
                            }).ToList().Join(suppliers,
                            cart => cart.product.SupplierId,
                            seller => seller.Id,
                            (cart, seller) => new
                            {
                                cart,
                                seller
                            }).ToList().GroupBy(
                                                c => c.seller,
                                                c => new ProductDetailViewModel
                                                {
                                                    ProductId = c.cart.product.Id,
                                                    ProductName = c.cart.product.Name,
                                                    Price = c.cart.product.Price,
                                                    FormatPrice = GetIndoFormat(c.cart.product.Price),
                                                    Quantity = c.cart.cartCustomer.cart.Quantity,
                                                    Checked = c.cart.cartCustomer.cart.Checked
                                                },
                                                (key, g) => new CartDetailViewModel
                                                {
                                                    Seller = key.CompanyName,
                                                    Products = g.ToList(),
                                                    CheckedAll = g.All(c => c.Checked == true ? true : false),
                                                    SellerId = key.Id

                                                }).ToList();

            return carts;

        }


        #endregion

        #region Plus, Minus, Delete Product in Cart

        public void PlusQty(long idProd, long idCus)
        {
            var cart = CartRepository.GetRepository().GetAll().SingleOrDefault(a=> a.ProductId == idProd && a.CustomerId == idCus);
            cart.Quantity++;
            CartRepository.GetRepository().Update(cart);
        }

        public void MinusQty(long idProd, long idCus)
        {
            var cart = CartRepository.GetRepository().GetAll().SingleOrDefault(a => a.ProductId == idProd && a.CustomerId == idCus);
           
            if (cart.Quantity <= 1)
            {
                CartRepository.GetRepository().Delete(cart.Id);
            }
            else
            {
                cart.Quantity--;
                CartRepository.GetRepository().Update(cart);
            }

        }

        public void DeleteProduct(long idProd, long idCus)
        {
            var cart = CartRepository.GetRepository().GetAll().SingleOrDefault(a => a.ProductId == idProd && a.CustomerId == idCus);
            CartRepository.GetRepository().Delete(cart.Id);
            
        }


        #endregion


        #region Check

        public void CheckProduct(long idProd, long idCus, bool check)
        {

            var cart = CartRepository.GetRepository().GetAll().SingleOrDefault(a=> a.CustomerId == idCus && a.ProductId == idProd);
            cart.Checked = !check;
            CartRepository.GetRepository().Update(cart);

        }

        public void CheckSupplier(long idCus, long idSeller, bool checkedAll)
        {

            var products = ProductRepository.GetRepository().GetAll().Where(p => p.SupplierId == idSeller).ToList();
            foreach (var prod in products)
            {
                var cart = CartRepository.GetRepository().GetAll().SingleOrDefault(c => c.CustomerId == idCus && c.ProductId == prod.Id);
                if (cart != null)
                {
                    cart.Checked = !checkedAll;
                    CartRepository.GetRepository().Update(cart);
                }
            }
            
        }

        #endregion

        #region Catalog product and Add to Cart
        public IEnumerable<CatalogProductViewModel> GetCatalogProduct(long idCus)
        {
            
            var catalogProd = (from prod in ProductRepository.GetRepository().GetAll().AsEnumerable()
                               where prod.Stock > 0
                               select new CatalogProductViewModel
                               {
                                   ProductId = prod.Id,
                                   CustomerId = idCus,
                                   ProductName = prod.Name,
                                   Price = prod.Price,
                                   FormatedPrice = GetIndoFormat(prod.Price),
                                   ImagePath = "~/84330567.jpg"
                               }).AsEnumerable();

            return catalogProd.ToList();

        }

        

        public void AddtoCart(long idCus, long idProd)
        {

            var check = CartRepository.GetRepository().GetAll().Any(a => a.CustomerId == idCus && a.ProductId == idProd);

            if (check)
            {
                var addQty = CartRepository.GetRepository().GetAll().SingleOrDefault(b => b.CustomerId == idCus && b.ProductId == idProd);
                addQty.Quantity++;
                CartRepository.GetRepository().Update(addQty);
            }
            else
            {
                var cartCus = new Cart
                {
                    CustomerId = idCus,
                    ProductId = idProd,
                    Quantity = +1
                };

                CartRepository.GetRepository().Insert(cartCus);

               
            }

            
        }

        #endregion


        #region Transaction Checkout

        private string GetInvNumber(string highestInvoiceNumber)
        {
            string[] splitNumber = highestInvoiceNumber.Split("-");
            const string first = "0001";

            string month = splitNumber[0];
            string year = splitNumber[1];
            string num = splitNumber[2];

            string newMonth = DateTime.Now.Month.ToString("D2");
            string newYear = DateTime.Now.ToString("yy");
            string newNum;

            if (month == newMonth && year == newYear)
                newNum = (Convert.ToInt32(num) + 1).ToString("D4");
            else
                newNum = first;

            return $"{newMonth}-{newYear}-{newNum}";
        }

        public bool Checkout(CartViewModel model)
        {
            try
            {

                string highestInvoiceNumber = (from order in OrderRepository.GetRepository().GetAll().AsEnumerable()
                                               orderby order.InvoiceNumber
                                               select order.InvoiceNumber).Last();

                var ship = ShipmentRepository.GetRepository().GetSingle((long)2);
                var cust = CustomerRepository.GetRepository().GetSingle(model.CustomerId);

                var newOrder = new Order
                {
                    InvoiceNumber = GetInvNumber(highestInvoiceNumber),
                    CustomerId = model.CustomerId,
                    SalesEmployeeNumber = "J101",
                    OrderDate = DateTime.Now,
                    DeliveryId = 2,
                    DeliveryCost = ship.Cost /*.SingleOrDefault(d => d.Id == 2).Cost*/,
                    DestinationAddress = cust.Address,
                    DestinationCity = cust.City,
                    DestinationPostalCode = "11720" //_context.Customers.SingleOrDefault(c => c.Id == model.CustomerId).PostalCode,


                };


                //OrderRepository.GetRepository().Insert(newOrder);

                //berhasil

                var listProd = new List<Product>();
                var listCart = new List<Cart>();
                var listOrdet = new List<OrderDetail>();

                foreach (var item in model.CartDetails.ToList())
                {
                    foreach (var prod in item.Products.Where(p => p.Checked == true))
                    {
                        listOrdet.Add(new OrderDetail
                        {
                            InvoiceNumber = newOrder.InvoiceNumber,
                            ProductId = prod.ProductId,
                            UnitPrice = prod.Price,
                            Quantity = prod.Quantity,
                            Discount = 0
                        });


                        var upProd = ProductRepository.GetRepository().GetSingle(prod.ProductId);
                        upProd.Stock = upProd.Stock - prod.Quantity;
                        upProd.OnOrder = upProd.OnOrder + prod.Quantity;
                        listProd.Add(upProd);
                    
                        
                        
                        var upCart = CartRepository.GetRepository().GetAll().SingleOrDefault(c => c.CustomerId == model.CustomerId && c.ProductId == prod.ProductId);
                        listCart.Add(upCart);

                        //CartRepository.GetRepository().Delete(upCart.Id);
                    }
                }

                
                return TransactionRepository.GetRepository().Checkout(newOrder, listOrdet, listProd, listCart);

            }
            catch
            {
                return false;
            }


        }
            #endregion

    }

}
