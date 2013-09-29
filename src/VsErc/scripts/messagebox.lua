local MessageBox        = luanet.import_type "System.Windows.Forms.MessageBox"
local MessageBoxButtons = luanet.import_type "System.Windows.Forms.MessageBoxButtons"
local MessageBoxIcon    = luanet.import_type "System.Windows.Forms.MessageBoxIcon"
local DialogResult    	= luanet.import_type "System.Windows.Forms.DialogResult"

-- erc._log(MessageBox)
-- print(type(MessageBox))
-- print(print(getmetatable(MessageBox)))
local m = getmetatable(MessageBox)

local messagebox = {}

function messagebox.ok(message, title)
    MessageBox.Show(message, title or '')
end

function messagebox.error(message, title)
    MessageBox.Show(message, title or 'Error', MessageBoxButtons.OK, MessageBoxIcon.Error)
end

function messagebox.okcancel(message, title, ok, cancel)
	-- ok and cancel text not supported yet
	local result = MessageBox.Show(message, title or '', MessageBoxButtons.OKCancel)
	if result == DialogResult.OK then
		return true
	else
		return false
	end
end

return messagebox
