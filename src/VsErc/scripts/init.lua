
function print( ... )
	-- redefine the print to use erc._log
	local arguments = {...}
	local printResult = ''
	local first = true
	for i,v in ipairs(arguments) do
		if not first then
			printResult = "\t" .. printResult
		end
		printResult = printResult .. tostring(v)
	end
	printResult = printResult .. "\n"
	erc._log(printResult)
end

function _dump(o)
	if type(o) == 'table' then
		local s = '{ '
		for k,v in pairs(o) do
			if type(k) ~= 'number' then k = '"'..k..'"' end
			s = s .. '['..k..'] = ' .. dump(v) .. ','
		end
		return s .. '} '
	else
		return tostring(o)
	end
end


local function scriptPath()
   return debug.getinfo(2, "S").source:sub(2)
end

local scriptDir = string.gsub(scriptPath(), "^(.+\\)[^\\]+$", "%1") 

local oldPackagePath = package.path

package.path = package.path ..';' .. scriptDir .. '?.lua'

-- load assemblies
luanet.load_assembly(erc.editor.vs.assemblies.winforms)
luanet.load_assembly(erc.editor.vs.assemblies.vsshell)
luanet.load_assembly(erc.editor.vs.assemblies.vsinterop)

-- -- import packages here
erc.clipboard = require 'clipboard'
erc.messagebox = require 'messagebox'
erc.settimeout = require 'settimeout'
require 'events/init'
require 'commands/init'
--require 'statusbar'

-- restore the original package path
package.path = oldPackagePath;

if erc.MYERC then
    local myerc = assert(loadfile(erc.MYERC))
    myerc()
end
