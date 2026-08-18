using System;
using System.Collections.Specialized;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001A4 RID: 420
	internal interface IAmazonS3Provider : ICloudStorageProvider
	{
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000F60 RID: 3936
		string UploadedPartETag { get; }

		// Token: 0x06000F61 RID: 3937
		string InitiateMultiPartUpload(string keyName, NameValueCollection mataData);

		// Token: 0x06000F62 RID: 3938
		void AbortChunktUpload(string keyName, string uploadId);
	}
}
