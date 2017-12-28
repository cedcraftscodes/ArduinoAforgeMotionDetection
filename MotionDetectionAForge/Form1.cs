using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Vision.Motion;
using System.IO.Ports;

namespace MotionDetectionAForge
{
    public partial class Form1 : Form
    {

        FilterInfoCollection fic;
        VideoCaptureDevice device;
        MotionDetector motiondetector;
        float f;
    
        public Form1()
        {
            InitializeComponent();
            sp = new SerialPort("COM3");
            sp.BaudRate = 9600;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            motiondetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo item in fic)
            {
                cbosource.Items.Add(item.Name);
            }
            cbosource.SelectedIndex = 0;
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            device = new VideoCaptureDevice(fic[cbosource.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = device;
            videoSourcePlayer1.Start();
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            f = motiondetector.ProcessFrame(image);
        }

        double previousAlarm = 0;
        double delay = 4000;
        float motionThreshold = .0005f;
        SerialPort sp;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblvalue.Text = "Motion Detected: " + f;

            double currTime = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;

            if((previousAlarm + delay) < currTime && f >= motionThreshold)
            {
                previousAlarm = currTime;
                sendSerialPort("1");
            }

        }

        private void sendSerialPort(String message)
        {
            try
            {
                sp.Open();
                sp.WriteLine("1");
                sp.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
