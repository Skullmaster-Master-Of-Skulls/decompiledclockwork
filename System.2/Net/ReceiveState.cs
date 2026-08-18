using System;

namespace System.Net
{
	// Token: 0x0200019A RID: 410
	internal class ReceiveState
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x0005393E File Offset: 0x00051B3E
		internal ReceiveState(CommandStream connection)
		{
			this.Connection = connection;
			this.Resp = new ResponseDescription();
			this.Buffer = new byte[1024];
			this.ValidThrough = 0;
		}

		// Token: 0x04001312 RID: 4882
		private const int bufferSize = 1024;

		// Token: 0x04001313 RID: 4883
		internal ResponseDescription Resp;

		// Token: 0x04001314 RID: 4884
		internal int ValidThrough;

		// Token: 0x04001315 RID: 4885
		internal byte[] Buffer;

		// Token: 0x04001316 RID: 4886
		internal CommandStream Connection;
	}
}
