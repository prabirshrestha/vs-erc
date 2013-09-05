erc._events = {};
erc._editor.vs.events = {};

do
	
	local events = {};

	function erc.on (name, callback)
		if callback then
			local event = {}
			event[name] = callback
			table.insert(events, event)
		else
			table.insert(events, name)
		end
	end

	function erc._editor.vs.events.onload_VsErcWpfTextViewCreationListener_TextCreated(textView)
		for i,v in ipairs(events) do
			if(v.load) then
				v.load()
			end
		end
	end

	function erc._editor.vs.events.onclose_VsErcWpfTextViewCreationListener_TextClosed(textView)
		for i,v in ipairs(events) do
			if(v.close) then
				v.close()
			end
		end
	end

end

