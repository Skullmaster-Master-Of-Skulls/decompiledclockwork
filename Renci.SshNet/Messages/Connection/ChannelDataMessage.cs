using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x0200009B RID: 155
	[Message("SSH_MSG_CHANNEL_DATA", 94)]
	public class ChannelDataMessage : ChannelMessage
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001DC3D File Offset: 0x0001BE3D
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001DC45 File Offset: 0x0001BE45
		public byte[] Data { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001DC4E File Offset: 0x0001BE4E
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001DC56 File Offset: 0x0001BE56
		public int Offset { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001DC5F File Offset: 0x0001BE5F
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001DC67 File Offset: 0x0001BE67
		public int Size { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001DC70 File Offset: 0x0001BE70
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Size;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelDataMessage()
		{
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001DC81 File Offset: 0x0001BE81
		public ChannelDataMessage(uint localChannelNumber, byte[] data) : base(localChannelNumber)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.Data = data;
			this.Offset = 0;
			this.Size = data.Length;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001DCAF File Offset: 0x0001BEAF
		public ChannelDataMessage(uint localChannelNumber, byte[] data, int offset, int size) : base(localChannelNumber)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.Data = data;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		protected override void LoadData()
		{
			base.LoadData();
			this.Data = base.ReadBinary();
			this.Offset = 0;
			this.Size = this.Data.Length;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001DD05 File Offset: 0x0001BF05
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinary(this.Data, this.Offset, this.Size);
		}

		// Token: 0x040002FD RID: 765
		internal const byte MessageNumber = 94;
	}
}
