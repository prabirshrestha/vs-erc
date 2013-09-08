function erc._editor.vs.events.onactivated_VsErcWpfTextViewCreationListener_VisualElementOnGotFocus(textView)
    for i,v in ipairs(erc._editor.vs.events._list) do
        if(v.activated) then
            v.activated()
        end
    end
end