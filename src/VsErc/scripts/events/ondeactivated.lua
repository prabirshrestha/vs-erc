function erc._editor.vs.events.ondeactivated_VsErcWpfTextViewCreationListener_VisualElementOnLostFocus(textView)
    for i,v in ipairs(erc._editor.vs.events._list) do
        if(v.deactivated) then
            v.deactivated()
        end
    end
end