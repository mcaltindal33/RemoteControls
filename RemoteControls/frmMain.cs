using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace RemoteControls
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        int portNumber;
        private static Image grapDesktop()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(rectangle.Width,
                                           rectangle.Height,
                                           System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(screenshot);
            graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }
        void SendDesktopImage()
        {
            BinaryFormatter binary = new BinaryFormatter();
            mainStream = client.GetStream();
            binary.Serialize(mainStream, grapDesktop());
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            portNumber = int.Parse(tPort.Text);
            try
            {
                client.Connect(tAddress.Text, portNumber);
                XtraMessageBox.Show("Bağlandı");
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bağlantı hatalı");
            }
        }

        private void BtnScreen_Click(object sender, EventArgs e)
        {

        }
    }
}