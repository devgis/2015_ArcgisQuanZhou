using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Collections.Generic;
using ESRI.ArcGIS.NetworkAnalyst;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using System.Threading;

namespace MapApp
{
    public sealed partial class MainForm : Form
    {
        string strPath = System.Environment.CurrentDirectory + @"\map.mxd"; //地图路径
        IFeatureLayer JGLayer = null;
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        public MainForm()
        {
            InitializeComponent();
            m_ipActiveView = axMapControl1.ActiveView;
            this.axMapControl1.OnSelectionChanged += AxMapControl1_OnSelectionChanged;
        }

        private void AxMapControl1_OnSelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("cc");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //加载地图
            //string sPath = System.IO.Path.Combine(Application.StartupPath, @"test\map.mxd");
            axMapControl1.LoadMxFile(strPath);
            
            JGLayer=CreateLayerInMomeoy();
            axMapControl1.Map.AddLayer(JGLayer);

            LoadData();
            axMapControl1.Refresh();

        }

        private void LoadData()
        {
            DataTable dtTemp = OLEHelper.Instance.GetDataTable("select * from t_jg");
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                //清除
                //featureClass.
   
                //for (int i = featureClass. - 1; i >= 0; i--)
                //{
 
                //}

                IQueryFilter queryFilter = new QueryFilterClass();
                IFeatureCursor featureCusor;
                queryFilter.WhereClause = "1=1";
                featureCusor = featureClass.Search(queryFilter, true);
                //IFeature feature = null;
                //while ((feature = featureCusor.NextFeature()) != null)
                //{
                //    featureClass.DeleteIndex
                //}
                ITable pTable = featureClass as ITable;
                pTable.DeleteSearchedRows(queryFilter);


                    foreach (DataRow row in dtTemp.Rows)
                    {
                        //IFeatureClass feature = featureClass.CreateFeature();
                        //featureClass.
                        double x = Convert.ToDouble(row["x"]);
                        double y = Convert.ToDouble(row["y"]);

                        IFeature toFeature = featureClass.CreateFeature();
                        ESRI.ArcGIS.Geometry.IPoint newPoint = new PointClass();
                        newPoint.PutCoords(x, y);
                        toFeature.Shape = newPoint;
                        //id,x,y,z,name,type,owner,caizhi,guige,xinghao,xingzhuang,remarks
                        toFeature.set_Value(featureClass.Fields.FindField("id"), row["id"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("name"), row["name"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("x"), row["x"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("y"), row["y"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("z"), row["z"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("type"), row["type"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("owner"), row["owner"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("caizhi"), row["caizhi"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("guige"), row["guige"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("xinghao"), row["xinghao"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("xingzhuang"), row["xingzhuang"].ToString());
                        toFeature.set_Value(featureClass.Fields.FindField("remarks"), row["remarks"].ToString());
                        toFeature.Store();
                    }
                    this.axMapControl1.Refresh();
            }
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }


        IActiveView m_ipActiveView;
        //const string MEMORY_WORKSPACE = "WorkSpace_Data";
        //const string LAYER_NAME = "Deports Layer";
        //static IFeatureClass pInputFC, pVertexFC;
        IFeatureClass featureClass = null;
 
        public IFeatureLayer CreateLayerInMomeoy(  )
        {
            IWorkspaceName workspaceName = null;
            IWorkspaceFactory workspaceFactory = null;
            IFeatureWorkspace workspace = null;
            //IFeatureClass featureClass = null;
            IGeoFeatureLayer featureLayer = null; //IFeatureLayer
            IFieldEdit fieldEdit = null;
            IGeometryDefEdit geometryDefEdit = null;
            ISpatialReference spatialRef = null;
            //IFieldsEdit allFields = new ESRI.ArcGIS.Geodatabase();
            IFieldsEdit allFields = new ESRI.ArcGIS.Geodatabase.FieldsClass();
            //Add Shapes Fields
            fieldEdit = new FieldClass();
            fieldEdit.Name_2 = "Shape";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fieldEdit.IsNullable_2 = true;
            fieldEdit.Required_2 = true;
            geometryDefEdit = new GeometryDefClass();
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            spatialRef = new UnknownCoordinateSystemClass();
            spatialRef.SetDomain(-18000, 18000, -18000, 18000);
            geometryDefEdit.SpatialReference_2 = spatialRef;
            fieldEdit.GeometryDef_2 = geometryDefEdit;
            allFields.AddField(fieldEdit);

            //添加其他Field
            IFieldEdit nameField = new FieldClass();
            nameField.Name_2 = "name";
            nameField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(nameField);

            IFieldEdit idField = new FieldClass();
            idField.Name_2 = "id";
            idField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(idField);

            IFieldEdit xField = new FieldClass();
            xField.Name_2 = "x";
            xField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(xField);

            IFieldEdit yField = new FieldClass();
            yField.Name_2 = "y";
            yField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(yField);

            IFieldEdit zField = new FieldClass();
            zField.Name_2 = "z";
            zField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(zField);

            IFieldEdit typeField = new FieldClass();
            typeField.Name_2 = "type";
            typeField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(typeField);

            IFieldEdit ownerField = new FieldClass();
            ownerField.Name_2 = "owner";
            ownerField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(ownerField);

            IFieldEdit caizhiField = new FieldClass();
            caizhiField.Name_2 = "caizhi";
            caizhiField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(caizhiField);


            IFieldEdit guigeField = new FieldClass();
            guigeField.Name_2 = "guige";
            guigeField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(guigeField);

            IFieldEdit xinghaoField = new FieldClass();
            xinghaoField.Name_2 = "xinghao";
            xinghaoField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(xinghaoField);

            IFieldEdit xingzhuangField = new FieldClass();
            xingzhuangField.Name_2 = "xingzhuang";
            xingzhuangField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(xingzhuangField);

            IFieldEdit remarksField = new FieldClass();
            remarksField.Name_2 = "remarks";
            remarksField.Type_2 = esriFieldType.esriFieldTypeString;
            allFields.AddField(remarksField);
            //,x,y,z,name,type,owner,caizhi,guige,xinghao,xingzhuang,remarks


            //Create memory layer
            workspaceFactory = new InMemoryWorkspaceFactoryClass();
            workspaceName = workspaceFactory.Create("", "JGWorkSpace", null, 0);
            workspace = ((IName)workspaceName).Open() as IFeatureWorkspace;
            featureClass = workspace.CreateFeatureClass("JGFeatureClass", allFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            featureLayer = new FeatureLayerClass();
            featureLayer.Name = "JGLayer";
            featureLayer.FeatureClass = featureClass;
            #region 显示label
            //UniqueValueRenderFlyr(featureLayer); //符号化
            displayLabel(this.axMapControl1.Map, featureLayer, "name");
            symbolFeatureLayer(featureLayer);

            #endregion

            return featureLayer;
        }
        //

        /// <summary>显示label
        /// </summary>
        /// <param name="map"></param>
        /// <param name="featLayer"></param>
        /// <param name="field"></param>
        public void displayLabel(IMap map, IGeoFeatureLayer featLayer, string field)
        {
            featLayer.DisplayAnnotation = true;
            IAnnotateLayerPropertiesCollection pAnnProCol = featLayer.AnnotationProperties;
            ILabelEngineLayerProperties pLabelEngine = null;
            IAnnotateLayerProperties prop;
            IBasicOverposterLayerProperties pBasicOverposterLayerProps = new BasicOverposterLayerProperties();
            pBasicOverposterLayerProps.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
            ITextSymbol symbol = new TextSymbolClass();
            IColor c = new RgbColorClass();
            c.RGB = 123;
            symbol.Color = c;
            symbol.Size = 16;

            IBasicOverposterLayerProperties bo = new BasicOverposterLayerPropertiesClass();
            IPointPlacementPriorities ipp = new PointPlacementPrioritiesClass();
            ipp.BelowCenter = 1;
            bo.PointPlacementPriorities = ipp;
            for (int i = 0; i < pAnnProCol.Count; i++)
            {
                IElementCollection ec = new ElementCollectionClass();
                pAnnProCol.QueryItem(i, out prop, out ec, out ec);
                pLabelEngine = (ILabelEngineLayerProperties)prop;
                pLabelEngine.Expression = "[" + field + "]";
                pLabelEngine.Symbol = symbol;
                pLabelEngine.BasicOverposterLayerProperties = bo;
            }

            ITrackCancel pCon = new CancelTracker();
            pCon.Continue();
            featLayer.Draw(esriDrawPhase.esriDPAnnotation, ((IActiveView)map).ScreenDisplay, pCon);
        }

        public void symbolFeatureLayer(IGeoFeatureLayer fLayer)
        {
            ISimpleRenderer pRenderer;
            pRenderer = new SimpleRendererClass();
            IPictureMarkerSymbol picSymbol = new PictureMarkerSymbol();
            picSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPicturePNG, System.IO.Path.Combine(Application.StartupPath, "symbol.png"));
            RgbColorClass pBkColor = new RgbColorClass();
            pBkColor.Blue = 0;
            pBkColor.Red = 255;
            pBkColor.Green = 165;
            pBkColor.Transparency = 100;
            picSymbol.Size = 20;
            picSymbol.BitmapTransparencyColor = pBkColor;

            pRenderer.Symbol = picSymbol as ISymbol;

            fLayer.Renderer = pRenderer as IFeatureRenderer;
            //(fLayer as IGeoFeatureLayer).Renderer=new SimpleRendererClass
        }
        ///<summary>
        ///获取符号库中符号
        ///</summary>
        ///<param name="sServerStylePath">符号库全路径名称</param>
        ///<param name="sGalleryClassName">GalleryClass名称</param>
        ///<param name="symbolName">符号名称</param>
        ///<returns>符号</returns>
        private ISymbol GetSymbol(string sServerStylePath, string sGalleryClassName, string symbolName)
        {
            try
            {
                //ServerStyleGallery对象
                IStyleGallery pStyleGaller = new ServerStyleGalleryClass();
                IStyleGalleryStorage pStyleGalleryStorage = pStyleGaller as IStyleGalleryStorage;
                IEnumStyleGalleryItem pEnumSyleGalleryItem = null;
                IStyleGalleryItem pStyleGallerItem = null;
                IStyleGalleryClass pStyleGalleryClass = null;
                //使用IStyleGalleryStorage接口的AddFile方法加载ServerStyle文件
                pStyleGalleryStorage.AddFile(sServerStylePath);
                //遍历ServerGallery中的Class
                for (int i = 0; i < pStyleGaller.ClassCount; i++)
                {
                    pStyleGalleryClass = pStyleGaller.get_Class(i);
                    if (pStyleGalleryClass.Name != sGalleryClassName)
                        continue;
                    //获取EnumStyleGalleryItem对象
                    pEnumSyleGalleryItem = pStyleGaller.get_Items(sGalleryClassName, sServerStylePath, "");
                    pEnumSyleGalleryItem.Reset();
                    //遍历pEnumSyleGalleryItem
                    pStyleGallerItem = pEnumSyleGalleryItem.Next();
                    while (pStyleGallerItem != null)
                    {
                        if (pStyleGallerItem.Name == symbolName)
                        {
                            //获取符号
                            ISymbol pSymbol = pStyleGallerItem.Item as ISymbol;
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumSyleGalleryItem);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pStyleGalleryClass);
                            return pSymbol;
                        }
                        pStyleGallerItem = pEnumSyleGalleryItem.Next();
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumSyleGalleryItem);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pStyleGalleryClass);
                return null;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        ///<summary>
        ///设置要素图层唯一值符号化
        ///</summary>
        ///<param name="pFeatureLayer"></param>
        private void UniqueValueRenderFlyr(IFeatureLayer pFeatureLayer)
        {
            try
            {
                //创建UniqueValueRendererClass对象
                IUniqueValueRenderer pUVRender = new UniqueValueRendererClass();
                List<string> pFieldValues = new List<string>();
                //pFieldValues.Add("Hospital 2");
                //pFieldValues.Add("School 1");
                pFieldValues.Add("Airport");
                //for (int i = 0; i < pFieldValues.Count; i++)
                //{
                //    ISymbol pSymbol = new SimpleMarkerSymbolClass();
                //    pSymbol = GetSymbol(@"D:\Program Files (x86)\ArcGIS\Desktop10.1\Styles\ESRI.ServerStyle", "Marker Symbols", pFieldValues[i]);
                //    //添加唯一值符号化字段值和相对应的符号
                //    pUVRender.AddValue(pFieldValues[i], pFieldValues[i], pSymbol);
                //}
                ISymbol pSymbol = getSymbol();
                //pUVRender.DefaultSymbol = pSymbol;
                pUVRender.AddValue("Airport", "Airport", pSymbol);
                //pUVRender.Symbol = pSymbol;
                //设置唯一值符号化的字段个数和字段名
                pUVRender.FieldCount = 1;
                pUVRender.set_Field(0, "Airport");

                IGeoFeatureLayer pGFeatureLyr = pFeatureLayer as IGeoFeatureLayer;
                //设置IGeofeatureLayer的Renderer属性
                pGFeatureLyr.Renderer = pUVRender as IFeatureRenderer;
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private ISymbol getSymbol()
        {
            //创建SimpleMarkerSymbolClass对象
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            //创建RgbColorClass对象为pSimpleMarkerSymbol设置颜色
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pSimpleMarkerSymbol.Color = pRgbColor as IColor;
            //设置pSimpleMarkerSymbol对象的符号类型，选择钻石
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            //设置pSimpleMarkerSymbol对象大小，设置为５
            pSimpleMarkerSymbol.Size = 15;
            //显示外框线
            pSimpleMarkerSymbol.Outline = true;
            //为外框线设置颜色
            IRgbColor pLineRgbColor = new RgbColorClass();
            pLineRgbColor.Green = 255;
            pSimpleMarkerSymbol.OutlineColor = pLineRgbColor as IColor;
            //设置外框线的宽度
            pSimpleMarkerSymbol.OutlineSize = 1;

            return pSimpleMarkerSymbol as ISymbol;
        }
        List<IPoint> ptList=new List<IPoint>();
         
         //string strPath = System.Environment.CurrentDirectory + @"\ex16\ex16.mxd";
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (ptList.Count <= 0)
        //    {
        //        MessageBox.Show("请点击地图选择点！");
        //    }
        //    else
        //    {
        //        this.axMapControl1.AddLayer(CreateRouteLayer(strPath, ptList) as ILayer);
        //        ptList.Clear();
        //    } 
        //}

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IPoint ipNew;
            ipNew = m_ipActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y); //获取坐标
            //MessageBox.Show("Clicked");
        }

        private ILayer GetLayer(string LayerName)
        {
            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            {
                ILayer pLayer = axMapControl1.Map.get_Layer(i);
                if (pLayer.Name.Equals(LayerName))
                {
                    return pLayer;
                }
            }
            return null;

        }

        private void QueryLayer(string words)
        {
            //ILayer layer = GetLayer("xuequ");
            //IFeatureLayer featureLayer = layer as IFeatureLayer;
            
            IFeatureClass featureClass = JGLayer.FeatureClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            IFeatureCursor featureCusor;
            queryFilter.WhereClause = string.Format("name like '%{0}%' or type  like '%{0}%' or owner  like '%{0}%' or caizhi like '%{0}%' or guige like '%{0}%' or xinghao like '%{0}%' or xingzhuang like '%{0}%' or remarks like '%{0}%'", words);
            featureCusor = featureClass.Search(queryFilter, true);
            IFeature feature = null;

            //查找出所有点并计算距离
            while ((feature = featureCusor.NextFeature()) != null)
            {
                this.axMapControl1.CenterAt(feature.Shape as IPoint);
                this.axMapControl1.Refresh();
                Thread.Sleep(10);
                axMapControl1.FlashShape(feature.Shape);
                Thread.Sleep(10);
            }
        }


        private void tsmiJGManager_Click(object sender, EventArgs e)
        {
            JGManager frmJGManager = new JGManager();
            if (frmJGManager.ShowDialog() == DialogResult.OK)
            {
                //刷新数据
                LoadData();
            }
        }

        private void tsmiJGSearch_Click(object sender, EventArgs e)
        {
            MapSearchJG frmMapSearchJG = new MapSearchJG();
            if (frmMapSearchJG.ShowDialog() == DialogResult.OK)
            {
                //查找地图定位
                QueryLayer(frmMapSearchJG.Words);
            }
        }
    }
}