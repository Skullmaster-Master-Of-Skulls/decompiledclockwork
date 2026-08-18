using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B1 RID: 1457
	internal class UniqueContractNameValidationBehavior : IServiceBehavior
	{
		// Token: 0x060038EA RID: 14570 RVA: 0x000DC65C File Offset: 0x000DA85C
		public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (serviceHostBase == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceHostBase");
			}
			foreach (ServiceEndpoint serviceEndpoint in description.Endpoints)
			{
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(serviceEndpoint.Contract.Name, serviceEndpoint.Contract.Namespace);
				if (!this.contracts.ContainsKey(xmlQualifiedName))
				{
					this.contracts.Add(xmlQualifiedName, serviceEndpoint.Contract);
				}
				else if (this.contracts[xmlQualifiedName] != serviceEndpoint.Contract)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMultipleContractsWithSameName", new object[]
					{
						xmlQualifiedName.Name,
						xmlQualifiedName.Namespace
					})));
				}
			}
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000DC754 File Offset: 0x000DA954
		public void AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000DC756 File Offset: 0x000DA956
		public void ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x040029C1 RID: 10689
		private Dictionary<XmlQualifiedName, ContractDescription> contracts = new Dictionary<XmlQualifiedName, ContractDescription>();
	}
}
