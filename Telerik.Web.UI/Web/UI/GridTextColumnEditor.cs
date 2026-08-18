using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000365 RID: 869
	public abstract class GridTextColumnEditor : GridColumnEditorBase
	{
		// Token: 0x06001DF0 RID: 7664 RVA: 0x0005D361 File Offset: 0x0005B561
		public GridTextColumnEditor()
		{
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001DF1 RID: 7665
		// (set) Token: 0x06001DF2 RID: 7666
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract string Text { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0005D369 File Offset: 0x0005B569
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x0005D371 File Offset: 0x0005B571
		[Description("The ToolTip that will be applied to the GridTextColumnEditor control")]
		public string ToolTip
		{
			get
			{
				return this.toolTip;
			}
			set
			{
				this.toolTip = value;
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0005D37C File Offset: 0x0005B57C
		internal override void CopySettingsFrom(IGridColumnEditor editor)
		{
			base.CopySettingsFrom(editor);
			GridTextColumnEditor gridTextColumnEditor = editor as GridTextColumnEditor;
			if (gridTextColumnEditor != null)
			{
				this.ToolTip = gridTextColumnEditor.ToolTip;
			}
		}

		// Token: 0x04000765 RID: 1893
		private string toolTip;
	}
}
