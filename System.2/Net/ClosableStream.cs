using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000226 RID: 550
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x0006B8D7 File Offset: 0x00069AD7
		internal ClosableStream(Stream stream, EventHandler onClose) : base(stream)
		{
			this.onClose = onClose;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0006B8E7 File Offset: 0x00069AE7
		public override void Close()
		{
			if (Interlocked.Increment(ref this.closed) == 1 && this.onClose != null)
			{
				this.onClose(this, new EventArgs());
			}
		}

		// Token: 0x0400162B RID: 5675
		private EventHandler onClose;

		// Token: 0x0400162C RID: 5676
		private int closed;
	}
}
