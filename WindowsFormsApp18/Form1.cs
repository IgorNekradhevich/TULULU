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
using System.IO;

namespace WindowsFormsApp18
{
    public partial class Form1 : Form
    {
        List<string> names;
        List<string> id;
        public Form1()
        {
            InitializeComponent();
            names = new List<string>();
            id = new List<string>();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

            WebRequest request = WebRequest.Create("https://tululu.org/search/?q="+textBox1.Text);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string site = reader.ReadToEnd();

            id.Clear();
            names.Clear();
            int index;
            while (true)
            {
                index = site.IndexOf("_blank");
                if (index == -1)
                    break;
                site = site.Remove(0, index + 16);

                index = site.IndexOf("/\"");
                id.Add(site.Remove(index));

                index = site.IndexOf(">");
                site = site.Remove(0, index + 1);

                index = site.IndexOf("<");
                names.Add(site.Remove(index));
            }
            listBox1.Items.Clear();
           foreach( string name  in names)
            {
                listBox1.Items.Add(name);
            }

        
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = listBox1.SelectedIndex;
            if (num >= 0)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://tululu.org/txt.php?id="
                    + id[num], "C:\\1\\" + names[num] + ".txt");
                MessageBox.Show("Готово");
            } else
            {
                MessageBox.Show("Необходимо выбрать книгу!");
            }
        }
    }
}
