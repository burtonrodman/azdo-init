using System.Diagnostics;

namespace AzureDevOpsInit
{

    public static class ProcessUtilities
    {
        
        public static string ExecuteProcess(string cmd, params string[] args)
        {
            var info = new ProcessStartInfo
            {
                FileName = cmd,
                Arguments = string.Join(' ', args),
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var process = Process.Start(info)!;
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

    }
}