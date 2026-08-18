using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002CC RID: 716
	internal class ClassicDropDownRenderer : ClassicToolRenderer
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x000528CF File Offset: 0x00050ACF
		public ClassicDropDownRenderer(EditorDropDown owner) : base(owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x000528DF File Offset: 0x00050ADF
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x000528E7 File Offset: 0x00050AE7
		public new EditorDropDown Owner { get; private set; }

		// Token: 0x060018ED RID: 6381 RVA: 0x000528F0 File Offset: 0x00050AF0
		public override void AddTextAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Owner.Width != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Owner.Width.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Owner.Name);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00052948 File Offset: 0x00050B48
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderToolText(writer);
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00052951 File Offset: 0x00050B51
		public override string CssClassString
		{
			get
			{
				return "reDropdown";
			}
		}
	}
}
