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
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
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
            var list = db.CUSTOMERs;
            foreach (var item in list)
            {
                CustomerDetailDTO dto = new CustomerDetailDTO();
                dto.ID = item.ID;
                dto.CustomerName=item.CustomerName;
                customer.Add(dto);
            }
            return customer;
        }

        public bool Update(CUSTOMER entity)
        {
            throw new NotImplementedException();
        }
    }
}
