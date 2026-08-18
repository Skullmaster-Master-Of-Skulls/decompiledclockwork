using System;
using System.Collections.Generic;
using System.Linq;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x02000009 RID: 9
	internal class ClientAuthentication : IClientAuthentication
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000028E0 File Offset: 0x00000AE0
		public void Authenticate(IConnectionInfoInternal connectionInfo, ISession session)
		{
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			session.RegisterMessage("SSH_MSG_USERAUTH_FAILURE");
			session.RegisterMessage("SSH_MSG_USERAUTH_SUCCESS");
			session.RegisterMessage("SSH_MSG_USERAUTH_BANNER");
			session.UserAuthenticationBannerReceived += connectionInfo.UserAuthenticationBannerReceived;
			try
			{
				SshAuthenticationException ex = null;
				IAuthenticationMethod authenticationMethod = connectionInfo.CreateNoneAuthenticationMethod();
				if (authenticationMethod.Authenticate(session) != AuthenticationResult.Success && !ClientAuthentication.TryAuthenticate(session, new ClientAuthentication.AuthenticationState(connectionInfo.AuthenticationMethods), authenticationMethod.AllowedAuthentications.ToList<string>(), ref ex))
				{
					throw ex;
				}
			}
			finally
			{
				session.UserAuthenticationBannerReceived -= connectionInfo.UserAuthenticationBannerReceived;
				session.UnRegisterMessage("SSH_MSG_USERAUTH_FAILURE");
				session.UnRegisterMessage("SSH_MSG_USERAUTH_SUCCESS");
				session.UnRegisterMessage("SSH_MSG_USERAUTH_BANNER");
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029B8 File Offset: 0x00000BB8
		private static bool TryAuthenticate(ISession session, ClientAuthentication.AuthenticationState authenticationState, ICollection<string> allowedAuthenticationMethods, ref SshAuthenticationException authenticationException)
		{
			if (allowedAuthenticationMethods.Count == 0)
			{
				authenticationException = new SshAuthenticationException("No authentication methods defined on SSH server.");
				return false;
			}
			List<IAuthenticationMethod> list = (from a in authenticationState.SupportedAuthenticationMethods
			where allowedAuthenticationMethods.Contains(a.Name)
			select a).ToList<IAuthenticationMethod>();
			if (list.Count == 0)
			{
				authenticationException = new SshAuthenticationException(string.Format("No suitable authentication method found to complete authentication ({0}).", string.Join(",", allowedAuthenticationMethods.ToArray<string>())));
				return false;
			}
			foreach (IAuthenticationMethod authenticationMethod in ClientAuthentication.GetOrderedAuthenticationMethods(authenticationState, list))
			{
				if (!authenticationState.FailedAuthenticationMethods.Contains(authenticationMethod))
				{
					if (!authenticationState.ExecutedAuthenticationMethods.Contains(authenticationMethod))
					{
						authenticationState.ExecutedAuthenticationMethods.Add(authenticationMethod);
					}
					AuthenticationResult authenticationResult = authenticationMethod.Authenticate(session);
					switch (authenticationResult)
					{
					case AuthenticationResult.Success:
						authenticationException = null;
						break;
					case AuthenticationResult.PartialSuccess:
						if (ClientAuthentication.TryAuthenticate(session, authenticationState, authenticationMethod.AllowedAuthentications, ref authenticationException))
						{
							authenticationResult = AuthenticationResult.Success;
						}
						break;
					case AuthenticationResult.Failure:
						authenticationState.FailedAuthenticationMethods.Add(authenticationMethod);
						authenticationException = new SshAuthenticationException(string.Format("Permission denied ({0}).", authenticationMethod.Name));
						break;
					}
					if (authenticationResult == AuthenticationResult.Success)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B0C File Offset: 0x00000D0C
		private static IEnumerable<IAuthenticationMethod> GetOrderedAuthenticationMethods(ClientAuthentication.AuthenticationState authenticationState, IEnumerable<IAuthenticationMethod> matchingAuthenticationMethods)
		{
			List<IAuthenticationMethod> skippedAuthenticationMethods = new List<IAuthenticationMethod>();
			foreach (IAuthenticationMethod authenticationMethod in matchingAuthenticationMethods)
			{
				if (authenticationState.ExecutedAuthenticationMethods.Contains(authenticationMethod))
				{
					skippedAuthenticationMethods.Add(authenticationMethod);
				}
				else
				{
					yield return authenticationMethod;
				}
			}
			IEnumerator<IAuthenticationMethod> enumerator = null;
			foreach (IAuthenticationMethod authenticationMethod2 in skippedAuthenticationMethods)
			{
				yield return authenticationMethod2;
			}
			List<IAuthenticationMethod>.Enumerator enumerator2 = default(List<IAuthenticationMethod>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0200011D RID: 285
		private class AuthenticationState
		{
			// Token: 0x06000C24 RID: 3108 RVA: 0x00027612 File Offset: 0x00025812
			public AuthenticationState(IList<IAuthenticationMethod> supportedAuthenticationMethods)
			{
				this._supportedAuthenticationMethods = supportedAuthenticationMethods;
				this.ExecutedAuthenticationMethods = new List<IAuthenticationMethod>();
				this.FailedAuthenticationMethods = new List<IAuthenticationMethod>();
			}

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00027637 File Offset: 0x00025837
			// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002763F File Offset: 0x0002583F
			public IList<IAuthenticationMethod> ExecutedAuthenticationMethods { get; private set; }

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00027648 File Offset: 0x00025848
			// (set) Token: 0x06000C28 RID: 3112 RVA: 0x00027650 File Offset: 0x00025850
			public IList<IAuthenticationMethod> FailedAuthenticationMethods { get; private set; }

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00027659 File Offset: 0x00025859
			public IEnumerable<IAuthenticationMethod> SupportedAuthenticationMethods
			{
				get
				{
					return this._supportedAuthenticationMethods;
				}
			}

			// Token: 0x040004A9 RID: 1193
			private readonly IList<IAuthenticationMethod> _supportedAuthenticationMethods;
		}
	}
}
