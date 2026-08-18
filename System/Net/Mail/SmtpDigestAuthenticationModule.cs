using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006CB RID: 1739
	internal class SmtpDigestAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x000E57FF File Offset: 0x000E47FF
		internal SmtpDigestAuthenticationModule()
		{
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000E5814 File Offset: 0x000E4814
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		public Authorization Authenticate(string challenge, NetworkCredential credential, object sessionCookie, string spn, ChannelBinding channelBindingToken)
		{
			Authorization result;
			lock (this.sessions)
			{
				NTAuthentication ntauthentication = this.sessions[sessionCookie] as NTAuthentication;
				if (ntauthentication == null)
				{
					if (credential == null)
					{
						return null;
					}
					ntauthentication = (this.sessions[sessionCookie] = new NTAuthentication(false, "WDigest", credential, spn, ContextFlags.Connection, channelBindingToken));
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
			return result;
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000E58B8 File Offset: 0x000E48B8
		public string AuthenticationType
		{
			get
			{
				return "WDigest";
			}
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000E58BF File Offset: 0x000E48BF
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x04003117 RID: 12567
		private Hashtable sessions = new Hashtable();
	}
}
