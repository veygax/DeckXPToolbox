OutFile "DeckXPToolbox-setup.exe"

Name "DeckXP Toolbox 1.0"

InstallDir "$USERPROFILE\DeckXP\Toolbox"

RequestExecutionLevel admin

Icon "deckxp-icon.ico"

SetOutPath "$INSTDIR"

File /r "publish\*.*"

File "deckxp-icon.ico"

Section "Install"
  SetOutPath "$INSTDIR"

  File /r "publish\*.*"

  CreateShortcut "$DESKTOP\DeckXP.lnk" "$INSTDIR\DeckXP.exe" "" "$INSTDIR\deckxp-icon.ico"

  CreateDirectory "$SMPROGRAMS\DeckXP"
  CreateShortcut "$SMPROGRAMS\DeckXP\DeckXP.lnk" "$INSTDIR\DeckXP.exe" "" "$INSTDIR\deckxp-icon.ico"

  CreateShortcut "$SMPROGRAMS\DeckXP\Uninstall.lnk" "$INSTDIR\Uninstall.exe" "" "$INSTDIR\deckxp-icon.ico"

SectionEnd

Section "Uninstall"
  Delete "$INSTDIR\*.*"
  RMDir /r "$INSTDIR"

  Delete "$DESKTOP\DeckXP.lnk"

  Delete "$SMPROGRAMS\DeckXP\DeckXP.lnk"
  Delete "$SMPROGRAMS\DeckXP\Uninstall.lnk"
  RMDir "$SMPROGRAMS\DeckXP"
SectionEnd

WriteUninstaller "$INSTDIR\Uninstall.exe"
