using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving.Network
{
    public class FTPRequest : IFileSyncer
    {
        const string FTP_URL = "ftp://localhost:21/";
        const string USERNAME = "Todos";
        const string PASSWORD = "Todos";
        public async Task UploadFile(string[] filenames, string game_path, Action successCallback, Action failureCallback)
        {
            foreach (var fullfilename in filenames)
            {
                string filename = Path.GetFileName(fullfilename);
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_URL + filename);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(USERNAME, PASSWORD);
                request.Timeout = 50000;

                Console.WriteLine("Starting uploading file process");
                Console.WriteLine("Opening file...");
                // Copy the contents of the file to the request stream.
                using (FileStream fileStream = File.Open(game_path + @"\" + filename, FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLine("Requiesting file uploading permissions...");
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        Console.WriteLine("Copying file to server...");
                        await fileStream.CopyToAsync(requestStream);
                        Console.WriteLine("Upload finished");
                        fileStream.Close();
                        
                        /*using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                        }*/
                    }
                }
            }
        }

        public async Task DownloadFile(string[] filenames, string game_path, Action successCallback, Action failureCallback)
        {
            foreach (var fullfilename in filenames)
            {
                string filename = Path.GetFileName(fullfilename);

                string inputfilepath = game_path + filename;
                string ftphost = FTP_URL;
                string ftpfilepath = filename;

                string ftpfullpath = ftphost + ftpfilepath;

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(USERNAME, PASSWORD);
                    Console.WriteLine("Downloading File: " + ftpfullpath);
                    request.DownloadDataCompleted +=  (sender, e) =>
                    {
                        Console.WriteLine("Download Completed");
                        ByteRequestAsync(sender, e, inputfilepath, successCallback, failureCallback);
                    };
                    request.DownloadDataAsync(new Uri(ftpfullpath));  
                }

            }

        }

        static void ByteRequestAsync(object sender, DownloadDataCompletedEventArgs e, string inputfilepath, Action successCallback, Action failureCallback)
        {
            System.Threading.AutoResetEvent waiter = (System.Threading.AutoResetEvent)e.UserState;

            try
            {
                // If the request was not canceled and did not throw
                // an exception, display the resource.
                if (!e.Cancelled && e.Error == null)
                {
                    byte[] data = (byte[])e.Result;
                    string textData = System.Text.Encoding.UTF8.GetString(data);
                    Console.WriteLine(textData);
                    using (FileStream file = File.Create(inputfilepath))
                    {
                        file.Write(data, 0, data.Length);
                        file.Close();
                        Console.WriteLine("Saved file " + inputfilepath);
                        successCallback?.Invoke();
                    }
                }
                else
                {
                    Console.WriteLine("Error downloading file: " + e.Error);
                    failureCallback?.Invoke();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading file: " + ex.Message);
                failureCallback?.Invoke();
            }
            finally
            {
                // Let the main application thread resume.
                //waiter.Set();
            }
        }
    }
}