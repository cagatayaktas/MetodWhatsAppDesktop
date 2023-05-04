using DevExpress.XtraEditors;
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
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        ChromeOptions options;
        WebDriver driver;

        private void btnGetProducts_Click(object sender, EventArgs e)
        {
            

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            var gridDb = bsPhone.List as IList<PhoneBookModel>;
            foreach (var item in Services.PhoneDataService.PhoneData.OrderBy(a => a.Name))
                gridDb.Add(item);
            gridControl2.RefreshDataSource();

        }

        bool WhatsAppConnected = false;

        void ConnectWhatsApp()
        {
            Thread thread = new Thread(new ThreadStart(startDriver));
            thread.Start();
            void startDriver()
            {
                try
                {
                    barWhatsApp.Enabled = false;
                    barWhatsApp.Caption = "Bağlanıyor...";

                    ChromeDriverService servis = ChromeDriverService.CreateDefaultService();
                    servis.HideCommandPromptWindow = true;

                    options = new ChromeOptions();
                    options.AddArguments("start-maximized");
                    options.AddArguments("incognito");
                    options.AddExcludedArgument("enable-automation");

                    driver = new ChromeDriver(servis, options);
                    driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                    Thread.Sleep(10000);

                    WhatsAppConnected = true;

                    barWhatsApp.Caption = "Seçili Ürünleri Gönder";

                    barWhatsApp.Enabled = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("WA Bağlantı Hatası : \n\r" + ex.ToString());
                }
            }
        }

        void SendMessages()
        {
            try
            {
                barWhatsApp.Enabled = false;
                barWhatsApp.Caption = "Gönderiliyor...";

                var gridProductList = bsProduct.List as IList<ProductModel>;

                var products = new List<ProductModel>();

                foreach (var item in gridView1.GetSelectedRows())
                {
                    products.Add((ProductModel)gridView1.GetRow(item));
                }

                if (products.Count == 0)
                {
                    MessageBox.Show("Lütfen en az 1 adet ürün seçin");
                    return;
                }


                var gridPhoneList = bsPhone.List as IList<PhoneBookModel>;

                var phones = new List<PhoneBookModel>();

                foreach (var item in gridView2.GetSelectedRows())
                {
                    phones.Add((PhoneBookModel)gridView2.GetRow(item));
                }                

                if (phones.Count == 0)
                {
                    MessageBox.Show("Lütfen en az 1 adet alıcı telefonu seçin");
                    return;
                }

                var messages = Services.WhatsAppService.GenerateMessages(products);

                //bool ilkDosyaGonderildi = false;
                
                foreach (var phone in phones)
                {
                    try
                    {
                        driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone=+90{phone.Gsm}");

                        Thread.Sleep(5000);

                        var gruplar = messages.GroupBy(a => new { a.Model, a.Beden }).OrderBy(a => a.Key.Model).ThenBy(a => a.Key.Beden).ToList();

                        foreach (var grup in gruplar)
                        {
                            Thread.Sleep(5000);

                            //başlığı gönder
                            var messageArea = driver.FindElement(By.ClassName("_3Uu1_"));
                            messageArea.Click();
                            messageArea.SendKeys($"{grup.Key.Model} ({grup.Key.Beden})");
                            driver.FindElement(By.CssSelector(".tvf2evcx.oq44ahr5.lb5m6g5c.svlsagor.p2rjqpw5.epia9gcq")).Click();

                            Thread.Sleep(5000);


                            //resimleri gönder
                            var resimler = messages.Where(a => a.Model == grup.Key.Model && a.Beden == grup.Key.Beden).ToList();

                            //ilk resmi gönder
                            driver.FindElements(By.CssSelector("._3ndVb"))[6].Click();
                            Thread.Sleep(3000);
                            driver.FindElement(By.CssSelector("._3fV_S")).Click();
                            Thread.Sleep(2000);
                            SendKeys.Send(resimler[0].Content);
                            Thread.Sleep(2000);
                            SendKeys.Send("{ENTER}");
                            Thread.Sleep(3000);
                            SendKeys.Send("{ENTER}");

                            Thread.Sleep(5000);

                            string resimAdList = "";
                            foreach (var resim in resimler)
                            {
                                if (resim.Content == resimler[0].Content)
                                    continue;

                                if (resimAdList.Length > 0)
                                    resimAdList += " ";

                                resimAdList += "\"" + resim.ResimAdi + "\"";
                            }

                            if (resimAdList.Length > 0)
                            {
                                driver.FindElements(By.CssSelector("._3ndVb"))[6].Click();
                                Thread.Sleep(3000);
                                driver.FindElement(By.CssSelector("._3fV_S")).Click();
                                Thread.Sleep(2000);
                                SendKeys.Send(resimAdList);
                                Thread.Sleep(2000);
                                SendKeys.Send("{ENTER}");
                                Thread.Sleep(3000);
                                SendKeys.Send("{ENTER}");

                                Thread.Sleep(5000);
                            }
                        }

                        Thread.Sleep(5000);

                        //Thread.Sleep(5000);

                        //foreach (var message in messages.OrderBy(a => a.Index).ToList())
                        //{
                        //    if (message.MessageType == Enums.WhatsApp.MessageType.Text)
                        //    {
                        //        var messageArea = driver.FindElement(By.ClassName("_3Uu1_"));
                        //        messageArea.Click();
                        //        messageArea.SendKeys(message.Content);
                        //        driver.FindElement(By.CssSelector(".tvf2evcx.oq44ahr5.lb5m6g5c.svlsagor.p2rjqpw5.epia9gcq")).Click();
                        //        Thread.Sleep(3000);
                        //    }
                        //    else if (message.MessageType == Enums.WhatsApp.MessageType.Image)
                        //    {
                        //        Thread.Sleep(3000);
                        //        driver.FindElements(By.CssSelector("._3ndVb"))[6].Click();
                        //        Thread.Sleep(3000);
                        //        driver.FindElement(By.CssSelector("._3fV_S")).Click();
                        //        Thread.Sleep(2000);
                        //        SendKeys.Send(ilkDosyaGonderildi ? Path.GetFileName(message.Content) : message.Content);
                        //        Thread.Sleep(2000);
                        //        SendKeys.Send("{ENTER}");
                        //        Thread.Sleep(3000);
                        //        SendKeys.Send("{ENTER}");

                        //        ilkDosyaGonderildi = true;
                        //    }
                        //}

                        //Thread.Sleep(5000);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Gönderim işleminde hata meydana geldi\n\r" + ex.Message);
                //Debug.WriteLine("Genel Hata : \n\r " + ex.ToString());
            }
            finally
            {
                XtraMessageBox.Show("Gönderim işlemi tamamlandı");
                barWhatsApp.Caption = "Seçili Ürünleri Gönder";
                barWhatsApp.Enabled = true;
            }
        }

        private void btnSendProducts_Click(object sender, EventArgs e)
        {
            if (WhatsAppConnected)
                SendMessages();
            else
                ConnectWhatsApp();
        }

        private void barGetProducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Sunucudan Ürün Listesi Alınamadı\n\r" + ex.Message);
            }
        }

        private void barWhatsApp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (WhatsAppConnected)
                SendMessages();
            else
                ConnectWhatsApp();
        }

        PhoneBookModel selectedPhone;

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                selectedPhone = (PhoneBookModel)gridView2.GetFocusedRow();

                tbName.Text = selectedPhone.Name;
                tbGsm.Text = selectedPhone.Gsm;
            }
        }

        private void btnNew1_Click(object sender, EventArgs e)
        {
            selectedPhone = null;

            tbName.Text = "";
            tbGsm.Text = "";

            tbName.Focus();
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Length == 0 || tbGsm.Text.Length == 0)
                return;

            var gridDb = bsPhone.List as IList<PhoneBookModel>;

            if (selectedPhone == null)
            {
                if (gridDb.FirstOrDefault(a => a.Gsm == tbGsm.Text) == null)
                {
                    gridDb.Add(new PhoneBookModel
                    {
                        Name = tbName.Text,
                        Gsm = tbGsm.Text
                    });
                }
            }
            else
            {
                selectedPhone.Name = tbName.Text;
                selectedPhone.Gsm = tbGsm.Text;
            }

            gridControl2.RefreshDataSource();

            Services.PhoneDataService.PhoneData = gridDb.ToList();

            Services.PhoneDataService.Savedata();

            btnNew1_Click(null, null);
        }
               

        private void btnDelete1_Click(object sender, EventArgs e)
        {
            if (selectedPhone != null)
            {
                var gridDb = bsPhone.List as IList<PhoneBookModel>;
                gridDb.Remove(selectedPhone);

                Services.PhoneDataService.PhoneData = gridDb.ToList();

                Services.PhoneDataService.Savedata();

                btnNew1_Click(null, null);
            }
        }
    }
}
