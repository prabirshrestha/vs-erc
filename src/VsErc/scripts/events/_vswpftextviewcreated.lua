
erc.on('_vswpftextviewcreated', function(wpftextview)

	local 	closedhandler, 
			gotaggregatefocushandler,
			lostaggregatefocushandler

	function created()
		closedhandler = wpftextview.Closed:Add(closed)
		gotaggregatefocushandler = wpftextview.GotAggregateFocus:Add(gotaggregatefocus)
		lostaggregatefocushandler = wpftextview.LostAggregateFocus:Add(lostaggregatefocus)
		erc.emit('load')
	end

	function closed(sender, args)
		wpftextview.Closed:Remove(closedhandler)
		wpftextview.GotAggregateFocus:Remove(gotaggregatefocushandler)
		wpftextview.LostAggregateFocus:Remove(lostaggregatefocushandler)
		erc.emit('close')
	end

	function gotaggregatefocus()
		erc.emit('activated')
	end

	function lostaggregatefocus()
		erc.emit('deactivated')
	end

	created()

end)
