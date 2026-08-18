using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration
{
	// Token: 0x0200070E RID: 1806
	[ComVisible(true)]
	[Guid("A99B591A-23C6-4238-8452-C7B0E895063D")]
	public interface IRemoteWebConfigurationHostServer
	{
		// Token: 0x0600570C RID: 22284
		byte[] GetData(string fileName, bool getReadTimeOnly, out long readTime);

		// Token: 0x0600570D RID: 22285
		void WriteData(string fileName, string templateFileName, byte[] data, ref long readTime);

		// Token: 0x0600570E RID: 22286
		string GetFilePaths(int webLevel, string path, string site, string locationSubPath);

		// Token: 0x0600570F RID: 22287
		string DoEncryptOrDecrypt(bool doEncrypt, string xmlString, string protectionProviderName, string protectionProviderType, string[] parameterKeys, string[] parameterValues);

		// Token: 0x06005710 RID: 22288
		void GetFileDetails(string name, out bool exists, out long size, out long createDate, out long lastWriteDate);
	}
}
