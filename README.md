Nexomon: Extinction 存档编辑器
---

该文件在Nexomon-Extinction-Save-Editor的基础上增加了更多功能

原有的功能：
背包物品：添加/删除、更改数量等。
玩家资料，包括位置、选项、服装、性别、宠物、地图位置。
背包宠物。
仓库宠物。
可以更改怪物技能和核心。
金币等。

新增的功能：
如何使用：
---

It'll ask you which file to open on startup.<br>
Ctrl+S opens the save dialog.<br>
That's basically it for the interface.

The default save location is `C:\Program Files (x86)\Steam\userdata\<user id>\1196630\remote\data-<slot>.dat`<br>
Do **NOT** bother editing the one in AppData/LocalLow. The one there seems to just be a backup and gets overwritten so ignore it.

"<b>Made for & tested on Steam saves. Other platforms are supported but rely on the community to test, submit saves, etc, as I only own the Steam version.</b><br>"
Supported platforms:
#NAME?
#VALUE!
0

"If you have a save that breaks things, open an issue.<br>"
Should support saves up to version 23.

"### Use at your own risk, keep save backups, etc. I shouldn't need to tell you this part."

"I have the rest of the save file mapped out, but didn't bother adding stuff for achievement tracking, plot flags, etc.<br>"
List of things not yet included that I may get around to that at some point:
```
BeatenTamers                    beatenTamers;
Mining                          mining;
Rematcher                       rematcher;
Achievements                    achievements;
PermanentlyDestroyedEntities    permanentlyKilledEntities;
"Dictionary<int, bool>           switches;"
"Dictionary<int, List<string>>   permanentlyKilledFlags;"
"Dictionary<string, int>         variables;"
HashSet<DatabaseMonsters.Entry> seenMonsters;
HashSet<DatabaseMonsters.Entry> ownedMonsters;
List<int>                       cadiumMapsWithZieglerMiasma;
```

"If you want it, there's an 010 template for the start of the save file. (Goes till wallet, same as the save editor.)<br>"
[Save-Template.bt](Save-Template.bt)

Crashes
---

"If there's a crash, it'll get recorded in the Windows even viewer. `eventvwr.msc`<br>"
Open an issue with the crash log.

"If the exe doesn't appear to do anything when run, check for crash logs there.<br>"
The other common cause of this is people not extracting the files from the zip before running it. The dlls **MUST** be in the same directory for the program to work.
Running the exe from the zip will not do this and it'll fail to run.

Prerequisites
---

.Net Core/Desktop 3.1 Runtime is required to run this:
- https://dotnet.microsoft.com/download/dotnet-core/3.1
- (x64) https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x64-installer
- (x86) https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x86-installer

(Direct links may be out-of-date. Look for the "Desktop Runtime" section in the right column.)
