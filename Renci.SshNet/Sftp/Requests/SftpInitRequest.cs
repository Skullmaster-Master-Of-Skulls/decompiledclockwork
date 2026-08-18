using System;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000052 RID: 82
	internal class SftpInitRequest : SftpMessage
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000CACF File Offset: 0x0000ACCF
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Init;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000128A7 File Offset: 0x00010AA7
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x000128AF File Offset: 0x00010AAF
		public uint Version { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000128B8 File Offset: 0x00010AB8
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000128C2 File Offset: 0x00010AC2
		public SftpInitRequest(uint version)
		{
			this.Version = version;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000128D1 File Offset: 0x00010AD1
		protected override void LoadData()
		{
			base.LoadData();
			this.Version = base.ReadUInt32();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000128E5 File Offset: 0x00010AE5
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.Version);
		}
	}
}
