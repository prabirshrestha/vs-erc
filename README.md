# VsErc (~/.erc for Visual Studio)

# Recommended Readings & Links

* http://sblp2004.ic.uff.br/papers/mascarenhas-ierusalimschy.pdf
* http://penlight.luaforge.net/project-pages/penlight/packages/LuaInterface/
* https://github.com/madskristensen/WebEssentials2013/

# Starting

Currently only VS2013 RTM supported

VsErc -> Properties -> Debug
Start external program:

C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe

Start Options>command line args

/rootsuffix Exp

create ~/.erc file and start coding your script

# Samples

```lua
print(erc.name() .. ' v' .. erc.version())
print(erc.editor.name() .. ' v' .. erc.editor.version() .. ' ' .. erc.editor.arch())
print(erc.platform() .. ' ' .. erc.arch())

erc.messagebox.ok('message')
erc.messagebox.ok('message', 'title')

erc.messagebox.error('error message')
erc.messagebox.error('error message', 'title')

print(erc.messagebox.okcancel('are you sure?'))
print(erc.messagebox.okcancel('are you sure?', 'title'))

erc.settimeout(20 * 1000, function()
	print('timeout')
end)

-- executing vs commands
erc.editor.vs.commands.execute('File.NewFile')

-- listening to events
erc.on('load', function()
	erc.log('view loaded')
end)

erc.on('new', function()
	print('new view')
end)

erc.on('close', function()
	print('close view')
end)

erc.on({

	presave = function()
		print('pre save')
	end,

	postsave = function()
		print('post save')
	end,

})

erc.on({

	_vspostsolutionopen = function (  )
		print('_vspostsolutionopen')
	end,

	_vsquerysolutionclose = function (  )
		print('_vsquerysolutionclose')
	end,

	_vspresolutionclose = function (  )
		print('_vspresolutionclose')
	end,

	_vspostsolutionclose = function (  )
		print('_vspostsolutionclose')
	end,

})

erc.on({

	_vspostprojectopen = function(  )
		print('_vspostprojectopen')
	end,

	_vsqueryprojectclose = function( )
		print('_vsqueryprojectclose')
	end,

	_vspreprojectclose = function( )
		print('_vspreprojectclose')
	end,

	_vsqueryprojectunload = function(  )
		print('_vsqueryprojectunload')
	end,

	_vspreprojectunload = function(  )
		print('_vspreprojectunload')
	end,

	_vspostprojectload = function(  )
		print('_vspostprojectload')
	end,

})

erc.on({

	_vsqueryprojectaddfiles = function( )
		print('_vsqueryprojectaddfiles')
	end,

	_vspostprojectaddfiles = function( )
		erc.editor.vs.commands.execute('File.SaveAll')
	end,

	_vspostprojectadddirectories = function( )
		erc.editor.vs.commands.execute('File.SaveAll')
	end,

	_vspostprojectremovefiles = function( )
		erc.editor.vs.commands.execute('File.SaveAll')
	end,

	_vspostprojectremovedirectories = function( )
		erc.editor.vs.commands.execute('File.SaveAll')
	end

})

erc.on({

	activated = function( )
		print('activated')
	end,

	deactivated = function( )
		print('deactivated')
	end
})
```

## Adding to quick launch

```lua
local Uri = luanet.import_type "System.Uri"

erc.commands.add({ 
    type = 'application',
    name = 'URL Escape', 
    description = 'URL Escape',

    new = function () -- contructor for the command
        local self = {}, urlescape

        self.execute = function() -- method that is called when executing the command
            local view, seltext, result

            view = erc.activeview()
            if not view then return end -- make sure we have the view

            seltext = view.selection.gettext()
            if not seltext then return end -- make sure we have a selection
            
            result = urlescape(seltext)
            view.selection.replacetext(result)
        end

        urlescape = function(input)
            local output = Uri.EscapeDataString(input)
            return output
        end

        return self
    end
})
```