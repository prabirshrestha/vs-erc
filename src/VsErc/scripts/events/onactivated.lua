function erc._editor.vs.events.onactivated_VsErcWpfTextViewCreationListener_GotAggregateFocus(textView)
    for i,v in ipairs(erc._editor.vs.events._list) do
        if(v.activated) then
            v.activated()
        end
    end
end