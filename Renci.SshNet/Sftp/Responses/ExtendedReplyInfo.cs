using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x0200003F RID: 63
	internal abstract class ExtendedReplyInfo : SshData
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x00011FA7 File Offset: 0x000101A7
		protected override void LoadData()
		{
			base.ReadUInt32();
			base.ReadByte();
			base.ReadUInt32();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void SaveData()
		{
			throw new NotImplementedException();
		}
	}
}
