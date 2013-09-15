
-- erc.editor.vs._vshelper.Events.PreDocumentWindowShow:Add(function(o, e)
	
-- 	-- if(e.FirstShow) then
-- 	-- 	erc.emit('new')
-- 	-- end

-- end)

erc.editor.vs._vshelper.Events.PreDocumentWindowShow:Add(function(o, e)
	if(e.FirstShow) then
		erc.emit('new')
	end
end)
