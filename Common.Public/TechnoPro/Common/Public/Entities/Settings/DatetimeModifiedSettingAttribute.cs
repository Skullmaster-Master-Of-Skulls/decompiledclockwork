using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D4 RID: 468
	public class DatetimeModifiedSettingAttribute : SettingDataAttribute
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x0001570E File Offset: 0x0001390E
		public DatetimeModifiedSettingAttribute(string name, Group group, SettingSemantic semanticType) : base(name, group, semanticType)
		{
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0001571B File Offset: 0x0001391B
		public DatetimeModifiedSettingAttribute(string name, string description, Group group, SettingSemantic semanticType) : base(name, description, group, semanticType)
		{
		}
	}
}
