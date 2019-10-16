using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using System.IO;
using ESRI.ArcGIS.Controls;

namespace SimpleArcEngineDemo
{
    public partial class FeatrAttributeTable : Form
    {
        //声明地图控件的变量。
        private AxMapControl axMapControl;

        public FeatrAttributeTable()
        {
            InitializeComponent();
        }

        //构造函数。
        public FeatrAttributeTable(AxMapControl pMapControl)
        {
            InitializeComponent();
            axMapControl = pMapControl;
        }

        private void FeatrAttributeTable_Load(object sender, EventArgs e)
        {
            //得到地图控件的第0层图层。
            ILayer pLayer = axMapControl.get_Layer(0);
            //将pLayer类型强制转换为IFeatureLayer。
            IFeatureLayer pFLayer = pLayer as IFeatureLayer;
            IFeatureClass pFC = pFLayer.FeatureClass;

            //获得游标。
            IFeatureCursor pFCursor = pFC.Search(null, false);
            //获得第0图层的第一个要素, 要素中包含多个属性值。
            IFeature pFeature = pFCursor.NextFeature();

            //新建内存表格, 并构建表结构，包括属性字段和数据字段。
            DataTable pTable = new DataTable();
            DataColumn colName = new DataColumn("洲名");
            colName.DataType = System.Type.GetType("System.String");
            pTable.Columns.Add(colName);
            DataColumn colArea = new DataColumn("面积");
            colArea.DataType = System.Type.GetType("System.String");
            pTable.Columns.Add(colArea);

            //获得字段名为"CONTINENT"在内存表中的字段索引。下同
            int indexOfName = pFC.FindField("CONTINENT");
            int indexOfName2 = pFC.FindField("SQMI");
            //当要素不为空时
            while (pFeature != null)
            {
                //得到indexOfName的索引号
                string name = pFeature.get_Value(indexOfName).ToString();
                string area = pFeature.get_Value(indexOfName2).ToString();
                DataRow pRow = pTable.NewRow();
                pRow[0] = name;
                pRow[1] = area;
                pTable.Rows.Add(pRow);
                pFeature = pFCursor.NextFeature();
            }
            dataGridView1.DataSource = pTable; //将属性表连接到dataGridView1控件
        }
    }
}
