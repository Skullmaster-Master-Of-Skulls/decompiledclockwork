using System;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.Core.Azure.Adapters
{
	// Token: 0x02000005 RID: 5
	public static class AzureStorageAdaptercs
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003674 File Offset: 0x00001874
		public static string GetBlobName(this FileIdentifier fileId)
		{
			if (fileId == null)
			{
				return null;
			}
			if (fileId.FileUniqueId == null)
			{
				return null;
			}
			Guid? guid;
			return guid.GetValueOrDefault().ToString().ToLower();
		}
	}
}
