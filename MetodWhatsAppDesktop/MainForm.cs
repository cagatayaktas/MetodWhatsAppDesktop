using MetodWhatsAppDesktop.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MetodWhatsAppDesktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        ChromeOptions options;
        WebDriver driver;

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
            CheckForIllegalCrossThreadCalls = false;

            var gridDb = bsPhone.List as IList<PhoneBookModel>;
            foreach (var item in Services.PhoneDataService.PhoneData.OrderBy(a => a.Name))
                gridDb.Add(item);

        }

        bool WhatsAppConnected = false;

        void ConnectWhatsApp()
        {
            Thread thread = new Thread(new ThreadStart(startDriver));
            thread.Start();
            void startDriver()
            {
                ChromeDriverService servis = ChromeDriverService.CreateDefaultService();
                servis.HideCommandPromptWindow = true;

                options = new ChromeOptions();
                options.AddArguments("start-maximized");
                options.AddArguments("incognito");
                options.AddExcludedArgument("enable-automation");

                driver = new ChromeDriver(servis, options);
                driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                Thread.Sleep(1000);

                btnSendProducts.Text = "Seçili Ürünleri Gönder";

                WhatsAppConnected = true;
            }
        }

        void SendMessages()
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

            bool ilkDosyaGonderildi = false;

            foreach (var phone in phones)
            {
                try
                {
                    driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone=+90{phone.Gsm}");
                    Thread.Sleep(5000);

                    foreach (var message in messages.OrderBy(a => a.Index).ToList())
                    {
                        if (message.MessageType == Enums.WhatsApp.MessageType.Text)
                        {
                            var messageArea = driver.FindElement(By.ClassName("_3Uu1_"));
                            messageArea.Click();
                            messageArea.SendKeys(message.Content);
                            driver.FindElement(By.CssSelector(".tvf2evcx.oq44ahr5.lb5m6g5c.svlsagor.p2rjqpw5.epia9gcq")).Click();
                            Thread.Sleep(3000);
                        }
                        else if (message.MessageType == Enums.WhatsApp.MessageType.Image)
                        {
                            Thread.Sleep(3000);
                            driver.FindElements(By.CssSelector("._3ndVb"))[6].Click();
                            Thread.Sleep(3000);
                            driver.FindElement(By.CssSelector("._3fV_S")).Click();
                            Thread.Sleep(2000);
                            SendKeys.Send(ilkDosyaGonderildi ? Path.GetFileName(message.Content) : message.Content);
                            Thread.Sleep(2000);
                            SendKeys.Send("{ENTER}");
                            Thread.Sleep(3000);
                            SendKeys.Send("{ENTER}");

                            ilkDosyaGonderildi = true;
                        }
                    }

                    Thread.Sleep(5000);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void btnSendProducts_Click(object sender, EventArgs e)
        {
            if (WhatsAppConnected)
                SendMessages();
            else
                ConnectWhatsApp();
        }
    }
}
