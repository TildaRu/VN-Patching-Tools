# Love Kami Restoration Patch

This patch restores the H-scenes removed from the English release of the Love Kami games.
There are also patches for converting the Japanese versions which already have the H-scenes to English.

The H-scenes are machine translated and the gallery for the English releases have not been patched.

## Downloads

Download, unzip, and replace the game files.

| Game            | Applicable Version        | Download |
|-----------------|---------------------------|----------|
| Divinity Stage  | <https://vndb.org/r49129> | |
| Sweet Stars     | <https://vndb.org/r52608> | <https://github.com/kevlu123/VN-Patching-Tools/releases/download/LoveKami/Sweet.Stars.zip> |
| Useless Goddess | <https://vndb.org/r51504> | |
| Trouble Goddess | <https://vndb.org/r52609> | <https://github.com/kevlu123/VN-Patching-Tools/releases/download/LoveKami/Trouble.Goddess.zip> |
| Healing Harem   | <https://vndb.org/r60067> | |
| Pureness Harem  | <https://vndb.org/r60066> | <https://github.com/kevlu123/VN-Patching-Tools/releases/download/LoveKami/Pureness.Harem.zip> |

## Exe Patch Notes

For Trouble Goddess, the exe file has been patched to decrease the size of the text.

A byte at offset 0x39555 has been changed from 72 (0x48) to 80 (0x50) which is used as a divisor to compute the lfHeight field for [LOGFONT](https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-logfonta).