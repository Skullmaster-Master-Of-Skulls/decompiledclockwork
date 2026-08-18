using System;

namespace MailBee.SmtpMail
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	public class SmtpServerCollection : SortableByPriorityCollection
	{
		// Token: 0x170003F9 RID: 1017
		public SmtpServer this[int index]
		{
			get
			{
				return (SmtpServer)base.List[index];
			}
			set
			{
				base.List[index] = value;
				base.SortByPriority();
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00031F2B File Offset: 0x00030F2B
		public void Add(SmtpServer server)
		{
			base.List.Add(server);
			base.SortByPriority();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00031F40 File Offset: 0x00030F40
		public SmtpServer Add(string serverName)
		{
			SmtpServer smtpServer = new SmtpServer(serverName);
			base.List.Add(smtpServer);
			base.SortByPriority();
			return smtpServer;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00031F68 File Offset: 0x00030F68
		public SmtpServer Add(string serverName, int serverPort, int priority)
		{
			SmtpServer smtpServer = new SmtpServer(serverName, serverPort, priority);
			base.List.Add(smtpServer);
			base.SortByPriority();
			return smtpServer;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00031F94 File Offset: 0x00030F94
		public SmtpServer Add(string serverName, string accountName, string password)
		{
			SmtpServer smtpServer = new SmtpServer(serverName, accountName, password);
			base.List.Add(smtpServer);
			base.SortByPriority();
			return smtpServer;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00031FC0 File Offset: 0x00030FC0
		public SmtpServer Add(string serverName, string accountName, string password, AuthenticationMethods authMethods)
		{
			SmtpServer smtpServer = new SmtpServer(serverName, accountName, password, authMethods);
			base.List.Add(smtpServer);
			base.SortByPriority();
			return smtpServer;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00031FEC File Offset: 0x00030FEC
		public SmtpServer Add(string serverName, int serverPort, int priority, int timeout, bool pipelining, AuthenticationMethods authMethods, string accountName, string password, bool allowRefusedRecipients, string helloDomain, ExtendedSmtpOptions smtpOptions)
		{
			SmtpServer smtpServer = new SmtpServer(serverName, serverPort, priority, timeout, pipelining, authMethods, accountName, password, allowRefusedRecipients, helloDomain, smtpOptions);
			base.List.Add(smtpServer);
			base.SortByPriority();
			return smtpServer;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00032026 File Offset: 0x00031026
		internal void a(SmtpServer A_0)
		{
			base.List.Add(A_0);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00032035 File Offset: 0x00031035
		public void Remove(SmtpServer server)
		{
			base.List.Remove(server);
		}
	}
}
