using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class WSubTran : Form
    {
        string _url;
        CookieCollection _cookies = new CookieCollection();
        public WSubTran(string url, CookieCollection cookies)
        {
            _url = url;
            _cookies = cookies;            
            InitializeComponent();
            wb.ScriptErrorsSuppressed = true;            
        }

        [System.Runtime.InteropServices.DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        static extern bool InternetSetCookie(string UrlName, string CookieName, string CookieData);

        private void WSubTran_Load(object sender, EventArgs e)
        {
            txtUrl.Text = _url;
            this.Text = _url;
            foreach (var item in _cookies)
            {
                InternetSetCookie(_url, null, item.ToString());
            }
            wb.Navigate(_url);
        }

        private void btnF5_Click(object sender, EventArgs e)
        {
            wb.Refresh();
        }

        private void WSubTran_FormClosed(object sender, FormClosedEventArgs e)
        {
            wb.Stop();
            wb.Dispose();
        }
    }
}
