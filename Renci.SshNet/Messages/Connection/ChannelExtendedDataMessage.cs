using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x0200009D RID: 157
	[Message("SSH_MSG_CHANNEL_EXTENDED_DATA", 95)]
	public class ChannelExtendedDataMessage : ChannelMessage
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001DD25 File Offset: 0x0001BF25
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0001DD2D File Offset: 0x0001BF2D
		public uint DataTypeCode { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001DD36 File Offset: 0x0001BF36
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0001DD3E File Offset: 0x0001BF3E
		public byte[] Data { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001DD47 File Offset: 0x0001BF47
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + 4 + this.Data.Length;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelExtendedDataMessage()
		{
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001DD5C File Offset: 0x0001BF5C
		public ChannelExtendedDataMessage(uint localChannelNumber, uint dataTypeCode, byte[] data) : base(localChannelNumber)
		{
			this.DataTypeCode = dataTypeCode;
			this.Data = data;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001DD73 File Offset: 0x0001BF73
		protected override void LoadData()
		{
			base.LoadData();
			this.DataTypeCode = base.ReadUInt32();
			this.Data = base.ReadBinary();
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001DD93 File Offset: 0x0001BF93
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.DataTypeCode);
			base.WriteBinaryString(this.Data);
		}
	}
}
