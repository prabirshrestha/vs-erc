
erc.editor.vs._vshelper.Events.PostProjectOpen:Add(function(o, e)
	erc.emit('_vspostprojectopen')
end)

erc.editor.vs._vshelper.Events.QueryProjectClose:Add(function(o, e)
	erc.emit('_vsqueryprojectclose')
end)

erc.editor.vs._vshelper.Events.PreProjectClose:Add(function(o, e)
	erc.emit('_vspreprojectclose')
end)

erc.editor.vs._vshelper.Events.QueryProjectUnload:Add(function(o, e)
	erc.emit('_vsqueryprojectunload')
end)

erc.editor.vs._vshelper.Events.PreProjectUnload:Add(function(o, e)
	erc.emit('_vspreprojectunload')
end)

erc.editor.vs._vshelper.Events.PostProjectLoad:Add(function(o, e)
	erc.emit('_vspostprojectload')
end)
