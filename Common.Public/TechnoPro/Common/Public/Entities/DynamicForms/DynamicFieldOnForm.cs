using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200036D RID: 877
	[Serializable]
	public class DynamicFieldOnForm : DynamicField
	{
		// Token: 0x06001B31 RID: 6961 RVA: 0x0001F290 File Offset: 0x0001D490
		public DynamicFieldOnForm()
		{
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0001F29A File Offset: 0x0001D49A
		public DynamicFieldOnForm(DynamicField field, int screenNum) : base(field)
		{
			this.ScreenNum = screenNum;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0001F2AD File Offset: 0x0001D4AD
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x0001F2B5 File Offset: 0x0001D4B5
		public int ScreenNum { get; set; }
	}
}
