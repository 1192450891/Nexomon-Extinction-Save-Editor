Nexomon: 重生 存档编辑器（正在开发中 预计还需要2周完成）
---

该编辑器在Nexomon-Extinction-Save-Editor的基础上增加了更多功能（以下基本基于原版的readme 我进行了翻译以及新功能说明）

原有的功能：</br>
背包物品：添加/删除、更改数量等。</br>
玩家资料，包括位置、选项、服装、性别、宠物、地图位置。</br>
修改背包宠物。</br>
修改仓库宠物。</br>
修改怪物技能和核心。</br>
修改金币等。</br>
新增的功能：（所有功能建议通关后使用 防止坏档）</br>
一键物品全满 Ctrl+Q（所有道具999 包括剧情道具等）</br>
一键收集所有宠物 Ctrl+W（按一次后仓库会多一个箱子 里面存放了所有异色宠物）</br>
一键击败所有训练家2次 Ctrl+E</br>
已获得/花费金币、已打破石头数、食物投喂次数、碎片出售数量等数据修改</br>

如何使用：
---

运行Debug文件夹中的exe文件或使用编译器运行源代码</br>
会打开一个选择窗口请选择游戏的存档.dat文件</br>
编辑完成后使用Ctrl+S保存存档文件


Steam版的存档位置在 `Steam\userdata\<user id>\1196630\remote\data-<slot>.dat`</br>
在 AppData/LocalLow 路径下的只是存档的复制 不需要管</br>

在Steam上制作和测试。支持其他平台，但依赖社区进行测试、提交保存等，因为我只拥有Steam版本</br>
Supported platforms:</br>
Steam</br>
Switch</br>
PS4</br>

如果编辑器保存出现了存档问题，请创建一个issue提交</br>
应支持保存到版本23</br>

### 使用风险自负，保存备份等。我不需要告诉你这部分。

已经有部分可视化编辑功能，但目前没有添加成就跟踪、剧情标志等编辑功能。</br>
列出尚未包括的事项，我可能会在某个时候讨论这些事项：</br>
"（作者最后一次提交是三年前了 我来助你！）</br>
（每个训练师击败两次和抓所有怪真的很无聊- - ，为了保护大家的肝！）"</br>
BeatenTamers                    beatenTamers;(击败的训练家 已解析！)</br>
Mining                          mining;(挖矿数据 已解析！)</br>
Rematcher                       rematcher;(再次遇到的训练师数据 已解析！)</br>
Achievements                    achievements;</br>
PermanentlyDestroyedEntities    permanentlyKilledEntities;</br>
"Dictionary<int, bool>           switches;"</br>
"Dictionary<int, List<string>>   permanentlyKilledFlags;"</br>
"Dictionary<string, int>         variables;"</br>
HashSet<DatabaseMonsters.Entry> seenMonsters;</br>
HashSet<DatabaseMonsters.Entry> ownedMonsters;</br>
List<int>                       cadiumMapsWithZieglerMiasma;</br>
```

项目中有一个010模板可以作为存档的开头</br>
[Save-Template.bt](Save-Template.bt)
```
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
