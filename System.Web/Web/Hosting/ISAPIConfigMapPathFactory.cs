using System;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	internal class ISAPIConfigMapPathFactory : IConfigMapPathFactory
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x00096868 File Offset: 0x00095868
		IConfigMapPath IConfigMapPathFactory.Create(string virtualPath, string physicalPath)
		{
			return IISMapPath.GetInstance();
		}
	}
}
