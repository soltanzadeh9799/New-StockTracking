﻿using System;
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
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }
    }
}
