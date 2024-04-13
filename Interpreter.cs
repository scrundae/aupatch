using System.Net;
using System.Security;
using System.IO;

namespace Aupatch {
    namespace Scripting {
        public class Interpreter {
            public static string ansval;
            public static void Interpret(string[] script) {
                foreach (string line in script) {
                    var parsed = ParseCommandLine(line);
                    if (parsed.Length == 0)
                        continue;

                    if (parsed[0] == "cast") {
                        parsed[1] = parsed[1].Replace("%ANSVAL", ansval);
                        Console.WriteLine(parsed[1]);
                    }
                    else if (parsed[0] == "install") {
                        if (parsed.Length >= 4 && parsed[2] == "at") {
                            try {
                                string directoryPath = Path.GetDirectoryName(parsed[3].Replace("%ANSVAL", ansval));
                                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                using (WebClient client = new WebClient()) {
                                    client.DownloadFile(parsed[1], parsed[3].Replace("%ANSVAL", ansval));
                                }
                            }
                            catch(WebException ex) {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else if (parsed[0] == "ask") {
                        Console.Write(parsed[1]);
                        ansval = Console.ReadLine();
                    }
                }
    Console.WriteLine("\n--- AUPATCH INACTIVE ---\n");
}


            static string[] ParseCommandLine(string commandLine)
            {
                List<string> tokens = new List<string>();
                bool inQuotes = false;
                int tokenStart = 0;

                for (int i = 0; i < commandLine.Length; i++)
                {
                    char c = commandLine[i];

                    if (c == '"')
                    {
                        inQuotes = !inQuotes;
                    }
                    else if (char.IsWhiteSpace(c) && !inQuotes)
                    {
                        if (tokenStart < i)
                        {
                            string token = commandLine.Substring(tokenStart, i - tokenStart);
                            // Remove quotes from the token
                            token = RemoveQuotes(token);
                            tokens.Add(token);
                        }
                        tokenStart = i + 1;
                    }
                }

                if (tokenStart < commandLine.Length)
                {
                    string lastToken = commandLine.Substring(tokenStart);
                    // Remove quotes from the last token
                    lastToken = RemoveQuotes(lastToken);
                    tokens.Add(lastToken);
                }

                return tokens.ToArray();
            }

            static string RemoveQuotes(string input)
            {
                return input.Trim('"', '\'');
            }
        }
    }
}

