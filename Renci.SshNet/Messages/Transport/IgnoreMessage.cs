using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D2 RID: 210
	[Message("SSH_MSG_IGNORE", 2)]
	public class IgnoreMessage : Message
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001FB88 File Offset: 0x0001DD88
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0001FB90 File Offset: 0x0001DD90
		public byte[] Data { get; private set; }

		// Token: 0x06000935 RID: 2357 RVA: 0x0001FB99 File Offset: 0x0001DD99
		public IgnoreMessage()
		{
			this.Data = Array<byte>.Empty;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Data.Length;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001FBBF File Offset: 0x0001DDBF
		public IgnoreMessage(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.Data = data;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001FBDC File Offset: 0x0001DDDC
		protected override void LoadData()
		{
			this.Data = base.ReadBinary();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001FBEA File Offset: 0x0001DDEA
		protected override void SaveData()
		{
			base.WriteBinaryString(this.Data);
		}

		// Token: 0x04000398 RID: 920
		internal const byte MessageNumber = 2;
	}
}
