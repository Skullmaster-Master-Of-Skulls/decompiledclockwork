using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002C RID: 44
	internal static class EdmFunctionExtensions
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000B7C8 File Offset: 0x000099C8
		internal static bool IsCSpace(this EdmFunction function)
		{
			MetadataProperty metadataProperty = function.MetadataProperties.FirstOrDefault((MetadataProperty p) => p.Name == "DataSpace");
			return metadataProperty != null && (DataSpace)metadataProperty.Value == DataSpace.CSpace;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B811 File Offset: 0x00009A11
		internal static bool IsCanonicalFunction(this EdmFunction function)
		{
			return function.IsCSpace() && function.NamespaceName == "Edm";
		}
	}
}
