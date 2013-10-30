local com = require('com')

erc.editor.arch = function()
	return 'x32'
end

erc.editor.name = function()
	return 'vs'
end

erc.editor.version = function()
	return com.wrap(erc.editor.vs.dte()).Version
end