
erc.editor.vs._vshelper.Events.PreDocumentWindowShow:Add(function(o, e)
	local view = erc.view.new({
		firstshow = e.FirstShow,
		doccookie = e.DocCookie,
		vswindowframe = e.VsWindowFrame,
		filename = e.FilePath,
		wpftextview = e.WpfTextView
	})

	if(e.FirstShow) then
		erc.emit('new', view)
	end

	erc.emit('activated', view)
end)
