using System;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x0200000D RID: 13
	public class AdminTestMessageView
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002254 File Offset: 0x00000454
		public AdminTestMessageView()
		{
			this.ShowSessionContents = true;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002266 File Offset: 0x00000466
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000226E File Offset: 0x0000046E
		public string Context { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002277 File Offset: 0x00000477
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000227F File Offset: 0x0000047F
		public string Message { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002288 File Offset: 0x00000488
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002290 File Offset: 0x00000490
		public bool ShowSessionContents { get; set; }
	}
}
