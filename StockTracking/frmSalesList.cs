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
    public partial class frmSalesList : Form
    {
        public frmSalesList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProductPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSales frm=new frmSales();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }
        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        private void frmSalesList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[6].HeaderText = "Sales Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Sales Date";
            dataGridView1.Columns[3].Visible= false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;

            cmbCategoryName.DataSource=dto.Categories;
            cmbCategoryName.DisplayMember = "CategoryName";
            cmbCategoryName.ValueMember = "ID";
            cmbCategoryName.SelectedIndex = -1;




        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalesDetailDTO> list=dto.Sales;
            if (txtProductName.Text.Trim() != "")
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            if(txtCustomerName.Text.Trim()!="")
                list=list.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();   
            if(cmbCategoryName.SelectedIndex!=-1)
                list=list.Where(x=>x.CategoryID==Convert.ToInt32(cmbCategoryName.SelectedValue)).ToList();  
            if(txtProductPrice.Text.Trim()!="")
            {
                if (rbPriceEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtProductPrice.Text)).ToList();
                if (rbPriceMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtProductPrice.Text)).ToList();
                if (rbPriceLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtProductPrice.Text)).ToList();
                else
                    MessageBox.Show("Please Select a criterion from price group");

            }
            if (txtSalesAmount.Text.Trim() != "")
            {
                if (rbSalesEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtSalesAmount.Text)).ToList();
                if (rbSalesMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtSalesAmount.Text)).ToList();
                if (rbSalesLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtSalesAmount.Text)).ToList();
                else
                    MessageBox.Show("Please Select a criterion from sale Amount group");

            }
            if (chDate.Checked)
                list = list.Where(x => x.SalesDate > dpStart.Value && x.SalesDate < dpEnd.Value).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilter();
        }

        private void CleanFilter()
        {
            txtProductPrice.Clear();
            txtSalesAmount.Clear();
            txtCustomerName.Clear();
            txtProductName.Clear();
            rbPriceEqual.Checked = false;
            rbSalesMore.Checked = false;    
            rbPriceLess.Checked = false;
            rbSalesLess.Checked = false;
            rbPriceMore.Checked = false;
            rbSalesMore.Checked = false;
            chDate.Checked = false;
            dpStart.Value = DateTime.Today;
            dpEnd.Value = DateTime.Today;
            cmbCategoryName.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Sales;
            
        }
    }
}
