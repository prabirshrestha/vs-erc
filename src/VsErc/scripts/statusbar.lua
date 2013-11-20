local Type              = luanet.import_type "System.Type"
local Type_IVsStatusbar = Type.GetType('Microsoft.VisualStudio.Shell.Interop.IVsStatusbar, ' .. erc._editor.vs.assemblies.vsinterop)

local iVsStatusbar 		= erc._editor.vs.serviceprovider.getbytype(Type_IVsStatusbar)

erc.statusbar = {}

function erc.statusbar.settext(message)
	iVsStatusbar:SetText(message)
	iVsStatusbar:FreezeOutput(1)
end

function erc.statusbar.clear()
	iVsStatusbar:FreezeOutput(0)
	iVsStatusbar:SetText('')
end
