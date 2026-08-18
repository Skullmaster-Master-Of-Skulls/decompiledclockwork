using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000540 RID: 1344
	internal class SecurityValidationBehavior : IEndpointBehavior, IServiceBehavior
	{
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000C4A4C File Offset: 0x000C2C4C
		public static SecurityValidationBehavior Instance
		{
			get
			{
				if (SecurityValidationBehavior.instance == null)
				{
					SecurityValidationBehavior.instance = new SecurityValidationBehavior();
				}
				return SecurityValidationBehavior.instance;
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000C4A64 File Offset: 0x000C2C64
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint");
			}
			Binding binding = new SecurityValidationBehavior.ValidationBinding(serviceEndpoint.Binding);
			SecurityBindingElement securityBindingElement;
			this.ValidateBinding(binding, serviceEndpoint.Contract, out securityBindingElement);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000C4A9F File Offset: 0x000C2C9F
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000C4AA1 File Offset: 0x000C2CA1
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000C4AA3 File Offset: 0x000C2CA3
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000C4AA5 File Offset: 0x000C2CA5
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000C4AA7 File Offset: 0x000C2CA7
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000C4AAC File Offset: 0x000C2CAC
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			for (int i = 0; i < description.Endpoints.Count; i++)
			{
				ServiceEndpoint serviceEndpoint = description.Endpoints[i];
				Binding binding = new SecurityValidationBehavior.ValidationBinding(serviceEndpoint.Binding);
				SecurityBindingElement securityBindingElement;
				this.ValidateBinding(binding, serviceEndpoint.Contract, out securityBindingElement);
				if (securityBindingElement != null)
				{
					SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.Validate(securityBindingElement, binding, serviceEndpoint.Contract, description.Behaviors);
				}
			}
			SecurityValidationBehavior.WindowsIdentitySupportRule.Validate(description);
			SecurityValidationBehavior.UsernameImpersonationRule.Validate(description);
			SecurityValidationBehavior.MissingClientCertificateRule.Validate(description);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000C4B32 File Offset: 0x000C2D32
		private void ValidateBinding(Binding binding, ContractDescription contract, out SecurityBindingElement securityBindingElement)
		{
			securityBindingElement = SecurityValidationBehavior.GetSecurityBinding(binding, contract);
			if (securityBindingElement != null)
			{
				this.ValidateSecurityBinding(securityBindingElement, binding, contract);
				return;
			}
			this.ValidateNoSecurityBinding(binding, contract);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000C4B54 File Offset: 0x000C2D54
		private void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
		{
			SecurityValidationBehavior.ContractProtectionRequirementsRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.CookieAndSessionProtectionRequirementsRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.SoapOverSecureTransportRequirementsRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.SecurityVersionSupportForEncryptedKeyBindingRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.SecurityVersionSupportForThumbprintKeyIdentifierClauseRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.SecurityBindingSupportForOneWayOnlyRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.IssuedKeySizeCompatibilityWithAlgorithmSuiteRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.MessageSecurityAndManualAddressingRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.NoStreamingWithSecurityRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.UnknownHeaderProtectionRequirementsRule.ValidateSecurityBinding(sbe, binding, contract);
			SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.ValidateSecurityBinding(sbe, binding, contract);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000C4BBC File Offset: 0x000C2DBC
		private void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
		{
			SecurityValidationBehavior.ContractProtectionRequirementsRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.CookieAndSessionProtectionRequirementsRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.SoapOverSecureTransportRequirementsRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.SecurityVersionSupportForEncryptedKeyBindingRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.SecurityVersionSupportForThumbprintKeyIdentifierClauseRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.SecurityBindingSupportForOneWayOnlyRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.IssuedKeySizeCompatibilityWithAlgorithmSuiteRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.MessageSecurityAndManualAddressingRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.UnknownHeaderProtectionRequirementsRule.ValidateNoSecurityBinding(binding, contract);
			SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.ValidateNoSecurityBinding(binding, contract);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000C4C10 File Offset: 0x000C2E10
		private static SecurityBindingElement GetSecurityBinding(Binding binding, ContractDescription contract)
		{
			SecurityBindingElement securityBindingElement = null;
			BindingElementCollection bindingElementCollection = binding.CreateBindingElements();
			for (int i = 0; i < bindingElementCollection.Count; i++)
			{
				BindingElement bindingElement = bindingElementCollection[i];
				if (bindingElement is SecurityBindingElement)
				{
					if (securityBindingElement != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MoreThanOneSecurityBindingElementInTheBinding", new object[]
						{
							binding.Name,
							binding.Namespace,
							contract.Name,
							contract.Namespace
						})));
					}
					securityBindingElement = (SecurityBindingElement)bindingElement;
				}
			}
			return securityBindingElement;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000C4C96 File Offset: 0x000C2E96
		internal void AfterBuildTimeValidation(ServiceDescription description)
		{
			SecurityValidationBehavior.S4UImpersonationRule.Validate(description);
		}

		// Token: 0x0400274B RID: 10059
		private static SecurityValidationBehavior instance;

		// Token: 0x02000C55 RID: 3157
		private class ValidationBinding : Binding
		{
			// Token: 0x060077A8 RID: 30632 RVA: 0x001BF338 File Offset: 0x001BD538
			public ValidationBinding(Binding binding) : base(binding.Name, binding.Namespace)
			{
				this.binding = binding;
			}

			// Token: 0x17001B56 RID: 6998
			// (get) Token: 0x060077A9 RID: 30633 RVA: 0x001BF353 File Offset: 0x001BD553
			public override string Scheme
			{
				get
				{
					return this.binding.Scheme;
				}
			}

			// Token: 0x060077AA RID: 30634 RVA: 0x001BF360 File Offset: 0x001BD560
			public override BindingElementCollection CreateBindingElements()
			{
				if (this.elements == null)
				{
					this.elements = this.binding.CreateBindingElements();
				}
				return this.elements;
			}

			// Token: 0x060077AB RID: 30635 RVA: 0x001BF381 File Offset: 0x001BD581
			public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077AC RID: 30636 RVA: 0x001BF392 File Offset: 0x001BD592
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(params object[] parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077AD RID: 30637 RVA: 0x001BF3A3 File Offset: 0x001BD5A3
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, params object[] parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077AE RID: 30638 RVA: 0x001BF3B4 File Offset: 0x001BD5B4
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, params object[] parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077AF RID: 30639 RVA: 0x001BF3C5 File Offset: 0x001BD5C5
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode, params object[] parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077B0 RID: 30640 RVA: 0x001BF3D6 File Offset: 0x001BD5D6
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077B1 RID: 30641 RVA: 0x001BF3E7 File Offset: 0x001BD5E7
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077B2 RID: 30642 RVA: 0x001BF3F8 File Offset: 0x001BD5F8
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077B3 RID: 30643 RVA: 0x001BF409 File Offset: 0x001BD609
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(Uri listenUriBaseAddress, string listenUriRelativeAddress, ListenUriMode listenUriMode, BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077B4 RID: 30644 RVA: 0x001BF41A File Offset: 0x001BD61A
			public override bool CanBuildChannelFactory<TChannel>(BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060077B5 RID: 30645 RVA: 0x001BF42B File Offset: 0x001BD62B
			public override bool CanBuildChannelListener<TChannel>(BindingParameterCollection parameters)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x04004474 RID: 17524
			private Binding binding;

			// Token: 0x04004475 RID: 17525
			private BindingElementCollection elements;
		}

		// Token: 0x02000C56 RID: 3158
		private static class NoStreamingWithSecurityRule
		{
			// Token: 0x060077B6 RID: 30646 RVA: 0x001BF43C File Offset: 0x001BD63C
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				if ((sbe is SymmetricSecurityBindingElement || sbe is AsymmetricSecurityBindingElement) && SecurityValidationBehavior.NoStreamingWithSecurityRule.GetTransferMode(binding) != TransferMode.Buffered)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoStreamingWithSecurity", new object[]
					{
						binding.Name,
						binding.Namespace
					})));
				}
			}

			// Token: 0x060077B7 RID: 30647 RVA: 0x001BF494 File Offset: 0x001BD694
			private static TransferMode GetTransferMode(Binding binding)
			{
				TransferMode result = TransferMode.Buffered;
				BindingElementCollection bindingElementCollection = binding.CreateBindingElements();
				TransportBindingElement transportBindingElement = bindingElementCollection.Find<TransportBindingElement>();
				if (transportBindingElement is ConnectionOrientedTransportBindingElement)
				{
					result = ((ConnectionOrientedTransportBindingElement)transportBindingElement).TransferMode;
				}
				else if (transportBindingElement is HttpTransportBindingElement)
				{
					result = ((HttpTransportBindingElement)transportBindingElement).TransferMode;
				}
				return result;
			}
		}

		// Token: 0x02000C57 RID: 3159
		private static class WindowsIdentitySupportRule
		{
			// Token: 0x060077B8 RID: 30648 RVA: 0x001BF4DC File Offset: 0x001BD6DC
			public static void Validate(ServiceDescription description)
			{
				ServiceAuthorizationBehavior serviceAuthorizationBehavior = description.Behaviors.Find<ServiceAuthorizationBehavior>();
				bool flag = serviceAuthorizationBehavior != null && serviceAuthorizationBehavior.ImpersonateCallerForAllOperations;
				for (int i = 0; i < description.Endpoints.Count; i++)
				{
					ServiceEndpoint serviceEndpoint = description.Endpoints[i];
					if (!serviceEndpoint.InternalIsSystemEndpoint(description))
					{
						for (int j = 0; j < serviceEndpoint.Contract.Operations.Count; j++)
						{
							OperationDescription operationDescription = serviceEndpoint.Contract.Operations[j];
							OperationBehaviorAttribute operationBehaviorAttribute = operationDescription.Behaviors.Find<OperationBehaviorAttribute>();
							if (flag && !operationDescription.IsServerInitiated() && (operationBehaviorAttribute == null || operationBehaviorAttribute.Impersonation == ImpersonationOption.NotAllowed))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OperationDoesNotAllowImpersonation", new object[]
								{
									operationDescription.Name,
									serviceEndpoint.Contract.Name,
									serviceEndpoint.Contract.Namespace
								})));
							}
							if (flag || (operationBehaviorAttribute != null && operationBehaviorAttribute.Impersonation == ImpersonationOption.Required))
							{
								SecurityValidationBehavior.WindowsIdentitySupportRule.ValidateWindowsIdentityCapability(serviceEndpoint.Binding, serviceEndpoint.Contract, operationDescription);
							}
						}
					}
				}
			}

			// Token: 0x060077B9 RID: 30649 RVA: 0x001BF604 File Offset: 0x001BD804
			private static void ValidateWindowsIdentityCapability(Binding binding, ContractDescription contract, OperationDescription operation)
			{
				bool flag = false;
				ISecurityCapabilities property = binding.GetProperty<ISecurityCapabilities>(new BindingParameterCollection());
				if (property != null && property.SupportsClientWindowsIdentity)
				{
					flag = true;
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BindingDoesNotSupportWindowsIdenityForImpersonation", new object[]
					{
						operation.Name,
						binding.Name,
						binding.Namespace,
						contract.Name,
						contract.Namespace
					})));
				}
			}
		}

		// Token: 0x02000C58 RID: 3160
		private static class S4UImpersonationRule
		{
			// Token: 0x060077BA RID: 30650 RVA: 0x001BF67C File Offset: 0x001BD87C
			private static bool IsS4URequiredForImpersonation(SecurityBindingElement sbe)
			{
				foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
				{
					if (securityTokenParameters is SecureConversationSecurityTokenParameters)
					{
						SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = (SecureConversationSecurityTokenParameters)securityTokenParameters;
						if (!secureConversationSecurityTokenParameters.RequireCancellation)
						{
							return true;
						}
						if (secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement != null)
						{
							return SecurityValidationBehavior.S4UImpersonationRule.IsS4URequiredForImpersonation(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement);
						}
					}
					if (securityTokenParameters is SspiSecurityTokenParameters && !((SspiSecurityTokenParameters)securityTokenParameters).RequireCancellation)
					{
						return true;
					}
					if (securityTokenParameters is X509SecurityTokenParameters)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060077BB RID: 30651 RVA: 0x001BF71C File Offset: 0x001BD91C
			public static void Validate(ServiceDescription description)
			{
				ServiceAuthorizationBehavior serviceAuthorizationBehavior = description.Behaviors.Find<ServiceAuthorizationBehavior>();
				bool flag = serviceAuthorizationBehavior != null && serviceAuthorizationBehavior.ImpersonateCallerForAllOperations;
				for (int i = 0; i < description.Endpoints.Count; i++)
				{
					ServiceEndpoint serviceEndpoint = description.Endpoints[i];
					if (!serviceEndpoint.InternalIsSystemEndpoint(description))
					{
						bool flag2 = flag;
						if (!flag2)
						{
							flag2 = SecurityValidationBehavior.ValidatorUtils.EndpointRequiresImpersonation(serviceEndpoint);
						}
						if (flag2)
						{
							ICollection<BindingElement> collection = serviceEndpoint.Binding.CreateBindingElements();
							foreach (BindingElement bindingElement in collection)
							{
								SecurityBindingElement securityBindingElement = bindingElement as SecurityBindingElement;
								if (securityBindingElement != null)
								{
									if (!SecurityValidationBehavior.S4UImpersonationRule.IsS4URequiredForImpersonation(securityBindingElement))
									{
										break;
									}
									Version version = Environment.OSVersion.Version;
									if (version.Major < 5 || (version.Major == 5 && version.Minor < 2))
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotPerformS4UImpersonationOnPlatform", new object[]
										{
											serviceEndpoint.Binding.Name,
											serviceEndpoint.Binding.Namespace,
											serviceEndpoint.Contract.Name,
											serviceEndpoint.Contract.Namespace
										})));
									}
									break;
								}
							}
						}
					}
				}
			}

			// Token: 0x04004476 RID: 17526
			private const int WindowsServerMajorNumber = 5;

			// Token: 0x04004477 RID: 17527
			private const int WindowsServerMinorNumber = 2;
		}

		// Token: 0x02000C59 RID: 3161
		private static class UnknownHeaderProtectionRequirementsRule
		{
			// Token: 0x060077BC RID: 30652 RVA: 0x001BF87C File Offset: 0x001BDA7C
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				if (sbe is SymmetricSecurityBindingElement || sbe is AsymmetricSecurityBindingElement)
				{
					SecurityValidationBehavior.UnknownHeaderProtectionRequirementsRule.ValidateContract(binding, contract, sbe.GetIndividualProperty<ISecurityCapabilities>().SupportedRequestProtectionLevel, sbe.GetIndividualProperty<ISecurityCapabilities>().SupportedResponseProtectionLevel);
					return;
				}
				SecurityValidationBehavior.UnknownHeaderProtectionRequirementsRule.ValidateContract(binding, contract, ProtectionLevel.None, ProtectionLevel.None);
			}

			// Token: 0x060077BD RID: 30653 RVA: 0x001BF8B5 File Offset: 0x001BDAB5
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
				SecurityValidationBehavior.UnknownHeaderProtectionRequirementsRule.ValidateContract(binding, contract, ProtectionLevel.None, ProtectionLevel.None);
			}

			// Token: 0x060077BE RID: 30654 RVA: 0x001BF8C0 File Offset: 0x001BDAC0
			private static void ValidateContract(Binding binding, ContractDescription contract, ProtectionLevel defaultRequestProtectionLevel, ProtectionLevel defaultResponseProtectionLevel)
			{
				if (contract == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contract"));
				}
				ProtectionLevel protectionLevel;
				ProtectionLevel protectionLevel2;
				if (contract.HasProtectionLevel)
				{
					protectionLevel = contract.ProtectionLevel;
					protectionLevel2 = contract.ProtectionLevel;
				}
				else
				{
					protectionLevel = defaultRequestProtectionLevel;
					protectionLevel2 = defaultResponseProtectionLevel;
				}
				foreach (OperationDescription operationDescription in contract.Operations)
				{
					ProtectionLevel protectionLevel3;
					ProtectionLevel protectionLevel4;
					if (operationDescription.HasProtectionLevel)
					{
						protectionLevel3 = operationDescription.ProtectionLevel;
						protectionLevel4 = operationDescription.ProtectionLevel;
					}
					else
					{
						protectionLevel3 = protectionLevel;
						protectionLevel4 = protectionLevel2;
					}
					foreach (MessageDescription messageDescription in operationDescription.Messages)
					{
						ProtectionLevel protectionLevel5;
						if (messageDescription.HasProtectionLevel)
						{
							protectionLevel5 = messageDescription.ProtectionLevel;
						}
						else if (messageDescription.Direction == MessageDirection.Input)
						{
							protectionLevel5 = protectionLevel3;
						}
						else
						{
							protectionLevel5 = protectionLevel4;
						}
						foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
						{
							ProtectionLevel protectionLevel6;
							if (messageHeaderDescription.HasProtectionLevel)
							{
								protectionLevel6 = messageHeaderDescription.ProtectionLevel;
							}
							else
							{
								protectionLevel6 = protectionLevel5;
							}
							if (messageHeaderDescription.IsUnknownHeaderCollection && protectionLevel6 != ProtectionLevel.None)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnknownHeaderCannotProtected", new object[]
								{
									contract.Name,
									contract.Namespace,
									messageHeaderDescription.Name,
									messageHeaderDescription.Namespace
								})));
							}
						}
					}
				}
			}
		}

		// Token: 0x02000C5A RID: 3162
		private static class ContractProtectionRequirementsRule
		{
			// Token: 0x060077BF RID: 30655 RVA: 0x001BFA94 File Offset: 0x001BDC94
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				if (sbe is SymmetricSecurityBindingElement || sbe is AsymmetricSecurityBindingElement)
				{
					SecurityValidationBehavior.ContractProtectionRequirementsRule.ValidateContract(binding, contract, sbe.GetIndividualProperty<ISecurityCapabilities>().SupportedRequestProtectionLevel, sbe.GetIndividualProperty<ISecurityCapabilities>().SupportedResponseProtectionLevel);
					return;
				}
				SecurityValidationBehavior.ContractProtectionRequirementsRule.ValidateContract(binding, contract, ProtectionLevel.None, ProtectionLevel.None);
			}

			// Token: 0x060077C0 RID: 30656 RVA: 0x001BFACD File Offset: 0x001BDCCD
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
				SecurityValidationBehavior.ContractProtectionRequirementsRule.ValidateContract(binding, contract, ProtectionLevel.None, ProtectionLevel.None);
			}

			// Token: 0x060077C1 RID: 30657 RVA: 0x001BFAD8 File Offset: 0x001BDCD8
			private static void ValidateContract(Binding binding, ContractDescription contract, ProtectionLevel defaultRequestProtectionLevel, ProtectionLevel defaultResponseProtectionLevel)
			{
				ProtectionLevel request;
				ProtectionLevel response;
				SecurityValidationBehavior.ContractProtectionRequirementsRule.GetRequiredProtectionLevels(contract, defaultRequestProtectionLevel, defaultResponseProtectionLevel, out request, out response);
				SecurityValidationBehavior.ContractProtectionRequirementsRule.ValidateBindingProtectionCapability(binding, contract, request, response);
			}

			// Token: 0x060077C2 RID: 30658 RVA: 0x001BFAFC File Offset: 0x001BDCFC
			internal static void GetRequiredProtectionLevels(ContractDescription contract, ProtectionLevel defaultRequestProtectionLevel, ProtectionLevel defaultResponseProtectionLevel, out ProtectionLevel request, out ProtectionLevel response)
			{
				ChannelProtectionRequirements channelProtectionRequirements = ChannelProtectionRequirements.CreateFromContract(contract, defaultRequestProtectionLevel, defaultResponseProtectionLevel, false);
				if (channelProtectionRequirements.IncomingSignatureParts.IsEmpty())
				{
					request = ProtectionLevel.None;
				}
				else if (channelProtectionRequirements.IncomingEncryptionParts.IsEmpty())
				{
					request = ProtectionLevel.Sign;
				}
				else
				{
					request = ProtectionLevel.EncryptAndSign;
				}
				if (channelProtectionRequirements.OutgoingSignatureParts.IsEmpty())
				{
					response = ProtectionLevel.None;
					return;
				}
				if (channelProtectionRequirements.OutgoingEncryptionParts.IsEmpty())
				{
					response = ProtectionLevel.Sign;
					return;
				}
				response = ProtectionLevel.EncryptAndSign;
			}

			// Token: 0x060077C3 RID: 30659 RVA: 0x001BFB64 File Offset: 0x001BDD64
			private static void ValidateBindingProtectionCapability(Binding binding, ContractDescription contract, ProtectionLevel request, ProtectionLevel response)
			{
				bool flag = request == ProtectionLevel.None;
				bool flag2 = response == ProtectionLevel.None;
				if (!flag || !flag2)
				{
					ISecurityCapabilities property = binding.GetProperty<ISecurityCapabilities>(new BindingParameterCollection());
					if (property != null)
					{
						if (!flag)
						{
							flag = ProtectionLevelHelper.IsStrongerOrEqual(property.SupportedRequestProtectionLevel, request);
						}
						if (!flag2)
						{
							flag2 = ProtectionLevelHelper.IsStrongerOrEqual(property.SupportedResponseProtectionLevel, response);
						}
					}
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AtLeastOneContractOperationRequestRequiresProtectionLevelNotSupportedByBinding", new object[]
					{
						contract.Name,
						contract.Namespace,
						binding.Name,
						binding.Namespace
					})));
				}
				if (!flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AtLeastOneContractOperationResponseRequiresProtectionLevelNotSupportedByBinding", new object[]
					{
						contract.Name,
						contract.Namespace,
						binding.Name,
						binding.Namespace
					})));
				}
			}
		}

		// Token: 0x02000C5B RID: 3163
		private static class BearerKeyTypeIssuanceRequirementRule
		{
			// Token: 0x060077C4 RID: 30660 RVA: 0x001BFC40 File Offset: 0x001BDE40
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
				{
					if (securityTokenParameters is IssuedSecurityTokenParameters)
					{
						IssuedSecurityTokenParameters issuedSecurityTokenParameters = securityTokenParameters as IssuedSecurityTokenParameters;
						if (issuedSecurityTokenParameters.KeyType == SecurityKeyType.BearerKey)
						{
							if (sbe is SymmetricSecurityBindingElement && SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.IsBearerKeyType(((SymmetricSecurityBindingElement)sbe).ProtectionTokenParameters))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidBearerKeyUsage", new object[]
								{
									binding.Name,
									binding.Namespace
								})));
							}
							if (sbe is AsymmetricSecurityBindingElement && (SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.IsBearerKeyType(((AsymmetricSecurityBindingElement)sbe).InitiatorTokenParameters) || SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.IsBearerKeyType(((AsymmetricSecurityBindingElement)sbe).RecipientTokenParameters)))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidBearerKeyUsage", new object[]
								{
									binding.Name,
									binding.Namespace
								})));
							}
							foreach (SecurityTokenParameters tokenParameters in sbe.EndpointSupportingTokenParameters.Endorsing)
							{
								if (SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.IsBearerKeyType(tokenParameters))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidBearerKeyUsage", new object[]
									{
										binding.Name,
										binding.Namespace
									})));
								}
							}
							foreach (SecurityTokenParameters tokenParameters2 in sbe.EndpointSupportingTokenParameters.SignedEndorsing)
							{
								if (SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.IsBearerKeyType(tokenParameters2))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidBearerKeyUsage", new object[]
									{
										binding.Name,
										binding.Namespace
									})));
								}
							}
						}
						if (issuedSecurityTokenParameters.IssuerBinding != null)
						{
							SecurityBindingElement securityBinding = SecurityValidationBehavior.GetSecurityBinding(issuedSecurityTokenParameters.IssuerBinding, contract);
							if (securityBinding != null)
							{
								SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.ValidateSecurityBinding(securityBinding, issuedSecurityTokenParameters.IssuerBinding, contract);
							}
						}
					}
					else if (securityTokenParameters is SecureConversationSecurityTokenParameters)
					{
						SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = securityTokenParameters as SecureConversationSecurityTokenParameters;
						SecurityValidationBehavior.BearerKeyTypeIssuanceRequirementRule.ValidateSecurityBinding(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement, binding, contract);
					}
				}
			}

			// Token: 0x060077C5 RID: 30661 RVA: 0x001BFEB4 File Offset: 0x001BE0B4
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}

			// Token: 0x060077C6 RID: 30662 RVA: 0x001BFEB6 File Offset: 0x001BE0B6
			private static bool IsBearerKeyType(SecurityTokenParameters tokenParameters)
			{
				return tokenParameters is IssuedSecurityTokenParameters && ((IssuedSecurityTokenParameters)tokenParameters).KeyType == SecurityKeyType.BearerKey;
			}
		}

		// Token: 0x02000C5C RID: 3164
		private static class CookieAndSessionProtectionRequirementsRule
		{
			// Token: 0x060077C7 RID: 30663 RVA: 0x001BFED0 File Offset: 0x001BE0D0
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				if (!(sbe is TransportSecurityBindingElement))
				{
					foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
					{
						SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = securityTokenParameters as SecureConversationSecurityTokenParameters;
						if (secureConversationSecurityTokenParameters != null)
						{
							ISecurityCapabilities individualProperty = secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement.GetIndividualProperty<ISecurityCapabilities>();
							if (individualProperty == null || individualProperty.SupportedRequestProtectionLevel != ProtectionLevel.EncryptAndSign || individualProperty.SupportedResponseProtectionLevel != ProtectionLevel.EncryptAndSign)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BindingDoesNotSupportProtectionForRst", new object[]
								{
									binding.Name,
									binding.Namespace,
									contract.Name,
									contract.Namespace
								})));
							}
						}
					}
				}
			}

			// Token: 0x060077C8 RID: 30664 RVA: 0x001BFF94 File Offset: 0x001BE194
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C5D RID: 3165
		private static class SoapOverSecureTransportRequirementsRule
		{
			// Token: 0x060077C9 RID: 30665 RVA: 0x001BFF98 File Offset: 0x001BE198
			public static void ValidateSecurityBinding(SecurityBindingElement securityBindingElement, Binding binding, ContractDescription contract)
			{
				if (securityBindingElement is TransportSecurityBindingElement && !securityBindingElement.AllowInsecureTransport)
				{
					IEnumerable<BindingElement> enumerable = binding.CreateBindingElements();
					Collection<BindingElement> collection = new Collection<BindingElement>();
					bool flag = false;
					foreach (BindingElement bindingElement in enumerable)
					{
						SecurityBindingElement securityBindingElement2 = bindingElement as SecurityBindingElement;
						if (securityBindingElement2 != null)
						{
							flag = true;
						}
						else if (flag)
						{
							collection.Add(bindingElement);
						}
					}
					bool flag2 = false;
					if (collection.Count != 0)
					{
						BindingContext bindingContext = new BindingContext(new CustomBinding(collection), new BindingParameterCollection());
						ISecurityCapabilities innerProperty = bindingContext.GetInnerProperty<ISecurityCapabilities>();
						if (innerProperty != null && innerProperty.SupportsServerAuthentication && innerProperty.SupportedRequestProtectionLevel == ProtectionLevel.EncryptAndSign && innerProperty.SupportedResponseProtectionLevel == ProtectionLevel.EncryptAndSign)
						{
							flag2 = true;
						}
					}
					if (!flag2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportDoesNotProtectMessage", new object[]
						{
							binding.Name,
							binding.Namespace,
							contract.Name,
							contract.Namespace
						})));
					}
				}
			}

			// Token: 0x060077CA RID: 30666 RVA: 0x001C00B0 File Offset: 0x001BE2B0
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C5E RID: 3166
		private static class IssuedKeySizeCompatibilityWithAlgorithmSuiteRule
		{
			// Token: 0x060077CB RID: 30667 RVA: 0x001C00B4 File Offset: 0x001BE2B4
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				SecurityAlgorithmSuite defaultAlgorithmSuite = sbe.DefaultAlgorithmSuite;
				foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
				{
					if (securityTokenParameters is IssuedSecurityTokenParameters)
					{
						IssuedSecurityTokenParameters issuedSecurityTokenParameters = securityTokenParameters as IssuedSecurityTokenParameters;
						if (issuedSecurityTokenParameters.KeySize != 0)
						{
							bool flag = true;
							if (issuedSecurityTokenParameters.KeyType == SecurityKeyType.SymmetricKey && !sbe.DefaultAlgorithmSuite.IsSymmetricKeyLengthSupported(issuedSecurityTokenParameters.KeySize))
							{
								flag = false;
							}
							else if (issuedSecurityTokenParameters.KeyType == SecurityKeyType.AsymmetricKey && !sbe.DefaultAlgorithmSuite.IsAsymmetricKeyLengthSupported(issuedSecurityTokenParameters.KeySize))
							{
								flag = false;
							}
							if (!flag)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuedKeySizeNotCompatibleWithAlgorithmSuite", new object[]
								{
									binding.Name,
									binding.Namespace,
									sbe.DefaultAlgorithmSuite,
									issuedSecurityTokenParameters.KeySize
								})));
							}
						}
					}
					else if (securityTokenParameters is SecureConversationSecurityTokenParameters)
					{
						SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = securityTokenParameters as SecureConversationSecurityTokenParameters;
						SecurityValidationBehavior.IssuedKeySizeCompatibilityWithAlgorithmSuiteRule.ValidateSecurityBinding(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement, binding, contract);
					}
				}
			}

			// Token: 0x060077CC RID: 30668 RVA: 0x001C01D4 File Offset: 0x001BE3D4
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C5F RID: 3167
		private static class SecurityTokenParameterInclusionModeRule
		{
			// Token: 0x060077CD RID: 30669 RVA: 0x001C01D8 File Offset: 0x001BE3D8
			private static void EnforceInclusionMode(Binding binding, SecurityTokenParameters stp, params SecurityTokenInclusionMode[] allowedInclusionModes)
			{
				bool flag = false;
				for (int i = 0; i < allowedInclusionModes.Length; i++)
				{
					if (stp.InclusionMode == allowedInclusionModes[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityTokenParametersHasIncompatibleInclusionMode", new object[]
					{
						binding.Name,
						binding.Namespace,
						stp.GetType(),
						stp.InclusionMode,
						allowedInclusionModes[0]
					})));
				}
			}

			// Token: 0x060077CE RID: 30670 RVA: 0x001C025C File Offset: 0x001BE45C
			public static void Validate(SecurityBindingElement sbe, Binding binding, ContractDescription contract, KeyedByTypeCollection<IServiceBehavior> behaviors)
			{
				if (behaviors != null)
				{
					ServiceCredentials serviceCredentials = behaviors.Find<ServiceCredentials>();
					if (serviceCredentials != null && serviceCredentials.GetType() != typeof(ServiceCredentials))
					{
						return;
					}
				}
				SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
				AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = sbe as AsymmetricSecurityBindingElement;
				foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
				{
					if (securityTokenParameters is RsaSecurityTokenParameters)
					{
						SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.EnforceInclusionMode(binding, securityTokenParameters, new SecurityTokenInclusionMode[]
						{
							SecurityTokenInclusionMode.Never
						});
					}
					else
					{
						if (securityTokenParameters is SecureConversationSecurityTokenParameters)
						{
							SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.Validate(((SecureConversationSecurityTokenParameters)securityTokenParameters).BootstrapSecurityBindingElement, binding, contract, behaviors);
						}
						if (symmetricSecurityBindingElement != null)
						{
							if (symmetricSecurityBindingElement.ProtectionTokenParameters == securityTokenParameters && securityTokenParameters.HasAsymmetricKey)
							{
								SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.EnforceInclusionMode(binding, securityTokenParameters, new SecurityTokenInclusionMode[]
								{
									SecurityTokenInclusionMode.Never
								});
							}
							else
							{
								SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.EnforceInclusionMode(binding, securityTokenParameters, new SecurityTokenInclusionMode[]
								{
									SecurityTokenInclusionMode.AlwaysToRecipient,
									SecurityTokenInclusionMode.Once
								});
							}
						}
						else if (asymmetricSecurityBindingElement != null)
						{
							if (asymmetricSecurityBindingElement.InitiatorTokenParameters == securityTokenParameters && securityTokenParameters.HasAsymmetricKey)
							{
								SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.EnforceInclusionMode(binding, securityTokenParameters, new SecurityTokenInclusionMode[]
								{
									SecurityTokenInclusionMode.AlwaysToRecipient,
									SecurityTokenInclusionMode.AlwaysToInitiator,
									SecurityTokenInclusionMode.Once
								});
							}
							else
							{
								SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.EnforceInclusionMode(binding, securityTokenParameters, new SecurityTokenInclusionMode[]
								{
									SecurityTokenInclusionMode.AlwaysToRecipient,
									SecurityTokenInclusionMode.Once
								});
							}
						}
						else
						{
							SecurityValidationBehavior.SecurityTokenParameterInclusionModeRule.EnforceInclusionMode(binding, securityTokenParameters, new SecurityTokenInclusionMode[]
							{
								SecurityTokenInclusionMode.AlwaysToRecipient,
								SecurityTokenInclusionMode.Once
							});
						}
					}
				}
			}
		}

		// Token: 0x02000C60 RID: 3168
		private static class SecurityVersionSupportForEncryptedKeyBindingRule
		{
			// Token: 0x060077CF RID: 30671 RVA: 0x001C03AC File Offset: 0x001BE5AC
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
				if (sbe.MessageSecurityVersion.SecurityVersion == SecurityVersion.WSSecurity10 && symmetricSecurityBindingElement != null && symmetricSecurityBindingElement.ProtectionTokenParameters != null && symmetricSecurityBindingElement.ProtectionTokenParameters.HasAsymmetricKey)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityVersionDoesNotSupportEncryptedKeyBinding", new object[]
					{
						binding.Name,
						binding.Namespace,
						contract.Name,
						contract.Namespace,
						SecurityVersion.WSSecurity11
					})));
				}
			}

			// Token: 0x060077D0 RID: 30672 RVA: 0x001C0436 File Offset: 0x001BE636
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C61 RID: 3169
		private static class SecurityVersionSupportForThumbprintKeyIdentifierClauseRule
		{
			// Token: 0x060077D1 RID: 30673 RVA: 0x001C0438 File Offset: 0x001BE638
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				if (sbe.MessageSecurityVersion.SecurityVersion == SecurityVersion.WSSecurity10)
				{
					foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe))
					{
						X509SecurityTokenParameters x509SecurityTokenParameters = securityTokenParameters as X509SecurityTokenParameters;
						if (x509SecurityTokenParameters != null && x509SecurityTokenParameters.X509ReferenceStyle == X509KeyIdentifierClauseType.Thumbprint)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityVersionDoesNotSupportThumbprintX509KeyIdentifierClause", new object[]
							{
								binding.Name,
								binding.Namespace,
								contract.Name,
								contract.Namespace,
								SecurityVersion.WSSecurity11
							})));
						}
					}
				}
			}

			// Token: 0x060077D2 RID: 30674 RVA: 0x001C04F4 File Offset: 0x001BE6F4
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C62 RID: 3170
		private static class MessageSecurityAndManualAddressingRule
		{
			// Token: 0x060077D3 RID: 30675 RVA: 0x001C04F8 File Offset: 0x001BE6F8
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				TransportBindingElement transportBindingElement = binding.CreateBindingElements().Find<TransportBindingElement>();
				if (transportBindingElement != null && transportBindingElement.ManualAddressing)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageSecurityDoesNotWorkWithManualAddressing", new object[]
					{
						binding.Name,
						binding.Namespace
					})));
				}
			}

			// Token: 0x060077D4 RID: 30676 RVA: 0x001C054E File Offset: 0x001BE74E
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C63 RID: 3171
		private static class SecurityBindingSupportForOneWayOnlyRule
		{
			// Token: 0x060077D5 RID: 30677 RVA: 0x001C0550 File Offset: 0x001BE750
			public static void ValidateSecurityBinding(SecurityBindingElement sbe, Binding binding, ContractDescription contract)
			{
				if (sbe is AsymmetricSecurityBindingElement && ((AsymmetricSecurityBindingElement)sbe).IsCertificateSignatureBinding)
				{
					for (int i = 0; i < contract.Operations.Count; i++)
					{
						OperationDescription operationDescription = contract.Operations[i];
						if (!operationDescription.IsOneWay)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityBindingSupportsOneWayOnly", new object[]
							{
								binding.Name,
								binding.Namespace,
								contract.Name,
								contract.Namespace
							})));
						}
					}
				}
			}

			// Token: 0x060077D6 RID: 30678 RVA: 0x001C05E1 File Offset: 0x001BE7E1
			public static void ValidateNoSecurityBinding(Binding binding, ContractDescription contract)
			{
			}
		}

		// Token: 0x02000C64 RID: 3172
		private static class MissingClientCertificateRule
		{
			// Token: 0x060077D7 RID: 30679 RVA: 0x001C05E4 File Offset: 0x001BE7E4
			private static void ValidateCore(ServiceDescription description, ServiceCredentials credentials)
			{
				for (int i = 0; i < description.Endpoints.Count; i++)
				{
					ServiceEndpoint serviceEndpoint = description.Endpoints[i];
					BindingElementCollection bindingElementCollection = serviceEndpoint.Binding.CreateBindingElements();
					SecurityBindingElement securityBindingElement = bindingElementCollection.Find<SecurityBindingElement>();
					CompositeDuplexBindingElement compositeDuplexBindingElement = bindingElementCollection.Find<CompositeDuplexBindingElement>();
					if (securityBindingElement != null && compositeDuplexBindingElement != null && SecurityBindingElement.IsMutualCertificateDuplexBinding(securityBindingElement) && credentials.ClientCertificate.Certificate == null)
					{
						ProtectionLevel protectionLevel;
						ProtectionLevel protectionLevel2;
						SecurityValidationBehavior.ContractProtectionRequirementsRule.GetRequiredProtectionLevels(serviceEndpoint.Contract, securityBindingElement.GetIndividualProperty<ISecurityCapabilities>().SupportedRequestProtectionLevel, securityBindingElement.GetIndividualProperty<ISecurityCapabilities>().SupportedResponseProtectionLevel, out protectionLevel, out protectionLevel2);
						if (protectionLevel2 == ProtectionLevel.EncryptAndSign)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoClientCertificate", new object[]
							{
								serviceEndpoint.Binding.Name,
								serviceEndpoint.Binding.Namespace
							})));
						}
					}
				}
			}

			// Token: 0x060077D8 RID: 30680 RVA: 0x001C06B7 File Offset: 0x001BE8B7
			public static void Validate(ServiceDescription description)
			{
				if (!description.Behaviors.Contains(typeof(ServiceCredentials)))
				{
					return;
				}
				SecurityValidationBehavior.MissingClientCertificateRule.ValidateCore(description, description.Behaviors.Find<ServiceCredentials>());
			}
		}

		// Token: 0x02000C65 RID: 3173
		private static class UsernameImpersonationRule
		{
			// Token: 0x060077D9 RID: 30681 RVA: 0x001C06E4 File Offset: 0x001BE8E4
			[MethodImpl(MethodImplOptions.NoInlining)]
			private static void ValidateCore(ServiceDescription description, ServiceCredentials credentials)
			{
				if (credentials.UserNameAuthentication.UserNamePasswordValidationMode == UserNamePasswordValidationMode.Windows)
				{
					return;
				}
				ServiceAuthorizationBehavior serviceAuthorizationBehavior = description.Behaviors.Find<ServiceAuthorizationBehavior>();
				bool flag = serviceAuthorizationBehavior != null && serviceAuthorizationBehavior.ImpersonateCallerForAllOperations;
				for (int i = 0; i < description.Endpoints.Count; i++)
				{
					ServiceEndpoint serviceEndpoint = description.Endpoints[i];
					if (!serviceEndpoint.InternalIsSystemEndpoint(description) && SecurityValidationBehavior.ValidatorUtils.IsStandardBinding(serviceEndpoint.Binding))
					{
						bool flag2 = flag;
						if (!flag2)
						{
							flag2 = SecurityValidationBehavior.ValidatorUtils.EndpointRequiresImpersonation(serviceEndpoint);
						}
						if (flag2)
						{
							ICollection<BindingElement> collection = serviceEndpoint.Binding.CreateBindingElements();
							foreach (BindingElement bindingElement in collection)
							{
								SecurityBindingElement securityBindingElement = bindingElement as SecurityBindingElement;
								if (securityBindingElement != null)
								{
									SecurityValidationBehavior.UsernameImpersonationRule.ValidateSecurityBindingElement(securityBindingElement, serviceEndpoint);
									break;
								}
							}
						}
					}
				}
			}

			// Token: 0x060077DA RID: 30682 RVA: 0x001C07CC File Offset: 0x001BE9CC
			public static void Validate(ServiceDescription description)
			{
				ServiceCredentials serviceCredentials = description.Behaviors.Find<ServiceCredentials>();
				if (serviceCredentials == null)
				{
					return;
				}
				SecurityValidationBehavior.UsernameImpersonationRule.ValidateCore(description, serviceCredentials);
			}

			// Token: 0x060077DB RID: 30683 RVA: 0x001C07F0 File Offset: 0x001BE9F0
			private static void ValidateSecurityBindingElement(SecurityBindingElement sbe, ServiceEndpoint endpoint)
			{
				if (sbe == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sbe");
				}
				if (endpoint == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
				}
				foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
				{
					if (securityTokenParameters is UserNameSecurityTokenParameters)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotPerformImpersonationOnUsernameToken", new object[]
						{
							endpoint.Binding.Name,
							endpoint.Binding.Namespace,
							endpoint.Contract.Name,
							endpoint.Contract.Namespace
						})));
					}
					if (securityTokenParameters is SecureConversationSecurityTokenParameters)
					{
						SecurityValidationBehavior.UsernameImpersonationRule.ValidateSecurityBindingElement(((SecureConversationSecurityTokenParameters)securityTokenParameters).BootstrapSecurityBindingElement, endpoint);
					}
				}
			}
		}

		// Token: 0x02000C66 RID: 3174
		private static class ValidatorUtils
		{
			// Token: 0x060077DC RID: 30684 RVA: 0x001C08DC File Offset: 0x001BEADC
			public static bool EndpointRequiresImpersonation(ServiceEndpoint endpoint)
			{
				if (endpoint == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
				}
				for (int i = 0; i < endpoint.Contract.Operations.Count; i++)
				{
					OperationDescription operationDescription = endpoint.Contract.Operations[i];
					OperationBehaviorAttribute operationBehaviorAttribute = operationDescription.Behaviors.Find<OperationBehaviorAttribute>();
					if (operationBehaviorAttribute != null && operationBehaviorAttribute.Impersonation == ImpersonationOption.Required)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060077DD RID: 30685 RVA: 0x001C0944 File Offset: 0x001BEB44
			public static bool IsStandardBinding(Binding binding)
			{
				return binding is BasicHttpBinding || binding is BasicHttpsBinding || binding is NetTcpBinding || binding is NetMsmqBinding || binding is NetNamedPipeBinding || binding is NetPeerTcpBinding || binding is WSDualHttpBinding || binding is WSFederationHttpBinding || binding is WSHttpBinding || binding is NetHttpBinding || binding is NetHttpsBinding;
			}
		}
	}
}
