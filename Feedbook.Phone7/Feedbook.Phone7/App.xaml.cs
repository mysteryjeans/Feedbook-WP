﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using Feedbook.Phone7.Services;
using Feedbook.Phone7.Helper;
using Feedbook.Phone7.Model;

namespace Feedbook.Phone7
{
    public partial class App : Application
    {
        private DispatcherTimer synchronizerTimer = new DispatcherTimer();

        public static Dispatcher Dispatcher { get; private set; }

        // Easy access to the root frame
        public PhoneApplicationFrame RootFrame { get; private set; }

        // Constructor
        public App()
        {
            // Global handler for uncaught exceptions. 
            // Note that exceptions thrown by ApplicationBarItem.Click will not get caught here.
            UnhandledException += Application_UnhandledException;

            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            //Feedbook-specific initialization
            InitializeFeedbookApplication();
        }

        private void InitializeFeedbookApplication()
        {
            this.synchronizerTimer.Interval = SysConfig.SynchronizeInterval;
            this.synchronizerTimer.Tick += new EventHandler(synchronizerTimer_Tick);
            //Startup += new StartupEventHandler(App_Startup);
        }

        private void synchronizerTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (var channel in Storage.Channels)
                    SocialService.GetSocialService(null).AsyncUpdate(channel);        
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(
                    @"---- Message ------
{0}

-------StackTrace----
{1}
---------------------", ex.Message, ex.StackTrace));
            }
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            // Set the root visual to use the transitioning control.
            this.RootFrame.Template = (ControlTemplate)this.Resources["TransitioningFrame"];
            this.RootFrame.Source = Constants.Page.MAIN_PAGE;
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            Storage.Load();
            GlobalEventManager.Initialize();
            synchronizerTimer_Tick(sender, e);
            synchronizerTimer.Start();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            Storage.Load();
            GlobalEventManager.Initialize();
            synchronizerTimer.Start();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            synchronizerTimer.Stop();
            Storage.Save();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            synchronizerTimer.Stop();
            Storage.Save();
        }

        // Code to execute if a navigation fails
        void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger

                System.Diagnostics.Debugger.Break();
            }

            MessageBox.Show(string.Format(
@"---- Message ------
{0}

-------StackTrace----
{1}
---------------------", e.ExceptionObject.Message, e.ExceptionObject.StackTrace));
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new TransitionFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;

            // Acquiring Dispatcher Reference in UI/main thread
            Dispatcher = RootFrame.Dispatcher;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
