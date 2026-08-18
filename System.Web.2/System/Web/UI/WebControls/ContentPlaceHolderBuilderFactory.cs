using System;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A4 RID: 932
	internal class ContentPlaceHolderBuilderFactory : IWebObjectFactory
	{
		// Token: 0x06002C70 RID: 11376 RVA: 0x00090DBD File Offset: 0x0008EFBD
		object IWebObjectFactory.CreateInstance()
		{
			return new ContentPlaceHolderBuilder();
		}
	}
}
