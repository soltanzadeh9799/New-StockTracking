using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class ProductDAO : StockContext, IDAO<PRODUCT, ProductDetailDTO>
    {
        public bool Delete(PRODUCT entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    PRODUCT product = new PRODUCT();
                    product.isDeleted = true;
                    product.DeletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                else if(entity.CategoryID!=0)
                {
                    List<PRODUCT> product=db.PRODUCTs.Where(x => x.CategoryID==entity.CategoryID).ToList();
                    foreach (var item in product)
                    {
                        item.isDeleted = false;
                        item.DeletedDate = DateTime.Today;

                        List<SALE> sales = db.SALES.Where(x => x.ProductID == entity.ID).ToList();
                        foreach (var item1 in sales)
                        {
                            item1.isDeleted = false;
                            item1.DeletedDate = DateTime.Today;
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool GetBack(int ID)
        {
            try
            {
                PRODUCT product = db.PRODUCTs.First(x => x.ID == ID);
                product.isDeleted = false;
                product.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Insert(PRODUCT entity)
        {
            db.PRODUCTs.Add(entity);
            db.SaveChanges();
            return true;
        }

        public List<ProductDetailDTO> Select()
        {
            List<ProductDetailDTO> product = new List<ProductDetailDTO>();
            var list = (from p in db.PRODUCTs.Where(x => x.isDeleted == false)
                        join c in db.CATEGORies on p.CategoryID equals c.ID
                        select new
                        {
                            ProductName = p.ProductName,
                            CatgoryName = c.CategoryName,
                            StockAmount = p.StockAmount,
                            Price = p.Price,
                            ProductID = p.ID,
                            CategoryID = c.ID

                        }).OrderBy(x => x.ProductName).ToList();
            foreach (var item in list)
            {
                ProductDetailDTO dto = new ProductDetailDTO();
                dto.ProductName = item.ProductName;
                dto.CategoryName = item.CatgoryName;
                dto.StockAmount = item.StockAmount;
                dto.Price = item.Price;
                dto.ProductID = item.ProductID;
                dto.CategoryID = item.CategoryID;
                product.Add(dto);
            }
            return product;
        }
        public List<ProductDetailDTO> Select(bool isDeleted)
        {
            List<ProductDetailDTO> product = new List<ProductDetailDTO>();
            var list = (from p in db.PRODUCTs.Where(x => x.isDeleted == isDeleted)
                        join c in db.CATEGORies on p.CategoryID equals c.ID
                        select new
                        {
                            ProductName = p.ProductName,
                            CatgoryName = c.CategoryName,
                            StockAmount = p.StockAmount,
                            Price = p.Price,
                            ProductID = p.ID,
                            CategoryID = c.ID

                        }).OrderBy(x => x.ProductName).ToList();
            foreach (var item in list)
            {
                ProductDetailDTO dto = new ProductDetailDTO();
                dto.ProductName = item.ProductName;
                dto.CategoryName = item.CatgoryName;
                dto.StockAmount = item.StockAmount;
                dto.Price = item.Price;
                dto.ProductID = item.ProductID;
                dto.CategoryID = item.CategoryID;
                product.Add(dto);
            }
            return product;
        }

        public bool Update(PRODUCT entity)
        {
            try
            {
                PRODUCT product = db.PRODUCTs.First(x => x.ID == entity.ID);
                if (entity.CategoryID == 0)
                {
                    product.StockAmount = entity.StockAmount;

                }
                else
                {
                    product.ProductName = entity.ProductName;
                    product.Price = entity.Price;
                    product.CategoryID = entity.CategoryID;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
