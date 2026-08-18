using System;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000355 RID: 853
	[Serializable]
	public class SpecialControlTypeGroupAttribute : Attribute
	{
		// Token: 0x06001A81 RID: 6785 RVA: 0x0001E89D File Offset: 0x0001CA9D
		public SpecialControlTypeGroupAttribute() : this("")
		{
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		public SpecialControlTypeGroupAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0001E8BE File Offset: 0x0001CABE
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0001E8C6 File Offset: 0x0001CAC6
		public string Title { get; set; }

		// Token: 0x06001A85 RID: 6789 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		public static SpecialControlTypeGroupAttribute GetAttribute(eSpecialControlTypeGroup specialControlTypeGroup)
		{
			return specialControlTypeGroup.GetAttribute<SpecialControlTypeGroupAttribute>();
		}
	}
}
