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
    public partial class frmStockTracking : Form
    {
        public frmStockTracking()
        {
            InitializeComponent();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            frmSalesList frm =new frmSalesList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProductList frm =new frmProductList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomerList frm =new frmCustomerList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnDeleted_Click(object sender, EventArgs e)
        {
            frmDelete frm = new frmDelete();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategoryList frm=new frmCategoryList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            frmAddStock frm=new frmAddStock();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
