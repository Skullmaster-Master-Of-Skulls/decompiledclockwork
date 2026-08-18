using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WCFExtrasPlus.Wsdl.Documentation;

namespace WCFExtrasPlus.Wsdl
{
	// Token: 0x02000023 RID: 35
	public class WsdlExtensions : IEndpointBehavior, IWsdlExportExtension
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005829 File Offset: 0x00003A29
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000582B File Offset: 0x00003A2B
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000582D File Offset: 0x00003A2D
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000582F File Offset: 0x00003A2F
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005837 File Offset: 0x00003A37
		public Uri Location { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005840 File Offset: 0x00003A40
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005848 File Offset: 0x00003A48
		public XmlCommentFormat ExportXmlComments { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005851 File Offset: 0x00003A51
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005859 File Offset: 0x00003A59
		public bool SingleFile { get; set; }

		// Token: 0x060000D3 RID: 211 RVA: 0x00005862 File Offset: 0x00003A62
		public WsdlExtensions()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000586A File Offset: 0x00003A6A
		internal WsdlExtensions(WsdlExtensionsConfig config)
		{
			this.Location = config.Location;
			this.SingleFile = config.SingleFile;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000588A File Offset: 0x00003A8A
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000588C File Offset: 0x00003A8C
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000588E File Offset: 0x00003A8E
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			if (this.SingleFile)
			{
				SingleFileExporter.ExportEndpoint(exporter);
			}
			else
			{
				FlatWsdl.ExportEndpoint(exporter);
			}
			if (this.Location != null)
			{
				LocationOverrideExporter.ExportEndpoint(exporter, context, this.Location);
			}
		}
	}
}
