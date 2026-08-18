using System;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C4 RID: 964
	public sealed class ServiceAuthenticationBehavior : IServiceBehavior
	{
		// Token: 0x0600242E RID: 9262 RVA: 0x00083755 File Offset: 0x00081955
		public ServiceAuthenticationBehavior()
		{
			this.ServiceAuthenticationManager = this.defaultServiceAuthenticationManager;
			this.authenticationSchemes = AuthenticationSchemes.None;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00083770 File Offset: 0x00081970
		private ServiceAuthenticationBehavior(ServiceAuthenticationBehavior other)
		{
			this.serviceAuthenticationManager = other.ServiceAuthenticationManager;
			this.authenticationSchemes = other.authenticationSchemes;
			this.isReadOnly = other.isReadOnly;
			this.isAuthenticationManagerSet = other.isAuthenticationManagerSet;
			this.isAuthenticationSchemesSet = other.isAuthenticationSchemesSet;
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000837BF File Offset: 0x000819BF
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x000837C7 File Offset: 0x000819C7
		public ServiceAuthenticationManager ServiceAuthenticationManager
		{
			get
			{
				return this.serviceAuthenticationManager;
			}
			set
			{
				this.ThrowIfImmutable();
				this.serviceAuthenticationManager = value;
				this.isAuthenticationManagerSet = (value != null);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x000837E0 File Offset: 0x000819E0
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x000837E8 File Offset: 0x000819E8
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.authenticationSchemes;
			}
			set
			{
				this.ThrowIfImmutable();
				this.authenticationSchemes = value;
				this.isAuthenticationSchemesSet = true;
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000837FE File Offset: 0x000819FE
		public bool ShouldSerializeServiceAuthenticationManager()
		{
			return this.isAuthenticationManagerSet;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00083806 File Offset: 0x00081A06
		public bool ShouldSerializeAuthenticationSchemes()
		{
			return this.isAuthenticationSchemesSet;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0008380E File Offset: 0x00081A0E
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00083810 File Offset: 0x00081A10
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			if (this.serviceAuthenticationManager != null)
			{
				ServiceAuthenticationManager serviceAuthenticationManager = parameters.Find<ServiceAuthenticationManager>();
				if (serviceAuthenticationManager != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleAuthenticationManagersInServiceBindingParameters", new object[]
					{
						serviceAuthenticationManager
					})));
				}
				parameters.Add(this.serviceAuthenticationManager);
			}
			if (this.authenticationSchemes != AuthenticationSchemes.None)
			{
				AuthenticationSchemesBindingParameter authenticationSchemesBindingParameter = parameters.Find<AuthenticationSchemesBindingParameter>();
				if (authenticationSchemesBindingParameter != null)
				{
					if (authenticationSchemesBindingParameter.AuthenticationSchemes != this.authenticationSchemes)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleAuthenticationSchemesInServiceBindingParameters", new object[]
						{
							authenticationSchemesBindingParameter.AuthenticationSchemes
						})));
					}
				}
				else
				{
					parameters.Add(new AuthenticationSchemesBindingParameter(this.authenticationSchemes));
				}
			}
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000838D8 File Offset: 0x00081AD8
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("description"));
			}
			if (serviceHostBase == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceHostBase"));
			}
			if (this.serviceAuthenticationManager == null)
			{
				return;
			}
			for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null && !ServiceMetadataBehavior.IsHttpGetMetadataDispatcher(description, channelDispatcher))
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
						dispatchRuntime.ServiceAuthenticationManager = this.serviceAuthenticationManager;
						ServiceEndpoint serviceEndpoint = this.FindMatchingServiceEndpoint(description, endpointDispatcher);
						if (serviceEndpoint != null)
						{
							bool flag = this.IsSecureConversationBinding(serviceEndpoint.Binding);
							if (flag)
							{
								SecurityStandardsManager configuredSecurityStandardsManager = this.GetConfiguredSecurityStandardsManager(serviceEndpoint.Binding);
								dispatchRuntime.ServiceAuthenticationManager = new ServiceAuthenticationManagerWrapper(this.serviceAuthenticationManager, new string[]
								{
									configuredSecurityStandardsManager.SecureConversationDriver.CloseAction.Value
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00083A08 File Offset: 0x00081C08
		internal ServiceAuthenticationBehavior Clone()
		{
			return new ServiceAuthenticationBehavior(this);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00083A10 File Offset: 0x00081C10
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00083A19 File Offset: 0x00081C19
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00083A40 File Offset: 0x00081C40
		private ServiceEndpoint FindMatchingServiceEndpoint(ServiceDescription description, EndpointDispatcher endpointDispatcher)
		{
			foreach (ServiceEndpoint serviceEndpoint in description.Endpoints)
			{
				if (serviceEndpoint.Address.Equals(endpointDispatcher.EndpointAddress))
				{
					return serviceEndpoint;
				}
			}
			return null;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00083AA0 File Offset: 0x00081CA0
		private bool IsSecureConversationBinding(Binding binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			SecurityBindingElement securityBindingElement = binding.CreateBindingElements().Find<SecurityBindingElement>();
			if (securityBindingElement == null)
			{
				return false;
			}
			foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(securityBindingElement, true))
			{
				if (securityTokenParameters is SecureConversationSecurityTokenParameters)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00083B1C File Offset: 0x00081D1C
		private SecurityStandardsManager GetConfiguredSecurityStandardsManager(Binding binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			SecurityBindingElement securityBindingElement = binding.CreateBindingElements().Find<SecurityBindingElement>();
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("binding", SR.GetString("NoSecurityBindingElementFound"));
			}
			return new SecurityStandardsManager(securityBindingElement.MessageSecurityVersion, new WSSecurityTokenSerializer(securityBindingElement.MessageSecurityVersion.SecurityVersion));
		}

		// Token: 0x04002059 RID: 8281
		internal ServiceAuthenticationManager defaultServiceAuthenticationManager;

		// Token: 0x0400205A RID: 8282
		private ServiceAuthenticationManager serviceAuthenticationManager;

		// Token: 0x0400205B RID: 8283
		private AuthenticationSchemes authenticationSchemes;

		// Token: 0x0400205C RID: 8284
		private bool isAuthenticationManagerSet;

		// Token: 0x0400205D RID: 8285
		private bool isAuthenticationSchemesSet;

		// Token: 0x0400205E RID: 8286
		private bool isReadOnly;
	}
}
