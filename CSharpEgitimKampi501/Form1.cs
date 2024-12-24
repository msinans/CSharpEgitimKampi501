using CSharpEgitimKampi501.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi501
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Server=DESKTOP-K4R9NTU; initial Catalog = EgitimKampi501Db; integrated security = true; ");
        private async void btnList_Click(object sender, EventArgs e)
        {
            string query = "Select * from TblProduct";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource= values;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "Insert into TblProduct(ProductName, ProductStock, ProductPrice, ProductCategory) values (@productName, @productStock, @productPrice, @productCategory)";
            var parametres = new DynamicParameters();
            parametres.Add("@productName", txtProductName.Text);
            parametres.Add("@productStock", txtProductStock.Text);
            parametres.Add("@productPrice",txtProductPrice.Text);
            parametres.Add("@productCategory", txtProductCategory.Text);
            await connection.ExecuteAsync(query, parametres);
            MessageBox.Show("Yeni Kitap Eklendi");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete From TblProduct Where ProductId = @productId";
            var parametres = new DynamicParameters();
            parametres.Add("@productId", txtProductId.Text);
            await connection.ExecuteAsync(query, parametres);
            MessageBox.Show("Kitap Silindi!");

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update Tblproduct Set ProductName = @productName, ProductPrice = @productPrice, ProductStock = @productStock, ProductCategory =@productCategory Where ProductId = @productId";
            var parametres = new DynamicParameters();
            parametres.Add ("@productName", txtProductName.Text);
            parametres.Add("@productPrice", txtProductPrice.Text);
            parametres.Add("@productStock", txtProductStock.Text);
            parametres.Add("@productCategory", txtProductCategory.Text );
            parametres.Add("@productId", txtProductId.Text );
            await connection.ExecuteAsync(query, parametres);
            MessageBox.Show("Kitap Günceleme İşlemi Başarılı Şekilde Yapıldı!", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information); 

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string query1 = "Select Count(*) From TblProduct";
            var productTotalCount = await connection.QueryFirstOrDefaultAsync<int>(query1);
            lblTotalProductCount.Text = productTotalCount.ToString();

            string query2 = "Select ProductName From TblProduct Where ProductPrice = (Select Max(ProductPrice) From TblProduct)";
            var maxPriceProductName = await connection.QueryFirstOrDefaultAsync<string>(query2);
            lblMaxPriceProductName.Text = maxPriceProductName.ToString();

            string query3 = "Select Count(Distinct(ProductCategory)) From TblProduct";
            var distinctProductCount = await connection.QueryFirstOrDefaultAsync<int>(query3);
            lblDistinctCategoryCount.Text = distinctProductCount.ToString();

        }       
    }
}
/*
       string query = "Select * from TblProduct";
       var values = await connection.QueryAsync<ResultProductDto>(query);
       dataGridView1.DataSource= values;
           */