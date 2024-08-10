using DevExpress.XtraEditors;
using MetodWhatsAppDesktop.Models;
using MetodWhatsAppDesktop.Services;
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
                
        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            RehberListele();
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
            try
            {
                barWhatsApp.Enabled = false;
                barWhatsApp.Caption = "Gönderiliyor...";

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

                var phones = new List<WATelefonRehberi>();

                foreach (var item in gridView2.GetSelectedRows())
                {
                    phones.Add((WATelefonRehberi)gridView2.GetRow(item));
                }

                if (phones.Count == 0)
                {
                    MessageBox.Show("Lütfen en az 1 adet alıcı telefonu seçin");
                    return;
                }

                var WaMessageModel = new WAMessageRequestService();

                foreach (var item in products)
                {
                    WaMessageModel.StokIds.Add(item.StokId);                    
                }

                foreach (var item in phones)
                {
                    WaMessageModel.Recipients.Add($"{item.UlkeKodu}{item.Telefon}");
                }

                if(WAServices.SendMessages(WaMessageModel))
                    XtraMessageBox.Show("Gönderim işlemi tamamlandı");
                else
                    XtraMessageBox.Show("Gönderim işleminde hata meydana geldi");

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Gönderim işleminde hata meydana geldi\n\r" + ex.Message);
            }
            finally
            {
                barWhatsApp.Caption = "Seçili Ürünleri Gönder";
                barWhatsApp.Enabled = true;
            }
        }

        WATelefonRehberi selectedPhone;

        void RehberListele()
        {
            var gridDb = bsPhone.List as IList<WATelefonRehberi>;
            gridDb.Clear();

            var rehber = Services.WAServices.GetRehber();

            foreach (var item in rehber)
                gridDb.Add(item);

            gridControl2.RefreshDataSource();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            if (gridView2.FocusedRowHandle >= 0)
            {
                selectedPhone = (WATelefonRehberi)gridView2.GetFocusedRow();

                tbName.Text = selectedPhone.AdSoyadUnvan;
                tbUlkeKodu.Text = selectedPhone.UlkeKodu ?? "";
                tbGsm.Text = selectedPhone.Telefon;
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

            var model = new WATelefonRehberi
            {
                TelefonId = selectedPhone?.TelefonId ?? 0,
                AdSoyadUnvan = tbName.Text,
                UlkeKodu = tbUlkeKodu.Text,
                Telefon = tbGsm.Text,
            };

            WAServices.AddUpdateRehber(model);

            RehberListele();

            btnNew1_Click(null, null);
        }

        private void btnDelete1_Click(object sender, EventArgs e)
        {
            if (selectedPhone != null)
            {
                WAServices.DeleteRehber(selectedPhone?.TelefonId ?? 0);

                RehberListele();

                btnNew1_Click(null, null);
            }
        }                
    }
}