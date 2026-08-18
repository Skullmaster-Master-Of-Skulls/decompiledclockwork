using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000C6 RID: 198
	public class ServiceSecurityContext
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00014BF0 File Offset: 0x00012DF0
		public ServiceSecurityContext(ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (authorizationPolicies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorizationPolicies");
			}
			this.authorizationContext = null;
			this.authorizationPolicies = authorizationPolicies;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00014C19 File Offset: 0x00012E19
		public ServiceSecurityContext(AuthorizationContext authorizationContext) : this(authorizationContext, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00014C27 File Offset: 0x00012E27
		public ServiceSecurityContext(AuthorizationContext authorizationContext, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (authorizationContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorizationContext");
			}
			if (authorizationPolicies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorizationPolicies");
			}
			this.authorizationContext = authorizationContext;
			this.authorizationPolicies = authorizationPolicies;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00014C63 File Offset: 0x00012E63
		public static ServiceSecurityContext Anonymous
		{
			get
			{
				if (ServiceSecurityContext.anonymous == null)
				{
					ServiceSecurityContext.anonymous = new ServiceSecurityContext(EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance);
				}
				return ServiceSecurityContext.anonymous;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00014C80 File Offset: 0x00012E80
		public static ServiceSecurityContext Current
		{
			get
			{
				ServiceSecurityContext result = null;
				OperationContext operationContext = OperationContext.Current;
				if (operationContext != null)
				{
					MessageProperties incomingMessageProperties = operationContext.IncomingMessageProperties;
					if (incomingMessageProperties != null)
					{
						SecurityMessageProperty security = incomingMessageProperties.Security;
						if (security != null)
						{
							result = security.ServiceSecurityContext;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00014CB4 File Offset: 0x00012EB4
		public bool IsAnonymous
		{
			get
			{
				return this == ServiceSecurityContext.Anonymous || this.IdentityClaim == null;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00014CC9 File Offset: 0x00012EC9
		internal Claim IdentityClaim
		{
			get
			{
				if (this.identityClaim == null)
				{
					this.identityClaim = SecurityUtils.GetPrimaryIdentityClaim(this.AuthorizationContext);
				}
				return this.identityClaim;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00014CEC File Offset: 0x00012EEC
		public IIdentity PrimaryIdentity
		{
			get
			{
				if (this.primaryIdentity == null)
				{
					IIdentity identity = null;
					IList<IIdentity> identities = this.GetIdentities();
					if (identities != null && identities.Count == 1)
					{
						identity = identities[0];
					}
					this.primaryIdentity = (identity ?? SecurityUtils.AnonymousIdentity);
				}
				return this.primaryIdentity;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00014D34 File Offset: 0x00012F34
		public WindowsIdentity WindowsIdentity
		{
			get
			{
				if (this.windowsIdentity == null)
				{
					WindowsIdentity windowsIdentity = null;
					IList<IIdentity> identities = this.GetIdentities();
					if (identities != null)
					{
						for (int i = 0; i < identities.Count; i++)
						{
							WindowsIdentity windowsIdentity2 = identities[i] as WindowsIdentity;
							if (windowsIdentity2 != null)
							{
								if (windowsIdentity != null)
								{
									windowsIdentity = WindowsIdentity.GetAnonymous();
									break;
								}
								windowsIdentity = windowsIdentity2;
							}
						}
					}
					this.windowsIdentity = (windowsIdentity ?? WindowsIdentity.GetAnonymous());
				}
				return this.windowsIdentity;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00014D99 File Offset: 0x00012F99
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00014DA1 File Offset: 0x00012FA1
		public ReadOnlyCollection<IAuthorizationPolicy> AuthorizationPolicies
		{
			get
			{
				return this.authorizationPolicies;
			}
			set
			{
				this.authorizationPolicies = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00014DAA File Offset: 0x00012FAA
		public AuthorizationContext AuthorizationContext
		{
			get
			{
				if (this.authorizationContext == null)
				{
					this.authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(this.authorizationPolicies);
				}
				return this.authorizationContext;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00014DCC File Offset: 0x00012FCC
		private IList<IIdentity> GetIdentities()
		{
			AuthorizationContext authorizationContext = this.AuthorizationContext;
			object obj;
			if (authorizationContext != null && authorizationContext.Properties.TryGetValue("Identities", out obj))
			{
				return obj as IList<IIdentity>;
			}
			return null;
		}

		// Token: 0x04000979 RID: 2425
		private static ServiceSecurityContext anonymous;

		// Token: 0x0400097A RID: 2426
		private ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;

		// Token: 0x0400097B RID: 2427
		private AuthorizationContext authorizationContext;

		// Token: 0x0400097C RID: 2428
		private IIdentity primaryIdentity;

		// Token: 0x0400097D RID: 2429
		private Claim identityClaim;

		// Token: 0x0400097E RID: 2430
		private WindowsIdentity windowsIdentity;
	}
}
