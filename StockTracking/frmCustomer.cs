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
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        CustomerBLL bll = new CustomerBLL();
        public CustomerDetailDTO detail=new CustomerDetailDTO();
        public bool isupdate = false;
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            if (isupdate)
                txtCustomerName.Text = detail.CustomerName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtCustomerName.Text.Trim()=="")
                MessageBox.Show("Customer Name is Empty");
            else
            {
                if (!isupdate)
                {
                    CustomerDetailDTO customer = new CustomerDetailDTO();
                    customer.CustomerName = txtCustomerName.Text;
                    if (bll.Insert(customer))
                    {
                        MessageBox.Show("customer was Added");
                        this.Close();
                    }
                }
                else if(isupdate)
                {
                    if(detail.CustomerName==txtCustomerName.Text.Trim())
                        MessageBox.Show("there is no change");
                    else
                    {
                        detail.CustomerName=txtCustomerName.Text;
                        if(bll.Update(detail))
                        {
                            MessageBox.Show("customer was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
