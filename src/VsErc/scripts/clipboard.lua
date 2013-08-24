local Clipboard = luanet.import_type "System.Windows.Forms.Clipboard"

function erc.getclipboard()
	return Clipboard.GetText()
end

function erc.setclipboard(text)
	Clipboard.SetText(text)
end
