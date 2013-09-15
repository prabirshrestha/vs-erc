erc.editor.vs.commands = {}

erc.editor.vs.commands.execute = function(commandname, commandsargs)
	erc.editor.vs._vshelper.Commands:Execute(commandname, commandsargs or '')
end
