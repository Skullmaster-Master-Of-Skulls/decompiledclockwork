using System;
using System.Runtime.CompilerServices;

namespace System.Web.Helpers
{
	// Token: 0x0200001F RID: 31
	public class WebGridColumn
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000077F4 File Offset: 0x000059F4
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000077FC File Offset: 0x000059FC
		public bool CanSort { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007805 File Offset: 0x00005A05
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000780D File Offset: 0x00005A0D
		public string ColumnName { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007816 File Offset: 0x00005A16
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000781E File Offset: 0x00005A1E
		[Dynamic(new bool[]
		{
			false,
			true,
			false
		})]
		public Func<dynamic, object> Format { [return: Dynamic(new bool[]
		{
			false,
			true,
			false
		})] get; [param: Dynamic(new bool[]
		{
			false,
			true,
			false
		})] set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007827 File Offset: 0x00005A27
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000782F File Offset: 0x00005A2F
		public string Header { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007838 File Offset: 0x00005A38
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00007840 File Offset: 0x00005A40
		public string Style { get; set; }
	}
}
