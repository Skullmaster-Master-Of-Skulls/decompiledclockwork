using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x020008EE RID: 2286
	public class TabLiteRenderer : TabRendererBase
	{
		// Token: 0x06005668 RID: 22120 RVA: 0x00108B4C File Offset: 0x00106D4C
		internal TabLiteRenderer(RadTab tab) : base(tab)
		{
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x00108BFE File Offset: 0x00106DFE
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer, delegate(List<string> cssClass)
			{
				if (!string.IsNullOrEmpty(base.Tab.CssClass))
				{
					cssClass.Add(base.Tab.CssClass);
				}
				if (base.Tab.Selected)
				{
					cssClass.Add("rtsSelected");
					if (!string.IsNullOrEmpty(base.Tab.SelectedCssClass))
					{
						cssClass.Add(base.Tab.SelectedCssClass);
					}
				}
				if (!base.Tab.Enabled)
				{
					cssClass.Add("rtsDisabled");
					if (!string.IsNullOrEmpty(base.Tab.DisabledCssClass))
					{
						cssClass.Add(base.Tab.DisabledCssClass);
					}
				}
			});
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x00108C64 File Offset: 0x00106E64
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.Tab.Templated)
			{
				this.RenderDiv(writer, delegate
				{
					this.RenderImage(writer);
					this.RenderTemplateContent(writer);
				});
				return;
			}
			Action action = delegate()
			{
				this.RenderImage(writer);
				this.RenderText(writer);
			};
			if (!string.IsNullOrEmpty(base.Tab.NavigateUrl))
			{
				this.RenderLink(writer, action);
				return;
			}
			this.RenderSpan(writer, action);
		}
	}
}
