using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x02000289 RID: 649
	internal class SmtpDigestAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x0007BE23 File Offset: 0x0007A023
		internal SmtpDigestAuthenticationModule()
		{
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0007BE38 File Offset: 0x0007A038
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Authorization Authenticate(string challenge, NetworkCredential credential, object sessionCookie, string spn, ChannelBinding channelBindingToken)
		{
			Hashtable obj = this.sessions;
			Authorization result;
			lock (obj)
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

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0007BEE4 File Offset: 0x0007A0E4
		public string AuthenticationType
		{
			get
			{
				return "WDigest";
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0007BEEB File Offset: 0x0007A0EB
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x0400185B RID: 6235
		private Hashtable sessions = new Hashtable();
	}
}
