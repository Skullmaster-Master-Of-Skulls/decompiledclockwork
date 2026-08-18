using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D8 RID: 728
	public class HeaderToolsToolAdapter : ToolAdapter
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x00053134 File Offset: 0x00051334
		public HeaderToolsToolAdapter()
		{
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005313C File Offset: 0x0005133C
		public HeaderToolsToolAdapter(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00053145 File Offset: 0x00051345
		public override string ClientType
		{
			get
			{
				return "Telerik.Web.UI.Editor.HeaderToolsToolAdapter";
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005314C File Offset: 0x0005134C
		public override void PreRender()
		{
			this._toolbar = new HeaderToolsToolBar();
			foreach (object obj in base.Editor.HeaderTools)
			{
				EditorHeaderTool item = (EditorHeaderTool)obj;
				this._toolbar.Items.Add(item);
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000531C0 File Offset: 0x000513C0
		public override void Render(HtmlTextWriter writer)
		{
			this._toolbar.RenderControl(writer);
		}

		// Token: 0x04000690 RID: 1680
		private HeaderToolsToolBar _toolbar;
	}
}
