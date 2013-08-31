local function scriptPath()
   return debug.getinfo(2, "S").source:sub(2)
end

local scriptDir = string.gsub(scriptPath(), "^(.+\\)[^\\]+$", "%1") 

local oldPackagePath = package.path

package.path = package.path ..';' .. scriptDir .. '?.lua'

-- assemblies that may be used
erc._editor.vs.assemblies = {
	winforms 	= 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',
	vsshell 	= 'Microsoft.VisualStudio.Shell.12.0, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a',
	vsinterop	= 'Microsoft.VisualStudio.Shell.Interop, Version=7.1.40304.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
}

-- load assemblies
luanet.load_assembly(erc._editor.vs.assemblies.winforms)
luanet.load_assembly(erc._editor.vs.assemblies.vsshell)
luanet.load_assembly(erc._editor.vs.assemblies.vsinterop)

-- import packages here
require 'clipboard'
require 'messagebox'
require 'settimeout'
require 'statusbar'

-- restore the original package path
package.path = oldPackagePath;

if erc.MYERC then
    local myerc = assert(loadfile(erc.MYERC))
    myerc()
end
