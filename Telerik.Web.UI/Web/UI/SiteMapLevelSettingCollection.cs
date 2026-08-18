using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB4 RID: 6836
	public class SiteMapLevelSettingCollection : StronglyTypedStateManagedCollection<SiteMapLevelSetting>
	{
		// Token: 0x06010843 RID: 67651 RVA: 0x003B04B8 File Offset: 0x003AE6B8
		public SiteMapLevelSetting GetLevelSetting(int level)
		{
			if (base.Count == 0)
			{
				return null;
			}
			int num = 0;
			Dictionary<int, SiteMapLevelSetting> dictionary = new Dictionary<int, SiteMapLevelSetting>();
			foreach (object obj in this)
			{
				SiteMapLevelSetting siteMapLevelSetting = (SiteMapLevelSetting)obj;
				if (siteMapLevelSetting.Level == -1)
				{
					dictionary[num] = siteMapLevelSetting;
					num++;
				}
				else
				{
					if (dictionary.ContainsKey(siteMapLevelSetting.Level))
					{
						throw new InvalidOperationException("Duplicate LevelSettings detected for level " + level);
					}
					dictionary[siteMapLevelSetting.Level] = siteMapLevelSetting;
					num = siteMapLevelSetting.Level + 1;
				}
			}
			if (dictionary.ContainsKey(level))
			{
				return dictionary[level];
			}
			return null;
		}
	}
}
