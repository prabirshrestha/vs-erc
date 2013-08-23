luanet.load_assembly "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"

if not erc.messageBox then

	MessageBox = luanet.import_type "System.Windows.Forms.MessageBox"

	function erc.messageBox(message)
		MessageBox.Show(message)
	end

end

if erc.MYERC then
	dofile(erc.MYERC)
end
