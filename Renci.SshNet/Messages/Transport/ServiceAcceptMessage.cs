using System;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000DB RID: 219
	[Message("SSH_MSG_SERVICE_ACCEPT", 6)]
	public class ServiceAcceptMessage : Message
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00020175 File Offset: 0x0001E375
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0002017D File Offset: 0x0001E37D
		public ServiceName ServiceName { get; private set; }

		// Token: 0x06000989 RID: 2441 RVA: 0x00020186 File Offset: 0x0001E386
		protected override void LoadData()
		{
			this.ServiceName = base.ReadBinary().ToServiceName();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void SaveData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040003B7 RID: 951
		internal const byte MessageNumber = 6;
	}
}
