using System;

namespace Renci.SshNet.Sftp.Responses
{
	// Token: 0x02000040 RID: 64
	internal class StatVfsReplyInfo : ExtendedReplyInfo
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00011FBE File Offset: 0x000101BE
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00011FC6 File Offset: 0x000101C6
		public SftpFileSytemInformation Information { get; private set; }

		// Token: 0x060004CF RID: 1231 RVA: 0x00011FD0 File Offset: 0x000101D0
		protected override void LoadData()
		{
			base.LoadData();
			this.Information = new SftpFileSytemInformation(base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64(), base.ReadUInt64());
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void SaveData()
		{
			throw new NotImplementedException();
		}
	}
}
