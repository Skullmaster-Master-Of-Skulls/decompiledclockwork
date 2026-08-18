using System;
using System.Runtime.InteropServices;

namespace EmailClassLibrary
{
	// Token: 0x02000009 RID: 9
	[StructLayout(LayoutKind.Sequential)]
	public class MapiMessage
	{
		// Token: 0x0400001F RID: 31
		public int reserved;

		// Token: 0x04000020 RID: 32
		public string subject;

		// Token: 0x04000021 RID: 33
		public string noteText;

		// Token: 0x04000022 RID: 34
		public string messageType;

		// Token: 0x04000023 RID: 35
		public string dateReceived;

		// Token: 0x04000024 RID: 36
		public string conversationID;

		// Token: 0x04000025 RID: 37
		public int flags;

		// Token: 0x04000026 RID: 38
		public IntPtr originator;

		// Token: 0x04000027 RID: 39
		public int recipCount;

		// Token: 0x04000028 RID: 40
		public IntPtr recips;

		// Token: 0x04000029 RID: 41
		public int fileCount;

		// Token: 0x0400002A RID: 42
		public IntPtr files;
	}
}
