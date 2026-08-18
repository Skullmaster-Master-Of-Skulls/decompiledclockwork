using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000781 RID: 1921
	internal class RibbonBarApplicationMenuItemConverter : RibbonBarApplicationMenuItemBaseConverter
	{
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000D3D00 File Offset: 0x000D1F00
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarApplicationMenuItem);
				yield break;
			}
		}
	}
}
