using System;

namespace System.ServiceModel.Description
{
	// Token: 0x02000403 RID: 1027
	public interface IPolicyExportExtension
	{
		// Token: 0x06002738 RID: 10040
		void ExportPolicy(MetadataExporter exporter, PolicyConversionContext context);
	}
}
