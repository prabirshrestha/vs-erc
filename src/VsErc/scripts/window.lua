
erc.window = {};
erc.window.new = function(dtewindow)
	local self = {}

	return self
end

erc.activewindow = function()
	return erc.window.new(erc.editor.vs.dte())
end