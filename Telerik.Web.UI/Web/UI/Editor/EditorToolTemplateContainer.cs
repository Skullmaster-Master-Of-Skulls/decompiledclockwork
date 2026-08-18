using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000E86 RID: 3718
	internal class EditorToolTemplateContainer : WebControl
	{
		// Token: 0x06008CF8 RID: 36088 RVA: 0x0020015D File Offset: 0x001FE35D
		public EditorToolTemplateContainer(EditorTool toolValue)
		{
			this.tool = toolValue;
		}

		// Token: 0x06008CF9 RID: 36089 RVA: 0x0020016C File Offset: 0x001FE36C
		protected override void Render(HtmlTextWriter writer)
		{
			this.tool.RenderControl(writer);
		}

		// Token: 0x04002795 RID: 10133
		private readonly EditorTool tool;
	}
}
