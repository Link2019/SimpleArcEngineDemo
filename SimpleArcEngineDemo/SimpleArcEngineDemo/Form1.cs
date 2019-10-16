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

namespace SimpleArcEngineDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 2.2添加shp数据————添加ShapeFile文件到地图控件中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAddShp_Click(object sender, EventArgs e)
        {
            //步骤1: 创建工作空间工厂。
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();

            //文件过滤器, 选择后缀名为.shp
            openFileDialog1.Filter = "ShapeFile文件(*.shp)|*.shp";
            //设定文件对话框的初始路径
            openFileDialog1.InitialDirectory = @"E:\arcgis开发\实验室";
            //示例数据文件夹
            openFileDialog1.Multiselect = false;
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            //得到文件名对应的路径、文件夹名等
            string pPath = openFileDialog1.FileName;    //得到完整的路径（路径+文件名）
            string pFolder = Path.GetDirectoryName(pPath);  //得到文件的路径（不包括文件名）
            string pFileName = Path.GetFileName(pPath); //得到文件的文件名

            //步骤2: 打开ShapeFile文件名对应的工作空间。
            IWorkspace pWorkspace1 = pWorkspaceFactory.OpenFromFile(pFolder, 0);  //数据目录
            IFeatureWorkspace pFeatureWorkspce = pWorkspace1 as IFeatureWorkspace;

            //步骤3: 打开要素类。
            IFeatureClass pFC = pFeatureWorkspce.OpenFeatureClass(pFileName);

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
        /// <summary>
        /// 通过lyr文件添加图层。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAddLyr_Click(object sender, EventArgs e)
        {
            //步骤1: 打开文件对话框浏览到一个具体lyr文件。
            openFileDialog1.Filter = "lyr文件(*.lyr)|*.lyr";
            openFileDialog1.InitialDirectory = @"E:\arcgis开发\实验室";
            openFileDialog1.Multiselect = false;
            DialogResult pDialogResult = openFileDialog1.ShowDialog();
            if (pDialogResult != DialogResult.OK)
            {
                return;
            }
            string pFileName = openFileDialog1.FileName;

            //步骤2: 通过地图控件的方法(AddLayerFromFile)直接加载。
            axMapControl1.AddLayerFromFile(pFileName);
            axMapControl1.ActiveView.Refresh();
        }

        private void 图层属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeatrAttributeTable frm = new FeatrAttributeTable(axMapControl1);
            frm.ShowDialog();
        }
    }
}
