using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Interface.Windows
{
    public partial class ConnectingWindow : Window
    {
        private Stopwatch _stopwatch = new Stopwatch();

        public ConnectingWindow()
        {
            InitializeComponent();
            Closing += (_, __) => Application.Current.Shutdown();
            ButtonRetry.Click += async (_, __) => await RetryButtonClickAsync();
            ValidateConnectionAsync();
        }

        private async Task RetryButtonClickAsync()
        {
            HideErrorUI();
            await ValidateConnectionAsync();
        }

        private async Task ValidateConnectionAsync()
        {
            StartValidationTimer();
            try
            {
                var (isServerConnected, isLocalConnected) = await CheckDatabaseConnectionAsync();

                if (!isServerConnected || !isLocalConnected)
                {
                    ShowError(isServerConnected, isLocalConnected);
                }
                else
                {
                    OpenAuthorizationWindow();
                }
            }
            catch (Exception ex)
            {
                ShowErrorUI();
                TextBlockError.Text = "Connection error: " + ex.Message;
            }
            finally
            {
                StopValidationTimer();
            }
        }

        private void StartValidationTimer()
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            var dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += (sender, e) => TextBlockTimer.Text = $"{_stopwatch.Elapsed.TotalSeconds:F0} seconds";
            dispatcherTimer.Start();
        }

        private void StopValidationTimer()
        {
            _stopwatch.Stop();
        }

        private async Task<(bool, bool)> CheckDatabaseConnectionAsync()
        {
            bool isServerConnected = await Logic.DatabaseConnection.IsServerDatabasesRequestExistAsync();
            bool isLocalConnected = await Logic.DatabaseConnection.IsLocalDatabasesRequestExistAsync();

            return (isServerConnected, isLocalConnected);
        }

        private void ShowError(bool isServerConnected, bool isLocalConnected)
        {
            ShowErrorUI();

            string errorMessage = "";

            if (!isServerConnected)
                errorMessage = "Server Database connection does not exist";

            if (!isLocalConnected)
            {
                if (!string.IsNullOrEmpty(errorMessage))
                    errorMessage += "\n";
                errorMessage += "Local Database connection does not exist";
            }

            TextBlockError.Text = errorMessage;
        }

        private void HideErrorUI()
        {
            TextBlockTimer.Text = "";
            TextBlockError.Visibility = Visibility.Hidden;
            ButtonRetry.Visibility = Visibility.Hidden;
        }

        private void ShowErrorUI()
        {
            TextBlockError.Visibility = Visibility.Visible;
            ButtonRetry.Visibility = Visibility.Visible;
        }

        private void OpenAuthorizationWindow()
        {
            Hide();
            var authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
        }
    }
}