using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Services.Description;

namespace System.ServiceModel.Description
{
	// Token: 0x02000428 RID: 1064
	public class WsdlContractConversionContext
	{
		// Token: 0x0600291B RID: 10523 RVA: 0x0009C4C4 File Offset: 0x0009A6C4
		internal WsdlContractConversionContext(ContractDescription contract, PortType wsdlPortType)
		{
			this.contract = contract;
			this.wsdlPortType = wsdlPortType;
			this.wsdlOperations = new Dictionary<OperationDescription, Operation>();
			this.operationDescriptions = new Dictionary<Operation, OperationDescription>();
			this.wsdlOperationMessages = new Dictionary<MessageDescription, OperationMessage>();
			this.messageDescriptions = new Dictionary<OperationMessage, MessageDescription>();
			this.wsdlOperationFaults = new Dictionary<FaultDescription, OperationFault>();
			this.faultDescriptions = new Dictionary<OperationFault, FaultDescription>();
			this.operationBindings = new Dictionary<Operation, Collection<OperationBinding>>();
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x0009C534 File Offset: 0x0009A734
		internal IEnumerable<IWsdlExportExtension> ExportExtensions
		{
			get
			{
				foreach (IWsdlExportExtension wsdlExportExtension in this.contract.Behaviors.FindAll<IWsdlExportExtension>())
				{
					yield return wsdlExportExtension;
				}
				IEnumerator<IWsdlExportExtension> enumerator = null;
				foreach (OperationDescription operationDescription in this.contract.Operations)
				{
					if (WsdlExporter.OperationIsExportable(operationDescription))
					{
						Collection<IWsdlExportExtension> extensions = operationDescription.Behaviors.FindAll<IWsdlExportExtension>();
						int i = 0;
						while (i < extensions.Count)
						{
							if (WsdlExporter.IsBuiltInOperationBehavior(extensions[i]))
							{
								yield return extensions[i];
								extensions.RemoveAt(i);
							}
							else
							{
								int num = i;
								i = num + 1;
							}
						}
						foreach (IWsdlExportExtension wsdlExportExtension2 in extensions)
						{
							yield return wsdlExportExtension2;
						}
						enumerator = null;
						extensions = null;
					}
				}
				IEnumerator<OperationDescription> enumerator2 = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x0009C551 File Offset: 0x0009A751
		public ContractDescription Contract
		{
			get
			{
				return this.contract;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x0009C559 File Offset: 0x0009A759
		public PortType WsdlPortType
		{
			get
			{
				return this.wsdlPortType;
			}
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0009C561 File Offset: 0x0009A761
		public Operation GetOperation(OperationDescription operation)
		{
			return this.wsdlOperations[operation];
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0009C56F File Offset: 0x0009A76F
		public OperationMessage GetOperationMessage(MessageDescription message)
		{
			return this.wsdlOperationMessages[message];
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0009C57D File Offset: 0x0009A77D
		public OperationFault GetOperationFault(FaultDescription fault)
		{
			return this.wsdlOperationFaults[fault];
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x0009C58B File Offset: 0x0009A78B
		public OperationDescription GetOperationDescription(Operation operation)
		{
			return this.operationDescriptions[operation];
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x0009C599 File Offset: 0x0009A799
		public MessageDescription GetMessageDescription(OperationMessage operationMessage)
		{
			return this.messageDescriptions[operationMessage];
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x0009C5A7 File Offset: 0x0009A7A7
		public FaultDescription GetFaultDescription(OperationFault operationFault)
		{
			return this.faultDescriptions[operationFault];
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0009C5B5 File Offset: 0x0009A7B5
		internal void AddOperation(OperationDescription operationDescription, Operation wsdlOperation)
		{
			this.wsdlOperations.Add(operationDescription, wsdlOperation);
			this.operationDescriptions.Add(wsdlOperation, operationDescription);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x0009C5D1 File Offset: 0x0009A7D1
		internal void AddMessage(MessageDescription messageDescription, OperationMessage wsdlOperationMessage)
		{
			this.wsdlOperationMessages.Add(messageDescription, wsdlOperationMessage);
			this.messageDescriptions.Add(wsdlOperationMessage, messageDescription);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x0009C5ED File Offset: 0x0009A7ED
		internal void AddFault(FaultDescription faultDescription, OperationFault wsdlOperationFault)
		{
			this.wsdlOperationFaults.Add(faultDescription, wsdlOperationFault);
			this.faultDescriptions.Add(wsdlOperationFault, faultDescription);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x0009C60C File Offset: 0x0009A80C
		internal Collection<OperationBinding> GetOperationBindings(Operation operation)
		{
			Collection<OperationBinding> collection;
			if (!this.operationBindings.TryGetValue(operation, out collection))
			{
				collection = new Collection<OperationBinding>();
				ServiceDescriptionCollection serviceDescriptions = this.WsdlPortType.ServiceDescription.ServiceDescriptions;
				foreach (object obj in serviceDescriptions)
				{
					ServiceDescription serviceDescription = (ServiceDescription)obj;
					foreach (object obj2 in serviceDescription.Bindings)
					{
						Binding binding = (Binding)obj2;
						if (binding.Type.Name == this.WsdlPortType.Name && binding.Type.Namespace == this.WsdlPortType.ServiceDescription.TargetNamespace)
						{
							foreach (object obj3 in binding.Operations)
							{
								OperationBinding operationBinding = (OperationBinding)obj3;
								if (WsdlImporter.Binding2DescriptionHelper.Match(operationBinding, operation) != WsdlImporter.Binding2DescriptionHelper.MatchResult.None)
								{
									collection.Add(operationBinding);
									break;
								}
							}
						}
					}
				}
				this.operationBindings.Add(operation, collection);
			}
			return collection;
		}

		// Token: 0x0400226B RID: 8811
		private readonly ContractDescription contract;

		// Token: 0x0400226C RID: 8812
		private readonly PortType wsdlPortType;

		// Token: 0x0400226D RID: 8813
		private readonly Dictionary<OperationDescription, Operation> wsdlOperations;

		// Token: 0x0400226E RID: 8814
		private readonly Dictionary<Operation, OperationDescription> operationDescriptions;

		// Token: 0x0400226F RID: 8815
		private readonly Dictionary<MessageDescription, OperationMessage> wsdlOperationMessages;

		// Token: 0x04002270 RID: 8816
		private readonly Dictionary<FaultDescription, OperationFault> wsdlOperationFaults;

		// Token: 0x04002271 RID: 8817
		private readonly Dictionary<OperationMessage, MessageDescription> messageDescriptions;

		// Token: 0x04002272 RID: 8818
		private readonly Dictionary<OperationFault, FaultDescription> faultDescriptions;

		// Token: 0x04002273 RID: 8819
		private readonly Dictionary<Operation, Collection<OperationBinding>> operationBindings;
	}
}
