using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200028D RID: 653
	internal class SmtpLoginAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x06001874 RID: 6260 RVA: 0x0007C4A3 File Offset: 0x0007A6A3
		internal SmtpLoginAuthenticationModule()
		{
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0007C4B8 File Offset: 0x0007A6B8
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
					NetworkCredential networkCredential = this.sessions[sessionCookie] as NetworkCredential;
					if (networkCredential == null)
					{
						if (credential == null || credential is SystemNetworkCredential)
						{
							result = null;
						}
						else
						{
							this.sessions[sessionCookie] = credential;
							string text = credential.UserName;
							string domain = credential.Domain;
							if (domain != null && domain.Length > 0)
							{
								text = domain + "\\" + text;
							}
							result = new Authorization(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)), false);
						}
					}
					else
					{
						this.sessions.Remove(sessionCookie);
						result = new Authorization(Convert.ToBase64String(Encoding.UTF8.GetBytes(networkCredential.Password)), true);
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

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0007C5D8 File Offset: 0x0007A7D8
		public string AuthenticationType
		{
			get
			{
				return "login";
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0007C5DF File Offset: 0x0007A7DF
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x04001860 RID: 6240
		private Hashtable sessions = new Hashtable();
	}
}
