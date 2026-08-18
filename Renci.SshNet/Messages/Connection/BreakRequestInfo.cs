using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A9 RID: 169
	internal class BreakRequestInfo : RequestInfo
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001E55C File Offset: 0x0001C75C
		public override string RequestName
		{
			get
			{
				return "break";
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0001E563 File Offset: 0x0001C763
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x0001E56B File Offset: 0x0001C76B
		public uint BreakLength { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001E574 File Offset: 0x0001C774
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4;
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001E57E File Offset: 0x0001C77E
		public BreakRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001E58D File Offset: 0x0001C78D
		public BreakRequestInfo(uint breakLength) : this()
		{
			this.BreakLength = breakLength;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001E59C File Offset: 0x0001C79C
		protected override void LoadData()
		{
			base.LoadData();
			this.BreakLength = base.ReadUInt32();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.BreakLength);
		}

		// Token: 0x04000328 RID: 808
		public const string Name = "break";
	}
}
