using System;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp.Requests
{
	// Token: 0x02000049 RID: 73
	internal class FStatVfsRequest : SftpExtendedRequest
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001230C File Offset: 0x0001050C
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00012314 File Offset: 0x00010514
		public byte[] Handle { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001231D File Offset: 0x0001051D
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.Handle.Length;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00012330 File Offset: 0x00010530
		public FStatVfsRequest(uint protocolVersion, uint requestId, byte[] handle, Action<SftpExtendedReplyResponse> extendedAction, Action<SftpStatusResponse> statusAction) : base(protocolVersion, requestId, statusAction, "fstatvfs@openssh.com")
		{
			this.Handle = handle;
			base.SetAction(extendedAction);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00012350 File Offset: 0x00010550
		protected override void SaveData()
		{
			base.SaveData();
			base.WriteBinaryString(this.Handle);
		}
	}
}
