using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 彩牛通
{
    public partial class SelectorSetting : Form
    {
        public SelectorSetting(int count)
        {
            InitializeComponent();
            initDGV(count);
        }
        private void initDGV(int count)
        {
            string[] arr = readText().Split(';');
            for (int i = 0; i < count; i++)
            {
                try
                {
                    updateProject(i, arr[i]);
                }
                catch (Exception)
                {
                    updateProject(i, "");
                }

            }
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 2; i++)
            {
                dgv.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
        }
        private string readText()
        {
            string contents = File.ReadAllText(@"SelectorSetting.txt", Encoding.Default);

            return contents;
        }
        public void updateProject(int Index, string beishu)
        {
            int index = dgv.Rows.Add();
            dgv.Rows[index].Cells[0].Value = Index + 1 ;
            dgv.Rows[index].Cells[1].Value = beishu;

        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            writeText();
        }
        private void writeText()
        {
            string arrs = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                arrs += dgv.Rows[i].Cells[1].Value.ToString() + ";";
            }
            File.WriteAllText(@"SelectorSetting.txt", arrs, Encoding.Default);
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
