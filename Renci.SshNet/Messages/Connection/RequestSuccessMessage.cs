using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000BE RID: 190
	[Message("SSH_MSG_REQUEST_SUCCESS", 81)]
	public class RequestSuccessMessage : Message
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0001F060 File Offset: 0x0001D260
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x0001F068 File Offset: 0x0001D268
		public uint? BoundPort { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001F074 File Offset: 0x0001D274
		protected override int BufferCapacity
		{
			get
			{
				int num = base.BufferCapacity;
				if (this.BoundPort != null)
				{
					num += 4;
				}
				return num;
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		public RequestSuccessMessage()
		{
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001F09D File Offset: 0x0001D29D
		public RequestSuccessMessage(uint boundPort)
		{
			this.BoundPort = new uint?(boundPort);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001F0B1 File Offset: 0x0001D2B1
		protected override void LoadData()
		{
			if (!base.IsEndOfData)
			{
				this.BoundPort = new uint?(base.ReadUInt32());
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		protected override void SaveData()
		{
			if (this.BoundPort != null)
			{
				base.Write(this.BoundPort.Value);
			}
		}
	}
}
