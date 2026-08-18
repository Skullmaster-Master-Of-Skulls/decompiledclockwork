using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003BA RID: 954
	internal class MsmqIntegrationValidationBehavior : IEndpointBehavior, IServiceBehavior
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00082303 File Offset: 0x00080503
		internal static MsmqIntegrationValidationBehavior Instance
		{
			get
			{
				if (MsmqIntegrationValidationBehavior.instance == null)
				{
					MsmqIntegrationValidationBehavior.instance = new MsmqIntegrationValidationBehavior();
				}
				return MsmqIntegrationValidationBehavior.instance;
			}
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0008231B File Offset: 0x0008051B
		private MsmqIntegrationValidationBehavior()
		{
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00082324 File Offset: 0x00080524
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint");
			}
			ContractDescription contract = serviceEndpoint.Contract;
			Binding binding = serviceEndpoint.Binding;
			if (this.NeedValidateBinding(binding))
			{
				this.ValidateHelper(contract, binding, null);
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00082364 File Offset: 0x00080564
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00082366 File Offset: 0x00080566
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00082368 File Offset: 0x00080568
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0008236A File Offset: 0x0008056A
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0008236C File Offset: 0x0008056C
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00082370 File Offset: 0x00080570
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			for (int i = 0; i < description.Endpoints.Count; i++)
			{
				ServiceEndpoint serviceEndpoint = description.Endpoints[i];
				if (this.NeedValidateBinding(serviceEndpoint.Binding))
				{
					this.ValidateHelper(serviceEndpoint.Contract, serviceEndpoint.Binding, description);
					return;
				}
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000823D8 File Offset: 0x000805D8
		private bool NeedValidateBinding(Binding binding)
		{
			if (binding is MsmqIntegrationBinding)
			{
				return true;
			}
			if (binding is CustomBinding)
			{
				CustomBinding customBinding = new CustomBinding(binding);
				return customBinding.Elements.Find<MsmqIntegrationBindingElement>() != null;
			}
			return false;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00082410 File Offset: 0x00080610
		private void ValidateHelper(ContractDescription contract, Binding binding, ServiceDescription description)
		{
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				MessageDescription messageDescription = operationDescription.Messages[0];
				if (messageDescription.Body.Parts.Count != 0 || messageDescription.Headers.Count != 0)
				{
					if (messageDescription.Body.Parts.Count == 1)
					{
						Type type = messageDescription.Body.Parts[0].Type;
						if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(MsmqMessage<>))
						{
							continue;
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqInvalidServiceOperationForMsmqIntegrationBinding", new object[]
					{
						binding.Name,
						operationDescription.Name,
						contract.Name
					})));
				}
			}
		}

		// Token: 0x04002024 RID: 8228
		private static MsmqIntegrationValidationBehavior instance;
	}
}
