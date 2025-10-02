namespace screen_saver
{
    public partial class MainForm : Form
    {
        private readonly Bitmap SnowflakeImage = Properties.Resources.snowflake;

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
            KeyDown += (_, _) => { Close(); };
        }

    }
}
