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
        SalesDetailDTO detail = new SalesDetailDTO();
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
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;


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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new SalesDetailDTO();
            detail.SalesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            detail.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(detail.SalesID==0)
                MessageBox.Show("Please select a sales from table");
            else
            {
                frmSales frm = new frmSales();
                frm.detail = detail;
                frm.dto = dto;
                frm.isupdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new SalesBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Sales;
                CleanFilter();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(detail.SalesID==0)
                MessageBox.Show("Select a salea from table");
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?","Warning",MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    if(bll.Delete(detail))
                    {
                        MessageBox.Show("sale was deleted");
                        bll = new SalesBLL();
                        dto=bll.Select();
                        dataGridView1.DataSource = dto.Sales;
                        CleanFilter();
                    }
                }
            }
        }
    }
}
