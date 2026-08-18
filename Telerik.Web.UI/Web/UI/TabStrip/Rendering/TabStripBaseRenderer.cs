using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x020008EA RID: 2282
	public abstract class TabStripBaseRenderer : RendererBase
	{
		// Token: 0x06005644 RID: 22084 RVA: 0x0010808C File Offset: 0x0010628C
		public TabStripBaseRenderer(RadTabStrip owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06005645 RID: 22085 RVA: 0x0010809B File Offset: 0x0010629B
		// (set) Token: 0x06005646 RID: 22086 RVA: 0x001080A3 File Offset: 0x001062A3
		protected RadTabStrip Owner { get; set; }

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06005647 RID: 22087 RVA: 0x001080AC File Offset: 0x001062AC
		protected internal bool IsHorizontal
		{
			get
			{
				return this.Owner.Orientation == TabStripOrientation.HorizontalBottom || this.Owner.Orientation == TabStripOrientation.HorizontalTop;
			}
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06005648 RID: 22088 RVA: 0x001080CC File Offset: 0x001062CC
		protected internal bool IsVertical
		{
			get
			{
				return this.Owner.Orientation == TabStripOrientation.VerticalLeft || this.Owner.Orientation == TabStripOrientation.VerticalRight;
			}
		}

		// Token: 0x06005649 RID: 22089 RVA: 0x001080EC File Offset: 0x001062EC
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Owner.CallBaseAddAttributesToRender(writer);
		}

		// Token: 0x0600564A RID: 22090 RVA: 0x001080FC File Offset: 0x001062FC
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (this.Owner.InDesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this.Owner));
			}
			IList<IList<IList<RadTab>>> list = this.Owner.GroupTabsByLevel();
			if (this.Owner.Orientation != TabStripOrientation.HorizontalBottom)
			{
				for (int i = 0; i < list.Count; i++)
				{
					this.RenderLevel(writer, list[i], i + 1, null);
				}
				return;
			}
			for (int j = list.Count - 1; j >= 0; j--)
			{
				this.RenderLevel(writer, list[j], j + 1, null);
			}
		}

		// Token: 0x0600564B RID: 22091 RVA: 0x00108194 File Offset: 0x00106394
		protected virtual void RenderLevel(HtmlTextWriter writer, IEnumerable<IList<RadTab>> levelOfTabs, int level, Action<StringBuilder> action = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("rtsLevel rtsLevel{0}", level);
			if (action != null)
			{
				action(stringBuilder);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, stringBuilder.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			foreach (IList<RadTab> tabs in levelOfTabs)
			{
				this.RenderTabList(writer, tabs, null);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600564C RID: 22092 RVA: 0x00108220 File Offset: 0x00106420
		protected virtual void RenderTabList(HtmlTextWriter writer, IList<RadTab> tabs, Action<StringBuilder, IRadTabContainer> action = null)
		{
			IRadTabContainer owner = tabs[0].Owner;
			RadTab radTab = owner as RadTab;
			StringBuilder stringBuilder = new StringBuilder();
			if (radTab != null && RadTabStrip.ChildrenShouldBeHidden(radTab))
			{
				if (this.Owner.InDesignMode)
				{
					return;
				}
				stringBuilder.Append("display:none;");
			}
			StringBuilder stringBuilder2 = new StringBuilder("rtsUL");
			if (action != null)
			{
				action(stringBuilder, owner);
			}
			if (radTab != null && !string.IsNullOrEmpty(radTab.ChildGroupCssClass))
			{
				stringBuilder2.AppendFormat(" {0}", radTab.ChildGroupCssClass);
			}
			writer.WriteBeginTag("ul");
			writer.WriteAttribute("class", stringBuilder2.ToString());
			if (stringBuilder.ToString().Length > 0)
			{
				writer.WriteAttribute("style", stringBuilder.ToString());
			}
			writer.Write('>');
			foreach (RadTab radTab2 in tabs)
			{
				radTab2.RenderControl(writer);
			}
			writer.WriteEndTag("ul");
		}
	}
}
