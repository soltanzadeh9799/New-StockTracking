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
    public partial class frmProductList : Form
    {
        public frmProductList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProductStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void txtProductPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProduct frm=new frmProduct();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            dataGridView1.DataSource = dto.Products;
            ClearFilter();
        }
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        private void frmProductList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            cmbCategoryName.DataSource = dto.Categories;
            cmbCategoryName.DisplayMember = "CategoryName";
            cmbCategoryName.ValueMember = "ID";
            cmbCategoryName.SelectedIndex = -1;

            dataGridView1.DataSource = dto.Products;
            dataGridView1.Columns[0].HeaderText = "Product Name";
            dataGridView1.Columns[1].HeaderText = "Category Name";
            dataGridView1.Columns[2].HeaderText = "Stock Amount";
            dataGridView1.Columns[3].HeaderText = "Price";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ProductDetailDTO> list=dto.Products;
            if (txtProductName.Text.Trim() != null)
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            if (cmbCategoryName.SelectedIndex != -1)
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategoryName.SelectedValue)).ToList();
            if(txtProductPrice.Text.Trim()!="")
            {
                if (rbPriceEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtProductPrice.Text)).ToList();
            else if (rbPriceMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtProductPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtProductPrice.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from price group");
            }
            if (txtProductStock.Text.Trim() != "")
            {
                if (rbStockEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtProductStock.Text)).ToList();
                else if (rbStockMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtProductStock.Text)).ToList();
                else if (rbStockLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtProductStock.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from Stock group");
            }
            dataGridView1.DataSource = list;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            ClearFilter();
        }

        private void ClearFilter()
        {
            txtProductPrice.Clear();
            txtProductName.Clear();
            txtProductStock.Clear();
            cmbCategoryName.SelectedIndex = -1;
            rbPriceEqual.Checked = false;
            rbStockEqual.Checked = false;
            rbPriceLess.Checked = false;
            rbStockLess.Checked = false;
            rbPriceMore.Checked = false;
            rbStockMore.Checked = false;
            dataGridView1.DataSource = dto.Products;
        }
        ProductDetailDTO detail = new ProductDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new ProductDetailDTO();
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(detail.ProductID==0)
                MessageBox.Show("Please select a product from table");
            else
            {
                frmProduct frm = new frmProduct();
                frm.detail = detail;
                frm.dto = dto;
                frm.isupdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new ProductBLL();
                dto=bll.Select();
                dataGridView1.DataSource = dto.Products;
                ClearFilter();

            }
        }
    }
}
