local function scriptPath()
   return debug.getinfo(2, "S").source:sub(2)
end

local scriptDir = string.gsub(scriptPath(), "^(.+\\)[^\\]+$", "%1") 

local oldPackagePath = package.path

package.path = package.path..';' .. scriptDir .. '?.lua'

-- import packages here
luanet.load_assembly "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
require 'messagebox'

-- restore the original package path
package.path = oldPackagePath;

if erc.MYERC then
    local myerc = assert(loadfile(erc.MYERC))
    myerc()
end
