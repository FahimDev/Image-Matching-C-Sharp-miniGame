using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryStorm.Properties;

namespace MemoryStorm
{
    public partial class Form1 : Form
    {
        private bool _allowClick = true;
        private PictureBox _firstGuess;
        private readonly Random _random = new Random();
        private readonly Timer _ClickTimer = new Timer();
        int tick = 60;
        int matchLeft = 8;
        readonly Timer timer = new Timer { Interval = 1000 };
        

        public Form1()
        {
            InitializeComponent();
            SetRandomImages();
            HideImages();
            //StartGameTimer();
            _ClickTimer.Interval = 1000;
            _ClickTimer.Tick += _ClickTimer_tick;
            
        }

        public void clock()
        {
            this.Clock.Image = Resources.Clock;
        }
        private PictureBox[] PictureBoxes
        {
            
            get { return Controls.OfType<PictureBox>().ToArray(); }
            
        }


        private static IEnumerable<Image> Images
        {
            get
            {
                return new Image[]
                {
                   Resources.Img1,
                   Resources.Img2,
                   Resources.Img3,
                   Resources.Img4,
                   Resources.Img5,
                   Resources.Img6,
                   Resources.Img7,
                   Resources.Img8,
                   
                   



                };
            }


        }

        private void StartGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
                {
                    tick--;
                    if (tick == -1)
                    {
                        timer.Stop();
                        MessageBox.Show("Time Up");
                        ResetImages();
                        
                    }

                    var time = TimeSpan.FromSeconds(tick);
                    label1.Text = ":" + time.ToString("ss");

                    
                };

        }

        private void ResetImages()
        {
            foreach (var pic in PictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
                //pic.Image = Resources.Img0;
            }
        }

        private void HideImages()
        {
            foreach (var pic in PictureBoxes)
            {
                pic.Image = Resources.Img0;
            }
            clock();
        }

        private PictureBox GetFreeSlot()
        {
            int num;
            do
            {
                num = _random.Next(0, PictureBoxes.Count());
            }
            while (PictureBoxes[num].Tag != null);
            return PictureBoxes[num];
                

        }

        private void SetRandomImages()
        {
            foreach (var image in Images)
            {
                GetFreeSlot().Tag = image;
                GetFreeSlot().Tag = image;

            }

        }

        private void ClickImage(object sender,EventArgs e)
        {
            if (!_allowClick) return;
            var pic = (PictureBox)sender;
            if (_firstGuess == null)
            {
                _firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }
            pic.Image = (Image)pic.Tag;
            if(pic.Image == _firstGuess.Image && pic != _firstGuess)
            {
                pic.Visible = _firstGuess.Visible = false;
                {
                    _firstGuess = pic;
                }
                    HideImages();
                matchLeft--;
                label3.Text = "" + matchLeft;
            }
            else
            {
                _allowClick = false;
                _ClickTimer.Start();
            }
            _firstGuess = null;
            if (PictureBoxes.Any(p => p.Visible)) return;
            MessageBox.Show("You Win");
            ResetImages();
            
            

        }

        private void _ClickTimer_tick(object sender,EventArgs e)

        {
            HideImages();
            _allowClick = true;
            _ClickTimer.Stop();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newGame();
           

           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            foreach (var pic in PictureBoxes)
            {

                pic.Image = (Image)pic.Tag;
                

            }
            
            timer1.Start();
            clock();

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if(button4.Text == "Start")
            {
                button4.Text = "Pause";
                StartGameTimer();
                
            }
            else if(button4.Text == "Pause")
            {
                button4.Text = "Resume";
                timer.Stop();
            }
            else if(button4.Text == "Resume")
            {
                button4.Text = "Pause";
                timer.Start();
            }

        }
        private void newGame()
        {
            timer.Stop();
            button4.Text = "Start";
            tick = 60;
            matchLeft = 8;
            label3.Text = "8";
           
            foreach (var pb in this.Controls.OfType<PictureBox>())
            {
                pb.Image = Resources.Img0;
                pb.Visible = true;
            }
            clock();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            newGame();
        }
    }
}
