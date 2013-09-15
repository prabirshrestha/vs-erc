
erc.editor.vs._vshelper.Events.WpfTextViewCreated:Add(function(o, e)
	erc.emit('_vswpftextviewcreated', o, e)
end)

erc.on('_vswpftextviewcreated', function(o, e)
	
	local   wpftextview = e.WpfTextView, 
			closedhandler,
			gotaggregatefocushandler, 
			lostaggregatefocushandler

	function created()
		closedhandler = wpftextview.Closed:Add(closed)
		gotaggregatefocushandler = wpftextview.GotAggregateFocus:Add(gotaggregatefocus)
		lostaggregatefocushandler = wpftextview.LostAggregateFocus:Add(lostaggregatefocus)
	end

	function closed()
		wpftextview.Closed:Remove(closedhandler)
		wpftextview.GotAggregateFocus.Remove(gotaggregatefocushandler)
		wpftextview.LostAggregateFocus.Remove(lostaggregatefocushandler)
	end

	function gotaggregatefocus()
		erc.emit('activated')
	end

	function lostaggregatefocus()
		erc.emit('deactivated')
	end

	created()

end)
