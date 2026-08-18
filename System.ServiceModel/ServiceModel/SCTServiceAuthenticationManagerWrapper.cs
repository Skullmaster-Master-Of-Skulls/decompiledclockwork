using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000C4 RID: 196
	internal class SCTServiceAuthenticationManagerWrapper : ServiceAuthenticationManager
	{
		// Token: 0x06000381 RID: 897 RVA: 0x000148DC File Offset: 0x00012ADC
		internal SCTServiceAuthenticationManagerWrapper(ServiceAuthenticationManager wrappedServiceAuthManager)
		{
			if (wrappedServiceAuthManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedServiceAuthManager");
			}
			this.wrappedAuthenticationManager = wrappedServiceAuthManager;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00014900 File Offset: 0x00012B00
		public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
		{
			if (message != null && message.Properties != null && message.Properties.Security != null && message.Properties.Security.TransportToken != null && message.Properties.Security.ServiceSecurityContext != null && message.Properties.Security.ServiceSecurityContext.AuthorizationPolicies != null)
			{
				List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>(message.Properties.Security.ServiceSecurityContext.AuthorizationPolicies);
				foreach (IAuthorizationPolicy item in message.Properties.Security.TransportToken.SecurityTokenPolicies)
				{
					list.Remove(item);
				}
				authPolicy = list.AsReadOnly();
			}
			return this.wrappedAuthenticationManager.Authenticate(authPolicy, listenUri, ref message);
		}

		// Token: 0x04000976 RID: 2422
		private ServiceAuthenticationManager wrappedAuthenticationManager;
	}
}
