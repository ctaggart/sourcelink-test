# https://stackoverflow.com/a/12813949
# Import-Module .\FolderSize.psm1
# Get-FolderSize .
function Get-FolderSize([string] $folder)
{
    switch((ls -r $folder | measure -s Length).Sum) {
        {$_ -gt 1GB} {
          return '{0:0.0} GiB' -f ($_/1GB)
        }
        {$_ -gt 1MB} {
          return '{0:0.0} MiB' -f ($_/1MB)
        }
        {$_ -gt 1KB} {
          Return '{0:0.0} KiB' -f ($_/1KB)
        }
        return { "$_ bytes" }
      }
}