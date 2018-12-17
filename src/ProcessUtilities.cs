using System.Diagnostics;

namespace azdo_init
{

    public static class ProcessUtilities
    {
        
        public static string ExecuteProcess(string cmd, params string[] args)
        {
            var info = new ProcessStartInfo();
            info.FileName = cmd;
            info.Arguments = string.Join(' ', args);
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;

            var process = Process.Start(info);
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

    }
}