using System;
using System.Diagnostics;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001FA RID: 506
	internal static class Util
	{
		// Token: 0x06002150 RID: 8528 RVA: 0x000755A5 File Offset: 0x000737A5
		internal static void ThrowIfReadOnly(MetadataItem item)
		{
			if (item.IsReadOnly)
			{
				throw EntityUtil.OperationOnReadOnlyItem();
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000755B5 File Offset: 0x000737B5
		[Conditional("DEBUG")]
		internal static void AssertItemHasIdentity(MetadataItem item, string argumentName)
		{
			EntityUtil.GenericCheckArgumentNull<MetadataItem>(item, argumentName);
		}
	}
}
