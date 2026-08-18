using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x020008EC RID: 2284
	public class TabStripLiteRenderer : TabStripBaseRenderer
	{
		// Token: 0x06005655 RID: 22101 RVA: 0x00108638 File Offset: 0x00106838
		public TabStripLiteRenderer(RadTabStrip owner) : base(owner)
		{
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x00108644 File Offset: 0x00106844
		public override string CssClassFormatString
		{
			get
			{
				List<string> list = new List<string>
				{
					"RadTabStrip",
					"RadTabStrip_{0}"
				};
				if (base.IsHorizontal)
				{
					list.Add("rtsHorizontal");
				}
				else
				{
					list.Add("rtsVertical");
				}
				switch (base.Owner.Orientation)
				{
				case TabStripOrientation.HorizontalTop:
					list.Add("rtsTop");
					break;
				case TabStripOrientation.HorizontalBottom:
					list.Add("rtsBottom");
					break;
				case TabStripOrientation.VerticalRight:
					list.Add("rtsRight");
					break;
				case TabStripOrientation.VerticalLeft:
					list.Add("rtsLeft");
					break;
				}
				switch (base.Owner.Align)
				{
				case TabStripAlign.Center:
					list.Add("rtsAlignCenter");
					break;
				case TabStripAlign.Right:
					list.Add("rtsAlignRight");
					break;
				case TabStripAlign.Justify:
					list.Add("rtsAlignJustify");
					break;
				}
				if (base.Owner.Attributes["dir"] == "rtl")
				{
					list.Add("RadTabStrip_rtl");
				}
				if (!base.Owner.Enabled)
				{
					list.Add("rtsDisabled");
				}
				return string.Join(" ", list.ToArray());
			}
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x001087F0 File Offset: 0x001069F0
		protected override void RenderLevel(HtmlTextWriter writer, IEnumerable<IList<RadTab>> levelOfTabs, int level, Action<StringBuilder> action = null)
		{
			base.RenderLevel(writer, levelOfTabs, level, delegate(StringBuilder builder)
			{
				if (!levelOfTabs.Any(delegate(IList<RadTab> tabs)
				{
					IRadTabContainer owner = tabs[0].Owner;
					RadTab radTab = owner as RadTab;
					return radTab == null || !RadTabStrip.ChildrenShouldBeHidden(radTab);
				}))
				{
					builder.Append(" rtsHidden");
				}
			});
		}
	}
}
