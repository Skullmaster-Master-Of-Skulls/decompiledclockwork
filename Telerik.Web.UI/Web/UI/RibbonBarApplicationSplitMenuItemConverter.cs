using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000782 RID: 1922
	internal class RibbonBarApplicationSplitMenuItemConverter : RibbonBarApplicationMenuItemBaseConverter
	{
		// Token: 0x060043BC RID: 17340 RVA: 0x000D3D28 File Offset: 0x000D1F28
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			IDictionary<string, object> dictionary = base.Serialize(obj, serializer);
			RibbonBarApplicationSplitMenuItem ribbonBarApplicationSplitMenuItem = obj as RibbonBarApplicationSplitMenuItem;
			if (ribbonBarApplicationSplitMenuItem.Items.Count > 0)
			{
				dictionary["items"] = ribbonBarApplicationSplitMenuItem.Items;
			}
			return dictionary;
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000D3E34 File Offset: 0x000D2034
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarApplicationSplitMenuItem);
				yield break;
			}
		}
	}
}
