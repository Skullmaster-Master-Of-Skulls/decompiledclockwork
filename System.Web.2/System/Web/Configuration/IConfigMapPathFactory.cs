using System;

namespace System.Web.Configuration
{
	// Token: 0x02000706 RID: 1798
	public interface IConfigMapPathFactory
	{
		// Token: 0x060056D9 RID: 22233
		IConfigMapPath Create(string virtualPath, string physicalPath);
	}
}
