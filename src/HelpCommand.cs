namespace azdo_init
{
    public class HelpCommand
    {
        public static bool IsHelpCommand(string arg)
        {
            switch (arg)
            {
                case "--help":
                case "-help":
                case "-h":
                case "/help":
                case "/h":
                case "?":
                case "/?":
                    return true;
                default:
                    return false;
            }

        }

        public static void ShowHelp() {
            
        }
    }
}