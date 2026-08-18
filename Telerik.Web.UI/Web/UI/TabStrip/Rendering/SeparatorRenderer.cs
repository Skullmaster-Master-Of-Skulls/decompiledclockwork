using System;
using System.Web.UI;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x02001ADD RID: 6877
	public class SeparatorRenderer : TabRendererBase
	{
		// Token: 0x06010AC0 RID: 68288 RVA: 0x003B71A9 File Offset: 0x003B53A9
		internal SeparatorRenderer(RadTab tab) : base(tab)
		{
		}

		// Token: 0x06010AC1 RID: 68289 RVA: 0x003B71B4 File Offset: 0x003B53B4
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Tab.CssClass;
			base.Tab.CssClass = string.Format("rtsLI rtsSeparator {0}", base.Tab.CssClass);
			base.Tab.AddAttributes(writer);
			base.Tab.CssClass = cssClass;
		}

		// Token: 0x06010AC2 RID: 68290 RVA: 0x003B7205 File Offset: 0x003B5405
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.Write(base.Tab.Text);
		}
	}
}
