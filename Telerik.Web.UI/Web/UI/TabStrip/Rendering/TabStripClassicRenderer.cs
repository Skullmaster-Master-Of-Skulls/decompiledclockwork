using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x020008EB RID: 2283
	public class TabStripClassicRenderer : TabStripBaseRenderer
	{
		// Token: 0x0600564D RID: 22093 RVA: 0x00108334 File Offset: 0x00106534
		public TabStripClassicRenderer(RadTabStrip owner) : base(owner)
		{
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x00108340 File Offset: 0x00106540
		public override string CssClassFormatString
		{
			get
			{
				string text = string.Empty;
				switch (base.Owner.Orientation)
				{
				case TabStripOrientation.HorizontalTop:
					text = "RadTabStrip RadTabStrip_{0} RadTabStripTop_{0} RadTabStripTop";
					if (base.Owner.ShowBaseLine)
					{
						text += " RadTabStripTop_{0}_Baseline";
					}
					break;
				case TabStripOrientation.HorizontalBottom:
					text = "RadTabStrip RadTabStrip_{0} RadTabStripBottom_{0} RadTabStripBottom";
					if (base.Owner.ShowBaseLine)
					{
						text += " RadTabStripBottom_{0}_Baseline";
					}
					break;
				case TabStripOrientation.VerticalRight:
					text = "RadTabStripVertical RadTabStrip_{0} RadTabStripRight_{0} RadTabStripRight";
					break;
				case TabStripOrientation.VerticalLeft:
					text = "RadTabStripVertical RadTabStrip_{0} RadTabStripLeft_{0} RadTabStripLeft";
					break;
				}
				if (base.Owner.EnableSubLevelStyles)
				{
					text += " RadTabStrip_{0}_SimpleSubItems";
				}
				if (base.Owner.Attributes["dir"] == "rtl")
				{
					text += " RadTabStrip_rtl RadTabStrip_{0}_rtl";
					switch (base.Owner.Orientation)
					{
					case TabStripOrientation.HorizontalTop:
						text += " RadTabStripTop_{0}_rtl";
						break;
					case TabStripOrientation.HorizontalBottom:
						text += " RadTabStripBottom_{0}_rtl";
						break;
					case TabStripOrientation.VerticalRight:
						text += " RadTabStripRight_{0}_rtl";
						break;
					case TabStripOrientation.VerticalLeft:
						text += " RadTabStripLeft_{0}_rtl";
						break;
					}
				}
				if (!base.Owner.Enabled)
				{
					text += " RadTabStrip_{0}_disabled";
				}
				return text;
			}
		}

		// Token: 0x0600564F RID: 22095 RVA: 0x00108484 File Offset: 0x00106684
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.Owner.Align == TabStripAlign.Justify || (!base.IsHorizontal && base.Owner.Align != TabStripAlign.Left))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
				writer.RenderBeginTag(HtmlTextWriterTag.Script);
				writer.Write(string.Format("Telerik.Web.UI.RadTabStrip._align('{0}', {1}, {2});", base.Owner.ClientID, (int)base.Owner.Align, (int)base.Owner.Orientation));
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005650 RID: 22096 RVA: 0x00108511 File Offset: 0x00106711
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.Owner.ScrollChildren)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
			}
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x0010859E File Offset: 0x0010679E
		protected override void RenderLevel(HtmlTextWriter writer, IEnumerable<IList<RadTab>> levelOfTabs, int level, Action<StringBuilder> action = null)
		{
			base.RenderLevel(writer, levelOfTabs, level, delegate(StringBuilder builder)
			{
				if (!base.Owner.Width.IsEmpty && base.IsVertical)
				{
					builder.Append(" rtsHasWidth");
				}
				if (base.Owner.Align == TabStripAlign.Center)
				{
					builder.Append(" rtsCenter");
				}
				if (base.Owner.Align == TabStripAlign.Right)
				{
					builder.Append(" rtsRight");
				}
			});
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x00108622 File Offset: 0x00106822
		protected override void RenderTabList(HtmlTextWriter writer, IList<RadTab> tabs, Action<StringBuilder, IRadTabContainer> action = null)
		{
			base.RenderTabList(writer, tabs, delegate(StringBuilder builder, IRadTabContainer owner)
			{
				if (owner.ScrollChildren && owner.ScrollPosition != 0)
				{
					builder.Append("position:relative;");
					if (base.IsHorizontal)
					{
						builder.AppendFormat("left:{0}", Unit.Pixel(owner.ScrollPosition));
						return;
					}
					builder.AppendFormat("top:{0}", Unit.Pixel(owner.ScrollPosition));
				}
			});
		}
	}
}
