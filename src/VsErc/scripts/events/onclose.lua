function erc._editor.vs.events.onclose_VsErcWpfTextViewCreationListener_TextClosed(textView)
    for i,v in ipairs(erc._editor.vs.events._list) do
        if(v.close) then
            v.close()
        end
    end
end