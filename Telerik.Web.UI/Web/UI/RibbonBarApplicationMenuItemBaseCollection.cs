using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020007BF RID: 1983
	public class RibbonBarApplicationMenuItemBaseCollection : List<RibbonBarApplicationMenuItemBase>
	{
		// Token: 0x06004525 RID: 17701 RVA: 0x000DAD00 File Offset: 0x000D8F00
		public RibbonBarApplicationMenuItemBaseCollection()
		{
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x000DAD08 File Offset: 0x000D8F08
		public RibbonBarApplicationMenuItemBaseCollection(RadRibbonBar control)
		{
			this.RibbonBar = control;
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x000DAD17 File Offset: 0x000D8F17
		// (set) Token: 0x06004528 RID: 17704 RVA: 0x000DAD1F File Offset: 0x000D8F1F
		public RadRibbonBar RibbonBar { get; internal set; }

		// Token: 0x06004529 RID: 17705 RVA: 0x000DAD28 File Offset: 0x000D8F28
		public new void Add(RibbonBarApplicationMenuItemBase item)
		{
			if (!base.Contains(item))
			{
				item.RibbonBar = this.RibbonBar;
				base.Add(item);
			}
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000DAD46 File Offset: 0x000D8F46
		public new void Insert(int index, RibbonBarApplicationMenuItemBase item)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				item.RibbonBar = this.RibbonBar;
			}
			base.Insert(index, item);
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x000DAD80 File Offset: 0x000D8F80
		public new void AddRange(IEnumerable<RibbonBarApplicationMenuItemBase> collection)
		{
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in collection)
			{
				ribbonBarApplicationMenuItemBase.RibbonBar = this.RibbonBar;
			}
			base.AddRange(collection);
		}
	}
}
