using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;

namespace TechnoPro.Common.Core.Settings
{
	// Token: 0x02000043 RID: 67
	public class LookupSettingManager : ILookupSettingManager, IBaseOperationContext<SettingsOperationContext>
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0001028C File Offset: 0x0000E48C
		public LookupSettingManager(SettingsOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0001029E File Offset: 0x0000E49E
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x000102A6 File Offset: 0x0000E4A6
		public SettingsOperationContext OpContext { get; set; }

		// Token: 0x060002B4 RID: 692 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public IList<LookupSetting> GetAllLookupSettings()
		{
			Setting[] array = (Setting[])Enum.GetValues(typeof(Setting));
			LookupSetting[] array2 = new LookupSetting[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.GetLookupSetting(array[i]);
			}
			return array2;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00010304 File Offset: 0x0000E504
		public IList<LookupSetting> GetAllLookupSettings(Group group)
		{
			Setting[] source = (Setting[])Enum.GetValues(typeof(Setting));
			List<LookupSetting> list = (from setting in source
			let settGroup = setting.GetGroup()
			where settGroup == @group
			select this.GetLookupSetting(setting)).ToList<LookupSetting>();
			list.Sort(delegate(LookupSetting ls1, LookupSetting ls2)
			{
				bool flag = ls1 == null && ls2 == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = ls1 == null;
					if (flag2)
					{
						result = -1;
					}
					else
					{
						bool flag3 = ls2 == null;
						if (flag3)
						{
							result = 1;
						}
						else
						{
							result = (ls1.GroupName + ": " + ls1.SubGroup).CompareTo(ls2.GroupName + ": " + ls2.SubGroup);
						}
					}
				}
				return result;
			});
			return list;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public LookupSetting GetLookupSetting(Setting setting)
		{
			return new LookupSetting(setting);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000103CC File Offset: 0x0000E5CC
		public LookupSetting GetLookupSetting(int settingCode)
		{
			return new LookupSetting((Setting)settingCode);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000103E4 File Offset: 0x0000E5E4
		public IList<LookupSetting> GetLookupSetting(string settingName)
		{
			IList<LookupSetting> allLookupSettings = this.GetAllLookupSettings();
			List<LookupSetting> list = new List<LookupSetting>();
			foreach (LookupSetting lookupSetting in allLookupSettings)
			{
				bool flag = lookupSetting.Name.ToLower().IndexOf(settingName.ToLower()) >= 0;
				if (flag)
				{
					list.Add(lookupSetting);
				}
			}
			return list;
		}
	}
}
