using System;

namespace System.Web
{
	// Token: 0x0200006D RID: 109
	internal sealed class FileChangeEvent : EventArgs
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x0000A7E9 File Offset: 0x000089E9
		internal FileChangeEvent(FileAction action, string fileName)
		{
			this.Action = action;
			this.FileName = fileName;
		}

		// Token: 0x040001F9 RID: 505
		internal FileAction Action;

		// Token: 0x040001FA RID: 506
		internal string FileName;
	}
}
