using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A5 RID: 165
	internal class DirectTcpipChannelInfo : ChannelOpenInfo
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001E215 File Offset: 0x0001C415
		public override string ChannelType
		{
			get
			{
				return "direct-tcpip";
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001E21C File Offset: 0x0001C41C
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001E237 File Offset: 0x0001C437
		public string HostToConnect
		{
			get
			{
				return SshData.Utf8.GetString(this._hostToConnect, 0, this._hostToConnect.Length);
			}
			private set
			{
				this._hostToConnect = SshData.Utf8.GetBytes(value);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001E24A File Offset: 0x0001C44A
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001E252 File Offset: 0x0001C452
		public uint PortToConnect { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001E25B File Offset: 0x0001C45B
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001E276 File Offset: 0x0001C476
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

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001E289 File Offset: 0x0001C489
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001E291 File Offset: 0x0001C491
		public uint OriginatorPort { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001E29A File Offset: 0x0001C49A
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._hostToConnect.Length + 4 + 4 + this._originatorAddress.Length + 4;
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		public DirectTcpipChannelInfo(byte[] data)
		{
			base.Load(data);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001E2CB File Offset: 0x0001C4CB
		public DirectTcpipChannelInfo(string hostToConnect, uint portToConnect, string originatorAddress, uint originatorPort)
		{
			this.HostToConnect = hostToConnect;
			this.PortToConnect = portToConnect;
			this.OriginatorAddress = originatorAddress;
			this.OriginatorPort = originatorPort;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		protected override void LoadData()
		{
			base.LoadData();
			this._hostToConnect = base.ReadBinary();
			this.PortToConnect = base.ReadUInt32();
			this._originatorAddress = base.ReadBinary();
			this.OriginatorPort = base.ReadUInt32();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001E328 File Offset: 0x0001C528
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this._hostToConnect);
			base.Write(this.PortToConnect);
			base.WriteBinaryString(this._originatorAddress);
			base.Write(this.OriginatorPort);
		}

		// Token: 0x0400031A RID: 794
		private byte[] _hostToConnect;

		// Token: 0x0400031B RID: 795
		private byte[] _originatorAddress;

		// Token: 0x0400031C RID: 796
		public const string NAME = "direct-tcpip";
	}
}
