using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001253 RID: 4691
	[Flags]
	public enum TreeListItemType
	{
		// Token: 0x040032E3 RID: 13027
		Item = 1,
		// Token: 0x040032E4 RID: 13028
		AlternatingItem = 2,
		// Token: 0x040032E5 RID: 13029
		SelectedItem = 4,
		// Token: 0x040032E6 RID: 13030
		HeaderItem = 8,
		// Token: 0x040032E7 RID: 13031
		PagerItem = 16,
		// Token: 0x040032E8 RID: 13032
		DetailTemplateItem = 32,
		// Token: 0x040032E9 RID: 13033
		NoRecordsTemplateItem = 64,
		// Token: 0x040032EA RID: 13034
		EditItem = 128,
		// Token: 0x040032EB RID: 13035
		EditFormItem = 256,
		// Token: 0x040032EC RID: 13036
		FooterItem = 512,
		// Token: 0x040032ED RID: 13037
		CommandItem = 1024
	}
}
