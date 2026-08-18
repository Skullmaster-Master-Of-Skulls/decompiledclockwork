using System;
using System.Web.UI;

namespace Telerik.Web.UI.PageLayout
{
	// Token: 0x0200063F RID: 1599
	public class LayoutColumnCollection : BaseContainerCollection<LayoutColumn>
	{
		// Token: 0x06003A83 RID: 14979 RVA: 0x000BEFDE File Offset: 0x000BD1DE
		public LayoutColumnCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000BEFE8 File Offset: 0x000BD1E8
		public override void Add(LayoutColumn child)
		{
			TagName htmlTag = ((IMutableRendering)base.Parent).HtmlTag;
			if (htmlTag == TagName.Ol || htmlTag == TagName.Ul)
			{
				child.HtmlTag = TagName.Li;
			}
			base.Add(child);
		}
	}
}
