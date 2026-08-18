using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B2 RID: 178
	public abstract class RequestInfo : SshData
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000853 RID: 2131
		public abstract string RequestName { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001EB64 File Offset: 0x0001CD64
		public bool WantReply { get; protected set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001EB6D File Offset: 0x0001CD6D
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 1;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001EB77 File Offset: 0x0001CD77
		protected override void LoadData()
		{
			this.WantReply = base.ReadBoolean();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001EB85 File Offset: 0x0001CD85
		protected override void SaveData()
		{
			base.Write(this.WantReply);
		}
	}
}
