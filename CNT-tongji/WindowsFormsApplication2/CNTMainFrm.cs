using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 彩牛通.Entity;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using testPrint;
using CCWin;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Reflection;

namespace 彩牛通
{
    public partial class CNTMainFrm : CCSkinMain
    {
        List<RandomTemp> randomTemps;
        List<RandomTemp> SaverandomTemps;//保存筛选到15次的方案
        List<RandomTemp> TotalSaveRandomTemps;//保存筛选到15次的方案
        //private int ProjectNo=0;
        Dictionary<int, int> Project;
        Dictionary<int, string> ProjectMemo;
        Dictionary<int, int> Summarys;// 数 ： 倍数
        int ProjectNo=1;
        int ProjectsIndex=0;
        int deleteCount = 0, signCount=0,saveCount=0;
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
        //int AddColumnTwoCount;
        //int CTZeroCount;
        List<int> tongjixianshi;
        //分页
        int pageSize = 0;     //每页显示行数
        int nMax = 0;         //总记录数
        int pageCount = 0;    //页数＝总记录数/每页显示行数
        int pageCurrent = 0;   //当前页号
        int nCurrent = 0;      //当前记录行
        DataSet ds = new DataSet();
        List<RandomTemp> ranNow = new List<RandomTemp>();
        List<int> AdditionalRandoms = new List<int>();
        private string[] Multiples;//击打倍数的设置
        private  string huizong="";
        public CNTMainFrm()
        {
            InitializeComponent();
            randomTemps = new List<RandomTemp>();
            SaverandomTemps = new List<RandomTemp>();
            TotalSaveRandomTemps = new List<RandomTemp>();
            Project = new Dictionary<int, int>();
            ProjectMemo = new Dictionary<int, string>();
            Summarys = new Dictionary<int, int>();
            tongjixianshi = new List<int>();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        protected override void WndProc(ref Message msg)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (msg.Msg == WM_SYSCOMMAND && ((int)msg.WParam == SC_CLOSE))
            {
                // 点击winform右上关闭按钮 
                // 加入想要的逻辑处理
                return;
            }
            base.WndProc(ref msg);
        }

        private void CNTMainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                e.Cancel = false;  //点击OK  
            }
            else
            {
                e.Cancel = true;
            }
        }  
        private void CNTMainFrm_Load(object sender, EventArgs e)
        {
            ProjectNo = 1;
            loadConfig();
            
            initView();
            //for (int i = ColumnTwoMin; i <= ColumnTwoMax; i++)
            //{
            //    Summarys.Add(i, 0);
            //}
        }
        //private void initSummary()
        //{
        //    for (int i = ColumnTwoMin; i <= ColumnTwoMax; i++)
        //    {
        //        Summarys[i]=0;
        //    }

        //}
        /// <summary>
        /// 初始化一些窗体
        /// </summary>
        private void initView()
        {
           initDGV(dgv_Random);

        }
        private string readMultipleSetting()
        {
            string contents = File.ReadAllText(@"MultipleSetting.txt", Encoding.Default);
            return contents;
        }
        /// <summary>
        /// 初始化DGV
        /// </summary>
        /// <param name="dgv"></param>
        private void initDGV(DataGridView dgv)
        {
            //dgv.Columns.Clear();
            //DataGridViewTextBoxColumn C1 = new DataGridViewTextBoxColumn();
            //C1.HeaderCell.Value = "方案号";
            //C1.Width = 100;
            //dgv.Columns.Add(C1);

            //DataGridViewTextBoxColumn C2 = new DataGridViewTextBoxColumn();
            //C2.HeaderCell.Value = "列1";
            //C2.Width = 150;
            //dgv.Columns.Add(C2);

            //DataGridViewTextBoxColumn C3 = new DataGridViewTextBoxColumn();
            //C3.HeaderCell.Value = "列2";
            //C3.Width = 150;
            //dgv.Columns.Add(C3);

            //DataGridViewTextBoxColumn C4 = new DataGridViewTextBoxColumn();
            //C4.HeaderCell.Value = "列3";
            //C4.Width = 400;
            //dgv.Columns.Add(C4);

            ////for (int i = 0; i < AdditionalColumnCount; i++)
            ////{
            ////    DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            ////    c.HeaderCell.Value = "列" + (i + 3);
            ////    c.Width = 200;
            ////    dgv.Columns.Add(c);
            ////}
            //DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            //c1.HeaderCell.Value = "单条统计";
            //c1.Width = 300;
            //dgv.Columns.Add(c1);

            //DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            //c2.HeaderCell.Value = "方案筛选次数";
            //c2.Width = 150;
            //dgv.Columns.Add(c2);

            dgv.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
           // dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.RowsDefaultCellStyle.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 4; i++)
            {
                dgv.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
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
            //AddColumnTwoCount = int.Parse(config.AppSettings.Settings["AddColumnTwoCount"].Value);
            //CTZeroCount = int.Parse(config.AppSettings.Settings["CTZeroCount"].Value);

            Multiples = readMultipleSetting().Split(',');
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            //清除所有
            if (MessageBox.Show("将清除当前所有方案，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clearDGVandRts();
            }
           
        }
        private void clearDGVandRts()
        {
            status = false;
            deleteCount = 0;
            signCount = 0;
            randomTemps.Clear();
            dgv_Random.Rows.Clear();
            Summarys.Clear();
            txt_Summary.Text = "";
            SaverandomTemps.Clear();
            ProjectNo = 1;
            ProjectMemo.Clear();
            Project.Clear();

            tongjixianshi.Clear();
        }
        private void btn_Random_Click(object sender, EventArgs e)
        {
            deleteCount = 0;
            signCount = 0;
            //randomTemps.Clear();
            //dgv_Random.Rows.Clear();
            //Project.Clear();
            //SaverandomTemps.Clear();
            status = true;

            AddProgrammeForm addform = new AddProgrammeForm(ColumnOneRows, ColumnTwoRows, ProjectNo, AdditionalColumnCount, ColumnTwoMin, ColumnTwoMax);
            DialogResult res= addform.ShowDialog();
            if (res == DialogResult.OK)
            {
                Project.Add(ProjectNo, 0);
                ProjectMemo.Add(ProjectNo, "");
                ProjectNo++;

                //setAdditionalColumn(addform.randomTemps);
                //setColumnTwoNew(addform.randomTemps);
                randomTemps.AddRange(addform.randomTemps);
                if (randomTemps.Count > 0)
                {
                    pageSize = 100 * (ColumnOneRows);      //设置页面行数 100方案数
                    nMax = (randomTemps.Count / ColumnOneRows) * (ColumnOneRows);
                    pageCount = (nMax / pageSize);    //计算出总页数
                    if ((nMax % pageSize) > 0) pageCount++;
                    pageCurrent = 1;    //当前页数从1开始
                    nCurrent = 0;       //当前记录数从0开始
                    LoadData();
                }
                MessageBox.Show("添加成功");
            }
           
           
            
        }
        private String getAdditionalString(String ct)
        {

            return ct;
        }
      
        
        /// <summary>
        /// 计算附加列的随机串
        /// </summary>
        private void setAdditionalColumn(List<RandomTemp> randomTemps)
        {
            int bili = ColumnOneRows / ColumnTwoRows;
           
                List<RandomTemp> temps = new List<RandomTemp>();
                int t = 1;
                for (int i = 0; i < randomTemps.Count; i++)
                {
                     temps.Add(randomTemps[i]);
                     //List<int> lists = new List<int>();
                    //for (int x = 0; x < AdditionalNum + 1; x++)
                    //{
                    //    lists.Add(temps)
                    //}
                   
                    if (t == ColumnOneRows)
                    {
                        int index = 0;
                        List<int> lists = new List<int>();
                         for (int AdditionalNum = 0; AdditionalNum < AdditionalColumnCount; AdditionalNum++)
                        {

                         Random ran = new Random(GetRandomSeed());
                         //int index = ran.Next(1, ColumnTwoRows);
                        index++;
                        if (!lists.Contains(index))
                        {
                            lists.Add(index);
                            int biliCount = 1;
                            for (int x = ColumnOneRows - 1; x >= 0; x--)
                            {
                                switch (AdditionalNum + 3)
                                {
                                    case 3:
                                        randomTemps[i - x].ColumnThree = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 4:
                                        randomTemps[i - x].ColumnFour = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 5:
                                        randomTemps[i - x].ColumnFive = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 6:
                                        randomTemps[i - x].ColumnSix = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 7:
                                        randomTemps[i - x].ColumnSeven = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 8:
                                        randomTemps[i - x].ColumnEight = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 9:
                                        randomTemps[i - x].ColumnNine = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 10:
                                        randomTemps[i - x].ColumnTen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 11:
                                        randomTemps[i - x].ColumnEleven = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 12:
                                        randomTemps[i - x].ColumnTwelve = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 13:
                                        randomTemps[i - x].ColumnThirteen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 14:
                                        randomTemps[i - x].ColumnFourteen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 15:
                                        randomTemps[i - x].ColumnFifteen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 16:
                                        randomTemps[i - x].ColumnSixteen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 17:
                                        randomTemps[i - x].ColumnSeventeen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 18:
                                        randomTemps[i - x].ColumnEighteen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 19:
                                        randomTemps[i - x].ColumnNineteen = temps[index * bili].ColumnTwo.ToString();
                                        break;
                                    case 20:
                                        randomTemps[i - x].ColumnTwenty = temps[index * bili].ColumnTwo.ToString();
                                        break;

                                }

                                if (biliCount == bili)
                                {
                                    if (index == ColumnTwoRows - 1)
                                    {
                                        index = 0;
                                    }
                                    else
                                    {
                                        index++;
                                    }
                                    biliCount = 1;
                                }
                                else
                                {
                                    biliCount++;
                                }

                            }
                        }
                        else
                        {
                            //temps.Remove(randomTemps[i]);
                            AdditionalNum--;
                        }
                            
                         }
                        t = 1;
                        temps.Clear();
                       
                           
                       
                    }
                    else
                    {
                        t++;
                    }
                }
            
            
        }

       
        /// <summary>
        /// 列二中新增内容
        /// </summary>
        private void setColumnTwoNew(List<RandomTemp> randomTemps)
        {
            for (int i = 0; i < randomTemps.Count; i++)
            {
                switch (AdditionalColumnCount + 2)
                {
                    case 3:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString();
                        break;
                    case 4:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString();
                        break;
                    case 5:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString();
                        break;
                    case 6:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() ;
                        break;
                    case 7:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString();
                        break;
                    case 8:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString();
                        break;
                    case 9:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString();
                        break;
                    case 10:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString();
                        break;
                    case 11:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnEleven.ToString();
                        break;
                    case 12:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString();
                        break;
                    case 13:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString();
                        break;
                    case 14:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString();
                        break;
                    case 15:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString() + "," + randomTemps[i].ColumnFifteen.ToString();
                        break;
                    case 16:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString() + "," + randomTemps[i].ColumnFifteen.ToString() + "," + randomTemps[i].ColumnSixteen.ToString();
                        break;
                    case 17:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString() + "," + randomTemps[i].ColumnFifteen.ToString() + "," + randomTemps[i].ColumnSixteen.ToString() + "," + randomTemps[i].ColumnSeventeen.ToString();
                        break;
                    case 18:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString() + "," + randomTemps[i].ColumnFifteen.ToString() + "," + randomTemps[i].ColumnSixteen.ToString() + "," + randomTemps[i].ColumnSeventeen.ToString() + "," + randomTemps[i].ColumnEighteen.ToString();
                        break;
                    case 19:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString() + "," + randomTemps[i].ColumnFifteen.ToString() + "," + randomTemps[i].ColumnSixteen.ToString() + "," + randomTemps[i].ColumnSeventeen.ToString() + "," + randomTemps[i].ColumnEighteen.ToString() + "," + randomTemps[i].ColumnNineteen.ToString();
                        break;
                    case 20:
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString() + "," + randomTemps[i].ColumnSeven.ToString() + "," + randomTemps[i].ColumnEight.ToString() + "," + randomTemps[i].ColumnNine.ToString() + "," + randomTemps[i].ColumnTen.ToString() + "," + randomTemps[i].ColumnTwelve.ToString() + "," + randomTemps[i].ColumnThirteen.ToString() + "," + randomTemps[i].ColumnFourteen.ToString() + "," + randomTemps[i].ColumnFifteen.ToString() + "," + randomTemps[i].ColumnSixteen.ToString() + "," + randomTemps[i].ColumnSeventeen.ToString() + "," + randomTemps[i].ColumnEighteen.ToString() + "," + randomTemps[i].ColumnNineteen.ToString() + "," + randomTemps[i].ColumnTwenty.ToString();
                        break;

                }
            }
                
        }

        
       
        /// <summary>
        /// 随机数生成种子，防止太快出现重复
        /// </summary>
        /// <returns></returns>
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 设置一条
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="columnOne"></param>
        /// <param name="CT"></param>
        public void setProject(int projectNo, string columnOne, string CT,string CNO)
        {
            
            //保存到lists
            RandomTemp randomtemp = new RandomTemp();
            randomtemp.ColumnOne = columnOne.ToString();
            randomtemp.ColumnTwo = CT;
            randomtemp.ColumnNewOne = CNO;
            randomtemp.SingleCount1 = 0;
            randomtemp.Rowindex1 = -1;
            randomtemp.ProjectNo = projectNo;
            randomtemp.ProjectCount = 0;
            randomTemps.Add(randomtemp);

        }
        private void dgv_Random_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1,3列 最后 相同单元格进行合并
            if ((e.ColumnIndex == 0 && e.RowIndex != -1) || (e.ColumnIndex == 2 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1))
            {
                cellPainting(dgv_Random,e);
            }
            
            dgv_Random.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
        }
        private void cellPainting(DataGridView dg , DataGridViewCellPaintingEventArgs e)
        {

            using
                (
                Brush gridBrush = new SolidBrush(dg.GridColor),
                backColorBrush = new SolidBrush(e.CellStyle.BackColor)
                )
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    // 清除单元格
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                    // 画 Grid 边线（仅画单元格的底边线和右边线）
                    //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                    try
                    {


                        if (e.RowIndex < dg.Rows.Count - 1 &&
                        dg.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() !=
                        e.Value.ToString() )
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);
                        // 画右边线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                            e.CellBounds.Top, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom);
                    }
                    catch (Exception)
                    {

                    }
                    // 画（填写）单元格内容，相同的内容的单元格只填写第一个
                    if (e.Value != null)
                    {
                        if (e.RowIndex > 0 &&
                        dg.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() ==
                        e.Value.ToString())
                        {

                        }
                        else
                        {
                            StringFormat stringFormat = new StringFormat();

                            stringFormat.Alignment = StringAlignment.Near;
                            //格式.Alignment = StringAlignment.Far; //右对齐
                            e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                                Brushes.Black, e.CellBounds.X + 2,
                                e.CellBounds.Y + 5, stringFormat);
                        }
                    }
                    e.Handled = true;
                }
            }
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show(string.Format(" 列一：{0}\n 列二：{1}\n是否确定统计？", txt_SXRow1.Text, txt_SXRow2.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                status = false;
                saveCount = 0;
                deleteCount = 0;
                signCount = 0;
                Summarys.Clear();
                txt_Summary.Text = "";
                tongjixianshi.Clear();
                //筛选
                string columnOne = txt_SXRow1.Text;
               // string sxRow2 = txt_SXRow2.Text;


                bool isNewBiaoji = false;
                //删除达到15次的
                //int delFlag;

                for (int i = 0; i < randomTemps.Count; i++)
                {
                    RandomTemp randomtemp = randomTemps[i];
                    string[] strarr1 = randomtemp.ColumnOne.Split(',');

                    for (int x = 0; x < ColumnOneCount; x++)
                    {
                        if (strarr1[x].Equals(columnOne))
                        {
                            if (randomTemps[i].SingleCount1 < SingleMax)
                            {

                                //更新方案被筛选次数
                                Project[randomtemp.ProjectNo]++;//次数++
                                tongjixianshi.Add(randomtemp.ProjectNo);
                                //if (checkProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]))
                                //{
                                //    randomTemps[i].SingleCount1++;//删除的同时增加
                                //    //deleno.Add(randomtemp.ProjectNo);
                                //}
                                //else
                                //{
                                    //标记黄色
                                    signCount++;
                                    isNewBiaoji = true;

                                    //加X标记
                                    randomTemps[i].SingleCount1++;
                                    
                                    ProjectMemo[randomtemp.ProjectNo] = randomTemps[i].ColumnTwo;//次数++

                                   // randomTemps[i].ProjectMemo = Project[randomTemps[i].ProjectNo] + "次: \n " + randomTemps[i].ColumnTwo;

                                //}

                            }
                            else
                            {

                            }

                            //updateProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]);
                        }

                    }
                }
                //for (int i = 0; i < deleno.Count; i++)
                //{
                //    deleteProject(deleno[i]);
                //}
                //deleno.Clear();

                //更新VIEW
                if (randomTemps.Count > 0)
                {
                    nMax = randomTemps.Count;
                    pageCount = (nMax / pageSize);    //计算出总页数
                    if ((nMax % pageSize) > 0) pageCount++;
                    pageCurrent = 1;    //当前页数从1开始
                    nCurrent = 0;       //当前记录数从0开始
                    LoadData();

                    //updateDGV(dgv_Random, randomTemps);

                    //对当前第一页数据进行标记
                    for (int i = 0; i < ranNow.Count; i++)
                    {
                        RandomTemp randomtemp = ranNow[i];
                        string[] strarr1 = randomtemp.ColumnOne.Split(',');
                        
                        for (int x = 0; x < ColumnOneCount; x++)
                        {
                            if (strarr1[x].Equals(columnOne))
                            {

                                if (ranNow[i].SingleCount1 < SingleMax || isNewBiaoji)
                                {
                                    //标记黄色
                                    //signCount++;
                                    dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Yellow;
                                    //加X标记
                                    dgv_Random.Rows[randomtemp.Rowindex1].Cells[3].Value = "";
                                    for (int j = 0; j < ranNow[i].SingleCount1; j++)
                                    {
                                        dgv_Random.Rows[randomtemp.Rowindex1].Cells[3].Value += "X ";
                                    }
                                    //更新方案被筛选次数
                                    // Project[randomtemp.ProjectNo]++;//次数++
                                    // checkProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]);
                                    updateProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]);
                                }
                                else
                                {
                                    dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Red;
                                }

                            }

                        }
                    }
                }
                else
                {
                    dgv_Random.Rows.Clear();
                }
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    updateSummarys(randomTemps[i]);
                }
                string huizong = "";
                //for (int i = ColumnTwoMin; i <= Summarys.Count; i++)
                //{
                //    huizong += ("  "+i + " * " + Summarys[i] / 20 + " 倍 \r\n \r\n");
                //}
                foreach (KeyValuePair<int, int> kvp in Summarys)
                {

                    huizong += ("  " + kvp.Key + " * " + kvp.Value / ColumnOneRows + " 倍 \r\n \r\n");
                   // Console.WriteLine("姓名：{0},电影：{1}", kvp.Key, kvp.Value);
                }

                txt_Summary.Text =huizong;
                MessageBox.Show(string.Format("标记 {0} 个方案", signCount), "统计结果", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            
            
        }
        private Boolean checkProjectNo(int no, int Count)
        {
            if (Count % ProjectMax == 0)//满15次应该保留Count == ProjectMax
            {
                //先保存到数据库
                saveCount++;
                //不使用数据库，直接存到缓存SaverandomTemps
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    RandomTemp randomtemp = randomTemps[i];
                    if (randomtemp.ProjectNo == no)//匹配 保存
                    {
                        SaverandomTemps.Add(randomtemp);
                    }
                }

                //再删除在VIEW中
               //deleteProject(no);
              // updateDGV(dgv_Random, randomTemps);
               return true;
            }
            return false;
        }

        /// <summary>
        /// 修改方案号对应的被筛选次数
        /// </summary>
        /// <param name="no"></param>
        /// <param name="Count"></param>
        private void updateProjectNo(int no,int Count)
        {

            for (int i = 0; i < ranNow.Count; i++)
            {
                RandomTemp randomtemp = ranNow[i];
                if (randomtemp.ProjectNo == no)
                {
                    //if (Count >= ProjectMax)//满15次应该保留
                    //    dgv_Random.Rows[randomtemp.Rowindex1].Cells[4].Value = Count + "次";
                    //else
                    //    dgv_Random.Rows[randomtemp.Rowindex1].Cells[4].Value = Count + "次";
                }
            }
        }
        /// <summary>
        /// 根据方案号删除方案数据
        /// </summary>
        /// <param name="no"></param>
        private void deleteProject(int no)
        {
            for (int i = 0; i < randomTemps.Count; i++)
            {
                RandomTemp randomtemp = randomTemps[i];
                if (randomtemp.ProjectNo == no)//三条
                {
                    randomTemps.Remove(randomtemp);
                    i--;//关键
                }
            }
            
        }
        /// <summary>
        /// 更新DGV
        /// </summary>
        private void updateDGV(DataGridView dgv,List<RandomTemp> rt)
        {
            dgv.Rows.Clear();
            int x = 0;
            for (int i = 0; i < rt.Count; i++)
            {
                RandomTemp randomtemp = rt[i];

                updateProject(dgv,randomtemp, i);
                x++;
                if (x % ColumnOneRows == 0)
                {
                    //中间分隔线
                    int index = dgv.Rows.Add();
                   
                    for (int j= 0; j < 5;j++)
                    {
                        dgv.Rows[index].Cells[j].Value = "";
                        dgv.Rows[index].Cells[j].Style.BackColor = Color.LightSkyBlue;
                    }
                }
                
            }
        }
        private void updateSummarys(RandomTemp temp)
        {
            string memo = "";
            if (tongjixianshi.Contains(temp.ProjectNo))
            {
                if (Project[temp.ProjectNo] != 0)
                {
                    string[] arrs = ProjectMemo[temp.ProjectNo].Split(',');

                    for (int i = 0; i < arrs.Length; i++)
                    {
                        //int.Parse(arrs[i]);
                        //memo += arrs[i] + " * " + Multiples[int.Parse(Project[temp.ProjectNo].ToString()) - 1] + "倍 , ";
                        try
                        {
                            Summarys[int.Parse(arrs[i])] += int.Parse(Multiples[int.Parse(Project[temp.ProjectNo].ToString()) - 1]);
                        }
                        catch (Exception)
                        {
                            Summarys.Add(int.Parse(arrs[i]), int.Parse(Multiples[int.Parse(Project[temp.ProjectNo].ToString()) - 1]));
                        }

                    }
                }
            }
        }
        /// <summary>
        /// 更新单条内容
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="columnOne"></param>
        /// <param name="CT"></param>
        public void updateProject(DataGridView dgv,RandomTemp temp,int ranIndex)
        {

            int index = dgv.Rows.Add();
            dgv.Rows[index].Cells[0].Value = "方案" + temp.ProjectNo;
            dgv.Rows[index].Cells[1].Value = temp.ColumnOne.ToString();
            dgv.Rows[index].Cells[2].Value = temp.ColumnTwo.ToString();
            string memo = "";
            if (tongjixianshi.Contains(temp.ProjectNo))
            {
                if (Project[temp.ProjectNo] != 0)
                {
                    string[] arrs = ProjectMemo[temp.ProjectNo].Split(',');

                    for (int i = 0; i < arrs.Length; i++)
                    {
                        //int.Parse(arrs[i]);
                        memo += arrs[i] + " * " + Multiples[int.Parse(Project[temp.ProjectNo].ToString()) - 1] + "倍 , ";
                        //try
                        //{
                        //    Summarys[int.Parse(arrs[i])] += int.Parse(Multiples[int.Parse(Project[temp.ProjectNo].ToString()) - 1]);
                        //}
                        //catch (Exception)
                        //{
                        //    Summarys.Add(int.Parse(arrs[i]), int.Parse(Multiples[int.Parse(Project[temp.ProjectNo].ToString()) - 1]));
                        //}

                    }
                }
            }
            
             dgv.Rows[index].Cells[4].Value = Project[temp.ProjectNo] + "次 : " + memo;

            //if (Project[temp.ProjectNo] >= ProjectMax)//满15次应该保留
                
            //else
            //    dgv.Rows[index].Cells[4].Value = Project[temp.ProjectNo] + "次 : " + ProjectMemo[temp.ProjectNo];

           // dgv.Rows[index].Cells[4].Value = temp.ProjectMemo.ToString();

            dgv.Rows[index].Cells[3].Value = "";
            for (int x = 0; x < temp.SingleCount1; x++)
            {
                dgv.Rows[index].Cells[ 3].Value += "X ";
            }
            temp.Rowindex1 = index;

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        public bool ExportDataGridview(DataGridView gridView, bool isShowExcle)//生成Excel    
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象    
            Excel.Application excel = new Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称    
            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            }
            //填充数据    
            for (int i = 0; i < gridView.RowCount; i++)
            {
                for (int j = 0; j < gridView.ColumnCount; j++)
                {
                    if (gridView[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + gridView[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //获取Configuration对象
            if (int.Parse(tb_ColumnOneRows.Text) < int.Parse(tb_ColumnTwoRows.Text))
            {
                MessageBox.Show("列一行数不可小于列二行数","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            //if ((int.Parse(tb_CTMax.Text) - int.Parse(tb_CTMin.Text)) < (int.Parse(tb_ColumnTwoRows.Text) * int.Parse(tb_CTCount.Text)))
            //{
            //    MessageBox.Show("列二生成个数大于随机数的范围，错误设置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
           

            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            //写入<add>元素的Value
            //config.AppSettings.Settings["ColumnOneCount"].Value = tb_COCount.Text;
            config.AppSettings.Settings["ColumnOneMin"].Value = tb_COMin.Text;
            config.AppSettings.Settings["ColumnOneMax"].Value = tb_COMax.Text;

            //config.AppSettings.Settings["ColumnTwoCount"].Value = tb_CTCount.Text;
            config.AppSettings.Settings["ColumnTwoMin"].Value = tb_CTMin.Text;
            config.AppSettings.Settings["ColumnTwoMax"].Value = tb_CTMax.Text;

           // config.AppSettings.Settings["ProjectCount"].Value = tb_ProjectCount.Text;
            //config.AppSettings.Settings["ProjectMax"].Value = tb_ProjectMax.Text;
            config.AppSettings.Settings["ProjectRows"].Value = tb_AdditionalColumnCount.Text;
            config.AppSettings.Settings["SingleMax"].Value = tb_SingleMax.Text;
            config.AppSettings.Settings["ColumnOneRows"].Value = tb_ColumnOneRows.Text;
            config.AppSettings.Settings["ColumnTwoRows"].Value = tb_ColumnTwoRows.Text;
            config.AppSettings.Settings["AdditionalColumnCount"].Value = tb_AdditionalColumnCount.Text;
            //config.AppSettings.Settings["AddColumnTwoCount"].Value = tb_AddColumnTwoCount.Text;
            //config.AppSettings.Settings["CTZeroCount"].Value = tb_CTZeroCount.Text;
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            loadConfig();
            initView();//初始化
            MessageBox.Show("保存成功");
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabMain.SelectedIndex)
            {
                case 0:
                    //loadConfig();
                    
                    break;
               
                case 1:
                   // tb_COCount.Text = ColumnOneCount.ToString();
                    tb_COMin.Text = ColumnOneMin.ToString();
                    tb_COMax.Text = ColumnOneMax.ToString();
                   // tb_CTCount.Text = ColumnTwoCount.ToString();
                    tb_CTMin.Text = ColumnTwoMin.ToString();
                    tb_CTMax.Text = ColumnTwoMax.ToString();
                   // tb_ProjectCount.Text = ProjectCount.ToString();
                  //  tb_ProjectMax.Text = ProjectMax.ToString();
                    //tb_AdditionalColumnCount.Text = ProjectRows.ToString();
                    tb_SingleMax.Text = SingleMax.ToString();
                    tb_ColumnOneRows.Text = ColumnOneRows.ToString();
                    tb_ColumnTwoRows.Text = ColumnTwoRows.ToString();
                    tb_AdditionalColumnCount.Text = AdditionalColumnCount.ToString();
                    //tb_AddColumnTwoCount.Text = AddColumnTwoCount.ToString();
                    //tb_CTZeroCount.Text = CTZeroCount.ToString();
                    break;

            } 
        }

        private void btn_Default_Click(object sender, EventArgs e)
        {
            //默认
            //tb_COCount.Text = "1";
            tb_COMin.Text ="1";
            tb_COMax.Text = "36";
           // tb_CTCount.Text = "5";
            tb_CTMin.Text = "1";
            tb_CTMax.Text = "20";
            //tb_ProjectCount.Text = "500";
            //tb_ProjectMax.Text = "15";
            //tb_AdditionalColumnCount.Text = "3";
            tb_SingleMax.Text = "15";
            tb_ColumnOneRows.Text = "3";
            tb_ColumnTwoRows.Text = "3";
            tb_AdditionalColumnCount.Text = "0";
            //tb_CTZeroCount.Text = "20";
            //tb_AddColumnTwoCount.Text = "0";

            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //写入<add>元素的Value
            //config.AppSettings.Settings["ColumnOneCount"].Value = tb_COCount.Text;
            config.AppSettings.Settings["ColumnOneMin"].Value = tb_COMin.Text;
            config.AppSettings.Settings["ColumnOneMax"].Value = tb_COMax.Text;

           // config.AppSettings.Settings["ColumnTwoCount"].Value = tb_CTCount.Text;
            config.AppSettings.Settings["ColumnTwoMin"].Value = tb_CTMin.Text;
            config.AppSettings.Settings["ColumnTwoMax"].Value = tb_CTMax.Text;

            //config.AppSettings.Settings["ProjectCount"].Value = tb_ProjectCount.Text;
            //config.AppSettings.Settings["ProjectMax"].Value = tb_ProjectMax.Text;
            //config.AppSettings.Settings["ProjectRows"].Value = tb_AdditionalColumnCount.Text;
            config.AppSettings.Settings["SingleMax"].Value = tb_SingleMax.Text;
            config.AppSettings.Settings["ColumnOneRows"].Value = tb_ColumnOneRows.Text;
            config.AppSettings.Settings["ColumnTwoRows"].Value = tb_ColumnTwoRows.Text;
            config.AppSettings.Settings["AdditionalColumnCount"].Value = tb_AdditionalColumnCount.Text;
            //config.AppSettings.Settings["AddColumnTwoCount"].Value = tb_AddColumnTwoCount.Text;
            //config.AppSettings.Settings["CTZeroCount"].Value = tb_CTZeroCount.Text;
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            loadConfig();
            initView();//初始化
            MessageBox.Show("还原成功");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 0; //开始打印位置
            int y = 0;
            for (int i = 0; i < dgv_Random.Rows.Count; i++)
            {

                for (int j = 0; j < dgv_Random.Columns.Count; j++)
                {

                    try
                    {
                        System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10); //字体设置
                        e.Graphics.DrawString(dgv_Random.Rows[i].Cells[j].Value.ToString(), drawFont, Brushes.Blue, x, y);
                        x = x + 60; //宽度设置
                    }
                    catch (Exception) { } //当遇到空值是发生。
                }
                x = 0;
                y += 40; //行高
            } 
        }

       
        private void btn_clearSave_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("将清除当前已保存方案方案，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SaverandomTemps.Clear();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        
        private void tabMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.Q) || (e.Modifiers == Keys.Control && e.KeyCode == Keys.O))
            {
                txt_SXRow1.Focus();
            }
            if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.W) || (e.Modifiers == Keys.Control && e.KeyCode == Keys.P))
            {
                txt_SXRow2.Focus();
            }
            if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.A) || (e.Modifiers == Keys.Control && e.KeyCode == Keys.L))
            {
                btn_Statistics.Focus();
            }

            if (e.KeyCode == Keys.PageDown)//下一页
            {
                pageCurrent++;
                if (pageCurrent > pageCount)
                {
                    MessageBox.Show("已经是最后一页，请点击“上一页”查看！");
                    pageCurrent--;
                    return;
                }
                else
                {
                    nCurrent = pageSize * (pageCurrent - 1);
                }
                LoadData();
                if (txt_SXRow1.Text != null)
                {
                    setBiaoJi();
                }
            }
            if (e.KeyCode == Keys.PageUp)//上一页
            {
                pageCurrent--;
                if (pageCurrent <= 0)
                {
                    MessageBox.Show("已经是第一页，请点击“下一页”查看！");
                    pageCurrent++;
                    return;
                }
                else
                {
                    nCurrent = pageSize * (pageCurrent - 1);
                }

                LoadData();
                if (txt_SXRow1.Text != null)
                {
                    setBiaoJi();

                }
            }
        }

       
        /// <summary>
        /// 分页加载数据
        /// </summary>
        private void LoadData()
        {
            int nStartPos = 0;   //当前页面开始记录行
            int nEndPos = 0;     //当前页面结束记录行

            //DataTable dtTemp = dtInfo.Clone();   //克隆DataTable结构框架

            if (pageCurrent == pageCount)
                nEndPos = nMax;
            else
                nEndPos = pageSize * pageCurrent;

            nStartPos = nCurrent;

            tb_page.Text = Convert.ToString(pageCurrent) ;
            lb_page.Text =  "/" + pageCount;

            ranNow.Clear();

            //从元数据源复制记录行
            for (int i = nStartPos; i < nEndPos; i++)
            {
               // dtTemp.ImportRow(dtInfo.Rows[i]);
                    ranNow.Add(randomTemps[i]);
                nCurrent++;
            }

            updateDGV(dgv_Random, ranNow);


        }
        private void btn_upPage_Click(object sender, EventArgs e)
        {
            pageCurrent--;
            if (pageCurrent <= 0)
            {
                MessageBox.Show("已经是第一页，请点击“下一页”查看！");
                pageCurrent++;
                return;
            }
            else
            {
                nCurrent = pageSize * (pageCurrent - 1);
            }

            LoadData();
            if (txt_SXRow1.Text != null)
            {
                setBiaoJi();

            }
        }

        private void btn_nextPage_Click(object sender, EventArgs e)
        {
            pageCurrent++;
            if (pageCurrent > pageCount)
            {
                MessageBox.Show("已经是最后一页，请点击“上一页”查看！");
                pageCurrent--;
                return;
            }
            else
            {
                nCurrent = pageSize * (pageCurrent - 1);
            }
            LoadData();
            if (txt_SXRow1.Text != null)
            {
                setBiaoJi();

            }
        }

        private void tb_page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int tempPage = pageCurrent;
                    pageCurrent = int.Parse(tb_page.Text);
                    if (pageCurrent > pageCount || pageCurrent <= 0)
                    {
                        MessageBox.Show("无效数字!");
                        pageCurrent = tempPage;
                        return;
                    }
                    else
                    {
                        nCurrent = pageSize * (pageCurrent - 1);
                    }
                    LoadData();
                    if (txt_SXRow1.Text != null)
                    {
                        setBiaoJi();

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("无效数字!");
                    //throw;
                }
                
            }
        }
        /// <summary>
        /// 对当前页进行标记
        /// </summary>
        private void setBiaoJi()
        {
            string columnOne = txt_SXRow1.Text;
            //对当前第一页数据进行标记
            for (int i = 0; i < ranNow.Count; i++)
            {
                RandomTemp randomtemp = ranNow[i];
                string[] strarr1 = randomtemp.ColumnOne.Split(',');
                for (int x = 0; x < ColumnOneCount; x++)
                {
                    if (strarr1[x].Equals(columnOne))
                    {

                        if (ranNow[i].SingleCount1 < SingleMax)
                        {
                            //标记黄色
                            //signCount++;
                            dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Yellow;
                            
                            //加X标记
                            //randomTemps[i].SingleCount1++;
                            dgv_Random.Rows[randomtemp.Rowindex1].Cells[3].Value = "";
                            for (int j = 0; j < ranNow[i].SingleCount1; j++)
                            {
                                dgv_Random.Rows[randomtemp.Rowindex1].Cells[3].Value += "X ";
                            }
                            //更新方案被筛选次数
                            // Project[randomtemp.ProjectNo]++;//次数++
                            // checkProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]);
                            updateProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]);
                        }
                        else
                        {
                            //不标记红色了
                            //dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Red;
                        }

                    }

                }
            }
        }

        private void btn_Statistics_Click(object sender, EventArgs e)
        {
            StatisticsForm statForm = new StatisticsForm(SaverandomTemps);
            statForm.Show();
        }

        private void btn_offset_Click(object sender, EventArgs e)
        {
             Random ran = new Random(GetRandomSeed());
            int CORan=ran.Next(1,ColumnOneRows+1);
            int CTRan=ran.Next(1,ColumnTwoRows+1);
            if (MessageBox.Show(string.Format("随机数：{0}，{1}，是否确定错位？", CORan, CTRan), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                
            }
        }

        private void btn_save_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < randomTemps.Count; i++)
            {
                RandomTemp randomtemp = randomTemps[i];
                TotalSaveRandomTemps.Add(randomtemp);
            }
            MessageBox.Show("已成功保存到总保存");
        }

        private void btn_setBeishu_Click(object sender, EventArgs e)
        {
            MultipleSettingForm form = new MultipleSettingForm(int.Parse(tb_SingleMax.Text.ToString()));
            DialogResult res= form.ShowDialog();
            if (res == DialogResult.OK)
            {
                loadConfig();
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format(" 列一：{0}\n 列二：{1}\n是否确定删除？", txt_SXRow1.Text, txt_SXRow2.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                txt_Summary.Text = "";
                tongjixianshi.Clear();
                string columnOne = txt_SXRow1.Text;
                string sxRow2 = txt_SXRow2.Text;
                deleteCount = 0;
                List<int> deleno = new List<int>();
                //先删除数据，再重新绘制，再标记蓝色 X 次数
                //有问题，删除了randomtemp 这边没有更新 导致 有相邻两个方案需要删除时 无法删除第二个
                //解决方案：使用 deleno list集合保存no 然后一次性删除
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    RandomTemp randomtemp = randomTemps[i];

                    string[] strarr1 = randomtemp.ColumnOne.Split(',');

                    for (int x = 0; x < ColumnOneCount; x++)
                    {
                        if (randomTemps[i].SingleCount1 < SingleMax)//标记红色不删除
                        {
                            if (strarr1[x].Equals(columnOne))
                            {
                                string[] strarr = randomtemp.ColumnTwo.Split(','); ;
                                //查询列二
                                // strarr = randomtemp.ColumnTwo.Split(',');


                                if (sxRow2.Contains(","))//包含逗号 分隔
                                {
                                    string[] CTArr = sxRow2.Split(',');

                                    for (int h = 0; h < CTArr.Length; h++)
                                        for (int j = 0; j < ColumnTwoCount * (strarr.Length); j++)
                                        {
                                            if (strarr[j].Equals(CTArr[h]))//应该删除
                                            {
                                                // dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Style.BackColor = Color.Red;
                                                deleteCount++;
                                                deleno.Add(randomtemp.ProjectNo);
                                                //deleteProject(randomtemp.ProjectNo);
                                            }

                                        }
                                }
                                else
                                {
                                    for (int j = 0; j < ColumnTwoCount * (strarr.Length); j++)
                                    {
                                        if (strarr[j].Equals(sxRow2))//应该删除
                                        {
                                            // dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Style.BackColor = Color.Red;
                                            deleteCount++;
                                            deleno.Add(randomtemp.ProjectNo);
                                            //deleteProject(randomtemp.ProjectNo);
                                        }

                                    }
                                }



                            }
                        }

                    }

                }
                for (int i = 0; i < deleno.Count; i++)
                {
                    deleteProject(deleno[i]);
                }
                deleno.Clear();
                if (randomTemps.Count > 0)
                {
                    nMax = randomTemps.Count;
                    pageCount = (nMax / pageSize);    //计算出总页数
                    if ((nMax % pageSize) > 0) pageCount++;
                    pageCurrent = 1;    //当前页数从1开始
                    nCurrent = 0;       //当前记录数从0开始
                    LoadData();
                }
                else
                {
                    dgv_Random.Rows.Clear();
                }
                MessageBox.Show(string.Format("删除 {0} 个方案", deleteCount), "删除结果", MessageBoxButtons.OK, MessageBoxIcon.None);
            }

           

           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                Import_Text(file);
                //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
           // DataTableToExcel("彩牛通数据", GetRtsToTable(randomTemps), false);
            string time = DateTime.Now.ToString("yyyy-MM-dd hh：mm：ss");   
            Export_Text(time);
        }

        private void Export_Text(string name)
        {
            string file_path = @"导出文件\彩牛通-" + name + ".txt"; 
            FileStream fileStream = new FileStream(file_path, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

            StringBuilder strBuilder = new StringBuilder();

            try
            {
                int projectNo = 0;
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    strBuilder = new StringBuilder();
                    if (i % ColumnOneRows == 0)
                    {
                        projectNo++;
                    }
                    strBuilder.Append(projectNo + " ");
                    strBuilder.Append(randomTemps[i].ColumnOne.ToString() + " ");
                    strBuilder.Append(randomTemps[i].ColumnTwo.ToString() + " ");
                    strBuilder.Remove(strBuilder.Length - 1, 1);
                    streamWriter.WriteLine(strBuilder.ToString());
                }
                
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.Message;
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
                MessageBox.Show("导出数据成功!", "系统信息");
                //System.Windows.Forms.Application.StartupPath exe文件路径
                System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "//" + "导出文件");
               // System.Diagnostics.Process.Start("ExpLore", Environment.CurrentDirectory);
            }   
        }

        private void Import_Text(string file)
        {
            StreamReader sr = new StreamReader(file, Encoding.Default);
            String line;
            //clearDGVandRts();
            string projectNo = "";
            int proNo = ProjectNo;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                string[] strs = line.Split(' ');
                RandomTemp randomtemp = new RandomTemp();
                randomtemp.ColumnOne = strs[1];
                randomtemp.ColumnTwo = strs[2];
                randomtemp.SingleCount1 = 0;
                randomtemp.Rowindex1 = -1;
                randomtemp.ProjectCount = 0;
                randomtemp.ProjectMemo = "0次";
                
                if (projectNo != strs[0])
                {
                    
                    Project.Add(ProjectNo, 0);
                    ProjectMemo.Add(ProjectNo, "");
                    if (projectNo != "")
                        proNo++;
                    ProjectNo++;
                    projectNo = strs[0];
                    
                }
                randomtemp.ProjectNo = proNo;
                randomTemps.Add(randomtemp);
                
            }
            if (randomTemps.Count > 0)
            {
                
                pageSize = 100 * (ColumnOneRows);      //设置页面行数 100方案数
                nMax = (randomTemps.Count / ColumnOneRows) * (ColumnOneRows);
                pageCount = (nMax / pageSize);    //计算出总页数
                if ((nMax % pageSize) > 0) pageCount++;
                pageCurrent = 1;    //当前页数从1开始
                nCurrent = 0;       //当前记录数从0开始
                LoadData();
            }
            MessageBox.Show("添加成功");
            
        }
    }

}
