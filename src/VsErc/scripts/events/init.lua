erc._events = {};
erc._editor.vs.events = {};

do
	
	local eventsapi = {};
	local events = {};

	function eventsapi.register (eventlistener)
		table.insert(events, eventlistener)
	end

	erc.events = eventsapi;

	function erc._editor.vs.events.onload_VsErcWpfTextViewCreationListener_TextCreated(textView)
		for i,v in ipairs(events) do
			if(v.onload) then
				v.onload()
			end
		end
	end

	function erc._editor.vs.events.onclose_VsErcWpfTextViewCreationListener_TextClosed(textView)
		for i,v in ipairs(events) do
			if(v.onclose) then
				v.onclose()
			end
		end
	end

end