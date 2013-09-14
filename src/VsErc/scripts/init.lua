
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

-- import packages here
erc.clipboard = require 'clipboard'
erc.messagebox = require 'messagebox'
erc.settimeout = require 'settimeout'
require 'events/init'
--require 'statusbar'

-- restore the original package path
package.path = oldPackagePath;

if erc.MYERC then
    local myerc = assert(loadfile(erc.MYERC))
    myerc()
end
