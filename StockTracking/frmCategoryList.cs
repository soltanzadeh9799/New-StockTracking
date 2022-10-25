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
    public partial class frmCategoryList : Form
    {
        public frmCategoryList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCategory frm=new frmCategory();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            dataGridView1.DataSource = dto.categories;
        }
        CategoryBLL bll = new CategoryBLL();
        CategoryDTO dto = new CategoryDTO();
        private void frmCategoryList_Load(object sender, EventArgs e)
        {
            dto=bll.Select();
            dataGridView1.DataSource = dto.categories;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Category Name";
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            List<CategoryDetailDTO> list = dto.categories;
            list = list.Where(x => x.CategoryName.Contains(txtCategory.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(detail.ID==0)
                MessageBox.Show("Please select a category from table");
            else
            {
                frmCategory frm =new frmCategory();
                frm.detail= detail;
                frm.isupdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                dto = bll.Select();
                dataGridView1.DataSource = dto.categories;
            }

        }
        CategoryDetailDTO detail=new CategoryDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new CategoryDetailDTO();
            detail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
                MessageBox.Show("Select a category from table");
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("category was deleted");
                        bll = new CategoryBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.categories;
                        txtCategory.Clear();
                    }
                }
            }
        }
    }
}
