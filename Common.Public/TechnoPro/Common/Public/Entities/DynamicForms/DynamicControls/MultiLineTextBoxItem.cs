using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicControls
{
	// Token: 0x020003AA RID: 938
	public class MultiLineTextBoxItem
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00020B5C File Offset: 0x0001ED5C
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x00020B64 File Offset: 0x0001ED64
		public string Text { get; set; }

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00020B6D File Offset: 0x0001ED6D
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x00020B75 File Offset: 0x0001ED75
		public string WhoEntered { get; set; }

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00020B7E File Offset: 0x0001ED7E
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x00020B86 File Offset: 0x0001ED86
		public DateTime? DateEntered { get; set; }
	}
}
