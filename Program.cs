/////////////////////////////////////////////////////////////////////////
// aupatch: A cross-platform package manager.                          //
// Created by scrundaegames: @scrundae on YouTube and Twitter (sadly)  //
// My website is: https://scrundae.github.io                           //
// Thanks!                                                             //
/////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using Aupatch.Scripting;

namespace Aupatch {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("\n--- AUPATCH ACTIVE ---\n");
            //Console.WriteLine(args[0]);
            if (args.Length == 0) {
                Console.WriteLine(@"Auto (or Australian) Patcher
Created by scrundaegames: @scrundae on YouTube and Twitter (sadly)

aupatch allows you to download and install different applications.

Usage: aupatch [<mode>] [<options>]

These are the available modes:
    MODE         DESC
    --install    Allows you to run aupatch install scripts
    
You have not provided any commands. The session will now end. Good bye!

--- AUPATCH INACTIVE ---
");
            }
            else {
                if (args[0] == "--install") {
                    try {
                        WebClient client = new WebClient();
                        client.DownloadFile(new Uri(args[1]), "AU-TEMP");
                        string[] data = File.ReadAllLines("AU-TEMP");
                        Interpreter.Interpret(data);
                    }
                    catch (WebException ex) {
                        Console.WriteLine($"Error getting: {ex.Message}");
                    }
                    catch (IOException ex) {
                        Console.WriteLine($"Error reading or writing temporary file: {ex.Message}");
                    }
                    catch (Exception ex) {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }
                else {
                    Console.WriteLine("Invalid arguments.");
                }
            }
        }
    }
}
