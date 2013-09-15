
erc.editor.vs._vshelper.Events.PostSolutionOpen:Add(function(o, e)
	erc.emit('_vspostsolutionopen')
end)

erc.editor.vs._vshelper.Events.QuerySolutionClose:Add(function(o, e)
	erc.emit('_vsquerysolutionclose')
end)

erc.editor.vs._vshelper.Events.PreSolutionClose:Add(function(o, e)
	erc.emit('_vspresolutionclose')
end)

erc.editor.vs._vshelper.Events.PostSolutionClose:Add(function(o, e)
	erc.emit('_vspostsolutionclose')
end)
