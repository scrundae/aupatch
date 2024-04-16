/////////////////////////////////////////////////////////////////////////
// ironide: A cross-platform package manager.                          //
// Created by scrundaegames: @scrundae on YouTube and Twitter (sadly)  //
// My website is: https://scrundae.github.io                           //
// Thanks!                                                             //
/////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using ironide.Scripting;

namespace ironide {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("\n--- ironide ACTIVE ---\n");
            //Console.WriteLine(args[0]);
            if (args.Length == 0) {
                Console.WriteLine(@"IRONIDE PATCHER
Created by scrundaegames: @scrundae on YouTube and Twitter (sadly)

ironide allows you to download and install different applications.

Usage: ironide [<mode>] [<options>]

These are the available modes:
    MODE         DESC
    --install    Allows you to run ironide install scripts
    --curver     Shows the current ironide and OS version.
    --cat        Shows the install script that will be run.

You have not provided any commands. The session will now end. Good bye!

--- ironide INACTIVE ---
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
                else if (args[0] == "--curver") {
                    Console.WriteLine("-= ironide by Harry \"scrundae\" Collins =-");
                    Console.WriteLine("ironide version: 1.0");
                    Console.WriteLine($"OS version: {System.Environment.OSVersion}");
                    Console.WriteLine("\n--- ironide INACTIVE ---\n");
                }
                else if (args[0] == "--cat") {
                    try {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"[{args[1]}]");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        WebClient client = new WebClient();
                        client.DownloadFile(new Uri(args[1]), "AU-TEMP");
                        string[] data = File.ReadAllLines("AU-TEMP");
                        foreach (string line in data) {
                            Console.WriteLine(line);
                        }
                        Console.WriteLine("\n--- ironide INACTIVE ---\n");
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
