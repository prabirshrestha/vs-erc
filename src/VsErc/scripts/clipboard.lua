local Clipboard = luanet.import_type "System.Windows.Forms.Clipboard"

erc.clipboard = {}

function erc.clipboard.gettext()
	return Clipboard.GetText()
end

function erc.clipboard.settext(text)
	Clipboard.SetText(text)
end
