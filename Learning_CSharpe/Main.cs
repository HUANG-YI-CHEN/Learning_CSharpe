using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Learning_CSharpe
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql =    
                    "select *" +
                    "from(" +
                    "  values('0', '0000'), ('1', '0001'), ('2', '0010'), ('3', '0011'), ('4', '0100'), ('5', '0101'), ('6', '0110'), ('7', '0111')," +
                    "  ('8', '1000'), ('9', '1001'), ('A', '1010'), ('B', '1011'), ('C', '1100'), ('D', '1101'), ('E', '1110'), ('F', '1111')" +
                    ") as f(HexDigit, BinaryDigits)";

            MsSQLUtil msSQLUtil = new MsSQLUtil();
            DataTable dt = msSQLUtil.Query(sql);
            textBox1.AppendText("=============\r\n");
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object valu = item;
                    textBox1.AppendText(valu.ToString() + " ");
                }
                textBox1.AppendText("\r\n");
            }
            textBox1.AppendText("=============\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    object valu = dt.Rows[i].ItemArray[j];
                    textBox1.AppendText(valu.ToString() + " ");
                }
                textBox1.AppendText("\r\n");
            }
            textBox1.AppendText("=============\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string source = @"C:\Users\hsuan\Desktop\TFET_Fig";
            string dest = @"C:\Users\hsuan\Desktop\TFET_Fig_Bak";
            FileUtil fileUtil = new FileUtil();
            fileUtil.DirectoryCopy(source,dest);
        }
    }
}
