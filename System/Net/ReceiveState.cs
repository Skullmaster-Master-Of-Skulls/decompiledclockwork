using System;

namespace System.Net
{
	// Token: 0x020004BF RID: 1215
	internal class ReceiveState
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x000963DB File Offset: 0x000953DB
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x04002559 RID: 9561
		private const int bufferSize = 1024;

		// Token: 0x0400255A RID: 9562
		internal ResponseDescription Resp;

		// Token: 0x0400255B RID: 9563
		internal int ValidThrough;

		// Token: 0x0400255C RID: 9564
		internal byte[] Buffer;

		// Token: 0x0400255D RID: 9565
		internal CommandStream Connection;
	}
}
