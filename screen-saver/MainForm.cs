namespace screen_saver
{
    public partial class MainForm : Form
    {
        private readonly List<Snowflake> snowflakes = [];
        private readonly Bitmap snowflakeImage = Properties.Resources.snowflake;
        private readonly Random random = new ();
        private readonly System.Windows.Forms.Timer timer = new ();
        private const int TimerInterval = 100;
        private const int MinSnowflakeSize = 10;
        private const int MaxSnowflakeSize = 30;
        private const int AmountOfSnowflakes = 100;

        public MainForm()
        {
            InitializeComponent();
            InitializeFormData();
        }

        /// <summary>
        /// Метод инициализации основных свойств формы.
        /// </summary>
        private void InitializeFormData()
        {
            FormBorderStyle = FormBorderStyle.None;
            Size = Screen.PrimaryScreen!.Bounds.Size;
            BackgroundImage = Properties.Resources.bg;

            timer.Interval = TimerInterval;
            timer.Tick += OnTick;

            KeyDown += (_, _) => { Close(); };
            Paint += OnPaint;
            Load += (_, _) =>
            {
                GenerateSnowflakes(AmountOfSnowflakes);
                timer.Start();
            };
        }

        /// <summary>
        /// Метод создает снежинку со случайными параметрами и добавляет ее в коллекцию.
        /// </summary>
        private void CreateSnowflake()
        {
            snowflakes.Add(new Snowflake
            {
                PositionX = random.Next(0, Width),
                PositionY = random.Next(-Height, 0), // Тк генерить будем сразу все снежинки, поставил генерацию положения y от -высоты экрана (т.е. выше него), до его начала сверху
                Size = random.Next(MinSnowflakeSize, MaxSnowflakeSize)
            });
        }
        
        private void GenerateSnowflakes(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CreateSnowflake();
            }
        }

        /// <summary>
        /// Метод перемещает снежнки по форме
        /// </summary>
        private void OnTick(object? sender, EventArgs e)
        {
            foreach (var snowflake in snowflakes)
            {
                snowflake.PositionY += snowflake.Size;
                if (snowflake.PositionY > Height)
                {
                    snowflake.PositionY = -snowflake.Size;
                    snowflake.PositionX = random.Next(0, Width);
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Метод отрисовывает снежинки на форме.
        /// </summary>
        private void OnPaint(object? sender, PaintEventArgs e)
        {
            foreach (var snowflake in snowflakes)
            {
                var rectangle = new Rectangle(
                    snowflake.PositionX,
                    snowflake.PositionY,
                    snowflake.Size,
                    snowflake.Size 
                );

                e.Graphics.DrawImage(snowflakeImage, rectangle);
            }
        }
    }
}
