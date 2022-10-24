using StockTracking.DAL.DTO;
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
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CATEGORY entity)
        {
            try
            {
                db.CATEGORies.sa

            }
            catch (Exception ex)
            {

                throw ex;
            };
        }

        public List<CategoryDetailDTO> Select()
        {
            throw new NotImplementedException();
        }

        public bool Update(CATEGORY entity)
        {
            throw new NotImplementedException();
        }
    }
    
}
