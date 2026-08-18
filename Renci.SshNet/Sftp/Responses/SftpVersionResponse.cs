using System;
using System.Collections.Generic;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000048 RID: 72
	internal class SftpVersionResponse : SftpMessage
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00012297 File Offset: 0x00010497
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Version;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001229A File Offset: 0x0001049A
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x000122A2 File Offset: 0x000104A2
		public uint Version { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000122AB File Offset: 0x000104AB
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x000122B3 File Offset: 0x000104B3
		public IDictionary<string, string> Extentions { get; private set; }

		// Token: 0x06000504 RID: 1284 RVA: 0x000122BC File Offset: 0x000104BC
		protected override void LoadData()
		{
			base.LoadData();
			this.Version = base.ReadUInt32();
			this.Extentions = base.ReadExtensionPair();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000122DC File Offset: 0x000104DC
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.Version);
			if (this.Extentions != null)
			{
				base.Write(this.Extentions);
			}
		}
	}
}
