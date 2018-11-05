using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 彩牛通.Entity;

namespace 彩牛通
{
    public partial class AddProgrammeForm : Form
    {
        private int ColumnTwoRows;
        private int ColumnOneRows;
        private int AdditionalColumnCount;
        private int ColumnTwoMin, ColumnTwoMax;
        public  List<RandomTemp> randomTemps;
        public AddProgrammeForm(int ColumnOneRows, int ColumnTwoRows, int ProjectNo, int AdditionalColumnCount,int ColumnTwoMin, int  ColumnTwoMax)
        {
            this.ColumnTwoRows = ColumnTwoRows;
            this.ColumnOneRows = ColumnOneRows;
            this.AdditionalColumnCount = AdditionalColumnCount;
            this.ColumnTwoMin = ColumnTwoMin;
            this.ColumnTwoMax = ColumnTwoMax;
            randomTemps = new List<RandomTemp>();
            InitializeComponent();
            for (int i = 0; i < ColumnOneRows; i++)
            {
                RandomTemp randomtemp = new RandomTemp();
                randomtemp.ColumnOne = "";
                randomtemp.ColumnTwo = "";
                randomtemp.SingleCount1 = 0;
                randomtemp.Rowindex1 = -1;
                randomtemp.ProjectNo = ProjectNo;
                randomtemp.ProjectCount = 0;
                randomtemp.ProjectMemo = "0次";
                randomTemps.Add(randomtemp);

            }
            
            initDGV();
        }

        private void initDGV()
        {
            updateDGV(dgv_Random, randomTemps, ColumnOneRows);
            updateCTDGV(dgv_ColumnTwo, randomTemps, ColumnTwoRows);
            //单机单元格即可编辑
            this.dgv_Random.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_ColumnTwo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
        }
        private void updateDGV(DataGridView dgv, List<RandomTemp> rt,int count)
        {
            dgv.Rows.Clear();
            int x = 0;
            for (int i = 0; i < count; i++)
            {
                RandomTemp randomtemp = rt[i];
                updateProject(dgv, randomtemp, i);
                x++;
            }
        }
        private void updateCTDGV(DataGridView dgv, List<RandomTemp> rt, int count)
        {
            dgv.Rows.Clear();
            int x = 0;
            for (int i = 0; i < count; i++)
            {
                RandomTemp randomtemp = rt[i];
                updateProjectCT(dgv, randomtemp, i);
                x++;
            }
        }
        public void updateProject(DataGridView dgv, RandomTemp temp, int ranIndex)
        {

            int index = dgv.Rows.Add();
            dgv.Rows[index].Cells[0].Value = ranIndex+1;
            dgv.Rows[index].Cells[1].Value = temp.ColumnOne.ToString();
            temp.Rowindex1 = index;

        }
        public void updateProjectCT(DataGridView dgv, RandomTemp temp, int ranIndex)
        {

            int index = dgv.Rows.Add();
            dgv.Rows[index].Cells[0].Value = ranIndex + 1;
            dgv.Rows[index].Cells[1].Value = temp.ColumnTwo.ToString();
            temp.Rowindex1 = index;

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_Random.RowCount; i++)
            {
                randomTemps[i].ColumnOne = dgv_Random.Rows[i].Cells[1].Value.ToString();
            }
            int bili = ColumnOneRows / ColumnTwoRows;
            int index=0;
            for (int i = 0; i < dgv_Random.RowCount; i++)
            {
                randomTemps[i].ColumnTwo = dgv_ColumnTwo.Rows[index].Cells[1].Value.ToString();

                if ((i+1) % bili == 0||bili==1)
                {
                    index++;
                }
            }
        }


        public void DataGirdViewCellPaste(DataGridView p_Data)
        {
            try
            {
                // 获取剪切板的内容，并按行分割  
                string pasteText = Clipboard.GetText();
                if (string.IsNullOrEmpty(pasteText))
                    return;
                string[] lines = pasteText.Split('\n');
                //再按单元格填充行  
                int i = 0;
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line.Trim()))
                        continue;
                    // 按 Tab 分割数据  
                    //string[] vals = line.Split('\t');\
                    if (line.Contains("\r"))
                    {
                        string str = line.Substring(0,line.Length-1);
                        p_Data.Rows[i].Cells[1].Value = str;
                    }
                    else
                    {
                        p_Data.Rows[i].Cells[1].Value = line;
                    }
                    
                    i++;
                }
            }
            catch
            {
                // 不处理  
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          // MessageBox.Show("123");
            DataGirdViewCellPaste(dgv_Random);
        }

        private void dgv_Random_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.ContextMenuStrip = contextMenuStrip1;
        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGirdViewCellPaste(dgv_ColumnTwo);
        }

        private void dgv_ColumnTwo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.ContextMenuStrip = contextMenuStrip2;
        }

        private void btn_completion_Click(object sender, EventArgs e)
        {
            try
            {
                int first = int.Parse(txt_first.Text) - 1;
                for (int i = 0; i < randomTemps.Count; i++)
                {
                    first = (first + 1) <= ColumnTwoMax ? first + 1 : 1;
                    randomTemps[i].ColumnTwo = first.ToString();
                }
                setAdditionalColumn2();
                setColumnTwoNew();
                updateCTDGV(dgv_ColumnTwo, randomTemps, ColumnTwoRows);
            }
            catch (Exception)
            {
                
                throw;
            }
           
            
        }
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
                                        int two = (int.Parse(randomTemps[i - x].ColumnTwo.ToString()) < ColumnTwoMax ? int.Parse(randomTemps[i - x].ColumnTwo.ToString()) : 0) + 1;
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
                        randomTemps[i].ColumnTwo = randomTemps[i].ColumnTwo.ToString() + "," + randomTemps[i].ColumnThree.ToString() + "," + randomTemps[i].ColumnFour.ToString() + "," + randomTemps[i].ColumnFive.ToString() + "," + randomTemps[i].ColumnSix.ToString();
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

        private void AddProgrammeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
