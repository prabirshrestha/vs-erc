
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

require('events/onnew')
require('events/onclose')
require('events/onpresave')
require('events/onpostsave')
require('events/vs/solutionevents')
require('events/vs/projectevents')
require('events/vs/projectretargettingevents')
require('events/vs/projectdocumentevents')
require('events/vs/wpftextviewcreated')
