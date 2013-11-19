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

	self.editor.vs.wpftextview = viewdata.wpftextview

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

	self.selection = {}
	self.selection.gettext = function()
		local wpftextview = self.editor.vs.wpftextview
		if not wpftextview or wpftextview.Selection.IsEmpty then
			return nil
		end
		return wpftextview.Selection.StreamSelectionSpan:GetText()
	end

	self.selection.replacetext = function(text)
		local wpftextview = self.editor.vs.wpftextview
		if not wpftextview or wpftextview.Selection.IsEmpty then
			return nil
		end

		erc.editor.vs._vshelper:ReplaceSelectedText(wpftextview, text)

		-- local selection = wpftextview.Selection.StreamSelectionSpan.SnapshotSpan;
		-- local edit = wpftextview.TextBuffer:CreateEdit()
		-- edit:Replace(selection.Start.Position, selection.End.Position, text)
		-- print(selection.Start.Position)
		-- print(selection.End.Position)

		-- edit:Apply()
		-- edit:Dispose()
	end

	return self
end

erc.activeview = function()
	return activeview
end