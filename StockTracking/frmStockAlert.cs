﻿using StockTracking.BLL;
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
    public partial class frmStockAlert : Form
    {
        public frmStockAlert()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            frmStockTracking frm = new frmStockTracking();
            this.Hide();
            frm.ShowDialog();
        }
        ProductBLL bll = new ProductBLL();
        ProductDTO dto=new ProductDTO();
        private void frmStockAlert_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dto.Products=dto.Products.Where(x=>x.StockAmount<=100).ToList();
            dataGridView1.DataSource=dto.Products;
            dataGridView1.Columns[0].HeaderText = "Product Name";
            dataGridView1.Columns[1].HeaderText = "Category Name";
            dataGridView1.Columns[2].HeaderText = "Stock Amount";
            dataGridView1.Columns[3].HeaderText = "Price";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            if(dto.Products.Count<=0)
            {
                frmStockTracking frm = new frmStockTracking();
                this.Hide();
                frm.ShowDialog();
            }
        }
    }
}
