using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x02000382 RID: 898
	public class DynamicFieldAvailableConversionAttribute : Attribute
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public DynamicFieldAvailableConversionAttribute()
		{
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0001F7F7 File Offset: 0x0001D9F7
		public DynamicFieldAvailableConversionAttribute(string title, string description, eDynamicFieldConversionFieldInfo fromFields, eDynamicFieldConversionFieldInfo toFields)
		{
			this.FromFields = fromFields;
			this.ToFields = toFields;
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0001F812 File Offset: 0x0001DA12
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0001F81A File Offset: 0x0001DA1A
		public eDynamicFieldConversionFieldInfo FromFields { get; set; }

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0001F823 File Offset: 0x0001DA23
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0001F82B File Offset: 0x0001DA2B
		public eDynamicFieldConversionFieldInfo ToFields { get; set; }

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0001F834 File Offset: 0x0001DA34
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0001F83C File Offset: 0x0001DA3C
		public string Title { get; set; }

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0001F845 File Offset: 0x0001DA45
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0001F84D File Offset: 0x0001DA4D
		public string Description { get; set; }

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0001F856 File Offset: 0x0001DA56
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0001F85E File Offset: 0x0001DA5E
		public bool IsDisabled { get; set; }
	}
}
