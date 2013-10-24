erc.commands = {}

local commands = {}

function erc.commands.add (command)
	commands[command.name] = command
end

function erc.commands.get(name)
	return commands[name]
end

function erc.commands.getall()
	return commands
end

function erc.commands.remove(nameOrCommand)
	-- todo
end

function erc.commands.execute(name)
	local command = commands[name].new()
	command:execute()
end

require 'commands/vs/vscommands'
