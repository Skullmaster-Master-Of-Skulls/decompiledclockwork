using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A6 RID: 166
	internal class ForwardedTcpipChannelInfo : ChannelOpenInfo
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		public ForwardedTcpipChannelInfo(byte[] data)
		{
			base.Load(data);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001E360 File Offset: 0x0001C560
		public ForwardedTcpipChannelInfo(string connectedAddress, uint connectedPort, string originatorAddress, uint originatorPort)
		{
			this.ConnectedAddress = connectedAddress;
			this.ConnectedPort = connectedPort;
			this.OriginatorAddress = originatorAddress;
			this.OriginatorPort = originatorPort;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001E385 File Offset: 0x0001C585
		public override string ChannelType
		{
			get
			{
				return "forwarded-tcpip";
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001E38C File Offset: 0x0001C58C
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0001E3A7 File Offset: 0x0001C5A7
		public string ConnectedAddress
		{
			get
			{
				return SshData.Utf8.GetString(this._connectedAddress, 0, this._connectedAddress.Length);
			}
			private set
			{
				this._connectedAddress = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001E3BA File Offset: 0x0001C5BA
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001E3C2 File Offset: 0x0001C5C2
		public uint ConnectedPort { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001E3CB File Offset: 0x0001C5CB
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0001E3E6 File Offset: 0x0001C5E6
		public string OriginatorAddress
		{
			get
			{
				return SshData.Utf8.GetString(this._originatorAddress, 0, this._originatorAddress.Length);
			}
			private set
			{
				this._originatorAddress = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001E3F9 File Offset: 0x0001C5F9
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0001E401 File Offset: 0x0001C601
		public uint OriginatorPort { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001E40A File Offset: 0x0001C60A
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._connectedAddress.Length + 4 + 4 + this._originatorAddress.Length + 4;
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001E42C File Offset: 0x0001C62C
		protected override void LoadData()
		{
			base.LoadData();
			this._connectedAddress = base.ReadBinary();
			this.ConnectedPort = base.ReadUInt32();
			this._originatorAddress = base.ReadBinary();
			this.OriginatorPort = base.ReadUInt32();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001E464 File Offset: 0x0001C664
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._connectedAddress);
			base.Write(this.ConnectedPort);
			base.WriteBinaryString(this._originatorAddress);
			base.Write(this.OriginatorPort);
		}

		// Token: 0x0400031F RID: 799
		private byte[] _connectedAddress;

		// Token: 0x04000320 RID: 800
		private byte[] _originatorAddress;

		// Token: 0x04000321 RID: 801
		public const string NAME = "forwarded-tcpip";
	}
}
