using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002CB RID: 715
	internal class ClassicToolRenderer : ToolRendererBase
	{
		// Token: 0x060018E5 RID: 6373 RVA: 0x00052853 File Offset: 0x00050A53
		public ClassicToolRenderer(EditorTool owner) : base(owner)
		{
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005285C File Offset: 0x00050A5C
		public override void AddIconAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, base.Owner.Name);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00052871 File Offset: 0x00050A71
		public override void AddTextAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reButton_text");
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00052880 File Offset: 0x00050A80
		public override string CssClassString
		{
			get
			{
				return string.Format(this.CssClassFormatString, this.GetCssClassString());
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00052894 File Offset: 0x00050A94
		public override string GetCssClassString()
		{
			string str = "";
			string str2;
			if (base.Owner.ShowText)
			{
				str2 = "reTool_text";
			}
			else
			{
				str2 = "reTool";
			}
			return str + str2;
		}
	}
}
