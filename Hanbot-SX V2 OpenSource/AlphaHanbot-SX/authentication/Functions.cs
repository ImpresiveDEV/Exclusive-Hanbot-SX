using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AlphaHanbot_SX.authentication
{
    internal class Functions
    {
        private static readonly Random rng = new Random();

        internal static string CreateString(int stringLength)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789=";
            var chars = new char[stringLength];

            for (var i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rng.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string Scramble(string Text)
        {
            var chars1 = Text.ToArray();
            var r1 = new Random(259);

            for (var i = 0; i < chars1.Length; i++)
            {
                var randomIndex = r1.Next(0, chars1.Length);
                var temp = chars1[randomIndex];
                chars1[randomIndex] = chars1[i];
                chars1[i] = temp;
            }

            return new string(chars1);
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("<SERVER>"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool RegisterUser(string User, string Pass, string Hwid)
        {
            HttpWebResponse Response = null;

            try
            {
                var Request = WebRequest.Create(Globals.RegisterPHP);

                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";

                var postData = "username=" + User + "&password=" + Pass;
                var byteArray = Encoding.UTF8.GetBytes(postData);
                Request.ContentLength = byteArray.Length;

                var dataSend = Request.GetRequestStream();
                dataSend.Write(byteArray, 0, byteArray.Length);
                dataSend.Close();
                Response = (HttpWebResponse)Request.GetResponse();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = Response.GetResponseStream();
                    var Reader = new StreamReader(responseStream);
                    Globals.RegisterStatus = Reader.ReadToEnd();
                    Reader.Close();
                }
            }
            catch (WebException Error)
            {
                Console.WriteLine("Error: " + Error.Message, "Error!");
            }
            finally
            {
                if (Response != null)
                {
                    Response.Close();
                }

            }
            return Globals.FinishedRegister = true;
        }

        public static bool CheckUser(string Username, string Password)
        {
            Globals.ReadLine = 0;
            HttpWebResponse Response = null;

            try
            {
                var Request = WebRequest.Create(Globals.LoginPHP);
                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";
                var postData = "username=" + Username + "&password=" + Password;
                var byteArray = Encoding.UTF8.GetBytes(postData);
                Request.ContentLength = byteArray.Length;

                var dataSend = Request.GetRequestStream();
                dataSend.Write(byteArray, 0, byteArray.Length);
                dataSend.Close();
                Response = (HttpWebResponse)Request.GetResponse();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = Response.GetResponseStream();
                    var Reader = new StreamReader(responseStream);
                    Globals.CheckStatus = Reader.ReadToEnd();
                    Reader.Close();
                }
            }
            catch (WebException Error)
            {
                Console.WriteLine("Error: " + Error.Message, "Error!");
            }
            finally
            {
                if (Response != null)
                {
                    Response.Close();
                }
            }
            return Globals.FinishedChecking = true;
        }

        public static void GetGameInformations(string GameName)
        {
            Globals.ReadLine = 0;
            string Informations = null;
            HttpWebResponse Response = null;

            try
            {
                var Request = WebRequest.Create(Globals.InfosPHP);

                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";

                var postData = "game=" + GameName;
                var byteArray = Encoding.UTF8.GetBytes(postData);
                Request.ContentLength = byteArray.Length;
                var dataSend = Request.GetRequestStream();
                dataSend.Write(byteArray, 0, byteArray.Length);
                dataSend.Close();
                Response = (HttpWebResponse)Request.GetResponse();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = Response.GetResponseStream();
                    var Reader = new StreamReader(responseStream);
                    Informations = Reader.ReadToEnd();
                    Reader.Close();
                }
            }
            catch (WebException Error)
            {
                Console.WriteLine("Error: " + Error.Message, "Error!");
            }
            finally
            {
                if (Response != null)
                {
                    Response.Close();
                }
            }

            if (Informations.Contains("Code: #"))
            {
                Console.WriteLine("Settings File not set up!");
                return;
            }

            if (Informations.Contains("Code: #"))
            {
                Console.WriteLine("Error checking Game Data");
                return;
            }

            if (Informations.Contains("Code: #"))
            {
                Console.WriteLine("Game does not exist");
                return;
            }

            if (Informations.Contains("Error"))
            {
                Console.WriteLine(Informations);
                return;
            }

            foreach (var Info in Informations.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                Globals.ReadLine++;

                if (Globals.ReadLine == 1)
                {
                    Globals.GameStatus = Info;
                }

                if (Globals.ReadLine == 2)
                {
                    Globals.GameVersion = Info;
                }

                if (Globals.ReadLine == 3)
                {
                    Globals.LastDetection = Info;
                    Globals.ReadLine++;
                }
            }
        }

        public static void GetSubscriptionInformations(string Username)
        {
            Globals.ReadLine = 0;

            string Informations = null;
            HttpWebResponse Response = null;

            try
            {
                var Request = WebRequest.Create(Globals.InfosPHP);

                Request.Method = "POST";
                Request.ContentType = "application/x-www-form-urlencoded";

                var postData = "username=" + Username;
                var byteArray = Encoding.UTF8.GetBytes(postData);
                Request.ContentLength = byteArray.Length;
                var dataSend = Request.GetRequestStream();
                dataSend.Write(byteArray, 0, byteArray.Length);
                dataSend.Close();
                Response = (HttpWebResponse)Request.GetResponse();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = Response.GetResponseStream();
                    var Reader = new StreamReader(responseStream);
                    Informations = Reader.ReadToEnd();
                    Reader.Close();
                }
            }
            catch (WebException Error)
            {
                Console.WriteLine("Error: " + Error.Message, "Error!");
            }
            finally
            {
                if (Response != null)
                {
                    Response.Close();
                }
            }

            if (Informations.Contains("Code: #"))
            {
                Console.Write("Settings File not set up!");
                return;
            }

            if (Informations.Contains("Code: #"))
            {
                Console.WriteLine("Error checking User Data");
                return;
            }

            if (Informations.Contains("Code: #"))
            {
                Console.WriteLine("User does not exist");
                return;
            }

            if (Informations.Contains("Error"))
            {
                Console.WriteLine(Informations);
                return;
            }

            foreach (var Info in Informations.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {

                Globals.ReadLine++;

                if (Globals.ReadLine == 1)
                {
                    DateTime today = DateTime.Today;
                    DateTime SubLeft = DateTime.Parse(Info);
                    Globals.SubscriptionLeft = (SubLeft - today).Days.ToString();
                }

                if (Globals.ReadLine == 2)
                {
                    Globals.SubscriptionType = Info;
                    Globals.ReadLine++;
                }
            }
        }
    }
}
