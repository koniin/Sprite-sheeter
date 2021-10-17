using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpriteSheeter.Lib {
    public class CommandFileParser {
        private readonly string[] AVAILABLE_COMMANDS = new string[6] {
            "combinefolder", "combinesub", "split", "bw", "scale", "filetype"
        };

        public (string, string[]) Parse(string[] args) {
            if (args.Length > 1) {
                return ($"Wrong number of arguments got {args.Length} expected 1.", Array.Empty<string>());
            }

            if(!File.Exists(args[0])) {
                return ($"Config file @ {args[0]} does not exist.", Array.Empty<string>());
            }

            string[] lines = File.ReadAllLines(args[0]);
            if (lines.Length == 0) {
                return ($"Config file @ {args[0]} is empty.", Array.Empty<string>());
            }

            return ParseCommands(lines);
        }

        private (string, string[]) ParseCommands(string[] arguments) {
            int rowCounter = 0;
            var enumerator = arguments.GetEnumerator();
            List<string> commandResult = new List<string>();
            string currentCommand = "";
            while (enumerator.MoveNext()) {
                var row = enumerator.Current as string;
                
                if(AVAILABLE_COMMANDS.Contains(row.ToLowerInvariant())) {
                    rowCounter++;
                    if(!string.IsNullOrWhiteSpace(currentCommand)) {
                        commandResult.Add(currentCommand);
                    }
                    currentCommand = row;
                } else {
                    currentCommand += " " + row;
                }
            }
            commandResult.Add(currentCommand);
            return ("Succes.", commandResult.ToArray());
        }
    }
}
