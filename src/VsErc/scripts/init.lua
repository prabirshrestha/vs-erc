local function script_path()
   return debug.getinfo(2, "S").source:sub(2)
end

local script_dir = string.gsub(script_path(), "^(.+\\)[^\\]+$", "%1") 

local old_package_path = package.path

package.path = package.path..';' .. script_dir .. '?.lua'

-- import packages here
luanet.load_assembly "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
require 'messagebox'

-- restore the original package path
package.path = old_package_path;

if erc.MYERC then
    dofile(erc.MYERC)
end
