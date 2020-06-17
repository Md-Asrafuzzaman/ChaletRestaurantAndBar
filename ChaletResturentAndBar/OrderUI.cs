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
using Newtonsoft.Json.Linq;

namespace ChaletResturentAndBar
{
    public partial class OrderUI : Form
    {
        public class DataObject
        {
            public string Name { get; set; }
            public string id { get; set; }
            public string chair_name { get; set; }
            public string table_name { get; set; }
            public string final_total { get; set; }
            public string invoice_no { get; set; }
            public string waiter { get; set; }
            public string total_product { get; set; }
            public string complete { get; set; }
            public string drink_status { get; set; }


        }

        public OrderUI()
        {
            InitializeComponent();
        }

        DataTable dataTable = new DataTable();
        private void OrderUI_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("#Invoice");
            dataTable.Columns.Add("Table");
            dataTable.Columns.Add("Chair");
            dataTable.Columns.Add("Waiter");
            dataTable.Columns.Add("Amount");
            dataTable.Columns.Add("Total Product");
            dataTable.Columns.Add("Complete");
            dataTable.Columns.Add("Drink Status");
            dataTable.Columns.Add("Action");
            orderListDataGridView.DataSource = dataTable;
        }
        private const string URL = "https://softitsecurity.com/chalet_test/api/getTestOrder/";
        private string urlParameters = "?api_key=123";
        private void OrderButton_Click(object sender, EventArgs e)
        {
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
                DataTable dataTable = (DataTable)orderListDataGridView.DataSource;
                DataRow drToAdd = dataTable.NewRow();
                drToAdd[0] = d.invoice_no;
                drToAdd[1] = d.table_name;
                drToAdd[2] = d.chair_name;
                drToAdd[3] = d.waiter;
                drToAdd[4] = d.final_total;
                drToAdd[5] = d.total_product;
                drToAdd[6] = d.complete;
                drToAdd[7] = d.drink_status;
                drToAdd[8] = "Details";
                dataTable.Rows.Add(drToAdd);
                dataTable.AcceptChanges();
            }
            client.Dispose();
        }
        private void orderListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (orderListDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string value = orderListDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                OrderDetailsUI form2 = new OrderDetailsUI(value);
                form2.ShowDialog(); 
                //MessageBox.Show(orderListDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
    }
}
