local MessageBox        = luanet.import_type "System.Windows.Forms.MessageBox"
local MessageBoxButtons = luanet.import_type "System.Windows.Forms.MessageBoxButtons"
local MessageBoxIcon    = luanet.import_type "System.Windows.Forms.MessageBoxIcon"

function erc.message_box(message)
    MessageBox.Show(message)
end

function erc.error_message_box(message)
    MessageBox.Show(message, 'Error', MessageBoxButtons.OK, MessageBoxIcon.Error)
end
