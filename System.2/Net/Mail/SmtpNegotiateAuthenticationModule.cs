using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028E RID: 654
	internal class SmtpNegotiateAuthenticationModule : ISmtpAuthenticationModule
	{
		// Token: 0x06001878 RID: 6264 RVA: 0x0007C5E1 File Offset: 0x0007A7E1
		internal SmtpNegotiateAuthenticationModule()
		{
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0007C5F4 File Offset: 0x0007A7F4
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

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0007C6FC File Offset: 0x0007A8FC
		public string AuthenticationType
		{
			get
			{
				return "gssapi";
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0007C704 File Offset: 0x0007A904
		public void CloseContext(object sessionCookie)
		{
			NTAuthentication ntauthentication = null;
			Hashtable obj = this.sessions;
			lock (obj)
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

		// Token: 0x0600187C RID: 6268 RVA: 0x0007C76C File Offset: 0x0007A96C
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
			if (num < 4 || (array[0] & 1) != 1)
			{
				return null;
			}
			array[0] = 1;
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

		// Token: 0x04001861 RID: 6241
		private Hashtable sessions = new Hashtable();
	}
}
