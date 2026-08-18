using System;
using System.Web.Configuration;
using Telerik.Web.UI.CloudUpload;

namespace Telerik.Web.UI
{
	// Token: 0x020001B6 RID: 438
	internal static class CloudProviderFactory
	{
		// Token: 0x06001015 RID: 4117 RVA: 0x0003B30D File Offset: 0x0003950D
		static CloudProviderFactory()
		{
			CloudProviderFactory._providersConfigurations = (CloudUploadConfigurationSection)WebConfigurationManager.GetSection("telerik.web.ui/radCloudUpload");
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003B330 File Offset: 0x00039530
		public static ICloudStorageProvider GetProvider(string name, Type type)
		{
			ICloudStorageProvider result;
			lock (CloudProviderFactory._lock)
			{
				result = (ICloudStorageProvider)ProvidersHelper.InstantiateProvider(CloudProviderFactory._providersConfigurations.StorageProviders[name], type);
			}
			return result;
		}

		// Token: 0x04000492 RID: 1170
		private static CloudUploadConfigurationSection _providersConfigurations;

		// Token: 0x04000493 RID: 1171
		private static readonly object _lock = new object();
	}
}
