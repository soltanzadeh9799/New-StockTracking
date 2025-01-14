﻿using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class SalesDAO : StockContext, IDAO<SALE, SalesDetailDTO>
    {
        public bool Delete(SALE entity)
        {
            if (entity.ID != 0)
            {
                SALE sales = db.SALES.First(x => x.ID == entity.ID);
                sales.isDeleted = true;
                sales.DeletedDate = DateTime.Today;
                db.SaveChanges();
            }
            else if(entity.ProductID != 0)
            {
                List<SALE> sales=db.SALES.Where(x => x.ProductID == entity.ProductID).ToList();
                foreach (var item in sales)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Today;

                }
                db.SaveChanges();
            }
            else if (entity.CustomerID != 0)
            {
                List<SALE> sales = db.SALES.Where(x => x.CustomerID == entity.CustomerID).ToList();
                foreach (var item in sales)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Today;

                }
                db.SaveChanges();
            }
            return true;
        }

        public bool GetBack(int ID)
        {
            try
            {
                SALE sales = db.SALES.First(x => x.ID == ID);
                sales.isDeleted = false;
                sales.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Insert(SALE entity)
        {
            try
            {
                db.SALES.Add(entity);
                db.SaveChanges();
                return true;    


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SalesDetailDTO> Select()
        {
            List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
            var list = (from s in db.SALES.Where(x => x.isDeleted == false)
                        join p in db.PRODUCTs on s.ProductID equals p.ID
                        join c in db.CUSTOMERs on s.CustomerID equals c.ID
                        join category in db.CATEGORies on s.CategoryID equals category.ID
                        select new
                        {
                            ProductName = p.ProductName,
                            CustomerName = c.CustomerName,
                            CategoryName = category.CategoryName,
                            ProductID = s.ProductID,
                            CustomerID = s.CustomerID,
                            salesID = s.ID,
                            CategoryID = s.CategoryID,
                            SalesPrice = s.ProductSalesPrice,
                            SalesAmount = s.ProductSalesAmount,
                            SalesDate = s.SalesDate,
                            CategoryDeleted = category.isDeleted,
                            CustomerDeleted=c.isDeleted,
                            ProductDeleted=p.isDeleted
                        }).OrderBy(x=>x.SalesDate).ToList();
            foreach (var item in list)
            {
                SalesDetailDTO dto = new SalesDetailDTO();
                dto.ProductName = item.ProductName;
                dto.CustomerName = item.CustomerName;
                dto.CategoryName = item.CategoryName;
                dto.ProductID = item.ProductID;
                dto.CustomerID = item.CustomerID;
                dto.CategoryID = item.CategoryID;
                dto.SalesID = item.salesID;
                dto.Price = item.SalesPrice;
                dto.SalesAmount=item.SalesAmount;
                dto.SalesDate=item.SalesDate;
                dto.IscategoryDeleted = item.CategoryDeleted;
                dto.IsProductDeleted = item.ProductDeleted;
                dto.IsCustomerDeleted=item.CustomerDeleted;
                sales.Add(dto);

            }
            return sales;



        }
        public List<SalesDetailDTO> Select(bool isDeleted)
        {
            List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
            var list = (from s in db.SALES.Where(x => x.isDeleted == false)
                        join p in db.PRODUCTs on s.ProductID equals p.ID
                        join c in db.CUSTOMERs on s.CustomerID equals c.ID
                        join category in db.CATEGORies on s.CategoryID equals category.ID
                        select new
                        {
                            ProductName = p.ProductName,
                            CustomerName = c.CustomerName,
                            CategoryName = category.CategoryName,
                            ProductID = s.ProductID,
                            CustomerID = s.CustomerID,
                            salesID = s.ID,
                            CategoryID = s.CategoryID,
                            SalesPrice = s.ProductSalesPrice,
                            SalesAmount = s.ProductSalesAmount,
                            SalesDate = s.SalesDate
                        }).OrderBy(x => x.SalesDate).ToList();
            foreach (var item in list)
            {
                SalesDetailDTO dto = new SalesDetailDTO();
                dto.ProductName = item.ProductName;
                dto.CustomerName = item.CustomerName;
                dto.CategoryName = item.CategoryName;
                dto.ProductID = item.ProductID;
                dto.CustomerID = item.CustomerID;
                dto.CategoryID = item.CategoryID;
                dto.SalesID = item.salesID;
                dto.Price = item.SalesPrice;
                dto.SalesAmount = item.SalesAmount;
                dto.SalesDate = item.SalesDate;
                sales.Add(dto);

            }
            return sales;



        }

        public bool Update(SALE entity)
        {
            try
            {
                SALE sales = db.SALES.First(x => x.ID == entity.ID);
                sales.ProductSalesAmount = entity.ProductSalesAmount;
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
