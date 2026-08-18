using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel
{
	// Token: 0x020000C5 RID: 197
	internal class ServiceAuthenticationManagerWrapper : ServiceAuthenticationManager
	{
		// Token: 0x06000383 RID: 899 RVA: 0x000149F8 File Offset: 0x00012BF8
		internal ServiceAuthenticationManagerWrapper(ServiceAuthenticationManager wrappedServiceAuthManager, string[] actionUriFilter)
		{
			if (wrappedServiceAuthManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedServiceAuthManager");
			}
			if (actionUriFilter != null && actionUriFilter.Length != 0)
			{
				this.filteredActionUriCollection = new string[actionUriFilter.Length];
				for (int i = 0; i < actionUriFilter.Length; i++)
				{
					this.filteredActionUriCollection[i] = actionUriFilter[i];
				}
			}
			this.wrappedAuthenticationManager = wrappedServiceAuthManager;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00014A54 File Offset: 0x00012C54
		public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
		{
			if (this.CanSkipAuthentication(message))
			{
				return authPolicy;
			}
			if (this.filteredActionUriCollection != null)
			{
				for (int i = 0; i < this.filteredActionUriCollection.Length; i++)
				{
					if (message != null && message.Headers != null && !string.IsNullOrEmpty(message.Headers.Action) && message.Headers.Action == this.filteredActionUriCollection[i])
					{
						return authPolicy;
					}
				}
			}
			return this.wrappedAuthenticationManager.Authenticate(authPolicy, listenUri, ref message);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00014AD4 File Offset: 0x00012CD4
		private bool CanSkipAuthentication(Message message)
		{
			if (message != null && message.Properties != null && message.Properties.Security != null && message.Properties.Security.TransportToken == null)
			{
				if (message.Properties.Security.ProtectionToken != null && message.Properties.Security.ProtectionToken.SecurityToken != null && message.Properties.Security.ProtectionToken.SecurityToken.GetType() == typeof(SecurityContextSecurityToken))
				{
					return true;
				}
				if (message.Properties.Security.HasIncomingSupportingTokens)
				{
					foreach (SupportingTokenSpecification supportingTokenSpecification in message.Properties.Security.IncomingSupportingTokens)
					{
						if (supportingTokenSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing && supportingTokenSpecification.SecurityToken.GetType() == typeof(SecurityContextSecurityToken))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x04000977 RID: 2423
		private ServiceAuthenticationManager wrappedAuthenticationManager;

		// Token: 0x04000978 RID: 2424
		private string[] filteredActionUriCollection;
	}
}
