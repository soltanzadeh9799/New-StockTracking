﻿using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.BLL
{
    public class CategoryBLL : IBLL<CategoryDetailDTO, CategoryDTO>
    {
        CategoryDAO dao = new CategoryDAO();
        public bool Delete(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CategoryDetailDTO entity)
        {
            CATEGORY category = new CATEGORY();
            category.ID = entity.ID;
            category.CategoryName = entity.CategoryName;
            return dao.Insert(category);
        }

        public CategoryDTO Select()
        {
            throw new NotImplementedException();
        }

        public bool Update(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
