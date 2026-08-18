using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000AF RID: 175
	internal class ExitStatusRequestInfo : RequestInfo
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001E982 File Offset: 0x0001CB82
		public override string RequestName
		{
			get
			{
				return "exit-status";
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001E989 File Offset: 0x0001CB89
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001E991 File Offset: 0x0001CB91
		public uint ExitStatus { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001E574 File Offset: 0x0001C774
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001E69A File Offset: 0x0001C89A
		public ExitStatusRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001E99A File Offset: 0x0001CB9A
		public ExitStatusRequestInfo(uint exitStatus) : this()
		{
			this.ExitStatus = exitStatus;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001E9A9 File Offset: 0x0001CBA9
		protected override void LoadData()
		{
			base.LoadData();
			this.ExitStatus = base.ReadUInt32();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E9BD File Offset: 0x0001CBBD
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.ExitStatus);
		}

		// Token: 0x04000339 RID: 825
		public const string Name = "exit-status";
	}
}
