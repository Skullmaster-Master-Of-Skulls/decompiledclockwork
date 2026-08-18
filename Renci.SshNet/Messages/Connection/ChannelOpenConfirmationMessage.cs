using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A0 RID: 160
	[Message("SSH_MSG_CHANNEL_OPEN_CONFIRMATION", 91)]
	public class ChannelOpenConfirmationMessage : ChannelMessage
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001DE2F File Offset: 0x0001C02F
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001DE37 File Offset: 0x0001C037
		public uint RemoteChannelNumber { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001DE40 File Offset: 0x0001C040
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001DE48 File Offset: 0x0001C048
		public uint InitialWindowSize { get; private set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001DE51 File Offset: 0x0001C051
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0001DE59 File Offset: 0x0001C059
		public uint MaximumPacketSize { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001DE62 File Offset: 0x0001C062
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + 4 + 4;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelOpenConfirmationMessage()
		{
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001DE70 File Offset: 0x0001C070
		public ChannelOpenConfirmationMessage(uint localChannelNumber, uint initialWindowSize, uint maximumPacketSize, uint remoteChannelNumber) : base(localChannelNumber)
		{
			this.InitialWindowSize = initialWindowSize;
			this.MaximumPacketSize = maximumPacketSize;
			this.RemoteChannelNumber = remoteChannelNumber;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001DE8F File Offset: 0x0001C08F
		protected override void LoadData()
		{
			base.LoadData();
			this.RemoteChannelNumber = base.ReadUInt32();
			this.InitialWindowSize = base.ReadUInt32();
			this.MaximumPacketSize = base.ReadUInt32();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001DEBB File Offset: 0x0001C0BB
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.RemoteChannelNumber);
			base.Write(this.InitialWindowSize);
			base.Write(this.MaximumPacketSize);
		}
	}
}
