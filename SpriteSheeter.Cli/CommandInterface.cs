using System;
using System.Text;
using System.Collections.Generic;

namespace Commands {
    public abstract class CommandInterface {
        // individual command info
        protected struct Command {
            public string name;
            public string help;
            public int argCount;
            public Command(string name, string help, int arg_count) {
                this.name = name;
                this.help = help;
                argCount = arg_count;
            }
        }

        // list of user commands
        List<Command> _commands = new List<Command>();

        // function, arguments, and the result
        string func_;
        List<string> args_ = new List<string>();
        string result_;

        // report wrong number of arguments
        string err_num_args(int expected, int got) {
            string msg = $"Error: expected {expected} argument";
            if (expected != 1) {
                msg += "s";
            }
            msg += $"; got {got}";
            return msg;
        }

        // split the text into function and arguments
        protected void parse(string text) {
            func_ = "";
            args_.Clear();

            StringBuilder word = new StringBuilder();
            foreach (char c in text) {
                if (c == ' ') {
                    if (word.Length != 0) {
                        args_.Add(word.ToString());
                        word.Clear();
                    }
                } else {
                    word.Append(c);
                }
            }

            if (word.Length != 0) {
                args_.Add(word.ToString());
            }

            if (args_.Count != 0) {
                func_ = args_[0];
                args_.RemoveAt(0);
            }
        }

        // display user's commands
        protected string help(string[] args) {
            // find longest func name
            int longest = 0;
            foreach (var c in _commands) {
                longest = Math.Max(longest, c.name.Length);
            }
            longest += 2;

            // record each func name padded with space, and then the help text
            string str = "";
            foreach (var c in _commands) {
                str += c.name;
                for (int i = 0; i < longest - c.name.Length; i++) {
                    str += ' ';
                }
                str += c.help + Environment.NewLine;
            }
            return str.Remove(str.Length - 1);
        }

        public void register_command(Func<string[], string> func, int arg_count, string name, string help) {
            _commands.Add(new Command(name, help, arg_count));
            if (func_ == name) {
                if (args_.Count != arg_count) {
                    result_ = err_num_args(arg_count, args_.Count);
                    return;
                }

                result_ = func(args_.ToArray());
            }
        }

        // evaluate the user's input
        public string eval(string text) {
            parse(text);
            result_ = "";
            _commands.Clear();

            // the result will be overwritten if the command is recognized
            if (!string.IsNullOrEmpty(func_)) {
                result_ = "Unrecognized command: " + func_;
                register_commands();
                register_command(help, 0, "help", "Show this help");
            }

            return result_;
        }

        public void Interactive(string prompt) {
            string text = " ";
            Console.Write(prompt);
            while (!string.IsNullOrEmpty(text)) {
                text = Console.ReadLine();
                Console.WriteLine(eval(text));
                Console.Write($"{Environment.NewLine}{prompt}");
            }
        }

        // user must define with each command
        public abstract void register_commands();
    }
}