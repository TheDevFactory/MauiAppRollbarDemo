using Rollbar;

namespace RollbarMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            RollbarLocator.RollbarInstance.Info("MAUI App Loaded");
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            try
            {
                int value = 1 / int.Parse("0");
            }
            catch (System.Exception ex)
            {
                RollbarLocator.RollbarInstance.AsBlockingLogger(TimeSpan.FromSeconds(1)).Error(ex);
            }

        }
    }

}
