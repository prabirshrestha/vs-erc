local Clipboard = luanet.import_type "System.Windows.Forms.Clipboard"

function erc.getClipboard()
	return Clipboard.GetText()
end
