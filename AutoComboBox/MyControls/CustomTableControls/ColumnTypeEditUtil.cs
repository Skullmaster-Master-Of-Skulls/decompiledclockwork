using System;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200005C RID: 92
	public class ColumnTypeEditUtil
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0001A028 File Offset: 0x00019028
		public static ColumnTypeEditorPanel getEditor(int e, ColumnTypeDef editee)
		{
			return ColumnTypeEditUtil.Factories[e].getInstance(editee);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001A047 File Offset: 0x00019047
		public static void Release(Control panel)
		{
			panel.Parent.Controls.Remove(panel);
			panel.Dispose();
		}

		// Token: 0x04000327 RID: 807
		public static readonly ColumnTypeEditorFactory[] Factories = new ColumnTypeEditorFactory[]
		{
			new DroplistEditorFactory(),
			new NotesEditorFactory(),
			new WhoenteredEditorFactory(),
			new DateEditorFactory(),
			new FileNameEditorFactory(),
			new CheckBoxEditorFactory()
		};
	}
}
