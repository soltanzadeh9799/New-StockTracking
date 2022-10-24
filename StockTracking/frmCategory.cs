using StockTracking.BLL;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockTracking
{
    public partial class frmCategory : Form
    {
        public frmCategory()
        {
            InitializeComponent();
        }
        CategoryBLL bll=new CategoryBLL();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtCategoryName.Text.Trim()=="")
                MessageBox.Show("Category Name is Empty");
            else
            {
                CategoryDetailDTO category = new CategoryDetailDTO();
                category.CategoryName = txtCategoryName.Text;
                if(bll.Insert(category))
                {
                    MessageBox.Show("category was Added");
                    txtCategoryName.Clear();
                }
            }
        }
    }
}
