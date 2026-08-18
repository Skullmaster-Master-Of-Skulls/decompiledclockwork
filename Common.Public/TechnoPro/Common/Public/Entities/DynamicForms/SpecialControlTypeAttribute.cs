using System;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000353 RID: 851
	[Serializable]
	public class SpecialControlTypeAttribute : Attribute
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x0001E7EA File Offset: 0x0001C9EA
		public SpecialControlTypeAttribute() : this(eSpecialControlTypeGroup.Unknown, string.Empty, string.Empty, null)
		{
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0001E800 File Offset: 0x0001CA00
		public SpecialControlTypeAttribute(eSpecialControlTypeGroup group, string title, string description, params eControlCode[] supportedControlCodes)
		{
			this.Group = group;
			this.Title = title;
			this.Description = description;
			this.SupportedControlCodes = supportedControlCodes;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0001E82B File Offset: 0x0001CA2B
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0001E833 File Offset: 0x0001CA33
		public string Title { get; set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0001E83C File Offset: 0x0001CA3C
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x0001E844 File Offset: 0x0001CA44
		public string Description { get; set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0001E84D File Offset: 0x0001CA4D
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x0001E855 File Offset: 0x0001CA55
		public eSpecialControlTypeGroup Group { get; set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0001E85E File Offset: 0x0001CA5E
		// (set) Token: 0x06001A7D RID: 6781 RVA: 0x0001E866 File Offset: 0x0001CA66
		public bool IsHidden { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0001E86F File Offset: 0x0001CA6F
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x0001E877 File Offset: 0x0001CA77
		public eControlCode[] SupportedControlCodes { get; set; }

		// Token: 0x06001A80 RID: 6784 RVA: 0x0001E880 File Offset: 0x0001CA80
		public static SpecialControlTypeAttribute GetAttribute(eSpecialControlType specialControlType)
		{
			return specialControlType.GetAttribute<SpecialControlTypeAttribute>();
		}
	}
}
