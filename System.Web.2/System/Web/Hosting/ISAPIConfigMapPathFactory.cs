using System;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	// Token: 0x020007BE RID: 1982
	[Serializable]
	internal class ISAPIConfigMapPathFactory : IConfigMapPathFactory
	{
		// Token: 0x06005F0F RID: 24335 RVA: 0x00148550 File Offset: 0x00146750
		IConfigMapPath IConfigMapPathFactory.Create(string virtualPath, string physicalPath)
		{
			return IISMapPath.GetInstance();
		}
	}
}
