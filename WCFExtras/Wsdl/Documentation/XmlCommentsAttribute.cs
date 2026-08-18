using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Interface)]
	public class XmlCommentsAttribute : Attribute, IContractBehavior, IWsdlExportExtension
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003EA9 File Offset: 0x000020A9
		public XmlCommentsAttribute()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003EB4 File Offset: 0x000020B4
		public XmlCommentsAttribute(XmlCommentFormat format)
		{
			this.format = format;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003EC6 File Offset: 0x000020C6
		public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003EC9 File Offset: 0x000020C9
		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003ECC File Offset: 0x000020CC
		public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003ECF File Offset: 0x000020CF
		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003ED2 File Offset: 0x000020D2
		public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
			XmlCommentsExporter.ExportContract(exporter, context, this.Format);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003EE3 File Offset: 0x000020E3
		public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			XmlCommentsExporter.ExportEndpoint(exporter, this.Format);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003EF4 File Offset: 0x000020F4
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003F39 File Offset: 0x00002139
		public XmlCommentFormat Format
		{
			get
			{
				if (!this.initialized)
				{
					this.initialized = true;
					XmlCommentsConfig configuration = XmlCommentsConfig.GetConfiguration();
					if (configuration != null)
					{
						this.format = configuration.Format;
					}
				}
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x0400001B RID: 27
		private bool initialized;

		// Token: 0x0400001C RID: 28
		private XmlCommentFormat format;
	}
}
