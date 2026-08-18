using System;
using System.Collections;

namespace System.Web.UI.Design
{
	// Token: 0x02000011 RID: 17
	public sealed class ClientScriptItemCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000035F8 File Offset: 0x000017F8
		public ClientScriptItemCollection(ClientScriptItem[] clientScriptItems)
		{
			if (clientScriptItems != null)
			{
				foreach (ClientScriptItem value in clientScriptItems)
				{
					base.InnerList.Add(value);
				}
			}
		}
	}
}
