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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StockTracking
{
    public partial class frmDelete : Form
    {
        public frmDelete()
        {
            InitializeComponent();
        }
        SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        private void frmDelete_Load(object sender, EventArgs e)
        {
            cmbDeletedData.Items.Add("Category");
            cmbDeletedData.Items.Add("Product");
            cmbDeletedData.Items.Add("Customer");
            cmbDeletedData.Items.Add("Sales");
            dto = bll.Select(true);
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[6].HeaderText = "Sales Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Sales Date";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SalesDetailDTO saledetail = new SalesDetailDTO();
        CustomerDetailDTO customerdetail = new CustomerDetailDTO();
        CategoryDetailDTO categorydetail=new CategoryDetailDTO();
        ProductDetailDTO productdetail = new ProductDetailDTO();
        SalesBLL salesbll = new SalesBLL();
        CustomerBLL customerbll = new CustomerBLL();
        CategoryBLL categorybll = new CategoryBLL();
        ProductBLL productbll = new ProductBLL();
        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbDeletedData.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dto.Categories;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Category Name";
            }
            if (cmbDeletedData.SelectedIndex == 1)
            {
                dataGridView1.DataSource = dto.Products;
                dataGridView1.Columns[0].HeaderText = "Product Name";
                dataGridView1.Columns[1].HeaderText = "Category Name";
                dataGridView1.Columns[2].HeaderText = "Stock Amount";
                dataGridView1.Columns[3].HeaderText = "Price";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
            }
            if (cmbDeletedData.SelectedIndex == 2)
            {
                dataGridView1.DataSource = dto.Customers;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Customer Name";
            }
            if (cmbDeletedData.SelectedIndex == 3)
            {
                dataGridView1.DataSource = dto.Sales;
                dataGridView1.Columns[0].HeaderText = "Customer Name";
                dataGridView1.Columns[1].HeaderText = "Product Name";
                dataGridView1.Columns[2].HeaderText = "Category Name";
                dataGridView1.Columns[6].HeaderText = "Sales Amount";
                dataGridView1.Columns[7].HeaderText = "Price";
                dataGridView1.Columns[8].HeaderText = "Sales Date";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                categorydetail = new CategoryDetailDTO();
                categorydetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                categorydetail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (cmbDeletedData.SelectedIndex == 1)
            {
                productdetail = new ProductDetailDTO();
                productdetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                productdetail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                productdetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                productdetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            }
            if (cmbDeletedData.SelectedIndex == 2)
            {
                customerdetail = new CustomerDetailDTO();
                customerdetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerdetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }
            if (cmbDeletedData.SelectedIndex == 3)
            {
                saledetail = new SalesDetailDTO();
                saledetail.SalesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                saledetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                saledetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                saledetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                saledetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                saledetail.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            }
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {

            if (cmbDeletedData.SelectedIndex == 0)
            {
                if(categorybll.GetBack(categorydetail))
                {
                    MessageBox.Show("Category was Get back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Categories;

                }
            }
            if (cmbDeletedData.SelectedIndex == 1)
            {
                if (productbll.GetBack(productdetail))
                {
                    MessageBox.Show("product was Get back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Products;
                }
            }
            if (cmbDeletedData.SelectedIndex == 2)
            {
                if (customerbll.GetBack(customerdetail))
                {
                    MessageBox.Show("customer was Get back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Customers;
                }
            }
            if (cmbDeletedData.SelectedIndex == 3)
            {
                if (salesbll.GetBack(saledetail))
                {
                    MessageBox.Show("sales was Get back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Sales;
                }
            }
        }
    }
}
