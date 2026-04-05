using SP6KChannelManager.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace SP6KChannelManager.Services
{
    public class ErrorHandlerService
    {
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

        public static void AddError(Project project, string errorMessage, [CallerMemberName] string methodName = "")
        {
            project.ErrorHandler.HasError = true;

            if(project.ShowDebugInfo)
            {
                project.ErrorHandler.ErrorMessages.Add($"[{methodName}]: {errorMessage}");
            }
            else
            {
                project.ErrorHandler.ErrorMessages.Add(errorMessage);
            }
        }

        public static void ClearErrors(Project project)
        {
            project.ErrorHandler.HasError = false;
            project.ErrorHandler.ErrorMessages.Clear();
        }

        public static void ShowErrors(Project project)
        {
            if (project.ErrorHandler.HasError)
            {
                string message = string.Join(Environment.NewLine, project.ErrorHandler.ErrorMessages);
                MessageBox.Show(message, "Errors", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearErrors(project);
            }
        }
    }
}
