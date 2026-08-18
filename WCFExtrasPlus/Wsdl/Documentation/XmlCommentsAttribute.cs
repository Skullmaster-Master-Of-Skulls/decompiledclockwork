using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Interface)]
	public class XmlCommentsAttribute : Attribute, IContractBehavior, IWsdlExportExtension
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003A19 File Offset: 0x00001C19
		public XmlCommentsAttribute()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003A21 File Offset: 0x00001C21
		public XmlCommentsAttribute(XmlCommentFormat format)
		{
			this.format = format;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A30 File Offset: 0x00001C30
		public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003A32 File Offset: 0x00001C32
		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A34 File Offset: 0x00001C34
		public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003A36 File Offset: 0x00001C36
		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003A38 File Offset: 0x00001C38
		public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
			XmlCommentsExporter.ExportContract(exporter, context, this.Format);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A47 File Offset: 0x00001C47
		public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			XmlCommentsExporter.ExportEndpoint(exporter, this.Format);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003A8F File Offset: 0x00001C8F
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
