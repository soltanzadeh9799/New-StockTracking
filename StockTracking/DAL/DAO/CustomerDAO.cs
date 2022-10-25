﻿using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class CustomerDAO : StockContext, IDAO<CUSTOMER, CustomerDetailDTO>
    {
        public bool Delete(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == entity.ID);
                customer.isDeleted = true;
                customer.DeletedDate = DateTime.Today;
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
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == ID);
                customer.isDeleted = false;
                customer.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Insert(CUSTOMER entity)
        {
            try
            {
                db.CUSTOMERs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CustomerDetailDTO> Select()
        {
            List<CustomerDetailDTO> customer = new List<CustomerDetailDTO>();
            var list = db.CUSTOMERs.Where(x=>x.isDeleted==false).ToList();
            foreach (var item in list)
            {
                CustomerDetailDTO dto = new CustomerDetailDTO();
                dto.ID = item.ID;
                dto.CustomerName = item.CustomerName;
                customer.Add(dto);
            }
            return customer;
        }
        public List<CustomerDetailDTO> Select(bool isDeleted)
        {
            List<CustomerDetailDTO> customer = new List<CustomerDetailDTO>();
            var list = db.CUSTOMERs.Where(x => x.isDeleted == isDeleted).ToList();
            foreach (var item in list)
            {
                CustomerDetailDTO dto = new CustomerDetailDTO();
                dto.ID = item.ID;
                dto.CustomerName = item.CustomerName;
                customer.Add(dto);
            }
            return customer;
        }

        public bool Update(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERs.First(x => x.ID == entity.ID);
                customer.CustomerName = entity.CustomerName;
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
