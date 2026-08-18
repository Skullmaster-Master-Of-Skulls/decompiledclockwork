using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000685 RID: 1669
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x060033B1 RID: 13233 RVA: 0x000DA467 File Offset: 0x000D9467
		internal ClosableStream(Stream stream, EventHandler onClose) : base(stream)
		{
			this.onClose = onClose;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000DA477 File Offset: 0x000D9477
		public override void Close()
		{
			if (Interlocked.Increment(ref this.closed) == 1 && this.onClose != null)
			{
				this.onClose(this, new EventArgs());
			}
		}

		// Token: 0x04002FB5 RID: 12213
		private EventHandler onClose;

		// Token: 0x04002FB6 RID: 12214
		private int closed;
	}
}
