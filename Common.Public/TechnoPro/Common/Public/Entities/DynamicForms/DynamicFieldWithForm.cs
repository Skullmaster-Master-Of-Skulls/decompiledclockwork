using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200036E RID: 878
	[Serializable]
	public class DynamicFieldWithForm : DynamicField
	{
		// Token: 0x06001B35 RID: 6965 RVA: 0x0001F290 File Offset: 0x0001D490
		public DynamicFieldWithForm()
		{
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0001F2BE File Offset: 0x0001D4BE
		public DynamicFieldWithForm(DynamicField field, DynamicForm form) : base(field)
		{
			this.Form = form;
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0001F2D1 File Offset: 0x0001D4D1
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x0001F2D9 File Offset: 0x0001D4D9
		public DynamicForm Form { get; set; }
	}
}
