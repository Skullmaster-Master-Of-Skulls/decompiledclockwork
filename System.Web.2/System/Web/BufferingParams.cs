using System;

namespace System.Web
{
	// Token: 0x020000C0 RID: 192
	internal static class BufferingParams
	{
		// Token: 0x040004EB RID: 1259
		internal static readonly int INTEGRATED_MODE_BUFFER_SIZE = 16384 - 4 * IntPtr.Size;

		// Token: 0x040004EC RID: 1260
		internal const int OUTPUT_BUFFER_SIZE = 31744;

		// Token: 0x040004ED RID: 1261
		internal const int MAX_FREE_BYTES_TO_CACHE = 4096;

		// Token: 0x040004EE RID: 1262
		internal const int MAX_FREE_OUTPUT_BUFFERS = 64;

		// Token: 0x040004EF RID: 1263
		internal const int CHAR_BUFFER_SIZE = 1024;

		// Token: 0x040004F0 RID: 1264
		internal const int MAX_FREE_CHAR_BUFFERS = 64;

		// Token: 0x040004F1 RID: 1265
		internal const int MAX_BYTES_TO_COPY = 128;

		// Token: 0x040004F2 RID: 1266
		internal const int MAX_RESOURCE_BYTES_TO_COPY = 4096;

		// Token: 0x040004F3 RID: 1267
		internal const int INT_BUFFER_SIZE = 128;

		// Token: 0x040004F4 RID: 1268
		internal const int INTPTR_BUFFER_SIZE = 128;
	}
}
