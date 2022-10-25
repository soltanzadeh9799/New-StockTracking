using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.BLL
{
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        ProductDAO dao = new ProductDAO();
        CategoryDAO categorydao= new CategoryDAO();
        public bool Delete(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ProductName=entity.ProductName;
            product.Price=entity.Price;
            product.CategoryID=entity.CategoryID;
            return dao.Insert(product);
        }

        public ProductDTO Select()
        {
            ProductDTO dto=new ProductDTO();
            dto.Products=dao.Select();
            dto.Categories=categorydao.Select();
            return dto;
        }

        public bool Update(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.ProductName = entity.ProductName;
            product.Price=entity.Price;
            product.CategoryID = entity.CategoryID;
            product.StockAmount=entity.StockAmount;
            return dao.Update(product);
        }
    }
}
