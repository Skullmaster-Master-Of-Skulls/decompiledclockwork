using System;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000507 RID: 1287
	internal class ContentPlaceHolderBuilderFactory : IWebObjectFactory
	{
		// Token: 0x06003EBE RID: 16062 RVA: 0x001053B9 File Offset: 0x001043B9
		object IWebObjectFactory.CreateInstance()
		{
			return new ContentPlaceHolderBuilder();
		}
	}
}
