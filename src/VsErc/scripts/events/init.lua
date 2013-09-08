erc._events = {}
erc._editor.vs.events = {}

erc._editor.vs.events._list = {}
	
function erc.on (name, callback)
	if callback then
		local event = {}
		event[name] = callback
		table.insert(erc._editor.vs.events._list, event)
	else
		table.insert(erc._editor.vs.events._list, name)
	end
end

require('events/onload')
require('events/onclose')
