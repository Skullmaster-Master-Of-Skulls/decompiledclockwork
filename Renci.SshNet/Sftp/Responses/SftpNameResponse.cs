using System;
using System.Collections.Generic;
using System.Text;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000045 RID: 69
	internal class SftpNameResponse : SftpResponse
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000120F0 File Offset: 0x000102F0
		public override SftpMessageTypes SftpMessageType
		{
			get
			{
				return SftpMessageTypes.Name;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000120F4 File Offset: 0x000102F4
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x000120FC File Offset: 0x000102FC
		public uint Count { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00012105 File Offset: 0x00010305
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0001210D File Offset: 0x0001030D
		public Encoding Encoding { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012116 File Offset: 0x00010316
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0001211E File Offset: 0x0001031E
		public KeyValuePair<string, SftpFileAttributes>[] Files { get; private set; }

		// Token: 0x060004ED RID: 1261 RVA: 0x00012127 File Offset: 0x00010327
		public SftpNameResponse(uint protocolVersion, Encoding encoding) : base(protocolVersion)
		{
			this.Files = new KeyValuePair<string, SftpFileAttributes>[0];
			this.Encoding = encoding;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00012144 File Offset: 0x00010344
		protected override void LoadData()
		{
			base.LoadData();
			this.Count = base.ReadUInt32();
			this.Files = new KeyValuePair<string, SftpFileAttributes>[this.Count];
			int num = 0;
			while ((long)num < (long)((ulong)this.Count))
			{
				string key = base.ReadString(this.Encoding);
				base.ReadString(this.Encoding);
				SftpFileAttributes value = base.ReadAttributes();
				this.Files[num] = new KeyValuePair<string, SftpFileAttributes>(key, value);
				num++;
			}
		}
	}
}
