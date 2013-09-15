
erc.editor._events = {}

function erc.on (name, callback)
	if callback then
		local event = {}
		event[name] = callback
		table.insert(erc.editor._events, event)
	else
		table.insert(erc.editor._events, name)
	end
end

function erc.emit (name, ...)
	for i,v in ipairs(erc.editor._events) do
		local callback = v[name]
        if(callback) then
            callback(...)
        end
    end
end

require('events/onnewview')
require('events/oncloseview')
require('events/onpresave')
require('events/onpostsave')
