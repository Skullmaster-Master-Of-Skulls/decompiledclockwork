using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000DC RID: 220
	[Message("SSH_MSG_SERVICE_REQUEST", 5)]
	public class ServiceRequestMessage : Message
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00020199 File Offset: 0x0001E399
		public ServiceName ServiceName
		{
			get
			{
				return this._serviceName.ToServiceName();
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000201A6 File Offset: 0x0001E3A6
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._serviceName.Length;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000201B9 File Offset: 0x0001E3B9
		public ServiceRequestMessage(ServiceName serviceName)
		{
			this._serviceName = serviceName.ToArray();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001F4A6 File Offset: 0x0001D6A6
		protected override void LoadData()
		{
			throw new InvalidOperationException("Load data is not supported.");
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000201CD File Offset: 0x0001E3CD
		protected override void SaveData()
		{
			base.WriteBinaryString(this._serviceName);
		}

		// Token: 0x040003B9 RID: 953
		private readonly byte[] _serviceName;
	}
}
