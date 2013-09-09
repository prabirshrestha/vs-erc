# Terminologies

## Host
All scripts bundled in the VsErc.vsix plugin.

For example: Any `*.lua` scripts part of VsErc vsix plugin are known as host scripts.

## User
All scripts that are not bundled in VsErc.vsix plugin.

For example: Any scripts that are not part of VsErc plugin including `~/.erc` scripts are known as user scripts.

# LUA API Docs

## erc
`erc` - global table variable available to both the host and user scripts.

name | description | sample result
--- | --- | --- | ---
name() | gets the name of the host plugin | `vserc`
version() | gets the version of the host plugin | `0.0.1`

### erc.editor
`erc.editor` - global table variable available to both the host and user scripts.

Editor specific features.

name | description | sample result
--- | --- | --- | ---
name() | gets the name of the editor | `vs`
version() | gets the version of the editor | `12.0.0`

### erc.editor.vs
`erc.editor.vs` - visual studio specific features available to both the host and user scripts.

User scripts should always check for the existence of `erc.editor.vs` before accessing it.
