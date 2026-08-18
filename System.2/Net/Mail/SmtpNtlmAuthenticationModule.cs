using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028F RID: 655
	internal class SmtpNtlmAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0007C7E8 File Offset: 0x0007A9E8
		internal SmtpNtlmAuthenticationModule()
		{
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0007C7FC File Offset: 0x0007A9FC
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Authorization Authenticate(string challenge, NetworkCredential credential, object sessionCookie, string spn, ChannelBinding channelBindingToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Authenticate", null);
			}
			Authorization result;
			try
			{
				Hashtable obj = this.sessions;
				lock (obj)
				{
					NTAuthentication ntauthentication = this.sessions[sessionCookie] as NTAuthentication;
					if (ntauthentication == null)
					{
						if (credential == null)
						{
							return null;
						}
						ntauthentication = (this.sessions[sessionCookie] = new NTAuthentication(false, "Ntlm", credential, spn, ContextFlags.Connection, channelBindingToken));
					}
					string outgoingBlob = ntauthentication.GetOutgoingBlob(challenge);
					if (!ntauthentication.IsCompleted)
					{
						result = new Authorization(outgoingBlob, false);
					}
					else
					{
						this.sessions.Remove(sessionCookie);
						result = new Authorization(outgoingBlob, true);
					}
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, this, "Authenticate", null);
				}
			}
			return result;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0007C8E8 File Offset: 0x0007AAE8
		public string AuthenticationType
		{
			get
			{
				return "ntlm";
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0007C8EF File Offset: 0x0007AAEF
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x04001862 RID: 6242
		private Hashtable sessions = new Hashtable();
	}
}
