using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006D1 RID: 1745
	internal class SmtpNtlmAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x060035EE RID: 13806 RVA: 0x000E61D8 File Offset: 0x000E51D8
		internal SmtpNtlmAuthenticationModule()
		{
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000E61EC File Offset: 0x000E51EC
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		public Authorization Authenticate(string challenge, NetworkCredential credential, object sessionCookie, string spn, ChannelBinding channelBindingToken)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, this, "Authenticate", null);
			}
			Authorization result;
			try
			{
				lock (this.sessions)
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

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000E62CC File Offset: 0x000E52CC
		public string AuthenticationType
		{
			get
			{
				return "ntlm";
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000E62D3 File Offset: 0x000E52D3
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x0400311E RID: 12574
		private Hashtable sessions = new Hashtable();
	}
}
