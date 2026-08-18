using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000AA RID: 170
	[Message("SSH_MSG_CHANNEL_REQUEST", 98)]
	public class ChannelRequestMessage : ChannelMessage
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001E5CC File Offset: 0x0001C7CC
		public string RequestName
		{
			get
			{
				return this._requestName;
			}
			private set
			{
				this._requestName = value;
				this._requestNameBytes = SshData.Ascii.GetBytes(value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001E5E6 File Offset: 0x0001C7E6
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001E5EE File Offset: 0x0001C7EE
		public byte[] RequestData { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001E5F7 File Offset: 0x0001C7F7
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._requestNameBytes.Length + this.RequestData.Length;
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelRequestMessage()
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001E613 File Offset: 0x0001C813
		public ChannelRequestMessage(uint localChannelNumber, RequestInfo info) : base(localChannelNumber)
		{
			this.RequestName = info.RequestName;
			this.RequestData = info.GetBytes();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001E634 File Offset: 0x0001C834
		protected override void LoadData()
		{
			base.LoadData();
			this._requestNameBytes = base.ReadBinary();
			this._requestName = SshData.Ascii.GetString(this._requestNameBytes, 0, this._requestNameBytes.Length);
			this.RequestData = base.ReadBytes();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001E673 File Offset: 0x0001C873
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._requestNameBytes);
			base.Write(this.RequestData);
		}

		// Token: 0x0400032A RID: 810
		private string _requestName;

		// Token: 0x0400032B RID: 811
		private byte[] _requestNameBytes;
	}
}
