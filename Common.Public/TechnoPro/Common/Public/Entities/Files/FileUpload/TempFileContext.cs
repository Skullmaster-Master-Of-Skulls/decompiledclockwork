using System;

namespace TechnoPro.Common.Public.Entities.Files.FileUpload
{
	// Token: 0x0200033D RID: 829
	public class TempFileContext
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0001E23D File Offset: 0x0001C43D
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0001E245 File Offset: 0x0001C445
		public eTempFileUsage Usage { get; set; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0001E24E File Offset: 0x0001C44E
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x0001E256 File Offset: 0x0001C456
		public string GroupId { get; set; }
	}
}
