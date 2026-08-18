using System;
using System.Collections.Specialized;
using System.IO;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001A5 RID: 421
	internal interface IAzureProvider : ICloudStorageProvider
	{
		// Token: 0x06000F63 RID: 3939
		void UploadFileOnChunks(string keyName, NameValueCollection metaData, Stream fileStream);

		// Token: 0x06000F64 RID: 3940
		void UploadFileOnSigleRequest(string keyName, NameValueCollection metaData, Stream fileStream);
	}
}
