using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Services.Description;

namespace System.ServiceModel.Description
{
	// Token: 0x02000429 RID: 1065
	public class WsdlEndpointConversionContext
	{
		// Token: 0x06002929 RID: 10537 RVA: 0x0009C788 File Offset: 0x0009A988
		internal WsdlEndpointConversionContext(WsdlContractConversionContext contractContext, ServiceEndpoint endpoint, Binding wsdlBinding, Port wsdlport)
		{
			this.endpoint = endpoint;
			this.wsdlBinding = wsdlBinding;
			this.wsdlPort = wsdlport;
			this.contractContext = contractContext;
			this.wsdlOperationBindings = new Dictionary<OperationDescription, OperationBinding>();
			this.operationDescriptionBindings = new Dictionary<OperationBinding, OperationDescription>();
			this.wsdlMessageBindings = new Dictionary<MessageDescription, MessageBinding>();
			this.messageDescriptionBindings = new Dictionary<MessageBinding, MessageDescription>();
			this.wsdlFaultBindings = new Dictionary<FaultDescription, FaultBinding>();
			this.faultDescriptionBindings = new Dictionary<FaultBinding, FaultDescription>();
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x0009C7FC File Offset: 0x0009A9FC
		internal WsdlEndpointConversionContext(WsdlEndpointConversionContext bindingContext, ServiceEndpoint endpoint, Port wsdlport)
		{
			this.endpoint = endpoint;
			this.wsdlBinding = bindingContext.WsdlBinding;
			this.wsdlPort = wsdlport;
			this.contractContext = bindingContext.contractContext;
			this.wsdlOperationBindings = bindingContext.wsdlOperationBindings;
			this.operationDescriptionBindings = bindingContext.operationDescriptionBindings;
			this.wsdlMessageBindings = bindingContext.wsdlMessageBindings;
			this.messageDescriptionBindings = bindingContext.messageDescriptionBindings;
			this.wsdlFaultBindings = bindingContext.wsdlFaultBindings;
			this.faultDescriptionBindings = bindingContext.faultDescriptionBindings;
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x0009C880 File Offset: 0x0009AA80
		internal IEnumerable<IWsdlExportExtension> ExportExtensions
		{
			get
			{
				foreach (IWsdlExportExtension wsdlExportExtension in this.endpoint.Behaviors.FindAll<IWsdlExportExtension>())
				{
					yield return wsdlExportExtension;
				}
				IEnumerator<IWsdlExportExtension> enumerator = null;
				foreach (IWsdlExportExtension wsdlExportExtension2 in this.endpoint.Binding.CreateBindingElements().FindAll<IWsdlExportExtension>())
				{
					yield return wsdlExportExtension2;
				}
				enumerator = null;
				foreach (IWsdlExportExtension wsdlExportExtension3 in this.endpoint.Contract.Behaviors.FindAll<IWsdlExportExtension>())
				{
					yield return wsdlExportExtension3;
				}
				enumerator = null;
				foreach (OperationDescription operationDescription in this.endpoint.Contract.Operations)
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
						foreach (IWsdlExportExtension wsdlExportExtension4 in extensions)
						{
							yield return wsdlExportExtension4;
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x0009C89D File Offset: 0x0009AA9D
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.endpoint;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x0009C8A5 File Offset: 0x0009AAA5
		public Binding WsdlBinding
		{
			get
			{
				return this.wsdlBinding;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x0009C8AD File Offset: 0x0009AAAD
		public Port WsdlPort
		{
			get
			{
				return this.wsdlPort;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x0009C8B5 File Offset: 0x0009AAB5
		public WsdlContractConversionContext ContractConversionContext
		{
			get
			{
				return this.contractContext;
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0009C8BD File Offset: 0x0009AABD
		public OperationBinding GetOperationBinding(OperationDescription operation)
		{
			return this.wsdlOperationBindings[operation];
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x0009C8CB File Offset: 0x0009AACB
		public MessageBinding GetMessageBinding(MessageDescription message)
		{
			return this.wsdlMessageBindings[message];
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x0009C8D9 File Offset: 0x0009AAD9
		public FaultBinding GetFaultBinding(FaultDescription fault)
		{
			return this.wsdlFaultBindings[fault];
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x0009C8E7 File Offset: 0x0009AAE7
		public OperationDescription GetOperationDescription(OperationBinding operationBinding)
		{
			return this.operationDescriptionBindings[operationBinding];
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0009C8F5 File Offset: 0x0009AAF5
		public MessageDescription GetMessageDescription(MessageBinding messageBinding)
		{
			return this.messageDescriptionBindings[messageBinding];
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x0009C903 File Offset: 0x0009AB03
		public FaultDescription GetFaultDescription(FaultBinding faultBinding)
		{
			return this.faultDescriptionBindings[faultBinding];
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x0009C911 File Offset: 0x0009AB11
		internal void AddOperationBinding(OperationDescription operationDescription, OperationBinding wsdlOperationBinding)
		{
			this.wsdlOperationBindings.Add(operationDescription, wsdlOperationBinding);
			this.operationDescriptionBindings.Add(wsdlOperationBinding, operationDescription);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x0009C92D File Offset: 0x0009AB2D
		internal void AddMessageBinding(MessageDescription messageDescription, MessageBinding wsdlMessageBinding)
		{
			this.wsdlMessageBindings.Add(messageDescription, wsdlMessageBinding);
			this.messageDescriptionBindings.Add(wsdlMessageBinding, messageDescription);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x0009C949 File Offset: 0x0009AB49
		internal void AddFaultBinding(FaultDescription faultDescription, FaultBinding wsdlFaultBinding)
		{
			this.wsdlFaultBindings.Add(faultDescription, wsdlFaultBinding);
			this.faultDescriptionBindings.Add(wsdlFaultBinding, faultDescription);
		}

		// Token: 0x04002274 RID: 8820
		private readonly ServiceEndpoint endpoint;

		// Token: 0x04002275 RID: 8821
		private readonly Binding wsdlBinding;

		// Token: 0x04002276 RID: 8822
		private readonly Port wsdlPort;

		// Token: 0x04002277 RID: 8823
		private readonly WsdlContractConversionContext contractContext;

		// Token: 0x04002278 RID: 8824
		private readonly Dictionary<OperationDescription, OperationBinding> wsdlOperationBindings;

		// Token: 0x04002279 RID: 8825
		private readonly Dictionary<OperationBinding, OperationDescription> operationDescriptionBindings;

		// Token: 0x0400227A RID: 8826
		private readonly Dictionary<MessageDescription, MessageBinding> wsdlMessageBindings;

		// Token: 0x0400227B RID: 8827
		private readonly Dictionary<FaultDescription, FaultBinding> wsdlFaultBindings;

		// Token: 0x0400227C RID: 8828
		private readonly Dictionary<MessageBinding, MessageDescription> messageDescriptionBindings;

		// Token: 0x0400227D RID: 8829
		private readonly Dictionary<FaultBinding, FaultDescription> faultDescriptionBindings;
	}
}
