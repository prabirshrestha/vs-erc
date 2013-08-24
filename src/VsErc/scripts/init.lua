luanet.load_assembly "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"

if not erc.messageBox or not erc.errorMessage then

    local MessageBox        = luanet.import_type "System.Windows.Forms.MessageBox"
    local MessageBoxButtons = luanet.import_type "System.Windows.Forms.MessageBoxButtons"
    local MessageBoxIcon    = luanet.import_type "System.Windows.Forms.MessageBoxIcon"

    function erc.messageBox(message)
        MessageBox.Show(message)
    end

    function erc.errorMessageBox(message)
        MessageBox.Show(message, 'Error', MessageBoxButtons.OK, MessageBoxIcon.Error)
    end

end

if erc.MYERC then
    dofile(erc.MYERC)
end
