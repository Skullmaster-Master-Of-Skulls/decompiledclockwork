using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000267 RID: 615
	[Guid("80182d03-5ea4-4831-ae97-55beffc2e590")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServicePartitionConfig
	{
		// Token: 0x0600119A RID: 4506
		void PartitionConfig(PartitionOption partitionConfig);

		// Token: 0x0600119B RID: 4507
		void PartitionID([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidPartitionID);
	}
}
