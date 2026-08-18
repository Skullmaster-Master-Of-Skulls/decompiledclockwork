using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCFExtras.Wsdl.Documentation;

namespace WCFExtras.Wsdl
{
	// Token: 0x02000002 RID: 2
	public class WsdlExtensions : IEndpointBehavior, IWsdlExportExtension
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002056 File Offset: 0x00000256
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000205C File Offset: 0x0000025C
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002073 File Offset: 0x00000273
		public Uri Location { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002093 File Offset: 0x00000293
		public XmlCommentFormat ExportXmlComments { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020B3 File Offset: 0x000002B3
		public bool SingleFile { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x000020BC File Offset: 0x000002BC
		internal WsdlExtensions(WsdlExtensionsConfig config)
		{
			this.Location = config.Location;
			this.SingleFile = config.SingleFile;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E1 File Offset: 0x000002E1
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E4 File Offset: 0x000002E4
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E8 File Offset: 0x000002E8
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			if (this.SingleFile)
			{
				SingleFileExporter.ExportEndpoint(exporter);
			}
			if (this.Location != null)
			{
				LocationOverrideExporter.ExportEndpoint(exporter, context, this.Location);
			}
		}
	}
}
