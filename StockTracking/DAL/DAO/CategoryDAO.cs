﻿using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class CategoryDAO: StockContext,IDAO<CATEGORY, CategoryDetailDTO>
    {
        public bool Delete(CATEGORY entity)
        {
            try
            {
                CATEGORY category=db.CATEGORies.First(x=>x.ID==entity.ID);
                category.isDeleted = true;
                category.DeleteDate = DateTime.Today;
                db.SaveChanges();
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
                CATEGORY category = db.CATEGORies.First(x => x.ID == ID);
                category.isDeleted = false;
                category.DeleteDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Insert(CATEGORY entity)
        {
            try
            {
                db.CATEGORies.Add(entity);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            };
        }

        public List<CategoryDetailDTO> Select()
        {
            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORies.Where(x=>x.isDeleted==false).ToList();
            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.ID = item.ID;
                dto.CategoryName = item.CategoryName;
                categories.Add(dto);    
            }
            return categories;

        }
        public List<CategoryDetailDTO> Select(bool isDeleted)
        {
            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORies.Where(x => x.isDeleted == isDeleted).ToList();
            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.ID = item.ID;
                dto.CategoryName = item.CategoryName;
                categories.Add(dto);
            }
            return categories;

        }

        public bool Update(CATEGORY entity)
        {
            try
            {
                CATEGORY category = db.CATEGORies.First(x => x.ID == entity.ID);
                category.CategoryName=entity.CategoryName;
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
