using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B3 RID: 1459
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal class PeerValidationBehavior : IEndpointBehavior, IServiceBehavior
	{
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000DC815 File Offset: 0x000DAA15
		public static PeerValidationBehavior Instance
		{
			get
			{
				if (PeerValidationBehavior.instance == null)
				{
					PeerValidationBehavior.instance = new PeerValidationBehavior();
				}
				return PeerValidationBehavior.instance;
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000DC82D File Offset: 0x000DAA2D
		private PeerValidationBehavior()
		{
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000DC838 File Offset: 0x000DAA38
		private static bool IsRequestReplyContract(ContractDescription contract)
		{
			bool result = false;
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				if (operationDescription.Messages.Count > 1)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000DC894 File Offset: 0x000DAA94
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint");
			}
			ContractDescription contract = serviceEndpoint.Contract;
			Binding binding = serviceEndpoint.Binding;
			this.ValidateHelper(contract, binding);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000DC8CA File Offset: 0x000DAACA
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000DC8CC File Offset: 0x000DAACC
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000DC8CE File Offset: 0x000DAACE
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000DC8D0 File Offset: 0x000DAAD0
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000DC8D2 File Offset: 0x000DAAD2
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000DC8D4 File Offset: 0x000DAAD4
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			for (int i = 0; i < description.Endpoints.Count; i++)
			{
				ServiceEndpoint serviceEndpoint = description.Endpoints[i];
				this.ValidateHelper(serviceEndpoint.Contract, serviceEndpoint.Binding);
			}
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000DC929 File Offset: 0x000DAB29
		private void ValidateHelper(ContractDescription contract, Binding binding)
		{
			if (binding is NetPeerTcpBinding && PeerValidationBehavior.IsRequestReplyContract(contract))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BindingDoesnTSupportRequestReplyButContract1", new object[]
				{
					binding.Name
				})));
			}
		}

		// Token: 0x040029C3 RID: 10691
		private static PeerValidationBehavior instance;
	}
}
