using DevExpress.XtraEditors;
using MetodWhatsAppDesktop.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        void CapsLockOff()
        {
            if (Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
            {
                const int KEYEVENTF_EXTENDEDKEY = 0x1;
                const int KEYEVENTF_KEYUP = 0x2;
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                (UIntPtr)0);
                Thread.Sleep(1000);
            }
        }

        private void btnGetProducts_Click(object sender, EventArgs e)
        {


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            barImageButtonIndex.EditValue = Properties.Settings.Default.ImageButtonIndex;

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

                    barTest.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barBekleme.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

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

                CapsLockOff();

                var messages = Services.WhatsAppService.GenerateMessages(products);

                bool ilkDosyaGonderildi = false;

                var specOrder = new List<string> { "F", "P", "B" };

                foreach (var phone in phones)
                {
                    try
                    {
                        string phoneNumber = phone.Gsm;
                        if (String.IsNullOrEmpty(phone.UlkeKodu))
                            phoneNumber = "+90" + phoneNumber;
                        else
                            phoneNumber = "+" + phone.UlkeKodu + phone.Gsm;

                        driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={phoneNumber}");

                        var bekleme = Convert.ToInt32(barBekleme.EditValue);
                        bekleme = bekleme * 1000;
                        if (bekleme <= 0)
                            bekleme = 5000;

                        Thread.Sleep(bekleme);

                        var gruplar = messages.GroupBy(a => new { a.Model, a.Beden }).OrderBy(a => a.Key.Model)
                            .ThenBy(a => specOrder.IndexOf(a.Key.Beden.Substring(0, 1)))
                            //.ThenBy(a => a.Key.Beden)
                            .ToList();

                        int imageIndex = Convert.ToInt32(barImageButtonIndex.EditValue);

                        foreach (var grup in gruplar)
                        {
                            bool ilkResimMi = false;

                            //resimleri gönder
                            var resimler = messages.Where(a => a.Model == grup.Key.Model && a.Beden == grup.Key.Beden).ToList();

                            if (ilkDosyaGonderildi == false)
                            {
                                //ilk resmi gönder
                                //driver.FindElements(By.CssSelector("._3ndVb"))[6].Click();
                                //driver.FindElements(By.CssSelector("._3ndVb"))[7].Click();                                
                                driver.FindElements(By.CssSelector(".x11xpdln.x1d8287x.x1h4ghdb"))[0].Click();
                                Thread.Sleep(1000);
                                //driver.FindElement(By.CssSelector("._3fV_S")).Click();                                
                                driver.FindElements(By.CssSelector(".x1i64zmx.x1emribx"))[imageIndex].Click();
                                //x1i64zmx x1emribx
                                Thread.Sleep(1000);
                                SendKeys.Send(resimler[0].Content);
                                Thread.Sleep(1000);
                                SendKeys.Send("{ENTER}");
                                Thread.Sleep(1500);
                                //SendKeys.Send("{ENTER}");
                                //driver.FindElements(By.CssSelector("._ajx2"))[0].Click();
                                //x78zum5 x6s0dn4 xl56j7k xexx8yu x4uap5 x18d9i69 xkhd6sd x1f6kntn xk50ysn x7o08j2 xtvhhri x1rluvsa x14yjl9h xudhj91 x18nykt9 xww2gxu xu306ak x12s1jxh xkdsq27 xwwtwea x1gfkgh9 x1247r65 xng8ra
                                driver.FindElements(By.CssSelector(".x78zum5.x6s0dn4.xl56j7k.xexx8yu.x4uap5.x18d9i69.xkhd6sd.x1f6kntn.xk50ysn.x7o08j2.xtvhhri.x1rluvsa.x14yjl9h.xudhj91.x18nykt9.xww2gxu.xu306ak.x12s1jxh.xkdsq27.xwwtwea.x1gfkgh9.x1247r65.xng8ra"))[0].Click();
                                Thread.Sleep(2000);

                                ilkDosyaGonderildi = true;
                                ilkResimMi = true;

                            }


                            string resimAdList = "";
                            foreach (var resim in resimler)
                            {
                                if (ilkResimMi && resim.ResimAdi == resimler[0].ResimAdi) continue;

                                if (resimAdList.Length > 0)
                                    resimAdList += " ";

                                resimAdList += "\"" + resim.ResimAdi + "\"";
                            }

                            if (resimAdList.Length > 0)
                            {
                                //driver.FindElements(By.CssSelector("._3ndVb"))[6].Click();
                                //driver.FindElements(By.CssSelector("._3ndVb"))[7].Click();
                                driver.FindElements(By.CssSelector(".x11xpdln.x1d8287x.x1h4ghdb"))[0].Click();
                                Thread.Sleep(1000);
                                //driver.FindElement(By.CssSelector("._3fV_S")).Click();
                                //driver.FindElements(By.CssSelector(".erpdyial.tviruh8d.gfz4du6o.r7fjleex.lhj4utae.le5p0ye3"))[1].Click();
                                driver.FindElements(By.CssSelector(".x1i64zmx.x1emribx"))[imageIndex].Click();
                                Thread.Sleep(1000);
                                SendKeys.Send(resimAdList);
                                Thread.Sleep(1000);
                                SendKeys.Send("{ENTER}");
                                Thread.Sleep(1500);
                                //SendKeys.Send("{ENTER}");
                                //driver.FindElements(By.CssSelector("._ajx2"))[0].Click();
                                driver.FindElements(By.CssSelector(".x78zum5.x6s0dn4.xl56j7k.xexx8yu.x4uap5.x18d9i69.xkhd6sd.x1f6kntn.xk50ysn.x7o08j2.xtvhhri.x1rluvsa.x14yjl9h.xudhj91.x18nykt9.xww2gxu.xu306ak.x12s1jxh.xkdsq27.xwwtwea.x1gfkgh9.x1247r65.xng8ra"))[0].Click();

                                if (resimler.Count <= 1)
                                    Thread.Sleep(3000);
                                else if (resimler.Count <= 3)
                                    Thread.Sleep(5000);
                                else if (resimler.Count <= 5)
                                    Thread.Sleep(7000);
                                else
                                    Thread.Sleep(10000);
                            }


                            //başlığı gönder
                            //var messageArea = driver.FindElement(By.ClassName("_3Uu1_"));
                            var messageArea = driver.FindElements(By.CssSelector(".selectable-text.copyable-text.x15bjb6t.x1n2onr6"))[1];
                            messageArea.Click();
                            messageArea.SendKeys($"{grup.Key.Beden}");
                            //driver.FindElement(By.CssSelector(".tvf2evcx.oq44ahr5.lb5m6g5c.svlsagor.p2rjqpw5.epia9gcq")).Click();
                            //SendKeys.Send("{ENTER}");                            
                            driver.FindElements(By.CssSelector("._ak1t._ak1u"))[0].Click();

                            Thread.Sleep(1500);
                        }

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
                tbUlkeKodu.Text = selectedPhone.UlkeKodu ?? "";
                tbGsm.Text = selectedPhone.Gsm;
            }
        }

        private void btnNew1_Click(object sender, EventArgs e)
        {
            selectedPhone = null;

            tbName.Text = "";
            tbUlkeKodu.Text = "";
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
                        Gsm = tbGsm.Text,
                        UlkeKodu = tbUlkeKodu.Text,
                    });
                }
            }
            else
            {
                selectedPhone.Name = tbName.Text;
                selectedPhone.Gsm = tbGsm.Text;
                selectedPhone.UlkeKodu = tbUlkeKodu.Text;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WhatsAppConnected && driver != null)
                try
                {
                    Properties.Settings.Default.ImageButtonIndex = Convert.ToInt32(barImageButtonIndex.EditValue);
                    Properties.Settings.Default.Save();
                    driver.Close();
                    driver.Dispose();
                }
                catch (Exception ex)
                {
                    var msg = ex.ToString();
                }
        }

        private void barTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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

            try
            {
                var phone = phones.FirstOrDefault();

                string phoneNumber = phone.Gsm;
                if (!String.IsNullOrEmpty(phone.UlkeKodu))
                    phoneNumber = "+" + phone.UlkeKodu + phone.Gsm;
                
                driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={phoneNumber}");

                //barTest.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test İşleminde Hata Oluştu\n\r" + ex.ToString());
            }

        }
    }
}
