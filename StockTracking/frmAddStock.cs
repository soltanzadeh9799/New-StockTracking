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
    public partial class frmAddStock : Form
    {
        public frmAddStock()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        private void frmAddStock_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource=dto.Products;
            dataGridView1.Columns[0].HeaderText = "Product Name";
            dataGridView1.Columns[1].HeaderText = "Category Name";
            dataGridView1.Columns[2].HeaderText = "Stock Amount";
            dataGridView1.Columns[3].HeaderText = "Price";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

            cmbCategoryName.DataSource = dto.Categories;
            cmbCategoryName.DisplayMember = "CategoryName";
            cmbCategoryName.ValueMember = "ID";
            cmbCategoryName.SelectedIndex = -1;
            if (dto.Categories.Count > 0)
                combofull = true;

        }
        bool combofull = false;
        private void cmbCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategoryName.SelectedValue)).ToList();
                dataGridView1.DataSource = list;
                if(list.Count ==0)
                {
                    txtProductPrice.Clear();
                    txtProductName.Clear();
                    txtProductStock.Clear();    
                }
            }
        }
        ProductDetailDTO detail = new ProductDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductName.Text = detail.ProductName;
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            txtProductPrice.Text= detail.Price.ToString();
            detail.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            detail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtProductName.Text.Trim()=="")
                MessageBox.Show("Please select a product From Table");
            else if (txtProductStock.Text.Trim()=="")
                MessageBox.Show("Please give a stock Amount");
            else
            {
                int sumstock = detail.StockAmount;
                sumstock += Convert.ToInt32(txtProductStock.Text);
                detail.StockAmount = sumstock;
                if(bll.Update(detail))
                {
                    MessageBox.Show("Stock was Added");
                    dto = bll.Select();
                    dataGridView1.DataSource = dto.Products;
                    txtProductStock.Clear();
                }
            }
        }
    }
}
