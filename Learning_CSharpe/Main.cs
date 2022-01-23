using System;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace Learning_CSharpe
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private static void Create_Practice()
        {
            MsSQLUtil msSQLUtil = new MsSQLUtil();
            string stringQuery = "create table Practice(" +
                    "[p] nvarchar(3)," +
                    "[s] nvarchar(6)," +
                    "[m] nvarchar(8)," +
                    "[e] nvarchar(12)," +
                    "[t] nvarchar(20)," +
                    "[l] nvarchar(10)" +
                ")";
            msSQLUtil.NonQuery(stringQuery);
        }
        private static void Insert_Practice()
        {
            MsSQLUtil msSQLUtil = new MsSQLUtil();
            string stringQuery = "insert into [Practice] ([p], [s], [m], [e], [t], [l]) values" +
                "('p01','1000','015','e011000015', 'ssss-1234','L1 1F')," +
                "('p01','1000','020','e011000020', 'ssss-1234','L1 1F')," +
                "('p01','1000','025','e011000025', 'ssss-1234','L5 1F')," +
                "('p02','1000','004','e021000004', 'ssss-1234','L5 2F')," +
                "('p03','1000','014','e031000014', 'ssss-1234','L6 3F')," +
                "('p05','1000','024','e051000024', 'ssss-8888','L6 1F')," +
                "('p02','2000','024','e022000035', 'ssss-8888','L2 4F')," +
                "('p02','2000','035','e022000055', 'ssss-2234','L2 4F')," +
                "('p03','3000','001','e033000001', 'ssss-3234','L3 8F')," +
                "('p03','3000','002','e033000002', 'ssss-3234','L3 8F')," +
                "('p03','3000','003','e033000003', 'ssss-3234','L3 8F')," +
                "('p03','3000','004','e033000004', 'ssss-3234','L3 8F')," +
                "('p05','5000','101','e055000101', 'bbbb-4234','L4 7F')," +
                "('p05','5000','102','e055000102', 'bbbb-4234','L4 7F')," +
                "('p05','5000','103','e055000103', 'bbbb-4234','L4 7F')," +
                "('p05','5000','104','e055000104', 'bbbb-4234','L4 7F')";
            msSQLUtil.NonQuery(stringQuery);
        }
        private static void Delete_Practice()
        {
            MsSQLUtil msSQLUtil = new MsSQLUtil();
            string stringQuery = "if exists(select * from Practice) begin" +
                "   drop table [Practice]"+
                "end";
            msSQLUtil.NonQuery(stringQuery);
        }
        private void Select_Practice()
        {
            MsSQLUtil msSQLUtil = new MsSQLUtil();
            string stringQuery = "select * from [Practice]";
            DataTable dt = msSQLUtil.Query(stringQuery);

            textBox00.Clear();
            textBox00.AppendText("===================================\r\n");
            foreach (DataRow row in dt.Rows)
            {
                textBox00.AppendText(" | ");
                foreach (var item in row.ItemArray)
                {
                    object valu = item;
                    textBox00.AppendText(valu.ToString() + " | ");
                }
                textBox00.AppendText("\r\n");
            }
            textBox00.AppendText("===================================\r\n");
        }
        /*
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
            textBox00.Clear();
            textBox00.AppendText("=============\r\n");
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object valu = item;
                    textBox00.AppendText(valu.ToString() + " ");
                }
                textBox00.AppendText("\r\n");
            }
            textBox00.AppendText("=============\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    object valu = dt.Rows[i].ItemArray[j];
                    textBox00.AppendText(valu.ToString() + " ");
                }
                textBox00.AppendText("\r\n");
            }
            textBox00.AppendText("=============\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string source = @"C:\Users\hsuan\Desktop\TFET_Fig";
            string dest = @"C:\Users\hsuan\Desktop\CC";
            FileUtil fileUtil = new FileUtil();
            fileUtil.DirectoryMove(source, dest);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox00.Clear();
            ProcessesUtil processesUtil = new ProcessesUtil();
            foreach(Process ps in processesUtil.GetProcessesList())
            {
                textBox00.AppendText(String.Format("Process: {0}, ID: {1}\r\n", ps.ProcessName, ps.Id));
            }
        }
        */

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Create_Practice();
                Insert_Practice();
                comboBox22.Items.Clear();
                PlantInfomation pf = new PlantInfomation();
                pf.SetS(comboBox22);
            }
            catch
            {

            }     
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Delete_Practice();
            }
            catch
            {

            }  
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                Select_Practice();
            }
            catch
            {

            }
        }
        private void comboBox22_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox21.Clear();
            comboBox23.Items.Clear();
            comboBox24.Items.Clear();
            comboBox25.Items.Clear();
            textBox26.Clear();
            PlantInfomation pf = new PlantInfomation();
            pf.SetT(comboBox23, comboBox22);
            pf.SetP(comboBox24, comboBox22, comboBox23);
            pf.SetM(comboBox25, comboBox22, comboBox23, comboBox24);
            pf.SetEL(textBox21, textBox26, comboBox22, comboBox23, comboBox24, comboBox25);
        }

        private void comboBox23_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox21.Clear();
            comboBox24.Items.Clear();
            comboBox25.Items.Clear();
            textBox26.Clear();
            PlantInfomation pf = new PlantInfomation();
            pf.SetP(comboBox24, comboBox22, comboBox23);
            pf.SetM(comboBox25, comboBox22, comboBox23, comboBox24);
            pf.SetEL(textBox21, textBox26, comboBox22, comboBox23, comboBox24, comboBox25);
        }
        private void comboBox24_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox21.Clear();
            comboBox25.Items.Clear();
            textBox26.Clear();
            PlantInfomation pf = new PlantInfomation();
            pf.SetM(comboBox25, comboBox22, comboBox23, comboBox24);
            pf.SetEL(textBox21, textBox26, comboBox22, comboBox23, comboBox24, comboBox25);
        }
        private void comboBox25_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox21.Clear();
            textBox26.Clear();
            PlantInfomation pf = new PlantInfomation();
            pf.SetEL(textBox21, textBox26, comboBox22, comboBox23, comboBox24, comboBox25);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                Select_Practice();               
            }
            catch
            {
                flag = false;                
            }
            finally
            {
                textBox00.Clear();
            }

            if (flag)
            {
                textBox21.Clear();
                comboBox22.Items.Clear();
                comboBox23.Items.Clear();
                comboBox24.Items.Clear();
                comboBox25.Items.Clear();
                textBox26.Clear();
                PlantInfomation pf = new PlantInfomation();
                pf.SetS(comboBox22);
                pf.SetT(comboBox23, comboBox22);
                pf.SetP(comboBox24, comboBox22, comboBox23);
                pf.SetM(comboBox25, comboBox22, comboBox23, comboBox24);
                pf.SetEL(textBox21, textBox26, comboBox22, comboBox23, comboBox24, comboBox25);
            }
            textBox11.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            textBox12.Text = Environment.MachineName.ToString();
            textBox13.Text = System.Windows.Forms.SystemInformation.ComputerName;
            textBox14.Text = System.Net.Dns.GetHostName();
            textBox14.AppendText("\r\n["+System.Environment.GetEnvironmentVariable("COMPUTERNAME")+"]\r\n");
            textBox14.AppendText("\r\n[" + Environment.UserDomainName + "123131]\r\n");
            textBox14.AppendText("\r\n[" + System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName + "]\r\n");
            MessageBox.Show("1: " + WindowsIdentity.GetCurrent().Name.ToString() + "\r\n" +
               "2: " + Environment.UserDomainName + "\r\n" +
               "3: " + WindowsIdentity.GetCurrent().Name + "\r\n" +
                "4: " + Thread.CurrentPrincipal.Identity.Name + "\r\n" +
               "5: " + Environment.UserName + "\r\n"+
               "6: "+ UserPrincipal.Current.DisplayName);



        }
        private void button11_Click(object sender, EventArgs e)
        {

        }
        private void button21_Click(object sender, EventArgs e)
        {

        }
    }
}
