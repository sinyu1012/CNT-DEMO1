using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 彩牛通.Entity;

namespace 彩牛通
{
    public partial class StatisticsForm : Form
    {
        int ColumnOneCount;
        int ColumnOneMin;
        int ColumnOneMax;
        int ColumnTwoCount;
        int ColumnTwoMin;
        int ColumnTwoMax;
        int ProjectCount;
        int ProjectMax;
        int ProjectRows;
        int SingleMax;
        bool status = false;
        int ColumnOneRows;
        int ColumnTwoRows;
        int AdditionalColumnCount;
        int CTZeroCount;

        public StatisticsForm()
        {
            InitializeComponent();
        }
        public StatisticsForm(List<RandomTemp> randomSaves)
        {
            InitializeComponent();
            loadConfig();
            initView();
            initData(randomSaves);

        }
        private void loadConfig()
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //根据Key读取<add>元素的Value
            ColumnOneCount = int.Parse(config.AppSettings.Settings["ColumnOneCount"].Value);
            ColumnOneMin = int.Parse(config.AppSettings.Settings["ColumnOneMin"].Value);
            ColumnOneMax = int.Parse(config.AppSettings.Settings["ColumnOneMax"].Value);
            ColumnTwoCount = int.Parse(config.AppSettings.Settings["ColumnTwoCount"].Value);
            ColumnTwoMin = int.Parse(config.AppSettings.Settings["ColumnTwoMin"].Value);
            ColumnTwoMax = int.Parse(config.AppSettings.Settings["ColumnTwoMax"].Value);
            ProjectCount = int.Parse(config.AppSettings.Settings["ProjectCount"].Value);
            ProjectMax = int.Parse(config.AppSettings.Settings["ProjectMax"].Value);
            ProjectRows = int.Parse(config.AppSettings.Settings["ProjectRows"].Value);
            SingleMax = int.Parse(config.AppSettings.Settings["SingleMax"].Value);
            ColumnOneRows = int.Parse(config.AppSettings.Settings["ColumnOneRows"].Value);
            ColumnTwoRows = int.Parse(config.AppSettings.Settings["ColumnTwoRows"].Value);
            AdditionalColumnCount = int.Parse(config.AppSettings.Settings["AdditionalColumnCount"].Value);
            CTZeroCount = int.Parse(config.AppSettings.Settings["CTZeroCount"].Value);

        }
        private void initView()
        {
            DataGridViewTextBoxColumn C = new DataGridViewTextBoxColumn();
            C.HeaderCell.Value = "";
            C.Width = 100;
            dgv_Statistics.Columns.Add(C);
            for (int i = ColumnTwoMin; i <= ColumnTwoMax; i++)
            {
                DataGridViewTextBoxColumn C1 = new DataGridViewTextBoxColumn();
                C1.HeaderCell.Value = i + "";
                C1.Width = 50;
                dgv_Statistics.Columns.Add(C1);
            }
            dgv_Statistics.RowsDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Regular);
            dgv_Statistics.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = ColumnTwoMin; i <= ColumnTwoMax; i++)
            {
                dgv_Statistics.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void initData(List<RandomTemp> randomSaves)
        {
            int row = ColumnOneMax - ColumnOneMin + 1;
            int column = ColumnTwoMax - ColumnTwoMin + 2;
            int[,] statisticsArr = new int[row, column];
            //初始化
            for (int i = 0; i < row; i++)
            {
                statisticsArr[i, 0] = i + 1;
                for (int j = 1; j < column; j++)
                {
                    statisticsArr[i, j] = 0;
                }

            }
            //统计
            for (int i = 0; i < randomSaves.Count; i++)
            {
                RandomTemp randomtemp = randomSaves[i];
                string[] strarr = randomtemp.ColumnTwo.Split(',');
                for (int j = 0; j < ColumnTwoCount; j++)
                {
                    int r = int.Parse(randomtemp.ColumnOne.ToString());
                    int c = int.Parse(strarr[j]);
                    statisticsArr[r - 1, c]++;

                }
            }
            //显示 填充
            for (int i = 0; i < row; i++)
            {
                int index = dgv_Statistics.Rows.Add();
                dgv_Statistics.Rows[index].Cells[0].Value = statisticsArr[i, 0];
                for (int j = 1; j < column; j++)
                {
                    dgv_Statistics.Rows[index].Cells[j].Value = statisticsArr[i, j];
                }
            }
        }
        private void StatisticsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
