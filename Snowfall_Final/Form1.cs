namespace Snowfall_Final
{
    public partial class Form1 : Form
    {
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private Image backgroundImage;
        private Image snowflakeImage;
        private int snowflakeCount = 100; // ���������� �������� �� ���������


        public Form1()
        {
            InitializeComponent();
            LoadImages();
            this.DoubleBuffered = true; // �������� ������� ����������� ��� ������� ��������
            this.SizeChanged += OnSizeChanged;

            InitializeSnowflakes();
            StartAnimation();
        }

        /// <summary>
        /// ��������� ����������� ���� � ��������.
        /// </summary>
        private void LoadImages()
        {
            backgroundImage = Properties.Resources.background;
            var originalSnowflake = Properties.Resources.snowflake;
            snowflakeImage = new Bitmap(originalSnowflake, new Size(originalSnowflake.Width / 20, originalSnowflake.Height / 20)); // �������� ������
        }

        /// <summary>
        /// �������������� �������� �� ������ �������� ������� �����.
        /// ��������� ����� ��������, ���� �� ���������� ������ ������������.
        /// </summary>
        private void InitializeSnowflakes()
        {
            // ���������� �������� � ����������� �� �������� �����
            int currentCount = snowflakes.Count;

            for (int i = currentCount; i < snowflakeCount; i++) // ��������� ����� �������� ������ ��� �������������
            {
                snowflakes.Add(new Snowflake(this.ClientSize.Width, this.ClientSize.Height, snowflakeImage));
            }
        }

        /// <summary>
        /// ��������� �������� �������� � �������� ���������� ����������.
        /// </summary>
        private void StartAnimation()
        {
            timer.Interval = 30; // �������� ���������� � �������������
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        /// <summary>
        /// ������������ ������� �������. ��������� ������� �������� � �������������� �����.
        /// </summary>
        private void OnTimerTick(object sender, EventArgs e)
        {
            foreach (var snowflake in snowflakes)
            {
                snowflake.UpdatePosition();
            }
            Invalidate();
        }

        /// <summary>
        /// ������������ ��������� ������� �����. ��������������� ���������� �������� � ��������� �� �������.
        /// </summary>
        private void OnSizeChanged(object sender, EventArgs e)
        {
            // ��������� ���������� �������� �� ��������� ����� �������� �����
            snowflakeCount = (this.ClientSize.Width * this.ClientSize.Height) / 2000; // ������ ���������� �������� ������������ ������� �����

            // ��������� ������� ��� ���� ������������ ��������
            foreach (var snowflake in snowflakes)
            {
                snowflake.UpdateFormBounds(this.ClientSize.Width, this.ClientSize.Height);
            }

            // ��������� ����������� �������� ��� ����� �����������
            InitializeSnowflakes();
        }

        /// <summary>
        /// ������������ ��� � �������� �� �����.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // ������������ ���
            g.DrawImage(backgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            // ������������ ��������
            foreach (var snowflake in snowflakes)
            {
                int snowflakeWidth = snowflake.SnowflakeImage.Width / 2;  // ��������� ������ �������� � 2 ����
                int snowflakeHeight = snowflake.SnowflakeImage.Height / 2; // ��������� ������ �������� � 2 ����
                g.DrawImage(snowflake.SnowflakeImage, snowflake.Position.X, snowflake.Position.Y, snowflakeWidth, snowflakeHeight);
            }

            base.OnPaint(e);
        }
    }
}
