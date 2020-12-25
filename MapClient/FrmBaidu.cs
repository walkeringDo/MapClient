using System;
using System.Security.Permissions;
using System.Windows.Forms;
using CCWin;

namespace MapClient
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmBaidu : Form
    {

        #region 变量
        /// <summary>
        /// 是否是地图（true为地图，false为导航）
        /// </summary>
        private Boolean ismap = true;
        #endregion

        #region 构造函数
        public FrmBaidu()
        {
            InitializeComponent();
        }
        #endregion

        #region 方法
        //声明一个js调用返回string的方法
        public void getLon_lat(string point, string address)
        {
            if (IsClickGetIndomation.Checked)
            {
                MessageBoxEx.Show(point + "\r\n" + address,"位置信息");
            }
        }
        //声明一个js调用实时更新点击地图时的经纬度的方法
        public void ruternPoint(string lon, string lat)
        {
            if (IsGetLonLat.Checked)
            {
                TxtLon.SkinTxt.Text = lon;
                TxtLat.SkinTxt.Text = lat;
            }
        }

       /// <summary>
        /// 搜索线路方法（包含公交线路，步行线路，驾车线路）
       /// </summary>
       /// <param name="startAddress">起点位置信息</param>
       /// <param name="endAddress">终点位置信息</param>
        /// <param name="type">线路类型（包含公交线路，步行线路，驾车线路）</param>
        public void SousuoXianlu(string startAddress, string endAddress,string type)
        {
            //如果不是搜索地图，则自动切换成搜索地图
            if (!ismap)
            {
                if (MessageBoxEx.Show("正在使用导航地图，是否使用搜索地图？\r\n（确定）则取消导航，（取消）则继续导航！", "友情提示", MessageBoxButtons.OKCancel) ==
                    DialogResult.OK)
                {
                    webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
                    this.webBrowserMap.ObjectForScripting = this;
                    ismap = true;
                }
            }
            skinSplitContainer1.Panel1Collapsed=true;
            if (startAddress.Trim() == "" && endAddress.Trim() == "")
            {
                MessageBoxEx.Show("起点和终点不能为空", "查询条件不足");
            }
            else if (endAddress.Trim() == "")
            {
                MessageBoxEx.Show("终点不能为空", "查询条件不足");
            }
            else if (startAddress.Trim() == "")
            {
                MessageBoxEx.Show("起点不能为空", "查询条件不足");
            }
            else
            {
                object[] objects = new object[2];
                objects[0] = startAddress.Trim();
                objects[1] = endAddress.Trim();
                switch (type)
                {
                    case "步行":
                        webBrowserMap.Document.InvokeScript("Walking", objects);
                        break;
                    case "公交":
                        webBrowserMap.Document.InvokeScript("Transit", objects);
                        break;
                    case "驾车":
                        webBrowserMap.Document.InvokeScript("Driving", objects);
                        break;
                }
            }
        }

        /// <summary>
        /// 周边搜索
        /// </summary>
        /// <param name="keys">关键词</param>
        public void SousuoZhouBian(string keys)
        {
            //如果不是搜索地图，则自动切换成搜索地图
            if (!ismap)
            {

                if (MessageBoxEx.Show("正在使用导航地图，是否使用搜索地图？\r\n（确定）则取消导航，（取消）则继续导航！", "友情提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
                    this.webBrowserMap.ObjectForScripting = this;
                    ismap = true;
                    MessageBoxEx.Show("已切换至搜索地图,请重新搜索周边！", "周边搜索提示");
                }
            }
            else
            {
                if (keys.Trim() == "")
                {
                    MessageBoxEx.Show("请输入正确的关键字", "周边搜索提示");
                }
                else
                {
                    object[] objects = new object[1];
                    objects[0] = keys.Trim();
                    webBrowserMap.Document.InvokeScript("SearchZhoubian", objects);
                }
            }
        }
       /// <summary>
        /// 全国搜索
       /// </summary>
       /// <param name="keys">关键词</param>
        public void SousuoQuanGuo(string keys)
        {
            //如果不是搜索地图，则自动切换成搜索地图
            if (!ismap)
            {

                if (MessageBoxEx.Show("正在使用导航地图，是否使用搜索地图？\r\n（确定）则取消导航，（取消）则继续导航！", "友情提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
                    this.webBrowserMap.ObjectForScripting = this;
                    ismap = true;
                    MessageBoxEx.Show("已切换至搜索地图，请重新搜索全国！", "全国搜索提示");
                }
            }
            else
            {
                if (keys.Trim() == "")
                {
                    MessageBoxEx.Show("请输入正确的关键字", "全国搜索提示");
                }
                else
                {
                    object[] objects = new object[1];
                    objects[0] = keys.Trim();
                    webBrowserMap.Document.InvokeScript("SearchQuanguo", objects);
                }
            }
        }
        /// <summary>
        /// 根据IP地址快速定位
        /// </summary>
        public void IpDingWei()
        {
            //如果不是搜索地图，则自动切换成搜索地图
            if (!ismap)
            {

                if (MessageBoxEx.Show("正在使用导航地图，是否使用搜索地图？\r\n（确定）则取消导航，（取消）则继续导航！", "友情提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
                    this.webBrowserMap.ObjectForScripting = this;
                    ismap = true;
                }
            }
            else
            {
                string s = "";
                webBrowserMap.Document.InvokeScript("MyLocalCity");

                s = webBrowserMap.Document.InvokeScript("altStr").ToString();
                TxtLon.SkinTxt.Text = s.Substring(0, s.IndexOf(','));
                TxtLat.SkinTxt.Text = s.Substring(s.Substring(0, s.IndexOf(',')).Length + 1, s.Length - s.Substring(0, s.IndexOf(',')).Length - 1);
                //MessageBox.Show(s);
                //webBrowserMap.Document.InvokeScript("MoveD")
            }
        }
        /// <summary>
        /// 加载导航地图
        /// </summary>
        public void LoadDaohangMap()
        {
            webBrowserMap.Navigate(Application.StartupPath + "\\baidudaohang.html");
            this.webBrowserMap.ObjectForScripting = this;
            ismap = false;
        }
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="startAddress">起点位置信息</param>
        /// <param name="endAddress">终点位置信息</param>
        public void DaoHang(string startAddress,string endAddress)
        {
            if (ismap)
            {
                webBrowserMap.Navigate(Application.StartupPath + "\\baidudaohang.html");
                this.webBrowserMap.ObjectForScripting = this;
                ismap = false;
                MessageBoxEx.Show("已切换到导航页面，请重新加载导航线路！", "地图切换提示！");
            }
            else
            {
                if (startAddress.Trim() == "" && endAddress.Trim() == "")
                {
                    MessageBoxEx.Show("导航起始点和导航终点不能为空", "导航查询条件不足");
                }
                else if (endAddress.Trim() == "")
                {
                    MessageBoxEx.Show("导航终点不能为空", "导航查询条件不足");
                }
                else if (startAddress.Trim() == "")
                {
                    MessageBoxEx.Show("导航起始点不能为空", "导航查询条件不足");
                }
                else
                {
                    object[] objects = new object[2];
                    objects[0] = startAddress.Trim();
                    objects[1] = endAddress.Trim();
                    webBrowserMap.Document.InvokeScript("daohang", objects);
                }
            }
        }
        /// <summary>
        /// 重置地图
        /// </summary>
        public void CreatMap()
        {
            webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
            this.webBrowserMap.ObjectForScripting = this;
            ismap = true;
        }

        /// <summary>
        /// 按具体地址定位
        /// </summary>
        /// <param name="addressStr">具体地址</param>
        public void AddressToPoint(string addressStr)
        {
            if (addressStr.Trim() == "")
            {
                MessageBoxEx.Show("查询地址不能为空", "提示");
            }
            else
            {
                object[] objects = new object[1];
                objects[0] = addressStr.Trim();
                webBrowserMap.Document.InvokeScript("AddressToPoint", objects);
            }
        }
        /// <summary>
        /// 快速定位按钮
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        public void QuickSet(string lon, string lat)
        {
            object[] objects = new object[2];
            objects[0] = lon.Trim();
            objects[1] = lat.Trim();
            webBrowserMap.Document.InvokeScript("QuickSet", objects);
        }
        /// <summary>
        /// 添加兴趣点按钮
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        public void AddMarker(string lon, string lat)
        {
            object[] objects = new object[2];
            objects[0] = lon.Trim();
            objects[1] = lat.Trim();
            webBrowserMap.Document.InvokeScript("AddMarker", objects);
        }
        #endregion
        
        #region 事件
        //窗体加载事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
                ismap = true;
                this.webBrowserMap.ObjectForScripting = this;
                CbxRoute.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("该软件未正确安装或部分文件丢失，请重装软件或联系开发者！\r\n错误信息：" + ex.Message);
            }
        }
        //搜索线路按钮事件
        private void BtnSousuoXianluClick(object sender, EventArgs e)
        {
            SousuoXianlu(TxtStartAddress.SkinTxt.Text, TxtEndAddress.SkinTxt.Text, CbxRoute.Text);
        }
        //搜索周边
        private void BtnSousuoZhouBianClick(object sender, EventArgs e)
        {
            SousuoZhouBian(TxtKeys.SkinTxt.Text);
        }
        //搜索全国
        private void BtnSousuoQuanGuoClick(object sender, EventArgs e)
        {
            SousuoQuanGuo(TxtKeys.SkinTxt.Text);
        }
        //根据具体位置定位
        private void BtnAddressToPointClick(object sender, EventArgs e)
        {
            AddressToPoint(TxtAddress.SkinTxt.Text);
        }
        //快速定位
        private void BtnQuickSetClick(object sender, EventArgs e)
        {
            QuickSet(TxtLon.SkinTxt.Text, TxtLat.SkinTxt.Text);
        }
        //添加兴趣点
        private void BtnAddMarkerClick(object sender, EventArgs e)
        {
            AddMarker(TxtLon.SkinTxt.Text, TxtLat.SkinTxt.Text);
        }
        
        //加载导航地图
        private void BtnLoadDaohangMapClick(object sender, EventArgs e)
        {
            LoadDaohangMap();
        }
        //加载进度条
        private void webBrowserMap_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            skinProgressBar1.Visible = true;
            if ((e.CurrentProgress > 0) && (e.MaximumProgress > 0))
            {
                skinProgressBar1.Maximum = Convert.ToInt32(e.MaximumProgress);
                skinProgressBar1.Step = Convert.ToInt32(e.CurrentProgress);
                skinProgressBar1.PerformStep();
            }
            else if (webBrowserMap.ReadyState == WebBrowserReadyState.Complete)
            {
                skinProgressBar1.Value = 0;
                skinProgressBar1.Visible = false;
            }
        }
        //重置地图
        private void BtnCreatMapClick(object sender, EventArgs e)
        {
            CreatMap();
        }
        
        //导航线路
        private void BtnDaoHangClick(object sender, EventArgs e)
        {
            DaoHang(TxtDhStartAddress.SkinTxt.Text, TxtDhEndAddress.SkinTxt.Text);
        }

        //测量工具
        private void Tool_CeLiangClick(object sender, EventArgs e)
        {
            if (!ismap)
            {
                webBrowserMap.Navigate(Application.StartupPath + "\\baidu.html");
                this.webBrowserMap.ObjectForScripting = this;
                ismap = true;
                MessageBox.Show("已更换到搜索界面，请重新选择测量工具", "提示");
            }
            webBrowserMap.Document.InvokeScript("MouseDis");
        }
        #endregion
    }
}
