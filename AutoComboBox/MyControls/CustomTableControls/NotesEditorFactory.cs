using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000027 RID: 39
	public class NotesEditorFactory : ColumnTypeEditorFactory
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000C010 File Offset: 0x0000B010
		public override ColumnTypeEditorPanel getInstance(ColumnTypeDef editee)
		{
			return new NotesEditor(editee as NotesDef);
		}
	}
}
