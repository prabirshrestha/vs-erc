
erc.editor.vs._vshelper.Events.QueryProjectAddFiles:Add(function(o, e)
	erc.emit('_vsqueryprojectaddfiles')
end)

erc.editor.vs._vshelper.Events.PostProjectAddFiles:Add(function(o, e)
	erc.emit('_vspostprojectaddfiles')
end)

erc.editor.vs._vshelper.Events.PostProjectAddDirectories:Add(function(o, e)
	erc.emit('_vspostprojectadddirectories')
end)

erc.editor.vs._vshelper.Events.PostProjectRemoveFiles:Add(function(o, e)
	erc.emit('_vspostprojectremovefiles')
end)
