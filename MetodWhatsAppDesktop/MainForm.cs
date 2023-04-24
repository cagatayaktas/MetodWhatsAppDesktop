using MetodWhatsAppDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MetodWhatsAppDesktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGetProducts_Click(object sender, EventArgs e)
        {
            try
            {
                var apiResult = Services.ApiServices.GetProducts();

                bsProduct.List.Clear();

                bsProduct.DataSource = apiResult
                    .Where(a => a.Bakiye > 0)
                    .OrderBy(a => a.Model)
                    .ThenBy(a => a.Beden)
                    .ThenBy(a => a.Renk)
                    .ToList();

                gridProducts.DataSource = bsProduct;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucudan Ürün Listesi Alınamadı\n\r" + ex.Message);
            }

        }

        PhoneBookModel selectedPhone;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Length == 0 || tbGsm.Text.Length == 0)
                return;

            var gridDb = bsPhone.List as IList<PhoneBookModel>;

            if (selectedPhone == null)
                if (gridDb.FirstOrDefault(a => a.Gsm == tbGsm.Text) == null)
                {
                    gridDb.Add(new PhoneBookModel
                    {
                        Name = tbName.Text,
                        Gsm = tbGsm.Text
                    });
                }
                else
                {
                    selectedPhone.Name = tbName.Text;
                    selectedPhone.Gsm = tbGsm.Text;
                }

            Services.PhoneDataService.PhoneData = gridDb.ToList();

            Services.PhoneDataService.Savedata();

            btnNew_Click(null, null);
        }

        private void gridPhones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridPhones.SelectedRows.Count > 0)
            {
                var gridDb = bsPhone.List as IList<PhoneBookModel>;
                selectedPhone = gridDb[gridPhones.SelectedRows[0].Index];

                tbName.Text = selectedPhone.Name;
                tbGsm.Text = selectedPhone.Gsm;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            selectedPhone = null;

            tbName.Text = "";
            tbGsm.Text = "";

            tbName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedPhone != null)
            {
                var gridDb = bsPhone.List as IList<PhoneBookModel>;
                gridDb.Remove(selectedPhone);

                Services.PhoneDataService.PhoneData = gridDb.ToList();

                Services.PhoneDataService.Savedata();

                btnNew_Click(null, null);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var gridDb = bsPhone.List as IList<PhoneBookModel>;
            foreach (var item in Services.PhoneDataService.PhoneData.OrderBy(a => a.Name))
                gridDb.Add(item);

        }

        private void btnSendProducts_Click(object sender, EventArgs e)
        {
            var gridProductList = bsProduct.List as IList<ProductModel>;

            var products = new List<ProductModel>();

            for (int i = 0; i < this.gridProducts.RowCount; i++)
            {
                var seciliMi = this.gridProducts.Rows[i].Cells["colSelect"].Value == null 
                    ? false 
                    : Convert.ToBoolean(this.gridProducts.Rows[i].Cells["colSelect"].Value);

                if (seciliMi)
                    products.Add(gridProductList[i]);
            }

            if (products.Count == 0)
            {
                MessageBox.Show("Lütfen en az 1 adet ürün seçin");
                return;
            }


            var gridPhoneList = bsPhone.List as IList<PhoneBookModel>;

            var phones = new List<PhoneBookModel>();

            for (int i = 0; i < this.gridPhones.RowCount; i++)
            {
                var seciliMi = this.gridPhones.Rows[i].Cells["colPhoneSec"].Value == null
                    ? false
                    : Convert.ToBoolean(this.gridPhones.Rows[i].Cells["colPhoneSec"].Value);

                if (seciliMi)
                    phones.Add(gridPhoneList[i]);
            }

            if (phones.Count == 0)
            {
                MessageBox.Show("Lütfen en az 1 adet alıcı telefonu seçin");
                return;
            }

            var messages = Services.WhatsAppService.GenerateMessages(products);

            foreach (var phone in phones)
            {
                //phone.Gsm;

                foreach (var message in messages)
                {
                    if (message.MessageType == Enums.WhatsApp.MessageType.Text)
                        ;//text message

                    if (message.MessageType == Enums.WhatsApp.MessageType.Image)
                        ;//image message
                }
            }
        }
    }
}
