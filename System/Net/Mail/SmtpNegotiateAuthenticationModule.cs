using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006D0 RID: 1744
	internal class SmtpNegotiateAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x060035E9 RID: 13801 RVA: 0x000E5FC9 File Offset: 0x000E4FC9
		internal SmtpNegotiateAuthenticationModule()
		{
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000E5FDC File Offset: 0x000E4FDC
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
						ntauthentication = (this.sessions[sessionCookie] = new NTAuthentication(false, "Negotiate", credential, spn, ContextFlags.Connection | ContextFlags.AcceptStream, channelBindingToken));
					}
					string token = null;
					if (!ntauthentication.IsCompleted)
					{
						byte[] incomingBlob = null;
						if (challenge != null)
						{
							incomingBlob = Convert.FromBase64String(challenge);
						}
						SecurityStatus securityStatus;
						byte[] outgoingBlob = ntauthentication.GetOutgoingBlob(incomingBlob, false, out securityStatus);
						if (ntauthentication.IsCompleted && outgoingBlob == null)
						{
							token = "\r\n";
						}
						if (outgoingBlob != null)
						{
							token = Convert.ToBase64String(outgoingBlob);
						}
					}
					else
					{
						token = this.GetSecurityLayerOutgoingBlob(challenge, ntauthentication);
					}
					result = new Authorization(token, ntauthentication.IsCompleted);
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

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000E60EC File Offset: 0x000E50EC
		public string AuthenticationType
		{
			get
			{
				return "gssapi";
			}
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000E60F4 File Offset: 0x000E50F4
		public void CloseContext(object sessionCookie)
		{
			NTAuthentication ntauthentication = null;
			lock (this.sessions)
			{
				ntauthentication = (this.sessions[sessionCookie] as NTAuthentication);
				if (ntauthentication != null)
				{
					this.sessions.Remove(sessionCookie);
				}
			}
			if (ntauthentication != null)
			{
				ntauthentication.CloseContext();
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000E6154 File Offset: 0x000E5154
		private string GetSecurityLayerOutgoingBlob(string challenge, NTAuthentication clientContext)
		{
			if (challenge == null)
			{
				return null;
			}
			byte[] array = Convert.FromBase64String(challenge);
			int num;
			try
			{
				num = clientContext.VerifySignature(array, 0, array.Length);
			}
			catch (Win32Exception)
			{
				return null;
			}
			if (num < 4 || array[0] != 1 || array[1] != 0 || array[2] != 0 || array[3] != 0)
			{
				return null;
			}
			byte[] inArray = null;
			try
			{
				num = clientContext.MakeSignature(array, 0, 4, ref inArray);
			}
			catch (Win32Exception)
			{
				return null;
			}
			return Convert.ToBase64String(inArray, 0, num);
		}

		// Token: 0x0400311D RID: 12573
		private Hashtable sessions = new Hashtable();
	}
}
