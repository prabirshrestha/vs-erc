local com = require('com')

erc.editor.vs.commands = {}

erc.editor.vs.commands.execute = function(commandname, commandsargs)
	com.wrap(erc.editor.vs.dte()):ExecuteCommand(commandname, commandsargs or '')
end

erc.editor.vs.commands.get = function (commandname, commandsargs)
	return erc.editor.vs._vshelper.Commands:Get(commandname, commandsargs or -1)
end