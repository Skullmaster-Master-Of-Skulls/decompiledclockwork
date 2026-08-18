using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A5 RID: 1445
	public class AlertTriggerTypeAttribute : Attribute
	{
		// Token: 0x06002EE0 RID: 12000 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public AlertTriggerTypeAttribute()
		{
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00033925 File Offset: 0x00031B25
		public AlertTriggerTypeAttribute(string title, string description, Type defType, Type baseDefType)
		{
			this.Title = title;
			this.Description = description;
			this.DefinitionType = defType;
			this.DefinitionBaseType = baseDefType;
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x00033950 File Offset: 0x00031B50
		// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x00033958 File Offset: 0x00031B58
		public bool IsDisabled { get; set; }

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x00033961 File Offset: 0x00031B61
		// (set) Token: 0x06002EE5 RID: 12005 RVA: 0x00033969 File Offset: 0x00031B69
		public string Title { get; set; }

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x00033972 File Offset: 0x00031B72
		// (set) Token: 0x06002EE7 RID: 12007 RVA: 0x0003397A File Offset: 0x00031B7A
		public string Description { get; set; }

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x00033983 File Offset: 0x00031B83
		// (set) Token: 0x06002EE9 RID: 12009 RVA: 0x0003398B File Offset: 0x00031B8B
		public bool IsForInternalUseOnly { get; set; }

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x00033994 File Offset: 0x00031B94
		// (set) Token: 0x06002EEB RID: 12011 RVA: 0x0003399C File Offset: 0x00031B9C
		public Type DefinitionType { get; set; }

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000339A5 File Offset: 0x00031BA5
		// (set) Token: 0x06002EED RID: 12013 RVA: 0x000339AD File Offset: 0x00031BAD
		public Type DefinitionBaseType { get; set; }
	}
}
