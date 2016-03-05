using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class License
    {
        public static string ConvertStringToSecureCode(string Input1)
        {
            MD5 Secu1 = MD5.Create();
            byte[] data1 = Secu1.ComputeHash(Encoding.Default.GetBytes(Input1));
            StringBuilder sbd = new StringBuilder();
            for (int i = 0; i <= data1.Length - 1; i++)
            {
                sbd.Append(data1[i].ToString("x2"));
            }
            return sbd.ToString();
        }

        public static string GetRequestLicenseCode()
        {
            string Hd1 = HardDiskSeriesNumber();
            string RequestActivateCode = ConvertStringToSecureCode(Hd1);
            string Code2 = RequestActivateCode.Substring(24).ToUpper();
            return Code2;
        }


        private static string HardDiskSeriesNumber()
        {
            string output = ExecuteCommandSync("vol");
            string aa = output.Split('.')[output.Split('.').Length - 1];
            string bb = aa.Split(' ')[aa.Split(' ').Length - 1];
            return bb.ToString().ToUpper();
        }

        public static string ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                return result;
            }
            catch (Exception)
            {
                // Log the exception
                return null;
            }
        }
    }
}
