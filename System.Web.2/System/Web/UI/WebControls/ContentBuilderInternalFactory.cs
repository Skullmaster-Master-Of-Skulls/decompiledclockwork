using System;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A0 RID: 928
	internal class ContentBuilderInternalFactory : IWebObjectFactory
	{
		// Token: 0x06002C59 RID: 11353 RVA: 0x00090B7E File Offset: 0x0008ED7E
		object IWebObjectFactory.CreateInstance()
		{
			return new ContentBuilderInternal();
		}
	}
}
