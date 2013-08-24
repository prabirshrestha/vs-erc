local Task 						= luanet.import_type "System.Threading.Tasks.Task"
local CancellationTokenSource 	= luanet.import_type "System.Threading.CancellationTokenSource"

function erc.settimeout(timeout, callback)

	local cts = CancellationTokenSource()

	local task = Task.Delay(timeout, cts.Token):ContinueWith(function(t)
		callback()
	end)

end
