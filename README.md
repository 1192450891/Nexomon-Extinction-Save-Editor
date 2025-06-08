Nexomon: 重生 存档编辑器
---

该编辑器在Nexomon-Extinction-Save-Editor的基础上增加了更多功能（以下基本基于原版的readme 我进行了翻译以及新功能说明）

原有的功能：
背包物品：添加/删除、更改数量等。
玩家资料，包括位置、选项、服装、性别、宠物、地图位置。
背包宠物。
仓库宠物。
可以更改怪物技能和核心。
金币等。
新增的功能：（所有功能建议通关后使用 防止坏档）
一键物品全满 Ctrl+Q（所有道具999 包括剧情道具等）
一键收集所有宠物 Ctrl+W（按一次后仓库会多一个箱子 里面存放了所有异色宠物）
一键击败所有训练家2次 Ctrl+E
已获得/花费金币、已打破石头数、食物投喂次数、碎片出售数量等数据修改
如何使用：
---

运行Debug文件夹中的exe文件或使用编译器运行源代码
会打开一个选择窗口请选择游戏的存档.dat文件
编辑完成后使用Ctrl+S保存存档文件


Steam版的存档位置在 `Steam\userdata\<user id>\1196630\remote\data-<slot>.dat`
在 AppData/LocalLow 路径下的只是存档的复制 不需要管

在Steam上制作和测试。支持其他平台，但依赖社区进行测试、提交保存等，因为我只拥有Steam版本
Supported platforms:
Steam
Switch
PS4

如果编辑器保存出现了存档问题，请创建一个issue提交
应支持保存到版本23

### 使用风险自负，保存备份等。我不需要告诉你这部分。

已经有部分可视化编辑功能，但目前没有添加成就跟踪、剧情标志等编辑功能。
列出尚未包括的事项，我可能会在某个时候讨论这些事项：
"（作者最后一次提交是三年前了 我来助你！）
（每个训练师击败两次和抓所有怪真的很无聊- - ，为了保护大家的肝！）"
BeatenTamers                    beatenTamers;(击败的训练家 已解析！)
Mining                          mining;(挖矿数据 已解析！)
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

项目中有一个010模板可以作为存档的开头
[Save-Template.bt](Save-Template.bt)

崩溃
---

如果发生崩溃，它甚至会被记录在Windows查看器中。`eventvwr.msc`<br>
打开崩溃日志的问题。

如果运行时exe似乎没有做任何事情，请检查那里的崩溃日志。
另一个常见的原因是人们在运行zip之前没有从zip中提取文件。dlls**必须**在同一目录中，程序才能工作。
从zip运行exe不会这样做，它将无法运行。

先决条件
---

运行此操作需要.Net Core/Desktop 3.1运行时：
- https://dotnet.microsoft.com/download/dotnet-core/3.1
-（x64）https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x64-installer
-（x86）https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x86-installer
（直接链接可能已过时。请在右侧列中查找“桌面运行时”部分。）
