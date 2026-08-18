using System;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000039 RID: 57
	public abstract class LdapMessageQueue
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000BFD0 File Offset: 0x0000AFD0
		internal virtual string DebugName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000BFE8 File Offset: 0x0000AFE8
		internal virtual MessageAgent MessageAgent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000C000 File Offset: 0x0000B000
		public virtual int[] MessageIDs
		{
			get
			{
				return this.agent.MessageIDs;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C01C File Offset: 0x0000B01C
		internal LdapMessageQueue(string myname, MessageAgent agent)
		{
			this.agent = agent;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000C044 File Offset: 0x0000B044
		public virtual LdapMessage getResponse()
		{
			return this.getResponse(null);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C05C File Offset: 0x0000B05C
		public virtual LdapMessage getResponse(int msgid)
		{
			return this.getResponse(new Integer32(msgid));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C07C File Offset: 0x0000B07C
		private LdapMessage getResponse(Integer32 msgid)
		{
			object ldapMessage;
			LdapMessage result;
			if ((ldapMessage = this.agent.getLdapMessage(msgid)) == null)
			{
				result = null;
			}
			else if (ldapMessage is LdapResponse)
			{
				result = (LdapMessage)ldapMessage;
			}
			else
			{
				RfcLdapMessage rfcLdapMessage = (RfcLdapMessage)ldapMessage;
				int type = rfcLdapMessage.Type;
				LdapMessage ldapMessage2;
				if (type != 4)
				{
					if (type != 19)
					{
						switch (type)
						{
						case 24:
						{
							ExtResponseFactory extResponseFactory = new ExtResponseFactory();
							ldapMessage2 = ExtResponseFactory.convertToExtendedResponse(rfcLdapMessage);
							break;
						}
						case 25:
							ldapMessage2 = IntermediateResponseFactory.convertToIntermediateResponse(rfcLdapMessage);
							break;
						default:
							ldapMessage2 = new LdapResponse(rfcLdapMessage);
							break;
						}
					}
					else
					{
						ldapMessage2 = new LdapSearchResultReference(rfcLdapMessage);
					}
				}
				else
				{
					ldapMessage2 = new LdapSearchResult(rfcLdapMessage);
				}
				result = ldapMessage2;
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000C118 File Offset: 0x0000B118
		public virtual bool isResponseReceived()
		{
			return this.agent.isResponseReceived();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C134 File Offset: 0x0000B134
		public virtual bool isResponseReceived(int msgid)
		{
			return this.agent.isResponseReceived(msgid);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C154 File Offset: 0x0000B154
		public virtual bool isComplete(int msgid)
		{
			return this.agent.isComplete(msgid);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C174 File Offset: 0x0000B174
		static LdapMessageQueue()
		{
			LdapMessageQueue.nameLock = new object();
		}

		// Token: 0x04000112 RID: 274
		internal MessageAgent agent;

		// Token: 0x04000113 RID: 275
		internal string name = "";

		// Token: 0x04000114 RID: 276
		internal static object nameLock;

		// Token: 0x04000115 RID: 277
		internal static int queueNum = 0;
	}
}
