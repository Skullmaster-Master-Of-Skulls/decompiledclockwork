using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002DF RID: 735
	internal class MobileToolAdapter : DefaultToolAdapter
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x00054652 File Offset: 0x00052852
		public MobileToolAdapter()
		{
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005465A File Offset: 0x0005285A
		public MobileToolAdapter(RadEditor editor) : base(editor)
		{
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x00054663 File Offset: 0x00052863
		public override string ClientType
		{
			get
			{
				return "Telerik.Web.UI.Editor.MobileToolAdapter";
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0005466A File Offset: 0x0005286A
		public override void PreRender()
		{
			base.PreRender();
			this.SetDefaultTabName();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00054678 File Offset: 0x00052878
		public virtual void SetDefaultTabName()
		{
			EditorToolGroupCollection tools = base.Editor.Tools;
			foreach (object obj in tools)
			{
				EditorToolGroup editorToolGroup = (EditorToolGroup)obj;
				editorToolGroup.Tab = (string.IsNullOrEmpty(editorToolGroup.Tab) ? base.Editor.Localization.Tools.GetString("Home", false) : editorToolGroup.Tab);
			}
		}
	}
}
