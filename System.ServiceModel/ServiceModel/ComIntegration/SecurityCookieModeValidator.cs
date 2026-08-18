using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000219 RID: 537
	internal class SecurityCookieModeValidator : IServiceBehavior
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x0003B4C8 File Offset: 0x000396C8
		private void CheckForCookie(SecurityTokenParameters tokenParameters, ServiceEndpoint endpoint)
		{
			bool flag = false;
			SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = tokenParameters as SecureConversationSecurityTokenParameters;
			if (secureConversationSecurityTokenParameters != null && !secureConversationSecurityTokenParameters.RequireCancellation)
			{
				flag = true;
			}
			SspiSecurityTokenParameters sspiSecurityTokenParameters = tokenParameters as SspiSecurityTokenParameters;
			if (sspiSecurityTokenParameters != null && !sspiSecurityTokenParameters.RequireCancellation)
			{
				flag = true;
			}
			SspiSecurityTokenParameters sspiSecurityTokenParameters2 = tokenParameters as SspiSecurityTokenParameters;
			if (sspiSecurityTokenParameters2 != null && !sspiSecurityTokenParameters2.RequireCancellation)
			{
				flag = true;
			}
			if (flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("RequireNonCookieMode", new object[]
				{
					endpoint.Binding.Name,
					endpoint.Binding.Namespace
				})));
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003B552 File Offset: 0x00039752
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0003B554 File Offset: 0x00039754
		void IServiceBehavior.Validate(ServiceDescription service, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003B558 File Offset: 0x00039758
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription service, ServiceHostBase serviceHostBase)
		{
			foreach (ServiceEndpoint serviceEndpoint in service.Endpoints)
			{
				ICollection<BindingElement> collection = serviceEndpoint.Binding.CreateBindingElements();
				foreach (BindingElement bindingElement in collection)
				{
					SymmetricSecurityBindingElement symmetricSecurityBindingElement = bindingElement as SymmetricSecurityBindingElement;
					if (symmetricSecurityBindingElement != null)
					{
						this.CheckForCookie(symmetricSecurityBindingElement.ProtectionTokenParameters, serviceEndpoint);
						using (IEnumerator<SecurityTokenParameters> enumerator3 = symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								SecurityTokenParameters tokenParameters = enumerator3.Current;
								this.CheckForCookie(tokenParameters, serviceEndpoint);
							}
							break;
						}
					}
				}
			}
		}
	}
}
