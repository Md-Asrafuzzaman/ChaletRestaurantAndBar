using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ChaletResturentAndBar
{
    public partial class OrderDetailsUI : Form
    {
        public OrderDetailsUI(string value)
        {
            InitializeComponent();
            //MessageBox.Show(value);
            sellsInvoiceLabel.Text = value;
            invoiceNoLabel.Text = value;
        }
        public class DataObject
        {
            public string name { get; set; }
            public string quantity { get; set; }
            public string unit_price { get; set; }
            public string discount_amount { get; set; }
            public string unit_price_inc_tax { get; set; }
            public string item_tax { get; set; }
            public string sub_table { get; set; }
            public string status { get; set; }


        }
        DataTable dataTable = new DataTable();
        private const string URL = "https://softitsecurity.com/chalet_test/api/getTestOrderDetails/21811";
        private string urlParameters = "?api_key=123";
        private void OrderDetailsUI_Load(object sender, EventArgs e)
        {

            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "checkBoxColumn";
            productsDataGridView.Columns.Insert(0, checkBoxColumn);
            dataTable.Columns.Add("Product");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Unit Price");
            dataTable.Columns.Add("Discount");
            dataTable.Columns.Add("Tax");
            dataTable.Columns.Add("Price inc.Tax");
            dataTable.Columns.Add("Sub Toatal");
            dataTable.Columns.Add("Status");
            productsDataGridView.DataSource = dataTable;
            //MessageBox.Show("Ja Issa tai");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

            // Parse the response body.
            var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            foreach (var d in dataObjects)
            {
                DataTable dataTable = (DataTable)productsDataGridView.DataSource;
                DataRow drToAdd = dataTable.NewRow();
                drToAdd[0] = d.name;
                drToAdd[1] = d.unit_price;
                drToAdd[2] = d.discount_amount;
                drToAdd[3] = d.item_tax;
                drToAdd[4] = d.unit_price_inc_tax;
                drToAdd[5] = d.sub_table;
                drToAdd[6] = "Pending";
                dataTable.Rows.Add(drToAdd);
                dataTable.AcceptChanges();
            }
            client.Dispose();

        }
        

       
    }
}
