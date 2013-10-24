erc.editor.vs.commands = {}

erc.editor.vs.commands.execute = function(commandname, commandsargs)
	erc.editor.vs._vshelper.Commands:Execute(commandname, commandsargs or '')
end

erc.editor.vs.commands.get = function (commandname, commandsargs)
	return erc.editor.vs._vshelper.Commands:Get(commandname, commandsargs or -1)
end