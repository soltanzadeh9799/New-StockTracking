using StockTracking.BLL;
using StockTracking.DAL;
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
    public partial class frmCustomerList : Form
    {
        public frmCustomerList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            dataGridView1.DataSource = dto.customers;
        }
        CustomerBLL bll = new CustomerBLL();
        CustomerDTO dto = new CustomerDTO();
        CustomerDetailDTO detail=new CustomerDetailDTO();
        private void frmCustomerList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.customers;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Customer Name";

        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(detail.ID==0)
                MessageBox.Show("Please select a customer From tabe");
            else
            {
                frmCustomer frm = new frmCustomer();
                frm.detail = detail;
                frm.isupdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new CustomerBLL();
                dto = bll.Select();
                dataGridView1.DataSource=dto.customers;



            }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail=new CustomerDetailDTO();
            detail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
                MessageBox.Show("Select a customer from table");
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Customer was deleted");
                        bll = new CustomerBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.customers;
                        txtCustomerName.Clear();
                    }
                }
            }
        }
    }
}
