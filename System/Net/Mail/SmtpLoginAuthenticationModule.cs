using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x020006CF RID: 1743
	internal class SmtpLoginAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x060035E5 RID: 13797 RVA: 0x000E5E9B File Offset: 0x000E4E9B
		internal SmtpLoginAuthenticationModule()
		{
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000E5EB0 File Offset: 0x000E4EB0
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
							result = new Authorization(Convert.ToBase64String(Encoding.ASCII.GetBytes(text)), false);
						}
					}
					else
					{
						this.sessions.Remove(sessionCookie);
						result = new Authorization(Convert.ToBase64String(Encoding.ASCII.GetBytes(networkCredential.Password)), true);
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

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000E5FC0 File Offset: 0x000E4FC0
		public string AuthenticationType
		{
			get
			{
				return "login";
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000E5FC7 File Offset: 0x000E4FC7
		public void CloseContext(object sessionCookie)
		{
		}

		// Token: 0x0400311C RID: 12572
		private Hashtable sessions = new Hashtable();
	}
}
