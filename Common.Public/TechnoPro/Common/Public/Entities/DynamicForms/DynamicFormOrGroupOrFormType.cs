using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200034F RID: 847
	public class DynamicFormOrGroupOrFormType
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public DynamicForm DynamicForm { get; set; }

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0001E6D1 File Offset: 0x0001C8D1
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x0001E6D9 File Offset: 0x0001C8D9
		public eDynamicFormType? DynamicFormType { get; set; }

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0001E6E2 File Offset: 0x0001C8E2
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x0001E6EA File Offset: 0x0001C8EA
		public string GroupName { get; set; }
	}
}
