using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000106 RID: 262
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQMsgPropsValCtx
	{
		// Token: 0x04000873 RID: 2163
		internal int isDirty;

		// Token: 0x04000874 RID: 2164
		internal int isNull;

		// Token: 0x04000875 RID: 2165
		internal int msgState;

		// Token: 0x04000876 RID: 2166
		internal int deliveryMode;

		// Token: 0x04000877 RID: 2167
		internal int year;

		// Token: 0x04000878 RID: 2168
		internal int month;

		// Token: 0x04000879 RID: 2169
		internal int day;

		// Token: 0x0400087A RID: 2170
		internal int hour;

		// Token: 0x0400087B RID: 2171
		internal int min;

		// Token: 0x0400087C RID: 2172
		internal int sec;

		// Token: 0x0400087D RID: 2173
		internal int dequeueAttempts;

		// Token: 0x0400087E RID: 2174
		internal int delay;

		// Token: 0x0400087F RID: 2175
		internal int expiration;

		// Token: 0x04000880 RID: 2176
		internal int priority;

		// Token: 0x04000881 RID: 2177
		internal IntPtr pRecipients;

		// Token: 0x04000882 RID: 2178
		internal int numRecipients;

		// Token: 0x04000883 RID: 2179
		internal IntPtr pOrigMsgId;

		// Token: 0x04000884 RID: 2180
		internal int origMsgIdLen;

		// Token: 0x04000885 RID: 2181
		internal IntPtr pOrigMsgIdObject;
	}
}
