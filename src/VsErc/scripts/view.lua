local com = require ('com')

local activeview

erc.on('activated', function(view)
	activeview = view
end)

erc.view = {};
erc.view.new = function(viewdata)

	local self = {}
	self.editor = {}
	self.editor.vs = {}

	self.id = function()
		return viewdata.doccookie
	end

	self.filename = function()
		return viewdata.filename
	end

	self.lineheight = function()
		return self.editor.vs.wpftextview.LineHeight
	end

	self.linecount = function()
		return self.editor.vs.wpftextview.TextSnapshot.LineCount
	end

	self.editor.vs.wpftextview = viewdata.wpftextview

	return self
end

erc.activeview = function()
	return activeview
end