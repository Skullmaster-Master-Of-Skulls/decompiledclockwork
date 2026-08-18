using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004B RID: 75
	internal abstract class SmiStream
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002D0 RID: 720
		public abstract bool CanRead { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002D1 RID: 721
		public abstract bool CanSeek { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002D2 RID: 722
		public abstract bool CanWrite { get; }

		// Token: 0x060002D3 RID: 723
		public abstract long GetLength(SmiEventSink sink);

		// Token: 0x060002D4 RID: 724
		public abstract long GetPosition(SmiEventSink sink);

		// Token: 0x060002D5 RID: 725
		public abstract void SetPosition(SmiEventSink sink, long position);

		// Token: 0x060002D6 RID: 726
		public abstract void Flush(SmiEventSink sink);

		// Token: 0x060002D7 RID: 727
		public abstract long Seek(SmiEventSink sink, long offset, SeekOrigin origin);

		// Token: 0x060002D8 RID: 728
		public abstract void SetLength(SmiEventSink sink, long value);

		// Token: 0x060002D9 RID: 729
		public abstract int Read(SmiEventSink sink, byte[] buffer, int offset, int count);

		// Token: 0x060002DA RID: 730
		public abstract void Write(SmiEventSink sink, byte[] buffer, int offset, int count);

		// Token: 0x060002DB RID: 731
		public abstract int Read(SmiEventSink sink, char[] buffer, int offset, int count);

		// Token: 0x060002DC RID: 732
		public abstract void Write(SmiEventSink sink, char[] buffer, int offset, int count);
	}
}
