using System;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C8 RID: 200
	internal class RequestMessageNone : RequestMessage
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		public RequestMessageNone(ServiceName serviceName, string username) : base(serviceName, username, "none")
		{
		}
	}
}
