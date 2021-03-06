﻿using System;
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

namespace 彩牛通
{
    public partial class CNTMainFrm : CCSkinMain
    {
        List<RandomTemp> randomTemps;
        List<RandomTemp> helpSaveRandomTemps;
        List<RandomTemp> SaverandomTemps;//保存筛选到15次的方案
        List<RandomTemp> TotalSaveRandomTemps;//保存筛选到15次的方案
        //private int ProjectNo=0;
        Dictionary<int, int> Project;
        Dictionary<int, int> ProjectgdDeleIndex;

        Dictionary<int, string> zhqDic;
        int ZHQMin, ZHQMax, ZHQCount, ZHQRowCount;
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
        int SelectorMax;
        int SelectorMin;
        bool status = false;
        int ColumnOneRows;
        int ColumnTwoRows;
        int AdditionalColumnCount;
        int SelectorCount;
        //辅助
        List<RandomTemp> help_randomTemps;
        List<RandomTemp> help3_randomTemps;
        int Help_ColumnCount;
        int Help_ColumnMin;
        int Help_ColumnMax;
        int Help_ColumnRowCount;
        int Help_AdditionalColumnCount;

        List<RandomTemp> help2_randomTemps;
        int Help2_ColumnCount;
        int Help2_ColumnMin;
        int Help2_ColumnMax;
        int Help2_ColumnRowCount=1;//固定一行

        int Help_automaticCount;
        //int AddColumnTwoCount;
        //int CTZeroCount;
        int ColumnFourColumns;

        //分页
        int pageSize = 0;     //每页显示行数
        int nMax = 0;         //总记录数
        int pageCount = 0;    //页数＝总记录数/每页显示行数
        int pageCurrent = 0;   //当前页号
        int nCurrent = 0;      //当前记录行
        DataSet ds = new DataSet();
        DataTable dtInfo = new DataTable();
        List<RandomTemp> ranNow = new List<RandomTemp>();
        string[] Selector;//选号器
        List<int> AdditionalRandoms = new List<int>();

        string[] COTemp;//临时存储变换选号的随机
        public CNTMainFrm()
        {
            InitializeComponent();
            randomTemps = new List<RandomTemp>();
            help_randomTemps = new List<RandomTemp>();
            help3_randomTemps = new List<RandomTemp>();
            helpSaveRandomTemps = new List<RandomTemp>();
            help2_randomTemps = new List<RandomTemp>();
            SaverandomTemps = new List<RandomTemp>();
            TotalSaveRandomTemps = new List<RandomTemp>();
            Project = new Dictionary<int, int>();
            ProjectgdDeleIndex = new Dictionary<int, int>();
            zhqDic = new Dictionary<int, string>();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void CNTMainFrm_Load(object sender, EventArgs e)
        {
            loadConfig();
            
            initView();
        }
        private string readSelectorSetting()
        {
            string contents = File.ReadAllText(@"SelectorSetting.txt", Encoding.Default);
            return contents;
        }

        /// <summary>
        /// 初始化一些窗体
        /// </summary>
        private void initView()
        {
            cb_Column1.Items.Clear();
            //for (int i = 2; i <= AdditionalColumnCount+2; i++)
            //{
            //    cb_Column.Items.Add("列"+i+":");
            //}
            cb_Column1.Items.Add("列一:");
            cb_Column1.Items.Add("列二:");
            cb_Column1.SelectedIndex = 0;
            cb_Column1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//不可编辑状态

            initDGV(dgv_Random);

            
        }
        /// <summary>
        /// 初始化DGV
        /// </summary>
        /// <param name="dgv"></param>
        private void initDGV(DataGridView dgv)
        {
            dgv_help.Columns.Clear();
            DataGridViewTextBoxColumn C = new DataGridViewTextBoxColumn();
            C.HeaderCell.Value = "辅助列";
            C.Width = 400;
            dgv_help.Columns.Add(C);

            DataGridViewTextBoxColumn HelpC4 = new DataGridViewTextBoxColumn();
            HelpC4.HeaderCell.Value = "列4";
            HelpC4.Width = 250;
            dgv_help3.Columns.Add(HelpC4);


            dgv_help2.Columns.Clear();
            DataGridViewTextBoxColumn Ch2 = new DataGridViewTextBoxColumn();
            Ch2.HeaderCell.Value = "辅助列2";
            Ch2.Width = 100;
            dgv_help2.Columns.Add(Ch2);


            dgv.Columns.Clear();
            DataGridViewTextBoxColumn C1 = new DataGridViewTextBoxColumn();
            C1.HeaderCell.Value = "方案号";
            C1.Width = 100;
            C1.Visible = false;
            dgv.Columns.Add(C1);



            DataGridViewTextBoxColumn C3 = new DataGridViewTextBoxColumn();
            C3.HeaderCell.Value = "列1 期号";
            C3.Width = 150;
            dgv.Columns.Add(C3);

            DataGridViewTextBoxColumn C4 = new DataGridViewTextBoxColumn();
            C4.HeaderCell.Value = "列2 选号器1";
            C4.Width = 150;
            dgv.Columns.Add(C4);
            DataGridViewTextBoxColumn C5 = new DataGridViewTextBoxColumn();
            C5.HeaderCell.Value = "列2 选号器2";
            C5.Width = 150;
            dgv.Columns.Add(C5);

            DataGridViewTextBoxColumn C2 = new DataGridViewTextBoxColumn();
            C2.HeaderCell.Value = "列3";
            C2.Width = 250;
            dgv.Columns.Add(C2);

            DataGridViewTextBoxColumn NewC4 = new DataGridViewTextBoxColumn();
            NewC4.HeaderCell.Value = "列4";
            NewC4.Width = 250;
            dgv.Columns.Add(NewC4);

            DataGridViewTextBoxColumn NewC5 = new DataGridViewTextBoxColumn();
            NewC5.HeaderCell.Value = "列5 排列号2";
            NewC5.Width = 150;
            dgv.Columns.Add(NewC5);
            //for (int i = 0; i < AdditionalColumnCount; i++)
            //{
            //    DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            //    c.HeaderCell.Value = "列" + (i + 3);
            //    c.Width = 200;
            //    dgv.Columns.Add(c);
            //}
            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.HeaderCell.Value = "单条统计";
            c1.Width = 300;
            c1.Visible = false;
            dgv.Columns.Add(c1);

            DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            c2.HeaderCell.Value = "方案筛选次数";
            c2.Width = 150;
            c2.Visible = false;
            dgv.Columns.Add(c2);

            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 5; i++)
            {
                dgv.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgv_zhq.Columns.Clear();
            DataGridViewTextBoxColumn B1 = new DataGridViewTextBoxColumn();
            B1.HeaderCell.Value = "转化器";
            B1.Width = 100;
            dgv_zhq.Columns.Add(B1);
            DataGridViewTextBoxColumn B2 = new DataGridViewTextBoxColumn();
            B2.HeaderCell.Value = "对应号";
            B2.Width = 100;
            dgv_zhq.Columns.Add(B2);

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
            SelectorCount = int.Parse(config.AppSettings.Settings["SelectorCount"].Value);
            SelectorMax = int.Parse(config.AppSettings.Settings["SelectorMax"].Value);
            SelectorMin = int.Parse(config.AppSettings.Settings["SelectorMin"].Value);
            //AddColumnTwoCount = int.Parse(config.AppSettings.Settings["AddColumnTwoCount"].Value);
            //CTZeroCount = int.Parse(config.AppSettings.Settings["CTZeroCount"].Value);
            ZHQMax = int.Parse(config.AppSettings.Settings["ZHQMax"].Value);
            ZHQMin = int.Parse(config.AppSettings.Settings["ZHQMin"].Value);
            ZHQCount = int.Parse(config.AppSettings.Settings["ZHQCount"].Value);
            ZHQRowCount = int.Parse(config.AppSettings.Settings["ZHQRowCount"].Value);
            //设置
            Help_ColumnCount = int.Parse(config.AppSettings.Settings["Help_ColumnCount"].Value);
            Help_ColumnMin = int.Parse(config.AppSettings.Settings["Help_ColumnMin"].Value);
            Help_ColumnMax = int.Parse(config.AppSettings.Settings["Help_ColumnMax"].Value);
            Help_ColumnRowCount = int.Parse(config.AppSettings.Settings["Help_ColumnRowCount"].Value);
            Help_AdditionalColumnCount = int.Parse(config.AppSettings.Settings["Help_AdditionalColumnCount"].Value);

            Help2_ColumnCount = int.Parse(config.AppSettings.Settings["Help2_ColumnCount"].Value);
            Help2_ColumnMin = int.Parse(config.AppSettings.Settings["Help2_ColumnMin"].Value);
            Help2_ColumnMax = int.Parse(config.AppSettings.Settings["Help2_ColumnMax"].Value);

            Help_automaticCount = int.Parse(config.AppSettings.Settings["Help_automaticCount"].Value);
            ColumnFourColumns = int.Parse(config.AppSettings.Settings["ColumnFourColumns"].Value); 
            Selector = readSelectorSetting().Split(';');
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            //清除所有
            if (MessageBox.Show("将清除当前所有方案，同时清除已保存数据，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                status = false;
                deleteCount = 0;
                signCount = 0;
                randomTemps.Clear();
                dgv_Random.Rows.Clear();
                SaverandomTemps.Clear();
                dgv_Save.Rows.Clear();
            }
           
        }

        private void btn_Random_Click(object sender, EventArgs e)
        {
            RandomCreate();
        }
        private void RandomCreate()
        {
            deleteCount = 0;
            signCount = 0;
            randomTemps.Clear();
            dgv_Random.Rows.Clear();
            Project.Clear();
            ProjectgdDeleIndex.Clear();
            SaverandomTemps.Clear();
            dgv_Save.Rows.Clear();
            status = true;
            zhqDic.Clear();
            dgv_zhq.Rows.Clear();
            setZHQ();
            //生成随机数方案
            //try
            //{
            help_randomTemps.Clear();
            help3_randomTemps.Clear();
            dgv_help.Rows.Clear();
            GetHelpRan();

            help2_randomTemps.Clear();
            dgv_help2.Rows.Clear();
            //辅助列2
            //GetHelpRan2();

            GenerateData();
            setAdditionalColumn();
            setColumnTwoNew();
            setColumnZhq();
            //setColumnTwoBiliNew();
            //for (int i = 0; i < randomTemps.Count; i++)
            //{
            //    RandomTemp rantemp = randomTemps[i];
            //    // COTemp[i] = rantemp.ColumnOne;
            //    string[] cos = rantemp.ColumnOne.Split(',');
            //    string newCO = "";
            //    for (int j = 0; j < cos.Length; j++)
            //    {
            //        if (j != cos.Length - 1)
            //        {
            //            newCO += Selector[int.Parse(cos[j]) - 1] + ",";
            //        }
            //        else
            //        {
            //            newCO += Selector[int.Parse(cos[j]) - 1];
            //        }
            //    }
            //    randomTemps[i].ColumnOne = newCO;
            //}
            pageSize = 100 * (ColumnOneRows);      //设置页面行数 100方案数
            nMax = (randomTemps.Count / ColumnOneRows) * (ColumnOneRows);
            pageCount = (nMax / pageSize);    //计算出总页数
            if ((nMax % pageSize) > 0) pageCount++;
            pageCurrent = 1;    //当前页数从1开始
            nCurrent = 0;       //当前记录数从0开始
            LoadData();

            COTemp = new string[randomTemps.Count];
            //MessageBox.Show(string.Format("成功生成{0}个随机数方案", ProjectCount));
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("随机数生成失败，请检查设置选项是否有误。重新设置或联系开发者。");
            //}
           
        }
        private void setColumnZhq()
        {
            for (int i = 0; i < randomTemps.Count; i++)
            {
                for (int j = 1; j < ZHQCount + 1; j++)
                {
                    string s = randomTemps[i].ColumnOne.ToString();
                    string[] strs = zhqDic[j].Split(',');
                    for (int x = 0; x < strs.Length; x++)
                    {
                        if (strs[x].Equals(s))
                        {
                            randomTemps[i].ColumnZhq = j.ToString();
                            // dgv.Rows[index].Cells[3].Value = j;//选号器2
                        }
                    }


                }
            }
        }
        private void setZHQ()
        {
            int bili = ZHQRowCount;
            int total = ZHQRowCount * ZHQCount;
            List<int> lists = GETZhq(total, 1, ZHQMax + 1);
            int x = 1;
            for (int i = 0; i < total; i += bili)
            {
                string s = "";
                for (int j = 0; j < bili; j++)
                {
                    if (j == bili - 1)
                    {
                        s += lists[i + j];
                    }
                    else
                    {
                        s += lists[i + j] + ",";
                    }

                }
                zhqDic.Add(x, s);
                x++;
            }
            for (int i = 0; i < ZHQCount; i++)
            {
                int index = dgv_zhq.Rows.Add();
                dgv_zhq.Rows[index].Cells[0].Value = i + 1;
                dgv_zhq.Rows[index].Cells[1].Value = zhqDic[i + 1];
            }
        }
        private List<int> GETZhq(int count, int min, int max)
        {
            Random ran = new Random(GetRandomSeed());
            List<int> lists = new List<int>();

            while (lists.Count < count)
            {
                int x = ran.Next(min, max);
                if (!lists.Contains(x))
                {
                    lists.Add(x);
                }
            }
            return lists;
        }
        private String getAdditionalString(String ct)
        {

            return ct;
        }


        /// <summary>
        /// 根据列三ColumnTwo生成列四
        /// </summary>
        private void setColumnNewFour()
        {
            int bili = Help_ColumnCount;
            int bili2 = bili / ColumnTwoCount;
            int t = bili;
            List<int> lists_heng = new List<int>();
            List<int> lists_lie = new List<int>();
            lists_lie.Clear();
            lists_heng.Clear();

            for (int j = 0; j < ColumnFourColumns; j++)
            {
                Random ran = new Random(GetRandomSeed());
                int index = ran.Next(1, Help_ColumnCount);
                if (!lists_heng.Contains(index))
                {
                    lists_heng.Add(index);
                }
                else
                {
                    j--;
                    continue;
                }
            }
            for (int j = 0; j < Help_ColumnCount; j++)
            {
                Random ran = new Random(GetRandomSeed());
                int index = ran.Next(0, Help_ColumnCount);
                if (!lists_lie.Contains(index))
                {
                    lists_lie.Add(index);
                }
                else
                {
                    j--;
                    continue;
                }
            }
            int mm = 0 ;
            for (int i = 0; i < help3_randomTemps.Count; i++)
            {
                if (t == bili)
                {
                    string[] ColumnTwos = help_randomTemps[mm].ColumnNewOne.ToString().Split(',');
                    string[] newCts = new string[bili];

                    for (int j = 0; j < ColumnFourColumns; j++)
                    {
                        int n = 0;
                        for (int x = 0; x < bili; x += bili2)
                        {
                            if (j == 0)
                            {
                                int m = 0;
                                while (m < bili2)
                                {
                                    int index = i + x + m;
                                    help3_randomTemps[index].ColumnNewFour = ColumnTwos[(lists_lie[x] + n) % ColumnTwoCount].ToString();
                                    //help3_randomTemps[i + x].ColumnNewFour = ColumnTwos[(lists_lie[x])].ToString();
                                    newCts[x] = ColumnTwos[(lists_lie[x])].ToString();
                                    m++;
                                }
                                n++;

                            }
                            else
                            {
                                help3_randomTemps[i + x].ColumnNewFour = help3_randomTemps[i + x].ColumnNewFour + "," + newCts[(lists_heng[j] + x) % bili].ToString();
                            }
                        }
                        //for (int x = 0; x < bili; x++)
                        //{
                        //    if (j == 0)
                        //    {
                        //        help3_randomTemps[i + x].ColumnNewFour = ColumnTwos[(lists_lie[x])].ToString();
                        //        newCts[x] = ColumnTwos[(lists_lie[x])].ToString();
                        //    }
                        //    else
                        //    {
                        //        help3_randomTemps[i + x].ColumnNewFour = help3_randomTemps[i + x].ColumnNewFour + "," + newCts[(lists_heng[j] + x) % bili].ToString();
                        //    }
                        //}
                    }
                    mm++;
                    t = 1;
                }
                else
                {
                    t++;
                }
            }
        }
        private void setColumnNewFour1()
        {
            int bili = Help_ColumnCount;
            int t = bili;
            List<int> lists_heng = new List<int>();
            List<int> lists_lie = new List<int>();
            lists_lie.Clear();
            lists_heng.Clear();

            for (int j = 0; j < ColumnFourColumns; j++)
            {
                Random ran = new Random(GetRandomSeed());
                int index = ran.Next(1, Help_ColumnCount);
                if (!lists_heng.Contains(index))
                {
                    lists_heng.Add(index);
                }
                else
                {
                    j--;
                    continue;
                }
            }
            for (int j = 0; j < Help_ColumnCount; j++)
            {
                Random ran = new Random(GetRandomSeed());
                int index = ran.Next(0, Help_ColumnCount);
                if (!lists_lie.Contains(index))
                {
                    lists_lie.Add(index);
                }
                else
                {
                    j--;
                    continue;
                }
            }
            int m = 0;
            for (int i = 0; i < help3_randomTemps.Count; i++)
            {
                if (t == bili)
                {
                    string[] ColumnTwos = help_randomTemps[m].ColumnNewOne.ToString().Split(',');
                    string[] newCts = new string[bili];

                    for (int j = 0; j < ColumnFourColumns; j++)
                    {
                        for (int x = 0; x < bili; x++)
                        {
                            if (j == 0)
                            {
                                help3_randomTemps[i + x].ColumnNewFour = ColumnTwos[(lists_lie[x])].ToString();
                                newCts[x] = ColumnTwos[(lists_lie[x])].ToString();
                            }
                            else
                            {
                                help3_randomTemps[i + x].ColumnNewFour = help3_randomTemps[i + x].ColumnNewFour + "," + newCts[(lists_heng[j] + x) % bili].ToString();
                            }
                        }
                    }
                    m++;
                    t = 1;
                }
                else
                {
                    t++;
                }
            }
        }
        private void GetHelpRan()
        {
            List<int> ColumnNewOne = new List<int>();
            ColumnNewOne = GETColumnOne(Help_ColumnCount * Help_ColumnRowCount, Help_ColumnMin, Help_ColumnMax + 1);//随机
            
            int i,j;
            for (j = 1; j <= Help_ColumnRowCount; j++)//单条
            {
                List<int> ColumnNewOne1 = new List<int>();
                for (i = 1; i <= Help_ColumnCount; i++)
                {
                    ColumnNewOne1.Add(ColumnNewOne[Help_ColumnCount * (j - 1) + i - 1]);
                }
                string CNO = "";
                for (i = 0; i < ColumnNewOne1.Count; i++)
                {
                    if (i != ColumnNewOne1.Count - 1)
                        CNO += ColumnNewOne1[i] + ",";
                    else
                        CNO += ColumnNewOne1[i] + "";
                }
                RandomTemp randomtemp = new RandomTemp();
                randomtemp.ColumnNewOne = CNO;
                help_randomTemps.Add(randomtemp);
            }
            setHelpAdditionalColumn(); 
            setHelpColumnTwoNew();

            for (int x = 0; x < Help_ColumnRowCount*Help_ColumnCount; x++)
            {
                RandomTemp randomtemp = new RandomTemp();
                help3_randomTemps.Add(randomtemp);
            }
            setColumnTwoSort();
            setColumnNewFour1();

            updateHelpDGV(dgv_help,help_randomTemps,1);
            updateHelpDGV(dgv_help3, help3_randomTemps,2);
        }
        /// <summary>
        /// 排序
        /// </summary>
        private void setColumnTwoSort()
        {
            for (int i = 0; i < help_randomTemps.Count; i++)
            {
                string[] ColumnTwos = help_randomTemps[i].ColumnNewOne.ToString().Split(',');
                int[] ColumnTwosint = Array.ConvertAll(ColumnTwos, new Converter<string, int>(StrToInt));
                Array.Sort(ColumnTwosint);
                String newTwos = "";
                for (int j = 0; j < ColumnTwosint.Length; j++)
                {
                    if (j == 0)
                    {
                        newTwos += ColumnTwosint[j].ToString();
                    }
                    else
                    {
                        newTwos += "," + ColumnTwosint[j];
                    }
                }
                help_randomTemps[i].ColumnNewOne = newTwos;
            }
        }
        public static int StrToInt(string str)
        {
            return int.Parse(str);
        }
        private void GetHelpRan2()
        {
            List<int> ColumnNewOne = new List<int>();
            ColumnNewOne = GETColumnOne(Help2_ColumnCount * Help2_ColumnRowCount, Help2_ColumnMin, Help_ColumnMax + 1);//随机

            int i, j;
            for (j = 1; j <= Help2_ColumnRowCount; j++)//单条
            {
                List<int> ColumnNewOne1 = new List<int>();
                for (i = 1; i <= Help2_ColumnCount; i++)
                {
                    ColumnNewOne1.Add(ColumnNewOne[Help2_ColumnCount * (j - 1) + i - 1]);
                }
                string CNO = "";
                for (i = 0; i < ColumnNewOne1.Count; i++)
                {
                    if (i != ColumnNewOne1.Count - 1)
                        CNO += ColumnNewOne1[i] + ",";
                    else
                        CNO += ColumnNewOne1[i] + "";
                }
                RandomTemp randomtemp = new RandomTemp();
                randomtemp.ColumnNewOne = CNO;
                help2_randomTemps.Add(randomtemp);
            }
            //setHelpAdditionalColumn();
            //setHelpColumnTwoNew();
            updateHelpDGV(dgv_help2, help2_randomTemps,1);
        }
 
        private void setHelpAdditionalColumn()
        {
            int bili =1;

            List<RandomTemp> temps = new List<RandomTemp>();
            int t = 1;
            for (int i = 0; i < help_randomTemps.Count; i++)
            {
                temps.Add(help_randomTemps[i]);
                //List<int> lists = new List<int>();
                //for (int x = 0; x < AdditionalNum + 1; x++)
                //{
                //    lists.Add(temps)
                //}

                if (t == Help_ColumnRowCount)
                {
                    //int index = 0;
                    List<int> lists = new List<int>();
                    for (int AdditionalNum = 0; AdditionalNum < Help_AdditionalColumnCount; AdditionalNum++)
                    {

                        Random ran = new Random(GetRandomSeed());
                        int index = ran.Next(1, Help_ColumnRowCount);
                       // index++;
                        if (!lists.Contains(index))
                        {
                            lists.Add(index);
                            int biliCount = 1;
                            for (int x = Help_ColumnRowCount - 1; x >= 0; x--)
                            {
                                switch (AdditionalNum + 3)
                                {
                                    case 3:
                                        help_randomTemps[i - x].ColumnThree = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 4:
                                        help_randomTemps[i - x].ColumnFour = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 5:
                                        help_randomTemps[i - x].ColumnFive = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 6:
                                        help_randomTemps[i - x].ColumnSix = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 7:
                                        help_randomTemps[i - x].ColumnSeven = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 8:
                                        help_randomTemps[i - x].ColumnEight = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 9:
                                        help_randomTemps[i - x].ColumnNine = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 10:
                                        help_randomTemps[i - x].ColumnTen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 11:
                                        help_randomTemps[i - x].ColumnEleven = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 12:
                                        help_randomTemps[i - x].ColumnTwelve = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 13:
                                        help_randomTemps[i - x].ColumnThirteen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 14:
                                        help_randomTemps[i - x].ColumnFourteen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 15:
                                        help_randomTemps[i - x].ColumnFifteen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 16:
                                        help_randomTemps[i - x].ColumnSixteen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 17:
                                        help_randomTemps[i - x].ColumnSeventeen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 18:
                                        help_randomTemps[i - x].ColumnEighteen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 19:
                                        help_randomTemps[i - x].ColumnNineteen = temps[index * bili].ColumnNewOne.ToString();
                                        break;
                                    case 20:
                                        help_randomTemps[i - x].ColumnTwenty = temps[index * bili].ColumnNewOne.ToString();
                                        break;

                                }

                                if (biliCount == bili)
                                {
                                    if (index == Help_ColumnRowCount - 1)
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
        private void setHelpColumnTwoNew()
        {
            for (int i = 0; i < help_randomTemps.Count; i++)
            {
                switch (Help_AdditionalColumnCount + 2)
                {
                    case 3:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString();
                        break;
                    case 4:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString();
                        break;
                    case 5:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString();
                        break;
                    case 6:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString();
                        break;
                    case 7:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString();
                        break;
                    case 8:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString();
                        break;
                    case 9:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString();
                        break;
                    case 10:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString();
                        break;
                    case 11:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnEleven.ToString();
                        break;
                    case 12:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString();
                        break;
                    case 13:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString();
                        break;
                    case 14:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString();
                        break;
                    case 15:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString() + "," + help_randomTemps[i].ColumnFifteen.ToString();
                        break;
                    case 16:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString() + "," + help_randomTemps[i].ColumnFifteen.ToString() + "," + help_randomTemps[i].ColumnSixteen.ToString();
                        break;
                    case 17:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString() + "," + help_randomTemps[i].ColumnFifteen.ToString() + "," + help_randomTemps[i].ColumnSixteen.ToString() + "," + help_randomTemps[i].ColumnSeventeen.ToString();
                        break;
                    case 18:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString() + "," + help_randomTemps[i].ColumnFifteen.ToString() + "," + help_randomTemps[i].ColumnSixteen.ToString() + "," + help_randomTemps[i].ColumnSeventeen.ToString() + "," + help_randomTemps[i].ColumnEighteen.ToString();
                        break;
                    case 19:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString() + "," + help_randomTemps[i].ColumnFifteen.ToString() + "," + help_randomTemps[i].ColumnSixteen.ToString() + "," + help_randomTemps[i].ColumnSeventeen.ToString() + "," + help_randomTemps[i].ColumnEighteen.ToString() + "," + help_randomTemps[i].ColumnNineteen.ToString();
                        break;
                    case 20:
                        help_randomTemps[i].ColumnNewOne = help_randomTemps[i].ColumnNewOne.ToString() + "," + help_randomTemps[i].ColumnThree.ToString() + "," + help_randomTemps[i].ColumnFour.ToString() + "," + help_randomTemps[i].ColumnFive.ToString() + "," + help_randomTemps[i].ColumnSix.ToString() + "," + help_randomTemps[i].ColumnSeven.ToString() + "," + help_randomTemps[i].ColumnEight.ToString() + "," + help_randomTemps[i].ColumnNine.ToString() + "," + help_randomTemps[i].ColumnTen.ToString() + "," + help_randomTemps[i].ColumnTwelve.ToString() + "," + help_randomTemps[i].ColumnThirteen.ToString() + "," + help_randomTemps[i].ColumnFourteen.ToString() + "," + help_randomTemps[i].ColumnFifteen.ToString() + "," + help_randomTemps[i].ColumnSixteen.ToString() + "," + help_randomTemps[i].ColumnSeventeen.ToString() + "," + help_randomTemps[i].ColumnEighteen.ToString() + "," + help_randomTemps[i].ColumnNineteen.ToString() + "," + help_randomTemps[i].ColumnTwenty.ToString();
                        break;

                }
            }

        }

        private void updateHelpDGV(DataGridView dgv, List<RandomTemp> rt,int type)
        {
            dgv.Rows.Clear();
            int x = 0;
            for (int i = 0; i < rt.Count; i++)
            {
                RandomTemp randomtemp = rt[i];

                int index = dgv.Rows.Add();
                if (type==1)
                {
                    dgv.Rows[index].Cells[0].Value = randomtemp.ColumnNewOne;
                }
                else if (type == 2)
                {
                    //
                    try
                    {
                        dgv.Rows[index].Cells[0].Value = randomtemp.ColumnNewFour.ToString();//选号器 列四
                    }
                    catch (Exception)
                    {
                        dgv.Rows[index].Cells[0].Value = "";//选号器 列四
                    }
                }
               
            }
        }
       
        /// <summary>
        /// 生成随机数方案
        /// </summary>
        private void GenerateData()
        {
            //List<int> ColumnOne = new List<int>();
            List<int> ColumnTwo = new List<int>();
            List<string> ColumnNewTwo = new List<string>();
            List<int> ColumnNewOne = new List<int>();
            int j;
            int i;
            int x;
            for ( x = 0; x < ProjectCount; x++)//方案
            {
               // ColumnOne.Clear();
                ColumnTwo.Clear();
                ColumnNewOne.Clear();

                //生成一个方案
                ColumnNewOne = GETColumnOne(ColumnOneCount * ColumnOneRows, ColumnOneMin, ColumnOneMax + 1);// 列一 随机
                //ColumnOne = GETColumnSelector(ColumnNewOne, ColumnOneCount * ColumnOneRows); 
                ColumnNewTwo = GETColumnSelector(ColumnNewOne, ColumnOneCount * ColumnOneRows);//选号器 
                //ColumnNewTwo = GETColumnNewSelector(SelectorCount * ColumnOneRows, SelectorMin, SelectorMax + 1);//选号器 
                ColumnTwo = GETRandom(ColumnTwoCount * ColumnTwoRows, ColumnTwoMin, ColumnTwoMax + 1);//列三
                int bili = ColumnOneRows / ColumnTwoRows;
                int columnTworow=1;
                int columnJ = 1;
                string CT = "";
                for (j = 1; j <= ColumnOneRows; j++)//单条
                {
                    List<string> ColumnOne1 = new List<string>();

                    for (i = 1; i <= SelectorCount; i++)
                    {
                        //ColumnOne1.Add(ColumnOne[ColumnOneCount * (j - 1) + i - 1]);
                        ColumnOne1.Add(ColumnNewTwo[SelectorCount * (j - 1) + i - 1]);
                    }
                    string CO = "";
                    for ( i = 0; i < ColumnOne1.Count; i++)
                    {
                        if (i != ColumnOne1.Count - 1)
                            CO += ColumnOne1[i] + ",";
                        else
                            CO += ColumnOne1[i] + "";
                    }

                    List<int> ColumnNewOne1 = new List<int>();

                    for (i = 1; i <= ColumnOneCount; i++)
                    {
                        ColumnNewOne1.Add(ColumnNewOne[ColumnOneCount * (j - 1) + i - 1]);
                    }
                    string CNO = "";
                    for (i = 0; i < ColumnNewOne1.Count; i++)
                    {
                        if (i != ColumnNewOne1.Count - 1)
                            CNO += ColumnNewOne1[i] + ",";
                        else
                            CNO += ColumnNewOne1[i] + "";
                    }
                    if (columnTworow == 1 || (columnTworow-1)%bili==0 || bili == 1)
                    {
                        CT = "";
                        List<int> ColumnTwo1 = new List<int>();
                        for ( i = 1; i <= ColumnTwoCount; i++)
                        {
                            ColumnTwo1.Add(ColumnTwo[ColumnTwoCount * (columnJ - 1) + i - 1]);
                        }
                        for ( i = 0; i < ColumnTwo1.Count; i++)
                        {
                            if (i != ColumnTwo1.Count - 1)
                                CT += ColumnTwo1[i] + ",";
                            else
                                CT += ColumnTwo1[i] + "";
                        }
                        columnJ++;
                    }
                    //else
                    //{//ct不变
                    //    //columnTworow = 1;
                    //}
                    columnTworow++;
                    setProject(x + 1, CO, CT,CNO);
                    
                }
                Project.Add(x + 1, 0);
                ProjectgdDeleIndex.Add(x + 1, 0);//初始化
                //中间分隔线
               
                //不睡眠 会出现重复数据现象,原因 random 伪随机 速度快 数据重复
                //Thread.Sleep(10);
            }

        }

        /// <summary>
        /// 计算附加列的随机串
        /// </summary>
        private void setAdditionalColumn()
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
                        //int index = 0;
                        List<int> lists = new List<int>();
                         for (int AdditionalNum = 0; AdditionalNum < AdditionalColumnCount; AdditionalNum++)
                        {

                         Random ran = new Random(GetRandomSeed());
                         int index = ran.Next(1, ColumnTwoRows);
                        //index++;
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
        /// 列二附加列 +1
        /// </summary>
        private void setAdditionalColumn2()
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
                    //int index = 0;
                    List<int> lists = new List<int>();
                    for (int AdditionalNum = 0; AdditionalNum < AdditionalColumnCount; AdditionalNum++)
                    {

                        Random ran = new Random(GetRandomSeed());
                        int index = ran.Next(1, ColumnTwoRows);
                        //index++;
                        if (!lists.Contains(index))
                        {
                            lists.Add(index);
                            int biliCount = 1;
                            for (int x = ColumnOneRows - 1; x >= 0; x--)
                            {
                                switch (AdditionalNum + 3)
                                {
                                    case 3:
                                        int two=(int.Parse(randomTemps[i - x].ColumnTwo.ToString()) < ColumnTwoMax?int.Parse(randomTemps[i - x].ColumnTwo.ToString()):0 )+ 1;
                                        randomTemps[i - x].ColumnThree = two.ToString();

                                        break;
                                    case 4:

                                        randomTemps[i - x].ColumnFour = ((int.Parse(randomTemps[i - x].ColumnThree.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnThree.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 5:
                                        randomTemps[i - x].ColumnFive = ((int.Parse(randomTemps[i - x].ColumnFour.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnFour.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 6:
                                        randomTemps[i - x].ColumnSix = ((int.Parse(randomTemps[i - x].ColumnFive.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnFive.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 7:
                                        randomTemps[i - x].ColumnSeven = ((int.Parse(randomTemps[i - x].ColumnSix.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnSix.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 8:
                                        randomTemps[i - x].ColumnEight = ((int.Parse(randomTemps[i - x].ColumnSeven.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnSeven.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 9:
                                        randomTemps[i - x].ColumnNine = ((int.Parse(randomTemps[i - x].ColumnEight.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnEight.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 10:
                                        randomTemps[i - x].ColumnTen = ((int.Parse(randomTemps[i - x].ColumnNine.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnNine.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 11:
                                        randomTemps[i - x].ColumnEleven = ((int.Parse(randomTemps[i - x].ColumnTen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnTen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 12:
                                        randomTemps[i - x].ColumnTwelve = ((int.Parse(randomTemps[i - x].ColumnEleven.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnEleven.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 13:
                                        randomTemps[i - x].ColumnThirteen = ((int.Parse(randomTemps[i - x].ColumnTwelve.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnTwelve.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 14:
                                        randomTemps[i - x].ColumnFourteen = ((int.Parse(randomTemps[i - x].ColumnThirteen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnThirteen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 15:
                                        randomTemps[i - x].ColumnFifteen = ((int.Parse(randomTemps[i - x].ColumnFourteen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnFourteen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 16:
                                        randomTemps[i - x].ColumnSixteen = ((int.Parse(randomTemps[i - x].ColumnFifteen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnFifteen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 17:
                                        randomTemps[i - x].ColumnSeventeen = ((int.Parse(randomTemps[i - x].ColumnSixteen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnSixteen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 18:
                                        randomTemps[i - x].ColumnEighteen = ((int.Parse(randomTemps[i - x].ColumnSeventeen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnSeventeen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 19:
                                        randomTemps[i - x].ColumnNineteen = ((int.Parse(randomTemps[i - x].ColumnEighteen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnEighteen.ToString()) : 0) + 1).ToString();
                                        break;
                                    case 20:
                                        randomTemps[i - x].ColumnTwenty = ((int.Parse(randomTemps[i - x].ColumnNineteen.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnNineteen.ToString()) : 0) + 1).ToString();
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
        private void setColumnTwoNew()
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
        /// 列二比例 一一对应，行随机位置
        /// </summary>
        private void setColumnTwoBiliNew()
        {

            int bili = ColumnOneRows / ColumnTwoRows;
            if (bili > 1)
            {
                List<RandomTemp> temps = new List<RandomTemp>();
                
                string[] CTArr = new string[ColumnTwoRows];
                int CTArrIndex = 0;
                for (int i = 0; i < randomTemps.Count; i++)
                {

                    temps.Add(randomTemps[i]);
                    //List<int> lists = new List<int>();
                    //for (int x = 0; x < AdditionalNum + 1; x++)
                    //{
                    //    lists.Add(temps)
                    //}

                    if (i % ColumnOneRows == 0 && i!=0)
                    {
                        //处理
                        
                         List<int> lists = new List<int>();
                         for (int biliNum = bili; biliNum > 0; biliNum--)
                         {
                             Random ran = new Random(GetRandomSeed());
                             int index = ran.Next(1, ColumnTwoRows);
                             if (biliNum == bili)
                             {
                                 index = 0;
                                 lists.Add(index);
                                 for (int x = ColumnTwoRows * bili; x > ColumnTwoRows * bili-ColumnTwoRows; x--)
                                 {
                                     randomTemps[i - x].ColumnTwo = CTArr[index].ToString();
                                     index++;
                                 }
                             }
                             else
                             {
                                 
                                 if (!lists.Contains(index))
                                 {
                                     lists.Add(index);
                                     for (int x = ColumnTwoRows * biliNum; x > ColumnTwoRows * biliNum - ColumnTwoRows; x--)
                                     {
                                         randomTemps[i - x].ColumnTwo = CTArr[index].ToString();
                                         if (index == ColumnTwoRows-1)
                                         {
                                             index = 0;
                                         }
                                         else
                                         {
                                             index++;
                                         }
                                         
                                     }
                                 }
                             }
                             
                         }
                         //temps.Clear();
                         CTArrIndex = 0;
                    }
                    else
                    {
                        if (i % bili == 0)
                        {
                            CTArr[CTArrIndex] = temps[i].ColumnTwo.ToString();
                            CTArrIndex++;
                        }
                        
                    }
                }
            }
           
        }
        /// <summary>
        /// 列一顺序 123456 固定递增 、or 随机
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private List<int> GETColumnOne(int count, int min, int max)
        {
            Random ran = new Random(GetRandomSeed());
            List<int> lists = new List<int>();

            while (lists.Count < count)
            {
                int x = ran.Next(min, max);
                if (!lists.Contains(x))
                {
                    lists.Add(x);
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取选号器
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private List<string> GETColumnSelector(List<int> ctos, int count)
        {
            List<string> lists = new List<string>();
            foreach (int item in ctos)
            {
                try
                {
                    lists.Add(Selector[item - 1]);
                }
                catch (Exception)
                {

                    lists.Add(" ");
                }
                
            }
            return lists;
        }
        /// <summary>
        /// 随机数选号器
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private List<int> GETColumnNewSelector(int count, int min, int max)
        {
            Random ran = new Random(GetRandomSeed());
            List<int> lists = new List<int>();

            while (lists.Count < count)
            {
                int x = ran.Next(min, max);
                lists.Add(x);
                //if (!lists.Contains(x))
                //{
                //   
                //}
            }
            return lists;
        }

        /// <summary>
        /// 列一顺序 123456 固定递增 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private List<int> GETColumnNewOne(int count, int min, int max)
        {
            List<int> lists = new List<int>();
            int x = 0;
            while (lists.Count < count)
            {
                x++;
                if (!lists.Contains(x))
                {
                    lists.Add(x);
                }
            }
            return lists;
        }
        private List<int> GETColumnTwo2(int count, int min, int max)
        {
            Random ran = new Random(GetRandomSeed());
            int first = ran.Next(1, count);
            List<int> lists = new List<int>();
            int x = 0;
            while (lists.Count < count)
            {
                first++;
                if (first > ColumnTwoMax)
                {
                    first = 0;
                    continue;
                }
                if (!lists.Contains(first))
                {
                    lists.Add(first);
                }
                //x++;


            }
            return lists;
        }

        /// <summary>
        /// 生成X个不重复的随机数
        /// </summary>
        /// <param name="count">个数</param>
        /// <param name="min">范围</param>
        /// <param name="max">范围</param>
        /// <returns></returns>
        private List<int> GETRandom(int count, int min, int max)
        {
            Random ran = new Random(GetRandomSeed());
            List<int> lists = new List<int>();
            //int x = 0;
            while (lists.Count < count)
            {
                int x = ran.Next(min, max);
                if (!lists.Contains(x))
                {
                    lists.Add(x);
                }
            }
            return lists;
            //Random ran = new Random(GetRandomSeed());
            //List<int> lists = new List<int>();
            //int zeroCount = 0;
            //int nonzeroCount = 0;
            //for (int i = 0; i < ColumnTwoRows; i++)//多少行
            //{
            //    int isZero = ran.Next(0,2);
            //    if (isZero > 0 && nonzeroCount < (ColumnTwoRows - CTZeroCount) || CTZeroCount==0)
            //    {
            //        nonzeroCount++;
            //        for(int j=0;j < ColumnTwoCount;j++)//一行多少个
            //        {
            //            int x = ran.Next(min, max);
            //            if (!lists.Contains(x))
            //            {
            //                lists.Add(x);
            //            }
            //            else
            //            {
            //                j--;
            //            }
            //        }

            //    }
            //    else if (isZero == 0 && zeroCount < CTZeroCount)
            //    {
            //        zeroCount++;
            //        for (int j = 0; j < ColumnTwoCount; j++)//一行多少个
            //        {
            //            lists.Add(0);
            //        }

            //    }
            //    else
            //    {
            //        i--;
            //    }
               
            //}
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
        /// 生成一条
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
            if ((e.ColumnIndex == 0 && e.RowIndex != -1) || (e.ColumnIndex == (5) && e.RowIndex != -1) || (e.ColumnIndex == (6) && e.RowIndex != -1) || (e.ColumnIndex == (4) && e.RowIndex != -1))
            {
                cellPainting(dgv_Random, e);
            }
            //for (int AdditionalNum = 0; AdditionalNum < AdditionalColumnCount; AdditionalNum++)
            //{
            //    if ((e.ColumnIndex == (AdditionalColumnCount + 3) && e.RowIndex != -1))
            //    {
            //        cellPainting(dgv_Random, e);
            //    }
            //}

            //switch (AdditionalColumnCount)
            //{
            //    case 1:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 2:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 3:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 4:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 5:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 6:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 7:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 8:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 9:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 10:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 11:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 12:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 13:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1) || (e.ColumnIndex == 15 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 14:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1) || (e.ColumnIndex == 15 && e.RowIndex != -1) || (e.ColumnIndex == 16 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 15:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1) || (e.ColumnIndex == 15 && e.RowIndex != -1) || (e.ColumnIndex == 16 && e.RowIndex != -1) || (e.ColumnIndex == 17 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 16:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1) || (e.ColumnIndex == 15 && e.RowIndex != -1) || (e.ColumnIndex == 16 && e.RowIndex != -1) || (e.ColumnIndex == 17 && e.RowIndex != -1) || (e.ColumnIndex == 18 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 17:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1) || (e.ColumnIndex == 15 && e.RowIndex != -1) || (e.ColumnIndex == 16 && e.RowIndex != -1) || (e.ColumnIndex == 17 && e.RowIndex != -1) || (e.ColumnIndex == 18 && e.RowIndex != -1) || (e.ColumnIndex == 19 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //    case 18:
            //        if ((e.ColumnIndex == 3 && e.RowIndex != -1) || (e.ColumnIndex == 4 && e.RowIndex != -1) || (e.ColumnIndex == 5 && e.RowIndex != -1) || (e.ColumnIndex == 6 && e.RowIndex != -1) || (e.ColumnIndex == 7 && e.RowIndex != -1) || (e.ColumnIndex == 8 && e.RowIndex != -1) || (e.ColumnIndex == 9 && e.RowIndex != -1) || (e.ColumnIndex == 10 && e.RowIndex != -1) || (e.ColumnIndex == 11 && e.RowIndex != -1) || (e.ColumnIndex == 12 && e.RowIndex != -1) || (e.ColumnIndex == 13 && e.RowIndex != -1) || (e.ColumnIndex == 14 && e.RowIndex != -1) || (e.ColumnIndex == 15 && e.RowIndex != -1) || (e.ColumnIndex == 16 && e.RowIndex != -1) || (e.ColumnIndex == 17 && e.RowIndex != -1) || (e.ColumnIndex == 18 && e.RowIndex != -1) || (e.ColumnIndex == 19 && e.RowIndex != -1) || (e.ColumnIndex == 20 && e.RowIndex != -1))
            //        {
            //            cellPainting(dgv_Random, e);
            //        }
            //        break;
            //}
            
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
            try
            {
                if (MessageBox.Show(string.Format(" 列一：{0}\n 列二：{1}\n是否确定筛选？", txt_SXRow1.Text, txt_SXRow2.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    status = false;
                    saveCount = 0;
                    deleteCount = 0;
                    signCount = 0;
                    //筛选
                    string columnOne = txt_SXRow1.Text;
                    string sxRow2 = txt_SXRow2.Text;

                    List<int> deleno = new List<int>();
                    //先删除数据，再重新绘制，再标记蓝色 X 次数
                    //有问题，删除了randomtemp 这边没有更新 导致 有相邻两个方案需要删除时 无法删除第二个
                    //解决方案：使用 deleno list集合保存no 然后一次性删除
                    for (int i = 0; i < randomTemps.Count; i++)
                    {
                        RandomTemp randomtemp = randomTemps[i];

                        string[] strarr1 = randomtemp.ColumnOne.Split(',');
                        switch (cb_Column1.SelectedIndex + 1)
                        {
                            case 1:
                                strarr1 = randomtemp.ColumnNewOne.Split(',');//列一固定递增
                                break;
                            case 2:
                                strarr1 = randomtemp.ColumnOne.Split(',');
                                break;
                        }
                        for (int x = 0; x < strarr1.Length; x++)
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
                                            for (int j = 0; j < ColumnTwoCount * (AdditionalColumnCount + 1); j++)
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
                                        for (int j = 0; j < ColumnTwoCount * (AdditionalColumnCount + 1); j++)
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
                    //删除达到15次的
                    //int delFlag;
                    bool isNewBiaoji = false;
                    for (int i = 0; i < randomTemps.Count; i++)
                    {
                        RandomTemp randomtemp = randomTemps[i];
                        string[] strarr1 = randomtemp.ColumnOne.Split(',');
                        switch (cb_Column1.SelectedIndex + 1)
                        {
                            case 1:
                                strarr1 = randomtemp.ColumnNewOne.Split(',');//列一固定递增
                                break;
                            case 2:
                                strarr1 = randomtemp.ColumnOne.Split(',');
                                break;
                        }
                        for (int x = 0; x < strarr1.Length; x++)
                        {
                            if (strarr1[x].Equals(columnOne))
                            {
                                if (randomTemps[i].SingleCount1 < SingleMax)
                                {

                                    //更新方案被筛选次数
                                    Project[randomtemp.ProjectNo]++;//次数++
                                    if (checkProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]))
                                    {
                                        randomTemps[i].SingleCount1++;//删除的同时增加
                                        deleno.Add(randomtemp.ProjectNo);
                                    }
                                    else
                                    {
                                        //标记黄色
                                        signCount++;
                                        isNewBiaoji = true;

                                        //加X标记
                                        randomTemps[i].SingleCount1++;
                                    }

                                }
                                else
                                {

                                }

                                //updateProjectNo(randomtemp.ProjectNo, Project[randomtemp.ProjectNo]);
                            }

                        }
                    }
                    for (int i = 0; i < deleno.Count; i++)
                    {
                        deleteProject(deleno[i]);
                    }
                    deleno.Clear();

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
                            switch (cb_Column1.SelectedIndex + 1)
                            {
                                case 1:
                                    strarr1 = randomtemp.ColumnNewOne.Split(',');//列一固定递增
                                    break;
                                case 2:
                                    strarr1 = randomtemp.ColumnOne.Split(',');
                                    break;
                            }
                            for (int x = 0; x < strarr1.Length; x++)
                            {
                                if (strarr1[x].Equals(columnOne))
                                {

                                    if (ranNow[i].SingleCount1 < SingleMax || isNewBiaoji)
                                    {
                                        //标记黄色
                                        //signCount++;
                                        switch (cb_Column1.SelectedIndex + 1)
                                        {
                                            case 1:
                                                dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Yellow;
                                                break;
                                            case 2:
                                                dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Style.BackColor = Color.Yellow;
                                                break;
                                        }

                                        //加X标记
                                        dgv_Random.Rows[randomtemp.Rowindex1].Cells[4].Value = "";
                                        for (int j = 0; j < ranNow[i].SingleCount1; j++)
                                        {
                                            dgv_Random.Rows[randomtemp.Rowindex1].Cells[4].Value += "X ";
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

                    MessageBox.Show(string.Format("保存 {0} 个大于{1}次筛选的方案 \n删除 {2} 个方案 \n标记 {3} 个方案", saveCount, ProjectMax, deleteCount, signCount), "筛选结果", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception)
            {
                 MessageBox.Show("有错误操作！");
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
                    if (Count >= ProjectMax)//满15次应该保留
                        dgv_Random.Rows[randomtemp.Rowindex1].Cells[5].Value = Count + "次";
                    else
                        dgv_Random.Rows[randomtemp.Rowindex1].Cells[5].Value = Count + "次";
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
                   
                    for (int j= 0; j < 6;j++)
                    {
                        dgv.Rows[index].Cells[j].Value = "";
                        dgv.Rows[index].Cells[j].Style.BackColor = Color.LightSkyBlue;
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
            dgv.Rows[index].Cells[4].Value = temp.ColumnTwo.ToString(); //原来的列二 打的号 列三
            dgv.Rows[index].Cells[1].Value = temp.ColumnNewOne.ToString();// 期号 列一
            dgv.Rows[index].Cells[2].Value = temp.ColumnOne.ToString();//选号器 列二
            if (temp.ColumnZhq != null)
            {
                dgv.Rows[index].Cells[3].Value = temp.ColumnZhq.ToString();//选号器2
            }
            else
            {
                dgv.Rows[index].Cells[3].Value = "0";//选号器2
            }

            //try
            //{
            //    dgv.Rows[index].Cells[5].Value = temp.ColumnNewFour.ToString();//选号器 列四
            //    int zhqIndex = int.Parse(temp.ColumnNewFour.ToString());
            //    if (zhqIndex <= zhqDic.Count)
            //    {
            //        dgv.Rows[index].Cells[6].Value = zhqDic[int.Parse(temp.ColumnNewFour.ToString())];//选号器 列5
            //    }
            //    else
            //    {
            //        dgv.Rows[index].Cells[6].Value = "检测转换器";//选号器 列5 dgv.Rows[index].Cells[6].Value = zhqDic[int.Parse(temp.ColumnNewFour.ToString())];//选号器 列5
            //    }


            //}
            //catch (Exception)
            //{
            //    dgv.Rows[index].Cells[5].Value = "!!";//选号器 列四
            //}
           
            //if (Project[temp.ProjectNo] >= ProjectMax)//满15次应该保留
            //    dgv.Rows[index].Cells[5 ].Value = Project[temp.ProjectNo] + "次";
            //else
            //    dgv.Rows[index].Cells[5 ].Value = Project[temp.ProjectNo] + "次";

            //dgv.Rows[index].Cells[4 ].Value = "";
            for (int x = 0; x < temp.SingleCount1; x++)
            {
                dgv.Rows[index].Cells[5 ].Value += "X ";
            }
            temp.Rowindex1 = index;

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(this.dgv_Save);

            ////导出到Excel
            //if (ExportDataGridview(dgv_Random, true))
            //    MessageBox.Show("导出成功，请记得保存!");
            //else
            //    MessageBox.Show("导出未成功，请检查是否有错!");    
            //printDialog1.ShowDialog();
            //printPreviewDialog1.Document = this.printDocument1;
            //printPreviewDialog1.ShowDialog();
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
            if (int.Parse(tb_AdditionalColumnCount.Text) >18)
            {
                MessageBox.Show("附加列个数不可超过18，错误设置！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            //写入<add>元素的Value
            config.AppSettings.Settings["ColumnOneCount"].Value = tb_COCount.Text;
            config.AppSettings.Settings["ColumnOneMin"].Value = tb_COMin.Text;
            config.AppSettings.Settings["ColumnOneMax"].Value = tb_COMax.Text;

            config.AppSettings.Settings["ColumnTwoCount"].Value = tb_CTCount.Text;
            config.AppSettings.Settings["ColumnTwoMin"].Value = tb_CTMin.Text;
            config.AppSettings.Settings["ColumnTwoMax"].Value = tb_CTMax.Text;

            config.AppSettings.Settings["ProjectCount"].Value = tb_ProjectCount.Text;
            config.AppSettings.Settings["ProjectMax"].Value = tb_ProjectMax.Text;
            config.AppSettings.Settings["ProjectRows"].Value = tb_AdditionalColumnCount.Text;
            config.AppSettings.Settings["SingleMax"].Value = tb_SingleMax.Text;
            config.AppSettings.Settings["ColumnOneRows"].Value = tb_ColumnOneRows.Text;
            config.AppSettings.Settings["ColumnTwoRows"].Value = tb_ColumnTwoRows.Text;
            config.AppSettings.Settings["AdditionalColumnCount"].Value = tb_AdditionalColumnCount.Text;

            config.AppSettings.Settings["SelectorMax"].Value = tb_SelectorMax.Text;
            config.AppSettings.Settings["SelectorMin"].Value = tb_SelectorMin.Text;
            config.AppSettings.Settings["SelectorCount"].Value = tb_SelectorCount.Text;

            config.AppSettings.Settings["ZHQCount"].Value = tb_ZHQCount.Text;
            config.AppSettings.Settings["ZHQMin"].Value = tb_ZHQMin.Text;
            config.AppSettings.Settings["ZHQMax"].Value = tb_ZHQMax.Text;
            config.AppSettings.Settings["ZHQRowCount"].Value = tb_ZHQRowCount.Text;
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
                    initDGV(dgv_Save);
                   
                    updateDGV(dgv_Save, SaverandomTemps);
                    break;
                case 2://总保存
                     initDGV(dgv_totalSave);

                     updateDGV(dgv_totalSave, TotalSaveRandomTemps);
                    break;
                case 3:
                    tb_COCount.Text = ColumnOneCount.ToString();
                    tb_COMin.Text = ColumnOneMin.ToString();
                    tb_COMax.Text = ColumnOneMax.ToString();
                    tb_CTCount.Text = ColumnTwoCount.ToString();
                    tb_CTMin.Text = ColumnTwoMin.ToString();
                    tb_CTMax.Text = ColumnTwoMax.ToString();
                    tb_ProjectCount.Text = ProjectCount.ToString();
                    tb_ProjectMax.Text = ProjectMax.ToString();
                    //tb_AdditionalColumnCount.Text = ProjectRows.ToString();
                    tb_SingleMax.Text = SingleMax.ToString();
                    tb_ColumnOneRows.Text = ColumnOneRows.ToString();
                    tb_ColumnTwoRows.Text = ColumnTwoRows.ToString();
                    tb_AdditionalColumnCount.Text = AdditionalColumnCount.ToString();
                    tb_SelectorMin.Text = SelectorMin.ToString();
                    tb_SelectorMax.Text = SelectorMax.ToString();
                    tb_SelectorCount.Text = SelectorCount.ToString();
                     tb_ZHQCount.Text = ZHQCount.ToString();
                    tb_ZHQMin.Text = ZHQMin.ToString();
                    tb_ZHQMax.Text = ZHQMax.ToString();
                    tb_ZHQRowCount.Text = ZHQRowCount.ToString();
                    //tb_AddColumnTwoCount.Text = AddColumnTwoCount.ToString();
                    //tb_CTZeroCount.Text = CTZeroCount.ToString();
                    break;
                case 4:
                    txt_Help_ColumnCount.Text = Help_ColumnCount.ToString();
                    txt_Help_ColumnMin.Text = Help_ColumnMin.ToString();
                    txt_Help_ColumnMax.Text = Help_ColumnMax.ToString();
                    txt_Help_ColumnRowCount.Text = Help_ColumnRowCount.ToString();
                    txt_Help_AdditionalColumnCount.Text = Help_AdditionalColumnCount.ToString();

                    txt_Help2_ColumnCount.Text = Help2_ColumnCount.ToString();
                    txt_Help2_ColumnMin.Text = Help2_ColumnMin.ToString();
                    txt_Help2_ColumnMax.Text = Help2_ColumnMax.ToString();
                    txt_Help_automaticCount.Text = Help_automaticCount.ToString();

                    tb_ColumnFourColumns.Text = ColumnFourColumns.ToString();
                    break;

            } 
        }

        private void btn_Default_Click(object sender, EventArgs e)
        {
            //默认
            tb_COCount.Text = "1";
            tb_COMin.Text ="1";
            tb_COMax.Text = "36";
            tb_CTCount.Text = "5";
            tb_CTMin.Text = "1";
            tb_CTMax.Text = "20";
            tb_ProjectCount.Text = "500";
            tb_ProjectMax.Text = "15";
            //tb_AdditionalColumnCount.Text = "3";
            tb_SingleMax.Text = "15";
            tb_ColumnOneRows.Text = "3";
            tb_ColumnTwoRows.Text = "3";
            tb_AdditionalColumnCount.Text = "0";
            //tb_CTZeroCount.Text = "20";
            //tb_AddColumnTwoCount.Text = "0";

            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //写入<add>元素的Value
            config.AppSettings.Settings["ColumnOneCount"].Value = tb_COCount.Text;
            config.AppSettings.Settings["ColumnOneMin"].Value = tb_COMin.Text;
            config.AppSettings.Settings["ColumnOneMax"].Value = tb_COMax.Text;

            config.AppSettings.Settings["ColumnTwoCount"].Value = tb_CTCount.Text;
            config.AppSettings.Settings["ColumnTwoMin"].Value = tb_CTMin.Text;
            config.AppSettings.Settings["ColumnTwoMax"].Value = tb_CTMax.Text;

            config.AppSettings.Settings["ProjectCount"].Value = tb_ProjectCount.Text;
            config.AppSettings.Settings["ProjectMax"].Value = tb_ProjectMax.Text;
            //config.AppSettings.Settings["ProjectRows"].Value = tb_AdditionalColumnCount.Text;
            config.AppSettings.Settings["SingleMax"].Value = tb_SingleMax.Text;
            config.AppSettings.Settings["ColumnOneRows"].Value = tb_ColumnOneRows.Text;
            config.AppSettings.Settings["ColumnTwoRows"].Value = tb_ColumnTwoRows.Text;
            config.AppSettings.Settings["AdditionalColumnCount"].Value = tb_AdditionalColumnCount.Text;
            config.AppSettings.Settings["SelectorMin"].Value = tb_SelectorMin.Text;
            config.AppSettings.Settings["SelectorMax"].Value = tb_SelectorMax.Text;
            config.AppSettings.Settings["SelectorCount"].Value = tb_SelectorCount.Text;
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
                        Font drawFont = new Font("Arial", 10); //字体设置
                        e.Graphics.DrawString(dgv_Random.Rows[i].Cells[j].Value.ToString(), drawFont, Brushes.Blue, x, y);
                        x = x + 60; //宽度设置
                    }
                    catch (Exception) { } //当遇到空值是发生。
                }
                x = 0;
                y += 40; //行高
            } 
        }

        private void btn_loadSave_Click(object sender, EventArgs e)
        {
            LoadSaveFromQuesstion load=new LoadSaveFromQuesstion();
            DialogResult res=load.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (SaverandomTemps.Count > 0)
                {
                    if (MessageBox.Show("将删除当前随机生成的方案，载入暂时保存的方案继续筛选，并删除暂保存，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        randomTemps.Clear();
                        //randomTemps = SaverandomTemps;
                        foreach (RandomTemp item in SaverandomTemps)
                        {
                            //不需要重置
                            //item.SingleCount1 = 0;
                            //Project[item.ProjectNo] = 0;
                            randomTemps.Add(item);
                        }
                        SaverandomTemps.Clear();

                        nMax = randomTemps.Count;
                        pageCount = (nMax / pageSize);    //计算出总页数
                        if ((nMax % pageSize) > 0) pageCount++;
                        pageCurrent = 1;    //当前页数从1开始
                        nCurrent = 0;       //当前记录数从0开始
                        LoadData();

                        //updateDGV(dgv_Random, randomTemps);
                    }
                }
                else
                {
                    MessageBox.Show("当前已保存数据为空，不需要载入！");
                }
            }
            else if (res == DialogResult.Yes)
            {
                if (TotalSaveRandomTemps.Count > 0)
                {
                    if (MessageBox.Show("将删除当前随机生成的方案，载入总保存的方案继续筛选，并删除总保存，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        randomTemps.Clear();
                        //randomTemps = SaverandomTemps;
                        foreach (RandomTemp item in TotalSaveRandomTemps)
                        {
                            //不需要重置
                            //item.SingleCount1 = 0;
                            //Project[item.ProjectNo] = 0;
                            randomTemps.Add(item);
                        }
                        TotalSaveRandomTemps.Clear();

                        nMax = randomTemps.Count;
                        pageCount = (nMax / pageSize);    //计算出总页数
                        if ((nMax % pageSize) > 0) pageCount++;
                        pageCurrent = 1;    //当前页数从1开始
                        nCurrent = 0;       //当前记录数从0开始
                        LoadData();

                        //updateDGV(dgv_Random, randomTemps);
                    }
                }
                else
                {
                    MessageBox.Show("当前已保存数据为空，不需要载入！");
                }
            }
                
        }

        private void btn_clearSave_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("将清除当前已保存方案方案，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SaverandomTemps.Clear();
                dgv_Save.Rows.Clear();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {//刷新
            updateDGV(dgv_Save, SaverandomTemps);
        }

        private void dgv_Save_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1,4列 最后 相同单元格进行合并
            if ((e.ColumnIndex == 0 && e.RowIndex != -1) || (e.ColumnIndex == (5) && e.RowIndex != -1) || (e.ColumnIndex == 3 && e.RowIndex != -1))
            {
                cellPainting(dgv_Save, e);
            }
            //for (int AdditionalNum = 0; AdditionalNum < AdditionalColumnCount; AdditionalNum++)
            //{
            //    if ((e.ColumnIndex == (AdditionalColumnCount + 3) && e.RowIndex != -1))
            //    {
            //        cellPainting(dgv_Save, e);
            //    }
            //}

           
            dgv_Save.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
        }
        private void qcdel(string columnOne)
        {
            //全场删除
            List<int> lists = new List<int>();
            try
            {

                //string columnOne = txt_SXRow1.Text;
                //string sxRow2 = txt_SXRow2.Text;
                string[] strarr0 = columnOne.Split(',');//
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    RandomTemp randomtemp = randomTemps[i];

                    string[] strarr1 = randomtemp.ColumnOne.Split(',');//选号器
                    for (int x = 0; x < strarr1.Length; x++)//对比
                    {
                        for (int j = 0; j < strarr0.Length; j++)
                        {
                            if (strarr1[x].Equals(strarr0[j]))
                            {//删除
                                dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Value = "";
                                randomTemps[i].ColumnNewOne = "-1";
                            }

                        }

                    }

                }




            }
            catch (Exception)
            {
                MessageBox.Show("有错误操作！");
            }

        }
       
        private void btn_ColumnOneRan_Click(object sender, EventArgs e)
        {
            //一键生成
            string sxRow2 = txt_SXRow2.Text;
            help_randomTemps.Clear();
            help2_randomTemps.Clear();
            helpSaveRandomTemps.Clear();
            int project_j = 0;
            for (int j = 0; j < Help_automaticCount; j++)
            {
                project_j++;
                RandomCreate();
                //qcdel(help2_randomTemps[0].ColumnNewOne);

                for (int i = 0; i < Help_ColumnRowCount; i++)
                {
                    dgdel(help_randomTemps[i].ColumnNewOne);
                }
                int x = 0;
                int count = 0;
                int biliCtrl = 0;
                int bili = int.Parse(txt_SXRow2.Text.ToString()) * Help_ColumnRowCount / (Help_ColumnRowCount * Help_ColumnCount);
                for (int i = 0; i < randomTemps.Count && x < Help_ColumnRowCount * Help_ColumnCount; i++)
                {

                    if (!(randomTemps[i].ColumnNewOne.Equals("-1") || randomTemps[i].ColumnNewOne.Equals("-2")))
                    {
                        biliCtrl++;
                        if (biliCtrl == bili)
                        {
                            biliCtrl = 0;
                            x++;
                        }
                        count++;
                    }
                }
                int min=((Help_ColumnRowCount ) * int.Parse(txt_SXRow2.Text.ToString()));
                if (count != min)
                    isExport = false;
                if (isExport)
                {
                    // StringBuilder strBuilder = new StringBuilder();

                    int projectNo = 0;
                    x = 0;
                    biliCtrl = 0;
                    for (int i = 0; i < randomTemps.Count && x < Help_ColumnRowCount * Help_ColumnCount; i++)
                    {

                        if (!(randomTemps[i].ColumnNewOne.Equals("-1") || randomTemps[i].ColumnNewOne.Equals("-2")))
                        {
                            // strBuilder = new StringBuilder();
                            if (i % Help_ColumnRowCount * Help_ColumnCount * bili == 0)
                            {
                                projectNo++;
                            }
                            //2018-5-4,新版本将辅助列以逗号拆分，生成两个方案
                            
                                RandomTemp temp = new RandomTemp();
                                temp.ProjectNo = project_j;

                                //temp.ColumnTwo = help3_randomTemps[x].ColumnNewFour.ToString() ;//辅助列
                                int zhqIndex = int.Parse(help3_randomTemps[x].ColumnNewFour.ToString());
                                if (zhqIndex <= zhqDic.Count)
                                {
                                    temp.ColumnTwo = zhqDic[zhqIndex] + " ";//辅助列
                                }
                                else
                                {
                                    // dgv.Rows[index].Cells[6].Value = "检测转换器";//选号器 列5 dgv.Rows[index].Cells[6].Value = zhqDic[int.Parse(temp.ColumnNewFour.ToString())];//选号器 列5
                                    temp.ColumnTwo = " 0";
                                }
                                temp.ColumnOne = randomTemps[i].ColumnNewOne.ToString();
                                helpSaveRandomTemps.Add(temp);
                            
                           

                            biliCtrl++;
                            if (biliCtrl == bili)
                            {
                                biliCtrl = 0;
                                x++;
                            }


                        }

                    }
                    //2018-5-4,新版本将辅助列以逗号拆分，生成两个方案 新增
                    projectNo = 0;
                    x = 0;
                    biliCtrl = 0;
                    //for (int i = 0; i < randomTemps.Count && x < Help_ColumnRowCount; i++)
                    //{

                    //    if (!(randomTemps[i].ColumnNewOne.Equals("-1") || randomTemps[i].ColumnNewOne.Equals("-2")))
                    //    {
                    //        // strBuilder = new StringBuilder();
                    //        if (i % Help_ColumnRowCount * bili == 0)
                    //        {
                    //            projectNo++;
                    //        }
                    //        //2018-5-4,新版本将辅助列以逗号拆分，生成两个方案

                    //        RandomTemp temp = new RandomTemp();
                    //        temp.ProjectNo = project_j + 1;
                    //        temp.ColumnTwo = help_randomTemps[x].ColumnNewOne.ToString().Split(',')[2] + "," + help_randomTemps[x].ColumnNewOne.ToString().Split(',')[3];//辅助列
                    //        temp.ColumnOne = randomTemps[i].ColumnNewOne.ToString();
                    //        helpSaveRandomTemps.Add(temp);



                    //        biliCtrl++;
                    //        if (biliCtrl == bili)
                    //        {
                    //            biliCtrl = 0;
                    //            x++;
                    //        }


                    //    }

                    //}
                    //MessageBox.Show("成功\n");
                    project_j++;

                }
                else
                {
                    project_j--;
                    j--;
                }
                    
               
            }
            string time = DateTime.Now.ToString("yyyy-MM-dd hh：mm：ss");
            Export_Text2(time);
            //列一随机
            //if (status)
            //{
            //    int index = 0;
            //    for (int x = 0; x < ProjectCount; x++)//方案
            //    {

            //        List<int> ColumnOne = new List<int>();

            //        //生成一个方案
            //        ColumnOne = GETColumnOne(ColumnOneCount * ColumnOneRows, ColumnOneMin, ColumnOneMax + 1);

            //        for (int j = 1; j <= ColumnOneRows; j++)//单条
            //        {
            //            RandomTemp randomtemp = randomTemps[index];
            //            List<int> ColumnOne1 = new List<int>();
            //            for (int i = 1; i <= ColumnOneCount; i++)
            //            {
            //                ColumnOne1.Add(ColumnOne[ColumnOneCount * (j - 1) + i - 1]);
            //            }
            //            string CO = "";
            //            for (int i = 0; i < ColumnOne1.Count; i++)
            //            {
            //                if (i != ColumnOne1.Count - 1)
            //                    CO += ColumnOne1[i] + ",";
            //                else
            //                    CO += ColumnOne1[i] + "";
            //            }
            //            randomTemps[index].ColumnNewOne = CO;
            //            //if (randomtemp.Rowindex1 != -1)
            //            //    dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Value = CO;

            //            // updateProject(randomtemp.Rowindex1, CO);
            //            index++;
            //        }
            //    }
            //    pageCurrent = 1;    //当前页数从1开始
            //    nCurrent = 0;       //当前记录数从0开始

            //    LoadData();
            //}
            //else
            //{
            //    MessageBox.Show("无法执行该操作，该操作只有在初始化方案时执行！");
            //}
            
        }
        private void Export_Text2(string name)
        {
            string file_path = @"导出文件\彩牛通-" + name + ".txt";
            FileStream fileStream = new FileStream(file_path, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

            StringBuilder strBuilder = new StringBuilder();

            try
            {

                for (int i = 0; i < helpSaveRandomTemps.Count; i++)
                {
                    
                    strBuilder = new StringBuilder();
                    strBuilder.Append(helpSaveRandomTemps[i].ProjectNo + " ");
                    strBuilder.Append(helpSaveRandomTemps[i].ColumnOne.ToString() + " ");

                    strBuilder.Append(helpSaveRandomTemps[i].ColumnTwo.ToString() + " ");
                    
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

        private void btn_ColumnTwoRan_Click(object sender, EventArgs e)
        {
            if (status)
            {
                int index = 0;
                for (int x = 0; x < ProjectCount; x++)//方案
                {
                    int bili = ColumnOneRows / ColumnTwoRows;
                    int columnTworow = 1;
                    int columnJ = 1;
                    string CT = "";
                    

                    List<int> ColumnTwo = new List<int>();
                    ColumnTwo = GETRandom(ColumnTwoCount * ColumnTwoRows, ColumnTwoMin, ColumnTwoMax + 1);
                    for (int j = 1; j <= ColumnOneRows; j++)//单条
                    {
                        RandomTemp randomtemp = randomTemps[index];

                        if (columnTworow == 1 || (columnTworow - 1) % bili == 0 || bili == 1)
                        {
                            CT = "";
                            List<int> ColumnTwo1 = new List<int>();
                            for (int i = 1; i <= ColumnTwoCount; i++)
                            {
                                ColumnTwo1.Add(ColumnTwo[ColumnTwoCount * (columnJ - 1) + i - 1]);
                            }
                            for (int i = 0; i < ColumnTwo1.Count; i++)
                            {
                                if (i != ColumnTwo1.Count - 1)
                                    CT += ColumnTwo1[i] + ",";
                                else
                                    CT += ColumnTwo1[i] + "";
                            }
                            
                            columnJ++;
                        }
                       
                        columnTworow++;
                        

                        randomTemps[index].ColumnTwo = CT;
                        //if (randomtemp.Rowindex1!=-1)
                        //    dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Value = CT;

                        // updateProject(randomtemp.Rowindex1, CO);
                        index++;
                    }
                   
                }
                pageCurrent = 1;    //当前页数从1开始
                nCurrent = 0;       //当前记录数从0开始

                setAdditionalColumn();
                setColumnTwoNew();
                LoadData();
            }
            else
            {
                MessageBox.Show("无法执行该操作，该操作只有在初始化方案时执行！");
            }
           
      
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
            if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.A) )
            {
                btn_OK.Focus();
            }
            if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.L))
            {
                dgdel(txt_SXRow1.Text);
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
                            switch (cb_Column1.SelectedIndex + 1)
                            {
                                case 1:
                                    dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Yellow;
                                    break;
                                case 2:
                                    dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Style.BackColor = Color.Yellow;
                                    break;
                            }
                            //加X标记
                            //randomTemps[i].SingleCount1++;
                            dgv_Random.Rows[randomtemp.Rowindex1].Cells[4].Value = "";
                            for (int j = 0; j < ranNow[i].SingleCount1; j++)
                            {
                                dgv_Random.Rows[randomtemp.Rowindex1].Cells[4].Value += "X ";
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

        private void dgv_totalSave_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1,4列 最后 相同单元格进行合并
            if ((e.ColumnIndex == 0 && e.RowIndex != -1) || (e.ColumnIndex == (5) && e.RowIndex != -1) || (e.ColumnIndex == 3 && e.RowIndex != -1))
            {
                cellPainting(dgv_totalSave, e);
            }
            //for (int AdditionalNum = 0; AdditionalNum < AdditionalColumnCount; AdditionalNum++)
            //{
            //    if ((e.ColumnIndex == (AdditionalColumnCount + 3) && e.RowIndex != -1))
            //    {
            //        cellPainting(dgv_Save, e);
            //    }
            //}


            dgv_totalSave.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btn_totalsave_print_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(this.dgv_totalSave);
        }

        private void btn_totalsave_Statistics_Click(object sender, EventArgs e)
        {
            StatisticsForm statForm = new StatisticsForm(TotalSaveRandomTemps);
            statForm.Show();
        }

        private void btn_Selector_Click(object sender, EventArgs e)
        {
            SelectorSetting form = new SelectorSetting(int.Parse(tb_SelectorMax.Text.ToString()));
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                loadConfig();
            }
        }

        Boolean isExport = true;
        private void Export_Text(string name)
        {
            string file_path = @"导出文件\彩牛通-" + name + ".txt";
            FileStream fileStream = new FileStream(file_path, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

            StringBuilder strBuilder = new StringBuilder();

            try
            {
                int projectNo = 0;
                int x = 0;
                int biliCtrl = 0;
                int bili = int.Parse(txt_SXRow2.Text.ToString());
                for (int i = 0; i < randomTemps.Count; i++)
                {
                   
                    if (!randomTemps[i].ColumnNewOne.Equals("-1"))
                    {
                        strBuilder = new StringBuilder();
                        if (i % Help_ColumnRowCount * bili == 0)
                        {
                            projectNo++;
                        }
                        strBuilder.Append(1 + " ");
                        strBuilder.Append(randomTemps[i].ColumnNewOne.ToString() + " ");
                        //strBuilder.Append(help_randomTemps[x].ColumnNewOne.ToString() + " ");

                        int zhqIndex = int.Parse(help3_randomTemps[i].ColumnNewFour.ToString());
                        if (zhqIndex <= zhqDic.Count)
                        {
                            // dgv.Rows[index].Cells[6].Value = zhqDic[int.Parse(temp.ColumnNewFour.ToString())];//选号器 列5
                            strBuilder.Append(zhqDic[zhqIndex] + " ");
                        }
                        else
                        {
                            // dgv.Rows[index].Cells[6].Value = "检测转换器";//选号器 列5 dgv.Rows[index].Cells[6].Value = zhqDic[int.Parse(temp.ColumnNewFour.ToString())];//选号器 列5
                            strBuilder.Append("空 ");
                        }

                        strBuilder.Remove(strBuilder.Length - 1, 1);
                        streamWriter.WriteLine(strBuilder.ToString());
                        biliCtrl++;
                        if (biliCtrl==bili)
                        {
                            biliCtrl = 0;
                            x++;
                        }
                       
                        
                    }
                   
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

        private void btn_gddel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Help_ColumnRowCount; i++)
            {
                dgdel(help_randomTemps[i].ColumnNewOne);
            }
            if (isExport)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd hh：mm：ss");
                Export_Text(time);
            }
            
            
        }
        private void dgdel(string columnOne)
        {
            isExport = true;
            List<int> lists = new List<int>();
            try
            {

                //if (MessageBox.Show(string.Format(" 筛选号：{0}\n 行数保留：{1}\n是否确定滚动删除？", txt_SXRow1.Text, txt_SXRow2.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //{
                    //string columnOne = txt_SXRow1.Text;
                    string sxRow2 = txt_SXRow2.Text;
                    string[] strarr0 = columnOne.Split(',');//
                    int proNo = -1;
                    int prodgIndex = 0;
                    int saveCount = 0;
                    for (int i = 0; i < randomTemps.Count; i++)
                    {
                        RandomTemp randomtemp = randomTemps[i];
                        if (!randomtemp.ColumnNewOne.Equals("-1"))
                        {
                            if (proNo != randomtemp.ProjectNo)
                            {//新方案
                                if (saveCount != int.Parse(sxRow2) && proNo != -1)
                                {
                                    lists.Add(proNo);
                                }
                                proNo = randomtemp.ProjectNo;
                                saveCount = 0;
                                prodgIndex = ProjectgdDeleIndex[randomtemp.ProjectNo];
                            }
                            if (prodgIndex != 0)
                            {
                                prodgIndex--;
                                continue;
                            }
                            if (saveCount == int.Parse(sxRow2))
                            {
                                //dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Style.BackColor = Color.Yellow;
                                continue;
                            }
                            Boolean isSave = true;

                            dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Style.BackColor = Color.Yellow;
                            ProjectgdDeleIndex[randomtemp.ProjectNo]++;
                            string strarr1 = "0";
                            try
                            {
                                if (randomtemp.ColumnZhq != null)
                                {
                                    strarr1 = randomtemp.ColumnZhq;//选号器
                                }
                                else
                                {
                                    strarr1 = "0";
                                }

                            }
                            catch (Exception)
                            {
                                strarr1 = "0";
                            }

                                for (int j = 0; j < strarr0.Length; j++)
                                {
                                    if (strarr1.Equals(strarr0[j]))
                                    {//删除
                                        dgv_Random.Rows[randomtemp.Rowindex1].Cells[1].Value = "";
                                        randomTemps[i].ColumnNewOne = "-2";
                                        isSave = false;
                                    }

                                }


                            if (isSave)
                            {
                                saveCount++;
                            }
                            if (saveCount != int.Parse(sxRow2) && proNo != -1)
                            {
                                lists.Add(proNo);
                            }
                        }
                        
                    }
                //}
                if (lists.Count > 0)
                {
                    string str = "";
                    foreach (int item in lists)
                    {
                        str += "方案" + item + "\n";
                    }
                    //isExport = false;
                   // MessageBox.Show("以下方案号为错误方案，不是保留行数的整数倍：\n" + str);
                }
                
                    
            }
            catch (Exception)
            {
                isExport = false;
                MessageBox.Show("有错误操作！");
            }
        }

        private void btn_Corresponding_Click(object sender, EventArgs e)
        {
            List<int> lists = new List<int>();
            //try
            //{
                if (MessageBox.Show(string.Format(" 筛选号：{0}\n ,{1}\n 是否确定对应删除？", txt_SXRow1.Text, txt_SXRow2.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string columnOne = txt_SXRow1.Text;
                    string sxRow2 = txt_SXRow2.Text;
                    string[] strarr0 = columnOne.Split(',');//
                    int bili = ColumnOneRows / ColumnTwoRows;
                    int proNo = -1;
                    int prodgIndex = 0;
                    int saveCount = 0;
                    for (int i = 0; i < randomTemps.Count; i++)
                    {
                        RandomTemp randomtemp = randomTemps[i];
                         if (proNo != randomtemp.ProjectNo)
                        {//新方案
                            proNo = randomtemp.ProjectNo;
                            prodgIndex ++;
                        }
                        string[] strarr1 = randomtemp.ColumnOne.Split(',');//选号器
                        for (int x = 0; x < strarr1.Length; x++)//对比
                        {
                            for (int j = 0; j < strarr0.Length; j++)
                            {
                                if (strarr1[x].Equals(strarr0[j]))
                                {//删除
                                    dgv_Random.Rows[randomtemp.Rowindex1].Cells[2].Value = "";
                                    int indexstart = randomtemp.Rowindex1-(randomtemp.Rowindex1 - (prodgIndex - 1) * (ColumnOneRows + 1)) % bili;

                                    for (int m = 0; m < bili; m++)
                                    {
                                        dgv_Random.Rows[indexstart].Cells[2].Value = "";
                                        dgv_Random.Rows[indexstart].Cells[1].Value = "";
                                        indexstart++;
                                    }

                                }

                            }

                        }

                    }
                   
                }
              

                
            //}
            //catch (Exception)
            //{
            //     MessageBox.Show("有错误操作！");
            //}
        }
        Boolean isCHG = false;
        private void btn_SelectorCHG_Click(object sender, EventArgs e)//选号器变换
        {
           return;
            if (!isCHG)
            {
                btn_SelectorCHG.Text = "变换随机";
                isCHG = true;
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    RandomTemp rantemp = randomTemps[i];
                    COTemp[i] = rantemp.ColumnOne;
                    string[] cos = rantemp.ColumnOne.Split(',');
                    string newCO = "";
                    for (int j = 0; j < cos.Length; j++)
                    {
                        if (j != cos.Length - 1)
                        {
                            newCO += Selector[int.Parse(cos[j])-1] + ",";
                        }
                        else
                        {
                            newCO += Selector[int.Parse(cos[j]) - 1];
                        }
                    }
                    randomTemps[i].ColumnOne = newCO;
                }
                pageSize = 100 * (ColumnOneRows);      //设置页面行数 100方案数
                nMax = (randomTemps.Count / ColumnOneRows) * (ColumnOneRows);
                pageCount = (nMax / pageSize);    //计算出总页数
                if ((nMax % pageSize) > 0) pageCount++;
                pageCurrent = 1;    //当前页数从1开始
                nCurrent = 0;       //当前记录数从0开始
                LoadData();
            }
            else
            {
                btn_SelectorCHG.Text = "变换选号";
                isCHG = false;
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    randomTemps[i].ColumnOne = COTemp[i];
                }
                pageSize = 100 * (ColumnOneRows);      //设置页面行数 100方案数
                nMax = (randomTemps.Count / ColumnOneRows) * (ColumnOneRows);
                pageCount = (nMax / pageSize);    //计算出总页数
                if ((nMax % pageSize) > 0) pageCount++;
                pageCurrent = 1;    //当前页数从1开始
                nCurrent = 0;       //当前记录数从0开始
                LoadData();
            }
            
        }

        private void btn_saveHelp_Click(object sender, EventArgs e)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Help_ColumnCount"].Value = txt_Help_ColumnCount.Text;
            config.AppSettings.Settings["Help_ColumnMin"].Value = txt_Help_ColumnMin.Text;
            config.AppSettings.Settings["Help_ColumnMax"].Value = txt_Help_ColumnMax.Text;
            config.AppSettings.Settings["Help_AdditionalColumnCount"].Value = txt_Help_AdditionalColumnCount.Text;
            config.AppSettings.Settings["Help_ColumnRowCount"].Value = txt_Help_ColumnRowCount.Text;

            config.AppSettings.Settings["Help2_ColumnCount"].Value = txt_Help2_ColumnCount.Text;
            config.AppSettings.Settings["Help2_ColumnMin"].Value = txt_Help2_ColumnMin.Text;
            config.AppSettings.Settings["Help2_ColumnMax"].Value = txt_Help2_ColumnMax.Text;

            config.AppSettings.Settings["Help_automaticCount"].Value = txt_Help_automaticCount.Text;
            config.AppSettings.Settings["ColumnFourColumns"].Value = tb_ColumnFourColumns.Text;
            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            loadConfig();
            initView();//初始化
            MessageBox.Show("保存成功");
        }
    }
}
