using System;

namespace System.Windows.Forms
{
	// Token: 0x02000151 RID: 337
	public enum CloseReason
	{
		// Token: 0x0400078A RID: 1930
		None,
		// Token: 0x0400078B RID: 1931
		WindowsShutDown,
		// Token: 0x0400078C RID: 1932
		MdiFormClosing,
		// Token: 0x0400078D RID: 1933
		UserClosing,
		// Token: 0x0400078E RID: 1934
		TaskManagerClosing,
		// Token: 0x0400078F RID: 1935
		FormOwnerClosing,
		// Token: 0x04000790 RID: 1936
		ApplicationExitCall
	}
}
