using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlSplitter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var xml = new XmlDocument();

            xml.Load(@"d:\Projects\NewAddress\rooms.xml");

            var rowDataElements = xml.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElements.Count == 1)
            {
                var rowDataElement = rowDataElements[0];

                var rowCount = rowDataElement.ChildNodes.Count;

                for (int i = 0; i < rowCount; ++i)
                {
                    if(i == 50) continue;
                    rowDataElement.RemoveChild(rowDataElement.ChildNodes[0]);
                }

                xml.Save(@"d:\Projects\NewAddress\rooms_filtered.xml");

                
            }
        }
    }
}
