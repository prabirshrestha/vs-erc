function erc._editor.vs.events.onload_VsErcWpfTextViewCreationListener_TextCreated(textView)
	for i,v in ipairs(erc._editor.vs.events._list) do
		if(v.load) then
			v.load()
		end
	end
end
