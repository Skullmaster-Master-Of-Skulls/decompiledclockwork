using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Transport;
using Renci.SshNet.NetConf;
using Renci.SshNet.Security;
using Renci.SshNet.Sftp;

namespace Renci.SshNet
{
	// Token: 0x02000016 RID: 22
	internal class ServiceFactory : IServiceFactory
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00003D88 File Offset: 0x00001F88
		public IClientAuthentication CreateClientAuthentication()
		{
			return new ClientAuthentication();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003D8F File Offset: 0x00001F8F
		public ISession CreateSession(ConnectionInfo connectionInfo)
		{
			return new Session(connectionInfo, this);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003D98 File Offset: 0x00001F98
		public ISftpSession CreateSftpSession(ISession session, TimeSpan operationTimeout, Encoding encoding)
		{
			return new SftpSession(session, operationTimeout, encoding);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003DA2 File Offset: 0x00001FA2
		public PipeStream CreatePipeStream()
		{
			return new PipeStream();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003DAC File Offset: 0x00001FAC
		public IKeyExchange CreateKeyExchange(IDictionary<string, Type> clientAlgorithms, string[] serverAlgorithms)
		{
			if (clientAlgorithms == null)
			{
				throw new ArgumentNullException("clientAlgorithms");
			}
			if (serverAlgorithms == null)
			{
				throw new ArgumentNullException("serverAlgorithms");
			}
			Type type = (from c in clientAlgorithms
			from s in serverAlgorithms
			where s == c.Key
			select c.Value).FirstOrDefault<Type>();
			if (type == null)
			{
				throw new SshConnectionException("Failed to negotiate key exchange algorithm.", DisconnectReason.KeyExchangeFailed);
			}
			return type.CreateInstance<IKeyExchange>();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003E7F File Offset: 0x0000207F
		public INetConfSession CreateNetConfSession(ISession session, TimeSpan operationTimeout)
		{
			return new NetConfSession(session, operationTimeout);
		}
	}
}
