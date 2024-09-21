OutFile "DeckXPToolbox-setup.exe"

Name "DeckXP Toolbox"

InstallDir "$PROFILE\DeckXP\Toolbox"

RequestExecutionLevel admin

Icon "deckxp-icon.ico"

Section "Install"
  SetOutPath "$INSTDIR"

  File /r "publish\*.*"
  File "deckxp-icon.ico"

  CreateShortcut "$DESKTOP\DeckXP.lnk" "$INSTDIR\DeckXPToolbox.exe" "" "$INSTDIR\deckxp-icon.ico"

  CreateDirectory "$SMPROGRAMS\DeckXP"
  CreateShortcut "$SMPROGRAMS\DeckXP\DeckXP.lnk" "$INSTDIR\DeckXPToolbox.exe" "" "$INSTDIR\deckxp-icon.ico"
  
  CreateShortcut "$SMPROGRAMS\DeckXP\Uninstall.lnk" "$INSTDIR\Uninstall.exe" "" "$INSTDIR\deckxp-icon.ico"

  WriteUninstaller "$INSTDIR\Uninstall.exe"

  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "DisplayName" "DeckXP Toolbox"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "UninstallString" "$INSTDIR\Uninstall.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "InstallLocation" "$INSTDIR"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "DisplayIcon" "$INSTDIR\deckxp-icon.ico"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "DisplayVersion" "1.0"
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox" "NoRepair" 1
SectionEnd

Section "Uninstall"
  Delete "$INSTDIR\*.*"
  RMDir /r "$INSTDIR"

  Delete "$DESKTOP\DeckXP.lnk"

  Delete "$SMPROGRAMS\DeckXP\DeckXP.lnk"
  Delete "$SMPROGRAMS\DeckXP\Uninstall.lnk"
  RMDir "$SMPROGRAMS\DeckXP"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\DeckXP Toolbox"
SectionEnd
