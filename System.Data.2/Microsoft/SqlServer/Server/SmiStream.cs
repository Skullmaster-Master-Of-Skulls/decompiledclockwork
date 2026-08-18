using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004B RID: 75
	internal abstract class SmiStream
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000302 RID: 770
		public abstract bool CanRead { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000303 RID: 771
		public abstract bool CanSeek { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000304 RID: 772
		public abstract bool CanWrite { get; }

		// Token: 0x06000305 RID: 773
		public abstract long GetLength(SmiEventSink sink);

		// Token: 0x06000306 RID: 774
		public abstract long GetPosition(SmiEventSink sink);

		// Token: 0x06000307 RID: 775
		public abstract void SetPosition(SmiEventSink sink, long position);

		// Token: 0x06000308 RID: 776
		public abstract void Flush(SmiEventSink sink);

		// Token: 0x06000309 RID: 777
		public abstract long Seek(SmiEventSink sink, long offset, SeekOrigin origin);

		// Token: 0x0600030A RID: 778
		public abstract void SetLength(SmiEventSink sink, long value);

		// Token: 0x0600030B RID: 779
		public abstract int Read(SmiEventSink sink, byte[] buffer, int offset, int count);

		// Token: 0x0600030C RID: 780
		public abstract void Write(SmiEventSink sink, byte[] buffer, int offset, int count);

		// Token: 0x0600030D RID: 781
		public abstract int Read(SmiEventSink sink, char[] buffer, int offset, int count);

		// Token: 0x0600030E RID: 782
		public abstract void Write(SmiEventSink sink, char[] buffer, int offset, int count);
	}
}
