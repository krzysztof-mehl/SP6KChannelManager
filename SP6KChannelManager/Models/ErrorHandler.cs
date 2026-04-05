using System.Runtime.CompilerServices;
using System.Windows;

namespace SP6KChannelManager.Models
{
    public class ErrorHandler
    {
        public bool HasError { get; set; } = false;

        private List<string> ErrorMessages { get; set; } = [];

        public static void NotImplemented([CallerMemberName] string methodName = "")
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{methodName}: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void AddError(ErrorHandler errorHandler, string errorMessage, bool showMethodName = false, [CallerMemberName] string methodName = "")
        {
            errorHandler.HasError = true;

            if (showMethodName)
            {
                errorHandler.ErrorMessages.Add($"[{methodName}]: {errorMessage}");
            }
            else
            {
                errorHandler.ErrorMessages.Add(errorMessage);
            }
        }

        public static void ClearErrors(ErrorHandler errorHandler)
        {
            errorHandler.HasError = false;
            errorHandler.ErrorMessages.Clear();
        }

        public static void ShowErrors(ErrorHandler errorHandler, bool clearErrors = true)
        {
            if (errorHandler.HasError)
            {
                string message = string.Join(Environment.NewLine, errorHandler.ErrorMessages);

                MessageBox.Show(message, "Errors", MessageBoxButton.OK, MessageBoxImage.Error);

                if (clearErrors) ClearErrors(errorHandler);
            }
        }
    }
}
