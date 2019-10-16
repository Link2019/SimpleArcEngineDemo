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

namespace SimpleArcEngineDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuAddShp_Click(object sender, EventArgs e)
        {
            //步骤1: 创建工作空间工厂。
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
           
            //步骤2: 打开ShapeFile文件名对应的工作空间。
            IWorkspace pWorkspace1 = pWorkspaceFactory.OpenFromFile(@"E:\arcgis开发\实验室\15华东地区房价专题图分析", 0);  //数据目录
            IFeatureWorkspace pFeatureWorkspce = pWorkspace1 as IFeatureWorkspace;

            //步骤3: 打开要素类。
            IFeatureClass pFC = pFeatureWorkspce.OpenFeatureClass("华东地区.shp");

            //步骤4: 创建要素图层。
            IFeatureLayer pFLayer = new FeatureLayerClass();
            pFLayer.FeatureClass = pFC;
            pFLayer.Name = pFC.AliasName;

            //步骤5: 关联图层和要素类。
            ILayer pLayer = pFLayer as ILayer;
            IMap pMap = axMapControl1.Map;

            //步骤6: 添加到地图控件中。
            pMap.AddLayer(pLayer);
            axMapControl1.ActiveView.Refresh();


        }
    }
}
