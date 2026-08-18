using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000368 RID: 872
	[Serializable]
	public class DynamicFormTypeAttribute : Attribute
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0001ED13 File Offset: 0x0001CF13
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x0001ED1B File Offset: 0x0001CF1B
		public string TablePostFix { get; set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0001ED24 File Offset: 0x0001CF24
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		public bool UseSecondaryContextId { get; set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0001ED35 File Offset: 0x0001CF35
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x0001ED3D File Offset: 0x0001CF3D
		public eDynamicDataContextColumnName PrimaryContextId { get; set; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x0001ED46 File Offset: 0x0001CF46
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x0001ED4E File Offset: 0x0001CF4E
		public eDynamicDataContextColumnName SecondaryContextId { get; set; }

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public DynamicFormTypeAttribute()
		{
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0001ED57 File Offset: 0x0001CF57
		public DynamicFormTypeAttribute(string TablePostFix, bool UseSecondaryContextId)
		{
			this.TablePostFix = TablePostFix;
			this.UseSecondaryContextId = UseSecondaryContextId;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0001ED71 File Offset: 0x0001CF71
		public DynamicFormTypeAttribute(string TablePostFix, bool UseSecondaryContextId, eDynamicDataContextColumnName primaryContextId, eDynamicDataContextColumnName secondaryContextId)
		{
			this.TablePostFix = TablePostFix;
			this.UseSecondaryContextId = UseSecondaryContextId;
			this.PrimaryContextId = primaryContextId;
			this.SecondaryContextId = secondaryContextId;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0001ED9C File Offset: 0x0001CF9C
		public DynamicFormTypeAttribute(string TablePostFix, bool UseSecondaryContextId, eDynamicDataContextColumnName primaryContextId)
		{
			this.TablePostFix = TablePostFix;
			this.UseSecondaryContextId = UseSecondaryContextId;
			this.PrimaryContextId = primaryContextId;
		}
	}
}
