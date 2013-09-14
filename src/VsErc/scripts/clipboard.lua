local Clipboard = luanet.import_type "System.Windows.Forms.Clipboard"

local clipboard = {}

function clipboard.gettext()
	return Clipboard.GetText()
end

function clipboard.settext(text)
	Clipboard.SetText(text)
end

return clipboard
