using System;
using System.Collections.ObjectModel;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.MsmqIntegration;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B2 RID: 1458
	internal class PartialTrustValidationBehavior : IServiceBehavior, IEndpointBehavior
	{
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060038ED RID: 14573 RVA: 0x000DC758 File Offset: 0x000DA958
		internal static PartialTrustValidationBehavior Instance
		{
			get
			{
				if (PartialTrustValidationBehavior.instance == null)
				{
					PartialTrustValidationBehavior.instance = new PartialTrustValidationBehavior();
				}
				return PartialTrustValidationBehavior.instance;
			}
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000DC770 File Offset: 0x000DA970
		private void ValidateEndpoint(ServiceEndpoint endpoint)
		{
			Binding binding = endpoint.Binding;
			if (binding != null)
			{
				new PartialTrustValidationBehavior.BindingValidator(endpoint.Binding).Validate();
			}
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000DC79A File Offset: 0x000DA99A
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.ValidateEndpoint(endpoint);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000DC7B6 File Offset: 0x000DA9B6
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000DC7B8 File Offset: 0x000DA9B8
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000DC7BA File Offset: 0x000DA9BA
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000DC7BC File Offset: 0x000DA9BC
		public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			for (int i = 0; i < description.Endpoints.Count; i++)
			{
				ServiceEndpoint serviceEndpoint = description.Endpoints[i];
				if (serviceEndpoint != null)
				{
					this.ValidateEndpoint(serviceEndpoint);
				}
			}
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000DC809 File Offset: 0x000DAA09
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000DC80B File Offset: 0x000DAA0B
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x040029C2 RID: 10690
		private static PartialTrustValidationBehavior instance;

		// Token: 0x02000CB3 RID: 3251
		private struct BindingValidator
		{
			// Token: 0x06007967 RID: 31079 RVA: 0x001C52E3 File Offset: 0x001C34E3
			internal BindingValidator(Binding binding)
			{
				this.binding = binding;
			}

			// Token: 0x06007968 RID: 31080 RVA: 0x001C52EC File Offset: 0x001C34EC
			internal void Validate()
			{
				Type type = this.binding.GetType();
				if (this.IsUnsupportedBindingType(type))
				{
					this.UnsupportedSecurityCheck("FullTrustOnlyBindingSecurityCheck1", type);
				}
				string resource = typeof(WSHttpBinding).IsAssignableFrom(type) ? "FullTrustOnlyBindingElementSecurityCheckWSHttpBinding1" : "FullTrustOnlyBindingElementSecurityCheck1";
				BindingElementCollection bindingElementCollection = this.binding.CreateBindingElements();
				foreach (BindingElement bindingElement in bindingElementCollection)
				{
					Type type2 = bindingElement.GetType();
					if (bindingElement != null && this.IsUnsupportedBindingElementType(type2))
					{
						this.UnsupportedSecurityCheck(resource, type2);
					}
				}
			}

			// Token: 0x06007969 RID: 31081 RVA: 0x001C53A4 File Offset: 0x001C35A4
			private bool IsUnsupportedBindingType(Type bindingType)
			{
				for (int i = 0; i < PartialTrustValidationBehavior.BindingValidator.unsupportedBindings.Length; i++)
				{
					if (PartialTrustValidationBehavior.BindingValidator.unsupportedBindings[i] == bindingType)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600796A RID: 31082 RVA: 0x001C53D8 File Offset: 0x001C35D8
			private bool IsUnsupportedBindingElementType(Type bindingElementType)
			{
				for (int i = 0; i < PartialTrustValidationBehavior.BindingValidator.unsupportedBindingElements.Length; i++)
				{
					if (PartialTrustValidationBehavior.BindingValidator.unsupportedBindingElements[i] == bindingElementType)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600796B RID: 31083 RVA: 0x001C540C File Offset: 0x001C360C
			private void UnsupportedSecurityCheck(string resource, Type type)
			{
				try
				{
					PartialTrustValidationBehavior.BindingValidator.fullTrust.Demand();
				}
				catch (SecurityException)
				{
					throw new InvalidOperationException(SR.GetString(resource, new object[]
					{
						this.binding.Name,
						type
					}));
				}
			}

			// Token: 0x04004536 RID: 17718
			private static Type[] unsupportedBindings = new Type[]
			{
				typeof(NetNamedPipeBinding),
				typeof(WSDualHttpBinding),
				typeof(WS2007FederationHttpBinding),
				typeof(WSFederationHttpBinding),
				typeof(NetMsmqBinding),
				typeof(NetPeerTcpBinding),
				typeof(MsmqIntegrationBinding)
			};

			// Token: 0x04004537 RID: 17719
			private static Type[] unsupportedBindingElements = new Type[]
			{
				typeof(AsymmetricSecurityBindingElement),
				typeof(CompositeDuplexBindingElement),
				typeof(MsmqTransportBindingElement),
				typeof(NamedPipeTransportBindingElement),
				typeof(OneWayBindingElement),
				typeof(PeerCustomResolverBindingElement),
				typeof(PeerTransportBindingElement),
				typeof(PnrpPeerResolverBindingElement),
				typeof(ReliableSessionBindingElement),
				typeof(SymmetricSecurityBindingElement),
				typeof(TransportSecurityBindingElement),
				typeof(MtomMessageEncodingBindingElement)
			};

			// Token: 0x04004538 RID: 17720
			private Binding binding;

			// Token: 0x04004539 RID: 17721
			private static readonly PermissionSet fullTrust = new PermissionSet(PermissionState.Unrestricted);
		}
	}
}
