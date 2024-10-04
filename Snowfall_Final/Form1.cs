namespace Snowfall_Final
{
    public partial class Form1 : Form
    {
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private Image backgroundImage;
        private Image snowflakeImage;
        private int snowflakeCount = 100; //  оличество снежинок по умолчанию


        public Form1()
        {
            InitializeComponent();
            LoadImages();
            this.DoubleBuffered = true; // ¬ключаем двойную буферизацию дл€ плавной анимации
            this.SizeChanged += OnSizeChanged;

            InitializeSnowflakes();
            StartAnimation();
        }

        /// <summary>
        /// «агружает изображени€ фона и снежинки.
        /// </summary>
        private void LoadImages()
        {
            backgroundImage = Properties.Resources.background;
            var originalSnowflake = Properties.Resources.snowflake;
            snowflakeImage = new Bitmap(originalSnowflake, new Size(originalSnowflake.Width / 20, originalSnowflake.Height / 20)); // ”меньшаю размер
        }

        /// <summary>
        /// »нициализирует снежинки на основе текущего размера формы.
        /// ƒобавл€ет новые снежинки, если их количество меньше необходимого.
        /// </summary>
        private void InitializeSnowflakes()
        {
            // √енерируем снежинки в зависимости от размеров формы
            int currentCount = snowflakes.Count;

            for (int i = currentCount; i < snowflakeCount; i++) // ƒобавл€ем новые снежинки только при необходимости
            {
                snowflakes.Add(new Snowflake(this.ClientSize.Width, this.ClientSize.Height, snowflakeImage));
            }
        }

        /// <summary>
        /// «апускает анимацию снежинок с заданным интервалом обновлени€.
        /// </summary>
        private void StartAnimation()
        {
            timer.Interval = 30; // »нтервал обновлени€ в миллисекундах
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        /// <summary>
        /// ќбрабатывает событие таймера. ќбновл€ет позицию снежинок и перерисовывает форму.
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
        /// ќбрабатывает изменение размера формы. ѕерерасчитывает количество снежинок и обновл€ет их границы.
        /// </summary>
        private void OnSizeChanged(object sender, EventArgs e)
        {
            // ќбновл€ем количество снежинок на основании новых размеров формы
            snowflakeCount = (this.ClientSize.Width * this.ClientSize.Height) / 2000; // «адаем количество снежинок относительно площади формы

            // ќбновл€ем границы дл€ всех существующих снежинок
            foreach (var snowflake in snowflakes)
            {
                snowflake.UpdateFormBounds(this.ClientSize.Width, this.ClientSize.Height);
            }

            // ƒобавл€ем недостающие снежинки дл€ новых пространств
            InitializeSnowflakes();
        }

        /// <summary>
        /// ќтрисовывает фон и снежинки на форме.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // ќтрисовываем фон
            g.DrawImage(backgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            // ќтрисовываем снежинки
            foreach (var snowflake in snowflakes)
            {
                int snowflakeWidth = snowflake.SnowflakeImage.Width / 2;  // ”меньшаем ширину снежинки в 2 раза
                int snowflakeHeight = snowflake.SnowflakeImage.Height / 2; // ”меньшаем высоту снежинки в 2 раза
                g.DrawImage(snowflake.SnowflakeImage, snowflake.Position.X, snowflake.Position.Y, snowflakeWidth, snowflakeHeight);
            }

            base.OnPaint(e);
        }
    }
}
