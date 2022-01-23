using System;
using System.Data;
using System.Windows.Forms;

namespace Learning_CSharpe
{
    class PlantInfomation
    {
        MsSQLUtil sqlUtil;
        string sqlQuery;
        public string GetText(ComboBox _cb)
        {
            return ComboUtil.GetItem(_cb).Text;
        }
        public void SetS(ComboBox _cb)
        {
            AutoCompleteStringCollection _cbdata = new AutoCompleteStringCollection();
            _cb.Items.Clear();
            sqlUtil = new MsSQLUtil();
            sqlQuery = "select distinct [s] from Practice";            
            DataTable dt = sqlUtil.Query(sqlQuery);
            int idx = 1;
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object _value = item;
                    ComboUtil.AddItem(_cb, new ComboBoxItem(idx.ToString(), _value.ToString()));
                    _cbdata.Add(_value.ToString());
                    idx += 1;
                }
            }
            ComboUtil.SetItemValue(_cb, "1");
            _cb.AutoCompleteCustomSource = _cbdata;
        }

        public void SetT(ComboBox _cb, ComboBox _cbS)
        {
            _cb.Items.Clear();
            sqlUtil = new MsSQLUtil();
            sqlQuery = String.Format("select distinct [t] from Practice where [s] ='{0}'", this.GetText(_cbS));            
            DataTable dt = sqlUtil.Query(sqlQuery);
            int idx = 1;
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object _value = item;
                    ComboUtil.AddItem(_cb, new ComboBoxItem(idx.ToString(), _value.ToString()));
                    idx += 1;
                }
            }
            ComboUtil.SetItemValue(_cb, "1");
        }
        public string GetT(ComboBox _cbT)
        {
            return ComboUtil.GetItem(_cbT).Text;
        }
        public void SetP(ComboBox _cb, ComboBox _cbS, ComboBox _cbT)
        {
            _cb.Items.Clear();
            sqlUtil = new MsSQLUtil();
            sqlQuery = String.Format("select distinct [p] from Practice where [s]='{0}' and [t]='{1}'", this.GetText(_cbS), this.GetText(_cbT));
            DataTable dt = sqlUtil.Query(sqlQuery);
            int idx = 1;
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object _value = item;
                    ComboUtil.AddItem(_cb, new ComboBoxItem(idx.ToString(), _value.ToString()));
                    idx += 1;
                }
            }
            ComboUtil.SetItemValue(_cb, "1");
        }

        public void SetM(ComboBox _cb, ComboBox _cbS, ComboBox _cbT, ComboBox _cbP)
        {
            _cb.Items.Clear();
            sqlUtil = new MsSQLUtil();
            sqlQuery = String.Format("select distinct [m] from Practice where [s]='{0}' and [t]='{1}' and [p]='{2}'", this.GetText(_cbS), this.GetText(_cbT), this.GetText(_cbP));
            DataTable dt = sqlUtil.Query(sqlQuery);
            int idx = 1;
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object _value = item;
                    ComboUtil.AddItem(_cb, new ComboBoxItem(idx.ToString(), _value.ToString()));
                    idx += 1;
                }
            }
            ComboUtil.SetItemValue(_cb, "1");
        }
        public void SetEL(TextBox _tE, TextBox _tL,ComboBox _cbS, ComboBox _cbT, ComboBox _cbP, ComboBox _cbM)
        {
            _tE.Clear();
            _tL.Clear();
            sqlUtil = new MsSQLUtil();
            sqlQuery = String.Format("select [e], [l] from Practice where [s]='{0}' and [t]='{1}' and [p]='{2}' and [m]='{3}'",
                                    this.GetText(_cbS), this.GetT(_cbT), this.GetText(_cbP), this.GetText(_cbM));
            DataTable dt = sqlUtil.Query(sqlQuery);
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    object _value = item;                    
                    if ( item == row.ItemArray.GetValue(0))
                        _tE.Text = _value.ToString();
                    else
                        _tL.Text = _value.ToString();
                }
            }
        }
    }
}
