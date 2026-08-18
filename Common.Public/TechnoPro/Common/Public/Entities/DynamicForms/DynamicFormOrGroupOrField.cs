using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200034E RID: 846
	public class DynamicFormOrGroupOrField
	{
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x0001E68D File Offset: 0x0001C88D
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x0001E695 File Offset: 0x0001C895
		public string GroupName { get; set; }

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0001E69E File Offset: 0x0001C89E
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x0001E6A6 File Offset: 0x0001C8A6
		public DynamicForm DynamicForm { get; set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x0001E6AF File Offset: 0x0001C8AF
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x0001E6B7 File Offset: 0x0001C8B7
		public DynamicField Field { get; set; }
	}
}
