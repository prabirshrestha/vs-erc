local MessageBox        = luanet.import_type "System.Windows.Forms.MessageBox"
local MessageBoxButtons = luanet.import_type "System.Windows.Forms.MessageBoxButtons"
local MessageBoxIcon    = luanet.import_type "System.Windows.Forms.MessageBoxIcon"

function erc.messagebox(message)
    MessageBox.Show(message)
end

function erc.errormessagebox(message)
    MessageBox.Show(message, 'Error', MessageBoxButtons.OK, MessageBoxIcon.Error)
end
