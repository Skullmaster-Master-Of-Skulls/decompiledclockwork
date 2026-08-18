using System;

namespace System.ServiceModel.Description
{
	// Token: 0x02000406 RID: 1030
	public interface IWsdlExportExtension
	{
		// Token: 0x0600273B RID: 10043
		void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context);

		// Token: 0x0600273C RID: 10044
		void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context);
	}
}
