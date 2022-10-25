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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }
        public ProductDTO dto = new ProductDTO();
        ProductBLL bll = new ProductBLL();
        public ProductDetailDTO detail = new ProductDetailDTO();
        public bool isupdate = false;
        private void frmProduct_Load(object sender, EventArgs e)
        {
            cmbCategoryName.DataSource = dto.Categories;
            cmbCategoryName.DisplayMember = "CategoryName";
            cmbCategoryName.ValueMember = "ID";
            cmbCategoryName.SelectedIndex = -1;
            if(isupdate)
            {
                txtProductName.Text = detail.ProductName;
                txtPrice.Text = detail.Price.ToString();
                cmbCategoryName.SelectedValue = detail.CategoryID; 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtProductName.Text.Trim()=="")
                MessageBox.Show("Product name is empty");
            else if(txtPrice.Text.Trim()=="")
                MessageBox.Show("Price is Empty");
            else if (cmbCategoryName.SelectedIndex==-1)
                MessageBox.Show("please select a CategoryName");
            else
            {
                if (!isupdate)
                {
                    ProductDetailDTO product = new ProductDetailDTO();
                    product.ProductName = txtProductName.Text;
                    product.CategoryID = Convert.ToInt32(cmbCategoryName.SelectedValue);
                    product.Price = Convert.ToInt32(txtPrice.Text);
                    if (bll.Insert(product))

                    {
                        MessageBox.Show("Product was added");
                        txtProductName.Clear();
                        cmbCategoryName.SelectedIndex = -1;
                        txtPrice.Clear();
                    }
                }
                else 
                    if(isupdate)
                {
                    if(detail.ProductName==txtProductName.Text && 
                        detail.CategoryID==Convert.ToInt32(cmbCategoryName.SelectedValue)&&
                        detail.Price==Convert.ToInt32(txtPrice.Text)
                        )
                        MessageBox.Show("There is No Change");
                    else
                    {
                        detail.ProductName = txtProductName.Text;
                        detail.CategoryID = Convert.ToInt32(cmbCategoryName.SelectedValue);
                        detail.Price = Convert.ToInt32(txtPrice.Text);
                        if(bll.Update(detail))
                        {
                            MessageBox.Show("Product was Updated");
                            this.Close();
                        }
                    }
                }

            }
        }
    }
}
