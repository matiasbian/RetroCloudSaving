using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;


public class FTPRequest
{
    const string FTP_URL = "ftp://ftp.dlptest.com/";
    public static async Task Main(string filename, string game_path)
    {
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_URL + filename);
        request.Method = WebRequestMethods.Ftp.UploadFile;

        // This example assumes the FTP site uses anonymous logon.
        request.Credentials = new NetworkCredential("dlpuser", "rNrKYTX9g7z3RgJRmxWuGHbeu");

        // Copy the contents of the file to the request stream.
        using (FileStream fileStream = File.Open(game_path + filename, FileMode.Open, FileAccess.Read))
        {
            using (Stream requestStream = request.GetRequestStream())
            {
                await fileStream.CopyToAsync(requestStream);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
        }
    }
}
