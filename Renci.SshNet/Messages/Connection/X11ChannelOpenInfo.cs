using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A8 RID: 168
	internal class X11ChannelOpenInfo : ChannelOpenInfo
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001E4AB File Offset: 0x0001C6AB
		public override string ChannelType
		{
			get
			{
				return "x11";
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001E4B2 File Offset: 0x0001C6B2
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0001E4CD File Offset: 0x0001C6CD
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		public uint OriginatorPort { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001E4F1 File Offset: 0x0001C6F1
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._originatorAddress.Length + 4;
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		public X11ChannelOpenInfo(byte[] data)
		{
			base.Load(data);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001E506 File Offset: 0x0001C706
		public X11ChannelOpenInfo(string originatorAddress, uint originatorPort)
		{
			this.OriginatorAddress = originatorAddress;
			this.OriginatorPort = originatorPort;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001E51C File Offset: 0x0001C71C
		protected override void LoadData()
		{
			base.LoadData();
			this._originatorAddress = base.ReadBinary();
			this.OriginatorPort = base.ReadUInt32();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001E53C File Offset: 0x0001C73C
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._originatorAddress);
			base.Write(this.OriginatorPort);
		}

		// Token: 0x04000325 RID: 805
		private byte[] _originatorAddress;

		// Token: 0x04000326 RID: 806
		public const string Name = "x11";
	}
}
