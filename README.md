Nexomon: ���� �浵�༭��
---

�ñ༭����Nexomon-Extinction-Save-Editor�Ļ����������˸��๦�ܣ����»�������ԭ���readme �ҽ����˷����Լ��¹���˵����

ԭ�еĹ��ܣ�
������Ʒ�����/ɾ�������������ȡ�
������ϣ�����λ�á�ѡ���װ���Ա𡢳����ͼλ�á�
�������
�ֿ���
���Ը��Ĺ��＼�ܺͺ��ġ�
��ҵȡ�
�����Ĺ��ܣ������й��ܽ���ͨ�غ�ʹ�� ��ֹ������
һ����Ʒȫ�� Ctrl+Q�����е���999 ����������ߵȣ�
һ���ռ����г��� Ctrl+W����һ�κ�ֿ���һ������ ��������������ɫ���
һ����������ѵ����2�� Ctrl+E
�ѻ��/���ѽ�ҡ��Ѵ���ʯͷ����ʳ��Ͷι��������Ƭ���������������޸�
���ʹ�ã�
---

����Debug�ļ����е�exe�ļ���ʹ�ñ���������Դ����
���һ��ѡ�񴰿���ѡ����Ϸ�Ĵ浵.dat�ļ�
�༭��ɺ�ʹ��Ctrl+S����浵�ļ�


Steam��Ĵ浵λ���� `Steam\userdata\<user id>\1196630\remote\data-<slot>.dat`
�� AppData/LocalLow ·���µ�ֻ�Ǵ浵�ĸ��� ����Ҫ��

��Steam�������Ͳ��ԡ�֧������ƽ̨���������������в��ԡ��ύ����ȣ���Ϊ��ֻӵ��Steam�汾
Supported platforms:
Steam
Switch
PS4

����༭����������˴浵���⣬�봴��һ��issue�ύ
Ӧ֧�ֱ��浽�汾23

### ʹ�÷����Ը������汸�ݵȡ��Ҳ���Ҫ�������ⲿ�֡�

�Ѿ��в��ֿ��ӻ��༭���ܣ���Ŀǰû����ӳɾ͸��١������־�ȱ༭���ܡ�
�г���δ����������ҿ��ܻ���ĳ��ʱ��������Щ���
"���������һ���ύ������ǰ�� �������㣡��
��ÿ��ѵ��ʦ�������κ�ץ���й���ĺ�����- - ��Ϊ�˱�����ҵĸΣ���"
BeatenTamers                    beatenTamers;(���ܵ�ѵ���� �ѽ�����)
Mining                          mining;(�ڿ����� �ѽ�����)
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

��Ŀ����һ��010ģ�������Ϊ�浵�Ŀ�ͷ
[Save-Template.bt](Save-Template.bt)

����
---

��������������������ᱻ��¼��Windows�鿴���С�`eventvwr.msc`<br>
�򿪱�����־�����⡣

�������ʱexe�ƺ�û�����κ����飬��������ı�����־��
��һ��������ԭ��������������zip֮ǰû�д�zip����ȡ�ļ���dlls**����**��ͬһĿ¼�У�������ܹ�����
��zip����exe�����������������޷����С�

�Ⱦ�����
---

���д˲�����Ҫ.Net Core/Desktop 3.1����ʱ��
- https://dotnet.microsoft.com/download/dotnet-core/3.1
-��x64��https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x64-installer
-��x86��https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x86-installer
��ֱ�����ӿ����ѹ�ʱ�������Ҳ����в��ҡ���������ʱ�����֡���
