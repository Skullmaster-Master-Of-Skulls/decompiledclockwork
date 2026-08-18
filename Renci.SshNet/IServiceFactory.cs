using System;
using System.Collections.Generic;
using System.Text;
using Renci.SshNet.Common;
using Renci.SshNet.NetConf;
using Renci.SshNet.Security;
using Renci.SshNet.Sftp;

namespace Renci.SshNet
{
	// Token: 0x02000013 RID: 19
	internal interface IServiceFactory
	{
		// Token: 0x060000C6 RID: 198
		IClientAuthentication CreateClientAuthentication();

		// Token: 0x060000C7 RID: 199
		ISession CreateSession(ConnectionInfo connectionInfo);

		// Token: 0x060000C8 RID: 200
		ISftpSession CreateSftpSession(ISession session, TimeSpan operationTimeout, Encoding encoding);

		// Token: 0x060000C9 RID: 201
		PipeStream CreatePipeStream();

		// Token: 0x060000CA RID: 202
		IKeyExchange CreateKeyExchange(IDictionary<string, Type> clientAlgorithms, string[] serverAlgorithms);

		// Token: 0x060000CB RID: 203
		INetConfSession CreateNetConfSession(ISession session, TimeSpan operationTimeout);
	}
}
