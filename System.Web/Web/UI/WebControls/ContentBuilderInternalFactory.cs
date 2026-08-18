using System;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000503 RID: 1283
	internal class ContentBuilderInternalFactory : IWebObjectFactory
	{
		// Token: 0x06003EA7 RID: 16039 RVA: 0x00105119 File Offset: 0x00104119
		object IWebObjectFactory.CreateInstance()
		{
			return new ContentBuilderInternal();
		}
	}
}
