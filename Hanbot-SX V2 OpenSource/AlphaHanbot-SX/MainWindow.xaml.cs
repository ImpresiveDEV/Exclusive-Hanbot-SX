using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Windows;
using System.Drawing;
using Console = Colorful.Console;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AlphaHanbot_SX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime Timestamp = DateTime.Now;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern void mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 dwData, UIntPtr dwExtraInfo);

        private const int WmSyscommand = 0x0112;
        private const int ScMonitorpower = 0xF170;
        private const int MonitorShutoff = 2;
        private const int MouseeventfMove = 0x0001;

        public static void MonitorOff(IntPtr handle)
        {
            SendMessage(handle, WmSyscommand, (IntPtr)ScMonitorpower, (IntPtr)MonitorShutoff);
        }

        private static void MonitorOn()
        {
            mouse_event(MouseeventfMove, 0, 1, 0, UIntPtr.Zero);
            Thread.Sleep(40);
            mouse_event(MouseeventfMove, 0, -1, 0, UIntPtr.Zero);
        }


        public MainWindow()
        {
            InitializeComponent();
            Console.Title = "HANBOT-SX";

            int DA = 120;
            int V = 10;
            int ID = 40;
            for (int i = 0; i < 1; i++)
            {
                Console.WriteAscii("hanbot exclusive", Color.FromArgb(DA, V, ID));

                DA -= 0;
                V -= 0;
            }



            //SERVER CHECK
            if (!authentication.Functions.CheckForInternetConnection())
                {
                Environment.Exit(0);
                }

            //AUTHENTICATION_ALPHA
            string username, password = string.Empty;

            //UNIQUEKEY LOCK + AUTHENTICATION PLUGIN
            string whitelist = new WebClient() { Proxy = null }.DownloadString("<source>");
            //string blacklist = new WebClient() { Proxy = null }.DownloadString("<source>");

            if (whitelist.Contains(authentication.FingerPrint.ValueKey()))
            {
                Console.WriteLine(authentication.FingerPrint.ValueKey(), ColorTranslator.FromHtml("#cc0099"));
            }
            else
            {
                if (!whitelist.Contains(authentication.FingerPrint.ValueKey()))
                {
                    Console.WriteLine(authentication.FingerPrint.ValueKey());
                    Console.WriteLine("[" + Timestamp + "]" + " unauthorized machine!", ColorTranslator.FromHtml("#cc0000"));
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            Console.Write("Your username: ");
            username = Console.ReadLine();

            if (username.Length == 0)
            {
                Console.WriteLine("[" + Timestamp + "]" + " username cannot be empty", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            Console.Write("Your password: ");
            password = Console.ReadLine();

            if (password.Length == 0)
            {
                Console.WriteLine("[" + Timestamp + "]" + " password cannot be empty!", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            authentication.Globals.Username = username;
            authentication.Globals.Password = password;


            //START CHECK FUNCTION
            CheckUser();
        }

        public void CheckUser()
        {
            authentication.Globals.CheckStatus = null;
            authentication.Globals.FinishedChecking = false;

            // Create new Background Worker
            var BW = new BackgroundWorker();
            BW.DoWork += delegate
            {
                // Encrypt Scrambled Username, Password & HWID
                var SecureUsername = authentication.Globals.Username;
                var SecurePassword = authentication.Globals.Password;

                // Start Check Function
                authentication.Functions.CheckUser(SecureUsername, SecurePassword);

                // Wait for Check Function to Finish
                while (!authentication.Globals.FinishedChecking)
                {
                    // Let the Thread Sleep while it's waiting
                    Thread.Sleep(500);
                }
            };

            // Update Status Text after it is Complete
            BW.RunWorkerCompleted += delegate
            {
                // Return if Status is null
                if (authentication.Globals.CheckStatus == null)
                {
                    // Change Status Label Text
                    Console.WriteLine("Failed getting Status", ColorTranslator.FromHtml("#cc0000"));
                    // Return Function to default state
                    return;
                }

                // Read/Translate Website Output
                ReadCheckUserCode();
            };

            // Run Background Worker
            BW.RunWorkerAsync();
        }


        //PHP RETURN
        private void ReadCheckUserCode()
        {
            

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Could not Connect to our server.", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Settings File not set up!", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Error while checking Data", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " User does not exist in our service.", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Account is currently locked. Contact with IMPRESIVE!", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Wrong Password", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Wrong Password", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " Wrong Password.", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " You write to many wrong password. Account is locked.", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Equals("Code: #"))
            {
                Console.WriteLine("[" + Timestamp + "]" + " You have not subscribtion in our server.", ColorTranslator.FromHtml("#cc0000"));
                return;
            }

            if (authentication.Globals.CheckStatus.Contains("Code: #"))
            {
                Console.WriteLine("["+Timestamp+"] " + " Succesfully logged into AUTH-SX!", ColorTranslator.FromHtml("#00cc66"));
          

                Console.Write("User: ");
                Console.WriteLine(authentication.Globals.Username, ColorTranslator.FromHtml("#cc9900"));

                // Get Users Subscription Informations
                GetSubscriptionInfo();
            }
        }

        private void GetSubscriptionInfo()
        {

            //USERNAME SET
            var username = authentication.Globals.Username;


            //SCRAPPING USERNAME INFORMATION
            authentication.Functions.GetSubscriptionInformations(username);
            Console.Write("Subscription Expires in: ", ColorTranslator.FromHtml("#ffffff"));
            Console.WriteLine(authentication.Globals.SubscriptionLeft + " Days", ColorTranslator.FromHtml("#cc9900"));
            Console.Write("Account type: ", ColorTranslator.FromHtml("#ffffff"));

            if (authentication.Globals.SubscriptionType.Contains("Administrator"))
            {
                
                Console.WriteLine(authentication.Globals.SubscriptionType, ColorTranslator.FromHtml("#ff0000"));
            }

            if (authentication.Globals.SubscriptionType.Contains("Tester"))
            {

                Console.WriteLine(authentication.Globals.SubscriptionType, ColorTranslator.FromHtml("#99cc00"));
            }

            if (authentication.Globals.SubscriptionType.Contains("Subscriber"))
            {

                Console.WriteLine(authentication.Globals.SubscriptionType, ColorTranslator.FromHtml("#ffff00"));
            }


            //SELECTOR OPTION
            Console.WriteLine("[1] Hanbot-SX authentication", ColorTranslator.FromHtml("#ace600"));
            Console.WriteLine("[2] Exit Application", ColorTranslator.FromHtml("#ace600"));

            string selector = Console.ReadLine();
            if (selector == "1")
            {
                Console.Clear();
                if (File.Exists("hanbot++.exe"))
                {
                    try
                    {
                        System.Diagnostics.Process.Start("hanbot++.exe");
                    }
                    catch (Exception)
                    {
                        //THROW EXCEPTION
                    }
                }
                if (!File.Exists("hanbot++.exe"))
                {
                    try
                    {
                        Console.WriteLine("[" + Timestamp + "]" + " hanbot++ executable is missing!", ColorTranslator.FromHtml("#cc0000"));
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    catch (Exception)
                    {
                        //THROW EXCEPTION
                    }
                }

                Process[] pname = Process.GetProcessesByName("hanbot++");
                if (pname.Length > 0)
                {
                    Console.WriteLine("[" + Timestamp + "]" + "hooked hanbot++", ColorTranslator.FromHtml("#0099cc"));
                }
                else
                {
                    Console.WriteLine("[" + Timestamp + "]" + "cannot hook hanbot++ module!", ColorTranslator.FromHtml("#cc0000"));
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                Thread.Sleep(1000);
                Console.WriteLine("[" + Timestamp + "]" + "running authentication hanbot-sx with internal memory!", ColorTranslator.FromHtml("#0099cc"));
                Outbuilt.Protection.FreezeMouse();
                Thread.Sleep(500);
                //CHECK FOR DRIVER
                if (File.Exists(@"C:\Windows\driver.exe"))
                {
                    try
                    {
                        File.Delete(@"C:\Windows\driver.exe");
                    }
                    catch (Exception)
                    {
                        //THROW EXCEPTION
                    }
                }

                //ATTACH_FILE_TO_PROJECT + RUN
                byte[] exeBytes = Properties.Resources.driver;
                string exeToRun = @"C:\Windows\driver.exe";

                using (FileStream exeFile = new FileStream(exeToRun, FileMode.CreateNew))
                {
                    var form = new Form();
                    MonitorOff(form.Handle);
                    Thread.Sleep(1000);
                    MonitorOn();
                    Thread.Sleep(1000);

                    exeFile.Write(exeBytes, 0, exeBytes.Length);

                }

                using (Process exeProcess = Process.Start(exeToRun))
                {
                    exeProcess.WaitForExit();
                    Thread.Sleep(300);

                    //CLEAN_DRIVER
                    File.Delete(@"C:\Windows\driver.exe");

                    Outbuilt.Protection.ReleaseMouse();

                    Console.Write("[" + Timestamp + "]" + "hanbot-sx authentication complete.", ColorTranslator.FromHtml("#0099cc"));
                }
            }
            if (selector == "2")
            {
                Environment.Exit(0);
            }
        }
    }
}
