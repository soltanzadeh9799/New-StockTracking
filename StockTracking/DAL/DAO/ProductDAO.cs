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
            PRODUCT product = new PRODUCT();
            product.isDeleted = true;
            product.DeletedDate = DateTime.Today;
            db.SaveChanges();
            return true;
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
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
            var list = (from p in db.PRODUCTs.Where(x=>x.isDeleted==false)
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
                    product.ProductName=entity.ProductName;
                    product.Price = entity.Price;
                    product.CategoryID=entity.CategoryID;
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
