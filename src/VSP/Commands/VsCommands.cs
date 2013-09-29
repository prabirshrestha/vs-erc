namespace VSP.Commands
{
    public class VsCommands
    {
        private readonly VsHelper vsHelper;

        public VsCommands(VsHelper vsHelper)
        {
            this.vsHelper = vsHelper;
        }

        /// <summary>
        /// Executes a Visual Studio command
        /// </summary>
        /// <param name="commandName">command name</param>
        /// <param name="commandArgs">command args</param>
        /// <example>
        /// Opens a File.NewFile dialog
        /// vsHelpers.Commands.ExecuteCommand("File.NewFile")
        /// </example>
        public void Execute(string commandName, string commandArgs)
        {
            this.vsHelper.DTE.ExecuteCommand(commandName, commandArgs);
        }

        public EnvDTE.Command Get(string commandName, int id)
        {
            return this.vsHelper.DTE.Commands.Item(commandName, id);
        }

    }
}