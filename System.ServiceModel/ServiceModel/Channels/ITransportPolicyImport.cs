using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008AA RID: 2218
	internal interface ITransportPolicyImport
	{
		// Token: 0x060054A5 RID: 21669
		void ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext);
	}
}
