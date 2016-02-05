using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace BrightBoxClient
{
    public partial class ClientForm : Form
    {
        private Dictionary<int, string> statusesDictionary = new Dictionary<int, string>()
        {
            {0, "Все работает штатно, работы по обновлению не ведутся"},
            {1, "Сервис недоступен, ведутся технические работы"},
            {2, "Сейчас все работает штатно, но на дд.мм.гггг запланированы работы"}
        };

        public ClientForm()
        {
            InitializeComponent();
        }

        private void btnGetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                WebRequest request = WebRequest.Create(tbUrl.Text + "/api/technicalworks");

                request.Credentials = CredentialCache.DefaultCredentials;

                using (var webResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        tbResponse.Text = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                tbResponse.Text = "Exception catched";
            }
        }
    }
}
