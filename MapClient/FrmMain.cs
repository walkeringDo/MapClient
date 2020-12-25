using System.Windows.Forms;
using CCWin;

namespace MapClient
{
    public partial class FrmMain : Skin_Mac
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private FrmBaidu frmBaidu;
        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            frmBaidu = new FrmBaidu();
            frmBaidu.Dock=DockStyle.Fill;
            frmBaidu.TopLevel = false;
            frmBaidu.FormBorderStyle=FormBorderStyle.None;
            frmBaidu.Show();
            panel1.Controls.Add(frmBaidu);
        }
    }
}
