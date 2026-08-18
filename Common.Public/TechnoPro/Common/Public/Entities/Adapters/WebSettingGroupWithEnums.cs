using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D2 RID: 1490
	public class WebSettingGroupWithEnums
	{
		// Token: 0x06002FDE RID: 12254 RVA: 0x0000D55A File Offset: 0x0000B75A
		public WebSettingGroupWithEnums()
		{
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0003B230 File Offset: 0x00039430
		public WebSettingGroupWithEnums(Group grp, GroupDataAttribute grpDataAttr, List<WebSettingWithAttribute> wsas)
		{
			this.Group = grp;
			this.GroupDataAttribute = grpDataAttr;
			wsas.Sort((WebSettingWithAttribute g1, WebSettingWithAttribute g2) => (g1.Name + "__" + g1.SubGroup).CompareTo(g2.Name + "__" + g2.SubGroup));
			this.GroupItems = wsas;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0003B284 File Offset: 0x00039484
		public static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T t = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = t;
			}
			return result;
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x0003B2DE File Offset: 0x000394DE
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x0003B2E6 File Offset: 0x000394E6
		public Group Group { get; set; }

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x0003B2EF File Offset: 0x000394EF
		// (set) Token: 0x06002FE4 RID: 12260 RVA: 0x0003B2F7 File Offset: 0x000394F7
		public GroupDataAttribute GroupDataAttribute { get; set; }

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x0003B300 File Offset: 0x00039500
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x0003B308 File Offset: 0x00039508
		public IList<WebSettingWithAttribute> GroupItems { get; set; }
	}
}
