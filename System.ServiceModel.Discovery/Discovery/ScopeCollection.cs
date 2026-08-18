using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200004C RID: 76
	internal class ScopeCollection : NonNullItemCollection<Uri>
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000B39F File Offset: 0x0000959F
		protected override void InsertItem(int index, Uri item)
		{
			if (item != null && !item.IsAbsoluteUri)
			{
				throw FxTrace.Exception.Argument("item", SR.DiscoveryArgumentInvalidScopeUri(item));
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B3D0 File Offset: 0x000095D0
		protected override void SetItem(int index, Uri item)
		{
			if (item != null && !item.IsAbsoluteUri)
			{
				throw FxTrace.Exception.Argument("item", SR.DiscoveryArgumentInvalidScopeUri(item));
			}
			base.SetItem(index, item);
		}
	}
}
