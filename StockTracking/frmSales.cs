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
    public partial class frmSales : Form
    {
        public frmSales()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();  
        private void frmSales_Load(object sender, EventArgs e)
        {
            cmbCategorySearchName.DataSource = dto.Categories;
            cmbCategorySearchName.DisplayMember = "CategoryName";
            cmbCategorySearchName.ValueMember = "ID";
            cmbCategorySearchName.SelectedIndex = -1;
            if (dto.Categories.Count > 0)
                combofull = true;

            dgProductList.DataSource = dto.Products;
            dgProductList.Columns[0].HeaderText = "Product Name";
            dgProductList.Columns[1].HeaderText = "Category Name";
            dgProductList.Columns[2].HeaderText = "Stock Amount";
            dgProductList.Columns[3].HeaderText = "Price";
            dgProductList.Columns[4].Visible = false;
            dgProductList.Columns[5].Visible = false;

            dgCustomer.DataSource = dto.Customers;
            dgCustomer.Columns[0].Visible = false;
            dgCustomer.Columns[1].HeaderText = "Customer Name";
           

        }
        bool combofull = false;
        private void cmbCategorySearchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategorySearchName.SelectedValue)).ToList();
                dgProductList.DataSource = list;
                if(list.Count == 0)
                {
                    txtProductName.Clear();
                    txtProductPrice.Clear();
                    txtProductStock.Clear();

                }
            }
        }

        private void txtCustomerSearchName_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomerSearchName.Text)).ToList();
            dgCustomer.DataSource=list;
            if (list.Count == 0)
            {
                txtCustomerName.Clear();   
            }

        }

        SalesDetailDTO detail=new SalesDetailDTO();
        private void dgProductList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = dgProductList.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Price = Convert.ToInt32(dgProductList.Rows[e.RowIndex].Cells[3].Value);
            detail.StockAmount = Convert.ToInt32(dgProductList.Rows[e.RowIndex].Cells[2].Value);
            detail.ProductID = Convert.ToInt32(dgProductList.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryID = Convert.ToInt32(dgProductList.Rows[e.RowIndex].Cells[5].Value);
            txtProductName.Text=detail.ProductName;
            txtProductPrice.Text = detail.Price.ToString();
            txtProductStock.Text=detail.StockAmount.ToString();


        }

        private void dgCustomerList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.CategoryName = dgCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CustomerID=Convert.ToInt32(dgCustomer.Rows[e.RowIndex].Cells[0].Value);
            txtCustomerName.Text=detail.CustomerName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(detail.ProductID==0)
                MessageBox.Show("Please select a Product from Product Table");
            else if (detail.CustomerID==0)
                MessageBox.Show("Please select a customer from Customer Table");
            else if (detail.StockAmount<Convert.ToInt32(txtProductSalesAmount.Text))
                MessageBox.Show("you have bot enough product for sale");
            else
            {
                detail.SalesAmount=Convert.ToInt32(txtProductSalesAmount.Text);
                detail.SalesDate = DateTime.Today;
                if(bll.Insert(detail))
                {
                    MessageBox.Show("Sales was added");
                    bll = new SalesBLL();
                    dto = bll.Select();
                    dgProductList.DataSource = dto.Products;
                    dto.Customers=dto.Customers;
                    combofull = false;
                    cmbCategorySearchName.DataSource = dto.Categories;
                    if (dto.Products.Count > 0)
                        combofull = true;
                    txtProductSalesAmount.Clear();
                }



            }
        }
    }
}
