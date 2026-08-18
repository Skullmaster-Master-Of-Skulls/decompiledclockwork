using System;
using System.Threading;

namespace Renci.SshNet
{
	// Token: 0x02000007 RID: 7
	public class CommandAsyncResult : IAsyncResult
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000027FD File Offset: 0x000009FD
		internal CommandAsyncResult()
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002805 File Offset: 0x00000A05
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000280D File Offset: 0x00000A0D
		public int BytesReceived { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002816 File Offset: 0x00000A16
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000281E File Offset: 0x00000A1E
		public int BytesSent { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002827 File Offset: 0x00000A27
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000282F File Offset: 0x00000A2F
		public object AsyncState { get; internal set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002838 File Offset: 0x00000A38
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002840 File Offset: 0x00000A40
		public WaitHandle AsyncWaitHandle { get; internal set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002849 File Offset: 0x00000A49
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002851 File Offset: 0x00000A51
		public bool CompletedSynchronously { get; internal set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000285A File Offset: 0x00000A5A
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002862 File Offset: 0x00000A62
		public bool IsCompleted { get; internal set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000286B File Offset: 0x00000A6B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002873 File Offset: 0x00000A73
		internal bool EndCalled { get; set; }
	}
}
